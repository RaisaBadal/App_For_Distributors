using PashaBankApp.Enums;

namespace PashaBankApp.ResponseAndRequest
{
    public class UpdateDistributorRequest
    {
        public int DistributorID { get; set; } 
        public string addressInfo { get; set; }
        public AddressTypeEnum addressType { get; set; }
        public GenderEnum gender { get; set; }
        public DateTime birthdate { get; set; }
        public string distributorName { get; set; }

        public ContactTypeEnum contactType { get; set; }

        public string contactInformation { get; set; }
        public DocumentTypeEnum documentType { get; set; }
        public string documentSeria { get; set; }
        public string documentNumber { get; set; }
        public DateTime dateofIssue { get; set; }

        public DateTime expireDate { get; set; }
        public string personalNumber { get; set; }

        public string issuingAuthority { get; set; }
        public int recomendedby { get; set; }
    }
}
