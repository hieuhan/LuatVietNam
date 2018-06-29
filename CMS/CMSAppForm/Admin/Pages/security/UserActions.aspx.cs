using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_UserActions : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short UserId = 0;
    protected List<UserActions> l_UserAction = new List<UserActions>();
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
            lblUserName.Text = Users.Static_GetUserNameFullName(UserId);
            UserActions m_UserActions = new UserActions();
            Actions m_Actions = new Actions();
            l_UserAction = m_UserActions.GetByUser(UserId).Cast<UserActions>().ToList<UserActions>();
            m_grid.DataSource = m_Actions.GetAllHierachy();
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
        if (l_UserAction != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
                Label lblActionName = (Label)e.Row.FindControl("lblActionName");
                Actions m_DataItem = (Actions)e.Row.DataItem;
                if (lblActionName != null)
                {
                    if (m_DataItem.ParentActionId == 0) lblActionName.Font.Bold = true;
                }
                if (chkAction != null)
                {
                    UserActions m_UserActions = l_UserAction.Find(i => i.ActionId == m_DataItem.ActionId);
                    if (m_UserActions != null)
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
            UserActions m_UserActions = new UserActions();
            m_UserActions.UserId = UserId;
            m_UserActions.DeleteQuickBy();
            foreach (GridViewRow row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_UserActions.ActionId = short.Parse(chkAction.ToolTip);
                        m_UserActions.Insert(ActUserId, ref SysMessageId);
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