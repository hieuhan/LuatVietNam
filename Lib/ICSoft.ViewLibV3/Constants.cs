using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.ViewLibV3
{
    public class Constants        
    {
        public static string CONNECTION_STRING = (ConfigurationManager.AppSettings["CONNECTION_STRING"] == null) ? "" : ConfigurationManager.AppSettings["CONNECTION_STRING"];
        public static string CMS_CONSTR = (ConfigurationManager.AppSettings["CMS_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["CMS_CONSTR"];
        public static string DOC_CONSTR = (ConfigurationManager.AppSettings["DOC_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["DOC_CONSTR"];
        public static string CUSTOMER_CONSTR = (ConfigurationManager.AppSettings["CUSTOMER_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["CUSTOMER_CONSTR"];
        public static string ROOT_PATH = (ConfigurationManager.AppSettings["ROOT_PATH"] == null) ? "" : ConfigurationManager.AppSettings["ROOT_PATH"];
        public static string NO_IMAGE_URL = (ConfigurationManager.AppSettings["NO_IMAGE_URL"] == null) ? "" : ConfigurationManager.AppSettings["NO_IMAGE_URL"];
        public static string WEBSITE_IMAGEDOMAIN = (ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"];
        public static string LOG_DIR = (ConfigurationManager.AppSettings["LOG_DIR"] == null) ? "" : ConfigurationManager.AppSettings["LOG_DIR"];
    }
}
