using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;


public partial class Admin_Pages_lawsdocs_Lawers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        try
        {
            l_ReviewStatus = ReviewStatus.Static_GetList();
            if (!IsPostBack)
            {
                DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
                DropDownListHelpers.DDLLawerGroups_BindByCulture(ddlLawerGroups, LawerGroups.Static_GetList(), "...");
                DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
                DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Lawers"), "");
                ShowGrid();
            }
            else if (CustomPaging.ChangePage)
            {
                ShowGrid();
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    //--------------------------------------------------------------------------------

    private void ShowGrid()
    {
        try
        {
            Lawers m_Lawers = new Lawers();
            m_Lawers.FullName = txtSearch.Text;
            m_Lawers.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            m_Lawers.LawerGroupId = byte.Parse(ddlLawerGroups.SelectedValue);
            m_Lawers.FieldId = short.Parse(ddlFields.SelectedValue);
            List<Lawers> l_Lawers = m_Lawers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, ddlOrderBy.SelectedValue, "", "", ref RowCount);

            m_grid.DataSource = l_Lawers;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    // End Show Grid      
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Lawers m_DataItem = (Lawers)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("AdminResource", "DeleteConfirm").ToString() + "');";
            }
            Label lblImage = (Label)e.Row.FindControl("lblImage");
            string ImageUrl = m_DataItem.GetImageUrl();
            if (ImageUrl == "" || ImageUrl == CmsConstants.ROOT_PATH)
                lblImage.Text = "";
            else
                lblImage.Text = "<a title='Xem ảnh lớn' class=\"popup\" data-width='640' data-height='480' href=\"" + m_DataItem.GetImageUrl() + "\"><img style=\"width:" + 40 + "px;height:" + 40 + "px\" src=\"" + m_DataItem.GetImageUrl_Thumb() + "\" /></a>";
        }
    }


    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Lawers m_Lawers = new Lawers();
            m_Lawers.LawerID = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Lawers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
        ShowGrid();
        string Message = "Xóa Luật sư thành công.";
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ShowGrid();
    }


    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        ShowGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ShowGrid();
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int CountSuc = 0;
            int CountFalse = 0;
            int CountFalseByStatus = 0;
            byte SysMessageTypeId = 0;
            short SysMessageId = 0;
            Lawers m_Lawers = new Lawers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Lawers.LawerID = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Lawers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
            ShowGrid();
            
            string Message = "Xóa Luật sư thành công.";
            Message = "Xóa thành công " + CountSuc.ToString() + " Luật sư. ";
            
            JSAlertHelpers.ShowNotify("15", "success", Message, this);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            int CountSuc = 0;
            int SysMessageTypeId = 0;
            short SysMessageId = 0;
            Lawers m_Lawers = new Lawers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Lawers.LawerID = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Lawers = m_Lawers.Get();
                        m_Lawers.ReviewStatusId = ReviewStatusHelpers.Reject;
                        SysMessageTypeId = m_Lawers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
            ShowGrid();
            string Message = "";
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " Luật sư. ";

            JSAlertHelpers.ShowNotify("15", "success", Message, this);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        try
        {
            int CountSuc = 0;
            int SysMessageTypeId = 0;
            short SysMessageId = 0;
            Lawers m_Lawers = new Lawers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Lawers.LawerID = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Lawers = m_Lawers.Get();
                        m_Lawers.ReviewStatusId = ReviewStatusHelpers.Reviewed;
                        SysMessageTypeId = m_Lawers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
            ShowGrid();
            string Message = "";
            Message = "Duyệt duyệt thành công " + CountSuc.ToString() + " Luật sư. ";

            JSAlertHelpers.ShowNotify("15", "success", Message, this);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        ShowGrid();
    }
}