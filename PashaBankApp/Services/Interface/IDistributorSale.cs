using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IDistributorSale
    {
        bool InsertDistributoSale(InsertDistributorSaleRequest insale);
        bool DeleteDistributorSale(int distributorSaleID);
        bool SoftDeletedDistributorSale(int distributorSaleID);
        List<DistributorSale> DistributorSaleGetDist(int distributorID);
        List<DistributorSale> DistributorSaleGetDate(DateTime saleDate);
        List<DistributorSale> DistributorSaleGetProduct(int productID);

    }
}
