using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Bonus")]
    public class Bonus
    {
        [Key]
        public int BonusID { get; set; }

        [Required]
        public DateTime DateOfBonus { get; set; }
        [Required]
        public decimal BonusAmount { get; set; }

        [ForeignKey("distributor")]
        public int DistributorID { get; set; }
        public Distributor distributor { get; set; }
        

    }
}
