using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_SmsMos : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int MoId = 0;
    protected List<SmsProcessStatus> l_SmsProcessStatus = new List<SmsProcessStatus>();
    protected List<SmsServices> l_SmsServices = new List<SmsServices>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLSmsServices_BindByCulture(ddlSmsServices, SmsServices.Static_GetList(), "...");
            DropDownListHelpers.DDLSmsProcessStatus_BindByCulture(ddlSmsProcessStatus, SmsProcessStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("SmsMos"), "");
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
            l_SmsProcessStatus = SmsProcessStatus.Static_GetList();
            l_SmsServices = SmsServices.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            SmsMos m_SmsMos = new SmsMos();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_SmsServiceId = byte.Parse(ddlSmsServices.SelectedValue);
            byte m_SmsProcessStatusId = byte.Parse(ddlSmsProcessStatus.SelectedValue);
            string m_UserId = txtUserId.Text;
            string m_MessageIn = txtMessageIn.Text;
            string m_PrefixId = ddlPrefixs.SelectedValue.ToString();
            string m_RequestId="";
            byte m_TelcoId = byte.Parse(ddlTelcos.SelectedValue);
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;

            m_grid.DataSource = m_SmsMos.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_UserId, m_PrefixId, m_TelcoId, m_MessageIn,m_RequestId, m_SmsServiceId, m_SmsProcessStatusId,m_DateFrom, m_DateTo, ref RowCount);
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
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MoId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());            
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
    protected void ddlSmsServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSmsProcessStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlTelcos_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlPrefixs_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}