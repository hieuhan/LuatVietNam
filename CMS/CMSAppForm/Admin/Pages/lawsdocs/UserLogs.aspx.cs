using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_UserLogs : System.Web.UI.Page
{
    int ActUserId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetListOrderBy("StatusDesc"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("UserLogs"), "");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }

    }

    private void BindData()
    {
        int RowCount = 0;
        l_Status = Status.Static_GetList();
        ICSoft.CMSLib.UserLogs m_UserLogs = new ICSoft.CMSLib.UserLogs();
        string m_OrderBy=ddlOrderBy.SelectedValue;
        string m_UserName = txtSearch.Text;
        byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
        string m_DateFrom = txtDateFrom.Text;
        string m_DateTo = txtDateTo.Text;
        m_grid.DataSource = m_UserLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1,m_OrderBy, m_UserName,"",m_StatusId, m_DateFrom, m_DateTo, ref RowCount);
        m_grid.DataBind();
        lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
        CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}