using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IProduct
    {
        bool InsertProduct(string productName, decimal price);
        bool UpdateProduct(int productID, string productName, decimal price);
        bool DeleteProduct(int productID);
        bool SoftDeletedProduct(int productID);
       List<GetProductResponse> getProductResponses();

    }
}
