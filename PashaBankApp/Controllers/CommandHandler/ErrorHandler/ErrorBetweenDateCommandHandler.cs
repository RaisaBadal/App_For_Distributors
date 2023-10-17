using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.ErrorHandler
{
    public class ErrorBetweenDateCommandHandler:IcommandHandlerListAndResponse<ErrorBetweenDateRequest,Error>
    {
        private readonly IError error;
        public ErrorBetweenDateCommandHandler(IError error)
        {
            this.error = error;
        }

        public List<Error> Handle(ErrorBetweenDateRequest command)
        {
            return error.GetAllErrorsBetweenDate(command);
        }
    }
}
