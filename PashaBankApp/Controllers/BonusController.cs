using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ManagerOnly")]
    public class BonusController : ControllerBase
    {

        private readonly ICommandHandler<InsertBonus> insertBonusCmdHandler;
        private readonly IcommandHandlerListAndResponse<GetBonus,SortBonus> GetBySurnameBonusCmdHandler;
        private readonly IcommandHandlerList<SortBonusAsc> orderAscBonusCmdHandler;
        private readonly IcommandHandlerList<SortBonus> orderDescBonusCmdHandler;

        public BonusController(ICommandHandler<InsertBonus> insertBonusCmdHandler,
           IcommandHandlerListAndResponse<GetBonus, SortBonus> GetBySurnameBonusCmdHandler,
           IcommandHandlerList<SortBonusAsc> orderAscBonusCmdHandler,
           IcommandHandlerList<SortBonus> orderDescBonusCmdHandler
           )
        {
            this.insertBonusCmdHandler= insertBonusCmdHandler;
            this.GetBySurnameBonusCmdHandler = GetBySurnameBonusCmdHandler;
            this.orderAscBonusCmdHandler = orderAscBonusCmdHandler;
            this.orderDescBonusCmdHandler = orderDescBonusCmdHandler;
        }
        [HttpPost("Insert")]
        public async Task <IActionResult> CalcBonus(InsertBonus insertBonus)
        {
            try
            {
                if (insertBonus == null)
                {
                    return BadRequest("Argument is null");
                    
                }
                if(insertBonusCmdHandler.Handle(insertBonus)==true)
                {
                    return Ok("success inserted bonus");
                }
                return NotFound("Failed");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }

        [HttpGet("ByNameSurname")]
        public async Task<IActionResult> GetBonusByNameSurname(GetBonus getbonus)
        {
            try
            {
                
                if (getbonus == null)
                {
                    return BadRequest("Argument is null");

                }
                var res = GetBySurnameBonusCmdHandler.Handle(getbonus);
                if (res ==null)
                {
                   return NotFound("Failed");
                   
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }
        [HttpGet("SortDesc")]
        public async Task<IActionResult> SortByBonusDesc()
        {
            try
            {
                var res = orderAscBonusCmdHandler.Handle();

               
                if (res == null)
                {
                    return NotFound("Failed");

                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }
        [HttpGet("SortAsc")]
        public async Task<IActionResult> SortByBonusAsc()
        {
            try
            {
                var res = orderDescBonusCmdHandler.Handle();


                if (res == null)
                {
                    return NotFound("Failed");

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
