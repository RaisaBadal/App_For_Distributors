using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class DistributorSaleGetProductCommandHandler:IcommandHandlerListAndResponse<DistributorSaleGetProductRequest,DistributorSale>
    {
        private readonly IDistributorSale distributorSale;
        public DistributorSaleGetProductCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public List<DistributorSale> Handle(DistributorSaleGetProductRequest command)
        {
            return distributorSale.DistributorSaleGetProduct(command);
        }
    }
}
