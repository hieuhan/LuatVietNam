using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;
public partial class Admin_Users : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<UserStatus> l_UserStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            UserStatus m_UserStatus = new UserStatus();
            Users m_Users = new Users();
            m_grid.DataSource = txtSearch.Text != "" ? m_Users.GetListOrderByUserName().FindAll(i=>i.Username.Contains(txtSearch.Text)) : m_Users.GetListOrderByUserName();
            l_UserStatus = m_UserStatus.GetAll().Cast<UserStatus>().ToList<UserStatus>();
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
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
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            UserStatus m_UserStatus = l_UserStatus.Find(i => i.UserStatusId == ((Users)e.Row.DataItem).UserStatusId);
            if (m_UserStatus != null)
            {
                lblStatus.Text = LanguageHelpers.IsCultureVietnamese() ? m_UserStatus.UserStatusDesc : m_UserStatus.UserStatusName;
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Users m_Users = new Users();
            m_Users.UserId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Users.Delete(ActUserId, ref SysMessageId);
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
}