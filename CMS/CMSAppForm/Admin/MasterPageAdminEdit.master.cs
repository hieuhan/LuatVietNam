using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using System.Threading;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_MasterPageAdminEdit : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        int ActUserId = SessionHelpers.GetUserId();
        if (ActUserId == 0)
        {
            Response.Redirect(CmsConstants.PRJ_ROOT + "pages/security/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl), true);
        }
        else
        {
            Users m_Users = new Users();
            if (!m_Users.HasPriv(ActUserId, Request.Url.AbsolutePath))
            {
                Response.Redirect(CmsConstants.PRJ_ROOT + "errMsg.aspx", false);
            }
        }
        //if (!IsPostBack)
        //{
        //    try
        //    {


        //    }
        //    catch (Exception ex)
        //    {
        //        JSAlertHelpers.Alert(ex.Message.ToString(), this.Page);
        //        sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        //    }
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //ICSoft.HelperLib.JSAlertHelpers.ShowMessage("15");
    }
}
