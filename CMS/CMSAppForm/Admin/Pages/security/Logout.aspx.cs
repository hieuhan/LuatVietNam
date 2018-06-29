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
namespace ICSoft.AppForm.Admin
{
    public partial class PageLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            string redirect = CmsConstants.PRJ_ROOT + "pages/security/Login.aspx";
            Response.Redirect(redirect, true);
        }
    }
}
