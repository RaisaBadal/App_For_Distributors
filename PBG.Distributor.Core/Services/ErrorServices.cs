using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PBG.Distributor.Core.Interface;

namespace PashaBankApp.Services
{
    public class ErrorServices:IError
    {
        private readonly IErrorRepos ierrorrepos;
        public ErrorServices(IErrorRepos ierrorrepos)
        {
            this.ierrorrepos = ierrorrepos;
        }


        #region GetAllErrors
        public List<Error> GetAllErrors()
        {
            //error cxrilshi chawerili yvela chanaweris dabruneba
            return ierrorrepos.GetAllErrors();
        }
        #endregion

        #region GetAllErrorsBetweenDate
        public List<Error> GetAllErrorsBetweenDate(ErrorBetweenDateRequest dateresp)
        {
            return ierrorrepos.GetAllErrorsBetweenDate(dateresp);
        }

        #endregion



    }
}
