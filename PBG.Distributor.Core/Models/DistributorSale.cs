using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Models
{
    [Table("DistributorSales")]
    public class DistributorSale
    {
        [Key]
        public int DistributorSaleID { get; set; }
       
        public DateTime SaleDate { get; set; }
       
        public int? ProductQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? ExpireOn { get; set; }
        public string? status { get; set; }

        public DateTime? ExpireDate { get; set; } 
        [ForeignKey("product")] //kavshiri distributorsalestan 1:n
        public int ProductID { get; set; } //product tabletan dakavshirebistvis
        public Product product { get; set; }
        [ForeignKey("distributor")]
        public int distributorID { get; set; }
        public Distributor distributor { get; set; }

    }
}
