using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.CMSLib
{
    public class CmsConstants        
    {
        public static byte PRIVATE = Byte.Parse((ConfigurationManager.AppSettings["PRIVATE"]==null)?"1":ConfigurationManager.AppSettings["PRIVATE"].ToString());
        public static byte PUBLIC = Byte.Parse((ConfigurationManager.AppSettings["PUBLIC"]==null)?"2":ConfigurationManager.AppSettings["PUBLIC"].ToString());
        public static byte PRIVATE_AND_PUBLIC = Byte.Parse((ConfigurationManager.AppSettings["PRIVATE_AND_PUBLIC"]==null)?"3":ConfigurationManager.AppSettings["PRIVATE_AND_PUBLIC"].ToString());
        public static byte PRIVATE_PUBLIC = (ConfigurationManager.AppSettings["PRIVATE_PUBLIC"] == null) ? PRIVATE : Byte.Parse(ConfigurationManager.AppSettings["PRIVATE_PUBLIC"].ToString());
        public static byte SUCCESS = Byte.Parse((ConfigurationManager.AppSettings["SUCCESS"] == null) ? "1" : ConfigurationManager.AppSettings["SUCCESS"].ToString());
        public static string ARTICLES_CONNECTION_STRING = (ConfigurationManager.AppSettings["ARTICLES_CONNECTION_STRING"] == null) ? "" : ConfigurationManager.AppSettings["ARTICLES_CONNECTION_STRING"];
        public static byte ROW_AMOUNT = Byte.Parse((ConfigurationManager.AppSettings["ROW_AMOUNT"] == null) ? "50" : ConfigurationManager.AppSettings["ROW_AMOUNT"].ToString());
        public static byte DISTRIBUTED_PROCESS = Byte.Parse((ConfigurationManager.AppSettings["DISTRIBUTED_PROCESS"] == null) ? "0" : ConfigurationManager.AppSettings["DISTRIBUTED_PROCESS"].ToString());
        public static string PRJ_TITLE = (ConfigurationManager.AppSettings["PRJ_TITLE"]==null)?"":ConfigurationManager.AppSettings["PRJ_TITLE"];
        public static string CONNECTION_STRING = (ConfigurationManager.AppSettings["CONNECTION_STRING"]==null)?"":ConfigurationManager.AppSettings["CONNECTION_STRING"];
        public static string CMS_CONSTR = (ConfigurationManager.AppSettings["CMS_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["CMS_CONSTR"];
        public static string EXT_CONSTR = (ConfigurationManager.AppSettings["EXT_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["EXT_CONSTR"];
        public static string UPLOAD_PATH = (ConfigurationManager.AppSettings["UPLOAD_PATH"] == null) ? "" : ConfigurationManager.AppSettings["UPLOAD_PATH"];
        public static string ROOT_PATH = (ConfigurationManager.AppSettings["ROOT_PATH"] == null) ? "" : ConfigurationManager.AppSettings["ROOT_PATH"];
        public static string PRJ_ROOT = ROOT_PATH + "admin/"; //admin root path
        public static string COOKIESID = (ConfigurationManager.AppSettings["COOKIESID"]==null)?"":ConfigurationManager.AppSettings["COOKIESID"];
        public static string DOMAIN = (ConfigurationManager.AppSettings["DOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["DOMAIN"];
        public static string WEBSITE_TITLE = (ConfigurationManager.AppSettings["WEBSITE_TITLE"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_TITLE"];
        public static string WEBSITE_DOMAIN = (ConfigurationManager.AppSettings["WEBSITE_DOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_DOMAIN"];
        public static string WEBSITE_IMAGEDOMAIN = (ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"];
        public static string WEBSITE_MEDIADOMAIN = (ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"];

        //Article config
        public static string MEDIA_PATH = (ConfigurationManager.AppSettings["MEDIA_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_PATH"];
        public static string MEDIA_ORIGINAL_PATH = (ConfigurationManager.AppSettings["MEDIA_ORIGINAL_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_ORIGINAL_PATH"];
        public static string MEDIA_THUMNAIL_PATH = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_THUMNAIL_PATH"];
        public static string MEDIA_ICON_PATH = (ConfigurationManager.AppSettings["MEDIA_ICON_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_ICON_PATH"];
        public static int MEDIA_WIDTH = (ConfigurationManager.AppSettings["MEDIA_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_WIDTH"].ToString());
        public static int MEDIA_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_HEIGHT"].ToString());
        public static int MEDIA_THUMNAIL_WIDTH = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"].ToString());
        public static int MEDIA_THUMNAIL_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_HEIGHT"].ToString());
        public static int MEDIA_ICON_WIDTH = (ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"].ToString());
        public static int MEDIA_ICON_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_ICON_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_HEIGHT"].ToString());
        public static string CATEGORIES_ICON_PATH = (ConfigurationManager.AppSettings["CATEGORIES_ICON_PATH"] == null) ? "" : ConfigurationManager.AppSettings["CATEGORIES_ICON_PATH"];
        public static short ARTICLES_ADMIN_ROWSAMOUNT = (ConfigurationManager.AppSettings["ARTICLES_ADMIN_ROWSAMOUNT"] == null) ? (short)0 : Int16.Parse(ConfigurationManager.AppSettings["ARTICLES_ADMIN_ROWSAMOUNT"].ToString());
        public static string FLV_PATH = (ConfigurationManager.AppSettings["FLV_PATH"] == null) ? "" : ConfigurationManager.AppSettings["FLV_PATH"];
        public static string NO_IMAGE_URL = (ConfigurationManager.AppSettings["NO_IMAGE_URL"] == null) ? "" : ConfigurationManager.AppSettings["NO_IMAGE_URL"];
        public static string CONTACT_PRICE = (ConfigurationManager.AppSettings["CONTACT_PRICE"] == null) ? "Liên hệ" : ConfigurationManager.AppSettings["CONTACT_PRICE"];
        public static string CURRENCY = (ConfigurationManager.AppSettings["CURRENCY"] == null) ? " VNĐ" : ConfigurationManager.AppSettings["CURRENCY"];
        public static string CURRENCY_VND = (ConfigurationManager.AppSettings["CURRENCY_VND"] == null) ? " VNĐ" : ConfigurationManager.AppSettings["CURRENCY_VND"];
        public static string CURRENCY_USD = (ConfigurationManager.AppSettings["CURRENCY_USD"] == null) ? " USD" : ConfigurationManager.AppSettings["CURRENCY_USD"];
        
        //Mail config
        public static string SMTP_HOST = (ConfigurationManager.AppSettings["SMTP_HOST"] == null) ? "" : ConfigurationManager.AppSettings["SMTP_HOST"];
        public static int SMTP_PORT = (ConfigurationManager.AppSettings["SMTP_PORT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["SMTP_PORT"].ToString());
        public static string MAIL_ADDR = (ConfigurationManager.AppSettings["MAIL_ADDR"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_ADDR"];
        public static string MAIL_PASS = (ConfigurationManager.AppSettings["MAIL_PASS"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_PASS"];

        public static string CULTURE_VN = "vi-VN";
        public static int COPYBYAPPTYPE = (ConfigurationManager.AppSettings["COPYBYAPPTYPE"] == null) ? 1 : Int32.Parse(ConfigurationManager.AppSettings["COPYBYAPPTYPE"].ToString());
        public static int COPYBYLANG = (ConfigurationManager.AppSettings["COPYBYLANG"] == null) ? 1 : Int32.Parse(ConfigurationManager.AppSettings["COPYBYLANG"].ToString());
    }
}
