using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Errors")]
    public class Error
    {
        [Key]
        public int ErrorId { get; set; }
        public String Text { get; set; }
        public ErrorTypeEnum ErrorType { get; set; }
        public  DateTime TimeofOccured { get; set; }

    }
}
