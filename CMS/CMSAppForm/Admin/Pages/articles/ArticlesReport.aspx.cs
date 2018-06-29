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
using System.Globalization;

public partial class Admin_Pages_articles_ArticlesReport : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterReadLogId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    private List<ArticlesReport> l_ArticlesReport = new List<ArticlesReport>();
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Users> l_Users = new List<Users>();
    Users m_Users = new Users();
    DateTime now;
    int dayofweek, dayofmonth;
    int RowCount;
    protected int SumPending = 0, SumReviewed = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        l_ReviewStatus = ReviewStatus.Static_GetList();
        l_DataSources = DataSources.Static_GetList();
        l_Users = m_Users.GetAll();
        now = DateTime.Now;
        dayofweek = (int)now.DayOfWeek;
        dayofmonth = (int)DateTime.DaysInMonth(now.Year, now.Month);
        if (!IsPostBack)
        {
            txtDateFrom.Text = now.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");

            List<OrderByClauses> l_OrderByClauses = new List<OrderByClauses>();
            List<OrderByClauses> l_ReportBy = new List<OrderByClauses>();
            OrderByClauses m_OrderByClauses = new OrderByClauses();
            m_OrderByClauses.OrderByDesc = "Ngày"; m_OrderByClauses.OrderBy = "0";
            l_OrderByClauses.Add(m_OrderByClauses);
            OrderByClauses m_OrderByClauses1 = new OrderByClauses();
            m_OrderByClauses1.OrderByDesc = "Tuần"; m_OrderByClauses1.OrderBy = "1";
            l_OrderByClauses.Add(m_OrderByClauses1);
            OrderByClauses m_OrderByClauses2 = new OrderByClauses();
            m_OrderByClauses2.OrderByDesc = "Tháng"; m_OrderByClauses2.OrderBy = "2";
            l_OrderByClauses.Add(m_OrderByClauses2);
            OrderByClauses m_OrderByClauses3 = new OrderByClauses();
            m_OrderByClauses3.OrderByDesc = "Năm"; m_OrderByClauses3.OrderBy = "3";
            l_OrderByClauses.Add(m_OrderByClauses3);
            OrderByClauses m_OrderByClauses4 = new OrderByClauses();
            m_OrderByClauses4.OrderByDesc = "Tùy chọn"; m_OrderByClauses4.OrderBy = "4";
            l_OrderByClauses.Add(m_OrderByClauses4);
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, l_OrderByClauses, "");

            DropDownListHelpers.DDL_Bind(ddlSites, Sites.Static_GetList(ActUserId),"");
            List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSites.SelectedValue), 0, 0, 0, 0, 0, "- ");
            DropDownListHelpers.DDL_Bind(ddlReportBy, l_Category, "...");
            DropDownListHelpers.DDL_Bind(ddlReviewStatus, ReviewStatus.Static_GetList(),"...");
            ddlOrderBy.SelectedValue = "4";
            txtDateFrom.Enabled = true;
            txtDateTo.Enabled = true;
            Newsletters m_Newsletters = new Newsletters();

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
            CheckDate();
            string m_reportBy = ddlReportBy.SelectedValue;
            string m_reportBy_text = ddlReportBy.Text;
            int RowAmount = 50, PageIndex = CustomPaging.PageIndex - 1;
            string OrderBy = ddlGridOrderBy.SelectedValue;
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            byte SiteId = byte.Parse(ddlSites.SelectedValue);
            int LanguageId = 1, ApplicationTypeId = 1, ArticleId = 0, CategoryId = int.Parse(m_reportBy);
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            ArticlesReport m_ArticlesReport = new ArticlesReport();
            l_ArticlesReport = m_ArticlesReport.GetListReport(ActUserId,RowAmount,PageIndex,SiteId,OrderBy,LanguageId,
                ApplicationTypeId, ArticleId, CategoryId, m_DateFrom, m_DateTo, ReviewStatusId, ref RowCount, ref SumPending, ref SumReviewed);
            m_grid.DataSource = l_ArticlesReport;
            m_grid.DataBind();
            
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            lblSumPending.Text = (string.Format("{0:#,#}", SumPending) != "" ? string.Format("{0:#,#}", SumPending) : "0");
            lblSumReviewed.Text = (string.Format("{0:#,#}", SumReviewed) != "" ? string.Format("{0:#,#}", SumReviewed) : "0");
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        string m_OrderBy = ddlOrderBy.SelectedValue;
        string datefrom, dateto;
        if (m_OrderBy.Equals("0"))
        {
            txtDateFrom.Text = now.ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");
        }
        else if (m_OrderBy.Equals("1"))
        {
            txtDateFrom.Text = now.AddDays(1 - dayofweek).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.AddDays(6 - dayofweek).ToString("dd/MM/yyyy");
        }
        else if (m_OrderBy.Equals("2"))
        {
            datefrom = "01/" + now.Month + "/" + now.Year;
            dateto = dayofmonth + "/" + now.Month + "/" + now.Year;
            txtDateFrom.Text = datefrom;
            txtDateTo.Text = dateto;
        }
        else if (m_OrderBy.Equals("3"))
        {
            datefrom = "01/01/" + now.Year;
            dateto = now.ToString("dd/MM/yyyy");
            txtDateFrom.Text = datefrom;
            txtDateTo.Text = dateto;
        }
        else if (m_OrderBy.Equals("4"))
        {
            txtDateFrom.Text = now.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");
        }
        if (ddlOrderBy.SelectedValue == "4")
        {
            txtDateFrom.Enabled = true;
            txtDateTo.Enabled = true;
        }
        else
        {
            txtDateFrom.Enabled = false;
            txtDateTo.Enabled = false;
        }
        BindData();
    }
    //protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    CustomPaging.PageIndex = 1;
    //    Newsletters m_Newsletters = new Newsletters();
    //    int RowCount = 0;
    //    DropDownListHelpers.DDL_Bind(ddlMessage, m_Newsletters.GetPage(ActUserId, 100, 0, "", short.Parse(ddlSite.SelectedValue), 0, "", "", 0, "", "", ref RowCount), "...");
    //    BindData();
    //}
    protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSites.SelectedValue), 0, 0, 0, 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlReportBy, l_Category, "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void txtDateFrom_OnTextChanged(object sender, EventArgs e)
    {
        CheckDate();
    }

    protected void txtDateTo_OnTextChanged(object sender, EventArgs e)
    {
        CheckDate();
    }
    private void CheckDate()
    {
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        DateTime df, dt;
        DateTime.TryParse(dateFrom, out df);
        DateTime.TryParse(dateTo, out dt);
        TimeSpan Time = dt - df;
        int TongSoNgay = Time.Days;
        if (TongSoNgay >= 0)
        {
            lblMsg.Text = "";
        }
        else
        {
            lblMsg.Text = "Vui lòng nhập ngày hợp lệ.";
        }
    }
}