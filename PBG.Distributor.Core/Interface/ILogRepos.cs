using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBG.Distributor.Core.Interface
{
    public interface ILogRepos
    {
        List<Log> GetAllLog();

        List<Log> GetAllLogsBetweenDate(LogBetweenDateRequest logs);
        void ActionLog(string message);
    }
}
