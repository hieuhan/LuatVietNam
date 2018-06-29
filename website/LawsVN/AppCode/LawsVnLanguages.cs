using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace LawsVN.Library
{
    public class LawsVnLanguages
    {
        /// <summary>
        /// Danh sách ngôn ngữ hỗ trợ
        /// </summary>
        public static readonly List<Languages> AvailableLanguages = new List<Languages>
        {
            new Languages{ LangFullName = "Vietnamese", LangCultureName = "vi" , LanguageId = 1},
            new Languages{ LangFullName = "English", LangCultureName = "en", LanguageId = 2}
        };

        private static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.FirstOrDefault(a => a.LangCultureName.Equals(lang)) != null;
        }

        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LangCultureName;
        }

        public static byte GetCurrentLanguageId()
        {
            return 1;
            //string lang = null;
            //HttpCookie langCookie = HttpContext.Current.Request.Cookies["LawsVnCulture"];
            //lang = langCookie != null ? langCookie.Value : GetDefaultLanguage();
            //return AvailableLanguages.Find(x => x.LangCultureName.Equals(lang)).LanguageId;
        }


        public void SetLanguage(string lang)
        {
            if (!IsLanguageAvailable(lang))
                lang = GetDefaultLanguage();
            var cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            //HttpCookie langCookie = new HttpCookie("LawsVnCulture", lang) { Expires = DateTime.Now.AddMonths(1) };
            //if (HttpContext.Current.Request.Cookies == null || HttpContext.Current.Request.Cookies["LawsVnCulture"] == null)
            //    HttpContext.Current.Response.Cookies.Add(langCookie);
        }

    }

    public class Languages
    {
        public string LangFullName { get; set; }
        public string LangCultureName { get; set; }
        public byte LanguageId { get; set; }
    }
}