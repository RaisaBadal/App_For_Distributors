using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string  ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ExpireOn { get; set; }
        public DateTime? ExpireDate { get; set; }
        public List<DistributorSale> distributorSales { get; set; }
    }
}
