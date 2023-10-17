using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class DistributorController : ControllerBase
    {

        private readonly ICommandHandler<InsertDistributorRequest> insertdistributorCmdHandler;
        private readonly ICommandHandler<UpdateDistributorRequest> updatedistributorCmdHandler;
        private readonly ICommandHandler<DeleteDistributor> deletedistributorCmdHandler;
        private readonly ICommandHandler<SoftDeleteDistributor> softDeletedistributorCmdHandler;
        private readonly IcommandHandlerList<GetDistributor> getDistributorCmdHandler;
        public DistributorController(
            ICommandHandler<InsertDistributorRequest> insertdistributorCmdHandler,
            ICommandHandler<UpdateDistributorRequest> updatedistributorCmdHandler,
            ICommandHandler<DeleteDistributor> deletedistributorCmdHandler,
            ICommandHandler<SoftDeleteDistributor> softDeletedistributorCmdHandler,
            IcommandHandlerList<GetDistributor> getDistributorCmdHandler
            )
        {
            this.insertdistributorCmdHandler = insertdistributorCmdHandler;
            this.updatedistributorCmdHandler = updatedistributorCmdHandler;
            this.deletedistributorCmdHandler = deletedistributorCmdHandler;
            this.softDeletedistributorCmdHandler = softDeletedistributorCmdHandler;
            this.getDistributorCmdHandler = getDistributorCmdHandler;
        }
        [HttpPost("Insert")]
        public async Task <IActionResult> InsertDistributori([FromBody] InsertDistributorRequest req)
        {
            try
            {
                if (req == null) return BadRequest("argument is null");
                if (insertdistributorCmdHandler.Handle(req) == true)
                {
                    return Ok("success inserted");
                }
                return NotFound("Failed!");

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }
        }
        [HttpPut("Update")]
        public async Task <IActionResult> UpdateDistributor([FromBody] UpdateDistributorRequest updis)
        {
            try
            {
                if (updis == null) return BadRequest("argument is null");
                if (updatedistributorCmdHandler.Handle(updis) == true)
                {
                    return Ok("success Updated");
                }
                return NotFound("Failed!");

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }

        }
        [HttpDelete("Delete")]
        public async Task <IActionResult> DeleteDistributor(DeleteDistributor deletedistr)
        {
            try
            {
                if (deletedistr == null) return BadRequest("argument is null");
                if (deletedistributorCmdHandler.Handle(deletedistr) == true)
                {
                    return Ok("success Deleted");
                }
                return NotFound("Failed!");

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }

        }

        [HttpPatch("SoftDeleted")]
         public async Task <IActionResult> SoftDistributorDelete(SoftDeleteDistributor softdeletedist)
        {

            try
            {
                if (softdeletedist == null) return BadRequest("argument is null");
                if (softDeletedistributorCmdHandler.Handle(softdeletedist) == true)
                {
                    return Ok("success SoftDeleted");
                }
                return NotFound("Failed!");

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetDistributor()
        {
            try
            {
                var res = getDistributorCmdHandler.Handle();
                if (res == null)
                {
                    return NoContent();
                }
                return Ok(res);

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }
        }
    }
}
