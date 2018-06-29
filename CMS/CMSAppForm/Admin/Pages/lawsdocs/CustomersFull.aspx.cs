using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_CustomersFull : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int CustomerId = 0;
    protected short GroupId = 0, InfoChannelId = 0, OccupationId, ProvinceId = 0, ServiceId = 0;
    protected string CrDatetime = string.Empty;
    private const string pattern = "dd-MM-yyyy";
    private DateTime parsedDate;
    protected List<Status> l_Status = new List<Status>();
    protected List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CrDateTime"] != null && Request.Params["CrDateTime"].ToString() != "")
        {
            CrDatetime = Request.Params["CrDateTime"].ToString();
            CrDatetime = DateTime.TryParseExact(CrDatetime, pattern, null, DateTimeStyles.None, out parsedDate) ? parsedDate.ToString("dd-MM-yyyy") : string.Empty;
        }
        if (Request.Params["GroupId"] != null && Request.Params["GroupId"].ToString() != "")
        {
            GroupId = short.Parse(Request.Params["GroupId"].ToString());
        }
        if (Request.Params["InfoChannelId"] != null && Request.Params["InfoChannelId"].ToString() != "")
        {
            InfoChannelId = short.Parse(Request.Params["InfoChannelId"].ToString());
        }
        if (Request.Params["OccupationId"] != null && Request.Params["OccupationId"].ToString() != "")
        {
            OccupationId = short.Parse(Request.Params["OccupationId"].ToString());
        }
        if (Request.Params["ProvinceId"] != null && Request.Params["ProvinceId"].ToString() != "")
        {
            ProvinceId = short.Parse(Request.Params["ProvinceId"].ToString());
        }
        if (Request.Params["ServiceId"] != null && Request.Params["ServiceId"].ToString() != "")
        {
            ServiceId = short.Parse(Request.Params["ServiceId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLCustomerGroups_BindByCulture(ddlCustomerGroups, CustomerGroups.Static_GetList(), "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetListOrderBy("StatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Customers"), "");
            if (Session["Customers-ddlServices"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("CustomersEditFull.aspx")))
            {
                ddlServices.SelectedValue=Session["Customers-ddlServices"].ToString();
                ddlCustomerGroups.SelectedValue=Session["Customers-ddlCustomerGroups"].ToString();
                ddlStatus.SelectedValue=Session["Customers-ddlStatus"].ToString();
                ddlOrderBy.SelectedValue=Session["Customers-ddlOrderBy"].ToString();
                ddlSearchByValue.SelectedValue = Session["Customers-ddlSearchByValue"].ToString();
                ddlSearchByDate.SelectedValue = Session["Customers-ddlSearchByDate"].ToString();
                txtDateFrom.Text = Session["Customers-txtDateFrom"].ToString();
                txtDateTo.Text = Session["Customers-txtDateTo"].ToString();
                txtSearch.Text = Session["Customers-txtSearch"].ToString();
                rblFindTypes.SelectedValue = Session["Customers-rblFindTypes"].ToString();
                txtDateTo.Text = Session["Customers-txtDateTo"].ToString();
                txtSearch.Text = Session["Customers-txtSearch"].ToString();
                txtValueFrom.Text = Session["Customers-txtValueFrom"].ToString();
                txtValueTo.Text = Session["Customers-txtValueTo"].ToString();
                rblFindTypes.SelectedValue = Session["Customers-rblFindTypes"].ToString();
            }
            //BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    protected void SetCurentData()
    {
        Session["Customers-ddlServices"] = ddlServices.SelectedValue;
        Session["Customers-ddlCustomerGroups"] = ddlCustomerGroups.SelectedValue;
        Session["Customers-ddlStatus"] = ddlStatus.SelectedValue;
        Session["Customers-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["Customers-ddlSearchByValue"] = ddlSearchByValue.SelectedValue;
        Session["Customers-ddlSearchByDate"] = ddlSearchByDate.SelectedValue;
        Session["Customers-txtDateFrom"] = txtDateFrom.Text;
        Session["Customers-txtDateTo"] = txtDateTo.Text;
        Session["Customers-txtSearch"] = txtSearch.Text;
        Session["Customers-txtValueFrom"] = txtValueFrom.Text;
        Session["Customers-txtValueTo"] = txtValueTo.Text;
        Session["Customers-rblFindTypes"] = rblFindTypes.SelectedValue;
    }
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Status = Status.Static_GetList();
            l_CustomerGroups = CustomerGroups.Static_GetList();
            CustomerCount m_CustomerCount = new CustomerCount();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_Keyword = "";
            string m_CustomerName = "";
            string m_CustomerMail = "";
            string m_CustomerFullName = "";
            string m_CustomerMobile = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "All":
                    m_Keyword = txtSearch.Text.Trim();
                    break;
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

            if (GroupId > 0)
            {
                ddlCustomerGroups.ClearSelection();
                ddlCustomerGroups.Items.FindByValue(GroupId.ToString()).Selected = true;
            }
            else GroupId = short.Parse(ddlCustomerGroups.SelectedValue);

            if (ServiceId > 0)
            {
                ddlServices.ClearSelection();
                ddlServices.Items.FindByValue(ServiceId.ToString()).Selected = true;
            }
            else ServiceId = short.Parse(ddlServices.SelectedValue);


            if (!string.IsNullOrEmpty(CrDatetime))
            {
                ddlSearchByDate.ClearSelection();
                ddlSearchByDate.Items.FindByValue("CrDateTime").Selected = true;
                txtDateFrom.Text = CrDatetime.Replace("-", "/");
                //txtDateTo.Text = CrDatetime.Replace("-", "/");
            }

            string m_CustomerNickName = "";
            short m_ServiceId = ServiceId; //short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = 0;
            short m_ProvinceId = ProvinceId;
            short m_CustomerGroupId = GroupId;// short.Parse(ddlCustomerGroups.SelectedValue);
            short m_OccupationId = OccupationId;
            short m_InfoChannelId = InfoChannelId;
            byte m_RegisterNewsletter = 0;
            string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            string m_SearchByValue = ddlSearchByValue.SelectedValue.ToString();
            int m_FromValue =(txtValueFrom.Text!="") ? int.Parse(txtValueFrom.Text) : 0;
            int m_ToValue =(txtValueTo.Text!="") ? int.Parse(txtValueTo.Text) : 0;
            string m_GroupCustomer = ddlGroupCustomer.SelectedValue;
            m_CustomerCount.SiteId = short.Parse(ddlSite.SelectedValue);
            List<CustomerCount> l_CustomerCount = m_CustomerCount.CustomerCount_GetPage_Keyword(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_Keyword, m_CustomerName, m_CustomerFullName, 
                                                                  m_CustomerNickName, m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, 
                                                                  m_CustomerGroupId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter,m_SearchByDate, m_DateFrom, m_DateTo,
                                                                  m_SearchByValue, m_FromValue, m_ToValue, ref RowCount, m_GroupCustomer);

            m_grid.DataSource = l_CustomerCount;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0";
            CustomPaging.TotalPage = RowCount == 0 ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            //ToDo Lọc theo khách hàng đóng phí thì lấy tổng tiền theo gói dịch vụ
            if (m_GroupCustomer.Equals("CustomersPayAFee"))
            {
                DateTime df, dt;
                if (!String.IsNullOrEmpty(m_DateFrom))
                    df = sms.utils.StringUtil.ConvertToDateTime(m_DateFrom);
                else
                    df = DateTime.Now.AddYears(-100);
                if (!String.IsNullOrEmpty(m_DateTo))
                    dt = sms.utils.StringUtil.ConvertToDateTime(m_DateTo);
                else
                    dt = DateTime.Now;
                var ds = new PaymentTransactions().TotalAmountByServicePackage(df,dt);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var totalAmount = ds.Tables[0].Compute("Sum(TotalAmount)", "");
                    ListItemCollection listItems =
                        new ListItemCollection { new ListItem("Số tiền giao dịch", ""), new ListItem(string.Format("Tất cả: {0:#,##} VNĐ", totalAmount), "") };
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string ServiceDesc = "Dịch vụ cũ (VB,VBE,HL,SMS)";
                        if (row["ServiceDesc"] != DBNull.Value)
                            ServiceDesc = row["ServiceDesc"].ToString();
                        listItems.Add(new ListItem { Text = string.Format("{0} - {1:#,##} VNĐ", ServiceDesc, row["TotalAmount"]), Value = "" });
                    }
                    ddlTotalAmount.DataSource = listItems;
                    ddlTotalAmount.DataBind();
                    ddlTotalAmount.Visible = true;
                }
                else ddlTotalAmount.Visible = false;
            }
            else ddlTotalAmount.Visible = false;
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
            Label lblServiceEndTime = (Label)e.Row.FindControl("lblServiceEndTime");
            if (lblServiceEndTime.Text != "")
            {
                DateTime m_ServiceEndTime = DateTime.Parse(lblServiceEndTime.Text, System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN"));
                if (m_ServiceEndTime > DateTime.Now)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                    e.Row.Cells[0].ToolTip = "KH còn hạn dịch vụ";
                }
                else if (m_ServiceEndTime <= DateTime.Now)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[0].ToolTip = "KH hết hạn dịch vụ";
                }
            }
            
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFalseByStatus = 0;
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
                        CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Xóa thành công khách hàng.";

        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_PENDING);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        BindData();
    }

    private void Review_Click(byte StatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
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
                        m_Customers.StatusId = StatusId;
                        SysMessageTypeId = m_Customers.UpdateStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Duyệt thành công " + CountSuc.ToString() + " khách hàng. ";
        if (StatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " khách hàng. ";
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }
    protected void ddlSearchByValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSearchByDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            CustomerServices m_CustomerServices = new CustomerServices();
            m_CustomerServices.CustomerServiceId = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_CustomerServices.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int CustomerServiceId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
       
        short SysMessageId = 0;
        CustomerServices m_CustomerServices = new CustomerServices();
        m_CustomerServices.CustomerServiceId = CustomerServiceId;
        m_CustomerServices.UpdateStatusId(1);
        GridView1.EditIndex = -1;
        BindData();
    }
    
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int RowCount = 0;
            l_Status = Status.Static_GetList();
            l_CustomerGroups = CustomerGroups.Static_GetList();
            CustomerCount m_CustomerCount = new CustomerCount();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_Keyword = "";
            string m_CustomerName = "";
            string m_CustomerMail = "";
            string m_CustomerFullName = "";
            string m_CustomerMobile = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "All":
                    m_Keyword = txtSearch.Text.Trim();
                    break;
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

            if (GroupId > 0)
            {
                ddlCustomerGroups.ClearSelection();
                ddlCustomerGroups.Items.FindByValue(GroupId.ToString()).Selected = true;
            }
            else GroupId = short.Parse(ddlCustomerGroups.SelectedValue);

            if (ServiceId > 0)
            {
                ddlServices.ClearSelection();
                ddlServices.Items.FindByValue(ServiceId.ToString()).Selected = true;
            }
            else ServiceId = short.Parse(ddlServices.SelectedValue);


            if (!string.IsNullOrEmpty(CrDatetime))
            {
                ddlSearchByDate.ClearSelection();
                ddlSearchByDate.Items.FindByValue("CrDateTime").Selected = true;
                txtDateFrom.Text = CrDatetime.Replace("-", "/");
                //txtDateTo.Text = CrDatetime.Replace("-", "/");
            }

            string m_CustomerNickName = "";
            short m_ServiceId = ServiceId; //short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = 0;
            short m_ProvinceId = ProvinceId;
            short m_CustomerGroupId = GroupId;// short.Parse(ddlCustomerGroups.SelectedValue);
            short m_OccupationId = OccupationId;
            short m_InfoChannelId = InfoChannelId;
            byte m_RegisterNewsletter = 0;
            string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            string m_SearchByValue = ddlSearchByValue.SelectedValue.ToString();
            int m_FromValue = (txtValueFrom.Text != "") ? int.Parse(txtValueFrom.Text) : 0;
            int m_ToValue = (txtValueTo.Text != "") ? int.Parse(txtValueTo.Text) : 0;
            string m_GroupCustomer = ddlGroupCustomer.SelectedValue;
            m_CustomerCount.SiteId = short.Parse(ddlSite.SelectedValue);
            List<CustomerCount> l_CustomerCount = m_CustomerCount.CustomerCount_GetPage_Keyword(ActUserId, 0, 0, m_OrderBy, m_Keyword, m_CustomerName, m_CustomerFullName,
                                                                  m_CustomerNickName, m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId,
                                                                  m_CustomerGroupId, m_OccupationId, m_InfoChannelId, m_RegisterNewsletter, m_SearchByDate, m_DateFrom, m_DateTo,
                                                                  m_SearchByValue, m_FromValue, m_ToValue, ref RowCount, m_GroupCustomer);
            DataTable dataTable = ToDataTable(l_CustomerCount);
            ExportToExcel(dataTable);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private DataTable ToDataTable(List<CustomerCount> l_CustomerCount)
    {
        DataTable dataTable = new DataTable(typeof(CustomerCount).Name);
        l_Status = Status.Static_GetList();
        if (l_CustomerCount == null) return dataTable;
        dataTable.Columns.Add("STT", typeof(string));
        dataTable.Columns.Add("Tên tài khoản", typeof(string));
        dataTable.Columns.Add("Thông tin khách hàng", typeof(string));
        dataTable.Columns.Add("Thông tin dịch vụ", typeof(string));
        dataTable.Columns.Add("Thông tin giao dịch", typeof(string));
        dataTable.Columns.Add("Thông tin truy cập", typeof(string));
        dataTable.Columns.Add("Trạng thái", typeof(string));
        for (var index = 0; index < l_CustomerCount.Count; index++)
        {
            CustomerCount mCustomers = l_CustomerCount[index];
            string thongtinkhachhang = "Họ và tên: " + mCustomers.CustomerFullName + "<br />Email: " + mCustomers.CustomerMail;
            string thongtindichvu= mCustomers.ServiceDetail +"<br />Bắt đầu DV: " + DateTimeHelpers.GetDateHH24(mCustomers.ServiceBeginTime) +"<br />Hết hạn DV: " +DateTimeHelpers.GetDateHH24(mCustomers.ServiceEndTime);
            string thongtingiaodich= "Số lần GD: "+ mCustomers.PaymentCount+"<br />GD đầu: " + mCustomers.FirstPaymentTime+"<br />GD cuối: " + mCustomers.LastPaymentTime+"<br />Tổng tiền: " + mCustomers.TotalPaymentMoney+"<br />Số tiền GD cuối: " +mCustomers.LastPaymentMoney ;
            string tongtintruycap="Đăng ký: "+ DateTimeHelpers.GetDateAndTime(mCustomers.CrDateTime)+"<br />Số lần login: "+mCustomers.LoginCount+"<br />Login cuối: "+DateTimeHelpers.GetDateAndTime(mCustomers.LastLoginTime)+"<br />Số lần đổi MK: "+mCustomers.ChangePassCount+"<br />Đổi MK cuối: "+mCustomers.LastChangePassTime;
            DataRow rowAdded = dataTable.Rows.Add();
            rowAdded.SetField("STT", index + 1);
            rowAdded.SetField("Tên tài khoản", mCustomers.CustomerName);
            rowAdded.SetField("Thông tin khách hàng", thongtinkhachhang);
            rowAdded.SetField("Thông tin dịch vụ", thongtindichvu);
            rowAdded.SetField("Thông tin giao dịch", thongtingiaodich);
            rowAdded.SetField("Thông tin truy cập", tongtintruycap);
            rowAdded.SetField("Trạng thái",Status.Static_Get(byte.Parse(mCustomers.StatusId.ToString()), l_Status).StatusDesc);
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
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=DanhSachKhachHang.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
        HttpContext.Current.Response.Write(string.Format("<strong style='text-align: center;'>Báo cáo thành viên từ {0} đến {1}</strong>", txtDateFrom.Text == "" ? "ngày bắt đầu" : txtDateFrom.Text, txtDateTo.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtDateTo.Text));
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
        HttpContext.Current.Response.Write("Thông tin khách hàng");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thông tin dịch vụ");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thông tin giao dich");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Thông tin truy cập");
        HttpContext.Current.Response.Write("</b>");
        HttpContext.Current.Response.Write("</td>");

        HttpContext.Current.Response.Write("<td>");
        HttpContext.Current.Response.Write("<b>");
        HttpContext.Current.Response.Write("Trạng thái");
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
}

