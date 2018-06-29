using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
/// <summary>
/// Summary description for JSAlert
/// </summary>
/// 
namespace ICSoft.HelperLib
{
    public class JSAlertHelpers
    {
        public JSAlertHelpers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public static string ExceptionMessage = (ConfigurationManager.AppSettings["ExceptionMessage"] == null) ? "Có lỗi trong quá trình xử lý, vui lòng liên hệ quản trị viên để biết thêm chi tiết." : ConfigurationManager.AppSettings["ExceptionMessage"];
        
        public static void Alert(string Msg, Page m_Page)
        {
            Msg = Msg.Replace("'", "");
            string script = @"<script language='JavaScript'>" +
                    "$.msgBox({title:'Alert', content: '" + Msg + "' });" +
                    "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void AlertException(string Msg, Page m_Page)
        {
            
            string script = @"<script language='JavaScript'>" +
                    "$.msgBox({title:'Alert', content: '" + ExceptionMessage + "' });" +
                    "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void AlertOtherPage(string Msg)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["AlertOtherPage"] = Msg;
            }
        }
        public static void ShowMessage(string Duration,string msgType= "success")
        {
            if (HttpContext.Current.Session != null)
            {
                if(HttpContext.Current.Session["AlertOtherPage"] != null)
                {
                    string AlertMsg = HttpContext.Current.Session["AlertOtherPage"].ToString();
                    HttpContext.Current.Session["AlertOtherPage"] = null;
                    AlertMsg = AlertMsg.Replace("'", "");
                    string script = @"<script language='JavaScript'>" +
                            "$.msgBox({title:'Alert', content: '" + AlertMsg + "' });" +
                            "</script>";
                    Page m_Page = HttpContext.Current.Handler as Page;
                    ClientScriptManager csm = m_Page.ClientScript;
                    csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
                }
                if (HttpContext.Current.Session["NotifyOtherPage"] != null)
                {
                    string NotifyMsg = HttpContext.Current.Session["NotifyOtherPage"].ToString();
                    HttpContext.Current.Session["NotifyOtherPage"] = null;
                    NotifyMsg = NotifyMsg.Replace("'", "");
                    
                    Page m_Page = HttpContext.Current.Handler as Page;
                    string script = @"<script language='JavaScript'>" +
                    "showNotification({duration:" + Duration + ", type: '" + msgType + "', message: '" + NotifyMsg + "' });" +
                    "</script>";
                    ClientScriptManager csm = m_Page.ClientScript;
                    csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
                }
            }
        }
        public static void AlertLanguage(string LanguageName, string Msg, Page m_Page)
        {
            Msg = Msg.Replace("'", "");
            string jsMessage = "'" + Themes.GetItemAdmin("FORM", "ALERT", LanguageName, "Alert") + "',";
            jsMessage += "'" + Msg + "','" + Themes.GetItemAdmin("FORM", "CLOSE", LanguageName, "Close") + "'";
            string script = @"<script language='JavaScript'>" +
                    "MsgAlert(" + jsMessage + ");" +
                    "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void Alert(string MsgTitle, string Msg, Page m_Page)
        {
            Msg = Msg.Replace("'", "");
            string script = @"<script language='JavaScript'>" +
                    "$.msgBox({title:'" + MsgTitle + "', content: '" + Msg + "' });" +
                    "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void AlertParent(string MsgId, string MsgTitle, string Msg, Page m_Page)
        {
            Msg = Msg.Replace("'", "");
            string script = "<script language='JavaScript'>";
            //script += "window.parent.jQuery('#" + MsgId + "').dialog('close');";
            script += "window.parent.jQuery.msgBox({title:'" + MsgTitle + "', content: '" + Msg + "' });";
            script += "window.parent.jQuery('#" + MsgId + "').dialog('close');</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void Close(Page m_Page)
        {
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "close", script);
        }
        public static void Close(string MsgId, Page m_Page)
        {
            string script = @"<script language='JavaScript'>
            window.parent.jQuery('#" + MsgId + "').dialog('close');</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "close", script);
        }
        public static void ShowNotify(string Duration, string msgType, string Msg, Page m_Page)
        {
            Msg = Msg.Replace("'", "");
            if (string.IsNullOrEmpty(msgType))
                msgType = "success";
            string script = @"<script language='JavaScript'>" +
                    "showNotification({duration:" + Duration + ", type: '" + msgType + "', message: '" + Msg + "' });" +
                    "</script>";
            ClientScriptManager csm = m_Page.ClientScript;
            csm.RegisterClientScriptBlock(m_Page.GetType(), "alert", script);
        }
        public static void ShowNotifyOtherPage(string Duration, string msgType, string Msg, Page m_Page)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["NotifyOtherPage"] = Msg;
            }
        }
    }
}