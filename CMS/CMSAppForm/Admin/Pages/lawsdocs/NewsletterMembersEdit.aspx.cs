using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.utils;

public partial class Admin_NewsletterMembersEdit : System.Web.UI.Page
{
    private int NewsletterMemberId = 0;
   // private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["NewsletterMemberId"] != null && Request.Params["NewsletterMemberId"].ToString() != "")
        {
            NewsletterMemberId = int.Parse(Request.Params["NewsletterMemberId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, NewsletterGroups.Static_GetList(), "");
            DropDownListHelpers.DDLNewsletterMemberStatus_BindByCulture(ddlNewsletterMemberStatus, NewsletterMemberStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (NewsletterMemberId > 0)
            {
                NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
                m_NewsletterMembers = m_NewsletterMembers.Get(NewsletterMemberId);
                if (m_NewsletterMembers.NewsletterMemberId > 0)
                {
                    txtCustomerId.Value = m_NewsletterMembers.CustomerId.ToString();
                    txtCrUserName.Text = !string.IsNullOrEmpty(m_NewsletterMembers.CustomerName)
                        ? m_NewsletterMembers.CustomerName
                        : "";
                    txtEmail.Text = m_NewsletterMembers.Email.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlNewsletterMemberStatus,
                        m_NewsletterMembers.NewsletterMemberStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlNewsletterGroups,
                        m_NewsletterMembers.NewsletterGroupId.ToString());
                }
                else
                {
                    txtCustomerId.Value = "";
                    txtEmail.Text = "";
                    ddlNewsletterGroups.SelectedIndex = -1;
                    ddlNewsletterMemberStatus.SelectedIndex = -1;
                }
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;
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
            byte SysMessageTypeId = 0;
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            m_NewsletterMembers.NewsletterMemberId = NewsletterMemberId;
            m_NewsletterMembers.CustomerId = txtCustomerId.Value == "" ? 0 : int.Parse(txtCustomerId.Value);
            m_NewsletterMembers.Email = txtEmail.Text.ToString();           
            m_NewsletterMembers.NewsletterMemberStatusId = byte.Parse(ddlNewsletterMemberStatus.SelectedValue);
            m_NewsletterMembers.NewsletterGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            SysMessageTypeId = m_NewsletterMembers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        NewsletterMemberId > 0 ? "Cập nhật người dùng đăng ký bản tin thành công." : "Thêm mới người dùng đăng ký bản tin thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    NewsletterMemberId > 0 ? "Cập nhật người dùng đăng ký bản tin thành công." : "Thêm mới người dùng đăng ký bản tin thành công.", this);
            }
            //JSAlertHelpers.close(this);
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

    private void ClearForm()
    {
        txtCrUserName.Text = "";
        txtCustomerId.Value = "0";
        txtEmail.Text = "";
        ddlNewsletterGroups.SelectedIndex = -1;
        ddlNewsletterMemberStatus.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}