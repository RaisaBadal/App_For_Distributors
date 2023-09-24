using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILog dblog;
        public LogController(ILog dblog)
        {
            this.dblog = dblog;
        }
        [HttpGet("GetAllLog")]
        public List<Log> GetAllLog()
        {
            return dblog.GetAllLog();
        }
        [HttpGet("GetAllLogsBetweenDate")]
        public List<Log> GetAllLogsBetweenDate(DateTime start, DateTime end)
        {
            return dblog.GetAllLogsBetweenDate(start, end);
        }
    }
}
