using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorHandler
{
    public class DeleteDistributorCommandHandler:ICommandHandler<DeleteDistributor>
    {
        private readonly IDistributor distributor;
        public DeleteDistributorCommandHandler(IDistributor distributor)
        {
            this.distributor = distributor;
        }
        public bool Handle(DeleteDistributor deleteDistributor)
        {
            return distributor.DeleteDistributor(deleteDistributor);
        }
    }
}
