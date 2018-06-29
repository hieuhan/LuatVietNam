using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_Categories : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected short CategoryId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short DataTypeId = 1;
    protected byte EnableDataType = 1;
   
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<FeatureGroups> l_FeatureGroups = new List<FeatureGroups>();
    protected List<DataTypes> l_DataTypes = new List<DataTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = short.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["EnableDataType"] != null && Request.Params["EnableDataType"].ToString() != "")
        {
            EnableDataType = byte.Parse(Request.Params["EnableDataType"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Categories"), "");
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "...", DataTypeId.ToString());
            if (Session["cate-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("CategoriesEdit.aspx"))
            {
                ddlOrderBy.SelectedValue = Session["cate-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["cate-ddlSite"].ToString();
                ddlLanguage.SelectedValue = Session["cate-ddlLanguage"].ToString();
                ddlAppType.SelectedValue = Session["cate-ddlAppType"].ToString();
                ddlReviewStatus.SelectedValue = Session["cate-ddlReviewStatus"].ToString();
                ddlDataType.SelectedValue = Session["cate-ddlDataType"].ToString();
            }
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            Users m_Users = new Users(); 
			l_Users = m_Users.GetAll();
            l_FeatureGroups = FeatureGroups.Static_GetList();
            l_DataTypes = DataTypes.Static_GetList();

            Categories m_Categories = new Categories();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_CategoryId = 0;
            byte m_ReviewStatus = byte.Parse(ddlReviewStatus.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            SiteId = short.Parse(ddlSite.SelectedValue);
            m_Categories.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Categories.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Categories.GetAllHierachy(ActUserId, m_OrderBy, LanguageId, AppTypeId, m_CategoryId,  m_ReviewStatus, "&nbsp;&nbsp;&nbsp;&nbsp;");
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();

            ddlDataType.Enabled = EnableDataType == 1 ? true : false;
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
        Session["cate-ddlSite"] = ddlSite.SelectedValue;
        Session["cate-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["cate-ddlAppType"] = ddlAppType.SelectedValue;
        Session["cate-ddlDataType"] = ddlDataType.SelectedValue;
        Session["cate-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["cate-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Categories m_DataItem = (Categories)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
  
            CategoryId = m_DataItem.CategoryId;
            DataTypeId = m_DataItem.DataTypeId;
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }

            if (m_DataItem.ParentCategoryId == 0)
            {
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
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
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            if (lblLanguageId != null)
            {
                Categories m_Categories = new Categories();
                m_Categories.LanguageId = byte.Parse(lblLanguageId.Text);
                m_Categories.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                m_Categories.CategoryId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Categories.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1) Message = "Xóa chuyên mục thất bại";
                else Message = "Xóa chuyên mục thành công";
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
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            Categories m_Categories = new Categories();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_Categories.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_Categories.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_Categories.CategoryId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Categories.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Categories.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        Message = "Duyệt thành công " + CountSuc.ToString() + " chuyên mục. ";
        if (ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " chuyên mục. ";
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);

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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFail = 0;
        try
        {
            Categories m_Categories = new Categories();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_Categories.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_Categories.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_Categories.CategoryId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Categories.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        Message = "Xóa thành công " + CountSuc + " chuyên mục";
        if (CountFail > 0) Message += " và thất bại " + CountFail + " chuyên mục ";
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
