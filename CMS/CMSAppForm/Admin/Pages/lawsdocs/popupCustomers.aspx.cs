using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.admin.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_popupCustomers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int CustomerId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Users> l_Users = new List<Users>();
    protected List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        hdfParent.Value = Request["parent"] == null ? "" : Request["parent"].ToString();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLCustomerGroups_BindByCulture(ddlCustomerGroups, CustomerGroups.Static_GetList(), "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetListOrderBy("StatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Customers"), "");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Status = Status.Static_GetList();
            l_CustomerGroups = CustomerGroups.Static_GetList();
            Customers m_Customers = new Customers();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_CustomerName = "";
            string m_CustomerMail = "";
            string m_CustomerFullName = "";
            string m_CustomerMobile = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "CustomerName":
                    m_CustomerName = txtSearch.Text.Trim();
                    break;
                case "CustomerMail":
                    m_CustomerMail = txtSearch.Text.Trim();
                    break;
                case "CustomerFullName":
                    m_CustomerFullName = txtSearch.Text.Trim();
                    break;
                default:
                    m_CustomerMobile = txtSearch.Text.Trim();
                    break;
            }
            string m_CustomerNickName = "";
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = 0;
            short m_ProvinceId = 0;
            int m_BusinessApplicationPlatformId = 0;
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            short m_BusinessPartnerId = 0;
            short m_AppPlatformId = 0;
            int m_MapCustomerId = 0;
            short m_CustomerGroupId = short.Parse(ddlCustomerGroups.SelectedValue);
            short m_OccupationId = 0;
            short m_InfoChannelId = 0;
            byte m_RegisterNewsletter = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_Customers.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Customers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,
                m_CustomerName, m_CustomerFullName, m_CustomerNickName, m_CustomerMail, m_ServiceId, m_StatusId,
                m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId, m_ApplicationId,
                m_PlatformId, m_BusinessPartnerId, m_PlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId,
                m_InfoChannelId, 0, m_RegisterNewsletter, "", m_DateFrom, m_DateTo, ref RowCount);
                
                
                //ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerName, m_CustomerFullName, m_CustomerNickName,
                //                                    m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId,
                //                                    m_ApplicationId, m_PlatformId, m_BusinessPartnerId, m_AppPlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId,
                //                                    m_InfoChannelId, m_RegisterNewsletter, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string CrUserName = DataBinder.Eval(e.Row.DataItem, "CustomerName").ToString();
                CustomerId = (int)m_grid.DataKeys[e.Row.RowIndex].Value;
                Label lblChkSelect = (Label)(e.Row.FindControl("lblChkSelect"));
                if (lblChkSelect == null)
                {
                    return;
                }
                lblChkSelect.Text = "<input type='radio' name='ckuserid' onclick='itemCheck(this, " + CustomerId + " , \"" + CrUserName + "\")' />";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}