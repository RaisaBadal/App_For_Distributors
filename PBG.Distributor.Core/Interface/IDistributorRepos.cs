using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IDistributorRepos
    {
        bool InsertDistributor(InsertDistributorRequest req);
        bool UpdateDistributor(UpdateDistributorRequest updis);
        bool DeleteDistributor(DeleteDistributor deleteDistr);
        bool SoftDistributorDelete(SoftDeleteDistributor deleteDistr);
        List<GetDistributor> GetDistributor();
    }
}
