using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.BonusHandler
{
    public class InsertBonusCommandHandler:ICommandHandler<InsertBonus>
    {
        private readonly Ibonus bonus;
        public InsertBonusCommandHandler(Ibonus bonus)
        {
            this.bonus = bonus;
        }

        public bool Handle(InsertBonus insertBonus)
        {
     
            return bonus.CalcBonus(insertBonus);
        }
    }
}
