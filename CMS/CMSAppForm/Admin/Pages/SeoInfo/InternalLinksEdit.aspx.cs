using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using sms.common;
using sms.utils;
using ICSoft.CMSLib;

public partial class admin_pages_articles_InternalLinksEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int InternalLinkId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
        if (Request.Params["InternalLinkId"] != null && Request.Params["InternalLinkId"].ToString() != "")
        {
            InternalLinkId = short.Parse(Request.Params["InternalLinkId"].ToString());
        }
        if (!IsPostBack)
        {
            BindDDL();
            BindData();
        }
    }

    private void BindDDL()
    {
        Sites m_Sites = new Sites();
        InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups();
        try
        {
            ddlSites.DataSource = m_Sites.GetList(ActUserId);
            ddlSites.DataBind();
            ddlSites.Items[0].Selected = true;
            BindGroupsDDL(Convert.ToInt16(ddlSites.SelectedValue));
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }

    private void BindGroupsDDL(short siteId)
    {
        InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups();
        IList l_InternalLinkGroups = new ArrayList();
        try
        {
            l_InternalLinkGroups = m_InternalLinkGroups.GetList(siteId);
            m_InternalLinkGroups.InternalLinkGroupId = 0;
            m_InternalLinkGroups.InternalLinkGroupName = "";
            m_InternalLinkGroups.InternalLinkGroupDesc = "";
            l_InternalLinkGroups.Insert(0, m_InternalLinkGroups);
            ddlInternalLinkGroups.DataSource = l_InternalLinkGroups;
            ddlInternalLinkGroups.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }

    private void BindData()
    {
        try
        {
            if (InternalLinkId > 0)
            {
                InternalLinks m_InternalLinks = new InternalLinks();
                m_InternalLinks.InternalLinkId = InternalLinkId;
                m_InternalLinks = m_InternalLinks.Get();
                if (m_InternalLinks.InternalLinkId > 0)
                {
                    txtInternalName.Text = m_InternalLinks.InternalLinkName;
                    txtInternalLinkUrl.Text = m_InternalLinks.InternalLinkUrl;
                    txtCrDateTime.Text = m_InternalLinks.CrDateTime.ToString("dd/MM/yyyy HH:mm");
                    if (ddlSites.Items.FindByValue(m_InternalLinks.SiteId.ToString()) != null)
                    {
                        ddlSites.SelectedIndex = -1;
                        ddlSites.Items.FindByValue(m_InternalLinks.SiteId.ToString()).Selected = true;
                        BindGroupsDDL(m_InternalLinks.SiteId);
                    }
                    if (ddlInternalLinkGroups.Items.FindByValue(m_InternalLinks.InternalLinkGroupId.ToString()) != null)
                    {
                        ddlInternalLinkGroups.SelectedIndex = -1;
                        ddlInternalLinkGroups.Items.FindByValue(m_InternalLinks.InternalLinkGroupId.ToString()).Selected = true;
                    }
                    if (ddlStatus.Items.FindByValue(m_InternalLinks.StatusId.ToString()) != null)
                    {
                        ddlStatus.SelectedIndex = -1;
                        ddlStatus.Items.FindByValue(m_InternalLinks.StatusId.ToString()).Selected = true;
                    }
                }
                else
                {
                    txtInternalName.Text = "";
                    txtInternalLinkUrl.Text = "";
                    txtCrDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                }
            }
            else
            {
                txtInternalName.Text = "";
                txtInternalLinkUrl.Text = "";
                txtCrDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        short insertStatus = 0;
        try
        {
            lblMessage.Text = string.Empty;
            if (string.IsNullOrEmpty(txtInternalName.Text.Trim()))
            {
                lblMessage.Text = "Chưa nhập Từ khóa";
                return;
            }
            if (ddlInternalLinkGroups.SelectedValue == "0")
            {
                lblMessage.Text = "Chưa chọn nhóm cho Từ khóa";
                return;
            }
            if (string.IsNullOrEmpty(txtInternalLinkUrl.Text.Trim()))
            {
                lblMessage.Text = "Chưa nhập Url cho Từ khóa";
                return;
            }
            InternalLinks m_InternalLinks = new InternalLinks();
            m_InternalLinks.InternalLinkId = InternalLinkId;
            if (InternalLinkId > 0)
            {
                m_InternalLinks = m_InternalLinks.Get();
            }
            m_InternalLinks.InternalLinkName = txtInternalName.Text;
            m_InternalLinks.InternalLinkDesc = m_InternalLinks.InternalLinkName;
            m_InternalLinks.InternalLinkEnglishName = StringUtil.RemoveSignature(m_InternalLinks.InternalLinkName);
            m_InternalLinks.InternalLinkUrl = txtInternalLinkUrl.Text;
            m_InternalLinks.InternalLinkGroupId = Convert.ToInt16(ddlInternalLinkGroups.SelectedValue);
            m_InternalLinks.SiteId = Convert.ToInt16(ddlSites.SelectedValue);
            m_InternalLinks.StatusId = Convert.ToByte(ddlStatus.SelectedValue);
            if (InternalLinkId == 0)
                insertStatus = m_InternalLinks.Insert();
            else
                insertStatus = m_InternalLinks.Update();
            
            if (insertStatus > 0)
            {
                m_InternalLinks.m_SysMessages = m_InternalLinks.m_SysMessages.Get(m_InternalLinks.m_SysMessages.SysMessageId);
                if(String.IsNullOrEmpty(m_InternalLinks.m_SysMessages.SysMessageDesc))
                {
                    m_InternalLinks.m_SysMessages.SysMessageDesc = "Cập nhật InternalLink thành công";
                }
            }
            else
            {
                m_InternalLinks.m_SysMessages.SysMessageDesc = "Lỗi cập nhật InternalLink";
            }
            lblMessage.Text = m_InternalLinks.m_SysMessages.SysMessageDesc;
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }

    protected void ddlddlSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        short siteId = Convert.ToInt16(ddlSites.SelectedValue);
        BindGroupsDDL(siteId);
    }
}