using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManager manager;
        public ManagerController(IManager manager)
        {
            this.manager = manager;
        }
        [HttpPost("Registration")]
        public IActionResult RegistrationManager(InsertManager signUp)
        {
          
                try
                {
                    if (signUp.UserName ==null) return BadRequest("argument is null");
                    var res = manager.RegistrationManager(signUp);
                    if (res == false) return StatusCode(501, "Insert failed");
                    return Ok("Success Inserted");
                }
                catch (Exception ex)
                {

                    return StatusCode(103, ex.Message);
                }
        }
        [HttpPatch("signin")]
        public IActionResult SignIn(GetManagerAuthent manAuth)
        {
            try
            {
                if (manager == null)
                {
                    return BadRequest("Argument is null");
                }
                var res = manager.SignIn(manAuth);
                if (res == false)
                {
                    return StatusCode(501, "failed");
                }
                else
                {
                    return Ok("Success");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }
    }
}
