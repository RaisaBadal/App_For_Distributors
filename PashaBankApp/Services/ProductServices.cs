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
        public bool InsertProduct(string productName, decimal price)
        {
            using (var transact=dbRaisa.Database.BeginTransaction())
            {
                try
                {
                    var product = new Models.Product
                    {
                        ProductName = productName,
                        ProductPrice = price
                    };
                    var prod = dbRaisa.products.Where(a => a.ProductName == productName).FirstOrDefault();
                    if (prod == null)
                    {
                        dbRaisa.products.Add(product);
                        dbRaisa.SaveChanges();
                        log.ActionLog($"The product has been successfully added to the Product table : {productName}");
                        transact.Commit();
                        return true;
                    }
                    else
                    {
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
       public bool UpdateProduct(int productID, string productName, decimal price)
        {
            try
            {
                var product=dbRaisa.products.Where(a=>a.ProductID==productID).FirstOrDefault();
                if (product == null || product.ExpireOn!=null)
                {
                    Console.WriteLine("No such product exists, try adding :)");
                    error.Action($"No such product exists, or it is deleted, try adding, ID {productID}", Enums.ErrorTypeEnum.Info);
                    return false;
                }
                else
                {
                   product.ProductName = productName;
                   product.ProductPrice = price;
                   dbRaisa.SaveChanges();
                    log.ActionLog($"The product has been updated successfully, id: {productID}");
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
            var prod = dbRaisa.products.Select(a => new GetProductResponse { ExpireDate = a.ExpireDate, ExpireOn=a.ExpireOn, ProductName=a.ProductName,ProductPrice=a.ProductPrice,ProductID=a.ProductID}).ToList();
            return prod;
        }
        #endregion

    }
}
