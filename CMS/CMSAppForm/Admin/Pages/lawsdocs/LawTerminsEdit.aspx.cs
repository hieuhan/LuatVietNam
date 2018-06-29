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

public partial class Admin_LawTerminsEdit : System.Web.UI.Page
{
    private int LawTerminId = 0;
    private byte LanguageId = 0;
    private byte LawTerminGroupId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["LawTerminId"] != null && Request.Params["LawTerminId"].ToString() != "")
        {
            LawTerminId = short.Parse(Request.Params["LawTerminId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDLLawTerminGroup_BindByCulture(ddlLawTerminGroupID, LawTerminGroups.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (LawTerminId > 0)
            {
                LawTermins m_LawTermins = new LawTermins();
                m_LawTermins = m_LawTermins.Get(LawTerminId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_LawTermins.LawTerminId > 0)
                {
                    txtTermName.Text = m_LawTermins.TermName;
                    txtTermDesc.Text = m_LawTermins.TermDesc.ToString();
                    txtTermSource.Text = m_LawTermins.TermSource.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_LawTermins.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlLanguage, m_LawTermins.LanguageId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlLawTerminGroupID, m_LawTermins.LawTerminGroupId.ToString());
                }
                else
                {
                    txtTermName.Text = "";
                    txtTermDesc.Text = "";
                    txtTermSource.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlLanguage.SelectedIndex = -1;
                    ddlLawTerminGroupID.SelectedIndex = -1;
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
            LawTermins m_LawTermins = new LawTermins();
            m_LawTermins.TermName = txtTermName.Text.Trim();
            m_LawTermins.TermDesc = txtTermDesc.Text.Trim();
            m_LawTermins.LawTerminId = LawTerminId;
            m_LawTermins.TermSource = (txtTermSource.Text.Trim() != "") ? txtTermSource.Text : "";
            m_LawTermins.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_LawTermins.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_LawTermins.LawTerminGroupId = byte.Parse(ddlLawTerminGroupID.SelectedValue);
            SysMessageTypeId = m_LawTermins.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        LawTerminId > 0 ? "Cập nhật Thuật ngữ pháp lý thành công." : "Thêm mới Thuật ngữ pháp lý thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    LawTerminId > 0 ? "Cập nhật Thuật ngữ pháp lý thành công." : "Thêm mới Thuật ngữ pháp lý thành công.", this);
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
        txtTermName.Text = "";
        txtTermDesc.Text = "";
        txtTermSource.Text = "";
        cblAddOther.Checked = false;
        ddlLanguage.SelectedIndex = -1;
        ddlLawTerminGroupID.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
    }
}