using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using System.Runtime.CompilerServices;

namespace PashaBankApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class DistributorSaleController : ControllerBase
    {
      
        private readonly ICommandHandler<InsertDistributorSaleRequest> insertDistributorSaleHandler;
        private readonly ICommandHandler<DeleteDistributorSale> deleteDistributorSaleHandler;
        private readonly ICommandHandler<SoftDeleteDistributorSaleRequest> softDeleteDistributorSaleHandler;
        private readonly IcommandHandlerListAndResponse<GetDistributorSaleRequest, DistributorSale> getDistributorSaleCmdHandler;
        private readonly IcommandHandlerListAndResponse<DistributorSaleGetDateRequest, DistributorSale> getDistributorSaleDateCmdHandler;
        private readonly IcommandHandlerListAndResponse<DistributorSaleGetProductRequest, DistributorSale> getDistributorSaleProductCmdHandler;

        public DistributorSaleController( ICommandHandler<InsertDistributorSaleRequest> insertDistributorSaleHandler,
         ICommandHandler<DeleteDistributorSale> deleteDistributorSaleHandler, ICommandHandler<SoftDeleteDistributorSaleRequest> softDeleteDistributorSaleHandler)
        {
         
            this.insertDistributorSaleHandler = insertDistributorSaleHandler;
            this.deleteDistributorSaleHandler = deleteDistributorSaleHandler;
            this.softDeleteDistributorSaleHandler = softDeleteDistributorSaleHandler;
        }
        [HttpPost("Insert")]
        public async Task <IActionResult> InsertDistributoSale(InsertDistributorSaleRequest insale)
        {
         
            try
            {
                if(insale == null)
                {
                    return BadRequest("Argument is null");
                }
                if (insertDistributorSaleHandler.Handle(insale) == true)
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
        [HttpDelete("Delete")]
        public async Task <IActionResult> DeleteDistributorSale(DeleteDistributorSale deletedisSale)
        {

            try
            {
                if (deletedisSale == null)
                {
                    return BadRequest("Argument is null");
                }
                if (deleteDistributorSaleHandler.Handle(deletedisSale) == true)
                {
                    return Ok("Success Deleted");
                }
                return NotFound("Failed");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }


        }
        [HttpPatch("SoftDelete")]
        public async Task<IActionResult> SoftDeletedDistributorSale(SoftDeleteDistributorSaleRequest deletedisSale)
        {

            try
            {
                if (deletedisSale == null)
                {
                    return BadRequest("Argument is null");
                }
                if (softDeleteDistributorSaleHandler.Handle(deletedisSale) == true)
                {
                    return Ok("Success soft Deleted");
                }
                return NotFound("Failed");
            }
            catch (Exception ex)
            {

                return StatusCode(103, ex.Message);
            }
        }
       [HttpGet("SaleGetDist")]
       public async Task<IActionResult> DistributorSaleGetDist(GetDistributorSaleRequest getSale)
        {
            try
            {
                var res = getDistributorSaleCmdHandler.Handle(getSale);
                if (res == null)
                {
                    return NotFound("Failed");
                }
                return Ok(res);

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }
        }
        [HttpGet("GetDate")]
        public async Task<IActionResult> DistributorSaleGetDate(DistributorSaleGetDateRequest saleDate)
        {
            try
            {
                var res = getDistributorSaleDateCmdHandler.Handle(saleDate);
                if (res == null)
                {
                    return NotFound("Failed");
                }
                return Ok(res);

            }
            catch (Exception exp)
            {
                return StatusCode(103, exp.Message);
            }
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> DistributorSaleGetProduct(DistributorSaleGetProductRequest prod)
        {
            try
            {
                var res = getDistributorSaleProductCmdHandler.Handle(prod);
                if (res == null)
                {
                    return NotFound("Failed");
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
