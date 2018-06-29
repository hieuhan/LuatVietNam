using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;

public partial class Admin_Pages_lawsdocs_FeedBackGroups : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short FeedBackGroupId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            BindData();
        }

    }
    //------------------------------------------------------------------------------------------------------------------------

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups();
            //m_FeedBackGroups.FeedBackGroupName = txtContentSearch.Text.Trim();
            //string DateFrom = txtDateFrom.Text.Trim();
            //string DateTo = txtDateTo.Text.Trim();
            m_grid.DataSource = m_FeedBackGroups.GetList(ActUserId, short.Parse(ddlSite.SelectedValue));
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
    //------------------------------------------------------------------------------------------------------------------------

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }
    //------------------------------------------------------------------------------------------------------------------------

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
        try
        {
            Label lblFeedBackGroupName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblFeedBackGroupName");
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups();
            m_FeedBackGroups.FeedBackGroupId= short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_FeedBackGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1) //error
            {
                messages = string.Format("Lỗi khi xóa nhóm phản hồi : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2) // success
            {
                messages = string.Format("Xóa nhóm phản hồi <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblFeedBackGroupName.Text) ? lblFeedBackGroupName.Text : string.Empty); ;
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
    //------------------------------------------------------------------------------------------------------------------------

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //------------------------------------------------------------------------------------------------------------------------

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_FeedBackGroups.FeedBackGroupId= short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_FeedBackGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " nhóm phản hồi.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> nhóm phản hồi chưa được xóa.", countDeleteError);
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
    //------------------------------------------------------------------------------------------------------------------------

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //------------------------------------------------------------------------------------------------------------------------

    protected void ddlDataTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //------------------------------------------------------------------------------------------------------------------------
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}