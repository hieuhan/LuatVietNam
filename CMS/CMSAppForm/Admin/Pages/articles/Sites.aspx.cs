using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_Sites : System.Web.UI.Page
{
    protected int SiteId = 0;
    protected int ActUserId = 0;
    protected List<Users> l_User;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            if (Request.Params["GenUrlSiteId"] != null && Request.Params["GenUrlSiteId"].ToString() != "")
            {
                Sites m_Sites = new Sites();
                m_Sites.GenUrl(short.Parse(Request.Params["GenUrlSiteId"].ToString()), 1);
            }
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Users m_Users = new Users();
            l_User = m_Users.GetAll();
            Sites m_Sites = new Sites();
            m_Sites.SiteName = txtKey.Text.Trim();
            
            List<Sites> l_Sites = m_Sites.GetPage(ActUserId,500, 0,"SiteId Desc", "", "", ref RowCount);
            m_grid.DataSource = l_Sites;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";  
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
                Sites m_Sites = new Sites(); 
                m_Sites.SiteId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Sites.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 2)
                {
                    JSAlert.Alert(GetLocalResourceObject("DeleteSuccess").ToString(), this);
                }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}