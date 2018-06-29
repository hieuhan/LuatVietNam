using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_CategoryDisplayTypes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short CategoryId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlDisplayType, DisplayTypes.Static_GetListByDataTypeId(9) , ""); //Vi tri hien thi chuyen muc
            BindData();
        }
    }

    private void BindData()
    {
        try
        {

            Categories m_Categories = new Categories();
            m_Categories.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Categories.DataTypeId = short.Parse(ddlDataType.SelectedValue); ;
            m_grid.DataSource = m_Categories.GetListByDisplayType(short.Parse(ddlDisplayType.SelectedValue));
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
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
            m_CategoryDisplays.CategoryDisplayId = 0;
            m_CategoryDisplays.DisplayTypeId = short.Parse(ddlDisplayType.SelectedValue);
            m_CategoryDisplays.CategoryId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_CategoryDisplays.CrUserId = ActUserId;
            m_CategoryDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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

    protected void btnSavePosition_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
            m_CategoryDisplays.CategoryDisplayId = 0;
            m_CategoryDisplays.DisplayTypeId = short.Parse(ddlDisplayType.SelectedValue);
            m_CategoryDisplays.CrUserId = ActUserId;
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                TextBox txtDisplayOrder = (TextBox)m_Row.FindControl("txtDisplayOrder");
                if (txtDisplayOrder != null)
                {
                    m_CategoryDisplays.CategoryId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                    m_CategoryDisplays.DisplayOrder = int.Parse(txtDisplayOrder.Text);
                    m_CategoryDisplays.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlDisplayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}