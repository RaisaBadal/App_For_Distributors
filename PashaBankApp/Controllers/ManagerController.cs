using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> RegistrationManager(InsertManager signUp)
        {
          
                try
                {
                    if (signUp.UserName ==null) return BadRequest("argument is null");
                    var res = await manager.RegistrationManager(signUp);
                    if (res == false) return StatusCode(501, "Insert failed");
                    return Ok("Success Inserted");
                }
                catch (Exception ex)
                {

                    return StatusCode(103, ex.Message);
                }
        }
       
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(GetManagerAuthent sign)
        {
            var res = await manager.SignIn(sign); 

            if (res != null) return Ok(res);

            return BadRequest(" warumatebeli");
        }

    }
}
