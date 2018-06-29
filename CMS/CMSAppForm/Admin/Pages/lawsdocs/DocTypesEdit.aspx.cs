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

public partial class Admin_DocTypesEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte DocTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DocTypeId"] != null && Request.Params["DocTypeId"].ToString() != "")
        {
            DocTypeId = byte.Parse(Request.Params["DocTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (DocTypeId > 0)
            {
                DocTypes m_DocTypes = new DocTypes();
                m_DocTypes = m_DocTypes.Get(DocTypeId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_DocTypes.DocTypeId > 0)
                {
                    txtDocTypeName.Text = m_DocTypes.DocTypeName;
                    txtDocTypeDesc.Text = m_DocTypes.DocTypeDesc.ToString();
                    txtDisplayOrder.Text = m_DocTypes.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_DocTypes.ReviewStatusId.ToString());
                }
                else
                {
                    txtDocTypeName.Text ="";
                    txtDocTypeDesc.Text = "";
                    txtDisplayOrder.Text ="";
                    ddlReviewStatus.SelectedIndex = -1;
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
            DocTypes m_DocTypes = new DocTypes();
            m_DocTypes.DocTypeName = txtDocTypeName.Text.Trim();
            m_DocTypes.DocTypeDesc = txtDocTypeDesc.Text.Trim();
            m_DocTypes.DocTypeId = DocTypeId;            
            m_DocTypes.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? byte.Parse(txtDisplayOrder.Text) : byte.Parse("0");
            m_DocTypes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_DocTypes.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_DocTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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