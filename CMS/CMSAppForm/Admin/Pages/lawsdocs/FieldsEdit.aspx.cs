using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using iTextSharp.text;
using sms.common;

public partial class Admin_FieldsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private short FieldId = 0;
    protected short ParentFieldId = 0;
    private int ActUserId = 0;
    protected List<Fields> l_ParentField = new List<Fields>();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["FieldId"] != null && Request.Params["FieldId"].ToString() != "")
        {
            FieldId = short.Parse(Request.Params["FieldId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
           
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            Fields m_Fields = new Fields();
            l_ParentField = m_Fields.GetListByFieldId(byte.Parse(ddlLanguage.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentField, l_ParentField, "...", ParentFieldId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (FieldId > 0)
            {
                Fields m_Fields = new Fields();
                m_Fields = m_Fields.Get(FieldId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Fields.FieldId > 0)
                {
                    txtFieldName.Text = m_Fields.FieldName;
                    txtFieldDesc.Text = m_Fields.FieldDesc;
                    txtDisplayOrder.Text = m_Fields.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Fields.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlParentField, m_Fields.ParentFieldId.ToString());
                }
                else
                {
                    txtFieldName.Text = "";
                    txtFieldDesc.Text = "";
                    txtDisplayOrder.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlParentField.SelectedIndex = -1;
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

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            Fields m_Fields = new Fields();
            m_Fields.FieldId = FieldId;
            m_Fields.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Fields.FieldName = txtFieldName.Text;
            m_Fields.FieldDesc = txtFieldDesc.Text;
            m_Fields.CrUserId = ActUserId;
            m_Fields.DisplayOrder = txtDisplayOrder.Text == "" ? short.Parse("0") : short.Parse(txtDisplayOrder.Text);
            m_Fields.ParentFieldId = short.Parse(ddlParentField.SelectedValue);
            m_Fields.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            SysMessageTypeId = m_Fields.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            switch (SysMessageTypeId)
            {
                case 1:
                    JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
                    break;
                case 2:
                case 0:
                    if (cblAddOther.Checked)
                    {
                        JSAlertHelpers.ShowNotify("15", "success",
                            FieldId > 0 ? "Cập nhật Lĩnh vực thành công." : "Thêm mới Lĩnh vực thành công.", this);
                        ClearForm();
                        return;
                    }
                    JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                        FieldId > 0 ? "Cập nhật Lĩnh vực thành công." : "Thêm mới Lĩnh vực thành công.", this);
                    break;
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
        txtFieldName.Text = "";
        txtFieldDesc.Text = "";
        txtDisplayOrder.Text = "";
        ddlLanguage.SelectedIndex = -1;
        ddlParentField.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}