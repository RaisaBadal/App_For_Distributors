using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface Ibonus
    {
        bool CalcBonus(InsertBonus InBon);
        List<SortBonus> GetBonusByNameSurname(GetBonus getBon);
        List<SortBonus> SortByBonusDesc();
        List<SortBonus> SortByBonusAsc();

    }
}
