using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_RoleAction : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short RoleId = 0;
    protected List<Actions> l_RoleAction = new List<Actions>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["RoleId"] != null && Request.Params["RoleId"].ToString() != "")
        {
            RoleId = short.Parse(Request.Params["RoleId"].ToString());
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
            m_Roles = m_Roles.Get(RoleId);
            lblRoleName.Text = LanguageHelpers.IsCultureVietnamese() ? m_Roles.RoleDesc : m_Roles.RoleName;

            Actions m_Actions = new Actions();
            l_RoleAction = m_Actions.GetActionsByRole(RoleId).Cast<Actions>().ToList<Actions>();
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
        if (l_RoleAction != null)
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
                    Actions m_Actions = l_RoleAction.Find(i => i.ActionId == m_DataItem.ActionId);
                    if (m_Actions != null)
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
        try
        {
            RoleActions m_RoleActions = new RoleActions();
            m_RoleActions.RoleId = RoleId;
            m_RoleActions.DeleteQuickBy(ActUserId);
            foreach (GridViewRow row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_RoleActions.ActionId = short.Parse(chkAction.ToolTip);
                        m_RoleActions.InsertQuick(ActUserId);
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