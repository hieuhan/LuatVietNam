using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using System.Text;
using System.IO;
using System.Drawing;
public partial class Admin_PaymentTransactionsForPartner : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int PaymentTransactionId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected List<AccountTypes> l_AccountTypes = new List<AccountTypes>();
    protected List<TransactionStatus> l_TransactionStatus = new List<TransactionStatus>();
    protected List<ICSoft.LawDocsLib.PaymentTypes> l_PaymentTypes = new List<ICSoft.LawDocsLib.PaymentTypes>();
    protected List<ICSoft.LawDocsLib.TransactionTypes> l_TransactionTypes = new List<ICSoft.LawDocsLib.TransactionTypes>();
    private int RowAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            //DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId,short.Parse(ddlSite.SelectedValue)), "...");
            //DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
            DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
            DropDownListHelpers.DDLPaymentTypes_BindByCulture(ddlPaymentTypes, ICSoft.LawDocsLib.PaymentTypes.Static_GetListOrderBy("PaymentTypeName"), "...");
            DropDownListHelpers.DDLTransactionStatus_BindByCulture(ddlTransactionStatus, TransactionStatus.Static_GetListOrderBy("TransactionStatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("PaymentTransactions"), "");
            DropDownListHelpers.DDL_Bind(ddlTranType, TransactionTypes.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlAccountType, AccountTypes.Static_GetList(), "...");
            Partners m_Partners = new Partners();
            int RowCount = 0;
            DropDownListHelpers.DDL_Bind(ddlPartner, m_Partners.Search(ActUserId, "PartnerDesc", "", "", "", ref RowCount), "...");
            txtDateFrom.Text = DateTime.Now.AddDays((DateTime.Now.Day * -1) + 1).ToString("dd/MM/yyyy");

            if (Session["PaymentTransactions-ddlServices"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("CustomersEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("ServicePackagesEdit.aspx")))
            {
                ddlServices.SelectedValue = Session["PaymentTransactions-ddlServices"].ToString();
                ddlOrderBy.SelectedValue = Session["PaymentTransactions-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["PaymentTransactions-ddlSite"].ToString();
                ddlServicePackages.SelectedValue = Session["PaymentTransactions-ddlServicePackages"].ToString();
                ddlPaymentTypes.SelectedValue = Session["PaymentTransactions-ddlPaymentTypes"].ToString();
                ddlTransactionStatus.SelectedValue = Session["PaymentTransactions-ddlTransactionStatus"].ToString();
                ddlTranType.SelectedValue = Session["PaymentTransactions-ddlTranType"].ToString();
                ddlAccountType.SelectedValue = Session["PaymentTransactions-ddlAccountType"].ToString();
                ddlPartner.SelectedValue = Session["PaymentTransactions-ddlPartner"].ToString();
                ddlPlusSub.SelectedValue = Session["PaymentTransactions-ddlPlusSub"].ToString();
                txtDateFrom.Text = Session["PaymentTransactions-txtDateFrom"].ToString();
                txtDateTo.Text = Session["PaymentTransactions-txtDateTo"].ToString();
                txtSearch.Text = Session["PaymentTransactions-txtSearch"].ToString();
            }
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    protected void SetCurentData()
    {
        Session["PaymentTransactions-ddlServices"] = ddlServices.SelectedValue;
        Session["PaymentTransactions-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["PaymentTransactions-ddlSite"] = ddlSite.SelectedValue;
        Session["PaymentTransactions-ddlServicePackages"] = ddlServicePackages.SelectedValue;
        Session["PaymentTransactions-ddlPaymentTypes"] = ddlPaymentTypes.SelectedValue;
        Session["PaymentTransactions-ddlTransactionStatus"] = ddlTransactionStatus.SelectedValue;
        Session["PaymentTransactions-ddlTranType"] = ddlTranType.SelectedValue;
        Session["PaymentTransactions-ddlAccountType"] = ddlAccountType.SelectedValue;
        Session["PaymentTransactions-ddlPartner"] = ddlPartner.SelectedValue;
        Session["PaymentTransactions-ddlPlusSub"] = ddlPlusSub.SelectedValue;
        Session["PaymentTransactions-txtDateFrom"] = txtDateFrom.Text;
        Session["PaymentTransactions-txtDateTo"] = txtDateTo.Text;
        Session["PaymentTransactions-txtSearch"] = txtSearch.Text;
    }
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            decimal TotalMoney = 0;
            int TotalCustomers = 0;
            l_TransactionStatus = TransactionStatus.Static_GetList();
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_CustomerName = txtSearch.Text.Trim();
            int m_CustomerId = 0;
            short m_ServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            byte m_TransactionStatusId = 1;// Trạng thái giao dịch là thành công
            string m_TransactionDesc = "";
            string m_TransactionCode = txtTransactionCode.Text.Trim();
            byte m_PaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            int m_ApplicationPlatformId = 0;
            short m_BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue);
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            int m_CrUserId = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_PaymentTransactions.SiteId = short.Parse(ddlSite.SelectedValue);
            m_PaymentTransactions.TransactionTypeId = byte.Parse(ddlTranType.SelectedValue);
            m_PaymentTransactions.BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue);
            m_PaymentTransactions.AccountTypeId = byte.Parse(ddlAccountType.SelectedValue);
            m_PaymentTransactions.PlusSub = ddlPlusSub.SelectedValue;
            //m_grid.DataSource = m_PaymentTransactions.GetPage_ForParter(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId,
            //                                        m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId,
            //                                        m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId,0, m_DateFrom,
            //                                        m_DateTo, ref RowCount, ref TotalMoney);

            m_grid.DataSource = m_PaymentTransactions.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId,
                                                        m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId,
                                                        m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId, m_DateFrom, 
                                                        m_DateTo, ref RowCount, ref TotalMoney, ref TotalCustomers); 
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            RowAmount = RowCount;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            lblTotalMoneys.Text =(string.Format("{0:#,#}", TotalMoney)!="" ? string.Format("{0:#,#}", TotalMoney) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void BindDataForExcel()
    {
        try
        {
            int RowCount = 0;
            decimal TotalMoney = 0;
            l_TransactionStatus = TransactionStatus.Static_GetList();
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_CustomerName = txtSearch.Text.Trim();
            int m_CustomerId = 0;
            short m_ServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            byte m_TransactionStatusId = byte.Parse(ddlTransactionStatus.SelectedValue);
            string m_TransactionDesc = "";
            string m_TransactionCode = txtTransactionCode.Text.Trim();
            byte m_PaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            int m_ApplicationPlatformId = 0;
            short m_BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue);
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            int m_CrUserId = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_PaymentTransactions.SiteId = short.Parse(ddlSite.SelectedValue);
            m_PaymentTransactions.TransactionTypeId = byte.Parse(ddlTranType.SelectedValue);
            m_PaymentTransactions.BusinessPartnerId = byte.Parse(ddlPartner.SelectedValue);
            m_PaymentTransactions.AccountTypeId = byte.Parse(ddlAccountType.SelectedValue);
            m_PaymentTransactions.PlusSub = ddlPlusSub.SelectedValue;
            m_grid.DataSource = m_PaymentTransactions.GetPage_ForParter(ActUserId, RowAmount, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId,
                                                    m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId,
                                                    m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId, 0, m_DateFrom,
                                                    m_DateTo, ref RowCount, ref TotalMoney);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            lblTotalMoneys.Text = (string.Format("{0:#,#}", TotalMoney) != "" ? string.Format("{0:#,#}", TotalMoney) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
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
            PaymentTransactionsView m_DataItem = (PaymentTransactionsView)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            PaymentTransactionId = m_DataItem.PaymentTransactionId;          
        }
    }
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            m_PaymentTransactions.PaymentTransactionId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_PaymentTransactions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }   
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_PaymentTransactions.PaymentTransactionId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_PaymentTransactions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
        DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
        BindData();
    }
    protected void ddlServicePackages_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlPaymentTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlTransactionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlPlusSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
       // DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        //DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
        DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
        BindData();
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            m_grid.AllowSorting = false;
            BindDataForExcel();
            
            m_grid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            m_grid.FooterStyle.BackColor = System.Drawing.Color.LightGray;
            m_grid.Caption = "<span style='color:Black;font-size:18px;'> Danh sách khách hàng sử dụng gói Tiếng Anh, ngày: " + txtDateFrom.Text + " đến " + (string.IsNullOrEmpty(txtDateTo.Text) ? DateTime.Now.ToShortDateString() : txtDateTo.Text) + "</span>";

            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
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

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("content-disposition", "attachment; filename=DanhSachGiaoDich_DoiTac.xls");

            Response.Write(sb.ToString());
            Response.End();
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

    public string ServicePackages_GetServicePackageNumMonthUse(short ServicePackageId)
    {
        string RetVal = string.Empty;
        ICSoft.LawDocsLib.ServicePackages m_ServicePackages = ICSoft.LawDocsLib.ServicePackages.Static_Get(ServicePackageId);
        if (ServicePackageId == 0)
        {
            RetVal = "Theo gói VBE <br/>(Gói DV cũ)";
        }
        else if (ServicePackageId == 24)
        {
            RetVal = "Gói miễn phí";
        }
        else if (m_ServicePackages != null && m_ServicePackages.NumMonthUse > 0)
        {
            RetVal ="- "+ m_ServicePackages.ConcurrentLogin.ToString()+" tài khoản <br />- " + m_ServicePackages.NumMonthUse.ToString() + " Tháng";
        }
        else if (m_ServicePackages.NumMonthUse == 0 && m_ServicePackages.NumDayUse > 0)
        {
            RetVal = "- " + m_ServicePackages.ConcurrentLogin.ToString() + " tài khoản <br />-" + m_ServicePackages.NumDayUse.ToString() + " Ngày";
        }
        else
        {
            RetVal = "";
        }
        return RetVal;
    }
}

