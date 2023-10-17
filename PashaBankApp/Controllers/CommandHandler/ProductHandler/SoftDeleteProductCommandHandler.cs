using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ProductHandler
{
    public class SoftDeleteProductCommandHandler:ICommandHandler<SoftDeleteProductRequest>
    {
        private readonly IProduct product;
        public SoftDeleteProductCommandHandler(IProduct product)
        {
            this.product = product;
        }

        public bool Handle(SoftDeleteProductRequest deleteProd)
        {
            return product.SoftDeletedProduct(deleteProd);
        }
    }
}
