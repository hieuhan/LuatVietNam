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

public partial class Admin_NewsletterEmails : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterEmailId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId,"","", short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("NewsletterEmails"), "");
            if (Session["NletterEmails-ddlNewsletterGroups"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("NewsletterEmailsEdit.aspx"))
            {
                ddlNewsletterGroups.SelectedValue = Session["NletterEmails-ddlNewsletterGroups"].ToString();
                ddlOrderBy.SelectedValue = Session["NletterEmails-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["NletterEmails-ddlSite"].ToString();
                txtDateFrom.Text = Session["NletterEmails-txtDateFrom"].ToString();
                txtDateTo.Text = Session["NletterEmails-txtDateTo"].ToString();
                txtKeywords.Text = Session["NletterEmails-txtKeywords"].ToString();
            }
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
            NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_NewsletterGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            int m_CustomerId = 0;
            string m_Email = txtKeywords.Text.Trim();
            int m_IsReceiveNews = int.Parse(rbtIsReceiveNews.SelectedValue);
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_NewsletterEmails.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_NewsletterEmails.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_NewsletterGroupId, m_CustomerId, m_Email,m_IsReceiveNews, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
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
            }

            NewsletterEmailId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            
        }
    }
    protected void SetCurentData()
    {
        Session["NletterEmails-ddlNewsletterGroups"] = ddlNewsletterGroups.SelectedValue;
        Session["NletterEmails-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["NletterEmails-ddlSite"] = ddlSite.SelectedValue;
        Session["NletterEmails-txtDateFrom"] = txtDateFrom.Text;
        Session["NletterEmails-txtDateTo"] = txtDateTo.Text;
        Session["NletterEmails-txtKeywords"] = txtKeywords.Text;
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountFail = 0;
        int CountSuc = 0;
        try
        {
                NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
                m_NewsletterEmails.NewsletterEmailId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_NewsletterEmails.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1) CountFail++;
                else CountSuc++;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " bản ghi và thất bại "+CountFail.ToString()+" bản ghi", this);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountSuc = 0;
        try
        {
            NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_NewsletterEmails.NewsletterEmailId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_NewsletterEmails.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId != 1) CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " bản ghi", this);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
       CustomPaging.PageIndex = 1;
       BindData();
    }

    protected void ddlNewsletterGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void rbtIsReceiveNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
        DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }
}