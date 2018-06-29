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

public partial class Admin_NewsletterEmailsEdit : System.Web.UI.Page
{
    private int NewsletterEmailId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["NewsletterEmailId"] != null && Request.Params["NewsletterEmailId"].ToString() != "")
        {
            NewsletterEmailId = int.Parse(Request.Params["NewsletterEmailId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            CheckBoxListHelpers.Bind(chkGroup, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (NewsletterEmailId > 0)
            {
                NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
                m_NewsletterEmails = m_NewsletterEmails.Get(NewsletterEmailId);
                if (m_NewsletterEmails.NewsletterEmailId > 0)
                {
                    txtCustomerId.Enabled = false;
                    txtCustomerId.Text = m_NewsletterEmails.CustomerId.ToString();
                    txtEmail.Text = m_NewsletterEmails.Email;
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_NewsletterEmails.SiteId.ToString());
                    RadioButtonListHelpers.SetSelected(rbtIsReceiveNews, m_NewsletterEmails.IsReceiveNews.ToString());
                    NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
                    CheckBoxListHelpers.Bind(chkGroup, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "");
                    NewsletterEmailGroups m_NewsletterEmailGroups = new NewsletterEmailGroups();
                    List<NewsletterEmailGroups> l_NewsletterEmailGroups = new List<NewsletterEmailGroups>();
                    l_NewsletterEmailGroups=m_NewsletterEmailGroups.GetList(m_NewsletterEmails.NewsletterEmailId);
                    for (int i = 0; i < l_NewsletterEmailGroups.Count; i++)
                    {
                        CheckBoxListHelpers.SetSelected(chkGroup, l_NewsletterEmailGroups[i].NewsletterGroupId.ToString());
                    }
                }
                else
                {
                    txtCustomerId.Enabled = true;
                    txtCustomerId.Text = "";
                    txtEmail.Text = "";
                    rbtIsReceiveNews.SelectedIndex = -1;
                    NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
                    CheckBoxListHelpers.Bind(chkGroup, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "");
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
            NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
            m_NewsletterEmails.NewsletterEmailId = NewsletterEmailId;
            m_NewsletterEmails.CustomerId = int.Parse(txtCustomerId.Text);
            m_NewsletterEmails.Email = txtEmail.Text.ToString();
            m_NewsletterEmails.IsReceiveNews = byte.Parse(rbtIsReceiveNews.SelectedValue);
            m_NewsletterEmails.SiteId = short.Parse(ddlSite.SelectedValue);
            m_NewsletterEmails.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (m_NewsletterEmails.NewsletterEmailId > 0)
            {
                NewsletterEmailGroups m_NewsletterEmailGroups = new NewsletterEmailGroups();
                m_NewsletterEmailGroups.NewsletterEmailId = m_NewsletterEmails.NewsletterEmailId;
                m_NewsletterEmailGroups.DeleteByNewsletterEmailId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                for (int i = 0; i < chkGroup.Items.Count; i++)
                {
                    if (chkGroup.Items[i].Selected)
                    {
                        m_NewsletterEmailGroups.NewsletterGroupId = short.Parse(chkGroup.Items[i].Value);
                        m_NewsletterEmailGroups.ExpireTime=DateTime.Parse("1/1/2020");
                        m_NewsletterEmailGroups.NewsletterEmailGroupId = 0;
                        m_NewsletterEmailGroups.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
            if (cblAddOther.Checked)
            {
                if (NewsletterEmailId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                clearForm();
                return;
            }
            if (NewsletterEmailId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới thành công.", this); }

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
   protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
   {
       NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
       CheckBoxListHelpers.Bind(chkGroup, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "");
   }
   private void clearForm()
   {
       txtCustomerId.Text = "";
       txtEmail.Text = "";
       ddlSite.SelectedIndex = -1;
       NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
       CheckBoxListHelpers.Bind(chkGroup, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue)), "");
   }
}