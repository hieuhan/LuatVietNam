using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using System.Collections;
public partial class Admin_Actions : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<ActionStatus> l_ActionStatus = new List<ActionStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            Actions m_Actions = new Actions();
            List<Actions> l_Actions = m_Actions.GetAllHierachy2("--").Cast<Actions>().ToList<Actions>();
            DropDownListHelpers.DDL_Bind(ddlParentAction, l_Actions, "...");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Actions m_Actions = new Actions();
            l_ActionStatus = ActionStatus.Static_GetList();
            
            if (ddlParentAction.SelectedValue != "0")
            {
                List<Actions> l_Actions = new List<Actions>();
                foreach (Actions m_ActionsTemp in m_Actions.GetAllHierachy2("--"))
                {
                    if (m_ActionsTemp.ParentActionId.ToString() == ddlParentAction.SelectedValue)
                        l_Actions.Add(m_ActionsTemp);
                }
                m_grid.DataSource = l_Actions;// m_Actions.GetListbyParentId(int.Parse(ddlParentAction.SelectedValue));
            }
            else
            {
                IList data = m_Actions.GetAllHierachy2("--");
                m_grid.DataSource = data;
            }
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
            Label lblActionStatusDesc = (Label)e.Row.FindControl("lblActionStatusDesc");
            Actions m_DataItem = (Actions)e.Row.DataItem;
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            if (lblActionStatusDesc !=null)
            {
                ActionStatus m_ActionStatus = l_ActionStatus.Find(i => i.ActionStatusId == m_DataItem.ActionStatusId);
                if (m_ActionStatus != null)
                {
                    lblActionStatusDesc.Text = LanguageHelpers.IsCultureVietnamese() ? m_ActionStatus.ActionStatusDesc : m_ActionStatus.ActionStatusName;
                }
            }
            if (m_DataItem.ParentActionId == 0)
            {
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            
            Actions m_Actions = new Actions();
            m_Actions.ActionId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Actions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void ddlParentAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}