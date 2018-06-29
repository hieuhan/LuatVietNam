using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

using sms.admin.security;
using TextBox = System.Web.UI.WebControls.TextBox;

public partial class Admin_PaymentTransactions_Add : System.Web.UI.Page
{
    protected int CustomerId = 0;
    private int ActUserId = 0;
    private int PaymentTransactionId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected List<TransactionStatus> l_TransactionStatus = new List<TransactionStatus>();
    protected List<ICSoft.LawDocsLib.PaymentTypes> l_PaymentTypes = new List<ICSoft.LawDocsLib.PaymentTypes>();  

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = Int32.Parse(Request.Params["CustomerId"].ToString());
        }
        if (!IsPostBack)
        {
            txtBeginTime.Text=DateTime.Now.ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, 0), "...");
            DropDownListHelpers.DDL_Bind(ddlServicePackage, ServicePackages.Static_GetListByServiceId(short.Parse(ddlServices.SelectedValue), "--"), "...");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindCustomerService()
    {
        CustomerServices m_CustomerServices = new CustomerServices();
        List< CustomerServices> l_CustomerServices = m_CustomerServices.CustomerServices_GetListByCustomerId(CustomerId);
        //DataTable m_DataTable = m_CustomerServices.CustomerServices_LVN_GetList(CustomerId, "", 1);
        if (l_CustomerServices.Count > 0 && l_CustomerServices[0].RenewDateTime <= DateTime.MinValue)
        {
            m_GridService.Columns[4].Visible = false;
        }
        m_GridService.DataSource = l_CustomerServices;
        m_GridService.DataBind();
        txtBeginTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
        if (l_CustomerServices.Count == 1)
        {
            if (l_CustomerServices[0].ServiceId.ToString() == ddlServices.SelectedValue) txtBeginTime.Text = l_CustomerServices[0].EndTime.ToString("dd/MM/yyyy");
        }

    }
    private void BindData()
    {
        try
        {
            BindCustomerService();
            if (CustomerId > 0)
            {
                l_TransactionStatus = TransactionStatus.Static_GetList();
                l_PaymentTypes = ICSoft.LawDocsLib.PaymentTypes.Static_GetList();
                Users m_Users = new Users();
                l_Users = m_Users.GetAll();
                Customers m_Customers = new Customers();
                m_Customers = m_Customers.Get(CustomerId);
                if (m_Customers.CustomerId > 0)
                {
                    lblCustomerName.Text = m_Customers.CustomerName + " (" + m_Customers.CustomerFullName +  ")";
                }
                else
                {
                    lblCustomerName.Text = "";
                }
               
                int RowCount = 0;
                decimal TotalMoney = 0;
                PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                string m_OrderBy = "";
                string m_CustomerName = "";
                int m_CustomerId = CustomerId;
                short m_ServicePackageId = 0;
                short m_ServiceId = 0;
                byte m_TransactionStatusId = 0;
                string m_TransactionDesc = "";
                string m_TransactionCode = "";
                byte m_PaymentTypeId = 0;
                int m_ApplicationPlatformId = 0;
                short m_BusinessPartnerId = 0;
                short m_ApplicationId = 0;
                short m_PlatformId = 0;
                int m_CrUserId = 0;
                string m_DateFrom = "";
                string m_DateTo = "";
                int TotalCustomer = 0;
                
                m_grid.DataSource = m_PaymentTransactions.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_CustomerName, m_ServiceId, m_ServicePackageId, m_TransactionStatusId, m_TransactionDesc, m_TransactionCode, m_PaymentTypeId, m_ApplicationPlatformId, m_BusinessPartnerId, m_ApplicationId, m_PlatformId, m_CrUserId, m_DateFrom, m_DateTo, ref RowCount, ref TotalMoney,ref TotalCustomer);
                m_grid.DataBind();
                m_grid.Columns[m_grid.Columns.Count - 1].Visible = HasRole();
                m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                lblTong.Text = string.Format("{0:#,#}", RowCount)!= "" ? string.Format("{0:#,#}", RowCount) : "0";
                lblTotalMoneyALL.Text = string.Format("{0:#,#}", TotalMoney) !="" ? string.Format("{0:#,#}", TotalMoney) : "0";
                CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            }
            
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
    private void m_grid_CellValidating(object sender,
        DataGridViewCellValidatingEventArgs e)
    {
        if (e.ColumnIndex == 5) 
        {
            int i;

            if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
            {
                e.Cancel = true;
                lblMsg.Text = "Số tiền không hợp lệ.";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlServicePackage.SelectedValue == "0")
            {
                JSAlert.Alert("Cần chọn dịch vụ và gói!", this);
                return;
            }
            if (txtBeginTime.Text == "")
            {
                JSAlert.Alert("Cần chọn ngày bắt đầu!", this);
                return;
            }
            //if (txtEndTime.Text == "")
            //{
            //    JSAlert.Alert("Cần chọn ngày hết hạn!", this);
            //    return;
            //}

            DateTime m_BeginTime = DateTime.Parse(txtBeginTime.Text);
            DateTime m_EndTime = DateTime.Parse(txtBeginTime.Text);
            int m_MoneyAmount = txtTotal.Text == "" ? 0 : int.Parse(txtTotal.Text);
            string m_Msg = "";

            CustomerServices m_CustomerServices = new CustomerServices();
            m_CustomerServices.CustomerServices_LVN_DangKyCMS(ActUserId, CustomerId, "", short.Parse(ddlServices.SelectedValue), short.Parse(ddlServicePackage.SelectedValue), m_BeginTime, m_EndTime, m_MoneyAmount, Request.UserHostAddress, ref m_Msg);

            txtEndTime.Text = "";
            txtAmount.Text = "";
            txtVAT.Text = "";
            txtTotal.Text = "";
            ddlServicePackage.SelectedIndex = 0;

            JSAlert.Alert(m_Msg, this);
            BindData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }         

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string script = @"<script language='JavaScript'>" +
            "window.parent.jQuery('#divEdit').dialog('close');" +
            "</script>";
        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), "close", script);
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListHelpers.DDL_Bind(ddlServicePackage, ServicePackages.Static_GetListByServiceId(short.Parse(ddlServices.SelectedValue), "--"), "...");
        BindCustomerService();
    }

    protected void chkVAT_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVAT.Checked)
        {
            txtVAT.Text = (int.Parse(txtAmount.Text) * 0.1).ToString();
            txtTotal.Text = (int.Parse(txtAmount.Text) + int.Parse(txtVAT.Text)).ToString();
        }
        else
        {
            txtVAT.Text = "0";
            txtTotal.Text = txtAmount.Text;
        }
    }

    protected void ddlServicePackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ServicePackages m_ServicePackages = ServicePackages.Static_Get(short.Parse(ddlServicePackage.SelectedValue));
        txtAmount.Text = m_ServicePackages.Price.ToString();
        if (chkVAT.Checked)
        {
            txtVAT.Text = (int.Parse(txtAmount.Text) * 0.1).ToString();
            txtTotal.Text = (int.Parse(txtAmount.Text) + int.Parse(txtVAT.Text)).ToString();
        }
        else
        {
            txtVAT.Text = "0";
            txtTotal.Text = txtAmount.Text;
        }
        txtEndTime.Text = ((m_ServicePackages.NumMonthUse == 0) ? "" : (m_ServicePackages.NumMonthUse.ToString() + " tháng")) + ((m_ServicePackages.NumDayUse == 0) ? "" : (m_ServicePackages.NumDayUse.ToString() + " ngày"));
    }

    /// <summary>
    /// Check quyen sua gia tien giao dich
    /// </summary>
    /// <returns></returns>
    private bool HasRole()
    {
        return new PaymentTransactions().HasRoleEditAmount(ActUserId);
    }
}   