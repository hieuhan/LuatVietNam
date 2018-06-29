
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Web.UI.WebControls;
using System.Web;
using ICSoft.CMSLib;
using System.Threading;

namespace ICSoft.HelperLib
{
    public class LanguageHelpers
    {
        public static byte Vietnamese
        {
            get { return 1; }
            set { }
        }
        public static byte English
        {
            get { return 2; }
            set { }
        }
        //---------------------------------------------------------------------------------------
        public static string GetCurentName()
        {
            string RetVal = "";// = ConstantHelpers.DefaultLanguage;
            try
            {
                HttpRequest Request = HttpContext.Current.Request;
                if (string.IsNullOrEmpty(Request["Language"]) == false)// get first in request param
                {
                    //ICSoft.CMSLib.utils.Log.writeLog("Request: " + Request["Language"], ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    RetVal = Request["Language"];
                }
                else
                {
                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["Language"] != null)// get second in session
                    {
                        //ICSoft.CMSLib.utils.Log.writeLog("Session: " + HttpContext.Current.Session["Language"].ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                        RetVal = HttpContext.Current.Session["Language"].ToString();
                    }
                    else
                    {
                        if (Request.Cookies != null && Request.Cookies["Language"] != null)// get last in cookies
                        {
                            //ICSoft.CMSLib.utils.Log.writeLog("Cookies: " + Request.Cookies["Language"].Value, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                            RetVal = string.IsNullOrEmpty(Request.Cookies["Language"].Value) ? ConstantHelpers.DefaultLanguage : Request.Cookies["Language"].Value;
                        }
                        else
                        {
                            RetVal = ConstantHelpers.DefaultLanguage;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                RetVal = ConstantHelpers.DefaultLanguage;
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal.ToLower();
        }

        //---------------------------------------------------------------------------------------
        public static byte GetCurentId()
        {
            return GetCurent().LanguageId;
        }

        //---------------------------------------------------------------------------------------
        public static byte GetCurentDocLanguageId()
        {
            byte RetVal = Vietnamese;
            try
            {
                HttpRequest Request = HttpContext.Current.Request;
                if (string.IsNullOrEmpty(Request["DocLanguage"]) == false)// get first in request param
                {                    
                    byte.TryParse(Request["DocLanguage"], out RetVal);
                }
                else
                {
                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["DocLanguage"] != null)// get second in session
                    {
                        byte.TryParse(HttpContext.Current.Session["DocLanguage"].ToString(), out RetVal);
                    }
                    else
                    {
                        if (Request.Cookies != null && Request.Cookies["DocLanguage"] != null)// get last in cookies
                        {
                            string TempLanguageId = string.IsNullOrEmpty(Request.Cookies["DocLanguage"].Value) ? ConstantHelpers.DefaultLanguage : Request.Cookies["DocLanguage"].Value;
                            byte.TryParse(TempLanguageId, out RetVal);
                        }
                        else
                        {
                            RetVal = Vietnamese;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                RetVal = Vietnamese;
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            if(RetVal==0)
                RetVal = Vietnamese;
            return RetVal;
        }
        //---------------------------------------------------------------------------------------
        public static Languages GetCurent()
        {
            Languages RetVal;
            try
            {
                string LanguageName = GetCurentName().ToLower();
                List<Languages> l_Languages = CacheHelpers<Languages>.GetList();
                if (l_Languages == null)
                {
                    l_Languages = Languages.Static_GetList();
                    CacheHelpers<Languages>.InsertList(l_Languages);
                }
                RetVal = l_Languages.Find(x => x.LanguageName.ToLower() == LanguageName);
                if (RetVal == null)
                    RetVal = new Languages();
            }
            catch (Exception ex)
            {
                RetVal = new Languages();
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        //---------------------------------------------------------------------------------------
        public static Languages GetCurent(string LanguageName)
        {
            Languages RetVal;
            try
            {
                
                List<Languages> l_Languages = CacheHelpers<Languages>.GetList();
                if (l_Languages == null)
                {
                    l_Languages = Languages.Static_GetList();
                    CacheHelpers<Languages>.InsertList(l_Languages);
                }
                RetVal = l_Languages.Find(x => x.LanguageName.ToLower() == LanguageName.ToLower());
                if (RetVal == null)
                    RetVal = new Languages();
            }
            catch (Exception ex)
            {
                RetVal = new Languages();
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        //---------------------------------------------------------------------------------------
        public static List<Languages> GetListNotCurent()
        {
            List<Languages> RetVal = new List<Languages>();
            try
            {
                string LanguageName = GetCurentName();
                List<Languages> l_Languages = CacheHelpers<Languages>.GetList();
                if (l_Languages == null)
                {
                    l_Languages = Languages.Static_GetList();
                    CacheHelpers<Languages>.InsertList(l_Languages);
                }
                foreach (Languages m_Languages in l_Languages)
                {
                    if (m_Languages.LanguageName.ToLower() != LanguageName)
                        RetVal.Add(m_Languages);
                }
            }
            catch (Exception ex)
            {
                RetVal = new List<Languages>();
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            
            return RetVal;
        }
        //---------------------------------------------------------------------------------------
        public static void SetCurentName(string LanguageName)
        {
            try
            {
                LanguageName = LanguageName.ToLower();
                if (string.IsNullOrEmpty(LanguageName) == false)
                {
                    HttpResponse Response = HttpContext.Current.Response;
                    if (HttpContext.Current.Session != null)// set first in session
                    {
                        HttpContext.Current.Session["Language"] = LanguageName;
                        //ICSoft.CMSLib.utils.Log.writeLog("Set Session: " + HttpContext.Current.Session["Language"].ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    }
                    // set in cookie
                    HttpCookie LanguageCookie = new HttpCookie("Language");
                    LanguageCookie.Value = LanguageName;
                    DateTime dtExpiry = DateTime.Now.AddDays(15);
                    LanguageCookie.Expires = dtExpiry;
                    Response.Cookies.Remove("Language");
                    Response.Cookies.Add(LanguageCookie);
                    // set culture
                    Languages m_Languages = GetCurent();
                    HttpCookie CultureCookie = CookieHelpers.SetCookie("CultureInfo", m_Languages.LanguageCode, 15);
                    Response.Cookies.Remove("CultureInfo");
                    Response.Cookies.Add(CultureCookie);
                    //ICSoft.CMSLib.utils.Log.writeLog("Set Cookie: " + Language, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //---------------------------------------------------------------------------------------
        public static void SetCurentDocLanguageId(string LanguageId)
        {
            try
            {

                if (string.IsNullOrEmpty(LanguageId) == false)
                {
                    HttpResponse Response = HttpContext.Current.Response;
                    if (HttpContext.Current.Session != null)// set first in session
                    {
                        HttpContext.Current.Session["DocLanguage"] = LanguageId;
                    }
                    // set in cookie
                    HttpCookie LanguageCookie = new HttpCookie("DocLanguage");
                    LanguageCookie.Value = LanguageId;
                    DateTime dtExpiry = DateTime.Now.AddDays(15);
                    LanguageCookie.Expires = dtExpiry;
                    Response.Cookies.Remove("DocLanguage");
                    Response.Cookies.Add(LanguageCookie);
                    // set culture
                    Languages m_Languages = GetCurent();
                    
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //---------------------------------------------------------------------------------------
        public static bool IsCultureVietnamese()
        {
            bool RetVal = false;
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == ConstantHelpers.CULTURE_VN)
            {
                RetVal = true;
            }
            return RetVal;
        }
    } 
}

