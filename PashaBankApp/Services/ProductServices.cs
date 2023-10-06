using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class ProductServices:IProduct
    {
        public readonly DbRaisa dbRaisa;
        private readonly ErrorServices error;
        private readonly LogServices log;
        public ProductServices(DbRaisa dbRaisa)
        {
            this.dbRaisa = dbRaisa;
            error=new ErrorServices(dbRaisa);
            log= new LogServices(dbRaisa);
        }
        #region InsertProduct
        public bool InsertProduct(InsertProduct InProd)
        {
            //produqtebis cxrilis shevseba
            using (var transact=dbRaisa.Database.BeginTransaction())
            {
                try
                {
                    var product = new Models.Product
                    {
                        ProductName = InProd.ProductName,
                        ProductPrice = InProd.ProductPrice
                    };
                    var prod = dbRaisa.products.Where(a => a.ProductName == InProd.ProductName).FirstOrDefault();
                    if (prod == null)
                    {
                        dbRaisa.products.Add(product);
                        dbRaisa.SaveChanges();
                        log.ActionLog($"The product has been successfully added to the Product table : {InProd.ProductName}");
                        transact.Commit();
                        return true;
                    }
                    else
                    {
                        //tu arsebosb produqti mashin ar davamatebt
                        Console.WriteLine("Such a product already exists");
                        transact.Rollback();
                        error.Action("Such a product already exists", Enums.ErrorTypeEnum.Info);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("Try again :)");
                    error.Action(ex.Message + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                    return false;
                }
            }
        }
        #endregion

        #region UpdateProduct
       public bool UpdateProduct(UpdateProduct UpProd)
        {
            //produqtis ganaxleba
            try
            {
                //vamowmebt tu arsebobs aseti produqti da tu aris igi aqtiuri anu washliliar unda iyos da expireON ar unda iyos shevsebuli
                var product=dbRaisa.products.Where(a=>a.ProductID==UpProd.ProductID).FirstOrDefault();
                if (product == null || product.ExpireOn!=null)
                {
                    Console.WriteLine("No such product exists, try adding :)");
                    error.Action($"No such product exists, or it is deleted, try adding, ID {UpProd.ProductID}", Enums.ErrorTypeEnum.Info);
                    return false;
                }
                else
                {
                   product.ProductName = UpProd.ProductName;
                   product.ProductPrice = UpProd.ProductPrice;
                   dbRaisa.SaveChanges();
                    log.ActionLog($"The product has been updated successfully, id: {UpProd.ProductID}");
                   return true;
               }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message+" "+ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region DeleteProduct

        public bool DeleteProduct(int productID) 
        {
            //chanaweris washla mtlianad bazidan
            try
            {
            var prod = dbRaisa.products.Where(a => a.ProductID==productID).FirstOrDefault();
                if (prod == null)
                {
                    Console.WriteLine("There is no such product, so we cannot delete it :)");
                    error.Action($"There is no such product, so we cannot delete it, ID {productID}", Enums.ErrorTypeEnum.Info);
                    return false;
                }
                else
                {
                    dbRaisa.products.Remove(prod);
                    dbRaisa.SaveChanges();
                    log.ActionLog($"Product deleted successfully, ID: {productID}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message+" "+ex.StackTrace,Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region SoftDeletedProduct
       public bool SoftDeletedProduct(int productID)
        {
            //soft delete funqcia produqtebze
            try
            {
                var prod = dbRaisa.products.Where(a => a.ProductID == productID).FirstOrDefault();
                if(prod == null)
                {
                    Console.WriteLine($"No such record was found, ID {productID}");
                    error.Action($"No such record was found, ID {productID}", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    prod.ExpireOn = "Expired";
                    prod.ExpireDate= DateTime.Now;
                    dbRaisa.SaveChanges();
                    log.ActionLog($"Product: {productID}  is soft deleted");
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace,Enums.ErrorTypeEnum.Fatal);
                return false;
            }
        }
        #endregion

        #region GetallProduct
        public List<GetProductResponse> getProductResponses()
        {
            //yvela chanaweris wamogheba product cxrilidan
            var prod = dbRaisa.products.Select(a => new GetProductResponse { ExpireDate = a.ExpireDate, ExpireOn=a.ExpireOn, ProductName=a.ProductName,ProductPrice=a.ProductPrice,ProductID=a.ProductID}).ToList();
            return prod;
        }
        #endregion

    }
}
