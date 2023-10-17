using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.BonusHandler
{
    public class SortBonusDescCommandHandler:IcommandHandlerList<SortBonus>
    {
        private readonly Ibonus bonus;
        public SortBonusDescCommandHandler(Ibonus bonus)
        {
            this.bonus = bonus;
        }

        public List<SortBonus> Handle()
        {
            return bonus.SortByBonusDesc();
        }
    }
}
