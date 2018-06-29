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

public partial class Admin_CustomersEditFull : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int CustomerId = 0;
    private int ActUserId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Genders> l_Genders = new List<Genders>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CustomersRegisterNew-check"] = "1";
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = int.Parse(Request.Params["CustomerId"].ToString());
        }
        if (Session["Message"] != null)
        {
            JSAlertHelpers.ShowNotify("10", "information", Session["Message"].ToString(), this);
            Session["Message"] = null;
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
            DropDownListHelpers.DDLOccupations_BindByCulture(ddlOccupation, Occupations.Static_GetList(),"...");
            DropDownListHelpers.DDLOrganOccupations_BindByCulture(ddlOrganOccupation, OrganOccupations.Static_GetList(), "...");
            ddlMaxConcurrentLogin.SelectedIndex = -1;
            BindData();
            BindCustomerField();
        }
    }
    private void BindCustomerField(){
        try {
            List<Fields> l_Fields = new Fields().GetListApproved();
            CheckBoxListHelpers.Bind(chkFields, l_Fields, "");
            //CheckBoxListHelpers.Bind(chkFieldsTCVN, l_Fields, "");
            for (int i = 0; i < chkFields.Items.Count; i++)
            {
                if (!chkFields.Items[i].Text.StartsWith("&nbsp"))
                {
                    chkFields.Items[i].Attributes.Add("style", "font-weight:italic;");
                }
            }
            if (CustomerId > 0)
            {
                CustomerFields mCustomerFields = new CustomerFields();
                List<CustomerFields> lCustomerFields = mCustomerFields.GetListFieldsById(CustomerId, 1, 2);
                List<CustomerFields> lCustomerFieldsTCVN = mCustomerFields.GetListFieldsById(CustomerId, 3, 2);
                for (int i = 0; i < lCustomerFields.Count; i++)
                {
                    CheckBoxListHelpers.SetSelected(chkFields, lCustomerFields[i].FieldId.ToString(), "Yellow");
                }
                for (int i = 0; i < lCustomerFieldsTCVN.Count; i++)
                {
                    CheckBoxListHelpers.SetSelected(chkFields, lCustomerFieldsTCVN[i].FieldId.ToString(), "Yellow");
                } 
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
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
                txtCustomerName.Enabled = false;
                btnSearch.Enabled = false;
                cmpPassword.Enabled = false;
                rfvtxtConfirmPassword.Enabled = false;
                rfvtxtConfirmPassword.Enabled = false;
                rfvCustomerPass.Enabled = false;
                rfvtxtCustomerPass.Enabled = false;
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
                    DropDownListHelpers.DDL_SetSelected(ddlOccupation, m_Customers.OccupationId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOrganOccupation, m_Customers.OrganOccupationId.ToString());
                    txtAccountNumber.Text = m_Customers.AccountNumber.ToString();
                    txtOrganName.Text = m_Customers.OrganName;
                    txtOrganPhone.Text = m_Customers.OrganPhone.ToString();
                    txtOrganFax.Text = m_Customers.OrganFax.ToString();
                    txtOrganAddress.Text = m_Customers.OrganAddress;
                    txtOrganTaxCode.Text = m_Customers.OrganTaxCode;
                    RadioButtonListHelpers.SetSelected(rblRegisterNewsletter, m_Customers.RegisterNewsletter.ToString());
                    ddlMaxConcurrentLogin.SelectedValue = m_Customers.MaxConcurrentLogin.ToString();
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
            else
            {
                
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
        if (!checkNotExist() && CustomerId ==0)
        {
            return;
        }
        int CutomerIdTemp = SaveData();
        if (CustomerId == 0)
        {
            Session["Message"] = "Thêm mới khách hàng thành công.";
            Response.Redirect("CustomersEditFull.aspx?CustomerId=" + CutomerIdTemp.ToString());
        }
        else
        {
            JSAlertHelpers.ShowNotify("10", "", "Cập nhật thông tin khách hàng thành công.", this);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomersFull.aspx");
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        if (!checkNotExist() && CustomerId == 0)
        {
            return;
        }
        SaveData();
        if (CustomerId == 0)
        {
            ClearForm();
            JSAlertHelpers.ShowNotify("10", "", "Thêm mới khách hàng thành công.", this);
           
        }

        else
        {
            JSAlertHelpers.ShowNotifyOtherPage("10", "", "Cập nhật thông tin khách hàng thành công.", this);
            Response.Redirect("CustomersEditFull.aspx");
        }
            
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
        ddlMaxConcurrentLogin.SelectedIndex = -1;
        txtCustomerBalance.Text = "";
        txtCustomerDayLeft.Text = "";
        txtOrganTaxCode.Text = "";
        ddlOccupation.SelectedIndex = -1;
        ddlOrganOccupation.SelectedIndex = -1;
        ddlStatus.SelectedIndex=-1;
        ddlCustomerGroups.SelectedIndex = -1;
        ddlUser.SelectedIndex = -1;
    }
   private bool checkNotExist()
    {
        bool RetVal = true;
        Customers m_Customers = new Customers(CmsConstants.CONNECTION_STRING);
        m_Customers.CustomerName = txtCustomerName.Text;
        m_Customers = m_Customers.Customers_GetByName();
        if(m_Customers.CustomerId > 0 && CustomerId==0)
        {
            RetVal = false;
            JSAlertHelpers.ShowNotify("10", "", "Tên truy cập " + txtCustomerName.Text + " đã có người đăng ký. Vui lòng chọn tên truy cập khác!", this);
           
        }
        return RetVal;
    }
    protected int SaveData()
    {
        int RetVal = 0;
        try
        {
            short SysMessageId = 0;
            Customers m_Customers = new Customers();
            if (CustomerId > 0)
            {
                m_Customers = m_Customers.Get(CustomerId);
               
            }
            m_Customers.CustomerId = CustomerId;
            m_Customers.CustomerName = txtCustomerName.Text;
            //m_Customers.CustomerPass = txtCustomerPass.Text.Trim() !="" ? txtCustomerPass.Text.Trim() : "" ;
            if (!string.IsNullOrWhiteSpace(txtCustomerPass.Text)) m_Customers.CustomerPass = txtCustomerPass.Text.Trim();
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
            m_Customers.OccupationId = byte.Parse(ddlOccupation.SelectedValue); ;
            m_Customers.Address = txtAddress.Text;
            m_Customers.AccountNumber = txtAccountNumber.Text;
            m_Customers.OrganName = txtOrganName.Text;
            m_Customers.OrganPhone = txtOrganPhone.Text;
            m_Customers.OrganFax = txtOrganFax.Text;
            m_Customers.OrganAddress = txtOrganAddress.Text;
            m_Customers.Note = "";
            m_Customers.OrganTaxCode= txtOrganTaxCode.Text;
            m_Customers.InfoChannelId = 0;
            m_Customers.RegisterNewsletter = byte.Parse(rblRegisterNewsletter.SelectedValue);
            m_Customers.MaxConcurrentLogin = byte.Parse(ddlMaxConcurrentLogin.SelectedValue);
            m_Customers.OrganOccupationId = byte.Parse(ddlOrganOccupation.SelectedValue);
            if (ckbLockPassword.Checked == true){
                m_Customers.LockPassword = 1;
            }else{
                m_Customers.LockPassword = 0;
            }
            m_Customers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            RetVal = m_Customers.CustomerId;
            string listFieldId = "";
            if (RetVal > 0)
            {
                CustomerFields mCustomerFields = new CustomerFields();
                mCustomerFields.CustomerId = RetVal;
                mCustomerFields.DocGroupId = 1;
                mCustomerFields.DisplayOrder = 0;
                for (int i = 0; i < chkFields.Items.Count; i++)
                {
                    if (chkFields.Items[i].Selected)
                    {
                        listFieldId += chkFields.Items[i].Value;
                        listFieldId += ",";
                    }
                }
                mCustomerFields.InsertByListId(listFieldId);
                //TCVN
                CustomerFields mCustomerFieldsTCVN = new CustomerFields();
                mCustomerFieldsTCVN.CustomerId = RetVal;
                mCustomerFieldsTCVN.DocGroupId = 3;
                mCustomerFieldsTCVN.DisplayOrder = 0;
                mCustomerFieldsTCVN.InsertByListId(listFieldId);
            }
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
        return RetVal;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtCustomerName.Text=="")
        {
            JSAlertHelpers.ShowNotify("10", "", "Bạn chưa nhập tên truy cập", this);
        }
        if (checkNotExist())
        {
            JSAlertHelpers.ShowNotify("10","","Tên truy cập " + txtCustomerName.Text + " có thể dùng để đăng ký!", this);
        }
        else
        {
            return;
        }
    }
}