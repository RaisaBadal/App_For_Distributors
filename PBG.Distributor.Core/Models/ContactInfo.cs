using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("ContactInfos")]
    public class ContactInfo
    {
        [Key]
        public int ContactInfoID { get; set; }
        [Required]
        public ContactTypeEnum contactType { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactInformation { get; set; }
     
        public Distributor distributor { get; set; }



    }
}
