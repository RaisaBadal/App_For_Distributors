using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.BonusHandler
{
    public class SortBonusAscCommandHandler:IcommandHandlerList<SortBonusAsc>
    {
        private readonly Ibonus bonus;
        public SortBonusAscCommandHandler(Ibonus bonus)
        {
            this.bonus = bonus;
        }

        public List<SortBonusAsc> Handle()
        {
            return bonus.SortByBonusAsc();
        }
    }
}
