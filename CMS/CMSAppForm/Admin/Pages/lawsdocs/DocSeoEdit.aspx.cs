using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.HelperLib;
using sms.common;
using sms.utils;
using ICSoft.LawDocsLib;
using ICSoft.CMSLib;

public partial class Admin_Pages_lawsdocs_DocSeoEdit : System.Web.UI.Page
{

    private int DocId = 0;
    private byte LanguageId = 0;
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
            }
            if (!IsPostBack)
            {
                if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
                {
                    LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
                }
                bindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    //--------------------------------------------------------------------------------

    private void bindData()
    {
        try
        {
            if (DocId > 0)
            {
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Docs_GetSeos(DocId, LanguageId);
                if (m_Docs.DocId > 0)
                {
                    txtMetaDesc.Text = String.Format("{0:}", m_Docs.MetaDesc.ToString());
                    txtMetaKeyword.Text = String.Format("{0:}", m_Docs.MetaKeyword.ToString());
                    txtMetaTitle.Text = String.Format("{0:}", m_Docs.MetaTitle.ToString());
                    txtH1Tag.Text = String.Format("{0:}", m_Docs.H1Tag.ToString());
                    txtSeoHeader.Text = String.Format("{0:}", m_Docs.SeoHeader.ToString());
                    txtSeoFooter.Text = String.Format("{0:}", m_Docs.SeoFooter.ToString());
                    txtSocialTitle.Text = String.Format("{0:}", m_Docs.SocialTitle.ToString());
                    txtSocialDesc.Text = String.Format("{0:}", m_Docs.SocialDesc.ToString());
                    txturl.Text = m_Docs.DocUrl;
                    if (!string.IsNullOrEmpty(m_Docs.SocialImage.ToString()))
                        txtIcon.Text = m_Docs.SocialImage.ToString().StartsWith("http") ? m_Docs.SocialImage.ToString() : CmsConstants.WEBSITE_MEDIADOMAIN + m_Docs.SocialImage.ToString();
                    else
                        txtIcon.Text = "";

                    if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;
                }

            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            string messages = string.Empty;
            Docs m_Docs = new Docs();
            m_Docs.DocId = DocId;
            m_Docs.MetaDesc = txtMetaDesc.Text;
            m_Docs.MetaKeyword = txtMetaKeyword.Text;
            m_Docs.MetaTitle = txtMetaTitle.Text;
            m_Docs.H1Tag = txtH1Tag.Text;
            m_Docs.SeoHeader = txtSeoHeader.Text;
            m_Docs.SeoFooter = txtSeoFooter.Text;
            m_Docs.SocialTitle = txtSocialTitle.Text;
            m_Docs.SocialDesc = txtSocialDesc.Text;
            m_Docs.DocUrl = txturl.Text;
            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Docs.SocialImage = txtIcon.Text;
            m_Docs.LanguageId = LanguageId;
            SysMessageTypeId = m_Docs.UpdateSeo(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (!string.IsNullOrEmpty(txturl.Text))
            {
                SysMessageTypeId = m_Docs.UpdateUrl(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            switch (SysMessageTypeId)
            {
                case 1:
                    messages = "Cập nhật Seo văn bản không thành công.";
                    break;
                case 0:
                case 2:
                    messages = "Cập nhật Seo văn bản thành công.";
                    break;
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            ////JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
            //lblMsg.Text = "Cập nhật trích dẫn thành công.";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}
