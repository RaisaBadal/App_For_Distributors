using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using System.Runtime.CompilerServices;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorSaleController : ControllerBase
    {
        public readonly IDistributorSale distrsale;
        public DistributorSaleController(IDistributorSale distrsale)
        {
            this.distrsale = distrsale;
        }
        [HttpPost("InsetDistributorSale")]
        public IActionResult InsertDistributoSale(InsertDistributorSaleRequest insale)
        {
            try
            {
                if (insale == null) return BadRequest("Argument is null");
                var res = distrsale.InsertDistributoSale(insale);
                if (res == false) return StatusCode(501, "Insert failed");

                return Ok("success inserted");
            }
            catch (Exception ex)
            {

              return StatusCode(103, ex.Message);
            }
            
        }
        [HttpDelete("DeleteDistributorSale")]
        public IActionResult DeleteDistributorSale(int distributorSaleID)
        {
            try
            {
                if (distributorSaleID <1) return BadRequest("Argument is null");
                var res = distrsale.DeleteDistributorSale(distributorSaleID);
                if (res == false) return StatusCode(501, "Delete failed");

                return Ok("success Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
        [HttpGet("DistributorSaleGetDist")]
       public List<DistributorSale> DistributorSaleGetDist(int distributorID)
        {
            return distrsale.DistributorSaleGetDist(distributorID);
        }
        [HttpGet("DistributorSaleGetDate")]
        public List<DistributorSale> DistributorSaleGetDate(DateTime saleDate)
        {
            return distrsale.DistributorSaleGetDate(saleDate);
        }
        [HttpGet("DistributorSaleGetProduct")]
        public List<DistributorSale> DistributorSaleGetProduct(int productID)
        {
            return distrsale.DistributorSaleGetProduct(productID);
        }

       
             [HttpPatch("DeleteDistributorSale")]
        public IActionResult SoftDeletedDistributorSale(int distributorSaleID)
        {
            try
            {
                if (distributorSaleID < 1) return BadRequest("Argument is null");
                var res = distrsale.SoftDeletedDistributorSale(distributorSaleID);
                if (res == false) return StatusCode(501, "Expire failed");

                return Ok("success Expired");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
    }
}
