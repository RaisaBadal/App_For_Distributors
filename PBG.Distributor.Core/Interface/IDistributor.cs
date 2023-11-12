using PashaBankApp.Enums;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IDistributor
    {
        bool InsertDistributor(InsertDistributorRequest req);
        bool UpdateDistributor(UpdateDistributorRequest updis);
        bool DeleteDistributor(DeleteDistributor deleteDistr);
        bool SoftDistributorDelete(SoftDeleteDistributor deleteDistr);
        List<GetDistributor> GetDistributor();
    }
}
