using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;


namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IError dberror;
        public ErrorController(IError dberror)
        {
            this.dberror = dberror;
        }
        [HttpGet("GetAllErrors")]
        public List<Error> GetAllErrors()
        {
            return dberror.GetAllErrors();
        }
        [HttpGet("GetAllErrorsBetweenDate")]
        public List<Error> GetAllErrorsBetweenDate(DateTime start, DateTime end)
        {
            return dberror.GetAllErrorsBetweenDate(start, end);
        }
    }
}
