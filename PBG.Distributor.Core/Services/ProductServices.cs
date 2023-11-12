using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class ProductServices:IProduct
    {
        private readonly IProductRepos iproductrepos;
        
        public ProductServices(IProductRepos iproductrepos)
        { 
            this.iproductrepos=iproductrepos;
        }
        #region InsertProduct
        public bool InsertProduct(InsertProducts InProd)
        {
           return iproductrepos.InsertProduct(InProd);
        }
        #endregion

        #region UpdateProduct
        public bool UpdateProduct(UpdateProduct UpProd)
        {
            return iproductrepos.UpdateProduct(UpProd);
        }
        #endregion

        #region DeleteProduct

        public bool DeleteProduct(DeleteProducts deleteProd) 
        {
           return iproductrepos.DeleteProduct(deleteProd);
        }
        #endregion

        #region SoftDeletedProduct
        public bool SoftDeletedProduct(SoftDeleteProductRequest SoftDeletedProd)
        {
          return iproductrepos.SoftDeletedProduct(SoftDeletedProd);
        }
        #endregion

        #region GetallProduct
        public List<GetProductResponse> getProductResponses()
        {
          return iproductrepos.getProductResponses();
        }
        #endregion

    }
}
