using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IProduct
    {
        bool InsertProduct(InsertProducts InProd);
        bool UpdateProduct(UpdateProduct UpProd);
        bool DeleteProduct(DeleteProducts DeleteProd);
        bool SoftDeletedProduct(SoftDeleteProductRequest SoftDeleteProd);
        List<GetProductResponse> getProductResponses();

    }
}
