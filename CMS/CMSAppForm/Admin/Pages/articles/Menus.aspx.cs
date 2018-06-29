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

public partial class Admin_Menus : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
   
    protected List<Users> l_Users = new List<Users>();
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
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Users m_Users = new Users(); 
			l_Users = m_Users.GetAll();

            Menus m_Menus = new Menus();
            m_grid.DataSource = m_Menus.GetListBySiteId(short.Parse(ddlSite.SelectedValue));
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
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
                lbDelete.OnClientClick = "return confirm('Bạn có thực sự muốn xóa dữ liệu này ?');";
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
            Label lblMenuName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblMenuName");
            Menus m_Menus = new Menus();
            m_Menus.MenuId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Menus.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_Menus = m_Menus.Get();
            if (m_Menus.MenuId > 0)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa Menu : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else 
            {
                deleteMessage = string.Format("Xóa Menu <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblMenuName.Text) ? lblMenuName.Text : "");
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
            Menus m_Menus = new Menus();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Menus.MenuId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Menus.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        m_Menus = m_Menus.Get();
                        if(m_Menus.MenuId > 0)
                            countDeleteError++;
                        else
                            countDeleteSuccess++;
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " Menu.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> Menu chưa được xóa.", countDeleteError);
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        SiteId = short.Parse(ddlSite.SelectedValue);
        BindData();
    }
}
