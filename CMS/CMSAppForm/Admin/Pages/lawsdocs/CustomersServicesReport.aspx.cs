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
using System.IO;
using System.Data;

public partial class Admin_Pages_lawsdocs_CustomersServicesReport : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterReadLogId = 0;
    protected List<ServicePakages> l_ServicePakages = new List<ServicePakages>();
    protected List<PaymentTypes> l_PaymentTypes = new List<PaymentTypes>();
    DateTime now;
    string datefromsame, datetosame;
    int dayofweek, dayofmonth;
    string datefrom, dateto;
    private CustomerLogReportHeader m_CustomerLogReportHeader = new CustomerLogReportHeader();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        now = DateTime.Now;
        dayofweek = (int)now.DayOfWeek;
        dayofmonth = (int)DateTime.DaysInMonth(now.Year, now.Month);
        m_CustomerLogReportHeader = m_CustomerLogReportHeader.Get();
        if (!IsPostBack)
        {
            txtDateFrom.Text = now.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");

            lblTotalCustomerServices.Text = string.Format("{0:#,#}", m_CustomerLogReportHeader.CountCustomerServices);
            lblTotalMoney.Text = string.Format("{0:#,#}", m_CustomerLogReportHeader.TotalMoney);
            lblCountPayOnline.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountPayOnline);
            lblCountPayOffline.Text = string.Format("{0:#,#}", m_CustomerLogReportHeader.CountPayOffline);
            lblCountStatusStillExpired.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountStatusStillExpired);
            lblCountStatusExpiringSoon.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountStatusExpiringSoon);
            lblCountServicePackageTC.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountServicePackageTC);
            lblCountServicePackageNC.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountServicePackageNC);
            lblCountServicePackageTA.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountServicePackageTA);
            lblCountServicePackageNV.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountServicePackageNV);

            lblTotalMoneyTC.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.TotalMoneyTC);
            lblTotalMoneyNC.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.TotalMoneyNC);
            lblTotalMoneyTA.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.TotalMoneyTA);
            lblTotalMoneyNV.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.TotalMoneyNV);

            lblCountPaymentTypeBank.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountPaymentTypeBank);
            lblCountPaymentTypeIncomOffice.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountPaymentTypeIncomOffice);
            lblCountPaymentTypeLuatVNCard.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountPaymentTypeLuatVNCard);
            lblCountNumberPayFirst.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountNumberPayFirst);
            lblCountNumberPaySecond.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountNumberPaySecond);
            lblCountNumberPay3rd.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountNumberPay3rd);
            lblCountNumberPay4rd.Text = string.Format("{0:#,#}",m_CustomerLogReportHeader.CountNumberPay4rd);

            DropDownListHelpers.DDL_Bind(ddlServices, Services.Static_GetList(ActUserId, 0), "...");
            DropDownListHelpers.DDL_Bind(ddlPaymentTypes, PaymentTypes.Static_GetList(), "...");
            BindData();
            ddlOrderBy.SelectedValue = "4";
            txtDateFrom.Enabled = true;
            txtDateTo.Enabled = true;
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
        SetTooltip();
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            CustomerLogReport m_CustomerLogReport = new CustomerLogReport();

            l_ServicePakages = ServicePakages.Static_GetList();
            l_PaymentTypes = PaymentTypes.Static_GetList();
            string m_OrderBy = "";
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text; string dateFromSame = "", dateToSame = "";
            DateTime df, dt;
            DateTime.TryParse(m_DateFrom, out df);
            DateTime.TryParse(m_DateTo, out dt);
            TimeSpan Time = dt - df;
            int TongSoNgay = Time.Days;
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            else //cùng kỳ liền trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddDays(-TongSoNgay).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = df.AddDays(-1).ToString("dd/MM/yyyy");
            }
            short mServiceId = short.Parse(ddlServices.SelectedValue);
            short mServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            byte mPaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            byte mStatusId = byte.Parse(ddlStatus.SelectedValue);
            m_grid.DataSource = m_CustomerLogReport.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, mServiceId, mServicePackageId, mPaymentTypeId, mStatusId, m_OrderBy, m_DateFrom, m_DateTo, dateFromSame, dateToSame, ref RowCount);
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
    //private void BindDataForExcel()
    //{
    //    try
    //    {
    //        int RowCount = 0;
    //        CustomerLogReport m_CustomerLogReport = new CustomerLogReport();
    //        l_ServicePakages = ServicePakages.Static_GetList();
    //        l_PaymentTypes = PaymentTypes.Static_GetList();
    //        string m_OrderBy = "";
    //        string m_DateFrom = txtDateFrom.Text;
    //        string m_DateTo = txtDateTo.Text; string dateFromSame = "", dateToSame = "";
    //        DateTime df, dt;
    //        DateTime.TryParse(m_DateFrom, out df);
    //        DateTime.TryParse(m_DateTo, out dt);
    //        TimeSpan Time = dt - df;
    //        int TongSoNgay = Time.Days;
    //        if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
    //        {
    //            if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
    //            if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
    //        }
    //        else //cùng kỳ liền trước
    //        {
    //            if (df != DateTime.MinValue) dateFromSame = df.AddDays(-TongSoNgay).ToString("dd/MM/yyyy");
    //            if (dt != DateTime.MinValue) dateToSame = df.AddDays(-1).ToString("dd/MM/yyyy");
    //        }
    //        short mServiceId = short.Parse(ddlServices.SelectedValue);
    //        short mServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
    //        byte mPaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
    //        byte mStatusId = byte.Parse(ddlStatus.SelectedValue);
    //        List<CustomerLogReport> list = m_CustomerLogReport.GetPage(ActUserId, 1000, CustomPaging.PageIndex - 1, mServiceId, mServicePackageId, mPaymentTypeId, mStatusId, m_OrderBy, m_DateFrom, m_DateTo, dateFromSame, dateToSame, ref RowCount);
    //        m_grid.DataSource = list;
    //        m_grid.DataBind();
    //        m_grid.Caption = "Báo cáo khách hàng thuê bao từ ngày: " + m_DateFrom + " đến " + m_DateTo;
    //        lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
    //        CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
    //        JSAlertHelpers.Alert(ex.Message.ToString(), this);
    //    }
    //}
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

        txtDateFrom.Enabled = false;
        txtDateTo.Enabled = false;
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
            txtDateFrom.Enabled = true;
            txtDateTo.Enabled = true;
        }
        BindData();
    }
    protected void ddlMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlCompare_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SetTooltip();
        BindData();
    }

    protected void txtDateFrom_OnTextChanged(object sender, EventArgs e)
    {
        SetTooltip();
    }

    protected void txtDateTo_OnTextChanged(object sender, EventArgs e)
    {
        SetTooltip();
    }

    private void SetTooltip()
    {
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        DateTime df, dt;
        DateTime.TryParse(dateFrom, out df);
        DateTime.TryParse(dateTo, out dt);
        //int compare = DateTime.Compare(dt, df);
        TimeSpan Time = dt - df;
        int TongSoNgay = Time.Days;
        if (TongSoNgay >= 0)
        {
            lblMsg.Text = "";
            if (ddlCompare.SelectedValue.Equals("2"))
            {
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddYears(-1),
                    dt.AddYears(-1));
            }
            else
            {
                //var daysDiff = (dt - df).TotalDays;
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddDays(-TongSoNgay - 1),
                    df.AddDays(-1));
            }
        }
        else
        {
            ddlCompare.ToolTip = string.Empty;
            lblMsg.Text = "Vui lòng nhập ngày hợp lệ.";
        }
    }
    //-----------------------------------------------------------------------------
    public static ServicePakages ServicePakages_Static_Get(int ServicePakagesID, List<ServicePakages> lList)
    {
        ServicePakages RetVal = new ServicePakages();
        if (ServicePakagesID > 0 && lList.Count > 0)
        {
            RetVal = lList.Find(i => i.ServicePakageId == ServicePakagesID);
            if (RetVal == null) RetVal = new ServicePakages();
        }
        return RetVal;
    }
    protected void ddlPaymentTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (short.Parse(ddlServices.SelectedValue) != 0)
        {
            DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, 1, short.Parse(ddlServices.SelectedValue), "--"), "...");
        }
        else
        {
            DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, new List<ServicePackages>(), "...");
        }
        BindData();
    }

    protected void ddlServicePackages_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    public string ServicePackages_GetServicePackageNumMonthUse(short ServicePackageId)
    {
        string RetVal = string.Empty;
        ICSoft.LawDocsLib.ServicePackages m_ServicePackages = ICSoft.LawDocsLib.ServicePackages.Static_Get(ServicePackageId);
        if (ServicePackageId == 0)
        {
            RetVal = "Gói DV cũ";
        }
        else if (ServicePackageId == 24)
        {
            RetVal = "Gói miễn phí";
        }
        else if (m_ServicePackages != null && m_ServicePackages.NumMonthUse > 0)
        {
            RetVal = m_ServicePackages.NumMonthUse.ToString() + " Tháng";
        }
        else if (m_ServicePackages.NumMonthUse == 0 && m_ServicePackages.NumDayUse > 0)
        {
            RetVal = m_ServicePackages.NumDayUse.ToString() + " Ngày";
        }
        else
        {
            RetVal = "";
        }
        return RetVal;
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int RowCount = 0;
            CustomerLogReport m_CustomerLogReport = new CustomerLogReport();
            string m_OrderBy = "";
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text; string dateFromSame = "", dateToSame = "";
            DateTime df, dt;
            DateTime.TryParse(m_DateFrom, out df);
            DateTime.TryParse(m_DateTo, out dt);
            TimeSpan Time = dt - df;
            int TongSoNgay = Time.Days;
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            else //cùng kỳ liền trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddDays(-TongSoNgay).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = df.AddDays(-1).ToString("dd/MM/yyyy");
            }
            short mServiceId = short.Parse(ddlServices.SelectedValue);
            short mServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            byte mPaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            byte mStatusId = byte.Parse(ddlStatus.SelectedValue);
            List<CustomerLogReport> list = m_CustomerLogReport.GetPage(ActUserId, 0, 0, mServiceId, mServicePackageId, mPaymentTypeId, mStatusId, m_OrderBy, m_DateFrom, m_DateTo, dateFromSame, dateToSame, ref RowCount);
            DataTable dataTable = ToDataTable(list);
            ExportToExcel(dataTable);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private DataTable ToDataTable(List<CustomerLogReport> list)
    {
        DataTable dataTable = new DataTable(typeof(CustomerLogReport).Name);
        l_PaymentTypes = PaymentTypes.Static_GetList();
        if (list == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("Tài khoản", typeof(string));
        dataTable.Columns.Add("Họ và tên", typeof(string));
        dataTable.Columns.Add("Dịch vụ", typeof(string));
        dataTable.Columns.Add("Hình thức mua", typeof(string));
        dataTable.Columns.Add("Kênh thanh toán", typeof(string));
        dataTable.Columns.Add("Số tài khoản(dùng đồng thời)", typeof(string));
        dataTable.Columns.Add("Thời gian thuê bao", typeof(string));
        dataTable.Columns.Add("Ngày đăng ký", typeof(string));
        dataTable.Columns.Add("Hạn sử dụng", typeof(string));
        dataTable.Columns.Add("Tình trạng sử dụng", typeof(string));
        dataTable.Columns.Add("Tổng tiền", typeof(string));
        for (var index = 0; index < list.Count; index++)
        {
            CustomerLogReport mCustomerLogReport = list[index];
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index + 1);
            rowAdded.SetField("Tài khoản", mCustomerLogReport.CustomerName);
            rowAdded.SetField("Họ và tên", mCustomerLogReport.CustomerFullName);
            rowAdded.SetField("Dịch vụ", mCustomerLogReport.ServiceDesc);
            rowAdded.SetField("Hình thức mua", mCustomerLogReport.PaymentTypeId == 5 ? "Trực tiếp" : mCustomerLogReport.PaymentTypeId > 0 ? "Online" : "");
            rowAdded.SetField("Kênh thanh toán", PaymentTypes.Static_Get(byte.Parse(mCustomerLogReport.PaymentTypeId.ToString()), l_PaymentTypes).PaymentTypeDesc);
            rowAdded.SetField("Số tài khoản(dùng đồng thời)", mCustomerLogReport.MaxConcurrentLogin <=3? "1 đến 3" : mCustomerLogReport.MaxConcurrentLogin.ToString());
            rowAdded.SetField("Thời gian thuê bao", ServicePackages_GetServicePackageNumMonthUse(short.Parse(mCustomerLogReport.ServicePackageId.ToString())));
            rowAdded.SetField("Ngày đăng ký", DateTimeHelpers.GetDateAndTime(mCustomerLogReport.BeginTime));
            rowAdded.SetField("Hạn sử dụng", DateTimeHelpers.GetDateAndTime(mCustomerLogReport.EndTime));
            rowAdded.SetField("Tình trạng sử dụng", (DateTimeHelpers.GetDateAndTime(mCustomerLogReport.EndTime).Length > 0 ? (DateTime.Compare(DateTime.ParseExact(DateTimeHelpers.GetDateAndTime(mCustomerLogReport.EndTime), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture), DateTime.Now.Date) < 0 ? "Hết hạn" : "Còn hạn") : ""));
            rowAdded.SetField("Tổng tiền", mCustomerLogReport.TotalPaymentMoney.ToString().Replace(".",","));
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
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BaoCaoKhachHangThueBao.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        HttpContext.Current.Response.Write(string.Format("<strong style='text-align: center;'>Báo cáo khách hàng thuê bao từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text));

        HttpContext.Current.Response.Write("</ br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor=#5cd6ff>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tài khoản");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Họ và tên");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");


        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Hình thức mua");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Kênh thanh toán");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Số tài khoản <br />(Dùng đồng thời)");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thời gian thuê bao");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Ngày đăng ký");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Hạn sử dụng");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tình trạng sử dụng");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tổng tiền");
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