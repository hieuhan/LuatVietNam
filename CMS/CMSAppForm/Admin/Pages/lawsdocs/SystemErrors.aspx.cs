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

public partial class Admin_SystemErrors : System.Web.UI.Page
{
    int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
        if (!IsPostBack)
        {
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
        SystemErrors m_SystemErrors;
        if (ddlDb.SelectedValue == "1")
        {
            m_SystemErrors = new SystemErrors(ConfigurationManager.AppSettings["DOC_CONSTR"]);
        }
        else
        {
            m_SystemErrors = new SystemErrors(ConfigurationManager.AppSettings["CMS_CONSTR"]);
        }
        string m_SystemErrorInfo = txtSearch.Text;
        string m_DateFrom = txtDateFrom.Text;
        string m_DateTo = txtDateTo.Text;
        m_grid.DataSource = m_SystemErrors.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1,"", m_SystemErrorInfo, m_DateFrom, m_DateTo, ref RowCount);
        m_grid.DataBind();
        lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
        CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlDb_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}