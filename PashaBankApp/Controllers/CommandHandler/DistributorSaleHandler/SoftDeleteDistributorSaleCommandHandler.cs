using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class SoftDeleteDistributorSaleCommandHandler:ICommandHandler<SoftDeleteDistributorSaleRequest>
    {
        private readonly IDistributorSale distributorSale;
        public SoftDeleteDistributorSaleCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public bool Handle(SoftDeleteDistributorSaleRequest command)
        {
            return distributorSale.SoftDeletedDistributorSale(command);
        }
    }
}
