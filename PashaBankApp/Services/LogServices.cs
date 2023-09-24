using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class LogServices:ILog
    {
        private readonly DbRaisa dbraisa;
        public LogServices(DbRaisa dbraisa)
        {
            this.dbraisa = dbraisa;
        }

        #region ActionLog
        public void ActionLog(string message)
        {
            if (message != null)
            {
                dbraisa.log.Add(new Models.Log()
                {
                    LogDate = DateTime.Now,
                    LogText = message
                });
                dbraisa.SaveChanges();
            }
        }

        #endregion

        #region GetAllErrors
        public List<Log> GetAllLog()
        {
            return dbraisa.log.ToList();
        }
        #endregion

        #region GetAllLogBetweenDate
        public List<Log> GetAllLogsBetweenDate(DateTime start, DateTime end)
        {
            return dbraisa.log.Where(i => i.LogDate >= start && i.LogDate <= end).ToList();
        }
        #endregion



    }
}
