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

public partial class Admin_UserCategories : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int UserId = 0;
    protected List<UserStatus> l_UserStatus;  
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            UserStatus m_UserStatus = new UserStatus();
            Users m_Users = new Users();
            m_grid.DataSource = m_Users.GetListOrderByUserName();
            l_UserStatus = m_UserStatus.GetAll().Cast<UserStatus>().ToList<UserStatus>();
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            short SysMessageId=0;
            int UserId = Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            if (UserId > 0)
            {
                UserCategories m_UserCategories = new UserCategories();
                m_UserCategories.UserId = UserId;
                m_UserCategories.DeleteByUserId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }   
   
}

