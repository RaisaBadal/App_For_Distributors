using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler
{
    public class DistributorSaleGetDateCommandHandler:IcommandHandlerListAndResponse<DistributorSaleGetDateRequest,DistributorSale>
    {
        private readonly IDistributorSale distributorSale;
        public DistributorSaleGetDateCommandHandler(IDistributorSale distributorSale)
        {
            this.distributorSale = distributorSale;
        }

        public List<DistributorSale> Handle(DistributorSaleGetDateRequest command)
        {
            return distributorSale.DistributorSaleGetDate(command);
        }
    }
}
