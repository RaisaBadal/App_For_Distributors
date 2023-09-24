using PashaBankApp.Models;

namespace PashaBankApp.ResponseAndRequest
{
    public class InsertDistributorSaleRequest
    {
        public int ProductQuantity { get; set; }
        public int ProductID { get; set; }
        public int DistributorID { get; set; }
    }
}
