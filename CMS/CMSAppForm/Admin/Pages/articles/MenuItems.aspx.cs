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

public partial class Admin_MenuItems : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected int MenuId = 0;
   
    protected List<Users> l_Users = new List<Users>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDL_Bind(ddlMenu, Menus.Static_GetListBySiteId(short.Parse(ddlSite.SelectedValue)),"");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users(); 
			l_Users = m_Users.GetAll();
            MenuItems m_Menus = new MenuItems();
            m_Menus.MenuId = int.MaxValue;
            if (ddlMenu.Items.Count>0) m_Menus.MenuId = int.Parse(ddlMenu.SelectedValue);
            m_grid.DataSource = m_Menus.GetAllHierachy("", "&nbsp;&nbsp;");
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
            MenuId = m_Menus.MenuId;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        MenuItems m_DataItem = (MenuItems)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('Bạn có thực sự muốn xóa dữ liệu này ?');";
            }

            if (m_DataItem.ParentItemId == 0)
            {
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
            }

            Label lblImage = (Label)e.Row.FindControl("lblImage");
            lblImage.Text = m_DataItem.GetImageUrlWithHtmlTag(40, 40);
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblItemName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblItemName");
            MenuItems m_Menus = new MenuItems();
            m_Menus.MenuItemId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Menus.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_Menus = m_Menus.Get();
            if (m_Menus.MenuItemId > 0)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa MenuItem: {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else //success
            {
                deleteMessage = string.Format("Xóa MenuItem <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblItemName.Text) ? lblItemName.Text : "");
            }
            JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + deleteMessage + "' });", true);
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
            MenuItems m_Menus = new MenuItems();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Menus.MenuItemId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Menus.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        m_Menus = m_Menus.Get();
                        if(m_Menus.MenuItemId > 0)
                            countDeleteError++;
                        else
                            countDeleteSuccess++;
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " MenuItem.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> MenuItem chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + messages + "' });", true);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            MenuItems m_Menus = new MenuItems();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Menus.MenuItemId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Menus.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Menus.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        switch (SysMessageTypeId)
                        {
                            case 1:
                                countDeleteError++;
                                break;
                            case 2:
                                countDeleteSuccess++;
                                break;
                        }
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("{0} thành công <i>{1}</i> {2}", (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "Duyệt" : "Bỏ duyệt"), countDeleteSuccess, " MenuItem.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> MenuItem chưa được {1}.", countDeleteError, (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "duyệt" : "bỏ duyệt"));
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + messages + "' });", true);
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        SiteId = short.Parse(ddlSite.SelectedValue);
        DropDownListHelpers.DDL_Bind(ddlMenu, Menus.Static_GetListBySiteId(SiteId), "");
        BindData();
    }
    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
