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
public partial class Admin_Promotions : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int PromotionId = 0;
    protected List<ServicePackages> l_ServicePackages = new List<ServicePackages>();
    protected List<Users> l_Users = new List<Users>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue)), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Promotions"), "");

            if (Session["pro-ddlServicePackages"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("PromotionsEdit.aspx"))
            {
                ddlOrderBy.SelectedValue = Session["pro-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["pro-ddlSite"].ToString();
                ddlServicePackages.SelectedValue = Session["pro-ddlServicePackages"].ToString();
                ddlServices.SelectedValue = Session["pro-ddlServices"].ToString();
                txtDateFrom.Text = Session["pro-txtDateFrom"].ToString();
                txtDateTo.Text = Session["pro-txtDateTo"].ToString();
                txtSearch.Text = Session["pro-txtSearch"].ToString();
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
            l_ServicePackages = ServicePackages.Static_GetList(ActUserId,0,0);
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_ReviewStatus = ReviewStatus.Static_GetList();

            Promotions m_Promotions = new Promotions(System.Configuration.ConfigurationManager.AppSettings["DOC_CONSTR"]);
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_ServicePackageName = txtSearch.Text.Trim();
            short m_ServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_Promotions.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Promotions.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_ServicePackageName,  m_ServiceId, m_ServicePackageId,m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        Session["pro-ddlSite"] = ddlSite.SelectedValue;
        Session["pro-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["pro-ddlServicePackages"] = ddlServicePackages.SelectedValue;
        Session["pro-ddlServices"] = ddlServices.SelectedValue;
        Session["pro-txtDateFrom"] = txtDateFrom.Text;
        Session["pro-txtDateTo"] = txtDateTo.Text;
        Session["pro-txtSearch"] = txtSearch.Text;
    }
    

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Promotions m_DataItem = (Promotions)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            PromotionId = m_DataItem.PromotionId;          
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        try
        {
            Promotions m_Promotions = new Promotions(DocConstants.DOC_CONSTR);
            m_Promotions.PromotionId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Promotions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId != 2) Message = "Xóa khuyến mại thất bại";
            else Message = "Xóa khuyến mại thành công";
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFail = 0;
        try
        {
            Promotions m_Promotions = new Promotions(DocConstants.DOC_CONSTR);
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Promotions.PromotionId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Promotions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId != 2) CountFail++;
                        else CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Xóa thành công " + CountSuc + " khuyến mại";
        if(CountFail>0) Message+=" và thất bại "+CountFail+" khuyến mại ";
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue)), "...");
        BindData();
    }
    protected void ddlServicePackages_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), short.Parse(ddlServices.SelectedValue)), "...");
        BindData();
    }
}

