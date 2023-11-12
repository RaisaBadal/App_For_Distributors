using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IBonusRepos
    {
        bool CalcBonus(InsertBonus InBon);
        List<SortBonus> GetBonusByNameSurname(GetBonus getBon);
        List<SortBonus> SortByBonusDesc();
        List<SortBonusAsc> SortByBonusAsc();
    }
}
