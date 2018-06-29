using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_DocsUpdateSummaryEdit : System.Web.UI.Page
{
    private int DocId = 0;
    private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
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
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());           
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (DocId > 0)
            {
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Docs.DocId > 0)
                {
                    lblDocNameEdit.Text = String.Format("{0:}", m_Docs.DocName.ToString());
                    txtDocSummary.Text = String.Format("{0:}", m_Docs.DocSummary.ToString());
                    txtResult.Text = m_Docs.Result;
                    //if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]) && m_Docs.DocSummary != null && m_Docs.DocSummary.ToString() != "")
                    //{
                    //    btnSave.Enabled = false;
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa trích dẫn khi văn bản đã duyệt", this);
                    //}
                    //else
                    //{
                        btnSave.Enabled = true;
                        //btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                    //}
                    if (m_Docs.DocGroupId == 4) trResult.Visible = false;
                    
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
            byte SysMessageTypeId = 0;
            string messages = string.Empty;
            Docs m_Docs = new Docs();
            m_Docs.DocId = DocId;
            m_Docs.DocSummary = txtDocSummary.Text;
            m_Docs.Result = txtResult.Text;
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            m_Docs.LanguageId = LanguageId;
            SysMessageTypeId = m_Docs.UpdateSummary(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_Docs.UpdateResult_Quick(ActUserId);
            switch (SysMessageTypeId)
            {
                case 1:
                    messages = string.Format("Cập nhật trích dẫn văn bản : {0} không thành công.", !string.IsNullOrEmpty(lblDocNameEdit.Text) ? lblDocNameEdit.Text : "");
                    break;
                case 0:
                case 2:
                    messages = string.Format("Cập nhật trích dẫn văn bản <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblDocNameEdit.Text) ? lblDocNameEdit.Text : "");
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