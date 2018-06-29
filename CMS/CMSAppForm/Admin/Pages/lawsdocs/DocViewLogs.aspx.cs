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
public partial class Admin_DocViewLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocViewLogId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    private List<ApplicationTypes> l_ApplicationTypes = new List<ApplicationTypes>();
    protected List<ActionTypes> l_ActionTypes = new List<ActionTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            AppTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
       
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "...", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "...", AppTypeId.ToString());
            DropDownListHelpers.DDLActionTypes_BindByCulture(ddlActionTypes, ActionTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DocViewLogs"), "");
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
            int RowCount=0;
            l_Language = Languages.Static_GetList();
            l_ApplicationTypes = ApplicationTypes.Static_GetList();
            l_ActionTypes = ActionTypes.Static_GetList();

            DocViewLogs m_DocViewLogs = new DocViewLogs();
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue;
            int m_DocId = 0;
            int m_DocFileId = 0;
            string m_FromIP = txtFromIP.Text.Trim();
            byte m_ActionTypeId = byte.Parse(ddlActionTypes.SelectedValue);
            int m_CustomerId = 0;
            string m_RefererFrom = "";
            string m_UserAgent = "";
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_grid.DataSource = m_DocViewLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_DocId, m_DocFileId, LanguageId, AppTypeId, m_ActionTypeId, m_RefererFrom, m_UserAgent, m_CustomerId,m_FromIP, m_DateFrom, m_DateTo,ref RowCount);
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

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocViewLogs m_DataItem = (DocViewLogs)e.Row.DataItem;
            DocViewLogId = m_DataItem.DocViewLogId;
        }
    }

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlActionTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}

