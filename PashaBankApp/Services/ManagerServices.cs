using Azure.Core;
using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using BCrypt.Net;
using PashaBankApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using PashaBankApp.Validation.Regexi;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PashaBankApp.Services
{
    public class ManagerServices : IManager
    {
        public readonly DbRaisa dbraisa;
        private readonly ErrorServices error;
        private readonly LogServices log;
        private readonly UserManager<Manager> _managUser;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IConfiguration config;
        public ManagerServices(DbRaisa dbraisa, UserManager<Manager> manager, RoleManager<IdentityRole> rol, IConfiguration config)
        {
            this.dbraisa = dbraisa;
            error = new ErrorServices(dbraisa);
            log = new LogServices(dbraisa);
            _managUser = manager;
            _rolemanager = rol;
            this.config = config;
        }
        #region RegistrationManager
        public async Task<bool> RegistrationManager(InsertManager signUp)
        {
            using (var transaction = dbraisa.Database.BeginTransaction())
            {
                try
                {
                    if (!RegexForValidate.NameIsMatch(signUp.FirstName) || !RegexForValidate.SurnameIsMatch(signUp.LastName))
                    {
                        transaction.Rollback();
                        Console.WriteLine("regex failed");
                        return false;
                    }
                   /* if (!RegexForValidate.EmailIsMatch(signUp.Mail) || !RegexForValidate.PhoneIsMatch(signUp.PhoneNumber))
                    {
                        transaction.Rollback();
                        Console.WriteLine("regex failed");
                        return false;
                    }*/

                    Manager manage = new Manager()
                    {
                        Email = signUp.Mail,
                        UserName = signUp.UserName,
                        PhoneNumber = signUp.PhoneNumber,
                        LastName = signUp.LastName,
                        FirstName = signUp.FirstName,
                        PersonalNumber = signUp.PersonalNumber
                    };
                    var res = await _managUser.CreateAsync(manage, signUp.Password);
                    if (res.Succeeded)
                    {
                        Console.WriteLine("warmatebuliii");

                        string role = signUp.Role.ToUpper();
                        if (role == "ADMIN" || role == "USER" || role == "MODERATOR" || role == "GUEST" || role == "MANAGER"||role=="OPERATOR")
                        {
                            await Console.Out.WriteLineAsync("roli  validuria");

                            var roleExists = await _rolemanager.RoleExistsAsync(signUp.Role.ToUpper());


                            if (!roleExists)
                            {
                                var roleResult = await _rolemanager.CreateAsync(new IdentityRole(signUp.Role.ToUpper()));

                                if (!roleResult.Succeeded)
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }

                            var resultofroleasign = await _managUser.AddToRoleAsync(manage, signUp.Role.ToUpper());
                            await dbraisa.SaveChangesAsync();
                            if (resultofroleasign.Succeeded)
                            {
                                transaction.Commit();
                                log.ActionLog("Manager  Succesfully installed to the system");
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }transaction.Rollback();
                    return false;
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                    throw;
                }
            }
        }
        #endregion

        #region SignIn

       
        public async Task<string> SignIn(GetManagerAuthent manAuth)
        {
            try
            {
                Console.WriteLine(manAuth.UserName);
                var res = await _managUser.FindByNameAsync(manAuth.UserName);
                if (res == null) return null;

                var checkedpass = await _managUser.CheckPasswordAsync(res, manAuth.Password);
                if (checkedpass)
                {
                    var roli = await _managUser.GetRolesAsync(res);
                    if (roli.FirstOrDefault() != null)
                    {
                        await Console.Out.WriteLineAsync(roli.First());
                        var re = await GenerateJwtToken(res, roli.First());
                        if (re == null) return null;
                        return re;
                    }
                    await Console.Out.WriteLineAsync("role is null there");
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
     
        }

        private async Task<string> GenerateJwtToken(Manager user, string role)
        {
            if (user != null)
            {
                var claims = new[]
                {
              new Claim(ClaimTypes.NameIdentifier, user.Id),
              new Claim(ClaimTypes.Name,user.UserName),
              new Claim(ClaimTypes.Role,role),
              new Claim(ClaimTypes.Email,user.Email),
              new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:key").Value));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: config.GetSection("Jwt:Issuer").Value,
                    audience: config.GetSection("Jwt:Audience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }
        #endregion

    }
}