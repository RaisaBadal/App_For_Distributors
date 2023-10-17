using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.BonusHandler
{
    public class GetBonusBySurnameCommandHandler:IcommandHandlerListAndResponse<GetBonus,SortBonus>
    {
        private readonly Ibonus bonus;
        public GetBonusBySurnameCommandHandler(Ibonus bonus)
        {
            this.bonus = bonus;
        }

        public List<SortBonus> Handle(GetBonus command)
        {
            return bonus.GetBonusByNameSurname(command);
        }
    }
}
