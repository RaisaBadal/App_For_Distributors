namespace PashaBankApp.ResponseAndRequest
{
    public class GetProductResponse
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ExpireOn { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
