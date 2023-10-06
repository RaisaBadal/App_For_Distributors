using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Managers")]
    public class Manager
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(15)]
        public string PersonalNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(40)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(30)]
        public string Mail { get; set; }
        public ManagerAuthentification authManager { get; set; }
        public ManagerCookies managercookies { get; set; }



    }
}
