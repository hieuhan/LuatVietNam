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
using sms.common;
using Countries = ICSoft.CMSLib.Countries;
using Provinces = ICSoft.CMSLib.Provinces;

public partial class Admin_Pages_articles_Orders : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int OrderId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected List<OrderStatus> l_OrderStatus = new List<OrderStatus>();
    protected List<DeliveryStatus> l_DeliveryStatus = new List<DeliveryStatus>();
    protected List<PaymentTypes> l_PaymentTypes = new List<PaymentTypes>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", "0");
            DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "...");
            //DropDownListHelpers.DDL_Bind(dllCustomers, Customers.Static_Get(),"...";
            DropDownListHelpers.DDL_Bind(ddlDistricts, Districts.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlPaymentTypes, PaymentTypes.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlOrderStatus, OrderStatus.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlDeliveryStatus, DeliveryStatus.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlPartners, Partners.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Orders"), "");
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_OrderStatus = OrderStatus.Static_GetList();
            l_DeliveryStatus = DeliveryStatus.Static_GetList();
            l_PaymentTypes = PaymentTypes.Static_GetList();

            Orders m_Orders = new Orders();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_OrderName = txtContentSearch.Text.Trim();
            m_Orders.CountryId = short.Parse(ddlCountries.SelectedValue);
            m_Orders.ProvinceId = short.Parse(ddlProvinces.SelectedValue);
            m_Orders.PaymentTypeId = byte.Parse(ddlPaymentTypes.SelectedValue);
            m_Orders.DistrictId = short.Parse(ddlDistricts.SelectedValue);
            m_Orders.OrderStatusId = byte.Parse(ddlOrderStatus.SelectedValue);
            m_Orders.DeliveryStatusId = byte.Parse(ddlDeliveryStatus.SelectedValue);
            m_Orders.PartnerId = short.Parse(ddlPartners.SelectedValue);
            m_Orders.FullName = m_OrderName;
            string DateFrom = txtDateFrom.Text.Trim();
            string DateTo = txtDateTo.Text.Trim();
            m_Orders.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Orders.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1,m_OrderBy, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------

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
    //----------------------------------------------------------------------------------------------------------------------------

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
        try
        {
            Label lblOrderName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblOrderName");
            Orders m_Orders = new Orders();
            m_Orders.OrderId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Orders.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1) //error
            {
                messages = string.Format("Lỗi khi xóa đơn hàng : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2) // success
            {
                messages = string.Format("Xóa đơn hàng <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblOrderName.Text) ? lblOrderName.Text : string.Empty); ;
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            Orders m_Orders = new Orders();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Orders.OrderId = Int16.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Orders.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " đơn hàng.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> đơn hàng chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlProvinces_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlDistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void dllPaymentTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlDeliveryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlPartners_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}