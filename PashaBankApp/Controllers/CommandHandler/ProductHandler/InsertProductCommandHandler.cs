using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ProductHandler
{
    public class InsertProductCommandHandler:ICommandHandler<InsertProducts>
    {
        private readonly IProduct product;
        public InsertProductCommandHandler(IProduct product)
        {
            this.product = product;
        }
        public bool Handle(InsertProducts command)
        {
            var res=product.InsertProduct(command);
            if (res == true)
                return true;
            return false;
        }
    }
}
