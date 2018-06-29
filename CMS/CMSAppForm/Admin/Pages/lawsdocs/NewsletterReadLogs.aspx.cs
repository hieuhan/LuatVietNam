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

public partial class Admin_NewsletterReadLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterReadLogId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("NewsletterReadLogs"), "");
            Newsletters m_Newsletters = new Newsletters();
            int RowCount = 0;
            DropDownListHelpers.DDL_Bind(ddlMessage, m_Newsletters.GetPage(ActUserId, 100, 0, "", short.Parse(ddlSite.SelectedValue), 0, "", "", 0, "", "", ref RowCount), "...");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            NewsletterReadLogs m_NewsletterReadLogs = new NewsletterReadLogs();
            string m_OrderBy = ddlOrderBy.SelectedValue;         
            string m_Email = txtEmail.Text;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            int m_NewsletterId = short.Parse(ddlMessage.SelectedValue);
            short m_SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_NewsletterReadLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_SiteId, m_NewsletterId, m_Email, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
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
            NewsletterReadLogId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());            
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        Newsletters m_Newsletters = new Newsletters();
        int RowCount = 0;
        DropDownListHelpers.DDL_Bind(ddlMessage, m_Newsletters.GetPage(ActUserId, 100, 0, "", short.Parse(ddlSite.SelectedValue), 0, "", "", 0, "", "", ref RowCount), "...");
        BindData();
    }
    protected void ddlMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}