using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.HelperLib;
using sms.common;
using sms.utils;
using ICSoft.LawDocsLib;
using ICSoft.CMSLib;

public partial class Admin_Pages_lawsdocs_CustomersServicesReport_HistoryPay : System.Web.UI.Page
{
    private int ActUserId = 0;
    protected string FullName = "";
    protected int NewsletterReadLogId = 0;
    protected List<TransactionStatus> l_TransactionStatus = new List<TransactionStatus>();
    protected List<ICSoft.LawDocsLib.PaymentTypes> l_PaymentTypes = new List<ICSoft.LawDocsLib.PaymentTypes>();  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (!IsPostBack)
            {
                bindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    private void bindData()
    {
        System.Int32 CustomerId;
        if (Request.QueryString["id"] == null)
        {
            return;
        }
        else
        {
            l_TransactionStatus = TransactionStatus.Static_GetList();
            l_PaymentTypes = ICSoft.LawDocsLib.PaymentTypes.Static_GetList();
            int RowAmount= 50, PageIndex =0 ;
            string OrderBy = "", CustomerName = "";
            short ServiceId  = 0 ,ServicePackageId  = 0;
            string TransactionDesc ="" , TransactionCode = "";
            byte PaymentTypeId = 0, TransactionStatusId = 0;
            int ApplicationPlatformId =0;
            short BusinessPartnerId = 0 ,ApplicationId = 0, PlatformId =0;
            int CrUserId = 0;
            string DateFrom ="", DateTo = "";
            int RowCount = 0, TotalCustomers = 0;
            decimal TotalMoney = 0;
            CustomerId = System.Int32.Parse(Request.QueryString["id"].ToString());
            Customers mCustomers = Customers.Static_Get(CustomerId);
            FullName = string.IsNullOrEmpty(mCustomers.CustomerFullName) ? mCustomers.CustomerName : mCustomers.CustomerFullName + " - " + mCustomers.CustomerName;
            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
            List<PaymentTransactionsView> l_PaymentTransactionsView = new List<PaymentTransactionsView>();
            l_PaymentTransactionsView = m_PaymentTransactions.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, CustomerId, CustomerName, ServiceId,  
                                                  ServicePackageId, TransactionStatusId, TransactionDesc, TransactionCode, PaymentTypeId, 
                                                  ApplicationPlatformId, BusinessPartnerId, ApplicationId, PlatformId,  CrUserId,  DateFrom,
                                                  DateTo, ref RowCount, ref TotalMoney, ref TotalCustomers);
            m_grid.DataSource = l_PaymentTransactionsView;
            m_grid.DataBind();
            lblMsg.Text = "Khách hàng: " + FullName + "(" + l_PaymentTransactionsView.Count + " giao dịch)";
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewsletterReadLogId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
        }
    }
}