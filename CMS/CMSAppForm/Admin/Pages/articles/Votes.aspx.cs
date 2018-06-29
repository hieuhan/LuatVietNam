using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_Votes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int VoteId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLVoteTypes_BindByCulture(ddlVoteTypes, VoteTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Votes"), "");
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
            Votes m_Votes = new Votes();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short m_CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            byte m_VoteTypeId = byte.Parse(ddlVoteTypes.SelectedValue);
            byte m_LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            byte m_AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            string m_VoteName = "";
            m_Votes.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Votes.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_LanguageId, m_AppTypeId, VoteId, m_VoteName, m_VoteTypeId, m_CategoryId, m_ReviewStatusId, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), ref RowCount);
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
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
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            VoteId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }

            Repeater rptLanguageEditContent = (Repeater)e.Row.FindControl("rptLanguageEditContent");
            if (rptLanguageEditContent != null)
            {
                rptLanguageEditContent.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguageEditContent.DataBind();
            }
          
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            if (lblLanguageId != null)
            {
                Votes m_Votes = new Votes();
                m_Votes.VoteId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                m_Votes.LanguageId = byte.Parse(lblLanguageId.Text);
                Literal lblTermName = (Literal)m_grid.Rows[e.RowIndex].FindControl("lblVoteName");
                SysMessageTypeId = m_Votes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1)//error
                {
                    deleteMessage = string.Format("Lỗi khi xóa bình luận : {0}", sms.common.SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 2)
                {
                    deleteMessage = string.Format("Xóa bình luận <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
   protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            Votes m_Votes = new Votes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Votes.VoteId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Votes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " bình luận.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> bình luận chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlServiceTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlVoteTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlParentCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}