using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PashaBankApp.Models;

namespace PashaBankApp.Controllers.CommandHandler.LogHandler
{
    public class LogBetweenDateCommandHandler:IcommandHandlerListAndResponse<LogBetweenDateRequest,Log>
    {
        private readonly ILog logger;
        public LogBetweenDateCommandHandler(ILog logger)
        {
            this.logger = logger;
        }

        public List<Log> Handle(LogBetweenDateRequest command)
        {
            return logger.GetAllLogsBetweenDate(command);
        }
    }
}
