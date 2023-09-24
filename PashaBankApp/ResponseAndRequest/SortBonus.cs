using System.ComponentModel.DataAnnotations;

namespace PashaBankApp.ResponseAndRequest
{
    public class SortBonus
    {
        public int BonusID { get; set; }

        public DateTime DateOfBonus { get; set; }
       
        public decimal BonusAmount { get; set; }
        public int DistributorID { get; set; }
    }
}
