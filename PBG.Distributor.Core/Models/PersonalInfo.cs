using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("PersonalInfos")]
    public class PersonalInfo
    {
        [Key]
        public int PersonalInfoID { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        [MaxLength(10)]
        public string DocumentSeria { get; set; }
        [MaxLength(10)]
        public string DocumentNumber { get; set; }
        [Required] 
        public DateTime DateofIssue { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string PersonalNumber { get; set; }
        [MaxLength(100)]
        public string? IssuingAuthority { get; set; }
        public Distributor distributor { get; set; }


    }
}
