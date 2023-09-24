using PashaBankApp.DbContexti;
using PashaBankApp.Models;

namespace PashaBankApp.Services.Interface
{
    public interface ILog
    {
        List<Log> GetAllLog();

        List<Log> GetAllLogsBetweenDate(DateTime start, DateTime end);
        
    }
}
