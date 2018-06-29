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

public partial class Admin_KeywordsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int KeywordId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["KeywordId"] != null && Request.Params["KeywordId"].ToString() != "")
        {
            KeywordId = int.Parse(Request.Params["KeywordId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (KeywordId > 0)
            {
                Keywords m_Keywords = new Keywords();
                m_Keywords = m_Keywords.Get(KeywordId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Keywords.KeywordId > 0)
                {
                    txtKeywordName.Text = m_Keywords.KeywordName.ToString();
                }
                else
                {
                    txtKeywordName.Text = "";
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
            Keywords m_Keywords = new Keywords();
            m_Keywords.KeywordName = txtKeywordName.Text.Trim();    
            m_Keywords.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Keywords.KeywordId = KeywordId;            
            m_Keywords.CrUserId = ActUserId;
            SysMessageTypeId = m_Keywords.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            //if (SysMessageTypeId == 1)
            //{
            //    JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            //}
            //else if (SysMessageTypeId == 2)
            //{
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        KeywordId > 0 ? "Cập nhật Từ khóa thành công." : "Thêm mới Từ khóa thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    KeywordId > 0 ? "Cập nhật Từ khóa thành công." : "Thêm mới Từ khóa thành công.", this);

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

    private void ClearForm()
    {
        txtKeywordName.Text = "";
        ddlLanguage.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}