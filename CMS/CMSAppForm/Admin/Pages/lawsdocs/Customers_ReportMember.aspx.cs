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
using sms.admin.security;
using System.IO;
using System.Text;
using ICSoft.AppForm.Admin;
using System.Data;

public partial class Admin_Pages_lawsdocs_Customers_ReportMember : System.Web.UI.Page
{
    protected List<Status> l_Status = new List<Status>();
    protected List<Services> l_Services = new List<Services>();
    protected List<ServicePakages> l_ServicePakages = new List<ServicePakages>();
    protected List<NewsletterGroups> l_NewsletterGroups = new List<NewsletterGroups>();
    protected List<Provinces> l_Provinces = new List<Provinces>();
    private short SiteId = 1;
    private short CountryId = 1;
    string datefromsame, datetosame;
    DateTime now;

    protected void Page_Load(object sender, EventArgs e)
    {
        now = DateTime.Now;
        if (!IsPostBack)
        {
            txtDateFrom.Text = now.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");
            SetTooltip();
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            SetTooltip();
            BindData();
        }
    }
    public void BindData()
    {
        try
        {
            l_Status = Status.Static_GetList();
            l_Services = Services.Static_GetList(0, SiteId);
            l_ServicePakages = ServicePakages.Static_GetList();
            l_NewsletterGroups = NewsletterGroups.Static_GetList();
            l_Provinces = Provinces.Static_GetList(CountryId);
            Customers m_Customers = new Customers();
            int RowCount = 0, CountCustomers = 0, CountRegisterNewsletter = 0, CountFreeService = 0, CountTrialService = 0, CountByStatusId1 = 0, CountByStatusId2 = 0, CountByNewsletterGroupId1 = 0, CountByNewsletterGroupId2 = 0, CountByNewsletterGroupId3 = 0, CountByDocGroupId1 = 0, CountByDocGroupId2 = 0, CountByDocGroupId3 = 0;
            string OrderBy = "";
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            DateTime DateFrom = sms.utils.StringUtil.ConvertToDateTime(m_DateFrom);
            DateTime DateTo = sms.utils.StringUtil.ConvertToDateTime(m_DateTo);
            double day = (DateTo - DateFrom).TotalDays;
            string Datefromsame = DateFrom.AddDays(-1 * day).ToString("dd/MM/yyyy");
            string Datetosame = DateTo.AddDays(-1 * day).ToString("dd/MM/yyyy");
            m_grid.DataSource = m_Customers.Customers_ReportMember_GetPage(m_DateFrom, m_DateTo, Datefromsame, Datetosame, OrderBy, CustomPaging.PageIndex - 1, m_grid.PageSize, ref CountCustomers, ref CountRegisterNewsletter, ref CountFreeService, ref CountTrialService, ref CountByStatusId1, ref CountByStatusId2, ref CountByNewsletterGroupId1, ref CountByNewsletterGroupId2, ref CountByNewsletterGroupId3, ref CountByDocGroupId1, ref CountByDocGroupId2, ref CountByDocGroupId3, ref RowCount);

            if (m_grid.PageSize > RowCount)
            {
                m_grid.AllowPaging = false;
            }
            else
            {
                m_grid.AllowPaging = true;
            }
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader; 
            lblTong.Text = CountCustomers.ToString("N0");
            lblCountFreeService.Text = "Miễn phí: " + (string.Format("{0:#,#}", CountFreeService) != "" ? string.Format("{0:#,#}", CountFreeService) : "0");
            lblCountTrialService.Text = "Dùng thử: " +  (string.Format("{0:#,#}", CountTrialService) != "" ? string.Format("{0:#,#}", CountTrialService) : "0");
            lblCountRegisterNewsletter.Text = "Nhận bản tin VB: " + (string.Format("{0:#,#}", CountRegisterNewsletter) != "" ? string.Format("{0:#,#}", CountRegisterNewsletter) : "0");
            lblCountByNewsletterGroupId1.Text = "Tiếng Việt: " + (string.Format("{0:#,#}", CountByNewsletterGroupId1) != "" ? string.Format("{0:#,#}", CountByNewsletterGroupId1) : "0");
            lblCountByNewsletterGroupId2.Text = "Tiếng Anh: " + (string.Format("{0:#,#}", CountByNewsletterGroupId2) != "" ? string.Format("{0:#,#}", CountByNewsletterGroupId2) : "0");
            lblCountByNewsletterGroupId3.Text = "Cả hai: " + (string.Format("{0:#,#}", CountByNewsletterGroupId3) != "" ? string.Format("{0:#,#}", CountByNewsletterGroupId3) : "0");
            lblCountByStatusId1.Text = "Hoạt động: " + (string.Format("{0:#,#}", CountByStatusId1) != "" ? string.Format("{0:#,#}", CountByStatusId1) : "0");
            lblCountByStatusId2.Text = "Bị khóa: " + (string.Format("{0:#,#}", CountByStatusId2) != "" ? string.Format("{0:#,#}", CountByStatusId2) : "0");
            lblCountByDocGroupId1.Text = "Văn bản quan tâm: " + (string.Format("{0:#,#}", CountByDocGroupId1) != "" ? string.Format("{0:#,#}", CountByDocGroupId1) : "0");
            lblCountByDocGroupId2.Text = "TCVN quan tâm: " + (string.Format("{0:#,#}", CountByDocGroupId2) != "" ? string.Format("{0:#,#}", CountByDocGroupId2) : "0");
            lblCountByDocGroupId3.Text = "TTHC quan tâm: " + (string.Format("{0:#,#}", CountByDocGroupId3) != "" ? string.Format("{0:#,#}", CountByDocGroupId3) : "0");
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
        CheckDate();
        SetTooltip();
        CustomPaging.PageIndex = 1;
        BindData();
    }

    public string ServicePackages_GetServicePackageNumMonthUse(short ServicePackageId)
    {
        string RetVal = string.Empty;
        ICSoft.LawDocsLib.ServicePackages m_ServicePackages = ICSoft.LawDocsLib.ServicePackages.Static_Get(ServicePackageId);
        if (m_ServicePackages != null)
        {
            RetVal = m_ServicePackages.NumMonthUse.ToString();
        }
        if(RetVal == "0" && m_ServicePackages.NumDayUse > 0)
        {
            RetVal = m_ServicePackages.NumDayUse.ToString() + " Ngày";
        }
        return RetVal;
    }

    protected void ddlDatetime_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DayOfWeek = (int)DateTime.Now.DayOfWeek;
        int DayOfMonth = (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        DateTime FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);
        DateTime FirstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
        DateTime LastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
        if (ddlDatetime.SelectedValue == "0")
        {
            datefromsame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddDays(-6).ToString("dd/MM/yyyy");
            datetosame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddDays(-6).ToString("dd/MM/yyyy");
        }
        else 
        {
            datefromsame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddYears(-1).ToString("dd/MM/yyyy");
            datetosame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddYears(-1).ToString("dd/MM/yyyy");
        }
        
        CustomPaging.PageIndex = 1;
        BindData();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void txtDateFrom_OnTextChanged(object sender, EventArgs e)
    {
        CheckDate();
        SetTooltip();
    }

    protected void txtDateTo_OnTextChanged(object sender, EventArgs e)
    {
        CheckDate();
        SetTooltip();
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
            lblMsg.Text = "Vui lòng nhập ngày hợp lệ";
        }
    }
    private void SetTooltip()
    {
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        DateTime df, dt;
        DateTime.TryParse(dateFrom, out df);
        DateTime.TryParse(dateTo, out dt);
        int compare = DateTime.Compare(dt, df);
        if (compare >= 0)
        {
            if (ddlDatetime.SelectedValue.Equals("1"))
            {
                ddlDatetime.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddYears(-1),
                    dt.AddYears(-1));
            }
            else
            {
                var daysDiff = (dt - df).TotalDays;
                ddlDatetime.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddDays(-daysDiff - 1),
                    df.AddDays(-1));
            }
        }
        else ddlDatetime.ToolTip = string.Empty;
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Customers m_Customers = new Customers();
            int RowCount = 0, CountCustomers = 0, CountRegisterNewsletter = 0, CountFreeService = 0, CountTrialService = 0, CountByStatusId1 = 0, CountByStatusId2 = 0, CountByNewsletterGroupId1 = 0, CountByNewsletterGroupId2 = 0, CountByNewsletterGroupId3 = 0, CountByDocGroupId1 = 0, CountByDocGroupId2 = 0, CountByDocGroupId3 = 0;
            string OrderBy = "";
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            DateTime DateFrom = sms.utils.StringUtil.ConvertToDateTime(m_DateFrom);
            DateTime DateTo = sms.utils.StringUtil.ConvertToDateTime(m_DateTo);
            double day = (DateTo - DateFrom).TotalDays;
            string Datefromsame = DateFrom.AddDays(-1 * day).ToString("dd/MM/yyyy");
            string Datetosame = DateTo.AddDays(-1 * day).ToString("dd/MM/yyyy");
            List<Customers> lCustomers = m_Customers.Customers_ReportMember_GetPage(m_DateFrom, m_DateTo, Datefromsame, Datetosame, OrderBy, 0, 0, ref CountCustomers, ref CountRegisterNewsletter, ref CountFreeService, ref CountTrialService, ref CountByStatusId1, ref CountByStatusId2, ref CountByNewsletterGroupId1, ref CountByNewsletterGroupId2, ref CountByNewsletterGroupId3, ref CountByDocGroupId1, ref CountByDocGroupId2, ref CountByDocGroupId3, ref RowCount);
            DataTable dataTable = ToDataTable(lCustomers);
            ExportToExcel(dataTable);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private DataTable ToDataTable(List<Customers> lCustomers)
    {
        DataTable dataTable = new DataTable(typeof(Customers).Name);
        l_Status = Status.Static_GetList();
        if (lCustomers == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("Họ và tên", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));
        dataTable.Columns.Add("Nhận bản tin", typeof(string));
        dataTable.Columns.Add("Văn bản quan tâm", typeof(string));
        dataTable.Columns.Add("Quan tâm dịch vụ", typeof(string));
        dataTable.Columns.Add("TTHC quan tâm", typeof(string));
        dataTable.Columns.Add("TCVN quan tâm", typeof(string));
        dataTable.Columns.Add("Sử dụng dịch vụ", typeof(string));
        dataTable.Columns.Add("Gói dịch vụ", typeof(string));
        dataTable.Columns.Add("Thời hạn", typeof(string));
        dataTable.Columns.Add("Hoạt động gần đây", typeof(string));
        dataTable.Columns.Add("Ngày đăng ký & trạng thái", typeof(string));
        dataTable.Columns.Add("Địa chỉ", typeof(string));
        dataTable.Columns.Add("Tỉnh thành phố", typeof(string));
        for (var index = 0; index < lCustomers.Count; index++)
        {
            Customers mCustomers = lCustomers[index];
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index + 1);
            string name = "Tài khoản: " + mCustomers.CustomerName + " <br />" + "Họ và tên: " + mCustomers.CustomerFullName;
            string status1 = Status.Static_Get(byte.Parse(mCustomers.StatusId.ToString()), l_Status).StatusDesc;
            string ngaydangky_trangthai = "Ngày ĐK: " + DateTimeHelpers.GetDateAndTime(mCustomers.CrDateTime) + " <br />" + status1;
            rowAdded.SetField("Họ và tên", name);
            rowAdded.SetField("Email", mCustomers.CustomerMail);
            rowAdded.SetField("Nhận bản tin", mCustomers.RegisterNewsletter> 0 ?"x":"");
            rowAdded.SetField("Văn bản quan tâm", mCustomers.VBQT > 0 ? "x" : "");
            rowAdded.SetField("Quan tâm dịch vụ", mCustomers.ApplicationId == 3 ? "Cả hai" : mCustomers.ApplicationId == 2 ? "Tiếng anh" : mCustomers.ApplicationId == 1?"Tiếng việt":"");
            rowAdded.SetField("TTHC quan tâm", mCustomers.TTHC > 0 ? "x" : "");
            rowAdded.SetField("TCVN quan tâm", mCustomers.TCVN > 0 ? "x" : "");
            rowAdded.SetField("Sử dụng dịch vụ", Services.Static_Get(mCustomers.ServiceId).ServiceName);
            rowAdded.SetField("Gói dịch vụ", ServicePackages.Static_Get(mCustomers.ServicePackageId).ServicePackageDesc);
            rowAdded.SetField("Thời hạn", ServicePackages_GetServicePackageNumMonthUse(mCustomers.ServicePackageId));
            rowAdded.SetField("Hoạt động gần đây", DateTimeHelpers.GetDateAndTime(mCustomers.LastLoginTime));
            rowAdded.SetField("Ngày đăng ký & trạng thái", ngaydangky_trangthai);
            rowAdded.SetField("Địa chỉ", mCustomers.Address);
            rowAdded.SetField("Tỉnh thành phố", Provinces.Static_Get(mCustomers.ProvinceId).ProvinceDesc);
        }
        return dataTable;
    }
    private void ExportToExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BaoCaoThanhVien.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        HttpContext.Current.Response.Write(string.Format("<strong style='text-align: center;'>Báo cáo thành viên từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text));

        HttpContext.Current.Response.Write("</ br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor=#5cd6ff>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Họ và tên");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Email");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Nhận bản tin");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Văn bản quan tâm");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Quan tâm dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("TTHC quan tâm");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("TCVN quan tâm");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Sử dụng dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Gói dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thời hạn (Tháng)");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Hoạt động gần đây");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Ngày đăng ký & Trạng thái");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Địa chỉ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tỉnh T.Phố");
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
}