using System.ComponentModel.DataAnnotations;

namespace PashaBankApp.ResponseAndRequest
{
    public class InsertManager
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
