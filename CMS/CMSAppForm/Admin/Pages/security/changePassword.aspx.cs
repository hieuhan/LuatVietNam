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
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.utils;
namespace ICSoft.AppForm.Admin
{
    public partial class PageChangePassword : System.Web.UI.Page
    {
        private int user_id;
        private int ActUserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ActUserId = SessionHelpers.GetUserId();
                if (ActUserId > 0)
                {
                    user_id = ActUserId;
                }
                else
                {
                    Response.Redirect(CmsConstants.PRJ_ROOT + "/pages/security/Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                lblMsg.Text = ex.Message;
            }
        }
       
        protected void changePass_ChangingPassword(object sender, EventArgs e)
        {
            string Message1 = "";
            lblMsg.Text = "";
            short SysMessageId = 0;
            try
            {
                if (user_id > 0)
                {
                    Users m_user = new Users();
                    m_user = m_user.Get(user_id);
                    if (m_user.Birthday <= DateTime.MinValue)
                        m_user.Birthday = DateTime.Now;
                    if (md5.MD5Hash(txtOldPass.Text).ToLower() != m_user.Password.ToLower())
                    {
                        Message1 = GetLocalResourceObject("WrongOldPass").ToString();
                    }
                    else
                    {
                        if (txtPasswordConfirm.Text != txtPassword.Text)
                        {
                            Message1 = GetLocalResourceObject("cmpPass.ErrorMessage").ToString();
                        }
                        else
                        {
                            m_user.Password = md5.MD5Hash(txtPassword.Text);
                            short retVal = m_user.Update(ActUserId, ref SysMessageId);
                            Message1 = GetLocalResourceObject("ChangePassSucess").ToString();
                        }
                    }
                }
                else
                {
                    Message1 = "Chưa có Id của người sử dụng";
                }
                lblMsg.Text = Message1;
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), "admin_changePassword changePass_ChangingPassword");
                lblMsg.Text = ex.Message;
            }
        }
    }
}
