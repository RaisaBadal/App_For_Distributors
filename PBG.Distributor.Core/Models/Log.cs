using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public string LogText { get; set; }
        public DateTime LogDate { get; set; }

        
    }
}
