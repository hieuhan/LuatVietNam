using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_Tags : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int TagId = 0;
    protected byte LanguageId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Tags"), "");

            if (Session["Tags-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("TagsEdit.aspx"))
            {
                ddlLanguage.SelectedValue = Session["Tags-ddlLanguage"].ToString();
                ddlOrderBy.SelectedValue = Session["Tags-ddlOrderBy"].ToString();
                ddlReviewStatus.SelectedValue = Session["Tags-ddlReviewStatus"].ToString();
                txtDateFrom.Text = Session["Tags-txtDateFrom"].ToString();
                txtDateTo.Text = Session["Tags-txtDateTo"].ToString();
            }
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
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            Tags m_Tags = new Tags();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_TagId = 0;
            string m_TagName="";
            byte m_ReviewStatus = byte.Parse(ddlReviewStatus.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_grid.DataSource = m_Tags.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_TagId, m_TagName, m_ReviewStatus, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), ref RowCount);
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
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            TagId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
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
                Tags m_Tags = new Tags();
                m_Tags.LanguageId = byte.Parse(lblLanguageId.Text);
                m_Tags.TagId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Tags.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (m_Tags.TagId > 0) Message = "Xóa thất bại do tag đang được sử dụng trong bài viết";
                else Message = "Xóa tag thành công";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success",Message, this);
    }

protected void SetCurentData()
{
    Session["Tags-ddlLanguage"] = ddlLanguage.SelectedValue;
    Session["Tags-ddlOrderBy"] = ddlOrderBy.SelectedValue;
    Session["Tags-txtDateFrom"] = txtDateFrom.Text;
    Session["Tags-txtDateTo"] = txtDateTo.Text;
    Session["Tags-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
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

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountSuc = 0;
        int CountFail = 0;
        try
        {
            Tags m_Tags = new Tags();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_Tags.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_Tags.TagId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Tags.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (m_Tags.TagId > 0) CountFail++;
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
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " tag, xóa thất bại "+CountFail.ToString()+"do tag đang được sử dụng", this);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}