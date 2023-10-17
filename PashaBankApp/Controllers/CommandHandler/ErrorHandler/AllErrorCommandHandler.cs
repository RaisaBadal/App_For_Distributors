using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ErrorHandler
{
    public class AllErrorCommandHandler:IcommandHandlerList<Error>
    {
        private readonly IError error;
        public AllErrorCommandHandler(IError error)
        {
            this.error = error;
        }

        public List<Error> Handle()
        {
           return error.GetAllErrors();
        }
    }
}
