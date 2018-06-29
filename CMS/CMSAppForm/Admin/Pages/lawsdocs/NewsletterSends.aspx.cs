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

public partial class Admin_NewsletterSends : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short MessageTemplateId = 0;
    protected List<SendMethods> l_SendMethods = new List<SendMethods>();
    protected List<SendStatus> l_SendStatus = new List<SendStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLSendStatus_BindByCulture(ddlSendStatus, SendStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("NewsletterSends"), "");
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
            l_SendStatus = SendStatus.Static_GetList();
            
            NewsletterSends m_NewsletterSends = new NewsletterSends();
            int RowCount = 0;
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_SendStatusId = byte.Parse(ddlSendStatus.SelectedValue);
            short m_SiteId = short.Parse(ddlSite.SelectedValue);
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            string m_Keyword = txtSearchKeyword.Text;
            short m_NewsLetterId = short.Parse(ddlMessage.SelectedValue);
            m_grid.DataSource = m_NewsletterSends.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_Keyword, m_SiteId, m_NewsLetterId, m_SendStatusId, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text =m_grid.Rows.Count.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
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

    protected void ddlSendStatus_SelectedIndexChanged(object sender, EventArgs e)
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