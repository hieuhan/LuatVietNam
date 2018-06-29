using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ICSoft.HelperLib
{
    public class CookieHelpers
    {
        public static HttpCookie SetCookie(string CookieName, string CookieValue, int DayExpire)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Value = CookieValue;
            cookie.Expires.AddDays(DayExpire);
            return cookie;
        }

        public static string GetCookieValue(HttpCookie Cookie)
        {
            string RetVal = "";
            if (Cookie != null && Cookie.Value != null)
            {
                RetVal = Cookie.Value;
            }
            return RetVal;
        }
    }
}
