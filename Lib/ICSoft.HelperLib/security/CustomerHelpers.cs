using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
namespace ICSoft.HelperLib
{
    public class CustomerHelpers
    {
        public static int GetCurentId()
        {
            int RetVal = 0;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["CustomerId"] == null) ? 0 : Int32.Parse(HttpContext.Current.Session["CustomerId"].ToString());
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
            
        }
        //==================================================================================
        public static int GetCurentId(HttpContext context)
        {

            return GetCurent(context).CustomerId;

        }
        //==================================================================================
        /// <summary>
        /// get curent funring account infomation
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Customers GetCurent(HttpContext context)
        {

            Customers RetVal = new Customers();
            try
            {
                if (context.Session != null && context.Session["CustomerId"] != null)
                {
                    RetVal.CustomerId = int.Parse(context.Session["CustomerId"].ToString());
                    RetVal.CustomerFullName = context.Session["CustomerFullName"].ToString();
                    RetVal.CustomerMobile = context.Session["CustomerMobile"].ToString();
                    RetVal.CustomerMail = context.Session["CustomerMail"].ToString();
                    RetVal.Avatar = context.Session["CustomerAvatar"].ToString();
                    RetVal.StatusId = 1;
                }
                else if (context.Session != null && context.Session["CustomerId"] == null)
                {
                    ClearCurent(context);
                }
                else if (context.Session == null && context.Request.Cookies != null && context.Request.Cookies["AuthCode"] != null && context.Request.Cookies["ASP.NET_SessionId"] != null)// get second in session
                {
                    string SessionId = context.Request.Cookies["ASP.NET_SessionId"].Value;
                    string AuthCode = context.Request.Cookies["AuthCode"].Value;
                    string CustomerIdString = AuthCode.Substring(32);
                    //sms.utils.LogFiles.LogInfo("From Cookie:" + CustomerIdString);
                    if (sms.utils.md5.MD5Hash(ConstantHelpers.MEMBERDEFAULTPASS + CustomerIdString + SessionId) == AuthCode.Substring(0, 32))
                    {
                        
                        RetVal.CustomerId = int.Parse(CustomerIdString);
                        RetVal = RetVal.Get();
                    }
                    else
                    {
                        sms.utils.LogFiles.LogInfo("From Cookie wrong auth");
                        ClearCurent(context);
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            
            return RetVal;
        }
        public static string GetCurentFullName()
        {
            if (HttpContext.Current.Session == null)
            {
                return "";
            }
            return (HttpContext.Current.Session["CustomerFullName"] == null) ? "" : HttpContext.Current.Session["CustomerFullName"].ToString();
        }
        public static string GetCurentFullName(HttpContext context)
        {
            if (HttpContext.Current.Session == null)
            {
                return GetCurent(context).CustomerFullName;
            }
            return (HttpContext.Current.Session["CustomerFullName"] == null) ? "" : HttpContext.Current.Session["CustomerFullName"].ToString();
        }
        public static string GetCurentExpiredTime()
        {
            if (HttpContext.Current.Session == null)
                return "";
            return (HttpContext.Current.Session["CustomerExpiredTime"] == null) ? "" : HttpContext.Current.Session["CustomerExpiredTime"].ToString();
        }
        
        public static void SetCurentId(int CustomerId)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["CustomerId"] = CustomerId.ToString();
            // set in cookie
            string AuthCode = ConstantHelpers.MEMBERDEFAULTPASS + CustomerId.ToString() + HttpContext.Current.Session.SessionID;

            AuthCode = sms.utils.md5.MD5Hash(AuthCode);
            AuthCode = AuthCode + CustomerId.ToString();
            HttpCookie AuthCookie = new HttpCookie("AuthCode");
            AuthCookie.Value = AuthCode;
            DateTime dtExpiry = DateTime.MinValue;
            AuthCookie.Expires = dtExpiry;
            HttpContext.Current.Response.Cookies.Remove("AuthCode");
            HttpContext.Current.Response.Cookies.Add(AuthCookie);
        }
        public static void SetCurentId(Customers m_Customers)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["CustomerId"] = m_Customers.CustomerId.ToString();
            HttpContext.Current.Session["CustomerFullName"] = m_Customers.CustomerFullName;
            HttpContext.Current.Session["CustomerMobile"] = m_Customers.CustomerMobile;
            HttpContext.Current.Session["CustomerMail"] = m_Customers.CustomerMail;
            HttpContext.Current.Session["CustomerAvatar"] = m_Customers.Avatar;
            // set in cookie
            string AuthCode = ConstantHelpers.MEMBERDEFAULTPASS + m_Customers.CustomerId.ToString() + HttpContext.Current.Session.SessionID;

            AuthCode = sms.utils.md5.MD5Hash(AuthCode);
            AuthCode = AuthCode + m_Customers.CustomerId.ToString();
            HttpCookie AuthCookie = new HttpCookie("AuthCode");
            AuthCookie.Value = AuthCode;
            DateTime dtExpiry = DateTime.MinValue;
            AuthCookie.Expires = dtExpiry;
            HttpContext.Current.Response.Cookies.Remove("AuthCode");
            HttpContext.Current.Response.Cookies.Add(AuthCookie);
        }
        public static void SetCurentFullName(string CustomerFullName)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["CustomerFullName"] = CustomerFullName;
        }
        public static void SetCurentExpiredTime(string CustomerExpiredTime)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["CustomerExpiredTime"] = CustomerExpiredTime;
        }
        public static void ClearCurent()
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["CustomerId"] = null;
            HttpContext.Current.Session["CustomerFullName"] = null;
            HttpContext.Current.Session["CustomerExpiredTime"] = null;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["AuthCode"] != null)// 
            {
                // set in cookie
                HttpCookie AuthCookie = new HttpCookie("AuthCode");
                AuthCookie.Value = "";
                DateTime dtExpiry = DateTime.Now.AddDays(-1);
                AuthCookie.Expires = dtExpiry;
                //HttpContext.Current.Response.Cookies.Remove("AuthCode");
                HttpContext.Current.Response.Cookies.Add(AuthCookie);
            }
        }
        public static void ClearCurent(HttpContext context)
        {
            if (context.Session != null)
            {
                context.Session["CustomerId"] = null;
                context.Session["CustomerFullName"] = null;
                context.Session["CustomerExpiredTime"] = null;
            }
            if (context.Request.Cookies != null && context.Request.Cookies["AuthCode"] != null)// 
            {
                // set in cookie
                HttpCookie AuthCookie = new HttpCookie("AuthCode");
                AuthCookie.Value = "";
                DateTime dtExpiry = DateTime.Now.AddDays(-1);
                AuthCookie.Expires = dtExpiry;
                //context.Response.Cookies.Remove("AuthCode");
                context.Response.Cookies.Add(AuthCookie);
            }
        }
    }
}
