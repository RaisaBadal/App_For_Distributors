using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Controllers.CommandHandler.ProductHandler;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class ProductController : ControllerBase
    {
        //private readonly IProduct prod;
        private readonly ICommandHandler<InsertProducts> insertProductHandler;
        private readonly ICommandHandler<UpdateProduct> updateProductHandler;
        private readonly ICommandHandler<DeleteProducts> deleteProductHandler;
        private readonly ICommandHandler<SoftDeleteProductRequest> softDeleteProductHandler;
        private readonly IcommandHandlerList<GetProductResponse> getProductHandler;

        public ProductController(//IProduct prod,
         ICommandHandler<InsertProducts> insertProductHandler,
         ICommandHandler<UpdateProduct> updateProductHandler,
         ICommandHandler<DeleteProducts> deleteProductHandler,
         ICommandHandler<SoftDeleteProductRequest> softDeleteProductHandler,
         IcommandHandlerList<GetProductResponse> getProductHandler)
        {
           // this.prod = prod;
            this.insertProductHandler = insertProductHandler;
            this.updateProductHandler = updateProductHandler;
            this.deleteProductHandler = deleteProductHandler;
            this.softDeleteProductHandler= softDeleteProductHandler;
            this.getProductHandler = getProductHandler;

        }
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertProduct(InsertProducts insertpro)
        {
          
            try
            {
                if (insertpro == null)
                {
                    return BadRequest("Argument is null");
                }
                if(insertProductHandler.Handle(insertpro))
                {
                    return Ok("succesfully Inserted!!!");
                }

                 return NotFound("failed to insert");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }


        }
        [HttpPut("Update")]
        public async Task <IActionResult> UpdateProduct(UpdateProduct upProduct)
        {
           
            try
            {
                if (upProduct == null)
                {
                  return  BadRequest("Argument is null");
                }
                if (updateProductHandler.Handle(upProduct)==true)
                {
                    return Ok("succesfully Inserted!!!");
                }
               return  NotFound("Failed");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(DeleteProducts deleteprod)
        {
           
            try
            {
                if (deleteprod == null)
                {
                    return BadRequest("Argument is null");
                }
               if(deleteProductHandler.Handle(deleteprod) == true)
                {
                    return Ok("Success Inserted");
                }
                return NotFound("Failed");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }

        [HttpPatch("SoftDelete")]
        public async Task <IActionResult> SoftDeletedProduct(SoftDeleteProductRequest deleteprod)
        {
           
            try
            {
                if (deleteprod == null)
                {
                    return BadRequest("Argument is null");
                }
                if(softDeleteProductHandler.Handle(deleteprod)==true)
                {
                    return Ok("Success Inserted");
                }
                return NotFound("Failed!");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
        [HttpGet("GettAll")]
        public async Task<IActionResult> getProductResponses()

        {
            try
            {
                var res = getProductHandler.Handle();
                if (res == null) return NotFound("product not exist");

                return Ok(res);

            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message + "," + ex.StackTrace);
            }
        }
    }
}
