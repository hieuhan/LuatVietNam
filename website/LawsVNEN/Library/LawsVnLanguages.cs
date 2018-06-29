using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace LawsVNEN.Library
{
    public class LawsVnLanguages
    {
        public static List<Languages> AvailableLanguages = new List<Languages>
        {
            new Languages{ LangFullName = "English", LangCultureName = "en", LanguageId = 2},
            new Languages{ LangFullName = "Vietnamese", LangCultureName = "vi" , LanguageId = 1}
        };

        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.FirstOrDefault(a => a.LangCultureName.Equals(lang)) != null;
        }

        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LangCultureName;
        }

        public static byte GetCurrentLanguageId()
        {
            string lang = null;
            HttpCookie langCookie = HttpContext.Current.Request.Cookies["LawsVnENCulture"];
            lang = langCookie != null ? langCookie.Value : GetDefaultLanguage();
            return AvailableLanguages.Find(x => x.LangCultureName.Equals(lang)).LanguageId;
        }


        public void SetLanguage(string lang)
        {
            if (!IsLanguageAvailable(lang))
                lang = GetDefaultLanguage();
            var cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            HttpCookie langCookie = new HttpCookie("LawsVnENCulture", lang) { Expires = DateTime.Now.AddMonths(1) };
            HttpContext.Current.Response.Cookies.Add(langCookie);
        }

    }

    public class Languages
    {
        public string LangFullName { get; set; }
        public string LangCultureName { get; set; }
        public byte LanguageId { get; set; }
    }
}