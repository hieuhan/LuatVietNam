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
public partial class Admin_CustomerAccessLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<ActionTypes> l_ActionTypes = new List<ActionTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLActionTypes_BindByCulture(ddlActionTypes, ActionTypes.Static_GetListOrderBy("ActionTypeDesc"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("CustomerAccessLogs"), "");
            BindData();
        }
        if (!IsPostBack||CustomPaging.ChangePage)
        {
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            l_ActionTypes = ActionTypes.Static_GetList();
            int RowCount = 0;
            CustomerAccessLogs m_CustomerAccessLogs = new CustomerAccessLogs();
            int m_CustomerId = 0;
            string m_Keywords = txtSearch.Text.Trim();
            byte m_ActionTypeId = byte.Parse(ddlActionTypes.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            m_grid.DataSource = m_CustomerAccessLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_ActionTypeId, m_Keywords, m_Keywords, m_DateFrom, m_DateTo, ref RowCount);
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

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

   protected void ddlActionTypes_SelectedIndexChanged(object sender, EventArgs e)
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

