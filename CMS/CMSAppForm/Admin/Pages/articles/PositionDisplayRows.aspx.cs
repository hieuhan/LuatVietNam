using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_PositionDisplayRows : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short CategoryId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            PositionDisplayRows m_PositionDisplayRows = new PositionDisplayRows();

            m_grid.DataSource = m_PositionDisplayRows.GetList(short.Parse(ddlSite.SelectedValue));
            m_grid.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
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
            PositionDisplayRows m_PositionDisplayRows = new PositionDisplayRows();
            m_PositionDisplayRows.SiteId = short.Parse(ddlSite.SelectedValue);
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                TextBox txtRowDisplay = (TextBox)m_Row.FindControl("txtRowDisplay");
                CheckBox chkUseSummary = (CheckBox)m_Row.FindControl("chkUseSummary");
                CheckBox chkUseArticleContent = (CheckBox)m_Row.FindControl("chkUseArticleContent");

                m_PositionDisplayRows.DisplayTypeId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                m_PositionDisplayRows.PositionDisplayRowId = 0;
                m_PositionDisplayRows.RowDisplay = int.Parse(txtRowDisplay.Text);
                m_PositionDisplayRows.UseArticleContent = chkUseArticleContent.Checked ? (byte)1 : (byte)0;
                m_PositionDisplayRows.UseSummary = chkUseSummary.Checked ? (byte)1 : (byte)0;
                m_PositionDisplayRows.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
}