using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_MessageTemplatesEdit : System.Web.UI.Page
{
    private short MessageTemplateId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["MessageTemplateId"] != null && Request.Params["MessageTemplateId"].ToString() != "")
        {
            MessageTemplateId = short.Parse(Request.Params["MessageTemplateId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLSendMethods_BindByCulture(ddlSendMethods, SendMethods.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (MessageTemplateId > 0)
            {
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates = m_MessageTemplates.Get(MessageTemplateId);
                if (m_MessageTemplates.MessageTemplateId > 0)
                {
                    txtMessageName.Text = m_MessageTemplates.MessageName;
                    txtSendFrom.Text = m_MessageTemplates.SendFrom;
                    txtTitle.Text = m_MessageTemplates.Title.ToString();
                    txtMsgContent.Text = m_MessageTemplates.MsgContent.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSendMethods, m_MessageTemplates.SendMethodId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_MessageTemplates.SiteId.ToString());
                }
                else
                {
                    txtMessageName.Text = "";
                    txtSendFrom.Text = "";
                    txtTitle.Text = "";
                    txtMsgContent.Text = "";
                    ddlSendMethods.SelectedIndex = -1;                    
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

   protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            MessageTemplates m_MessageTemplates = new MessageTemplates();
            m_MessageTemplates.MessageTemplateId = MessageTemplateId;
            m_MessageTemplates.MessageName = txtMessageName.Text;
            m_MessageTemplates.SendFrom = txtSendFrom.Text.ToString();
            m_MessageTemplates.Title = txtTitle.Text.ToString();
            m_MessageTemplates.MsgContent = txtMsgContent.Text;            
            m_MessageTemplates.SendMethodId = byte.Parse(ddlSendMethods.SelectedValue);
            m_MessageTemplates.SiteId = short.Parse(ddlSite.SelectedValue);
            m_MessageTemplates.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (MessageTemplateId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                clearForm();
                return;
            }
            if (MessageTemplateId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới thành công.", this); }

            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
   private void clearForm()
   {
       txtMessageName.Text = "";
       txtMsgContent.Text = "";
       txtSendFrom.Text = "";
       txtTitle.Text = "";
       ddlSendMethods.SelectedIndex = -1;
       ddlSite.SelectedIndex = -1;
   }
}