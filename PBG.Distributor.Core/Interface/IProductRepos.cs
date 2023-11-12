using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IProductRepos
    {
        bool InsertProduct(InsertProducts InProd);
        bool UpdateProduct(UpdateProduct UpProd);
        bool DeleteProduct(DeleteProducts DeleteProd);
        bool SoftDeletedProduct(SoftDeleteProductRequest SoftDeleteProd);
        List<GetProductResponse> getProductResponses();
    }
}
