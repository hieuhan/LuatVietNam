using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_FaqViewLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int FaqId = 0;
    //protected byte LanguageId = 0;
    //protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("FaqViewLogs"), "");
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
            //Users m_Users = new Users();
            //l_Users = m_Users.GetAll();
            FaqViewLogs m_FaqViewLogs = new FaqViewLogs();
            int m_FaqId = 0;
            int m_CustomerId = 0;
            string m_RefererFrom = txtSearch.Text.Trim();
            string m_UserAgent = txtSearch.Text.Trim();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            string m_FromIP = txtSearch.Text.Trim();
            m_grid.DataSource = m_FaqViewLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_FaqId,m_RefererFrom,m_UserAgent, m_CustomerId,m_FromIP,m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;           
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
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

