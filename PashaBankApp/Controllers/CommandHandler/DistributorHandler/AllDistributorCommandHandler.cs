using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorHandler
{
    public class AllDistributorCommandHandler:IcommandHandlerList<GetDistributor>
    {
        private readonly IDistributor distributor;
        public AllDistributorCommandHandler(IDistributor distributor)
        {
            this.distributor = distributor;
        }

        public List<GetDistributor> Handle()
        {
            return distributor.GetDistributor();
        }
    }
}
