using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ProductHandler
{
    public class UpdateProductCommandHandler:ICommandHandler<UpdateProduct>
    {
        private readonly IProduct product;
        public UpdateProductCommandHandler(IProduct product)
        {
            this.product = product;
        }

        public bool Handle(UpdateProduct command)
        {
            var res=product.UpdateProduct(command);
            return res;
        }
    }
}
