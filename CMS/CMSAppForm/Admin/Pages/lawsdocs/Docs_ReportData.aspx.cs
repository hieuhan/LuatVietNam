using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;

public partial class Admin_Pages_Docs_ReportData : System.Web.UI.Page
{
    private int _sumByStatus1 = 0, _sumByStatus2 = 0, _sumByStatus3 = 0;
    private int _sumBySource1 = 0, _sumBySource2 = 0;
    private int _sumByDownload1 = 0, _sumByDownload2 = 0;
    private int _sumByView1 = 0, _sumByView2 = 0;
    private DateTime _dateTime = DateTime.Now;
    private string _dateFromSame, _dateToSame;

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
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                DateTime df, dt;
                DateTime.TryParse(dateFrom, out df);
                DateTime.TryParse(dateTo, out dt);
                if (df != DateTime.MinValue) _dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) _dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            m_grid.DataSource = new Docs().Docs_ReportData(dateFrom, dateTo, _dateFromSame, _dateToSame);
            m_grid.AllowPaging = false;
            m_grid.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                Docs mDoc = (Docs)e.Row.DataItem;
                _sumByStatus1 += mDoc.SumByStatus1;
                _sumByStatus2 += mDoc.SumByStatus2;
                _sumByStatus3 += mDoc.SumByStatus3;
                _sumBySource1 += mDoc.SumBySource1;
                _sumBySource2 += mDoc.SumBySource2;
                _sumByView1 += mDoc.SumByView1;
                _sumByView2 += mDoc.SumByView2;
                _sumByDownload1 += mDoc.SumByDownload1;
                _sumByDownload2 += mDoc.SumByDownload2;
                break;
            case DataControlRowType.Footer:
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

                e.Row.Cells[4].Text = TotalFormat(_sumByStatus3);
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[5].Text = TotalFormat(_sumBySource1);
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[6].Text = TotalFormat(_sumBySource2);
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[7].Text = TotalFormat(_sumByDownload1);
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[8].Text = TotalFormat(_sumByDownload2);
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[9].Text = TotalFormat(_sumByView1);
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[10].Text = TotalFormat(_sumByView2);
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].Font.Bold = true;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                break;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetTooltip();
        BindData();
    }

    private string TotalFormat(int total)
    {
        return total > 0 ? string.Format("{0:#,##}", total) : "0";
    }

    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string dateFrom = txtDateFrom.Text.Trim();
            string dateTo = txtDateTo.Text.Trim();
            if (ddlCompare.SelectedValue.Equals("2")) //cùng kỳ năm trước
            {
                DateTime df, dt;
                DateTime.TryParse(dateFrom, out df);
                DateTime.TryParse(dateTo, out dt);
                if (df != DateTime.MinValue) _dateFromSame = df.AddYears(-1).ToString("dd/MM/yyyy");
                if (dt != DateTime.MinValue) _dateToSame = dt.AddYears(-1).ToString("dd/MM/yyyy");
            }
            List<Docs> lDocs = new Docs().Docs_ReportData(dateFrom, dateTo, _dateFromSame, _dateToSame);
            DataTable dataTable = ToDataTable(lDocs);
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
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BaoCaoTongHopDuLieuVanBan.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        if (!string.IsNullOrWhiteSpace(txtDateFrom.Text) && !string.IsNullOrWhiteSpace(txtDateTo.Text))
        {
            HttpContext.Current.Response.Write(string.Format("<strong>Báo cáo tổng hợp dữ liệu văn bản từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text));
        }
        HttpContext.Current.Response.Write("</ br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor=#5cd6ff>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Văn bản");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Đã duyệt");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Chờ duyệt");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tác động thay đổi");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Nguồn LVN");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Nguồn cộng tác viên");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lượt tải");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lượt tải cùng kỳ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lượt xem");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Lượt xem cùng kỳ");
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
        if (lDocs == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("DocName", typeof(string));
        dataTable.Columns.Add("SumByStatus1", typeof(int));
        dataTable.Columns.Add("SumByStatus2", typeof(int));
        dataTable.Columns.Add("SumByStatus3", typeof(int));
        dataTable.Columns.Add("SumBySource1", typeof(int));
        dataTable.Columns.Add("SumBySource2", typeof(int));
        dataTable.Columns.Add("SumByDownload1", typeof(int));
        dataTable.Columns.Add("SumByDownload2", typeof(int));
        dataTable.Columns.Add("SumByView1", typeof(int));
        dataTable.Columns.Add("SumByView2", typeof(int));
        for (var index = 0; index < lDocs.Count; index++)
        {
            Docs doc = lDocs[index];
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index + 1);
            rowAdded.SetField("DocName", doc.DocName);
            rowAdded.SetField("SumByStatus1", doc.SumByStatus1);
            rowAdded.SetField("SumByStatus2",doc.SumByStatus2);
            rowAdded.SetField("SumByStatus3", doc.SumByStatus3);
            rowAdded.SetField("SumBySource1", doc.SumBySource1);
            rowAdded.SetField("SumBySource2", doc.SumBySource2);
            rowAdded.SetField("SumByDownload1", doc.SumByDownload1);
            rowAdded.SetField("SumByDownload2", doc.SumByDownload2);
            rowAdded.SetField("SumByView1", doc.SumByView1);
            rowAdded.SetField("SumByView2", doc.SumByView2);
            _sumByStatus1 += doc.SumByStatus1;
            _sumByStatus2 += doc.SumByStatus2;
            _sumByStatus3 += doc.SumByStatus3;
            _sumBySource1 += doc.SumBySource1;
            _sumBySource2 += doc.SumBySource2;
            _sumByDownload1 += doc.SumByDownload1;
            _sumByDownload2 += doc.SumByDownload2;
            _sumByView1 += doc.SumByView1;
            _sumByView2 += doc.SumByView2;
        }
        dataTable.Rows.Add("Tổng", string.Empty, _sumByStatus1, _sumByStatus2, _sumByStatus3, _sumBySource1, _sumBySource2, _sumByDownload1, _sumByDownload2, _sumByView1, _sumByView2);
        return dataTable;
    }

    protected void ddlCompare_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SetTooltip();
        BindData();
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
        txtDateFrom.Text = !string.IsNullOrWhiteSpace(txtDateFrom.Text)
            ? txtDateFrom.Text
            : DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
        txtDateTo.Text = !string.IsNullOrWhiteSpace(txtDateTo.Text)
            ? txtDateTo.Text
            : DateTime.Now.ToString("dd/MM/yyyy");
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        DateTime df, dt;
        DateTime.TryParse(dateFrom, out df);
        DateTime.TryParse(dateTo, out dt);
        ddlCompare.ToolTip = string.Empty;
        int compare = DateTime.Compare(dt, df);
        if (compare >= 0)
        {
            if (ddlCompare.SelectedValue.Equals("2"))
            {
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddYears(-1),
                    dt.AddYears(-1));
            }
            else
            {
                var daysDiff = (dt - df).TotalDays;
                ddlCompare.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}",
                    df.AddDays(-daysDiff - 1),
                    df.AddDays(-1));
            }
        }
        else JSAlertHelpers.Alert("Ngày chọn không hợp lệ", this);
    }
}