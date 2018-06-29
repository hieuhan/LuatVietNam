using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.admin.security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_DocDetailReports : System.Web.UI.Page
{
    protected List<Users> l_Users = new List<Users>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<OrderByClauses> l_OrderByClauses = new List<OrderByClauses>();
    protected int RowCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            //DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Docs"), "");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(),"...");
            l_Users = new Users().GetAll().OrderBy(x => x.Username).ToList();
            l_DataSources = DataSources.Static_GetListAll();

            ddlCrUserId.DataSource = l_Users;
            ddlCrUserId.DataBind();
            ddlCrUserId.Items.Insert(0, new ListItem("...", "0"));
            
            ddlRevUserIds.DataSource = l_Users;
            ddlRevUserIds.DataBind();
            ddlRevUserIds.Items.Insert(0, new ListItem("...", "0"));

            l_OrderByClauses = OrderByClauses.Static_GetList("Docs");
            l_OrderByClauses.Add(new OrderByClauses { OrderBy = "DownloadCount DESC", OrderByDesc = "Lượt tải" });
            ddlOrderBy.DataSource = l_OrderByClauses;
            ddlOrderBy.DataBind();

            //BindData();
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
            int mCrUserId = 0, mRevUserId=0, countPending = 0, countReviewed = 0, countUnreview = 0;
            l_Users = new Users().GetAll();
            l_DataSources = DataSources.Static_GetListAll();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            mCrUserId = int.Parse(ddlCrUserId.SelectedValue);
            mRevUserId = int.Parse(ddlRevUserIds.SelectedValue);
            string mOrderBy = ddlOrderBy.SelectedValue;
            short mDataSourceId = short.Parse(ddlDataSources.SelectedValue);
            short mFieldId = short.Parse(ddlFields.SelectedValue);
            short mOrganId = short.Parse(ddlOrgans.SelectedValue);
            byte mReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string m_SearchByDate = "CrDateTime";
            string mDateFrom = txtDateFrom.Text.Trim();
            string mDateTo = txtDateTo.Text.Trim();
            m_grid.DataSource = new Docs().Docs_ReportDetail_GetPage(0, m_SearchByDate, mFieldId, mOrganId, mReviewStatusId, mCrUserId, mRevUserId, mDataSourceId, mDateFrom, mDateTo, mOrderBy, CustomPaging.PageIndex - 1, m_grid.PageSize, ref countPending, ref countReviewed, ref countUnreview, ref RowCount);

            m_grid.AllowPaging = m_grid.PageSize <= RowCount;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = TotalFormat(RowCount);
            lblPending.Text = TotalFormat(countPending);
            lblReviewed.Text = TotalFormat(countReviewed);
            lblUnreview.Text = TotalFormat(countUnreview);
            CustomPaging.TotalPage = RowCount == 0 ? 1 : RowCount % m_grid.PageSize == 0 ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            btnXuatExcel.ToolTip = string.Format("Danh sách {0} văn bản", RowCount > 10000 ? "10.000" : RowCount.ToString("#,##"));
            btnXuatExcel.Visible = RowCount > 0;
            if (!string.IsNullOrEmpty(mDateFrom) && !string.IsNullOrEmpty(mDateTo))
            {
                DateTime df, dt;
                DateTime.TryParse(mDateFrom, out df);
                DateTime.TryParse(mDateTo, out dt);
                if (df == DateTime.MinValue || dt == DateTime.MinValue)
                {
                    JSAlertHelpers.Alert("Ngày chọn không hợp lệ.", this);
                }
                else
                {
                    int compare = DateTime.Compare(dt, df);
                    if (compare < 0)
                    {
                        JSAlertHelpers.Alert("Ngày chọn không hợp lệ.", this);
                    }
                }
            }
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

    protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlCrUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlDataSources_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlDatetime_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DayOfWeek = (int)DateTime.Now.DayOfWeek;
        int DayOfMonth = (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        DateTime FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);
        DateTime FirstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
        DateTime LastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
        switch (ddlDatetime.SelectedValue)
        {
            case "0":
                txtDateFrom.Text = "";
                txtDateTo.Text = "";
                break;
            case "1":
                txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                break;
            case "2":
                txtDateFrom.Text = DateTime.Now.AddDays(1 - DayOfWeek).ToString("dd/MM/yyyy");
                txtDateTo.Text = DateTime.Now.AddDays(6 - DayOfWeek).ToString("dd/MM/yyyy");
                break;
            case "3":
                txtDateFrom.Text = FirstDayOfMonth.ToString("dd/MM/yyyy");
                txtDateTo.Text = LastDayOfMonth.ToString("dd/MM/yyyy");
                break;
            case "4":
                txtDateFrom.Text = FirstDayOfYear.ToString("dd/MM/yyyy");
                txtDateTo.Text = LastDayOfYear.ToString("dd/MM/yyyy");
                break;
        }
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlRevUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int mCrUserId = 0, rowCount = 0, countPending = 0, countReviewed = 0, countUnreview = 0, pageSize = 10000;
            l_Users = new Users().GetAll();
            l_DataSources = DataSources.Static_GetListAll();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            string mOrderBy = ddlOrderBy.SelectedValue;
            short mDataSourceId = short.Parse(ddlDataSources.SelectedValue);
            short mFieldId = short.Parse(ddlFields.SelectedValue);
            short mOrganId = short.Parse(ddlOrgans.SelectedValue);
            byte mReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string m_SearchByDate = "GazetteDate";
            string mDateFrom = txtDateFrom.Text.Trim();
            string mDateTo = txtDateTo.Text.Trim();
            List<Docs> lDocs = new Docs().Docs_ReportDetail_GetPage(0, m_SearchByDate, mFieldId, mOrganId, mReviewStatusId, mCrUserId, 0, mDataSourceId, mDateFrom, mDateTo, mOrderBy, CustomPaging.PageIndex - 1, pageSize, ref countPending, ref countReviewed, ref countUnreview, ref rowCount);
            DataTable dt = ToDataTable(lDocs);
            ExportToExcel(dt);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private string TotalFormat(int total)
    {
        return total > 0 ? string.Format("{0:#,##}", total) : "0";
    }

    protected string GetIssueDate(string docGroupId, string issueDate, string issueYear)
    {
        string retVal = string.Empty;
        if (!string.IsNullOrEmpty(docGroupId))
        {
            retVal = docGroupId.Equals(WebConstans.TCVN_DocGroupId.ToString())
                ? (!string.IsNullOrEmpty(issueDate)
                    ? issueDate
                    : (!string.IsNullOrEmpty(issueYear) ? issueYear : ""))
                : (!string.IsNullOrEmpty(issueDate) ? issueDate : "");
        }
        return DateTimeHelpers.GetDateAndTimeOnly(retVal);
    }

    private void ExportToExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BaoCaoVanBanChiTiet.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        if (!string.IsNullOrWhiteSpace(txtDateFrom.Text) && !string.IsNullOrWhiteSpace(txtDateTo.Text))
        {
            HttpContext.Current.Response.Write(string.Format("<strong>Báo cáo dữ liệu văn bản từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text));
        }
        HttpContext.Current.Response.Write("<br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor=#5cd6ff>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tên văn bản");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Ngày ban hành");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Cơ quan ban hành");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lĩnh vực");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Trạng thái");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Biên tập viên");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Người duyệt");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Nguồn dữ liệu");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lượt tải");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("</tr>");
        foreach (DataRow row in table.Rows)
        {
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    private DataTable ToDataTable(List<Docs> lDocs)
    {
        DataTable dataTable = new DataTable(typeof(Docs).Name);
        if(lDocs == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("DocName", typeof(string));
        dataTable.Columns.Add("IssueDate", typeof(string));
        dataTable.Columns.Add("OrganName", typeof(string));
        dataTable.Columns.Add("FieldName", typeof(string));
        dataTable.Columns.Add("ReviewStatusId", typeof(string));
        dataTable.Columns.Add("CrUserId", typeof(string));
        dataTable.Columns.Add("RevUserId", typeof(string));
        dataTable.Columns.Add("DataSourceId", typeof(string));
        dataTable.Columns.Add("DownloadCount", typeof(int));
        for (var index = 0; index < lDocs.Count; index++)
        {
            Docs doc = lDocs[index];
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index+1);
            rowAdded.SetField("DocName", doc.DocName);
            rowAdded.SetField("IssueDate",
                GetIssueDate(doc.DocGroupId.ToString(), doc.IssueDate.ToString("dd/MM/yyyy"),
                    doc.IssueYear.ToString()));
            rowAdded.SetField("OrganName", doc.OrganName);
            rowAdded.SetField("FieldName", doc.FieldName);
            rowAdded.SetField("ReviewStatusId",
                ReviewStatus.Static_Get(doc.ReviewStatusId, l_ReviewStatus).ReviewStatusDesc);
            rowAdded.SetField("CrUserId", UserHelpers.Static_Get(doc.CrUserId, l_Users, string.Empty).Username);
            rowAdded.SetField("RevUserId", UserHelpers.Static_Get(doc.RevUserId, l_Users, string.Empty).Username);
            rowAdded.SetField("DataSourceId", DataSources.Static_Get(doc.DataSourceId, l_DataSources).DataSourceDesc);
            rowAdded.SetField("DownloadCount", doc.DownloadCount);
        }
        return dataTable;
    }
}