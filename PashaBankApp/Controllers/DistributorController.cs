using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        public readonly IDistributor idistr;
        public DistributorController(IDistributor idistr)
        {
            this.idistr = idistr;
        }
        [HttpPost("InsertDistributor")]
        public IActionResult InsertDistributori([FromBody] InsertDistributorRequest req)
        {
            try
            {
                if (req == null) return BadRequest("argument is null");//NotFound("argument is null");
                var res = idistr.InsertDistributor(req);
                if (res == false) return StatusCode(501, "Insert failed");

                return Ok("success inserted");

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }
        }
        [HttpPut("UpdateDistributor")]
        public IActionResult UpdateDistributor([FromBody] UpdateDistributorRequest updis)
        {
            try
            {
                if (updis == null) return BadRequest("argument is null");
                var res = idistr.UpdateDistributor(updis);
                if (res == false) return StatusCode(501, "Update failed");
                return Ok("Success Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }

        }
        [HttpDelete("DeleteDistributor")]
        public IActionResult DeleteDistributor(int ditributorID)
        {
            try
            {
                if (ditributorID < 1)
                {
                    return BadRequest("Argument is null");
                }
                var res = idistr.DeleteDistributor(ditributorID);
                if (res == false) return StatusCode(501, "Delete failed");
                return Ok("Success Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }

        }

        [HttpPatch("DistributorSoftDeleted")]
         public IActionResult SoftDistributorDelete(int ditributorID)
        {
           
            try
            {
                if (ditributorID < 1)
                {
                    return BadRequest("Argument is null");
                }
                var res = idistr.SoftDistributorDelete(ditributorID);
                if (res == false) return StatusCode(501, "Delete failed");
                return Ok("Success Soft Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }

        }
        [HttpGet("GetDistributor")]
        public List<GetDistributor> GetDistributor()
        {
            return idistr.GetDistributor();
        }
    }
}
