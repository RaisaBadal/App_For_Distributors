using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Distributors")]
    public class Distributor
    {
        [Key]
        public int DistributorID { get; set; }
        [Required]
        [MaxLength (50)]
        public string DistributorName { get;set;}
        [Required]
        [MaxLength (50)]
        public string DistributorLastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }
        public string? ExpireOn { get; set; }
        public DateTime? ExpireDate { get; set; }
       
        public int? Recomendedby { get; set; }  //chaiwereba id
        public int? Level { get; set; } = 0;//ganvsazghvrot meramdene donis momwvevia
        public int? CountOffInvetedDistributor { get; set; } //ramdeni distributori moiwvia
        [ForeignKey ("personalinfo")]
        public int PersonalInfoID { get; set; }
        public PersonalInfo personalinfo { get; set; } //1:1
        [ForeignKey("contactinfo")]
        public int ContactinfoID { get; set; }
        public ContactInfo contactinfo { get; set; } //1:1
        
        [ForeignKey("address")]
        public int AddressID { get; set; }
        public Address address { get; set; } //1:1
       /* [ForeignKey("DistributorSalesID")]
        public int DistributorSalesID { get; set; }*/
        public List<DistributorSale> distributorSales { get; set; }
        public List<Bonus> bonus { get; set; }

    }
}
