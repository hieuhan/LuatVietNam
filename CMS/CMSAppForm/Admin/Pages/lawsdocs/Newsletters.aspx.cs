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

public partial class Admin_Newsletters : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterId = 0;
    protected List<NewsletterStatus> l_NewsletterStatus = new List<NewsletterStatus>();
    protected List<NewsletterGroups> l_NewsletterGroups = new List<NewsletterGroups>();
    protected List<Newsletters> l_Newsletters = new List<Newsletters>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLNewsletterStatus_BindByCulture(ddlNewsletterStatus, NewsletterStatus.Static_GetList(), "...");
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Newsletters"), "");
            if (Session["NewsLetter-ddlNewsletterGroups"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("NewslettersEdit.aspx"))
            {
                
                ddlNewsletterGroups.SelectedValue = Session["NewsLetter-ddlNewsletterGroups"].ToString();
                ddlNewsletterStatus.SelectedValue = Session["NewsLetter-ddlNewsletterStatus"].ToString();
                ddlOrderBy.SelectedValue = Session["NewsLetter-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["NewsLetter-ddlSite"].ToString();
                txtDateFrom.Text = Session["NewsLetter-txtDateFrom"].ToString();
                txtDateFrom.Text = Session["NewsLetter-txtDateTo"].ToString();
                txtDateFrom.Text = Session["NewsLetter-txtSendFrom"].ToString();
                txtDateFrom.Text = Session["NewsLetter-txtTitle"].ToString();
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
            l_NewsletterStatus = NewsletterStatus.Static_GetList();
            l_NewsletterGroups = NewsletterGroups.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            Newsletters m_Newsletters = new Newsletters();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_NewsletterStatusId = byte.Parse(ddlNewsletterStatus.SelectedValue);
            short m_NewsletterGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            short m_SiteId = short.Parse(ddlSite.SelectedValue);
            string m_SendFrom = txtSendFrom.Text;
            string m_Title = txtTitle.Text;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            m_grid.DataSource = m_Newsletters.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_SiteId, m_NewsletterGroupId, m_SendFrom,m_Title, m_NewsletterStatusId, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
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
    protected void SetCurentData()
    {
        Session["NewsLetter-ddlNewsletterGroups"] = ddlNewsletterGroups.SelectedValue;
        Session["NewsLetter-ddlNewsletterStatus"] = ddlNewsletterStatus.SelectedValue;
        Session["NewsLetter-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["NewsLetter-ddlSite"] = ddlSite.SelectedValue;
        Session["NewsLetter-txtDateFrom"] = txtDateFrom.Text;
        Session["NewsLetter-txtDateTo"] = txtDateTo.Text;
        Session["NewsLetter-txtSendFrom"] = txtSendFrom.Text;
        Session["NewsLetter-txtTitle"] = txtTitle.Text;
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

            NewsletterId = short.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        try
        {
                Newsletters m_Newsletters = new Newsletters();
                m_Newsletters.NewsletterId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Newsletters.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                 if (SysMessageTypeId == 1) Message="Xóa bản tin thất bại do đang ở trạng thái duyệt";
                else Message="Xóa bản tin thành công";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountSuc = 0;
        int CountFail = 0;
        string Message = "";
        try
        {
            Newsletters m_Newsletters = new Newsletters();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Newsletters.NewsletterId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Newsletters.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) CountFail++;
                        else CountSuc++;
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
        if (CountFail > 0) Message = "Xóa thành công " + CountSuc.ToString() + " bản ghi, xóa thất bại " + CountFail + " bản ghi do đang ở trạng thái duyệt";
        else Message = "Xóa thành công " + CountSuc.ToString() + " bản ghi";
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }


    protected void ddlNewsletterStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlNewsletterGroups_SelectedIndexChanged(object sender, EventArgs e)
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