using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PBG.Distributor.Core.Interface;
using System.Numerics;

namespace PashaBankApp.Services
{
    public class LogServices:ILog
    {
        private readonly ILogRepos ilogrepos;
        public LogServices(ILogRepos ilogrepos)
        {
            this.ilogrepos = ilogrepos;
        }

        #region GetAllLog
        public List<Log> GetAllLog()
        {
            return ilogrepos.GetAllLog();
        }
        #endregion

        #region GetAllLogBetweenDate
        public List<Log> GetAllLogsBetweenDate(LogBetweenDateRequest logs)
        {
            return ilogrepos.GetAllLogsBetweenDate(logs);
        }
        #endregion



    }
}
