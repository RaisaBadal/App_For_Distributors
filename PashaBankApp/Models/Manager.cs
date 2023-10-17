using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Managers")]
    public class Manager:IdentityUser
    {
        [Key]
        public int ManagerID { get; set; }
        [Required]
        [MaxLength(15)]
        public string PersonalNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }


   



    }
}
