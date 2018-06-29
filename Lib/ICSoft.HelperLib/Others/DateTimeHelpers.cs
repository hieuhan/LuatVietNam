using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using ICSoft.CMSLib;

namespace ICSoft.HelperLib
{
    public class DateTimeHelpers
    {
        /// <summary>
        /// get date time by curent culture
        /// </summary>        
        /// <param name="m_DateTime">
        /// Date Time Object. 
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetDateAndTime(object m_DateTime)
        {
            string RetVal = "";
            try
            {

                //RetVal = DateTime.Parse(m_DateTime.ToString()).ToString("g", System.Threading.Thread.CurrentThread.CurrentCulture);
                RetVal = DateTime.Parse(m_DateTime.ToString())==DateTime.MinValue ? "" : DateTime.Parse(m_DateTime.ToString()).ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {                
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        public static string GetDateAndTimeOnly(object m_DateTime)
        {
            string RetVal = "";
            try
            {
                
                RetVal = DateTime.Parse(m_DateTime.ToString()) == DateTime.MinValue ? "" : DateTime.Parse(m_DateTime.ToString()).ToString("d", System.Threading.Thread.CurrentThread.CurrentCulture);
                
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        public static string ConvertToValidDateTime(string m_DateTime)
        {
            string RetVal = "";
            try
            {
                DateTime DateInput = DateTime.MinValue;
                DateTime.TryParse(m_DateTime, System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None, out DateInput);                
                if (DateInput > DateTime.MinValue)
                    RetVal = DateInput.ToString("dd/MM/yyyy");

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        public static string GetDateAndTimeHH24(object m_DateTime)
        {
            string RetVal = "";
            try
            {

                RetVal = DateTime.Parse(m_DateTime.ToString()) == DateTime.MinValue ? "" : DateTime.Parse(m_DateTime.ToString()).ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }

        public static string GetDateHH24(object m_DateTime)
        {
            string RetVal = "";
            try
            {

                RetVal = DateTime.Parse(m_DateTime.ToString()) == DateTime.MinValue ? "" : DateTime.Parse(m_DateTime.ToString()).ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
    }
}
