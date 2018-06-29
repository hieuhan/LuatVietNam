
/* ********************************************************************************
'     Document    :  SitesEdit.aspx.cs
' ********************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class admin_pages_SitesEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private short SiteId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
            {
                SiteId = short.Parse(Request.Params["SiteId"].ToString());
            }
            if (!IsPostBack)
            {
                bindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    //--------------------------------------------------------------------------------

    private void bindData()
    {
        try{
            //Users m_User = new Users();
            Sites m_Sites = new Sites();
            m_Sites = m_Sites.Get(ActUserId, SiteId);
            if (m_Sites.SiteId > 0)
            {
                txtSiteName.Text = m_Sites.SiteName;
                txtSiteDesc.Text = m_Sites.SiteDesc;
                txtMetaTitle.Text = m_Sites.MetaTitle;
                txtMetaDesc.Text = m_Sites.MetaDesc;
                txtMetaKeyword.Text = m_Sites.MetaKeyword;
                txtMetaTagAll.Text = m_Sites.MetaTagAll;
                txtCanonicalTag.Text = m_Sites.CanonicalTag;
                txtH1Tag.Text = m_Sites.H1Tag;
                txtSeoFooter.Text = m_Sites.SeoFooter;
                //txtCreateUser.Text = sms.admin.security.Users.Static_GetUserNameFullName(int.Parse(Eval("CrUserId").ToString()));
                //int uid = m_Sites.CrUserId;
                //m_User = m_User.Get(uid);
                //txtCreateUser.Text = m_User.Username;
            }
            else
            {
                txtSiteName.Text = "";
                txtSiteDesc.Text = "";
                txtMetaTitle.Text = "";
                txtMetaDesc.Text = "";
                txtMetaKeyword.Text = "";
                txtMetaTagAll.Text = "";
                //txtCreateUser.Text = sms.admin.security.Users.Static_GetUserNameFullName(int.Parse(Eval("ActUserId").ToString()));
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            if(txtSiteName.Text == "")
            {
                JSAlert.Alert(GetLocalResourceObject("err").ToString(), this);
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            Sites m_Sites = new Sites();
             m_Sites.SiteName=txtSiteName.Text;
            m_Sites.SiteDesc = txtSiteDesc.Text;
            m_Sites.MetaTitle = txtMetaTitle.Text ;
            m_Sites.MetaDesc = txtMetaDesc.Text ;
            m_Sites.MetaKeyword =txtMetaKeyword.Text;
            m_Sites.MetaTagAll = txtMetaTagAll.Text;
            m_Sites.CanonicalTag = txtCanonicalTag.Text;
            m_Sites.H1Tag = txtH1Tag.Text;
            m_Sites.SeoFooter = txtSeoFooter.Text;
            if (SiteId > 0)
            {
                //m_Sites.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                m_Sites.SiteId = SiteId;
                SysMessageTypeId = m_Sites.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Sites.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            //if (SysMessageTypeId == 2)
            //{
            //    JSAlert.Alert(GetLocalResourceObject("Success").ToString(), this);
            //}
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



}
