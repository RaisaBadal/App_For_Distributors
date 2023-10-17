using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class InsertDistributorSaleCommandHandler:ICommandHandler<InsertDistributorSaleRequest>
    {
        private readonly IDistributorSale distributorSale;
        public InsertDistributorSaleCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public bool Handle(InsertDistributorSaleRequest command)
        {
            return distributorSale.InsertDistributoSaleAsync(command);
        }
    }
}
