using System.Data;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_Pages_lawsdocs_Articles_ReportData : System.Web.UI.Page
{
    private int SumByStatus1 = 0, SumByStatus2 = 0, SumByStatus3 = 0;
    private int SumBySource1 = 0, SumBySource2 = 0;
    private int SumByView1 = 0, SumByView2 = 0;
    private string DateFromSame, DateToSame;
    DateTime now;

    protected void Page_Load(object sender, EventArgs e)
    {
        now = DateTime.Now;
        if (!IsPostBack)
        {
            txtDateFrom.Text = now.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");
            List<OrderByClauses> l_OrderByClauses = new List<OrderByClauses>();
            OrderByClauses m_OrderByClauses = new OrderByClauses();
            m_OrderByClauses.OrderByDesc = "Kỳ trước"; m_OrderByClauses.OrderBy = "0";
            l_OrderByClauses.Add(m_OrderByClauses);
            OrderByClauses m_OrderByClauses1 = new OrderByClauses();
            m_OrderByClauses1.OrderByDesc = "Năm trước"; m_OrderByClauses1.OrderBy = "1";
            l_OrderByClauses.Add(m_OrderByClauses1);
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, l_OrderByClauses, "");
            SetTooltip();
            BindData();
        }
    }

    public void BindData()
    {
        try
        {
            Articles m_Articles = new Articles();
            string DateFrom = txtDateFrom.Text.Trim();
            string DateTo = txtDateTo.Text.Trim();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            if (m_OrderBy.Equals("0"))
            {
                double day = (DateTime.Parse(DateTo).Date - DateTime.Parse(DateFrom).Date).TotalDays;
                DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddDays(-day).ToString("dd/MM/yyyy");
                DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddDays(-1).ToString("dd/MM/yyyy");
            }
            else
            {
                DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddYears(-1).ToString("dd/MM/yyyy");
                DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddYears(-1).ToString("dd/MM/yyyy");
            }
            List<Articles> lArticles = m_Articles.Articles_ReportData(DateFrom, DateTo, DateFromSame, DateToSame);
            
            m_grid.DataSource = lArticles;
            m_grid.AllowPaging = false;
            m_grid.DataBind();
            if (lArticles.Count > 0)
            {
                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;

                m_grid.FooterRow.Cells[1].Text = "Tổng :";
                m_grid.FooterRow.Cells[1].Font.Bold = true;
                m_grid.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[2].Text = SumByStatus1.ToString();
                m_grid.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[2].Font.Bold = true;
                m_grid.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[3].Text = SumByStatus2.ToString();
                m_grid.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[3].Font.Bold = true;
                m_grid.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[4].Text = SumByStatus3.ToString();
                m_grid.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[4].Font.Bold = true;
                m_grid.FooterRow.Cells[4].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[5].Text = SumBySource1.ToString();
                m_grid.FooterRow.Cells[5].Font.Bold = true;
                m_grid.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[6].Text = SumBySource2.ToString();
                m_grid.FooterRow.Cells[6].Font.Bold = true;
                m_grid.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[6].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[7].Text = SumByView1.ToString();
                m_grid.FooterRow.Cells[7].Font.Bold = true;
                m_grid.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[7].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[8].Text = SumByView2.ToString();
                m_grid.FooterRow.Cells[8].Font.Bold = true;
                m_grid.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[8].ForeColor = System.Drawing.Color.Red;
            }
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
            Articles m_Articles = (Articles)e.Row.DataItem;
            SumByStatus1 += m_Articles.SumByStatus1;
            SumByStatus2 += m_Articles.SumByStatus2;
            SumByStatus3 += m_Articles.SumByStatus3;
            SumBySource1 += m_Articles.SumBySource1;
            SumBySource2 += m_Articles.SumBySource2;
            SumByView1 += m_Articles.SumByView1;
            SumByView2 += m_Articles.SumByView2;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckDate();
        SetTooltip();
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string m_OrderBy = ddlOrderBy.SelectedValue;
        //if (m_OrderBy.Equals("0"))
        //{
        //    DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.AddDays(-6).ToString("dd/MM/yyyy") : txtDateFrom.Text).AddDays(-6).ToString("dd/MM/yyyy");
        //    DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddDays(-6).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddYears(-1).ToString("dd/MM/yyyy");
        //    DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddYears(-1).ToString("dd/MM/yyyy");
        //}

        //BindData();
        SetTooltip();
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=TongHopDuLieuTin.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                m_grid.AllowPaging = false;
                this.BindDataForExcel();

                    m_grid.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in m_grid.HeaderRow.Cells)
                    {
                        cell.BackColor = System.Drawing.Color.DodgerBlue;
                    }
                    foreach (GridViewRow row in m_grid.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        row.BorderColor = System.Drawing.Color.Black;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = System.Drawing.Color.Ivory;
                                cell.BorderColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                cell.BackColor = System.Drawing.Color.Gainsboro;
                                cell.BorderColor = System.Drawing.Color.Black;
                            }
                        }
                    }

                    m_grid.RenderControl(hw);
                
                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
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
            if (ddlOrderBy.SelectedValue.Equals("1"))
            {
                ddlOrderBy.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddYears(-1),
                    dt.AddYears(-1));
            }
            else
            {
                var daysDiff = (dt - df).TotalDays;
                ddlOrderBy.ToolTip = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", df.AddDays(-daysDiff - 1),
                    df.AddDays(-1));
            }
        }
        else ddlOrderBy.ToolTip = string.Empty;
    }
    private void BindDataForExcel()
    {
        try
        {
            Articles m_Articles = new Articles();
            string DateFrom = txtDateFrom.Text.Trim();
            string DateTo = txtDateTo.Text.Trim();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            if (m_OrderBy.Equals("0"))
            {
                double day = (DateTime.Parse(DateTo).Date - DateTime.Parse(DateFrom).Date).TotalDays;
                DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddDays(-day).ToString("dd/MM/yyyy");
                DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddDays(-1).ToString("dd/MM/yyyy");
            }
            else
            {
                DateFromSame = DateTime.Parse(txtDateFrom.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateFrom.Text).AddYears(-1).ToString("dd/MM/yyyy");
                DateToSame = DateTime.Parse(txtDateTo.Text == "" ? now.ToString("dd/MM/yyyy") : txtDateTo.Text).AddYears(-1).ToString("dd/MM/yyyy");
            }
            List<Articles> lArticles = m_Articles.Articles_ReportData(DateFrom, DateTo, DateFromSame, DateToSame);
            m_grid.DataSource = lArticles;
            m_grid.DataBind();
            string tieude= "Báo cáo khách hàng thuê bao từ ngày: " + DateFrom + " đến " + DateTo;
            m_grid.Caption = "<span style='font-weight: bold;font-size:18px;'>" + tieude + "</span>";
            if (lArticles.Count > 0)
            {
                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;

                m_grid.FooterRow.Cells[1].Text = "Tổng :";
                m_grid.FooterRow.Cells[1].Font.Bold = true;
                m_grid.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[2].Text = SumByStatus1.ToString();
                m_grid.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[2].Font.Bold = true;
                m_grid.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[3].Text = SumByStatus2.ToString();
                m_grid.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[3].Font.Bold = true;
                m_grid.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[4].Text = SumByStatus3.ToString();
                m_grid.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[4].Font.Bold = true;
                m_grid.FooterRow.Cells[4].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[5].Text = SumBySource1.ToString();
                m_grid.FooterRow.Cells[5].Font.Bold = true;
                m_grid.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[6].Text = SumBySource2.ToString();
                m_grid.FooterRow.Cells[6].Font.Bold = true;
                m_grid.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[6].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[7].Text = SumByView1.ToString();
                m_grid.FooterRow.Cells[7].Font.Bold = true;
                m_grid.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[7].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[8].Text = SumByView2.ToString();
                m_grid.FooterRow.Cells[8].Font.Bold = true;
                m_grid.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                m_grid.FooterRow.Cells[8].ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}