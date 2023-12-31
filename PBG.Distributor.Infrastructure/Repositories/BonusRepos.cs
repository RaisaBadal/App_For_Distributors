﻿using PashaBankApp.DbContexti;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services;
using PashaBankApp.Services.Interface;
using PBG.Distributor.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PBG.Distributor.Infrastructure.Repositories
{
    public class BonusRepos : IBonusRepos
    {
       
            public readonly DbRaisa dbraisa;
            public readonly IErrorRepos error;
            public readonly ILogRepos logger;
            public BonusRepos(DbRaisa dbraisa, IErrorRepos error, ILogRepos logger)
            {
                this.dbraisa = dbraisa;
                this.error = error;
                this.logger= logger;
            }
            #region InsertBonus(CalcBonus)
        
            public bool CalcBonus(InsertBonus InBon)
            {
                //bonus cxrilshi bonusebis damateba, bonusebis gamotvla
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
                                var totalForDist = dbraisa.distributorSales.Where(a => a.SaleDate > InBon.StartDate && a.SaleDate < InBon.EndDate && a.distributorID == distID && a.status == null).Sum(a => a.TotalPrice);
                                //distributoris saerto gayidvebis 10%
                                bonus += (decimal)(totalForDist * 10) / (decimal)100;

                                var lsforoff = dbraisa.distributorSales.Where(a => a.distributorID == distID).ToList();

                                foreach (var item in lsforoff)
                                {
                                    //statusis monishvna rogorc used, rata ar moxdes gayidvis meored gamoyeneba bonusebis gamotvlashi
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
                                var totalForInventedDist = dbraisa.distributorSales.Where(a => a.SaleDate > InBon.StartDate && a.SaleDate < InBon.EndDate && a.distributorID == distInv).Sum(a => a.TotalPrice);
                                bonus += (decimal)(totalForInventedDist * 5) / (decimal)100;
                            }

                            var bon = new PashaBankApp.Models.Bonus()
                            {
                                //bonus cxrilis shevseba
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
                        error.Action(ex.Message + ", at line: " + ex.StackTrace, PashaBankApp.Enums.ErrorTypeEnum.Fatal);
                        return false;
                    }
                }


            }
            #endregion

            #region GetBonusByNameSurname
            public List<SortBonus> GetBonusByNameSurname(GetBonus getBon)
            {
                //bonusebis dasortva gadacemuli saxelis da gvaris mixedvit
                var dist = dbraisa.Distributors.Where(a => a.DistributorName == getBon.name && a.DistributorLastName == getBon.surname).Select(a => a.DistributorID).FirstOrDefault();
                var distBonus = dbraisa.bonus.Where(a => a.DistributorID == dist).Select(a => new SortBonus { BonusID = a.BonusID, BonusAmount = a.BonusAmount, DateOfBonus = a.DateOfBonus, DistributorID = a.DistributorID }).ToList();
                return distBonus;
            }
            #endregion

            #region SortByBonusDesc
            public List<SortBonus> SortByBonusDesc()
            {
                //bonus cxrilis dasortva klebadobit saerto bonustanxit da yvela chanaweris dabruneba
                var distBon = dbraisa.bonus.OrderByDescending(a => a.BonusAmount).Select(a => new SortBonus { BonusID = a.BonusID, BonusAmount = a.BonusAmount, DateOfBonus = a.DateOfBonus, DistributorID = a.DistributorID }).ToList();
                return distBon;
            }
            #endregion

            #region SortByBonusAsc
            public List<SortBonusAsc> SortByBonusAsc()
            {
                //bonus cxrilshi chawerili monacemebis dasortva zrdadobit saerto bonusebis tanxit da dabruneba chanawerebis
                var distBon = dbraisa.bonus.OrderBy(a => a.BonusAmount).Select(a => new SortBonusAsc { BonusID = a.BonusID, BonusAmount = a.BonusAmount, DateOfBonus = a.DateOfBonus, DistributorID = a.DistributorID }).ToList();
                return distBon;
            }
            #endregion
        }
    }

