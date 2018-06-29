using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_NewsletterGroupsEdit : System.Web.UI.Page
{
    private short NewsletterGroupId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["NewsletterGroupId"] != null && Request.Params["NewsletterGroupId"].ToString() != "")
        {
            NewsletterGroupId = short.Parse(Request.Params["NewsletterGroupId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            if (Request.QueryString["NewsletterGroupId"] == null)
            {
                cblAddOther.Visible = true;
            }
            else
            {
                cblAddOther.Visible = false;
                BindData();
            }
        }
    }
    private void BindData()
    {
        try
        {
            if (NewsletterGroupId > 0)
            {
                NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
                m_NewsletterGroups = m_NewsletterGroups.Get(NewsletterGroupId);
                if (m_NewsletterGroups.NewsletterGroupId > 0)
                {
                    txtNewsletterGroupName.Text = m_NewsletterGroups.NewsletterGroupName;
                    txtNewsletterGroupDesc.Text = m_NewsletterGroups.NewsletterGroupDesc.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_NewsletterGroups.SiteId.ToString());
                }
                else
                {
                    txtNewsletterGroupName.Text = "";
                    txtNewsletterGroupDesc.Text = "";
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
            short SysMessageTypeId = 0;
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            m_NewsletterGroups.NewsletterGroupName = txtNewsletterGroupName.Text.Trim();
            m_NewsletterGroups.NewsletterGroupDesc = txtNewsletterGroupDesc.Text.Trim();
            m_NewsletterGroups.NewsletterGroupId = NewsletterGroupId;
            m_NewsletterGroups.SiteId = short.Parse(ddlSite.SelectedValue);
            SysMessageTypeId= m_NewsletterGroups.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", sms.common.SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        NewsletterGroupId > 0 ? "Cập nhật nhóm bản tin thành công." : "Thêm mới nhóm bản tin thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    NewsletterGroupId > 0 ? "Cập nhật nhóm bản tin thành công." : "Thêm mới nhóm bản tin thành công.", this);
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
        txtNewsletterGroupName.Text = "";
        txtNewsletterGroupDesc.Text = "";
        cblAddOther.Checked = false;
    }
}