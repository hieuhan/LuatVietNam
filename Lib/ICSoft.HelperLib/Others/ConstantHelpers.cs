using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public class ConstantHelpers        
    {
        public static byte LOG_GATEWAY = (ConfigurationManager.AppSettings["LOG_GATEWAY"] == null) ? (byte)0 : byte.Parse(ConfigurationManager.AppSettings["LOG_GATEWAY"]);
        public static string FILE_ALLOWED_UPLOAD = (ConfigurationManager.AppSettings["FILE_ALLOWED_UPLOAD"] == null) ? "jpg,png,gif,jpeg,flv" : ConfigurationManager.AppSettings["FILE_ALLOWED_UPLOAD"];

        public static string LanguageDir = (ConfigurationManager.AppSettings["LanguageDir"] == null) ? "Languages/" : ConfigurationManager.AppSettings["LanguageDir"];
        public static string LanguageAdminDir = (ConfigurationManager.AppSettings["LanguageAdminDir"] == null) ? "Admin/Languages/" : ConfigurationManager.AppSettings["LanguageAdminDir"];
        public static string MobileUserAgents = (ConfigurationManager.AppSettings["MobileUserAgents"] == null) ? "iphone,ipad,midp,windows ce,windows phone,android,blackberry,opera mini,mobile,palm,portable,webos,htc,armv,lg/u,elaine,nokia,playstation,symbian,sonyericsson,mmp,hd_mini" : ConfigurationManager.AppSettings["MobileUserAgents"];
        public static string DefaultLanguage = (ConfigurationManager.AppSettings["DefaultLanguage"] == null) ? "vietnamese" : ConfigurationManager.AppSettings["DefaultLanguage"];
        public static string DefaultApplicationType = (ConfigurationManager.AppSettings["DefaultApplicationType"] == null) ? "web" : ConfigurationManager.AppSettings["DefaultApplicationType"];
        public static string ApplicationTypeMobile = (ConfigurationManager.AppSettings["ApplicationTypeMobile"] == null) ? "wap" : ConfigurationManager.AppSettings["ApplicationTypeMobile"];
        public static string ApplicationTypeJSon = (ConfigurationManager.AppSettings["ApplicationTypeJSon"] == null) ? "App" : ConfigurationManager.AppSettings["ApplicationTypeJSon"];
        public static byte APPTYPEJSONID = (ConfigurationManager.AppSettings["APPTYPEJSONID"] == null) ? (byte)3 : byte.Parse(ConfigurationManager.AppSettings["APPTYPEJSONID"]);
        public static byte Replicated = (ConfigurationManager.AppSettings["Replicated"] == null) ? (byte)0 : byte.Parse(ConfigurationManager.AppSettings["Replicated"]);
        public static string MasterPageUrl = (ConfigurationManager.AppSettings["MasterPageUrl"] == null) ? "MasterPages/" : ConfigurationManager.AppSettings["MasterPageUrl"];
        
        public static byte ReviewStatusPending = 1;
        public static string WEBSITE_TITLE = (ConfigurationManager.AppSettings["WEBSITE_TITLE"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_TITLE"];
        public static string WEBSITE_DESC = (ConfigurationManager.AppSettings["WEBSITE_DESC"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_DESC"];
        public static string WEBSITE_KEYWORD = (ConfigurationManager.AppSettings["WEBSITE_KEYWORD"] == null) ? "" : ConfigurationManager.AppSettings["WEBSITE_KEYWORD"];

        public static string DEFAULT_CULTURE = (ConfigurationManager.AppSettings["DEFAULT_CULTURE"] == null) ? "vi-VN" : ConfigurationManager.AppSettings["DEFAULT_CULTURE"];
        public static string CULTURE_VN = "vi-VN";
        public static byte SYS_MESSAGE_TYPE_SUCCESS = 2;
        public static byte REVIEW_STATUS_PENDING = 1;
        public static byte REVIEW_STATUS_REVIEW = 2;
        public static byte REVIEW_STATUS_UNREVIEW = 3;
        public static byte REVIEW_STATUS_REJECT = 4;
        // rewrite 
        public static string RewriteExt = (ConfigurationManager.AppSettings["RewriteExt"] == null) ? ".html" : ConfigurationManager.AppSettings["RewriteExt"];
        public static string RewriteSaparator = (ConfigurationManager.AppSettings["RewriteSaparator"] == null) ? "-" : ConfigurationManager.AppSettings["RewriteSaparator"];
        public static string RewriteSpace = (ConfigurationManager.AppSettings["RewriteSpace"] == null) ? "-" : ConfigurationManager.AppSettings["RewriteSpace"];
        public static string REWRITE_PAGETYPE_PREFIX = (ConfigurationManager.AppSettings["REWRITE_PAGETYPE_PREFIX"] == null) ? "c" : ConfigurationManager.AppSettings["REWRITE_PAGETYPE_PREFIX"];
        public static string REWRITE_PAGE_PREFIX = (ConfigurationManager.AppSettings["REWRITE_PAGE_PREFIX"] == null) ? "" : ConfigurationManager.AppSettings["REWRITE_PAGE_PREFIX"];
        public static string REWRITE_ITEM_PREFIX = (ConfigurationManager.AppSettings["REWRITE_ITEM_PREFIX"] == null) ? "i" : ConfigurationManager.AppSettings["REWRITE_ITEM_PREFIX"];
        public static string REWRITE_DOCITEM_PREFIX = (ConfigurationManager.AppSettings["REWRITE_DOCITEM_PREFIX"] == null) ? "dvt" : ConfigurationManager.AppSettings["REWRITE_DOCITEM_PREFIX"];
        public static string REWRITE_PAGING_PREFIX = (ConfigurationManager.AppSettings["REWRITE_PAGING_PREFIX"] == null) ? "p" : ConfigurationManager.AppSettings["REWRITE_PAGING_PREFIX"];
        public static byte MASTER_CHANGE_BY_LANG = (ConfigurationManager.AppSettings["MASTER_CHANGE_BY_LANG"] == null) ? (byte)0 : byte.Parse(ConfigurationManager.AppSettings["MASTER_CHANGE_BY_LANG"]);
        public static byte MASTER_CHANGE_BY_APPTYPE = (ConfigurationManager.AppSettings["MASTER_CHANGE_BY_APPTYPE"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["MASTER_CHANGE_BY_APPTYPE"]);
        //===============
        public static int LOGINLOCKTIME = (ConfigurationManager.AppSettings["LOGINLOCKTIME"] == null) ? 3 : int.Parse(ConfigurationManager.AppSettings["LOGINLOCKTIME"]);
        public static int LOGININVALID = (ConfigurationManager.AppSettings["LOGININVALID"] == null) ? 3 : int.Parse(ConfigurationManager.AppSettings["LOGININVALID"]);
        public static string MEMBERDEFAULTPASS = (ConfigurationManager.AppSettings["MEMBERDEFAULTPASS"] == null) ? "112233" : ConfigurationManager.AppSettings["MEMBERDEFAULTPASS"];
        //======================
        public static string WEBSITE_DOMAIN = (ConfigurationManager.AppSettings["WEBSITE_DOMAIN"] == null) ? "/" : ConfigurationManager.AppSettings["WEBSITE_DOMAIN"];
        public static string WEBSITE_IMAGEDOMAIN = (ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] == null) ? "/" : ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"];
        public static string WEBSITE_MEDIADOMAIN = (ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"] == null) ? "/" : ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"];
        public static string IMAGEPATH = (ConfigurationManager.AppSettings["IMAGEPATH"] == null) ? "/Uploaded/Images/" : ConfigurationManager.AppSettings["IMAGEPATH"];
        public static string IMAGEPATHTHUMB = (ConfigurationManager.AppSettings["IMAGEPATHTHUMB"] == null) ? "/Uploaded/_Thumb/" : ConfigurationManager.AppSettings["IMAGEPATHTHUMB"];
        public static string MEDIAPATH = (ConfigurationManager.AppSettings["MEDIAPATH"] == null) ? "/Uploaded/Medias/" : ConfigurationManager.AppSettings["MEDIAPATH"];
        public static int HOMESLIDEID = (ConfigurationManager.AppSettings["HOMESLIDEID"] == null) ? 4 : int.Parse(ConfigurationManager.AppSettings["HOMESLIDEID"]);

        public static string IsLOG = (ConfigurationManager.AppSettings["IsLOG"] == null) ? "0" : ConfigurationManager.AppSettings["IsLOG"];

        public static string uid_md5 = ConfigurationManager.AppSettings["uid_md5"];
        public static string pwd_md5 = ConfigurationManager.AppSettings["pwd_md5"];
        public static string LOG_FILE_PATH = (ConfigurationManager.AppSettings["LOG_FILE_PATH"] == null) ? AppDomain.CurrentDomain.BaseDirectory + "Logs\\" : ConfigurationManager.AppSettings["LOG_FILE_PATH"];

        public static int RULEHEADID = (ConfigurationManager.AppSettings["RULEHEADID"] == null) ? 6684 : int.Parse(ConfigurationManager.AppSettings["RULEHEADID"]);
        public static int RULECONTENTID = (ConfigurationManager.AppSettings["RULECONTENTID"] == null) ? 5611 : int.Parse(ConfigurationManager.AppSettings["RULECONTENTID"]);
        public static int WELCOMID = (ConfigurationManager.AppSettings["WELCOMID"] == null) ? 6685 : int.Parse(ConfigurationManager.AppSettings["WELCOMID"]);
        public static int CONSULTINGNOTEID = (ConfigurationManager.AppSettings["CONSULTINGNOTEID"] == null) ? 6686 : int.Parse(ConfigurationManager.AppSettings["CONSULTINGNOTEID"]);
        public static int NEWSEFFECTNOTEID = (ConfigurationManager.AppSettings["NEWSEFFECTNOTEID"] == null) ? 6687 : int.Parse(ConfigurationManager.AppSettings["NEWSEFFECTNOTEID"]);
        public static int SEARCHEFFECTNOTEID = (ConfigurationManager.AppSettings["SEARCHEFFECTNOTEID"] == null) ? 3612 : int.Parse(ConfigurationManager.AppSettings["SEARCHEFFECTNOTEID"]);
        public static int SEARCHTIMENOTEID = (ConfigurationManager.AppSettings["SEARCHTIMENOTEID"] == null) ? 4924 : int.Parse(ConfigurationManager.AppSettings["SEARCHTIMENOTEID"]);
        public static int SEARCHEFFECTLOGINID = (ConfigurationManager.AppSettings["SEARCHEFFECTLOGINID"] == null) ? 3612 : int.Parse(ConfigurationManager.AppSettings["SEARCHEFFECTLOGINID"]);
        public static int SEARCHTIMELOGINID = (ConfigurationManager.AppSettings["SEARCHTIMELOGINID"] == null) ? 4924 : int.Parse(ConfigurationManager.AppSettings["SEARCHTIMELOGINID"]);

        public static int YEARFREEVIEWDOC = (ConfigurationManager.AppSettings["YEARFREEVIEWDOC"] == null) ? 5 : int.Parse(ConfigurationManager.AppSettings["YEARFREEVIEWDOC"]);
        public static int ENTERCODEINVALIDTIME = (ConfigurationManager.AppSettings["ENTERCODEINVALIDTIME"] == null) ? 5 : int.Parse(ConfigurationManager.AppSettings["ENTERCODEINVALIDTIME"]);

        //=====Email =================
        public static string MAIL_SERVER_HOST = (ConfigurationManager.AppSettings["MAIL_SERVER_HOST"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_SERVER_HOST"];
        public static string MAIL_USER = (ConfigurationManager.AppSettings["MAIL_USER"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_USER"];
        public static string MAIL_PASS = (ConfigurationManager.AppSettings["MAIL_PASS"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_PASS"];
        public static string MAIL_DOMAIN = (ConfigurationManager.AppSettings["MAIL_DOMAIN"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_DOMAIN"];
        public static string MAIL_URLBASE = (ConfigurationManager.AppSettings["MAIL_URLBASE"] == null) ? "" : ConfigurationManager.AppSettings["MAIL_URLBASE"];
        public static int MAIL_PORT = (ConfigurationManager.AppSettings["MAIL_PORT"] == null) ? 25 : int.Parse(ConfigurationManager.AppSettings["MAIL_PORT"]);
        public static bool MAIL_SSL = (ConfigurationManager.AppSettings["MAIL_SSL"] == null) ? false : bool.Parse(ConfigurationManager.AppSettings["MAIL_SSL"]);
        
    }
}
