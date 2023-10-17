using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ProductHandler
{
    public class DeleteProductCommandHandler: ICommandHandler<DeleteProducts>
    {
        private readonly IProduct product;
        public DeleteProductCommandHandler(IProduct product)
        {
            this.product = product;
        }
        public bool Handle(DeleteProducts command)
        {
            return product.DeleteProduct(command);
        }
    }
}
