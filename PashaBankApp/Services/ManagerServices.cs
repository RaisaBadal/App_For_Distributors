using Azure.Core;
using PashaBankApp.Cookies;
using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using BCrypt.Net;

namespace PashaBankApp.Services
{
    public class ManagerServices:IManager
    {
        public readonly DbRaisa dbraisa;
        private readonly ErrorServices error;
        private readonly LogServices log;
        private readonly CookiesForManager cookiesForManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ManagerServices(DbRaisa dbraisa,IHttpContextAccessor ihhtp)
        {
            this.dbraisa = dbraisa;
            error=new ErrorServices(dbraisa);
            log=new LogServices(dbraisa);
            httpContextAccessor = ihhtp;
            cookiesForManager = new CookiesForManager(dbraisa, httpContextAccessor);
        }
        #region RegistrationManager
        public bool RegistrationManager(InsertManager signUp)
        {
            try
            {
               
                var managerautID = 0;
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(signUp.Password, salt);
                var sig = new Models.Manager
                {
                    PersonalNumber = signUp.PersonalNumber,
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    PhoneNumber = signUp.PhoneNumber,
                    Mail = signUp.Mail
                };
                var sign = dbraisa.manager.Where(a => a.PersonalNumber == signUp.PersonalNumber).FirstOrDefault();
                if (sign == null)
                {
                    dbraisa.manager.Add(sig);
                    Console.WriteLine("test1");
                    dbraisa.SaveChanges();
                    Console.WriteLine("test2");
                    managerautID = dbraisa.manager.Max(a => a.ID);
                    log.ActionLog($"User is successfully registered,ID {sig.ID}");
                  
                }
                else
                {
                    error.Action("Such manager is already registered", Enums.ErrorTypeEnum.error);
                    return false;

                    // return true;
                }
                Console.WriteLine(hashedPassword);
                var managerauth=dbraisa.managerAuthentification.Where(a=>a.UserName==signUp.UserName).FirstOrDefault();
                Console.WriteLine("test 3");
                if (managerauth == null)
                {
                    
                    var auth = new Models.ManagerAuthentification
                    {
                        ManagerID = managerautID,
                        UserName = signUp.UserName,
                        Password = hashedPassword
                        //Password= signUp.Password
                    };
                    Console.WriteLine("test4");
                    dbraisa.managerAuthentification.Add(auth);
                    dbraisa.SaveChanges(true);
                    Console.WriteLine("test 5");
                    log.ActionLog($"Info is successfully add in Authentification table, ID {auth.ID}");
                    return true;
                   
                }
                else
                {
                    error.Action("Such manager is already registered", Enums.ErrorTypeEnum.error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                return false;
            }
        }
        #endregion

        #region SignIn
        public bool SignIn(GetManagerAuthent manAuth)
        {
            try
            {
               
                var auth=dbraisa.managerAuthentification.Where(a=>a.UserName== manAuth.UserName).FirstOrDefault();
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(manAuth.Password, auth.Password);
                if (auth == null || isPasswordValid==false)
                {
                    Console.WriteLine("aseti manageri ver moidzebna");
                    return false;
                }
                else
                {
                    cookiesForManager.ManageCookieforManager(auth.ManagerID);
                    Console.WriteLine("tqven warmatebit shexvedit sistemashi");
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region SignOut
        public bool SignOut()
        {
            if (httpContextAccessor != null)
            {
                var request = httpContextAccessor.HttpContext.Request;
                var response = httpContextAccessor.HttpContext.Response;
                if (request != null && response != null)
                {


                }

            }
            return true;
        }
        #endregion


    }
}
