using PashaBankApp.DbContexti;
using PBG.Distributor.Core.Interface;
using PashaBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PashaBankApp.ResponseAndRequest;

namespace PBG.Distributor.Presentation.Repositories
{
    public class LogRepos: ILogRepos
    {
        private readonly DbRaisa dbraisa;
        public LogRepos(DbRaisa dbraisa)
        {
            this.dbraisa = dbraisa;
        }

        #region ActionLog
        public void ActionLog(string message)
        {
            //log cxrilshi chanawerebis shevseba
            if (message != null)
            {
                dbraisa.log.Add(new Log()
                {
                    LogDate = DateTime.Now,
                    LogText = message
                });
                dbraisa.SaveChanges();
            }
        }

        #endregion

        #region GetAllLog
        public List<Log> GetAllLog()
        {
            //yvela chanaweris dabruneba logidan
            return dbraisa.log.ToList();
        }
        #endregion

        #region GetAllLogBetweenDate
        public List<Log> GetAllLogsBetweenDate(LogBetweenDateRequest logs)
        {
            //mocemul drois shualedshi momxdari logebis wamogheba
            return dbraisa.log.Where(i => i.LogDate >= logs.start && i.LogDate <= logs.end).ToList();
        }
        #endregion

    }
}
