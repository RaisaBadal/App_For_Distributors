using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class BonusServices:Ibonus
    {
        public readonly DbRaisa dbraisa;
        public readonly ErrorServices error;
        public readonly LogServices logger;
        public BonusServices(DbRaisa dbraisa)
        {
            this.dbraisa = dbraisa;
            error=new ErrorServices(dbraisa);
            logger=new LogServices(dbraisa);
        }
        #region InsertBonus(CalcBonus)
        public bool CalcBonus(DateTime startDate, DateTime endDate)
        {
            using (var trans = dbraisa.Database.BeginTransaction())
            {
                try
                {

                    decimal bonus = 0;
                    foreach (var distributor in dbraisa.Distributors.ToList())
                    {
                        var distID = distributor.DistributorID;
                        if (dbraisa.distributorSales.Where(a => a.distributorID == distID && a.TotalPrice != null).Any())
                        {
                            var totalForDist = dbraisa.distributorSales.Where(a => a.SaleDate > startDate && a.SaleDate < endDate && a.distributorID == distID && a.status == null).Sum(a => a.TotalPrice);

                            bonus += (decimal)(totalForDist * 10) / (decimal)100;

                            var lsforoff = dbraisa.distributorSales.Where(a => a.distributorID == distID).ToList();

                            foreach (var item in lsforoff)
                            {
                                item.status = "used";
                            }
                            dbraisa.SaveChanges();
                        }
                        else
                        {
                            bonus += 0;
                        }

                        var listOfInvertedDist = dbraisa.Distributors.Where(a => a.Recomendedby == distID).ToList();
                        foreach (var item in listOfInvertedDist)
                        {
                            var distInv = item.DistributorID;
                            var totalForInventedDist = dbraisa.distributorSales.Where(a => a.SaleDate > startDate && a.SaleDate < endDate && a.distributorID == distInv).Sum(a => a.TotalPrice);
                            bonus += (decimal)(totalForInventedDist * 5) / (decimal)100;
                        }

                        var bon = new Models.Bonus()
                        {
                            DateOfBonus = DateTime.Now,
                            BonusAmount = bonus,
                            DistributorID = distID
                        };
                        dbraisa.Add(bon);
                        dbraisa.SaveChanges();
                        logger.ActionLog($"Insert is success in Bonus Table");
                    }
                    trans.Commit();
                    return true;
                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    trans.Rollback();
                    error.Action(ex.Message + ", at line: " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                    return false;
                }
            }


        }
        #endregion

        #region GetBonusByNameSurname
        public List<SortBonus> GetBonusByNameSurname(string name,string surname)
        {
            var dist = dbraisa.Distributors.Where(a => a.DistributorName == name && a.DistributorLastName == surname).Select(a => a.DistributorID).FirstOrDefault();
            var distBonus=dbraisa.bonus.Where(a=>a.DistributorID==dist).Select(a => new SortBonus { BonusID = a.BonusID, BonusAmount = a.BonusAmount, DateOfBonus = a.DateOfBonus, DistributorID = a.DistributorID }).ToList();
            return distBonus;
        }
        #endregion

        #region SortByBonusDesc
        public List<SortBonus> SortByBonusDesc()
        {
            var distBon=dbraisa.bonus.OrderByDescending(a=>a.BonusAmount).Select(a=>new SortBonus { BonusID=a.BonusID,BonusAmount=a.BonusAmount,DateOfBonus=a.DateOfBonus,DistributorID=a.DistributorID}).ToList();
            return distBon;
        }
        #endregion

        #region SortByBonusAsc
        public List<SortBonus> SortByBonusAsc()
        {
            var distBon = dbraisa.bonus.OrderBy(a => a.BonusAmount).Select(a => new SortBonus { BonusID = a.BonusID, BonusAmount = a.BonusAmount, DateOfBonus = a.DateOfBonus, DistributorID = a.DistributorID }).ToList();
            return distBon;
        }
        #endregion
    }
}
