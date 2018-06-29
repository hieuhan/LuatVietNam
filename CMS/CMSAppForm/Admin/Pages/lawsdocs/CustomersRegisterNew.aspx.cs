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
using System.IO;
using System.Text;
using System.Data;
public partial class Admin_CustomersRegisterNew : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int CustomerId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            List<OrderByClauses> listoderbyCared = new List<OrderByClauses>();
            OrderByClauses mOrderByClauses0 = new OrderByClauses();
            mOrderByClauses0.OrderByDesc = "Tất cả";
            mOrderByClauses0.OrderBy = "0";
            listoderbyCared.Add(mOrderByClauses0);
            OrderByClauses mOrderByClauses1 = new OrderByClauses();
            mOrderByClauses1.OrderByDesc = "Đã liên hệ";
            mOrderByClauses1.OrderBy = "1";
            listoderbyCared.Add(mOrderByClauses1);
            OrderByClauses mOrderByClauses2 = new OrderByClauses();
            mOrderByClauses2.OrderByDesc = "Chưa liên hệ";
            mOrderByClauses2.OrderBy = "2";
            listoderbyCared.Add(mOrderByClauses2);
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId,0), "...");
            DropDownListHelpers.DDLCustomerGroups_BindByCulture(ddlCustomerGroups, CustomerGroups.Static_GetList(), "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetListOrderBy("StatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Customers"), "");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlCared, listoderbyCared, "");
            txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (Session["CustomersRegisterNew-ddlServices"] != null)
            {
                if (Session["CustomersRegisterNew-check"]!=null)
                {
                    ddlServices.SelectedValue = Session["CustomersRegisterNew-ddlServices"].ToString();
                    ddlCustomerGroups.SelectedValue = Session["CustomersRegisterNew-ddlCustomerGroups"].ToString();
                    ddlStatus.SelectedValue = Session["CustomersRegisterNew-ddlStatus"].ToString();
                    ddlOrderBy.SelectedValue = Session["CustomersRegisterNew-ddlOrderBy"].ToString();
                    ddlCared.SelectedValue = Session["CustomersRegisterNew-ddlCared"].ToString();
                    txtDateFrom.Text = Session["CustomersRegisterNew-DateFrom"].ToString();
                    txtDateTo.Text = Session["CustomersRegisterNew-DateTo"].ToString();
                    CustomPaging.PageIndex = int.Parse(Session["CustomersRegisterNew-PageIndex"].ToString());
                }
            }
            
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    protected void SetCurentData()
    {
        Session["CustomersRegisterNew-check"] = null;
        Session["CustomersRegisterNew-ddlServices"] = ddlServices.SelectedValue;
        Session["CustomersRegisterNew-ddlCustomerGroups"] = ddlCustomerGroups.SelectedValue;
        Session["CustomersRegisterNew-ddlStatus"] = ddlStatus.SelectedValue;
        Session["CustomersRegisterNew-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["CustomersRegisterNew-ddlCared"] = ddlCared.SelectedValue;
        Session["CustomersRegisterNew-DateFrom"] = txtDateFrom.Text;
        Session["CustomersRegisterNew-DateTo"] = txtDateTo.Text;
        Session["CustomersRegisterNew-PageIndex"] = CustomPaging.PageIndex;
    }
    private void ShowDatevalue()
    {
        if (rbtDate.SelectedValue == "1")
        {
            //Hôm nay
            txtDateFrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        if (rbtDate.SelectedValue == "2")
        {
            //Hôm trước
            DateTime Dateyesterday = default(DateTime);
            Dateyesterday =DateTime.Now.AddDays(-1);
            txtDateFrom.Text = Dateyesterday.ToString("dd/MM/yyyy");
            txtDateTo.Text = Dateyesterday.ToString("dd/MM/yyyy");

        }

        if (rbtDate.SelectedValue == "3")
        {
            //1 tuần
            DateTime weekStartDate = default(DateTime);
            DateTime weekEndDate = default(DateTime);
            weekStartDate =DateTime.Now.AddDays(-7);
            weekEndDate = DateTime.Now;
            txtDateFrom.Text = weekStartDate.ToString("dd/MM/yyyy");
            txtDateTo.Text = weekEndDate.ToString("dd/MM/yyyy");

        }
        if (rbtDate.SelectedValue == "4")
        {
            //2 tuần 
            DateTime weekStartDate = default(DateTime);
            DateTime weekEndDate = default(DateTime);
            weekStartDate = DateTime.Now.AddDays(-14);
            weekEndDate = DateTime.Now;
            txtDateFrom.Text = weekStartDate.ToString("dd/MM/yyyy");
            txtDateTo.Text = weekEndDate.ToString("dd/MM/yyyy");

        }
        if (rbtDate.SelectedValue == "5")
        {
            int currentMonth = DateTime.Now.Month;
            string maxDate = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + "/" + currentMonth.ToString("00") + "/" + DateTime.Now.Year;
            string minDate = "01/" + currentMonth.ToString("00") + "/" + DateTime.Now.Year;

            txtDateFrom.Text = minDate;
            txtDateTo.Text = maxDate;
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
            lblMsg.Text = "";
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
            string m_DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? "01/01/1990" : txtDateFrom.Text.Trim();
            string m_DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text.Trim(); 
            int m_CaredStatusId = short.Parse(ddlCared.SelectedValue);
            m_grid.DataSource = m_Customers.GetPageWithCaredID(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerName,m_CustomerFullName, m_CustomerNickName, 
                                                    m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId, 
                                                    m_ApplicationId,m_PlatformId, m_BusinessPartnerId, m_AppPlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId, 
                                                    0,m_InfoChannelId, m_RegisterNewsletter,"", m_DateFrom, m_DateTo, m_CaredStatusId,ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
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
            Customers m_DataItem = (Customers)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            CustomerId = m_DataItem.CustomerId;          
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Customers m_Customers = new Customers();
            m_Customers.CustomerId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Customers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Customers m_Customers = new Customers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Customers.CustomerId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Customers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlCustomerGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlCared_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void rbtDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (rbtDate.SelectedIndex != 0)
        {
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            ShowDatevalue();
            CustomPaging.PageIndex = 1;
            BindData();
        }
    }
    public static string getInfo(string CustomerId = "0")
    {
        string Info = "";
        Users m_Users = new Users();
        List<Users> l_Users = m_Users.GetAll();
        List<CustomerCare> list = CustomerCare.Static_GetListByCustomerId("", int.Parse(CustomerId));
        for (int i = 0; i < list.Count; i++)
        {
            Info += "<span> -<b style='color:#289ada;'> " + list[i].CrDateTime.ToString("dd/MM/yyyy HH:mm") + "<span style='color:Maroon;'> (" + UserHelpers.Static_Get(list[i].CrUserId, l_Users, "").Username + ")</span>" + ":</b> " + list[i].Info + "</span> <br />";
        }
        return Info;
    }
    public static string getInfoByExcel(string CustomerId = "0")
    {
        string Info = "";
        Users m_Users = new Users();
        List<Users> l_Users = m_Users.GetAll();
        List<CustomerCare> list = CustomerCare.Static_GetListByCustomerId("", int.Parse(CustomerId));
        for (int i = 0; i < list.Count; i++)
        {
            Info += "- "+list[i].Info + "</br>";
        }
        return Info;
    }
    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        DateTime m_DateFrom = DateTime.MinValue;
        DateTime m_DateTo = DateTime.Now;
        if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
        {
             m_DateFrom = DateTime.ParseExact(txtDateFrom.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);  
        }
        if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
        {
            m_DateTo = DateTime.ParseExact(txtDateTo.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        if (rbtDate.SelectedIndex == 5)
        {
            int currentMonth = m_DateFrom.Month;
            string maxDate = DateTime.DaysInMonth(m_DateFrom.Year, m_DateFrom.Month) + "/" + currentMonth.ToString("00") + "/" + m_DateFrom.Year;
            string minDate = "01/" + currentMonth.ToString("00") + "/" + m_DateFrom.Year;
            txtDateFrom.Text = minDate;
            txtDateTo.Text = maxDate;
        }
        else
        {
            rbtDate.SelectedIndex = 0;
            int result = DateTime.Compare(m_DateFrom, m_DateTo);
            if (result > 0)
            {
                lblMsg.Text = "Vui lòng nhập ngày hợp lệ!";
            }
            else
            {
                lblMsg.Text = "";
            }
        }
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
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
            lblMsg.Text = "";
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
            int m_CaredStatusId = short.Parse(ddlCared.SelectedValue);
            List<Customers> lCustomers = m_Customers.GetPageWithCaredID(ActUserId, 0, 0, m_OrderBy, m_CustomerName, m_CustomerFullName, m_CustomerNickName, 
                                                    m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId, 
                                                    m_ApplicationId,m_PlatformId, m_BusinessPartnerId, m_AppPlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId, 
                                                    0,m_InfoChannelId, m_RegisterNewsletter,"", m_DateFrom, m_DateTo, m_CaredStatusId,ref RowCount);
            DataTable dataTable = ToDataTable(lCustomers);
            ExportToExcel(dataTable);
            
        }
        
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }

    }
    private DataTable ToDataTable(List<Customers> lCustomers)
    {
        DataTable dataTable = new DataTable(typeof(Customers).Name);
        l_Status = Status.Static_GetList();
        if (lCustomers == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("Tên tài khoản", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));
        dataTable.Columns.Add("SĐT", typeof(string));
        dataTable.Columns.Add("Thông tin khách hàng", typeof(string));
        dataTable.Columns.Add("Cơ quan", typeof(string));
        dataTable.Columns.Add("Thời gian", typeof(string));
        dataTable.Columns.Add("CSKH", typeof(string));
        for (var index = 0; index < lCustomers.Count; index++) 
        {
            Customers mCustomers = lCustomers[index];
            DataRow rowAdded = dataTable.Rows.Add();
            string hoten = "Họ tên: " + mCustomers.CustomerFullName + "(" + mCustomers.MaxConcurrentLogin + ") <br />" ;
            string thongtinkh = "Địa chỉ: " + mCustomers.Address;
            string coquan = "Cơ quan: " + mCustomers.OrganName + "<br />SĐT: " + mCustomers.OrganPhone + "<br />Fax: " + mCustomers.OrganFax + "<br />Địa chỉ: " + mCustomers.OrganAddress;
            string thoigian = Status.Static_Get(byte.Parse(mCustomers.StatusId.ToString()), l_Status).StatusDesc + "<br />" + DateTimeHelpers.GetDateAndTime(mCustomers.CrDateTime);
            rowAdded.SetField("STT", index + 1);
            rowAdded.SetField("Tên tài khoản", mCustomers.CustomerName);
            rowAdded.SetField("Email", mCustomers.CustomerMail);
            rowAdded.SetField("SĐT", mCustomers.CustomerMobile);
            rowAdded.SetField("Thông tin khách hàng", hoten+thongtinkh);
            rowAdded.SetField("Cơ quan", coquan);
            rowAdded.SetField("Thời gian", thoigian);
            rowAdded.SetField("CSKH", getInfoByExcel(mCustomers.CustomerId.ToString()));
        }
        return dataTable;
    }
    private void ExportToExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=CSKH_MoiDK.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        HttpContext.Current.Response.Write(string.Format("<strong style='text-align: center;'>Báo cáo CSKH mới đăng ký từ {0} đến {1}</strong>", txtDateFrom.Text, txtDateTo.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text));

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
        HttpContext.Current.Response.Write("Email");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("SĐT");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thông tin khách hàng");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Cơ quan");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thời gian");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("CSKH");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    
}

