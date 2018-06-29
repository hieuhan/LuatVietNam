using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Reflection;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_CustomersEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int CustomerId = 0;
    private int ActUserId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Genders> l_Genders = new List<Genders>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = int.Parse(Request.Params["CustomerId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetList(), "");
            DropDownListHelpers.DDLCustomerGroups_BindByCulture(ddlCustomerGroups, CustomerGroups.Static_GetList(), "...");
            RadioButtonListHelpers.Bind(rblGenders, Genders.Static_GetList(), "");
            RadioButtonListHelpers.SetSelected(rblGenders, "1");
            DropDownListHelpers.DDLProvinces_BindByCulture(ddlProvinces, Provinces.Static_GetList(0), "...");
            sms.admin.security.Users m_Users = new sms.admin.security.Users();
            DropDownListHelpers.DDL_Bind(ddlUser, m_Users.GetListOrderByUserName(), "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (CustomerId > 0)
            {
                Customers m_Customers = new Customers();
                m_Customers = m_Customers.Get(CustomerId);
                if (m_Customers.CustomerId > 0)
                {
                    txtCustomerName.Text = m_Customers.CustomerName;
                    txtCustomerFullName.Text = m_Customers.CustomerFullName;
                    txtCustomerNickName.Text = m_Customers.CustomerNickName;
                    txtCustomerMail.Text =m_Customers.CustomerMail;
                    txtWebsite.Text = m_Customers.Website;
                    txtFacebook.Text = m_Customers.Facebook;
                    txtDateOfBirth.Text = (m_Customers.DateOfBirth==DateTime.MinValue) ? "" : m_Customers.DateOfBirth.ToString("dd/MM/yyyy");
                    RadioButtonListHelpers.SetSelected(rblGenders, m_Customers.GenderId.ToString());
                    txtCustomerMobile.Text = m_Customers.CustomerMobile.ToString();
                    txtAddress.Text = m_Customers.Address.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlProvinces, m_Customers.ProvinceId.ToString());
                    txtAccountNumber.Text = m_Customers.AccountNumber.ToString();
                    txtOrganName.Text = m_Customers.OrganName;
                    txtOrganPhone.Text = m_Customers.OrganPhone.ToString();
                    txtOrganFax.Text = m_Customers.OrganFax.ToString();
                    txtOrganAddress.Text = m_Customers.OrganAddress;
                    RadioButtonListHelpers.SetSelected(rblRegisterNewsletter, m_Customers.RegisterNewsletter.ToString());
                    txtMaxConcurrentLogin.Text = m_Customers.MaxConcurrentLogin.ToString();
                    txtCustomerBalance.Text = m_Customers.CustomerBalance.ToString();
                    txtCustomerDayLeft.Text = m_Customers.CustomerDayLeft.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Customers.SiteId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUser, m_Customers.MapCustomerId.ToString());
                    if (m_Customers.LockPassword == 0)
                    {
                        ckbLockPassword.Checked = false;
                    }else
                    {
                        ckbLockPassword.Checked = true;
                    }
                    DropDownListHelpers.DDL_SetSelected(ddlCustomerGroups, m_Customers.CustomerGroupId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlStatus, m_Customers.StatusId.ToString());
                }
                else
                {
                    ClearForm();
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["backUrl"]))
        {
            string backUrl = Request["backUrl"];
            Response.Redirect(backUrl);
        }
        else
        {
            Response.Redirect("CustomersFull.aspx");
        }
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        ClearForm();
    }
    private void ClearForm()
    {
        txtCustomerName.Text = "";
        txtCustomerFullName.Text = "";
        txtCustomerNickName.Text = "";
        txtCustomerMail.Text = "";
        txtFacebook.Text = "";
        txtWebsite.Text = "";
        txtDateOfBirth.Text = "";
        txtCustomerMobile.Text = "";
        txtAddress.Text = "";
        txtAccountNumber.Text = "";
        txtOrganName.Text = "";
        txtOrganPhone.Text = "";
        txtOrganFax.Text = "";
        txtOrganAddress.Text = "";
        txtMaxConcurrentLogin.Text = "";
        txtCustomerBalance.Text = "";
        txtCustomerDayLeft.Text = "";
        ddlStatus.SelectedIndex=-1;
        ddlCustomerGroups.SelectedIndex = -1;
        ddlUser.SelectedIndex = -1;
    }
   
    protected void SaveData()
    {
        try
        {
            short SysMessageId = 0;
            Customers m_Customers = new Customers();
            if (CustomerId > 0) m_Customers = m_Customers.Get(CustomerId);
            m_Customers.CustomerId = CustomerId;
            m_Customers.CustomerName = txtCustomerName.Text;
            if(!string.IsNullOrWhiteSpace(txtCustomerPass.Text)) m_Customers.CustomerPass = txtCustomerPass.Text.Trim();
            m_Customers.CustomerFullName = txtCustomerFullName.Text;
            m_Customers.CustomerNickName = txtCustomerNickName.Text;
            m_Customers.CustomerMail = txtCustomerMail.Text;
            m_Customers.Website = txtWebsite.Text;
            m_Customers.Facebook = txtFacebook.Text;
            m_Customers.CustomerBalance = (txtCustomerBalance.Text != "" ? int.Parse(txtCustomerBalance.Text) : 0);
            m_Customers.CustomerDayLeft = (txtCustomerDayLeft.Text != "" ? short.Parse(txtCustomerDayLeft.Text) : short.Parse("0"));
            m_Customers.StatusId = byte.Parse(ddlStatus.SelectedValue);
            m_Customers.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Customers.CustomerMobile = txtCustomerMobile.Text;
            m_Customers.ClientId = "";
            m_Customers.GenderId = rblGenders.SelectedIndex >= 0 ? byte.Parse(rblGenders.SelectedValue) : (byte)0;
            m_Customers.ProvinceId = short.Parse(ddlProvinces.SelectedValue);
            m_Customers.DateOfBirth = (txtDateOfBirth.Text != "" ? Convert.ToDateTime(txtDateOfBirth.Text) : DateTime.MinValue);
            m_Customers.Identifier = "";
            m_Customers.IMEI = "";
            m_Customers.GCMRegisterId = "";
            m_Customers.MSISDN = "";
            m_Customers.EmailAuto = "";
            m_Customers.BusinessApplicationPlatformId = 0;
            m_Customers.ApplicationId = 0;
            m_Customers.PlatformId = 0;
            m_Customers.BusinessPartnerId = 0;
            m_Customers.AppPlatformId = 0;
            m_Customers.MapCustomerId = int.Parse(ddlUser.SelectedValue);
            m_Customers.CustomerGroupId = short.Parse(ddlCustomerGroups.SelectedValue);
            m_Customers.OccupationId = 0;
            m_Customers.Address = txtAddress.Text;
            m_Customers.AccountNumber = txtAccountNumber.Text;
            m_Customers.OrganName = txtOrganName.Text;
            m_Customers.OrganPhone = txtOrganPhone.Text;
            m_Customers.OrganFax = txtOrganFax.Text;
            m_Customers.OrganAddress = txtOrganAddress.Text;
            m_Customers.Note = "";
            m_Customers.InfoChannelId = 0;
            m_Customers.RegisterNewsletter = byte.Parse(rblRegisterNewsletter.SelectedValue);
            m_Customers.MaxConcurrentLogin = byte.Parse(txtMaxConcurrentLogin.Text.Trim() == "" ? "0" : txtMaxConcurrentLogin.Text.Trim());
            if (ckbLockPassword.Checked == true){
                m_Customers.LockPassword = 1;
            }else{
                m_Customers.LockPassword = 0;
            }
            m_Customers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thuộc tính khách hàng thành công", this);
            //JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
   
}