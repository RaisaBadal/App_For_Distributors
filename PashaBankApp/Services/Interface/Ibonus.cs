using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface Ibonus
    {
        bool CalcBonus(DateTime startDate, DateTime endDate);
        List<SortBonus> GetBonusByNameSurname(string name, string surname);
        List<SortBonus> SortByBonusDesc();
        List<SortBonus> SortByBonusAsc();

    }
}
