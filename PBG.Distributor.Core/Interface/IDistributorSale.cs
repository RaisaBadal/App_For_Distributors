using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IDistributorSale
    {
        bool InsertDistributoSaleAsync(InsertDistributorSaleRequest insale);
        bool DeleteDistributorSale(DeleteDistributorSale deleteDistrSale);
        bool SoftDeletedDistributorSale(SoftDeleteDistributorSaleRequest deleteDistrSale);
        List<DistributorSale> DistributorSaleGetDist(GetDistributorSaleRequest distsale);
        List<DistributorSale> DistributorSaleGetDate(DistributorSaleGetDateRequest dist);
        List<DistributorSale> DistributorSaleGetProduct(DistributorSaleGetProductRequest prod);

    }
}
