using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_Seos : System.Web.UI.Page
{
    protected short siteId = 0;
    protected int ActUserId = 0;
    protected List<Users> l_User;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            if (Session["seos-ddlOrderBy"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("Seos_Edit.aspx"))
            {
                ddlOrderBy.SelectedValue = Session["seos-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["seos-ddlSite"].ToString();
                txtKey.Text = Session["seos-txtSearch"].ToString();
                txtUrl.Text = Session["seos-txtUrl"].ToString();
            }
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
            Users m_Users = new Users();
            l_User = m_Users.GetAll();
            Seos m_Seos = new Seos();
            m_Seos.SeoName = txtKey.Text.Trim();
            m_Seos.SiteId = short.Parse(ddlSite.SelectedValue);
            siteId = short.Parse(ddlSite.SelectedValue);
            m_Seos.Url = txtUrl.Text.Trim();
            List<Seos> l_Seos = m_Seos.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, ddlOrderBy.SelectedValue, ref RowCount);//(ActUserId, 500, 0, "SiteId Desc", "", "", ref RowCount);
            m_grid.DataSource = l_Seos;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["seos-ddlSite"] = ddlSite.SelectedValue;
        Session["seos-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["seos-txtSearch"] = txtKey.Text;
        Session["seos-txtUrl"] = txtUrl.Text;
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("AdminResource", "DeleteConfirm").ToString() + "');";
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        try
        {
            Seos m_Seos = new Seos(); 
            m_Seos.SeoId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Seos.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 2)
            {
                Message = "Xóa Seos thành công";
            }
            else
            {
                Message = "Xóa Seos thất bại";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}