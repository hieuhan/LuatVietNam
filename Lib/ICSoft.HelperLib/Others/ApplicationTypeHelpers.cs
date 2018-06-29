using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;

namespace ICSoft.HelperLib
{
	public class ApplicationTypeHelpers
	{
        //---------------------------------------------------------------------------------------
        public static string GetCurentName()
        {
            string ApplicationType = ConstantHelpers.DefaultApplicationType;
            try
            {
                HttpRequest Request = HttpContext.Current.Request;
                if (string.IsNullOrEmpty(Request["AppType"]) == false)// get first in request param
                    return Request["AppType"];
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["AppType"] != null)// get second in session
                    return HttpContext.Current.Session["AppType"].ToString();
                if (Request.Cookies != null && Request.Cookies["AppType"] != null)// get last in cookies
                    return string.IsNullOrEmpty(Request.Cookies["AppType"].Value) ? ApplicationType : Request.Cookies["AppType"].Value;
                
                if (UserAgentHelper.IsMobileDevice(Request.UserAgent))
                    ApplicationType = ConstantHelpers.ApplicationTypeMobile;
                
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return ApplicationType.ToLower();
        }
        public static byte GetCurentId()
        {
            return GetCurent().ApplicationTypeId;
        }
        public static ApplicationTypes GetCurent()
        {
            ApplicationTypes RetVal;
            try
            {
                string ApplicationTypeName = GetCurentName().ToLower();
                List<ApplicationTypes> l_ApplicationTypes = CacheHelpers<ApplicationTypes>.GetList();
                if (l_ApplicationTypes == null)
                {
                    l_ApplicationTypes = ApplicationTypes.Static_GetList();
                    CacheHelpers<ApplicationTypes>.InsertList(l_ApplicationTypes);
                }
                RetVal = l_ApplicationTypes.Find(x => x.ApplicationTypeName.ToLower() == ApplicationTypeName);
                if (RetVal == null)
                    RetVal = new ApplicationTypes();
            }
            catch (Exception ex)
            {
                RetVal = new ApplicationTypes();
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        public static ApplicationTypes GetCurent(string ApplicationTypeName)
        {
            ApplicationTypes RetVal;
            try
            {                
                List<ApplicationTypes> l_ApplicationTypes = CacheHelpers<ApplicationTypes>.GetList();
                if (l_ApplicationTypes == null)
                {
                    l_ApplicationTypes = ApplicationTypes.Static_GetList();
                    CacheHelpers<ApplicationTypes>.InsertList(l_ApplicationTypes);
                }
                RetVal = l_ApplicationTypes.Find(x => x.ApplicationTypeName.ToLower() == ApplicationTypeName.ToLower());
                if (RetVal == null)
                    RetVal = new ApplicationTypes();
            }
            catch (Exception ex)
            {
                RetVal = new ApplicationTypes();
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        public static void SetCurentName(string ApplicationType)
        {
            ApplicationType = ApplicationType.ToLower();
            if (string.IsNullOrEmpty(ApplicationType) == false)
            {
                HttpResponse Response = HttpContext.Current.Response;
                if (HttpContext.Current.Session != null)// set first in session
                    HttpContext.Current.Session["AppType"] = ApplicationType;
                // set in cookie
                HttpCookie AppTypeCookie = new HttpCookie("AppType");
                AppTypeCookie.Value = ApplicationType;
                DateTime dtExpiry = DateTime.Now.AddDays(15);
                AppTypeCookie.Expires = dtExpiry;
                Response.Cookies.Remove("AppType");
                Response.Cookies.Add(AppTypeCookie);
                

            }
        }
        public static byte Web
        {
            get { return 1; }
            set { }
        }
        public static byte Wap
        {
            get { return 2; }
            set { }
        }
        public static byte App
        {
            get { return 3; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
