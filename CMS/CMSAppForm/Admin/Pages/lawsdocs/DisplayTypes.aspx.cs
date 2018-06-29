using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using ICSoft.LawDocsLib;
using sms.admin.security;

public partial class Admin_DisplayTypes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte DataTypeId = 0;
    protected short DisplayTypeId = 0;
    protected List<Users> l_Users;
    protected List<DataTypes> l_DataTypes = new List<DataTypes>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataTypes, DataTypes.Static_GetListOrderBy("DataTypeDesc"), "");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DisplayTypes"), "");            
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_DataTypes = DataTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            DataTypeId = byte.Parse(ddlDataTypes.SelectedValue);

            DisplayTypes m_DisplayTypes = new DisplayTypes();
            byte m_DataTypeId = DataTypeId;
            m_grid.DataSource = m_DisplayTypes.GetListByDataTypeId(m_DataTypeId);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            DisplayTypes m_DataItem = (DisplayTypes)e.Row.DataItem;
            DisplayTypeId = m_DataItem.DisplayTypeId;
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
        string deleteMessage = string.Empty;
        try
        {
            DisplayTypes m_DisplayTypes = new DisplayTypes();
            m_DisplayTypes.DisplayTypeId = byte.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            Label lblTermName = (Label)m_grid.Rows[e.RowIndex].FindControl("lbDisplayTypeName");
            SysMessageTypeId = m_DisplayTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa loại danh sách hiển thị : {0}", sms.common.SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa loại danh sách hiển thị <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
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
            DisplayTypes m_DisplayTypes = new DisplayTypes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DisplayTypes.DisplayTypeId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_DisplayTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " loại danh sách hiển thị.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> loại danh sách hiển thị chưa được xóa.", countDeleteError);
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
    protected void ddlDataTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

