using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_Customers_ReportByServicePackage : System.Web.UI.Page
{
    private double SumByServicePackage = 0;
    private double TotalByServicePackage = 0;
    private double SumByServicePackage1 = 0;
    private double TotalByServicePackage1 = 0;
    private DateTime _dateTime = DateTime.Now;
    private int ActUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            txtDateFrom.Text = _dateTime.AddDays(-6).ToString("dd/MM/yyyy");
            txtDateTo.Text = _dateTime.ToString("dd/MM/yyyy");
            DropDownListHelpers.DDL_Bind(ddlServices, Services.Static_GetList(ActUserId, 0), "...");
            BindData();
        }
    }

    public void BindData()
    {
        try
        {
            SetTooltip();
            Customers m_Customers = new Customers();
            string DateFrom = txtDateFrom.Text.Trim();
            string DateTo = txtDateTo.Text.Trim();
            string dateFromSame ="", dateToSame ="";
                DateTime df, dt;
            df = sms.utils.StringUtil.ConvertToDateTime(DateFrom);
            dt = sms.utils.StringUtil.ConvertToDateTime(DateTo);
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
            string mOrderBy = ddlOrderBy.SelectedValue;
            DataSet list = m_Customers.Customers_ReportByServicePackageDataSet(DateFrom, DateTo, dateFromSame, dateToSame, mServiceId, mOrderBy);
            m_grid.DataSource = list.Tables[0];
            m_grid.AllowPaging = false;
            m_grid.DataBind();
            if (list.Tables[0].Rows.Count > 0)
            {
                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                m_grid.FooterRow.Cells[1].Text = "Tổng :";
                m_grid.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                m_grid.FooterRow.Cells[1].Font.Bold = true;
                m_grid.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                m_grid.FooterRow.Cells[2].Text = "";
                m_grid.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[2].Font.Bold = true;
                m_grid.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                m_grid.FooterRow.Cells[3].Text = "";
                m_grid.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[3].Font.Bold = true;
                m_grid.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[4].Text = SumByServicePackage.ToString("N0");
                m_grid.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[4].Font.Bold = true;
                m_grid.FooterRow.Cells[4].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[5].Text = SumByServicePackage1.ToString("N0");
                m_grid.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[5].Font.Bold = true;
                m_grid.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[6].Text = TotalByServicePackage.ToString("N0");
                m_grid.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[6].Font.Bold = true;
                m_grid.FooterRow.Cells[6].ForeColor = System.Drawing.Color.Red;

                m_grid.FooterRow.Cells[7].Text = TotalByServicePackage1.ToString("N0");
                m_grid.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                m_grid.FooterRow.Cells[7].Font.Bold = true;
                m_grid.FooterRow.Cells[7].ForeColor = System.Drawing.Color.Red;
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

            DataRowView m_ObjDataItem = (DataRowView)e.Row.DataItem;
            SumByServicePackage += int.Parse(m_ObjDataItem["SumByServicePackage"].ToString());
            TotalByServicePackage += int.Parse(m_ObjDataItem["TotalByServicePackage"].ToString());
            SumByServicePackage1 += int.Parse(m_ObjDataItem["SumByServicePackage1"].ToString());
            TotalByServicePackage1 += int.Parse(m_ObjDataItem["TotalByServicePackage1"].ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
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

    protected void ddlCompare_OnSelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BaoCao_TongHop_KHThueBao.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                m_grid.AllowPaging = false;
                this.BindData();

                string tieude = "Báo cáo tổng hợp khách hàng thuê bao từ ngày: " + txtDateFrom.Text + " đến " + txtDateTo.Text;
                m_grid.Caption = "<span style='color:Black;font-size:18px;'>" + tieude + "</span>";
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
    protected void ddlServicePackages_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}