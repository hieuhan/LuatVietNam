using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using Countries = ICSoft.CMSLib.Countries;
using Provinces = ICSoft.CMSLib.Provinces;

public partial class Admin_Pages_articles_OrdersEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private short OrderId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OrderId"] != null && Request.Params["OrderId"].ToString() != "")
        {
            OrderId = short.Parse(Request.Params["OrderId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "...");
            DropDownListHelpers.DDL_Bind(ddlDistricts, Districts.Static_GetList(short.Parse(ddlProvinces.SelectedIndex.ToString())), "...");
            DropDownListHelpers.DDL_Bind(ddlPaymentTypes, PaymentTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlOrderStatus, OrderStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlDeliveryStatus, DeliveryStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlPartners, Partners.Static_GetList(), "");
            BindData();
        }
    }
    //------------------------------------------------------------------------------------------------------
    private void BindData()
    {
        try
        {
            if (OrderId > 0)
            {
                Orders m_Orders = new Orders();
                m_Orders = m_Orders.Get(OrderId);
                if (m_Orders.OrderId > 0)
                {
                    txtOrderName.Text = m_Orders.OrderName.ToString();
                    txtOrderDesc.Text = m_Orders.OrderDesc.ToString();
                    txtOrderCode.Text = m_Orders.OrderCode.ToString();
                    txtCustomerId.Text = m_Orders.CustomerId.ToString();
                    txtFullName.Text = m_Orders.FullName.ToString();
                    txtAddress.Text = m_Orders.Address.ToString();
                    txtMobile.Text = m_Orders.Mobile.ToString();
                    txtEmail.Text = m_Orders.Email.ToString();
                    txtCouponCode.Text = m_Orders.CouponCode.ToString();
                    txtCouponValue.Text = m_Orders.CouponValue.ToString();
                    txtRequireInvoice.Text = m_Orders.RequireInvoice.ToString();
                    txtCompanyName.Text = m_Orders.CompanyName.ToString();
                    txtCompanyAddress.Text = m_Orders.CompanyAddress.ToString();
                    txtCompanyTaxCode.Text = m_Orders.CompanyTaxCode.ToString();
                    txtOrderValue.Text = m_Orders.OrderValue.ToString();
                    txtDeliveryFee.Text = m_Orders.DeliveryFee.ToString();
                    txtDeliveryNote.Text = m_Orders.DeliveryNote.ToString();
                    txtFromIP.Text = m_Orders.FromIP.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Orders.SiteId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlCountries, m_Orders.CountryId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlProvinces, m_Orders.ProvinceId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDistricts, m_Orders.DistrictId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlPaymentTypes, m_Orders.PaymentTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOrderStatus, m_Orders.OrderStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDeliveryStatus, m_Orders.DeliveryStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlPartners, m_Orders.PartnerId.ToString());
                }
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOrderName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }

            short SysMessageId = 0;
            short SysMessageTypeId = 0;
            Orders m_Orders = new Orders();
            m_Orders.OrderName = txtOrderName.Text;
            m_Orders.OrderDesc = txtOrderDesc.Text;
            m_Orders.OrderId = OrderId;
            m_Orders.OrderCode = txtOrderCode.Text;
            m_Orders.CustomerId = int.Parse(txtCustomerId.Text == "" ? "0" : txtCustomerId.Text);
            m_Orders.FullName = txtFullName.Text;
            m_Orders.Address = txtAddress.Text;
            m_Orders.Mobile = txtMobile.Text;
            m_Orders.Email = txtEmail.Text;
            m_Orders.CouponCode = txtCouponCode.Text;
            m_Orders.CouponValue = txtCouponValue.Text == "" ? 0 : double.Parse(txtCouponValue.Text);
            m_Orders.RequireInvoice = byte.Parse(txtRequireInvoice.Text == "" ? "0" : txtRequireInvoice.Text);
            m_Orders.CompanyName = txtCompanyName.Text;
            m_Orders.CompanyAddress = txtCompanyAddress.Text;
            m_Orders.CompanyTaxCode = txtCompanyTaxCode.Text;
            m_Orders.OrderValue = txtOrderValue.Text == "" ? 0 : double.Parse(txtOrderValue.Text);
            m_Orders.DeliveryFee = txtDeliveryFee.Text == "" ? 0 : double.Parse(txtDeliveryFee.Text);
            m_Orders.DeliveryNote = txtDeliveryNote.Text;
            m_Orders.FromIP = txtFromIP.Text;
            m_Orders.CountryId = short.Parse(ddlCountries.SelectedValue);
            m_Orders.ProvinceId = short.Parse(ddlProvinces.SelectedValue);
            m_Orders.DistrictId = short.Parse(ddlDistricts.SelectedValue);
            m_Orders.PaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            m_Orders.OrderStatusId = byte.Parse(ddlOrderStatus.SelectedValue);
            m_Orders.DeliveryStatusId = byte.Parse(ddlDeliveryStatus.SelectedValue);
            m_Orders.PartnerId = short.Parse(ddlPartners.SelectedValue);
            m_Orders.SiteId = short.Parse(ddlSite.SelectedValue);

            SysMessageTypeId = m_Orders.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        OrderId > 0 ? "Cập nhật đơn hàng thành công." : "Thêm mới đơn hàng thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    OrderId > 0 ? "Cập nhật đơn hàng thành công." : "Thêm mới đơn hàng thành công.", this);
            }

            //JSAlertHelpers.close(this);
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void ClearForm()
    {
        txtAddress.Text = "";
        txtCompanyAddress.Text = "";
        txtCompanyName.Text = "";
        txtCompanyTaxCode.Text = "";
        txtCouponCode.Text = "";
        txtCouponValue.Text = "";
        txtCustomerId.Text = "";
        txtDeliveryFee.Text = "";
        txtDeliveryNote.Text = "";
        txtEmail.Text = "";
        txtFromIP.Text = "";
        txtFullName.Text = "";
        txtMobile.Text = "";
        txtOrderCode.Text = "";
        txtOrderDesc.Text = "";
        txtOrderName.Text = "";
        txtOrderValue.Text = "";
        txtRequireInvoice.Text = "";
        ddlCountries.SelectedIndex = -1;
        ddlDeliveryStatus.SelectedIndex = -1;
        ddlDistricts.SelectedIndex = -1;
        ddlOrderStatus.SelectedIndex = -1;
        ddlPartners.SelectedIndex = -1;
        ddlPaymentTypes.SelectedIndex = -1;
        ddlProvinces.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Provinces> l_Provinces = Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlProvinces, l_Provinces, "...", "0");
    }
    protected void ddlProvinces_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Districts> l_district = Districts.Static_GetList(short.Parse(ddlProvinces.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlDistricts, l_district, "...", "0");
    }
}