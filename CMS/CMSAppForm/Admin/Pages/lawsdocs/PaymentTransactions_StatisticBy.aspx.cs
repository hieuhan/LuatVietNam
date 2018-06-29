using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using System.IO;
public partial class Admin_PaymentTransactions_StatisticBy : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected string TitleExcel = "ThongKe";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            //DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
            //DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
            DropDownListHelpers.DDL_Bind(ddlPaymentTypes, ICSoft.LawDocsLib.PaymentTypes.Static_GetListOrderBy("PaymentTypeName"), "...");
            DropDownListHelpers.DDL_Bind(ddlTransactionStatus, TransactionStatus.Static_GetListOrderBy("TransactionStatusName"), "...", "1");
            DropDownListHelpers.DDL_Bind(ddlTranType, TransactionTypes.Static_GetList(), "...");
            Partners m_Partners = new Partners();
            int RowCount = 0;
            DropDownListHelpers.DDL_Bind(ddlPartner, m_Partners.Search(ActUserId, "PartnerDesc", "", "", "", ref RowCount), "...");
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            int TotalCount = 0;
            Int64 TotalMoney = 0;
            DataSet ds;
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            short m_ServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            byte m_TransactionStatusId = byte.Parse(ddlTransactionStatus.SelectedValue);
            byte m_PaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            int m_ApplicationPlatformId = int.Parse(ddlPSC.SelectedValue);
            short m_BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue); ;
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            m_PaymentTransactions.SiteId = short.Parse(ddlSite.SelectedValue);
            m_PaymentTransactions.TransactionTypeId = byte.Parse(ddlTranType.SelectedValue);
            m_PaymentTransactions.BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue);
            switch (ddlSearchBy.SelectedValue)
            {
                case "CrDateTime":
                    //thống kê theo ngày tạo
                    TitleExcel = "Thongke_TheoNgayTao";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByCrDateTime(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId, 
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId , DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
                case "Month":
                    //Thống kê theo tháng
                    TitleExcel = "Thongke_TheoThang";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByMonth(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
                case "PaymentTypeId":
                    //Thống kê theo loại thanh toán
                    TitleExcel = "Thongke_TheoKenhThanhToan";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByPaymentTypeId(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
                case "TransactionTypeId":
                    //Thống kê theo loại thanh toán
                    TitleExcel = "Thongke_TheoLoaiGiaoDich";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByTransactionTypeId(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
                case "DataTypeId":
                    //Thống kê theo loại thanh toán
                    TitleExcel = "Thongke_TheoLoaiDuLieu";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByDataTypeId(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
                case "ServiceId":
                    //Thống kê theo dịch vụ
                    TitleExcel = "Thongke_TheoDichVu";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByServiceId(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;   
                default:
                    //Thống kê theo gói dịch vụ
                    TitleExcel = "Thongke_TheoGoiDichVu";
                    ds = m_PaymentTransactions.PaymentTransactions_StatisticByServicePackageId(ActUserId, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_PaymentTypeId,
                                                                                            m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, DateFrom,
                                                                                            DateTo, ref TotalCount, ref TotalMoney);
                    break;
            }
            m_grid.DataSource = ds;
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", TotalCount) != "" ? string.Format("{0:#,#}", TotalCount) : "0") +" giao dịch";
            lblTongTien.Text = (string.Format("{0:#,#}", TotalMoney) != "" ? string.Format("{0:#,#}", TotalMoney) : "0") + " vnđ";
            lblTongTienVAT.Text = (string.Format("{0:#,#}", TotalMoney) != "" ? string.Format("{0:#,#}", TotalMoney - (TotalMoney / 11)) : "0") + " vnđ";
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Start Header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                e.Row.Cells[0].Text = "Ngày tạo";
                e.Row.Cells[1].Text = "Tổng giao dịch";
                e.Row.Cells[2].Text = "Tổng tiền(VNĐ)";
            }

            if (ddlSearchBy.SelectedValue == "Month")
            {
                e.Row.Cells[0].Text = "Tháng";
                e.Row.Cells[1].Text = "Năm";
                e.Row.Cells[2].Text = "Tổng giao dịch";
                e.Row.Cells[3].Text = "Tổng tiền(VNĐ)";
            }
            if (ddlSearchBy.SelectedValue == "PaymentTypeId")
            {
                e.Row.Cells[0].Text = "Kênh thanh toán";
                e.Row.Cells[1].Text = "Số giao dịch";
                e.Row.Cells[2].Text = "Tổng số Account";
                e.Row.Cells[3].Text = "Tổng tiền(VNĐ)";
                e.Row.Cells[4].Text = "Tổng tiền chưa có VAT(VNĐ)";
            }

            if (ddlSearchBy.SelectedValue == "TransactionTypeId")
            {
                e.Row.Cells[0].Text = "Loại giao dịch";
                e.Row.Cells[1].Text = "Số giao dịch";
                e.Row.Cells[2].Text = "Tổng tiền(VNĐ)";
            }

            if (ddlSearchBy.SelectedValue == "DataTypeId")
            {
                e.Row.Cells[0].Text = "Loại dữ liệu";
                e.Row.Cells[1].Text = "Số giao dịch";
                e.Row.Cells[2].Text = "Tổng tiền(VNĐ)";
            }

            if (ddlSearchBy.SelectedValue == "ServiceId")
            {
                e.Row.Cells[0].Text = "Dịch vụ";
                e.Row.Cells[1].Text = "Số giao dịch";
                e.Row.Cells[2].Text = "Tổng số Account";
                e.Row.Cells[3].Text = "Tổng tiền(VNĐ)";
                e.Row.Cells[4].Text = "Tổng tiền chưa có VAT(VNĐ)";
            }

            if (ddlSearchBy.SelectedValue == "ServicePackageId")
            {
                e.Row.Cells[0].Text = "Dịch vụ";
                e.Row.Cells[1].Text = "Gói dịch vụ";
                e.Row.Cells[2].Text = "Số giao dịch";
                e.Row.Cells[3].Text = "Tổng số Account";
                e.Row.Cells[4].Text = "Tổng tiền(VNĐ)";
                e.Row.Cells[5].Text = "Tổng tiền chưa có VAT(VNĐ)";
            }

        }


        // End Header

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = ((DateTime)
                row[0]).ToString("dd/MM/yyyy");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,0}", row[1]).ToString()).Replace(".", ",");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = ((string)string.Format("{0:#,0}", row[2]).ToString()).Replace(".",",");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "Month")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                
                e.Row.Cells[2].Text = ((string)
                string.Format("{0:#,0}", row[2]).ToString()).Replace(".", ",");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Text = ((string)
                string.Format("{0:#,0}", row[3]).ToString()).Replace(".", ",");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }

            if (ddlSearchBy.SelectedValue == "PaymentTypeId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,0}", row[1]).ToString()).Replace(".", ",");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = ((string)
                string.Format("{0:#,0}", row[2]).ToString()).Replace(".", ",");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
           
                e.Row.Cells[3].Text = ((string)
                string.Format("{0:#,0}", row[3]).ToString()).Replace(".", ",");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[4].Text = ((string)
                string.Format("{0:#,0}", row[4]).ToString()).Replace(".", ",");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "ServiceId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,0}", row[1]).ToString()).Replace(".", ",");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = ((string)
                string.Format("{0:#,0}", row[2]).ToString()).Replace(".", ",");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Text = ((string)
                string.Format("{0:#,0}", row[3]).ToString()).Replace(".", ",");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[4].Text = ((string)
                string.Format("{0:#,0}", row[4]).ToString()).Replace(".", ",");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }

            if (ddlSearchBy.SelectedValue == "ServicePackageId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,0}", row[1]).ToString()).Replace(".", ",");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = ((string)
                string.Format("{0:#,0}", row[2]).ToString()).Replace(".", ",");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Text = ((string)
                string.Format("{0:#,0}", row[3]).ToString()).Replace(".", ",");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[4].Text = ((string)
                string.Format("{0:#,0}", row[4]).ToString()).Replace(".", ",");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[5].Text = ((string)
                string.Format("{0:#,0}", row[5]).ToString()).Replace(".", ",");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            }

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
           DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
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
   protected void ddlTransactionStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlPaymentTypes_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
   {
       DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
       DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
       BindData();
   }
   protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlPSC_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void btnXuatExcel_Click(object sender, EventArgs e)
   {

       try
       {
           switch (ddlSearchBy.SelectedValue)
           {
               case "CrDateTime":
                   TitleExcel = "Thongke_TheoNgayTao";
                   break;
               case "Month":
                   TitleExcel = "Thongke_TheoThang";
                   break;
               case "PaymentTypeId":
                   TitleExcel = "Thongke_TheoKenhThanhToan";
                   break;
               case "TransactionTypeId":
                   TitleExcel = "Thongke_TheoLoaiGiaoDich";
                   break;
               case "DataTypeId":
                   TitleExcel = "Thongke_TheoLoaiDuLieu";
                   break;
               case "ServiceId":
                   TitleExcel = "Thongke_TheoDichVu";
                   break;
               default:
                   TitleExcel = "Thongke_TheoGoiDichVu";
                   break;
           }
           Response.Clear();
           Response.Buffer = true;
           Response.AddHeader("content-disposition", "attachment;filename="+TitleExcel+".xls");
           Response.Charset = "";
           Response.ContentType = "application/vnd.ms-excel";
           using (StringWriter sw = new StringWriter())
           {
               HtmlTextWriter hw = new HtmlTextWriter(sw);

               //To Export all pages
               m_grid.AllowPaging = false;
               this.BindData();

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
}

