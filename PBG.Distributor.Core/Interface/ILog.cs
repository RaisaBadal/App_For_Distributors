using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface ILog
    {
        List<Log> GetAllLog();

        List<Log> GetAllLogsBetweenDate(LogBetweenDateRequest logs);
        
    }
}
