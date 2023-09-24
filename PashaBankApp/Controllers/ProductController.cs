using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct prod;
        public ProductController(IProduct prod)
        {
            this.prod = prod;
        }
        [HttpPost("InsertProduct")]
        public IActionResult InsertProduct(string productName, decimal price)
        {
            try
            {
                if (productName == null||price==0) return BadRequest("argument is null");
                var res = prod.InsertProduct(productName,price);
                if (res == false) return StatusCode(501, "Insert failed");
                return Ok("Success Inserted");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }


        }
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(int productID, string productName, decimal price)
        {
            try
            {
                if (productID < 1||productName == null||price==0) return BadRequest("argument is null");
                var res = prod.UpdateProduct(productID, productName, price);
                if (res == false) return StatusCode(501, "Update failed");
                return Ok("Success Updated");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            try
            {
                if (productID <1) return BadRequest("argument is null");
                var res = prod.DeleteProduct(productID);
                if (res == false) return StatusCode(501, "Delete failed");
                return Ok("Success Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }

        [HttpPatch("SoftDeletedProduct")]
        public IActionResult SoftDeletedProduct(int productID)
        {
            try
            {
                if (productID < 1) return BadRequest("argument is null");
                var res = prod.SoftDeletedProduct(productID);
                if (res == false) return StatusCode(501, "Delete failed");
                return Ok("Success Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
        [HttpGet("GettAllProduct")]
        public List<GetProductResponse> getProductResponses()
        {
            return prod.getProductResponses();
        }
    }
}
