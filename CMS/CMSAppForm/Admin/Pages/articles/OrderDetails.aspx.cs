using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;


public partial class Admin_Pages_articles_OrderDetails : System.Web.UI.Page
{
    protected int ActUserId = 0;
    private int OrderId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OrderId"] != null && Request.Params["OrderId"].ToString() != "")
        {
            OrderId = int.Parse(Request.Params["OrderId"].ToString());
        }
        if (!IsPostBack)
        {
            lblPositionName.Text = Orders.Static_Get(OrderId).OrderName.ToString();
            BindData();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------
    private void BindData()
    {
        try
        {
            int RowAmount = 200;
            int PageIndex = 0;
            string OrderBy = "";
            int RowCount = 0;
            OrderDetails m_OrderDetails = new OrderDetails();
            m_OrderDetails.OrderId = OrderId;
            m_grid.DataSource = m_OrderDetails.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();

            Orders m_Orders = new Orders();
            m_Orders = m_Orders.Get(OrderId);
            if (m_Orders.OrderId > 0)
            {
                txtOrderDesc.Text = m_Orders.OrderDesc.ToString();
                txtOrderCode.Text = m_Orders.OrderCode.ToString();
                txtCustomerId.Text = m_Orders.CustomerId.ToString();
                txtFullName.Text = m_Orders.FullName.ToString();
                txtAddress.Text = m_Orders.Address.ToString();
                txtMobile.Text = m_Orders.Mobile.ToString();
                txtEmail.Text = m_Orders.Email.ToString();
                txtCouponCode.Text = m_Orders.CouponCode.ToString();
                txtCouponValue.Text = m_Orders.CouponValue.ToString("N0");
                txtRequireInvoice.Text = m_Orders.RequireInvoice == 1 ? "Có" : "Không";
                txtCompanyName.Text = m_Orders.CompanyName.ToString();
                txtCompanyAddress.Text = m_Orders.CompanyAddress.ToString();
                txtCompanyTaxCode.Text = m_Orders.CompanyTaxCode.ToString();
                txtOrderValue.Text = m_Orders.OrderValue.ToString("N0");
                txtDeliveryFee.Text = m_Orders.DeliveryFee.ToString("N0");
                txtDeliveryNote.Text = m_Orders.DeliveryNote.ToString();
                txtFromIP.Text = m_Orders.FromIP.ToString();
                txtPaymentType.Text = PaymentTypes.Static_Get(m_Orders.PaymentTypeId).PaymentTypeDesc;
                txtOrderStatus.Text = OrderStatus.Static_Get(m_Orders.OrderStatusId).OrderStatusDesc;
                txtDeliveryStatus.Text = DeliveryStatus.Static_Get(m_Orders.DeliveryStatusId).DeliveryStatusDesc;
                txtDistrict.Text = Districts.Static_Get(m_Orders.DistrictId).DistrictDesc;
                txtProvince.Text = Provinces.Static_Get(m_Orders.ProvinceId).ProvinceDesc;
                txtPartners.Text = Partners.Static_Get(m_Orders.PartnerId).PartnerDesc;
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            OrderDetails m_OrderDetails = new OrderDetails();
            m_OrderDetails.OrderDetailId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_OrderDetails.OrderId = OrderId;
            SysMessageTypeId = m_OrderDetails.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    //-----------------------------------------------------------------------------------------------------------------

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    //-----------------------------------------------------------------------------------------------------------------

}