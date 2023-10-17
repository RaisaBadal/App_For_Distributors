using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorHandler
{
    public class SoftDeleteDistributorCommandHandler:ICommandHandler<SoftDeleteDistributor>
    {
        private readonly IDistributor distributor;
        public SoftDeleteDistributorCommandHandler(IDistributor distributor)
        {
            this.distributor = distributor;
        }

        public bool Handle(SoftDeleteDistributor command)
        {
            return distributor.SoftDistributorDelete(command);
        }
    }
}
