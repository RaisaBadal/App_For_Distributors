using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorHandler
{
    public class UpdateDistributorCommandHandler:ICommandHandler<UpdateDistributorRequest>
    {
        private readonly IDistributor distributor;
        public UpdateDistributorCommandHandler(IDistributor distributor)
        {
            this.distributor = distributor;
        }

        public bool Handle(UpdateDistributorRequest command)
        {
            return distributor.UpdateDistributor(command);
        }
    }
}
