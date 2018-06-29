using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
namespace ICSoft.HelperLib
{
    public class SessionHelpers
    {
        public static int GetUserId()
        {
            int RetVal = 0;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["userId"] == null) ? 0 : Int32.Parse(HttpContext.Current.Session["userId"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RetVal;
        }

        public static string GetUserName()
        {
            string RetVal = "";
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["user"] == null) ? "" : HttpContext.Current.Session["user"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RetVal;
        }

        public static string GetFullName()
        {
            string RetVal = "";
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["fullname"] == null) ? "" : HttpContext.Current.Session["fullname"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RetVal;
        }

        public static string GetCulture()
        {
            string RetVal = Thread.CurrentThread.CurrentCulture.Name;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["CultureInfo"] == null) ? Thread.CurrentThread.CurrentCulture.Name : HttpContext.Current.Session["CultureInfo"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RetVal;
        }
    }
}
