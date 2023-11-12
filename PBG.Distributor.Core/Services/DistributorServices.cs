using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PashaBankApp.Validation.Regexi;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
namespace PashaBankApp.Services
{
    public class DistributorServices:IDistributor
    {
        public readonly IDistributorRepos distributorReposdistr;

        public DistributorServices(IDistributorRepos distributorReposdistr)
        {
            this.distributorReposdistr= distributorReposdistr;
        }
   

        #region InsertDistributor
        public bool InsertDistributor(InsertDistributorRequest req)
        {
           return distributorReposdistr.InsertDistributor(req);
        }
        #endregion

        #region UpdateDistributor
        public bool UpdateDistributor(UpdateDistributorRequest updis)
        {
            return distributorReposdistr.UpdateDistributor(updis);
        }
        #endregion

        #region DeleteDistributor
        public bool DeleteDistributor(DeleteDistributor deleteDistr)
        {
            return distributorReposdistr.DeleteDistributor(deleteDistr);
        }
        #endregion

        #region SoftDistributorDelete
        public bool SoftDistributorDelete(SoftDeleteDistributor deleteDistr)
        {
            return distributorReposdistr.SoftDistributorDelete(deleteDistr);
        }

        #endregion

        #region GetDistributor

        public List<GetDistributor> GetDistributor()
        {
           return distributorReposdistr.GetDistributor();
        }
        #endregion

    }
}
