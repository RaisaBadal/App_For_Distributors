using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IProduct
    {
        bool InsertProduct(InsertProduct InProd);
        bool UpdateProduct(UpdateProduct UpProd);
        bool DeleteProduct(int productID);
        bool SoftDeletedProduct(int productID);
        List<GetProductResponse> getProductResponses();

    }
}
