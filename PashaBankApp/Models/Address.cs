using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int AdreessID { get; set; }
        public AddressTypeEnum AddressType { get; set; }
        [Required]
        [MaxLength(100)]
        public string AddressInfo { get; set; }
        public Distributor distributor{ get; set; }
    }
}
