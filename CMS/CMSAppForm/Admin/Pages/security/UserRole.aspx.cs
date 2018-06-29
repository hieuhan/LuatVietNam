using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_UserRole : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short UserId = 0;
    protected List<UserRoles> l_UserRoles = new List<UserRoles>();
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
            Roles m_Roles = new Roles();
            lblUserName.Text = Users.Static_GetUserNameFullName(UserId);
            UserRoles m_UserRoles = new UserRoles();
            l_UserRoles = m_UserRoles.GetListByUserId(UserId).Cast<UserRoles>().ToList<UserRoles>();
            m_grid.DataSource = m_Roles.GetAll();
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
        if (l_UserRoles != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
                Roles m_DataItem = (Roles)e.Row.DataItem;
                if (chkAction != null)
                {
                    UserRoles m_UserRoles = l_UserRoles.Find(i => i.RoleId == m_DataItem.RoleId);
                    if (m_UserRoles != null)
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
            UserRoles m_UserRoles = new UserRoles();
            m_UserRoles.UserId = UserId;
            m_UserRoles.DeleteQuickBy(ActUserId);
            foreach (GridViewRow row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_UserRoles.RoleId = short.Parse(chkAction.ToolTip);
                        m_UserRoles.InsertQuick(ActUserId);
                    }
                }
            }
            BindData();
            lblMsg.Text = GetLocalResourceObject("AssignSucess").ToString();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            lblMsg.Text = ex.Message;
        }
    }
}