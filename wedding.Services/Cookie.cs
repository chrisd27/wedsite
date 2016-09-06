using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace wedding.Services
{
    public class Cookie
    {
        public void createCookie(HttpContextBase context, string cookieName, string domain, string username, string nickname, int weddingId)
        {
            
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Domain = domain;
            cookie.Expires = new System.DateTime(2018, 06, 17);
            cookie.Value = "true";
            cookie.Values["username"] = username;
            cookie.Values["nickname"] = nickname;
            cookie.Values["id"] = weddingId.ToString();
            context.Response.Cookies.Add(cookie);

        }
    }
    
}
