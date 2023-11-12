using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class DistributorSaleServices :IDistributorSale
    {
        private readonly IDistributorSaleRepos idistributorsalerepos;
        public DistributorSaleServices(IDistributorSaleRepos idistributorsalerepos)
        {
            this.idistributorsalerepos = idistributorsalerepos;
        }

        #region InsertDistributoSale
        public bool InsertDistributoSaleAsync(InsertDistributorSaleRequest insale)
        {
            return idistributorsalerepos.InsertDistributoSaleAsync(insale);
        }
        #endregion

        #region DeleteDistributorSale
        public bool DeleteDistributorSale(DeleteDistributorSale deleteDistrSale)
        {
            return idistributorsalerepos.DeleteDistributorSale(deleteDistrSale);
        }
        #endregion

        #region DistributorSaleGetDist

        public List<DistributorSale> DistributorSaleGetDist(GetDistributorSaleRequest distributorsale)
        {
            return idistributorsalerepos.DistributorSaleGetDist(distributorsale);
        }

        #endregion

        #region DistributorSaleGetDate
        public List<DistributorSale> DistributorSaleGetDate(DistributorSaleGetDateRequest distsale)
        {
            return idistributorsalerepos.DistributorSaleGetDate(distsale);
        }
        #endregion

        #region DistributorSaleGetProduct
       public List<DistributorSale> DistributorSaleGetProduct(DistributorSaleGetProductRequest distprodSale)
        {
            return idistributorsalerepos.DistributorSaleGetProduct(distprodSale);
        }
        #endregion

        #region SoftDeletedDistributorSale
        public bool SoftDeletedDistributorSale(SoftDeleteDistributorSaleRequest deleteDistrSale)
        {
            return idistributorsalerepos.SoftDeletedDistributorSale(deleteDistrSale);
        }
        #endregion


    }
}