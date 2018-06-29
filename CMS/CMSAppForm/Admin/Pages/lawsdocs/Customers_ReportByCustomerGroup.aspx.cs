using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_Customers_ReportByCustomerGroup : System.Web.UI.Page
{
    private int _sumByStatus1 = 0,_sumByStatus2 = 0,_sumByTime1 = 0,_sumByTime2 = 0;
    private DateTime _dateTime = DateTime.Now;
    private string dateFromSame, dateToSame;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = _dateTime.AddDays(-7).ToString("dd/MM/yyyy");
            txtDateTo.Text = _dateTime.ToString("dd/MM/yyyy");
            SetTooltip();
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            string dateFrom = txtDateFrom.Text.Trim();
            string dateTo = txtDateTo.Text.Trim();
            DateTime df, dt;
            df = sms.utils.StringUtil.ConvertToDateTime(dateFrom);
            dt = sms.utils.StringUtil.ConvertToDateTime(dateTo);
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            else
            {
                double DayDiff = (dt - df).TotalDays;
                if (df != DateTime.MinValue) dateFromSame = df.AddDays(-1* DayDiff).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddDays(-1 * DayDiff).ToString("dd/MM/yyyy");
            }
            m_grid.DataSource = new Customers().Customers_ReportByCustomerGroup(dateFrom, dateTo, dateFromSame, dateToSame);
            m_grid.AllowPaging = false;
            m_grid.DataBind();
            SetTooltip();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Customers mCustomers = (Customers) e.Row.DataItem;
            _sumByStatus1 += mCustomers.SumByStatus1;
            _sumByStatus2 += mCustomers.SumByStatus2;
            _sumByTime1 += mCustomers.SumByTime1;
            _sumByTime2 += mCustomers.SumByTime2;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = @"Tổng: ";
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;

            e.Row.Cells[2].Text = TotalFormat(_sumByStatus1);
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;

            e.Row.Cells[3].Text = TotalFormat(_sumByStatus2);
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;

            e.Row.Cells[4].Text = TotalFormat(_sumByTime1);
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;

            e.Row.Cells[5].Text = TotalFormat(_sumByTime2);
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string dateFrom = txtDateFrom.Text.Trim();
            string dateTo = txtDateTo.Text.Trim();
            DateTime df, dt;
            df = sms.utils.StringUtil.ConvertToDateTime(dateFrom);
            dt = sms.utils.StringUtil.ConvertToDateTime(dateTo);
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            else
            {
                double DayDiff = (dt - df).TotalDays;
                if (df != DateTime.MinValue) dateFromSame = df.AddDays(-1 * DayDiff).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) dateToSame = dt.AddDays(-1 * DayDiff).ToString("dd/MM/yyyy");
            }
            List<Customers> lCustomers = new Customers().Customers_ReportByCustomerGroup(dateFrom, dateTo, dateFromSame, dateToSame);
            DataTable dataTable = ToDataTable(lCustomers);
            ExportToExcel(dataTable);
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

    private void ExportToExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BaoCaoTongHopDuLieuThanhVienKH.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        if (!string.IsNullOrWhiteSpace(txtDateFrom.Text) && !string.IsNullOrWhiteSpace(txtDateTo.Text))
        {
            HttpContext.Current.Response.Write(string.Format("<strong>Báo cáo tổng hợp dữ liệu thành viên/khách hàng từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text));
        }
        HttpContext.Current.Response.Write("<br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor='#5cd6ff'>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thành viên");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Đã kích hoạt");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Chưa kích hoạt");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Còn hạn");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Hết hạn");
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

    private DataTable ToDataTable(List<Customers> lCustomers)
    {
        DataTable dataTable = new DataTable(typeof(Docs).Name);
        if (lCustomers == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("CustomerName", typeof(string));
        dataTable.Columns.Add("SumByStatus1", typeof(int));
        dataTable.Columns.Add("SumByStatus2", typeof(int));
        dataTable.Columns.Add("SumByTime1", typeof(int));
        dataTable.Columns.Add("SumByTime2", typeof(int));
        for (var index = 0; index < lCustomers.Count; index++)
        {
            Customers customer = lCustomers[index];
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index + 1);
            rowAdded.SetField("CustomerName", customer.CustomerName);
            rowAdded.SetField("SumByStatus1", customer.SumByStatus1);
            rowAdded.SetField("SumByStatus2", customer.SumByStatus2);
            rowAdded.SetField("SumByTime1", customer.SumByTime1);
            rowAdded.SetField("SumByTime2", customer.SumByTime2);
            _sumByStatus1 += customer.SumByStatus1;
            _sumByStatus2 += customer.SumByStatus2;
            _sumByTime1 += customer.SumByTime1;
            _sumByTime2 += customer.SumByTime2;
        }
        dataTable.Rows.Add("Tổng", string.Empty, _sumByStatus1, _sumByStatus2, _sumByTime1, _sumByTime2);
        return dataTable;
    }

    private string TotalFormat(int total)
    {
        return total > 0 ? string.Format("{0:#,##}", total) : "0";
    }

    protected void ddlCompare_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SetTooltip();
    }

    protected void txtDateFrom_OnTextChanged(object sender, EventArgs e)
    {
        //SetTooltip();
    }

    protected void txtDateTo_OnTextChanged(object sender, EventArgs e)
    {
        //SetTooltip();
    }

    private void SetTooltip()
    {
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        DateTime df, dt;
        df = sms.utils.StringUtil.ConvertToDateTime(dateFrom);
        dt = sms.utils.StringUtil.ConvertToDateTime(dateTo);
        if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
        {
            if (df != DateTime.MinValue) dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
            if (dt != DateTime.MinValue) dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
        }
        else
        {
            double DayDiff = (dt - df).TotalDays;
            if (df != DateTime.MinValue) dateFromSame = df.AddDays(-1 * DayDiff).ToString("dd/MM/yyyy");
            if (dt != DateTime.MinValue) dateToSame = dt.AddDays(-1 * DayDiff).ToString("dd/MM/yyyy");
        }
        int compare = DateTime.Compare(dt, df);
        if (compare >= 0)
        {
            if (ddlCompare.SelectedValue.Equals("2"))
            {
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", dateFromSame, dateToSame);
            }
            else
            {
                var daysDiff = (dt - df).TotalDays;
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", dateFromSame, dateToSame);
            }
        }
        else ddlCompare.ToolTip = string.Empty;
    }
}