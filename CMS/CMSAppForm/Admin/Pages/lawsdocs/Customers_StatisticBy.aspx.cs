using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_Customers_StatisticBy : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Genders> l_Genders = new List<Genders>();
    protected List<Occupations> l_Occupations = new List<Occupations>();
    protected List<InfoChannels> l_InfoChannels = new List<InfoChannels>();
    protected List<CustomerGroups> lCustomerGroups = new List<CustomerGroups>();
    protected List<InfoChannels> lInfoChannels = new List<InfoChannels>();
    protected List<Occupations> lOccupations = new List<Occupations>();
    protected List<Provinces> lProvinces = new List<Provinces>();
    protected List<Services> lServices = new List<Services>();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetList(), "...");
            RadioButtonListHelpers.Bind(rblGenders, Genders.Static_GetList(), "Tất cả");
            RadioButtonListHelpers.SetSelected(rblGenders, "0");
            DropDownListHelpers.DDLProvinces_BindByCulture(ddlProvinces, Provinces.Static_GetList(0), "...");
            DropDownListHelpers.DDLInfoChannels_BindByCulture(ddlInfoChannels, InfoChannels.Static_GetList(), "...");
            DropDownListHelpers.DDLOccupations_BindByCulture(ddlOccupations, Occupations.Static_GetList(), "...");
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            int TotalCount = 0;
            DataSet ds;
            Customers m_Customers = new Customers();
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = byte.Parse(rblGenders.SelectedValue);
            short m_ProvinceId = short.Parse(ddlProvinces.SelectedValue);
            short m_OccupationId = short.Parse(ddlOccupations.SelectedValue);
            short m_InfoChannelId = short.Parse(ddlInfoChannels.SelectedValue);
            byte m_RegisterNewsletter = byte.Parse(rblRegisterNewsletter.SelectedValue);
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            m_Customers.SiteId = short.Parse(ddlSite.SelectedValue);
            lCustomerGroups = CustomerGroups.Static_GetList();
            lInfoChannels = InfoChannels.Static_GetList();
            lOccupations = Occupations.Static_GetList();
            lProvinces = Provinces.Static_GetList(1);
            lServices = Services.Static_GetList(0, short.Parse(ddlSite.SelectedValue));
            switch (ddlSearchBy.SelectedValue)
            {
                case "DailyLogin":
                    //thống kê theo login
                    ds = m_Customers.Customers_StatisticByDailyLogin(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "MonthlyLogin":
                    //thống kê theo tháng login
                    ds = m_Customers.Customers_StatisticByMonthlyLogin(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "CrDateTime":
                    //thống kê theo ngày tạo
                    ds = m_Customers.Customers_StatisticByCrDateTime(ActUserId,m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "GroupId":
                    //Thống kê theo nhóm
                    ds = m_Customers.Customers_StatisticByGroupId(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "InfoChannelId":
                    //Thống kê theo kênh thông tin
                    ds = m_Customers.Customers_StatisticByInfoChannelId(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "Month":
                    //Thống kê theo tháng
                    ds = m_Customers.Customers_StatisticByMonth(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "OccupationId":
                    //Thống kê theo nghề nghiệp
                    ds = m_Customers.Customers_StatisticByOccupationId(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                case "ProvinceId":
                    //Thống kê theo tỉnh thành
                    ds = m_Customers.Customers_StatisticByProvinceId(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
                default:
                    //Thống kê theo dịch vụ
                    ds = m_Customers.Customers_StatisticByServiceId(ActUserId, m_ServiceId, m_StatusId, m_GenderId, m_ProvinceId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, DateFrom, DateTo, ref TotalCount);
                    break;
            }
            m_grid.DataSource = ds;
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", TotalCount) != "" ? string.Format("{0:#,#}", TotalCount) : "0");
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Start Header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime" || ddlSearchBy.SelectedValue == "DailyLogin")
            {
                e.Row.Cells[0].Text = "Ngày";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }

            if (ddlSearchBy.SelectedValue == "GroupId")
            {
                e.Row.Cells[0].Text = "Nhóm khách hàng";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }
            if (ddlSearchBy.SelectedValue == "InfoChannelId")
            {
                e.Row.Cells[0].Text = "Kênh thông tin";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }
            if (ddlSearchBy.SelectedValue == "Month" || ddlSearchBy.SelectedValue == "MonthlyLogin")
            {
                e.Row.Cells[0].Text = "Tháng";
                e.Row.Cells[1].Text = "Năm";
                e.Row.Cells[2].Text = "Số lượng khách hàng";
            }
            if (ddlSearchBy.SelectedValue == "OccupationId")
            {
                e.Row.Cells[0].Text = "Nghề nghiệp";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }
            if (ddlSearchBy.SelectedValue == "ProvinceId")
            {
                e.Row.Cells[0].Text = "Tỉnh thành";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }
            if (ddlSearchBy.SelectedValue == "ServiceId")
            {
                e.Row.Cells[0].Text = "Dịch vụ";
                e.Row.Cells[1].Text = "Số lượng khách hàng";
            }
        }
        // End Header

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = ((DateTime)row[0]).ToString("dd/MM/yyyy");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?CrDateTime={0}", ((DateTime)row[0]).ToString("dd-MM-yyyy"));
                hyperLink.ToolTip = string.Format("Thống kê khách hàng từ ngày {0}", ((DateTime)row[0]).ToString("dd/MM/yyyy"));
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "GroupId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                CustomerGroups mCustomerGroup = lCustomerGroups.FirstOrDefault(x => x.CustomerGroupDesc.Equals(row[0]));
                string GroupId = mCustomerGroup == null ? "0" : mCustomerGroup.CustomerGroupId.ToString();
                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?GroupId={0}", GroupId);
                hyperLink.ToolTip = @"Thống kê khách hàng theo nhóm";
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "InfoChannelId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                InfoChannels mInfoChannel = lInfoChannels.FirstOrDefault(x => x.InfoChannelDesc.Equals(row[0]));
                string InfoChannelId = mInfoChannel == null ? "0" : mInfoChannel.InfoChannelId.ToString();
                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?InfoChannelId={0}", InfoChannelId);
                hyperLink.ToolTip = @"Thống kê khách hàng theo kênh thông tin";
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "Month")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = ((string)
                string.Format("{0:#,#}", row[2]).ToString());
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "OccupationId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                Occupations mOccupation = lOccupations.FirstOrDefault(x => x.OccupationDesc.Equals(row[0]));
                string OccupationId = mOccupation == null ? "0" : mOccupation.OccupationId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?OccupationId={0}", OccupationId);
                hyperLink.ToolTip = @"Thống kê khách hàng theo nghề nghiệp";
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "ProvinceId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                Provinces mProvince = lProvinces.FirstOrDefault(x => x.ProvinceDesc.Equals(row[0]));
                string ProvinceId = mProvince == null ? "0" : mProvince.ProvinceId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?ProvinceId={0}", ProvinceId);
                hyperLink.ToolTip = @"Thống kê khách hàng theo tỉnh thành";
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "ServiceId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                Services mServices = lServices.FirstOrDefault(x => x.ServiceDesc.Equals(row[0]));
                string ServiceId = mServices == null ? "0" : mServices.ServiceId.ToString();
                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                HyperLink hyperLink = new HyperLink();
                hyperLink.NavigateUrl = string.Format("~/admin/Pages/lawsdocs/CustomersFull.aspx?ServiceId={0}", ServiceId);
                hyperLink.ToolTip = @"Thống kê khách hàng theo tỉnh dịch vụ";
                hyperLink.Text = ((string)string.Format("{0:#,#}", row[1]));
                e.Row.Cells[1].Controls.Add(hyperLink);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
   protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

   protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }

   protected void rblGenders_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlProvinces_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlOccupations_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlInfoChannels_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
   {
       DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
       BindData();
   }
}

