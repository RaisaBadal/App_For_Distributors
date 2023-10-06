using Azure.Core;
using PashaBankApp.Cookies;
using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class ManagerServices:IManager
    {
        public readonly DbRaisa dbraisa;
        private readonly ErrorServices error;
        private readonly LogServices log;
        private readonly CookiesForManager cookiesForManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ManagerServices(DbRaisa dbraisa, IError error, ILog log,IHttpContextAccessor ihhtp)
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
                var sig = new Models.Manager
                {
                    PersonalNumber = signUp.PersonalNumber,
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    PhoneNumber = signUp.PhoneNumber,
                    Mail = signUp.Mail
                };
                var sign = dbraisa.manager.Where(a => a.PersonalNumber == signUp.PersonalNumber).FirstOrDefault();
                if (sig != null)
                {
                    error.Action("Such manager is already registered", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    dbraisa.manager.Add(sig);
                    dbraisa.SaveChanges();
                    managerautID = dbraisa.manager.Max(a => a.ID);
                    log.ActionLog($"User is successfully registered,ID {sig.ID}");
                   // return true;
                }
                var managerauth=dbraisa.managerAuthentification.Where(a=>a.UserName==signUp.UserName).FirstOrDefault();
                if (managerauth != null)
                {
                    error.Action("Such manager is already registered", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    string cryptedpass = BCrypt.Net.BCrypt.HashPassword(signUp.Password);
                    var auth = new Models.ManagerAuthentification
                    {
                        ID = managerautID,
                        UserName= signUp.UserName,
                        Password= cryptedpass
                    };
                    dbraisa.managerAuthentification.Add(auth);
                    dbraisa.SaveChanges();
                    return true;
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



    }
}
