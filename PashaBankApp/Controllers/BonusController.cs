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
        public IActionResult CalcBonus(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate == null || endDate==null) return BadRequest("Argument is null");
                var res = bonus.CalcBonus(startDate,endDate);
                if (res == false) return StatusCode(501, "Insert failed");

                return Ok("success inserted");
            }
            catch (Exception ex)
            {
                return StatusCode(103, ex.Message);
            }
        }

        [HttpGet("GetBonusByNameSurname")]
        public List<SortBonus> GetBonusByNameSurname(string name, string surname)
        {
            return bonus.GetBonusByNameSurname(name,surname);
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
