using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class DistributorSaleGetDistCommandHandler:IcommandHandlerListAndResponse<GetDistributorSaleRequest,DistributorSale>
    {
        private readonly IDistributorSale distributorSale;
        public DistributorSaleGetDistCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public List<DistributorSale> Handle(GetDistributorSaleRequest command)
        {
           return distributorSale.DistributorSaleGetDist(command);
        }
    }
}
