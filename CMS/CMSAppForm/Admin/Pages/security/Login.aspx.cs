using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;
using sms.utils;
namespace ICSoft.AppForm.Admin
{
    public partial class PageLogin : System.Web.UI.Page
    {
        private short DefaultActionId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    m_login.Focus();
                    if (Request.Cookies["myCookie"] != null)
                    {
                        HttpCookie icookie = Request.Cookies.Get("myCookie");
                        m_login.UserName = icookie.Values["uname"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-----------------------------------------------------------------------
        protected void m_login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                e.Authenticated = DoLogin(m_login.UserName.Trim(), m_login.Password);
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + m_login.UserName + " starting logged.");
                string urlRedirect = CmsConstants.PRJ_ROOT + "pages/security/changePassword.aspx"; // CmsConstants.PRJ_ROOT + "Default.aspx";
                //Response.Write(DefaultActionId);
                //Response.Write(e.Authenticated);
                //Response.End();
                //return;
                if (DefaultActionId > 0)
                {
                    Actions m_Actions = new Actions();
                    m_Actions = m_Actions.Get(DefaultActionId);
                    if (!string.IsNullOrEmpty(m_Actions.Url)) 
                    {
                        urlRedirect = CmsConstants.PRJ_ROOT + m_Actions.Url;
                    }
                }
                //Response.Write(urlRedirect);
                //Response.End();
                Response.Redirect(urlRedirect, false);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-----------------------------------------------------------------------
        private bool DoLogin(string username, string password)
        {
            bool retVal = false;
            //Session["userId"] = "1";
            //return true;

            short SysMessageId = 0;
            try
            {
                Users m_User = new Users();
                username = injectionString(username);
                password = injectionString(password);
                m_User = m_User.Get(username);
                if (m_User.UserId > 0)//Co ton tai username nay
                {
                    //Response.Write(md5.CalculateMD5Hash(password));
                    if (md5.MD5Hash(password).ToLower() == m_User.Password.ToLower())
                    {
                        if (m_User.UserStatusId == 1)
                        {
                            retVal = true;
                        }

                    }
                    else
                    {
                        sms.utils.LogFiles.LogInfo(md5.MD5Hash(password) + ":" + m_User.Password);
                    }
                }
                sms.admin.security.UserLogs m_UserLog = new sms.admin.security.UserLogs();
                m_UserLog.Username = m_User.Username;
                m_UserLog.Password = password;
                m_UserLog.IPAddress = Request.UserHostAddress;
                if (retVal)
                {
                    Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["DURATION_TIMEOUT"].ToString()); //10 * 3600; //Thoi gian timeout la 10 gio
                    Session["user"] = username;
                    Session["pass"] = password;
                    Session["fullname"] = m_User.Fullname;
                    Session["userId"] = m_User.UserId.ToString();
                    Session["defaultActionId"] = m_User.DefaultActionId.ToString();
                    DefaultActionId = m_User.DefaultActionId;
                    if (username.ToLower() == "admin")
                        Session["MediaRole"] = "Admin";
                    else
                    {
                        Session["MediaRole"] = "FullControl";
                    }

                    CheckBox rm = (CheckBox)m_login.FindControl("RememberMe");
                    if (rm.Checked)
                    {
                        HttpCookie myCookie = new HttpCookie("myCookie");
                        Response.Cookies.Remove("myCookie");
                        Response.Cookies.Add(myCookie);
                        myCookie.Values.Add("uname", this.m_login.UserName);
                        myCookie.Values.Add("pass", this.m_login.Password);
                        DateTime dtExpiry = DateTime.Now.AddDays(15);
                        Response.Cookies["myCookie"].Expires = dtExpiry;
                    }
                    m_UserLog.Status = m_User.UserStatusId;
                    retVal = true;
                }
                else
                {
                    m_UserLog.Status = 2;
                    retVal = false;
                }
                m_UserLog.InsertQuick();
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                JSAlertHelpers.Alert(ex.Message, this);
                return false;
            }
            return retVal;
        }
        //Xay dung ham loai mot so ky tu ma nguoi dung co the injection
        private string injectionString(string str)
        {
            try
            {
                string tmp;
                tmp = killChar(str).Replace("'", "''");
                return str;
            }
            catch
            {
                return "";
            }
        }
        //-----------------------------------------------------------------------
        private string killChar(string strInput)
        {
            try
            {
                string newChars;
                string[] badChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_" };
                newChars = strInput.Trim();
                for (int i = 0; i < badChars.Length; i++)
                {
                    newChars = newChars.Replace(badChars[i], "");
                }
                return newChars;
            }
            catch
            {
                return "";
            }
        }
    }
}
