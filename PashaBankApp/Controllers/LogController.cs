using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class LogController : ControllerBase
    {

        private readonly IcommandHandlerList<Log> allLogHandler;
        private readonly IcommandHandlerListAndResponse<LogBetweenDateRequest,Log>logBetweenDateHandler;

        public LogController(IcommandHandlerListAndResponse<LogBetweenDateRequest, Log> logBetweenDateHandler, IcommandHandlerList<Log> allLogHandler)
        {
       
            this.logBetweenDateHandler = logBetweenDateHandler;
            this.allLogHandler= allLogHandler;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>  GetAllLog()
        {
            try
            {
                var res = allLogHandler.Handle();
                if(res == null) 
                {
                    return NotFound("Logs not exist!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
           
        }
        [HttpPost("BetweenDate")]
        public async Task<IActionResult> GetAllLogsBetweenDate(LogBetweenDateRequest dateresponse)
        {
            try
            {
                var res = logBetweenDateHandler.Handle(dateresponse);
                if (res == null)
                {
                    return NotFound("Logs not exist!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103,ex.Message);
            }
        }
    }
}
