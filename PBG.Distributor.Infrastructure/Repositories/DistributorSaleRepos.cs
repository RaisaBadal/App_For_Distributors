using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services;
using PashaBankApp.Enums;
using PashaBankApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBG.Distributor.Core.Interface;

namespace PBG.Distributor.Infrastructure.Repositories
{
    public class DistributorSaleRepos: IDistributorSaleRepos
    {
        public readonly DbRaisa dbRaisa;
        private readonly IErrorRepos error;
        private readonly ILogRepos log;
        public DistributorSaleRepos(DbRaisa dbRaisa, IErrorRepos error, ILogRepos log)
        {
            this.dbRaisa = dbRaisa;
           this.error = error;
            this.log = log;
        }

        #region InsertDistributoSale
        public bool InsertDistributoSaleAsync(InsertDistributorSaleRequest insale)
        {
            //distributorsale cxrilis shevseba
            using (var tra = dbRaisa.Database.BeginTransaction())
            {
                try
                {
                    //vamowmebt tu arsebobs distributori da produqti gadacemuli ID-is mixedvit
                    var dist = dbRaisa.Distributors.Where(a => a.DistributorID == insale.DistributorID).FirstOrDefault();
                    var price = dbRaisa.products.Where(a => a.ProductID == insale.ProductID).FirstOrDefault().ProductPrice;
                    if (dist == null || price <= 0)
                    {
                        Console.WriteLine("The product or distributor could not be found in the system :))");
                        tra.Rollback();
                        error.Action("The product or distributor could not be found in the system",ErrorTypeEnum.Info);
                        return false;
                    }
                    else
                    {
                        //tu arsebobs mashin vavsebt cxrils 
                        var dissale = new DistributorSale
                        {
                            SaleDate = DateTime.Now,
                            ProductQuantity = insale.ProductQuantity,
                            //saerto jami=raodenoba *fiqsirebul tanxaze
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
                    error.Action(ex.Message + " " + ex.StackTrace, PashaBankApp.Enums.ErrorTypeEnum.Fatal);
                    tra.Rollback();
                    throw;

                }
            }
        }
        #endregion

        #region DeleteDistributorSale
        public bool DeleteDistributorSale(DeleteDistributorSale deleteDistrSale)
        {
            try
            {
                //distributorsale cxrilidan chanaweris washla
                //vamowmebt tu arsebobs aseti distributorsale ID
                var distributorSale = dbRaisa.distributorSales.Where(a => a.DistributorSaleID == deleteDistrSale.DistributorID).FirstOrDefault();
                if (distributorSale == null)
                {
                    error.Action("Such a record was not found in the database, so we cannot delete it", ErrorTypeEnum.Info);
                    throw new Exception("Such a record was not found in the database, so we cannot delete it :)");
                }
                else
                {
                    //tu moidzebna mashin bazidan wavshlit
                    dbRaisa.distributorSales.Remove(distributorSale);
                    //log cxrilshi chavwert meramdene ID waishala
                    log.ActionLog($"Deleted successfully, distributor ID: {deleteDistrSale.DistributorID}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, PashaBankApp.Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region DistributorSaleGetDist

        public List<DistributorSale> DistributorSaleGetDist(GetDistributorSaleRequest distributorsale)
        {
            //gadacemuli ID-s mixedvit distributoris gayidvebis dabruneba
            try
            {
                var getDist = dbRaisa.distributorSales.Where(a => a.distributorID == distributorsale.DistributorID && a.ExpireOn == null).ToList();
                if (getDist != null && getDist.Count > 0)
                {

                    return getDist;

                }
                else
                {
                    error.Action("No information found for this distributor.", ErrorTypeEnum.error);
                    throw new Exception("No information found for this distributor.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, PashaBankApp.Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }

        #endregion

        #region DistributorSaleGetDate
        public List<DistributorSale> DistributorSaleGetDate(DistributorSaleGetDateRequest distsale)
        {
            try
            {
                //tu arsebobs gadacemul tarighit shesrulebuli gayidva daabrunebs aset chanawerebs
                var getdate = dbRaisa.distributorSales.Where(a => a.SaleDate == distsale.saleDate && a.ExpireOn == null).ToList();
                if (getdate != null)
                {
                    return getdate;
                }
                else
                {
                    error.Action("Nothing was found for this date", PashaBankApp.Enums.ErrorTypeEnum.error);
                    throw new Exception("Nothing was found for this date :)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, PashaBankApp.Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region DistributorSaleGetProduct
        public List<DistributorSale> DistributorSaleGetProduct(DistributorSaleGetProductRequest distprodSale)
        {
            try
            {
                //daabrunebs gadacemuli product ID-is mixedvit chanawerebs
                var distprod = dbRaisa.distributorSales.Where(a => a.ProductID == distprodSale.productID && a.ExpireOn == null).ToList();
                if (distprod != null)
                {
                    return distprod;
                }
                else
                {
                    error.Action("No such product was found", PashaBankApp.Enums.ErrorTypeEnum.error);
                    throw new Exception("No such product was found :(");
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

        #region SoftDeletedDistributorSale
        public bool SoftDeletedDistributorSale(SoftDeleteDistributorSaleRequest deleteDistrSale)
        {
            try
            {
                //bazidan ar wavshlit, tumca aris egretwodebuli softDelete, gvaqvs ori veli
                //expireOn da expireDate, romelic tavdapirvelad nulls udris
                //roca shevavsebt mat shesabamisad washlil chanawerebad vigulisxmebt
                //tumca bazashi gamogvichndeba es chanawerebi mainc, shevsebuli eqnebat zemot xsenebuli ori veli
                //shemdgom roca garkveul servisebshi ar mogvindeba mati gamoyeneba shegvidzlia martivad shevamowmot
                //mag:expireOn!=null -> washlili chanaweria
                var distSale = dbRaisa.distributorSales.Where(a => a.DistributorSaleID == deleteDistrSale.DistributorSaleID).FirstOrDefault();
                if (distSale == null)
                {
                    Console.WriteLine("No such record was found");
                    error.Action("No such record was found", ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    distSale.ExpireOn = "Expired";
                    distSale.ExpireDate = DateTime.Now;
                    log.ActionLog($"Distributor Sale: {deleteDistrSale.DistributorSaleID} is softed deleted");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                error.Action(ex.Message + ", " + ex.StackTrace, ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion


    }
}
