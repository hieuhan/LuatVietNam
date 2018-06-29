using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_MessageTemplates : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short MessageTemplateId = 0;
    protected List<MessageTemplates> l_MessageTemplates = new List<MessageTemplates>();
    protected List<SendMethods> l_SendMethods = new List<SendMethods>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {

            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLSendMethods_BindByCulture(ddlSendMethods, SendMethods.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("MessageTemplates"), "");
            if (Session["MsgTemp-ddlSendMethods"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("MessageTemplatesEdit.aspx"))
            {
                ddlSendMethods.SelectedValue = Session["MsgTemp-ddlSendMethods"].ToString();
                ddlOrderBy.SelectedValue = Session["MsgTemp-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["MsgTemp-ddlSite"].ToString();
                txtMessageName.Text = Session["MsgTemp-txtMessageName"].ToString();
                txtSendFrom.Text = Session["MsgTemp-txtSendFrom"].ToString();
            }
            BindData();
        }       
    }

    private void BindData()
    {
        try
        {
            l_SendMethods = SendMethods.Static_GetList();          
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            MessageTemplates m_MessageTemplates = new MessageTemplates();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_SendMethodId = byte.Parse(ddlSendMethods.SelectedValue);         
            string m_SendFrom = txtSendFrom.Text;
            string m_MessageName = txtMessageName.Text;
            m_MessageTemplates.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_MessageTemplates.MessageTemplates_GetList(ActUserId, m_OrderBy, m_MessageName,m_SendFrom, m_SendMethodId);
            m_grid.DataBind();
            lblTong.Text =m_grid.Rows.Count.ToString();
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
        Session["MsgTemp-ddlSendMethods"] = ddlSendMethods.SelectedValue;
        Session["MsgTemp-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["MsgTemp-ddlSite"] = ddlSite.SelectedValue;
        Session["MsgTemp-txtMessageName"] = txtMessageName.Text;
        Session["MsgTemp-txtSendFrom"] = txtSendFrom.Text;
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

            MessageTemplateId = short.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountSuc = 0;
        string Message = "";
        try
        {
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates.MessageTemplateId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_MessageTemplates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                Message = "Xóa mẫu tin thành công";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       BindData();
    }
  
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountFail = 0;
        int CountSuc = 0;
        try
        {
            MessageTemplates m_MessageTemplates = new MessageTemplates();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_MessageTemplates.MessageTemplateId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_MessageTemplates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " bản ghi và thất bại "+CountFail.ToString()+" bản ghi", this);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
       BindData();
    }

    protected void ddlSendMethods_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}