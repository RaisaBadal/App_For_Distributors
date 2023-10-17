using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorHandler
{
    public class InsertDistributorCommandHandler : ICommandHandler<InsertDistributorRequest>
    {
        private readonly IDistributor distributor;
        public InsertDistributorCommandHandler(IDistributor distributor)
        {
            this.distributor = distributor;
        }

        public bool Handle(InsertDistributorRequest InsertDistributor)
        {
            return distributor.InsertDistributor(InsertDistributor);
        }
    }
    
}
