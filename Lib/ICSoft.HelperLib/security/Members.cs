using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ICSoft.CMSLib;
namespace ICSoft.HelperLib
{
    public class MemberHelpers
    {
        public static int GetCurentId()
        {
            int RetVal = 0;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["MemberId"] == null) ? 0 : Int32.Parse(HttpContext.Current.Session["MemberId"].ToString());
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
                return "";
            return (HttpContext.Current.Session["MemberFullName"] == null) ? "" : HttpContext.Current.Session["MemberFullName"].ToString();
        }
        public static string GetCurentLink()
        {
            if (HttpContext.Current.Session == null)
                return "";
            return (HttpContext.Current.Session["MemberLink"] == null) ? "" : HttpContext.Current.Session["MemberLink"].ToString();
        }
        public static void SetCurentId(int MemberId)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["MemberId"] = MemberId.ToString();
        }
        public static void SetCurentFullName(string MemberFullName)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["MemberFullName"] = MemberFullName;
        }
        public static void SetCurentLink(string MemberLink)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["MemberLink"] = MemberLink;
        }
        public static void ClearCurent()
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["MemberId"] = null;
            HttpContext.Current.Session["MemberFullName"] = null;
        }
    }
}
