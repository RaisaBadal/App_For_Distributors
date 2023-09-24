using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class DistributorSaleServices : IDistributorSale
    {
        public readonly DbRaisa dbRaisa;
        private readonly ErrorServices error;
        private readonly LogServices log;
        public DistributorSaleServices(DbRaisa dbRaisa)
        {
            this.dbRaisa = dbRaisa;
            error=new ErrorServices(dbRaisa);
            log=new LogServices(dbRaisa);
        }

        #region InsertDistributoSale
        public bool InsertDistributoSale(InsertDistributorSaleRequest insale)
        {
            using (var tra = dbRaisa.Database.BeginTransaction())
            {
                try
                {
                    var dist = dbRaisa.Distributors.Where(a => a.DistributorID == insale.DistributorID).FirstOrDefault();
                    var price = dbRaisa.products.Where(a => a.ProductID == insale.ProductID).FirstOrDefault().ProductPrice;
                    if (dist == null || price <= 0)
                    {
                        Console.WriteLine("The product or distributor could not be found in the system :))");
                        tra.Rollback();
                        error.Action("The product or distributor could not be found in the system", Enums.ErrorTypeEnum.Info);
                        return false;
                    }
                    else
                    {
                        var dissale = new Models.DistributorSale
                        {
                            SaleDate = DateTime.Now,
                            ProductQuantity = insale.ProductQuantity,
                            TotalPrice = insale.ProductQuantity * price,
                            distributorID = insale.DistributorID,
                            ProductID = insale.ProductID
                        };
                        log.ActionLog("Inserted successfully");
                        dbRaisa.distributorSales.Add(dissale);
                        dbRaisa.SaveChanges();
                        tra.Commit();
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                    tra.Rollback();
                    throw;

                }
            }
        }
        #endregion

        #region DeleteDistributorSale
        public bool DeleteDistributorSale(int distributorSaleID)
        {
            try
            {
                var distributorSale=dbRaisa.distributorSales.Where(a=>a.DistributorSaleID== distributorSaleID).FirstOrDefault();
                if(distributorSale == null)
                {
                    error.Action("Such a record was not found in the database, so we cannot delete it", Enums.ErrorTypeEnum.Info);
                    throw new Exception("Such a record was not found in the database, so we cannot delete it :)");
                }
                else
                {
                    dbRaisa.distributorSales.Remove(distributorSale);
                    log.ActionLog($"Deleted successfully, distributor ID: {distributorSaleID}");
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

        #region DistributorSaleGetDist

        public List<DistributorSale> DistributorSaleGetDist(int distributorID)
        {
            try
            {
                var getDist=dbRaisa.distributorSales.Where(a=>a.distributorID== distributorID&& a.ExpireOn==null).ToList();
                if (getDist!=null  && getDist.Count > 0)
                {

                    return getDist;
                
                }
                else
                {
                    error.Action("No information found for this distributor.", Enums.ErrorTypeEnum.error);
                    throw new Exception("No information found for this distributor.");
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

        #region DistributorSaleGetDate
        public List<DistributorSale> DistributorSaleGetDate(DateTime saleDate)
        {
            try
            {
                var getdate=dbRaisa.distributorSales.Where(a=>a.SaleDate==saleDate&&a.ExpireOn==null).ToList();
                if(getdate!=null)
                {
                    return getdate;
                }
                else
                {
                    error.Action("Nothing was found for this date", Enums.ErrorTypeEnum.error);
                    throw new Exception("Nothing was found for this date :)");
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

        #region DistributorSaleGetProduct
       public List<DistributorSale> DistributorSaleGetProduct(int productID)
        {
            try
            {
                var distprod=dbRaisa.distributorSales.Where(a=>a.ProductID==productID&&a.ExpireOn==null).ToList();
                if(distprod!=null)
                {
                    return distprod;
                }
                else
                {
                    error.Action("No such product was found", Enums.ErrorTypeEnum.error);
                    throw new Exception("No such product was found :(");
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

        #region SoftDeletedDistributorSale
        public bool SoftDeletedDistributorSale(int distributorSaleID)
        {
            try
            {
                var distSale = dbRaisa.distributorSales.Where(a => a.DistributorSaleID == distributorSaleID).FirstOrDefault();
                if(distSale==null)
                {
                    Console.WriteLine("No such record was found");
                    error.Action("No such record was found", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    distSale.ExpireOn = "Expired";
                    distSale.ExpireDate= DateTime.Now;
                    log.ActionLog($"Distributor Sale: {distributorSaleID} is softed deleted");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                error.Action(ex.Message+", "+ex.StackTrace,Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion


    }
}