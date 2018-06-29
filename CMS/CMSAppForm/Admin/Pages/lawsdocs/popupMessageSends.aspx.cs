using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_Pages_lawsdocs_popupMessageSends : System.Web.UI.Page
{
    protected int ActUserId = 0, CampaignId = 0;
    protected short MessageTemplateId = 0;
    protected List<SendMethods> l_SendMethods = new List<SendMethods>();
    protected List<SendStatus> l_SendStatus = new List<SendStatus>();
    protected List<MessageTemplates> l_MessageTemplates = new List<MessageTemplates>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CampaignId"] != null && Request.Params["CampaignId"].ToString() != "")
        {
            CampaignId = int.Parse(Request.Params["CampaignId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLSendMethods_BindByCulture(ddlSendMethods, SendMethods.Static_GetList(), "...");
            DropDownListHelpers.DDLSendStatus_BindByCulture(ddlSendStatus, SendStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("MessageSends"), "");
            MessageTemplates m_MessageTemplates = new MessageTemplates();
            m_MessageTemplates.SiteId = short.Parse(ddlSite.SelectedValue);
            DropDownListHelpers.DDL_Bind(ddlMessage, m_MessageTemplates.MessageTemplates_GetList(ActUserId, "", "", "", 0), "...");
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
            l_SendMethods = SendMethods.Static_GetList();
            l_SendStatus = SendStatus.Static_GetList();
            l_MessageTemplates = MessageTemplates.Static_GetList();
            MessageSends m_MessageSends = new MessageSends();
            int RowCount = 0;
            string m_OrderBy = ddlOrderBy.SelectedValue, tracking = ddlTracking.SelectedValue;
            byte m_SendMethodId = byte.Parse(ddlSendMethods.SelectedValue);
            byte m_SendStatusId = byte.Parse(ddlSendStatus.SelectedValue);
            byte hasOpenMail = 0, hasClickLink = 0;
            switch (tracking)
            {
                case "1":
                    hasOpenMail = 1;
                    break;
                case "2":
                    hasClickLink = 1;
                    break;
            }
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            string m_Keyword = "";
            short m_MessageTemplateId = short.Parse(ddlMessage.SelectedValue);
            string m_SendFrom = "";
            string m_SendTo = "";
            switch (ddlSearchBy.SelectedValue)
            {
                case "SendTo":
                    m_SendTo = txtSearchKeyword.Text.Trim();
                    break;
                case "SendFrom":
                    m_SendFrom = txtSearchKeyword.Text.Trim();
                    break;
                default:
                    //Title
                    m_Keyword = txtSearchKeyword.Text.Trim();
                    break;
            }
            m_MessageSends.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_MessageSends.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_MessageTemplateId, m_SendFrom, m_SendTo, m_Keyword, m_SendMethodId, m_SendStatusId, m_DateFrom, m_DateTo, ref RowCount, CampaignId, hasOpenMail, hasClickLink);
            m_grid.DataBind();
            lblTong.Text = string.Format("{0:#,##}", RowCount) != "" ? string.Format("{0:#,##}", RowCount) : "0";
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

    protected void ddlSendMethods_SelectedIndexChanged(object sender, EventArgs e)
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
        MessageTemplates m_MessageTemplates = new MessageTemplates();
        m_MessageTemplates.SiteId = short.Parse(ddlSite.SelectedValue);
        DropDownListHelpers.DDL_Bind(ddlMessage, m_MessageTemplates.MessageTemplates_GetList(ActUserId, "", "", "", 0), "...");
        BindData();
    }
    protected void ddlMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MessageSends messageSend = (MessageSends)e.Row.DataItem;
                Label lblMailTime = (Label)e.Row.FindControl("lblMailTime");
                lblMailTime.Text = messageSend.OpenMailTime != DateTime.MinValue ? string.Format("<p style='margin:0;font-weight:bold;'>Tgian mở:</p><p style='margin:0'>{0:dd/MM/yyyy HH:mm}</p>", messageSend.OpenMailTime) : "";
                lblMailTime.Text += messageSend.ClickLinkTime != DateTime.MinValue ? string.Format("<p style='margin:0;font-weight:bold;'>Tgian click:</p><p style='margin:0'>{0:dd/MM/yyyy HH:mm}</p>", messageSend.ClickLinkTime) : "";
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(),
                ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
    }
}