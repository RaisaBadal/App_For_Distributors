using System.Text.RegularExpressions;

namespace PashaBankApp.Validation.Regexi
{
    public class RegexForValidate
    {
        #region Regexs

        public static bool NameIsMatch(string Name)
        {
            Regex rgx = new Regex(@"^[A-Za-z]{5,50}$", RegexOptions.IgnoreCase);
            return rgx.IsMatch(Name);
        }
        public static bool SurnameIsMatch(string Surname)
        {
            Regex rgx = new Regex(@"^[A-Za-z]{5,50}$", RegexOptions.IgnoreCase);
            return rgx.IsMatch(Surname);
        }
        public static bool PhoneIsMatch(string phone)
        {
            Regex rgx = new Regex(@"^+995\s?((?\d{2})?\s?)?\d{7}$", RegexOptions.IgnoreCase);
            return rgx.IsMatch(phone);
        }
        public static bool AddressIsMatch(string Address)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9\s.,-]*$", RegexOptions.IgnoreCase);
            return rgx.IsMatch(Address);
        }
        public static bool EmailIsMatch(string Email)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);
            return rgx.IsMatch(Email);
        }


        #endregion
    }
}
