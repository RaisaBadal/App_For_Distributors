using PashaBankApp.Controllers.Interface;
using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers.CommandHandler.LogHandler
{
    public class AllLogCommandHandler:IcommandHandlerList<Log>
    {
        private readonly ILog logger;

        public AllLogCommandHandler(ILog logger)
        {
            this.logger = logger;
        }
        public List<Log> Handle()
        {
            return logger.GetAllLog();
        }
    }
}
