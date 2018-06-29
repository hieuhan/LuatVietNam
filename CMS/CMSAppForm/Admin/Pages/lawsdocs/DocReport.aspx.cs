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
using System.Drawing;

public partial class Admin_Pages_lawsdocs_DocReport : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterReadLogId = 0;
    protected List<ServicePakages> l_ServicePakages = new List<ServicePakages>();
    private List<DocsReportByDocType> l_DocsReportByDocType = new List<DocsReportByDocType>();
    private List<DocsReportByField> l_DocsReportByField = new List<DocsReportByField>();
    private List<DocsReportByOrgan> l_DocsReportByOrgan = new List<DocsReportByOrgan>();
    DateTime now;
    int dayofweek, dayofmonth;
    decimal sumFooterValue = 0;
    int s_SumByStatus1 = 0, s_SumByStatus2 = 0, s_SumByStatus3 = 0, s_SumBySource1 = 0, s_SumBySource2 = 0,
        s_SumBySource3 = 0, s_SumByDownload1 = 0, s_SumByDownload2 = 0;
    private List<int> l_sum = new List<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        now = DateTime.Now;
        dayofweek = (int)now.DayOfWeek;
        dayofmonth = (int)DateTime.DaysInMonth(now.Year, now.Month);
        l_sum = new List<int>();
        l_sum.Add(s_SumByStatus1); l_sum.Add(s_SumByStatus2); l_sum.Add(s_SumByStatus3);
        l_sum.Add(s_SumBySource1); l_sum.Add(s_SumBySource2); l_sum.Add(s_SumBySource3); l_sum.Add(s_SumByDownload1); l_sum.Add(s_SumByDownload2);
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "Tất cả");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "Tất cả");
            DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "Tất cả");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "Tất cả");
            txtDateFrom.Text = now.AddDays(-7).ToString("dd/MM/yyyy");
            txtDateTo.Text = now.ToString("dd/MM/yyyy");
            SetTooltip();
            BindData();
        }
       
    }

    private void BindData()
    {
        try
        {
            string m_reportBy = ddlReportBy.SelectedValue;
            string m_reportBy_text = ddlReportBy.Text;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            DateTime DateFrom = sms.utils.StringUtil.ConvertToDateTime(m_DateFrom);
            DateTime DateTo = sms.utils.StringUtil.ConvertToDateTime(m_DateTo);
            double day = (DateTo - DateFrom).TotalDays;
            string Datefromsame = DateFrom.AddDays(-1 * day).ToString("dd/MM/yyyy");
            string Datetosame = DateTo.AddDays(-1 * day).ToString("dd/MM/yyyy");
            if (ddlOrderBy.SelectedValue == "1")
            {
                Datefromsame = DateFrom.AddYears(-1).ToString("dd/MM/yyyy");
                Datetosame = DateTo.AddYears(-1).ToString("dd/MM/yyyy");
            }
            
            short ReviewStatus = short.Parse(ddlReviewStatus.SelectedValue);
            short DataSources = short.Parse(ddlDataSources.SelectedValue);
            short DocGroups = short.Parse(ddlDocGroups.SelectedValue);
            switch (m_reportBy)
            {
                case "0":
                    {
                        ddlDocTypes.Enabled = false;
                        ddlOrgans.Enabled = true;
                        ddlFields.Enabled = true;
                        short Organs = short.Parse(ddlOrgans.SelectedValue);
                        short Fields = short.Parse(ddlFields.SelectedValue); 
                        DocsReportByDocType m_DocsReportByDocType = new DocsReportByDocType();
                        l_DocsReportByDocType = m_DocsReportByDocType.GetDocsReportDataByDocType(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, Organs, Fields);
                        m_grid_doctype.DataSource = l_DocsReportByDocType;
                        m_grid_doctype.DataBind();
                        tblbydoctype.Visible = true;
                        tblbyOrgan.Visible = false;
                        tblbyField.Visible = false;
                        lblTong.Text = l_DocsReportByDocType.Count.ToString();
                        m_grid_doctype.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_doctype.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_doctype.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++ )
                        {
                            m_grid_doctype.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_doctype.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_doctype.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }
                case "1":
                    {
                        ddlDocTypes.Enabled = true;
                        ddlOrgans.Enabled = false;
                        ddlFields.Enabled = true;
                        short DocTypes = short.Parse(ddlDocTypes.SelectedValue);
                        short Fields = short.Parse(ddlFields.SelectedValue); 
                        DocsReportByOrgan m_DocsReportByOrgan = new DocsReportByOrgan();
                        l_DocsReportByOrgan = m_DocsReportByOrgan.GetDocsReportDataByOrgan(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, DocTypes, Fields);
                        m_grid_Organ.DataSource = l_DocsReportByOrgan;
                        m_grid_Organ.DataBind();
                        tblbydoctype.Visible = false;
                        tblbyOrgan.Visible = true;
                        tblbyField.Visible = false;
                        lblTong.Text = l_DocsReportByOrgan.Count.ToString();
                        m_grid_Organ.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_Organ.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_Organ.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++)
                        {
                            m_grid_Organ.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_Organ.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_Organ.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }
                case "2":
                    {
                        ddlDocTypes.Enabled = true;
                        ddlOrgans.Enabled = true;
                        ddlFields.Enabled = false;
                        short DocTypes = short.Parse(ddlDocTypes.SelectedValue);
                        short Organs = short.Parse(ddlOrgans.SelectedValue); 
                        DocsReportByField m_DocsReportByField = new DocsReportByField();
                        l_DocsReportByField = m_DocsReportByField.GetDocsReportDataByField(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, DocTypes, Organs);
                        m_grid_field.DataSource = l_DocsReportByField;
                        m_grid_field.DataBind();
                        tblbydoctype.Visible = false;
                        tblbyOrgan.Visible = false;
                        tblbyField.Visible = true;
                        lblTong.Text = l_DocsReportByField.Count.ToString();
                        m_grid_field.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_field.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_field.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++)
                        {
                            m_grid_field.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_field.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_field.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }

            }

            //lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            //CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_doctype_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocsReportByDocType m_DataItem = (DocsReportByDocType)e.Row.DataItem;
            if (l_sum.Count > 7)
            {
                l_sum[0] += m_DataItem.SumByStatus1;
                l_sum[1] += m_DataItem.SumByStatus2;
                l_sum[2] += m_DataItem.SumByStatus3;
                l_sum[3] += m_DataItem.SumBySource1;
                l_sum[4] += m_DataItem.SumBySource2;
                l_sum[5] += m_DataItem.SumBySource3;
                l_sum[6] += m_DataItem.SumByDownload1;
                l_sum[7] += m_DataItem.SumByDownload2;
            }
        }

   }
    protected void m_grid_Field_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocsReportByField m_DataItem = (DocsReportByField)e.Row.DataItem;
            if (l_sum.Count > 7)
            {
                l_sum[0] += m_DataItem.SumByStatus1;
                l_sum[1] += m_DataItem.SumByStatus2;
                l_sum[2] += m_DataItem.SumByStatus3;
                l_sum[3] += m_DataItem.SumBySource1;
                l_sum[4] += m_DataItem.SumBySource2;
                l_sum[5] += m_DataItem.SumBySource3;
                l_sum[6] += m_DataItem.SumByDownload1;
                l_sum[7] += m_DataItem.SumByDownload2;
            }
        }

   }
    protected void m_grid_Organ_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocsReportByOrgan m_DataItem = (DocsReportByOrgan)e.Row.DataItem;
            if (l_sum.Count > 7)
            {
                l_sum[0] += m_DataItem.SumByStatus1;
                l_sum[1] += m_DataItem.SumByStatus2;
                l_sum[2] += m_DataItem.SumByStatus3;
                l_sum[3] += m_DataItem.SumBySource1;
                l_sum[4] += m_DataItem.SumBySource2;
                l_sum[5] += m_DataItem.SumBySource3;
                l_sum[6] += m_DataItem.SumByDownload1;
                l_sum[7] += m_DataItem.SumByDownload2; ;
            }
        }

    }
    
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewsletterReadLogId = int.Parse(m_grid_doctype.DataKeys[e.Row.RowIndex].Value.ToString());
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
        SetTooltip();
        BindData();
    }
    protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetTooltip();
        BindData();
    }
    protected void ddlDataSources_SelectedIndexChanged(object sender, EventArgs e)
    {
        string m_OrderBy = ddlOrderBy.SelectedValue;
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=TK_VB_QPPL.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                if(tblbydoctype.Visible == true)
            {
                //To Export all pages
                m_grid_doctype.AllowPaging = false;
                this.BindDataForExcel();

                m_grid_doctype.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in m_grid_doctype.HeaderRow.Cells)
                {
                    cell.BackColor = System.Drawing.Color.DodgerBlue;
                }
                foreach (GridViewRow row in m_grid_doctype.Rows)
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

                m_grid_doctype.RenderControl(hw);
                }else if(tblbyOrgan.Visible == true) {
                    //To Export all pages
                    m_grid_Organ.AllowPaging = false;
                    this.BindData();

                    m_grid_Organ.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in m_grid_Organ.HeaderRow.Cells)
                    {
                        cell.BackColor = System.Drawing.Color.DodgerBlue;
                    }
                    foreach (GridViewRow row in m_grid_Organ.Rows)
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

                    m_grid_Organ.RenderControl(hw);
                }
                else
                {
                    //To Export all pages
                    m_grid_field.AllowPaging = false;
                    this.BindData();

                    m_grid_field.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in m_grid_field.HeaderRow.Cells)
                    {
                        cell.BackColor = System.Drawing.Color.DodgerBlue;
                    }
                    foreach (GridViewRow row in m_grid_field.Rows)
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

                    m_grid_field.RenderControl(hw);
                }
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
    protected void ddlDocGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindData();
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
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
            string m_reportBy = ddlReportBy.SelectedValue;
            string m_reportBy_text = ddlReportBy.Text;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            DateTime DateFrom = sms.utils.StringUtil.ConvertToDateTime(m_DateFrom);
            DateTime DateTo = sms.utils.StringUtil.ConvertToDateTime(m_DateTo);
            double day = (DateTo - DateFrom).TotalDays;
            string Datefromsame = DateFrom.AddDays(-1 * day).ToString("dd/MM/yyyy");
            string Datetosame = DateTo.AddDays(-1 * day).ToString("dd/MM/yyyy");
            if (ddlOrderBy.SelectedValue == "1")
            {
                Datefromsame = DateFrom.AddYears(-1).ToString("dd/MM/yyyy");
                Datetosame = DateTo.AddYears(-1).ToString("dd/MM/yyyy");
            }

            short ReviewStatus = short.Parse(ddlReviewStatus.SelectedValue);
            short DataSources = short.Parse(ddlDataSources.SelectedValue);
            short DocGroups = short.Parse(ddlDocGroups.SelectedValue);
            switch (m_reportBy)
            {
                case "0":
                    {
                        ddlDocTypes.Enabled = false;
                        ddlOrgans.Enabled = true;
                        ddlFields.Enabled = true;
                        short Organs = short.Parse(ddlOrgans.SelectedValue);
                        short Fields = short.Parse(ddlFields.SelectedValue);
                        DocsReportByDocType m_DocsReportByDocType = new DocsReportByDocType();
                        l_DocsReportByDocType = m_DocsReportByDocType.GetDocsReportDataByDocType(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, Organs, Fields);
                        m_grid_doctype.DataSource = l_DocsReportByDocType;
                        m_grid_doctype.DataBind();
                        string tieude = "Báo cáo thống kê văn bản quy phạm pháp luật từ ngày: " + DateFrom.ToString("dd/MM/yyyy") + " đến " + DateTo.ToString("dd/MM/yyyy");
                        m_grid_doctype.Caption = "<span style='font-weight: bold;font-size:18px;'>" + tieude + "</span>";
                        tblbydoctype.Visible = true;
                        tblbyOrgan.Visible = false;
                        tblbyField.Visible = false;
                        lblTong.Text = l_DocsReportByDocType.Count.ToString();
                        m_grid_doctype.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_doctype.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_doctype.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++)
                        {
                            m_grid_doctype.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_doctype.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_doctype.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }
                case "1":
                    {
                        ddlDocTypes.Enabled = true;
                        ddlOrgans.Enabled = false;
                        ddlFields.Enabled = true;
                        short DocTypes = short.Parse(ddlDocTypes.SelectedValue);
                        short Fields = short.Parse(ddlFields.SelectedValue);
                        DocsReportByOrgan m_DocsReportByOrgan = new DocsReportByOrgan();
                        l_DocsReportByOrgan = m_DocsReportByOrgan.GetDocsReportDataByOrgan(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, DocTypes, Fields);
                        m_grid_Organ.DataSource = l_DocsReportByOrgan;
                        m_grid_Organ.DataBind();
                        string tieude = "Báo cáo thống kê văn bản quy phạm pháp luật từ ngày: " + DateFrom + " đến " + DateTo;
                        m_grid_Organ.Caption = "<span style='font-weight: bold;font-size:18px;'>" + tieude + "</span>";
                        tblbydoctype.Visible = false;
                        tblbyOrgan.Visible = true;
                        tblbyField.Visible = false;
                        lblTong.Text = l_DocsReportByOrgan.Count.ToString();
                        m_grid_Organ.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_Organ.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_Organ.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++)
                        {
                            m_grid_Organ.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_Organ.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_Organ.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }
                case "2":
                    {
                        ddlDocTypes.Enabled = true;
                        ddlOrgans.Enabled = true;
                        ddlFields.Enabled = false;
                        short DocTypes = short.Parse(ddlDocTypes.SelectedValue);
                        short Organs = short.Parse(ddlOrgans.SelectedValue);
                        DocsReportByField m_DocsReportByField = new DocsReportByField();
                        l_DocsReportByField = m_DocsReportByField.GetDocsReportDataByField(m_DateFrom, m_DateTo, Datefromsame, Datetosame, ReviewStatus, DataSources, DocGroups, DocTypes, Organs);
                        m_grid_field.DataSource = l_DocsReportByField;
                        m_grid_field.DataBind();
                        string tieude = "Báo cáo thống kê văn bản quy phạm pháp luật từ ngày: " + DateFrom + " đến " + DateTo;
                        m_grid_field.Caption = "<span style='font-weight: bold;font-size:18px;'>" + tieude + "</span>";
                        tblbydoctype.Visible = false;
                        tblbyOrgan.Visible = false;
                        tblbyField.Visible = true;
                        lblTong.Text = l_DocsReportByField.Count.ToString();
                        m_grid_field.FooterRow.Cells[1].Text = "Tổng";
                        m_grid_field.FooterRow.Cells[1].Font.Bold = true;
                        m_grid_field.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                        for (int i = 0; i < l_sum.Count; i++)
                        {
                            m_grid_field.FooterRow.Cells[i + 2].Text = l_sum[i].ToString();
                            m_grid_field.FooterRow.Cells[i + 2].Font.Bold = true;
                            m_grid_field.FooterRow.Cells[i + 2].ForeColor = System.Drawing.Color.Red;
                        }
                        break;
                    }

            }
            
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}