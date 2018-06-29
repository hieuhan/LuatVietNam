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
using sms.common;
using sms.utils;

public partial class Admin_DocsUpdateTDADEdit : System.Web.UI.Page
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
            //if(LanguageId == 2)
            //{
            //    txtEffectDate.Enabled = false;
            //    txtExpireDate.Enabled = false;
            //}
        }
    }
    private void BindData()
    {
        try
        {
            if (DocId > 0)
            {
                Docs m_Docs = new Docs();
                DataSet ds;
                ds = m_Docs.Docs_GetProperties (ActUserId,byte.Parse(ddlLanguage.SelectedValue),DocId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["DocGroupId"].ToString() == "5")
                    {
                        lblIssueDate.Text = "Ngày ký xác thực";
                    }
                    else lblIssueDate.Text = "Ngày ban hành";
                    lblDocNameEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["DocName"]);
                    lblDocIdentityEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["DocIdentity"]);
                    lblDocTypesEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["DocTypeName"]);
                    lblIssueDateEdit.Text =String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["IssueDate"]);
                    lblOrganEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["OrganName"]);
                    lblSignersEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["SignerName"]);
                    lblFieldEdit.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["FieldName"]);
                    //if (byte.Parse(ds.Tables[0].Rows[0]["ReviewStatusId"].ToString()) == 2)
                    //{
                    //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["GazetteNumber"].ToString()))
                    //        txtGazetteNumber.Enabled = false;
                    //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["GazetteDate"].ToString()))
                    //        txtGazetteDate.Enabled = false;
                    //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EffectDate"].ToString()))
                    //        txtEffectDate.Enabled = false;
                    //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ExpireDate"].ToString()))
                    //        txtExpireDate.Enabled = false;
                    //}
                    txtGazetteNumber.Text = String.Format("{0:}", ds.Tables[0].Rows[0]["GazetteNumber"]);
                    txtGazetteDate.Text = String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["GazetteDate"]);
                    //txtEffectDate.Text = String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["EffectDate"]);
                    //txtExpireDate.Text = String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["ExpireDate"]);
                }
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                //if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                //{
                   
                //    btnSave.Enabled = false;
                //    btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa văn bản khi đã duyệt.", this);
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //}
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
            Docs m_Docs = new Docs();           
            m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
            m_Docs.GazetteNumber = txtGazetteNumber.Text;
            m_Docs.GazetteDate = !string.IsNullOrEmpty(txtGazetteDate.Text) ? Convert.ToDateTime(txtGazetteDate.Text) : DateTime.MinValue;
            //m_Docs.EffectDate = !string.IsNullOrEmpty(txtEffectDate.Text) ? Convert.ToDateTime(txtEffectDate.Text) : DateTime.MinValue;
            //m_Docs.ExpireDate = !string.IsNullOrEmpty(txtExpireDate.Text) ? Convert.ToDateTime(txtExpireDate.Text) : DateTime.MinValue;
            m_Docs.DocId = DocId;
            SysMessageTypeId = m_Docs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            switch (SysMessageTypeId)
            {
                case 1:
                    JSAlertHelpers.ShowNotifyOtherPage("15", "error", SysMessages.Static_GetDesc(DocConstants.DOC_CONSTR, SysMessageId), this);
                    break;
                case 0:
                case 2:
                    JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thời điểm áp dụng cho văn bản thành công.", this);
                    break;
            }
            
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