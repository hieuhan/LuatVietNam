using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using System.Text;
using System.IO;
using sms.common;

public partial class Admin_FeedBacks : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int FeedBackId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "...");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("FeedBacks"), "");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", "0");
            //DropDownListHelpers.DDLFeedBackGroups_BindByCulture(ddlFeedBackGroups, FeedBackGroups.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlFeedBackGroups, FeedBackGroups.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
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

            FeedBacks m_FeedBacks = new FeedBacks();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short m_FeedBackGroupId = short.Parse(ddlFeedBackGroups.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_FeedBacks.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_FeedBacks.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, AppTypeId, m_FeedBackGroupId, m_ReviewStatusId, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), ref RowCount);
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

            FeedBackId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
          
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
        try
        {
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            Label lblFullName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblFullName");
            if (lblLanguageId != null)
            {
                FeedBacks m_FeedBacks = new FeedBacks();
                m_FeedBacks.LanguageId = byte.Parse(lblLanguageId.Text);
                m_FeedBacks.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                m_FeedBacks.FeedBackId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_FeedBacks.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1) //error
                {
                    messages = string.Format("Lỗi khi xóa phản hồi : {0}", SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 2) // success
                {
                    messages = string.Format("Xóa phản hồi của người dùng <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblFullName.Text) ? lblFullName.Text : string.Empty); ;
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
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
   
    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
        int countReviewSuccess = 0, countReviewError = 0;
        try
        {
            FeedBacks m_FeedBacks = new FeedBacks();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_FeedBacks.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_FeedBacks.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_FeedBacks.FeedBackId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_FeedBacks.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_FeedBacks.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countReviewError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countReviewSuccess++;
                        }
                    }
                }
            }
            if (countReviewSuccess > 0)
            {
                messages += string.Format("{0} thành công <i>{1}</i> {2}", (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "Duyệt" : "Bỏ duyệt"), countReviewSuccess, " phản hồi.");
            }
            if (countReviewError > 0)
            {
                messages += string.Format("<i>{0}</i> phản hồi chưa được {1}.", countReviewError, (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "duyệt" : "bỏ duyệt"));
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
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
        try
        {
            FeedBacks m_FeedBacks = new FeedBacks();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_FeedBacks.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_FeedBacks.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_FeedBacks.FeedBackId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_FeedBacks.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlFeedBackGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDL_Bind(ddlFeedBackGroups, FeedBackGroups.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }

    protected void btnXuatExcel_Click(object sender, EventArgs e)
    {
        try
        {
            m_grid.AllowSorting = false;
            BindData();
            m_grid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            m_grid.FooterStyle.BackColor = System.Drawing.Color.LightGray;

            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            m_grid.RenderControl(hw);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("content-disposition", "attachment; filename=DanhSachGiaoDich.xls");
            Response.Write(sb.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}