
/* ********************************************************************************
'     Document    :  SeosEdit.aspx.cs
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

public partial class admin_pages_SeosEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private int SeoId;
    private short siteId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["SeoId"] != null && Request.Params["SeoId"].ToString() != "")
            {
                SeoId = short.Parse(Request.Params["SeoId"].ToString());
            }
            if (Request.Params["siteId"] != null && Request.Params["siteId"].ToString() != "")
            {
                siteId = short.Parse(Request.Params["siteId"].ToString());
            }
            if (!IsPostBack)
            {
                DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
                DropDownListHelpers.DDL_SetSelected(ddlSite, siteId.ToString());
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
            Seos m_Seos = new Seos();
            m_Seos = m_Seos.Get(SeoId);
            if (m_Seos.SeoId > 0)
            {
                DropDownListHelpers.DDL_SetSelected(ddlSite, m_Seos.SiteId.ToString());
                txtSeoName.Text = m_Seos.SeoName;
                txtUrl.Text = m_Seos.Url;
                txtMetaTitle.Text = m_Seos.MetaTitle;
                txtMetaDesc.Text = m_Seos.MetaDesc;
                txtMetaKeyword.Text = m_Seos.MetaKeyword;
                txtCanonicalTag.Text = m_Seos.CanonicalTag;
                txtH1Tag.Text = m_Seos.H1Tag;
                txtSeoFooter.Text = m_Seos.SeoFooter;
            }
            else
            {
                txtSeoName.Text = "";
                txtUrl.Text = "";
                txtMetaTitle.Text = "";
                txtMetaDesc.Text = "";
                txtMetaKeyword.Text = "";
                txtCanonicalTag.Text = "";
                txtH1Tag.Text = "";
                txtSeoFooter.Text = "";
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
            string message = string.Empty;
            Seos m_Seos = new Seos();
            if(txtSeoName.Text == "")
            {
                JSAlert.Alert(GetLocalResourceObject("err").ToString(), this);
                return;
            }
            if (txtUrl.Text == "")
            {
                JSAlert.Alert(GetLocalResourceObject("err").ToString(), this);
                return;
            }
            if(SeoId <= 0 && string.IsNullOrEmpty(txtUrl.Text.Trim())){
                m_Seos = m_Seos.GetByUrl(txtUrl.Text.Trim());
                if (m_Seos.SeoId > 0)
                {
                    SeoId = m_Seos.SeoId;
                    message = "Url này đã có trong bảng Seos và được ";
                }
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            m_Seos.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Seos.SeoName = txtSeoName.Text;
            m_Seos.Url = txtUrl.Text;
            m_Seos.MetaTitle = txtMetaTitle.Text;
            m_Seos.MetaDesc = txtMetaDesc.Text;
            m_Seos.MetaKeyword = txtMetaKeyword.Text;
            m_Seos.CanonicalTag = txtCanonicalTag.Text;
            m_Seos.H1Tag = txtH1Tag.Text;
            m_Seos.SeoFooter = txtSeoFooter.Text;
            if (SeoId > 0)
            {
                m_Seos.SeoId = SeoId;
                SysMessageTypeId = m_Seos.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else 
            {
                SysMessageTypeId = m_Seos.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            if (cblAddOther.Checked)
            {
                if (SeoId > 0) { JSAlertHelpers.ShowNotify("15", "success",message+ "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                clearForm();
                return;
            }
            if (SeoId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success",message+ "Cập nhật thành công.", this); }
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
        txtCanonicalTag.Text = "";
        txtH1Tag.Text = "";
        txtMetaDesc.Text = "";
        txtMetaKeyword.Text = "";
        txtMetaTitle.Text = "";
        txtSeoFooter.Text = "";
        txtSeoName.Text = "";
        txtUrl.Text = "";
        ddlSite.SelectedIndex = -1;
    }


}
