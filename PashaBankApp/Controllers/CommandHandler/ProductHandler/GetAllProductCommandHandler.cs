using Microsoft.EntityFrameworkCore.Query.Internal;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ProductHandler
{
    public class GetAllProductCommandHandler:IcommandHandlerList<GetProductResponse>
    {
        private readonly IProduct product;

        public GetAllProductCommandHandler(IProduct product)
        {
            this.product = product;
        }

        public List<GetProductResponse> Handle()
        {
          return  product.getProductResponses();
        }
    }
}
