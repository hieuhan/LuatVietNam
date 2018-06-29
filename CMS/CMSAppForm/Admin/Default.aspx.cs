using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;
using sms.admin.security;
public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string user_agent = Request.Headers["User-Agent"];
            string redirect = CmsConstants.PRJ_ROOT + "pages/security/Login.aspx";
            Actions m_Action = new Actions();
            int userId = SessionHelpers.GetUserId();
            if (userId > 0)
            {
                short defaultActionId = Int16.Parse((Session["defaultActionId"] == null) ? "0" : Session["defaultActionId"].ToString());
                m_Action = m_Action.Get(defaultActionId);
                if (m_Action.ActionId > 0)
                {
                    if (!string.IsNullOrEmpty(m_Action.Url)) redirect = CmsConstants.PRJ_ROOT + m_Action.Url;
                }
            }
            Response.Redirect(redirect, false);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
}