using PashaBankApp.DbContexti;

namespace PashaBankApp.Cookies
{
    public class CookiesForManager
    {
        private readonly DbRaisa dbraisa;
        private readonly IHttpContextAccessor _ict;

        public CookiesForManager(DbRaisa dbraisa, IHttpContextAccessor ict)
        {
            this.dbraisa = dbraisa;
            _ict = ict;
        }

        public void ManageCookieforManager(int ID)
        {
            string token=GenerateToken();
            if (_ict.HttpContext==null)
            {
                throw new Exception("ver moidzebna");
            }
            else
            {
                var response = _ict.HttpContext.Response.Cookies;
                var cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddHours(1);
                cookie.Secure = true;
                cookie.Path = "/";
                response.Append("ManagerCookies",token, cookie);

                StoredToDatabase(token, ID);
            }
        }
        public string GenerateToken()
        {
            string token=" ";
            string source = "ajsdheyjasguwixnbhjjjjashdieueoqksxsmn2384be45bahsbdxanknaksnhb121932943@@@@ahgwhsdbdjwb";
            Random random= new Random();
            foreach (var item in source)
            {
                token += item + random.Next(token.Length, token.Length + 2).ToString();
                if (token.Length > 30)
                {
                    break;
                }
            }
            return token;
            
        }
        public void StoredToDatabase(string token, int ID)
        {
            var cookiesformanager = new Models.ManagerCookies
            {
                Token = token,
                ManagerID = ID
            };
            dbraisa.managerCookies.Add(cookiesformanager);
            dbraisa.SaveChanges();
        }
    }
}
