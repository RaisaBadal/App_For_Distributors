using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("ManagerAuthentification")]
    public class ManagerAuthentification
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [ForeignKey("manager")]
        public int ManagerID { get; set; }
        public Manager manager { get; set; }

       
         
    }
}
