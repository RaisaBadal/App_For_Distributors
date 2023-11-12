using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PashaBankApp.Models;
using System.Text.RegularExpressions;
using PashaBankApp.Validation.Regexi;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace PashaBankApp.Services
{
    public class ManagerServices : IManager
    {

        private readonly IManagerRepos imanagerrepos;
        public ManagerServices(IManagerRepos imanagerrepos)
        {
            this.imanagerrepos = imanagerrepos;
        }
        #region RegistrationManager
        public async Task<bool> RegistrationManager(InsertManager signUp)
        {
            return await imanagerrepos.RegistrationManager(signUp);
        }
        #endregion

        #region SignIn
        public async Task<string> SignIn(GetManagerAuthent manAuth)
        {
            return await imanagerrepos.SignIn(manAuth);
        }
        #endregion

    }
}