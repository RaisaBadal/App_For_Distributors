using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;


namespace PashaBankApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class ErrorController : ControllerBase
    {
        private readonly IcommandHandlerList<Error> allErrorHandler;
        private readonly IcommandHandlerListAndResponse<ErrorBetweenDateRequest, Error> ErrorBetweenDateHandler;

        public ErrorController(IcommandHandlerList<Error> allErrorHandler, IcommandHandlerListAndResponse<ErrorBetweenDateRequest, Error> ErrorBetweenDateHandler)
        {
           this.allErrorHandler= allErrorHandler;
            this.ErrorBetweenDateHandler= ErrorBetweenDateHandler;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllErrors()
        {
            try
            {
                var res = allErrorHandler.Handle();
                if (res == null)
                {
                    return NotFound("Errors not exist!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }
        [HttpPost("BetweenDate")]
        public async Task<IActionResult> GetAllErrorsBetweenDate(ErrorBetweenDateRequest errorrequest)
        {
            try
            {
                var res = ErrorBetweenDateHandler.Handle(errorrequest);
                if (res == null)
                {
                    return NotFound("Errors not exist!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }
    }
}
