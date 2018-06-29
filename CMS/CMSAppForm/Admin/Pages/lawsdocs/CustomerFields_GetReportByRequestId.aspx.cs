using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_Pages_lawsdocs_CustomerFields_GetReportByRequestId : System.Web.UI.Page
{
    private int _messageSendRequestId = 0;
    private List<CustomerFields> ListCustomerFields = new List<CustomerFields>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["MessageSendRequestId"] != null && Request.Params["MessageSendRequestId"].ToString() != "")
        {
            _messageSendRequestId = int.Parse(Request.Params["MessageSendRequestId"].ToString());
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
            int rowCount = 0;
            ListCustomerFields = new CustomerFields().CustomerFields_GetReportByRequestId(_messageSendRequestId,
                txtSearch.Text, m_grid.PageSize, CustomPaging.PageIndex - 1, ref rowCount);
            m_grid.DataSource = ListCustomerFields;
            m_grid.AllowPaging = m_grid.PageSize <= rowCount;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", rowCount) != "" ? string.Format("{0:#,#}", rowCount) : "0");
            CustomPaging.TotalPage = (rowCount == 0) ? 1 :
                (rowCount % m_grid.PageSize == 0) ? rowCount / m_grid.PageSize :
                (rowCount - rowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            if (ListCustomerFields.Count <= 0)
            {
                lblMsg.Text = "Không có khách hàng nào.";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" +
                                        ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlCustomerName = (HyperLink)e.Row.FindControl("hlCustomerName");
            HyperLink hlContent = (HyperLink)e.Row.FindControl("hlContent");
            Label lblCustomerFullName = (Label)e.Row.FindControl("lblCustomerFullName");
            Label lblCustomerEmail = (Label)e.Row.FindControl("lblCustomerEmail");
            CustomerFields customerFields = (CustomerFields) e.Row.DataItem;
            Customers customer = Customers.Static_Get(customerFields.CustomerId);
            hlCustomerName.Text = customer.CustomerName;
            hlCustomerName.NavigateUrl = "~/admin/Pages/lawsdocs/CustomersEditFull.aspx?CustomerId=" + customer.CustomerId;
            hlCustomerName.Attributes.Add("target", "_blank");
            hlCustomerName.Attributes.Add("class", "FontBoldMaroon");
            lblCustomerFullName.Text = customer.CustomerFullName;
            lblCustomerEmail.Text = customer.CustomerMail;
            hlContent.NavigateUrl = "~/admin/Pages/lawsdocs/CustomerFields_GetContentMessageByCustomerIdAndRequestId.aspx?CustomerId=" + customer.CustomerId + "&MessageSendRequestId="+ _messageSendRequestId;
            hlContent.Attributes.Add("class", "popup");
        }
    }
}