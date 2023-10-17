using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class DeleteDistributorSaleCommandHandler:ICommandHandler<DeleteDistributorSale>
    {
        private readonly IDistributorSale distributorSale;
        public DeleteDistributorSaleCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public bool Handle(DeleteDistributorSale command)
        {
           return distributorSale.DeleteDistributorSale(command);
        }
    }
}
