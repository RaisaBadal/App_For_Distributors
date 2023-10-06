using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        public readonly Ibonus bonus;
        public BonusController(Ibonus bonus)
        {
            this.bonus = bonus;
        }
        [HttpPost("InsertBonus(CalcBonus)")]
        public IActionResult CalcBonus(InsertBonus insertBonus)
        {
            try
            {
                if (insertBonus.StartDate == null || insertBonus.EndDate==null) return BadRequest("Argument is null");
                var res = bonus.CalcBonus(insertBonus);
                if (res == false) return StatusCode(501, "Insert failed");

                return Ok("success inserted");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }

        [HttpGet("GetBonusByNameSurname")]
        public List<SortBonus> GetBonusByNameSurname(GetBonus getbonus)
        {
            return bonus.GetBonusByNameSurname(getbonus);
        }
        [HttpGet("SortByBonusDesc")]
        public List<SortBonus> SortByBonusDesc()
        {
            return bonus.SortByBonusDesc();
        }
        [HttpGet("SortByBonusAsc")]
        public List<SortBonus> SortByBonusAsc()
        {
            return bonus.SortByBonusAsc();
        }
    }
}
