using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_OrganTypes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte OrganTypeId = 0;
    //protected byte LanguageId = 0;
    //private List<ICSoft.CMSLib.Languages> l_Language = new List<ICSoft.CMSLib.Languages>();
   // protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("OrganTypes"), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            System.Int32 EditId;
            OrganTypes m_OrganTypes = new OrganTypes();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_OrganTypeName = txtSearch.Text.Trim();
            OrganTypeId = 0;
            m_grid.DataSource = m_OrganTypes.OrganTypes_GetList(ActUserId, m_OrderBy, OrganTypeId, m_OrganTypeName);
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
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
                OrganTypeId = byte.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            
                OrganTypes m_OrganTypes = new OrganTypes();               
                m_OrganTypes.OrganTypeId = byte.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                Label lblTermName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblGridColumnName");
                SysMessageTypeId = m_OrganTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1)//error
                {
                    deleteMessage = string.Format("Lỗi khi xóa loại cơ quan ban hành : {0}", sms.common.SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 2)
                {
                    deleteMessage = string.Format("Xóa loại cơ quan ban hành <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
           
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
  
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            OrganTypes m_OrganTypes = new OrganTypes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_OrganTypes.OrganTypeId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_OrganTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                    
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " loại cơ quan ban hành.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> loại cơ quan ban hành chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
       BindData();
    }
    
}