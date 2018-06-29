using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_UserSites : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short UserId = 0;
    private List<UserSites> l_UserSites;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["UserId"] != null && Request.Params["UserId"].ToString() != "")
        {
            UserId = short.Parse(Request.Params["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Sites m_Sites = new Sites();
            lblUserName.Text = Users.Static_GetUserNameFullName(UserId);
            UserSites m_UserSites = new UserSites();
            l_UserSites = m_UserSites.GetList(UserId);
            m_grid.DataSource = m_Sites.GetList(ActUserId);
            m_grid.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (l_UserSites != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
                Sites m_DataItem = (Sites)e.Row.DataItem;
                if (chkAction != null)
                {
                    UserSites m_UserSites = l_UserSites.Find(i => i.SiteId == m_DataItem.SiteId);
                    if (m_UserSites != null)
                    {
                        chkAction.Checked = true;
                        e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        short SysMessageId = 0;
        try
        {
            UserSites m_UserSites = new UserSites();
            m_UserSites.UserId = UserId;
            m_UserSites.DeleteByUserId(1, ActUserId, ref SysMessageId);
            foreach (GridViewRow row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_UserSites.SiteId = short.Parse(chkAction.ToolTip);
                        m_UserSites.CrUserId = ActUserId;
                        m_UserSites.Insert(1, ActUserId, ref SysMessageId);
                    }
                }
            }
            BindData();
            lblMsg.Text = "Success!";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            lblMsg.Text = ex.Message;
        }
    }
}