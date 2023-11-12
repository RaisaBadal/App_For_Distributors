using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class BonusServices:Ibonus
    {
        
        public readonly IBonusRepos ibonusrepos;
        public BonusServices(IBonusRepos ibonusrepos)
        {   
            this.ibonusrepos = ibonusrepos;
        }
        #region InsertBonus(CalcBonus)
        
        public bool CalcBonus(InsertBonus InBon)
        {
           return ibonusrepos.CalcBonus(InBon);

        }
     
        #endregion

        #region GetBonusByNameSurname
        public List<SortBonus> GetBonusByNameSurname(GetBonus getBon)
        {
            return ibonusrepos.GetBonusByNameSurname(getBon);
        }
        #endregion

        #region SortByBonusDesc
        public List<SortBonus> SortByBonusDesc()
        {
           return ibonusrepos.SortByBonusDesc();
        }
        #endregion

        #region SortByBonusAsc
        public List<SortBonusAsc> SortByBonusAsc()
        {
            //bonus cxrilshi chawerili monacemebis dasortva zrdadobit saerto bonusebis tanxit da dabruneba chanawerebis
            return ibonusrepos.SortByBonusAsc();
        }
        #endregion
    }
}
