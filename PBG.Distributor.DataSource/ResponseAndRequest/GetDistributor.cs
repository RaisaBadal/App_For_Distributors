using PashaBankApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace PashaBankApp.ResponseAndRequest
{
    public class GetDistributor
    {
        public int DistributorID { get; set; }
      
        public string DistributorName { get; set; }
       
        public string DistributorLastName { get; set; }
       
        public DateTime BirthDate { get; set; }

      
        public GenderEnum Gender { get; set; }
       
        public int? Recomendedby { get; set; }  //chaiwereba id
        
    }
}
