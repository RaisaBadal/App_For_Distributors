using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("ManagerCookies")]
    public class ManagerCookies
    {
        [Key]
        public int ManagerCookiesID { get; set; }
        public string Token { get; set; }
        [ForeignKey("manager")]
        public int ManagerID { get; set;}
        public Manager manager { get; set; }
    }
}
