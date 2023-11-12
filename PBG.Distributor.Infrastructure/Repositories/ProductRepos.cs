using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services;
using PashaBankApp.Services.Interface;
using PashaBankApp.Models;
using PashaBankApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBG.Distributor.Core.Interface;

namespace PBG.Distributor.Infrastructure.Repositories
{
    public class ProductRepos: IProductRepos
    {
        public readonly DbRaisa dbRaisa;
        private readonly IErrorRepos error;
        private readonly ILogRepos log;
        public ProductRepos(DbRaisa dbRaisa, IErrorRepos error,ILogRepos log)
        {
             this.dbRaisa = dbRaisa;
             this.error = error;
             this.log = log;
        }
        #region InsertProduct
        public bool InsertProduct(InsertProducts InProd)
        {
            //produqtebis cxrilis shevseba
            using (var transact = dbRaisa.Database.BeginTransaction())
            {
                try
                {
                    var product = new Product
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
                        error.Action("Such a product already exists", ErrorTypeEnum.Info);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("Try again :)");
                    error.Action(ex.Message + ex.StackTrace, ErrorTypeEnum.Fatal);
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
                var product = dbRaisa.products.Where(a => a.ProductID == UpProd.ProductID).FirstOrDefault();
                if (product == null || product.ExpireOn != null)
                {
                    Console.WriteLine("No such product exists, try adding :)");
                    error.Action($"No such product exists, or it is deleted, try adding, ID {UpProd.ProductID}", ErrorTypeEnum.Info);
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
                error.Action(ex.Message + " " + ex.StackTrace, ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region DeleteProduct

        public bool DeleteProduct(DeleteProducts deleteProd)
        {
            //chanaweris washla mtlianad bazidan
            try
            {
                var prod = dbRaisa.products.Where(a => a.ProductID == deleteProd.ProductID).FirstOrDefault();
                if (prod == null)
                {
                    Console.WriteLine("There is no such product, so we cannot delete it :)");
                    error.Action($"There is no such product, so we cannot delete it, ID {deleteProd.ProductID}", ErrorTypeEnum.Info);
                    return false;
                }
                else
                {
                    dbRaisa.products.Remove(prod);
                    dbRaisa.SaveChanges();
                    log.ActionLog($"Product deleted successfully, ID: {deleteProd.ProductID}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region SoftDeletedProduct
        public bool SoftDeletedProduct(SoftDeleteProductRequest SoftDeletedProd)
        {
            //soft delete funqcia produqtebze
            try
            {
                var prod = dbRaisa.products.Where(a => a.ProductID == SoftDeletedProd.ProductID).FirstOrDefault();
                if (prod == null)
                {
                    Console.WriteLine($"No such record was found, ID {SoftDeletedProd.ProductID}");
                    error.Action($"No such record was found, ID {SoftDeletedProd.ProductID}", ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    prod.ExpireOn = "Expired";
                    prod.ExpireDate = DateTime.Now;
                    dbRaisa.SaveChanges();
                    log.ActionLog($"Product: {SoftDeletedProd.ProductID}  is soft deleted");
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, ErrorTypeEnum.Fatal);
                return false;
            }
        }
        #endregion

        #region GetallProduct
        public List<GetProductResponse> getProductResponses()
        {
            //yvela chanaweris wamogheba product cxrilidan
            var prod = dbRaisa.products.Select(a => new GetProductResponse { ExpireDate = a.ExpireDate, ExpireOn = a.ExpireOn, ProductName = a.ProductName, ProductPrice = a.ProductPrice, ProductID = a.ProductID }).ToList();
            return prod;
        }
        #endregion

    }
}
