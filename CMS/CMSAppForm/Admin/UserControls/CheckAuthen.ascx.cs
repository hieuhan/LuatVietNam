using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ICSoft.CMSLib;
namespace ICSoft.AppForm.Admin
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        
        private int ActUserId;
        private string CON_PRIMARY_CONSTR = "";
        bool IsInRole = false;
        protected string LanguageName = ICSoft.HelperLib.LanguageHelpers.GetCurentName();
        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                
                ActUserId = ICSoft.HelperLib.UserHelpers.GetCurentId(ref IsInRole);
                if (ActUserId > 0)
                {
                    if (IsInRole)
                    {
                        authenmessage.Visible = false;
                    }
                    else
                    {
                        ContentPlaceHolder m_contentBody = (ContentPlaceHolder)this.Page.Master.FindControl("m_contentBody");
                        //m_contentBody.Visible = false;
                        authenmessage.Visible = false;
                    }
                }
                else
                {
                    //authenmessage.Visible = true;
                    Response.Redirect(CmsConstants.PRJ_ROOT + "pages/security/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl), true);
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
       
    }
}