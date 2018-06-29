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

public partial class Admin_RelateTypesEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte RelateTypeId = 0;
    private byte DocGroupId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["RelateTypeId"] != null && Request.Params["RelateTypeId"].ToString() != "")
        {
            RelateTypeId = byte.Parse(Request.Params["RelateTypeId"].ToString());
        }
        if (Request.Params["DocGroupId"] != null && Request.Params["DocGroupId"].ToString() != "")
        {
            DocGroupId = byte.Parse(Request.Params["DocGroupId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLRelateTypeGroups_BindByCulture(ddlRelateTypeGroupId, RelateTypeGroups.Static_GetList(), "");
            DropDownListHelpers.DDLDocGroups_BindByCulture(ddlDocGroups, DocGroups.GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (RelateTypeId > 0)
            {
                RelateTypes m_RelateTypes = new RelateTypes();
                m_RelateTypes = m_RelateTypes.Get(RelateTypeId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_RelateTypes.RelateTypeId > 0)
                {
                    txtRelateTypeName.Text = m_RelateTypes.RelateTypeName;
                    txtRelateTypeDesc.Text = m_RelateTypes.RelateTypeDesc.ToString();
                    txtRevertRelateTypeName.Text = m_RelateTypes.RevertRelateTypeName;
                    txtRevertRelateTypeDesc.Text = m_RelateTypes.RevertRelateTypeDesc.ToString();
                    txtDisplayOrder.Text = m_RelateTypes.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlRelateTypeGroupId, m_RelateTypes.RelateTypeGroupId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDocGroups, m_RelateTypes.DocGroupId.ToString());

                }
                else
                {
                    txtRelateTypeName.Text = "";
                    txtRelateTypeDesc.Text = "";
                    txtRevertRelateTypeName.Text = "";
                    txtRevertRelateTypeDesc.Text = "";
                    txtDisplayOrder.Text = "";
                    ddlRelateTypeGroupId.SelectedIndex = -1;
                    ddlDocGroups.SelectedIndex = -1;
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
            RelateTypes m_RelateTypes = new RelateTypes();
            m_RelateTypes.RelateTypeName = txtRelateTypeName.Text.Trim();
            m_RelateTypes.RelateTypeDesc = txtRelateTypeDesc.Text.Trim();
            m_RelateTypes.RelateTypeId = RelateTypeId;
            m_RelateTypes.RevertRelateTypeName = (txtRevertRelateTypeName.Text.Trim() != "") ? txtRevertRelateTypeName.Text : "";
            m_RelateTypes.RevertRelateTypeDesc = (txtRevertRelateTypeDesc.Text.Trim() != "") ? txtRevertRelateTypeDesc.Text : "";
            m_RelateTypes.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? byte.Parse(txtDisplayOrder.Text) : byte.Parse("0");
            m_RelateTypes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_RelateTypes.RelateTypeGroupId = byte.Parse(ddlRelateTypeGroupId.SelectedValue);
            m_RelateTypes.DocGroupId =  byte.Parse(ddlDocGroups.SelectedValue);
            SysMessageTypeId = m_RelateTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        RelateTypeId > 0 ? "Cập nhật Kiểu liên kết VB thành công." : "Thêm mới Kiểu liên kết VB thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    RelateTypeId > 0 ? "Cập nhật Kiểu liên kết VB thành công." : "Thêm mới Kiểu liên kết VB thành công.", this);
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
        txtRelateTypeName.Text = "";
        txtRelateTypeDesc.Text = "";
        txtRevertRelateTypeName.Text = "";
        txtRevertRelateTypeDesc.Text = "";
        txtDisplayOrder.Text = "";
        ddlLanguage.SelectedIndex = -1;
        ddlDocGroups.SelectedIndex = -1;
        ddlRelateTypeGroupId.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}