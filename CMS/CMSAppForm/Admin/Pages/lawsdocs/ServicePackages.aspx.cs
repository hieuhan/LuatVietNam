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
public partial class Admin_ServicePackages : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected short ServiceId = 0;
    protected short ServicePackageId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Services> l_Services = new List<Services>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)) , "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("ServicePackages"), "");
            if (Session["svp-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("ServicePackagesEdit.aspx"))
            {
                ddlOrderBy.SelectedValue = Session["svp-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["svp-ddlSite"].ToString();
                ddlReviewStatus.SelectedValue = Session["svp-ddlReviewStatus"].ToString();
                ddlServices.SelectedValue = Session["svp-ddlServices"].ToString();
                txtSearch.Text = Session["svp-txtSearch"].ToString();
            }
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_Services = Services.Static_GetList(ActUserId, 0);
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            ServicePackages m_ServicePackages = new ServicePackages();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_ServicePackageName = txtSearch.Text.Trim();            
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            m_ServicePackages.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_ServicePackages.ServicePackages_GetList(ActUserId, m_OrderBy, m_ServiceId, m_ServicePackageName, m_ReviewStatusId);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = m_grid.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["svp-ddlSite"] = ddlSite.SelectedValue;
        Session["svp-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["svp-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["svp-ddlServices"] = ddlServices.SelectedValue;
        Session["svp-txtSearch"] = txtSearch.Text;
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ServicePackages m_DataItem = (ServicePackages)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            ServicePackageId = m_DataItem.ServicePackageId;          
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        try
        {
            ServicePackages m_ServicePackages = new ServicePackages();
            m_ServicePackages.ServicePackageId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_ServicePackages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1) Message = "Xóa gói dịch vụ thất bại";
            else Message = "Xóa gói dịch vụ thành công";
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
        BindData();
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
            ServicePackages m_ServicePackages = new ServicePackages();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_ServicePackages.ServicePackageId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_ServicePackages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) CountFail++;
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
        Message = "Xóa thành công " + CountSuc + " gói dịch vụ";
        if (CountFail > 0) Message += " và thất bại " + CountFail + " gói dịch vụ ";
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        BindData();
    }

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFail = 0;
        try
        {
            ServicePackages m_ServicePackages = new ServicePackages();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {

                        short ServicePackageId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_ServicePackages.ServicePackageId = ServicePackageId;
                        m_ServicePackages.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_ServicePackages.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Duyệt thành công " + CountSuc.ToString() + " gói dịch vụ. ";
        if (ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " gói dịch vụ. ";
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);

    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }
}

