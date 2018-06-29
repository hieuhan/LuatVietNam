using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data;
using System.Globalization;
public partial class Admin_PaymentTransactions : System.Web.UI.Page
{
    protected int ActUserId = 0, pagesize = 0;
    protected int PaymentTransactionId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected List<AccountTypes> l_AccountTypes = new List<AccountTypes>();
    protected List<TransactionStatus> l_TransactionStatus = new List<TransactionStatus>();
    protected List<ICSoft.LawDocsLib.PaymentTypes> l_PaymentTypes = new List<ICSoft.LawDocsLib.PaymentTypes>();
    protected List<ICSoft.LawDocsLib.TransactionTypes> l_TransactionTypes = new List<ICSoft.LawDocsLib.TransactionTypes>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, 0), "...");
            DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, 0, short.Parse(ddlServices.SelectedValue), "--"), "...");
            DropDownListHelpers.DDLPaymentTypes_BindByCulture(ddlPaymentTypes, ICSoft.LawDocsLib.PaymentTypes.Static_GetListOrderBy("PaymentTypeName"), "...");
            DropDownListHelpers.DDLTransactionStatus_BindByCulture(ddlTransactionStatus, TransactionStatus.Static_GetListOrderBy("TransactionStatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("PaymentTransactions"), "");
            Partners m_Partners = new Partners();
            txtDateFrom.Text = DateTime.Now.AddDays((DateTime.Now.Day * -1)+1).ToString("dd/MM/yyyy");

            if (Session["PaymentTransactions-ddlServices"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("CustomersEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("ServicePackagesEdit.aspx")))
            {
                ddlServices.SelectedValue = Session["PaymentTransactions-ddlServices"].ToString();
                ddlOrderBy.SelectedValue = Session["PaymentTransactions-ddlOrderBy"].ToString();
                ddlServicePackages.SelectedValue = Session["PaymentTransactions-ddlServicePackages"].ToString();
                ddlPaymentTypes.SelectedValue = Session["PaymentTransactions-ddlPaymentTypes"].ToString();
                ddlTransactionStatus.SelectedValue = Session["PaymentTransactions-ddlTransactionStatus"].ToString();
                txtDateFrom.Text = Session["PaymentTransactions-txtDateFrom"].ToString();
                txtDateTo.Text = Session["PaymentTransactions-txtDateTo"].ToString();
                txtSearch.Text = Session["PaymentTransactions-txtSearch"].ToString();
                txtAmountFrom.Text = Session["PaymentTransactions-txtAmountFrom"].ToString();
                txtAmountTo.Text = Session["PaymentTransactions-txtAmountTo"].ToString();
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
        //Session["PaymentTransactions-ddlSite"] = ddlSite.SelectedValue;
        Session["PaymentTransactions-ddlServicePackages"] = ddlServicePackages.SelectedValue;
        Session["PaymentTransactions-ddlPaymentTypes"] = ddlPaymentTypes.SelectedValue;
        Session["PaymentTransactions-ddlTransactionStatus"] = ddlTransactionStatus.SelectedValue;
        //Session["PaymentTransactions-ddlTranType"] = ddlTranType.SelectedValue;
        //Session["PaymentTransactions-ddlAccountType"] = ddlAccountType.SelectedValue;
        //Session["PaymentTransactions-ddlPartner"] = ddlPartner.SelectedValue;
        //Session["PaymentTransactions-ddlPlusSub"] = ddlPlusSub.SelectedValue;
        Session["PaymentTransactions-txtDateFrom"] = txtDateFrom.Text;
        Session["PaymentTransactions-txtDateTo"] = txtDateTo.Text;
        Session["PaymentTransactions-txtSearch"] = txtSearch.Text;
        Session["PaymentTransactions-txtAmountFrom"] = txtAmountFrom.Text;
        Session["PaymentTransactions-txtAmountTo"] = txtAmountTo.Text;
    }
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            int TotalCustomers = 0;
            decimal TotalMoney = 0;
            l_TransactionStatus = TransactionStatus.Static_GetList();
            l_PaymentTypes = ICSoft.LawDocsLib.PaymentTypes.Static_GetList();
            l_TransactionTypes = ICSoft.LawDocsLib.TransactionTypes.Static_GetList();
            l_AccountTypes = AccountTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

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
            short m_BusinessPartnerId = 0;//byte.Parse(ddlPartner.SelectedValue);
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            int m_CrUserId = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_PaymentTransactions.SiteId = 0;// short.Parse(ddlSite.SelectedValue);
            m_PaymentTransactions.TransactionTypeId = 0;// byte.Parse(ddlTranType.SelectedValue);
            m_PaymentTransactions.BusinessPartnerId = 0;// byte.Parse(ddlPartner.SelectedValue);
            m_PaymentTransactions.AccountTypeId = 0;// byte.Parse(ddlAccountType.SelectedValue);
            m_PaymentTransactions.PlusSub = ".";// ddlPlusSub.SelectedValue;
            int m_FromAmount = (txtAmountFrom.Text != "") ? int.Parse(txtAmountFrom.Text) : 0;
            int m_ToAmount = (txtAmountTo.Text != "") ? int.Parse(txtAmountTo.Text) : 0;
            m_grid.DataSource = m_PaymentTransactions.GetPageV2(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId, m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId, m_FromAmount, m_ToAmount, m_DateFrom, m_DateTo, ref RowCount, ref TotalMoney, ref TotalCustomers);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            pagesize = RowCount;
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
    protected void m_grid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        m_grid.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Gán giá trị EditIndex -1 để hủy chế độ Edit trên Gridview  
        m_grid.EditIndex = -1;  
        BindData();
    }
    protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int paymentTransactionId = 0, amount = 0;
        TextBox txtAmount = m_grid.Rows[e.RowIndex].FindControl("txtAmount") as TextBox;
        Int32.TryParse(m_grid.DataKeys[e.RowIndex].Value.ToString(), out paymentTransactionId);
        Int32.TryParse(txtAmount.Text, out amount);
        if (paymentTransactionId > 0)
        {
            new PaymentTransactions().Update_Amount(ActUserId, paymentTransactionId, amount);
        } 
        m_grid.EditIndex = -1; 
        BindData();
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
        DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, 0, short.Parse(ddlServices.SelectedValue), "--"), "...");
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
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, 0), "...");
        //DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue), "--"), "...");
        DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, 0, short.Parse(ddlServices.SelectedValue), "--"), "...");
        BindData();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private bool HasRole()
    {
        return new PaymentTransactions().HasRoleEditAmount(ActUserId);
    }
    private void ExportToExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Danhsachgiaodich.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        HttpContext.Current.Response.Write(string.Format("<strong style='text-align: center;'>Báo cáo danh sách giao dịch từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text));
        
        HttpContext.Current.Response.Write("</ br>");
        HttpContext.Current.Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Arial; background:white;'> <tr bgcolor=#5cd6ff>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("STT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tên tài khoản");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Tên gói");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Mô tả");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Số tiền");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Trạng thái");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Hình thức thanh toán");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thời gian");
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
    private DataTable ToDataTable(List<PaymentTransactionsView> lPaymentTransactions)
    {
        DataTable dataTable = new DataTable(typeof(PaymentTransactionsView).Name);
        if (lPaymentTransactions == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("Tên tài khoản", typeof(string));
        dataTable.Columns.Add("Dịch vụ", typeof(string));
        dataTable.Columns.Add("Tên gói", typeof(string));
        dataTable.Columns.Add("Mô tả", typeof(string));
        dataTable.Columns.Add("Số tiền", typeof(string));
        dataTable.Columns.Add("Trạng thái", typeof(string));
        dataTable.Columns.Add("Hình thức thanh toán", typeof(string));
        dataTable.Columns.Add("Thời gian", typeof(string));
        for (var index = 0; index < lPaymentTransactions.Count; index++)
        {
        PaymentTransactionsView mPaymentTransactionsView= lPaymentTransactions[index];
        DataRow rowAdded = dataTable.Rows.Add();
        rowAdded.SetField("STT", index +1);
        rowAdded.SetField("Tên tài khoản", mPaymentTransactionsView.mCustomer.CustomerName);
        rowAdded.SetField("Dịch vụ", mPaymentTransactionsView.mService.ServiceDesc);
        rowAdded.SetField("Tên gói", mPaymentTransactionsView.mServicePackage.ServicePackageDesc);
        rowAdded.SetField("Mô tả", mPaymentTransactionsView.TransactionDesc);
        rowAdded.SetField("Số tiền", mPaymentTransactionsView.Amount);
        rowAdded.SetField("Trạng thái" , TransactionStatus.Static_Get(byte.Parse(mPaymentTransactionsView.TransactionStatusId.ToString()), l_TransactionStatus).TransactionStatusDesc);
        rowAdded.SetField("Hình thức thanh toán", ICSoft.LawDocsLib.PaymentTypes.Static_Get(byte.Parse(mPaymentTransactionsView.PaymentTypeId.ToString()), l_PaymentTypes).PaymentTypeDesc);
        rowAdded.SetField("Thời gian", mPaymentTransactionsView.CrDateTime.ToString("dd/MM/yyyy"));
        }
       
        
        return dataTable;
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
           
            int RowCount = 0;
            int TotalCustomers = 0;
            decimal TotalMoney = 0;
            l_TransactionStatus = TransactionStatus.Static_GetList();
            l_PaymentTypes = ICSoft.LawDocsLib.PaymentTypes.Static_GetList();
            l_TransactionTypes = ICSoft.LawDocsLib.TransactionTypes.Static_GetList();
            l_AccountTypes = AccountTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

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
            short m_BusinessPartnerId = 0;//byte.Parse(ddlPartner.SelectedValue);
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            int m_CrUserId = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_PaymentTransactions.SiteId = 0;// short.Parse(ddlSite.SelectedValue);
            m_PaymentTransactions.TransactionTypeId = 0;// byte.Parse(ddlTranType.SelectedValue);
            m_PaymentTransactions.BusinessPartnerId = 0;// byte.Parse(ddlPartner.SelectedValue);
            m_PaymentTransactions.AccountTypeId = 0;// byte.Parse(ddlAccountType.SelectedValue);
            m_PaymentTransactions.PlusSub = ".";// ddlPlusSub.SelectedValue;
            List<PaymentTransactionsView> lPaymentTransactions = m_PaymentTransactions.GetPage(ActUserId, pagesize, 0, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId, m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId, m_DateFrom, m_DateTo, ref RowCount, ref TotalMoney, ref TotalCustomers);
            DataTable dataTable = ToDataTable(lPaymentTransactions);
            ExportToExcel(dataTable);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}

