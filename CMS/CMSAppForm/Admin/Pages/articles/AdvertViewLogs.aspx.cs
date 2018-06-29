using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_AdvertViewLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<Customers> l_Customers = new List<Customers>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLAdverts_BindByCulture(ddlAdverts, Adverts.Static_GetList(), "...");
            DropDownListHelpers.DDLAdvertPositions_BindByCulture(ddlAdvertPositions, AdvertPositions.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("AdvertViewLogs"), "");
            BindData();
        }
        else if (CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount=0;
            AdvertViewLogs m_AdvertViewLogs = new AdvertViewLogs();
            string OrderBy = ddlOrderBy.SelectedValue.ToString();
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            int m_AdvertId = int.Parse(ddlAdverts.SelectedValue);
            int m_AdvertPositionId = int.Parse(ddlAdvertPositions.SelectedValue);
            string m_UserAgent = txtSearch.Text;
            int m_CustomerId = 0;
            string m_FromIP=txtSearch.Text;
            m_grid.DataSource = m_AdvertViewLogs.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, m_AdvertId, m_AdvertPositionId, m_UserAgent, m_CustomerId, m_FromIP, m_DateFrom, m_DateTo, ref RowCount); 
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}",RowCount)!="") ? string.Format("{0:#,#}",RowCount) : "0";
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
   protected void ddlAdverts_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlAdvertPositions_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
}

