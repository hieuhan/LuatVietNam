using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.IO;
using System.Text;
using System.Reflection;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_DocsEditPropertie : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    protected byte ReviewStatusId = 0;
    private int ActUserId = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    protected int StepCount = 1;
    public static byte DocGroupId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
            btnSaveAndNew.Visible = false;
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
           
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            
            BindData();
        }
        if (ddlLanguage.SelectedValue == "2")
        {
            if (DocId > 0)
            {
                lnkSearchIdentity.Visible = false;      
            }
            else
            {
                lnkSearchIdentity.Visible = true;      
            }
        
        
            txtIssueDate.Enabled = false;
        
            txtEffectDate.Enabled = false;
        
            txtExpireDate.Enabled = false;
           
            txtFields.Enabled = false;
      
            txtOrgans.Enabled = false;
           
            txtSigners.Enabled = false;
            
            txtProvices.Enabled = false;
           
            ddlDocTypes.Enabled = false;          
            tblDocshowByIdentity.Visible = false;
        }
        else 
        {
            lnkSearchIdentity.Visible = false;
        }
       
       
    }
    private void bindFiles()
    {
        
    }
    private void bindDocSteps()
    {
        
    }
    private void BindData()
    {
        try
        {
            if (DocId > 0)
            {
                Docs m_Docs = new Docs();
                DocFields m_DocFields = new DocFields();
                DocOrgans m_DocOrgans = new DocOrgans();
                DocSigners m_DocSigners = new DocSigners();


                string FieldName="";
                string OrganName = "";
                string SignerName = "";
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Docs.DocId > 0)
                {
                    DocGroupId = m_Docs.DocGroupId;
                    lblIssueDate.Text = m_Docs.DocGroupId == 5 ? "Ngày ký xác thực:" : "Ngày ban hành:";
                    txtDocName.Text = m_Docs.DocName;
                    txtDocIdentity.Text = m_Docs.DocIdentity;
                    txtIssueDate.Text = (m_Docs.IssueDate==DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                    txtEffectDate.Text =(m_Docs.EffectDate==DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                    txtExpireDate.Text = (m_Docs.ExpireDate==DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                    txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(1, DocId, ref FieldName);
                   
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(1, DocId, ref SignerName);

                    txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(LanguageId, DocId, 0, ref OrganName);
                   
                    txtProvices.Text = DocProvinces.Static_GetProviceDesc(DocId);
                   
                    DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                    //if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    //{
                    //    ReviewStatusId = m_Docs.ReviewStatusId;
                    //    btnSave.Enabled = false;
                    //    btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa văn bản khi đã duyệt", this);
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                        
                    //}
                    bindFiles();
                    //Bind DocKeywords
                    DocKeywords mDocKeywords = new DocKeywords();
                    List<DocKeywords> lDocKeywords = mDocKeywords.GetListByDocId(DocId);
                    Keywords m_Keywords = new Keywords();
                    string m_KeywordsString = "";
                    if (lDocKeywords != null && lDocKeywords.Any())
                    {
                        foreach (DocKeywords myDocKeywords in lDocKeywords)
                        {
                            Keywords mKeywords = m_Keywords.Get(myDocKeywords.KeywordId, LanguageId);
                            if (string.IsNullOrEmpty(m_KeywordsString))
                            {
                                m_KeywordsString = mKeywords.KeywordName;
                            }
                            else
                            {
                                m_KeywordsString += "; " + mKeywords.KeywordName;
                            }
                        }
                    }
                    txtKeywords.Text = m_KeywordsString;
                    if(m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    {
                        ddlUseStatus.Enabled = false;

                        if (txtDocName != null && txtDocName.Text != "")
                        {
                            txtDocName.Enabled = false;
                        }
                        if (txtDocIdentity != null && txtDocIdentity.Text != "")
                        {
                            txtDocIdentity.Enabled = false;
                        }
                        if (txtIssueDate != null && txtIssueDate.Text != "")
                        {
                            txtIssueDate.Enabled = false;
                        }
                        if (txtEffectDate != null && txtEffectDate.Text != "")
                        {
                            txtEffectDate.Enabled = false;
                        }
                        if (txtExpireDate != null && txtExpireDate.Text != "")
                        {
                            txtExpireDate.Enabled = false;
                        }
                        if (txtGazetteDate != null && txtGazetteDate.Text != "")
                        {
                            txtGazetteDate.Enabled = false;
                        }
                        if (txtGazetteNumber != null && txtGazetteNumber.Text != "")
                        {
                            txtGazetteNumber.Enabled = false;
                        }
                        if (txtFields != null && txtFields.Text != "")
                        {
                            txtFields.Enabled = false;
                        }
                        if (txtOrgans != null && txtOrgans.Text != "")
                        {
                            txtOrgans.Enabled = false;
                        }
                        if (txtProvices != null && txtProvices.ToString() != "")
                        {
                            txtProvices.Enabled = false;
                        }
                        if (txtSigners != null && txtSigners.ToString() != "")
                        {
                            txtSigners.Enabled = false;
                        }
                    }
                    if (m_Docs.DocGroupId != 8)//vb không có nội dung download
                    {
                        rfvSigners.Enabled = true;
                        rqSigners.Visible = true;
                        rfvFields.Enabled = true;
                        rqFields.Visible = true;
                        rfvtxtFields.Enabled = true;
                        rfvtxtSigners.Visible = true;
                    }
                    else
                    {
                        rfvSigners.Enabled = false;
                        rqSigners.Visible = false;
                        rfvSigners.IsValid = false;
                        rfvtxtSigners.IsValid = false;
                        rfvtxtSigners.Enabled = false;

                        rfvFields.Enabled = false;
                        rqFields.Visible = false;
                        rfvFields.IsValid = false;
                        
                        rfvtxtFields.Enabled = false;
                        rfvtxtFields.IsValid = false;  
                    }
                }
                else
                {
                    ClearForm();
                }
                btnSaveAndNew.Visible = false;
            }
            bindDocSteps();
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
        
        SaveData();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        Response.Redirect("DocsTTHC.aspx?LanguageId=" + LanguageId.ToString());
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        ClearForm();
    }
    private void ClearForm()
    {
        txtDocName.Text = "";
        txtDocIdentity.Text = "";
        txtIssueDate.Text = "";
        txtEffectDate.Text = "";
        txtExpireDate.Text = "";
        txtGazetteNumber.Text = "";
        txtGazetteDate.Text = "";
        txtFields.Text = "";
        txtOrgans.Text = "";
        txtSigners.Text = "";
        txtKeywords.Text = "";
        
        ddlDocTypes.SelectedIndex = -1;
        ddlEffectStatus.SelectedIndex = -1;
        ddlUseStatus.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        tblDocshowByIdentity.Visible = false;
    }
    private void ClearFormEn()
    {
        txtDocName.Text = "";
        txtIssueDate.Text = "";
        txtEffectDate.Text = "";
        txtExpireDate.Text = "";
        txtGazetteNumber.Text = "";
        txtGazetteDate.Text = "";
        txtFields.Text = "";
        txtOrgans.Text = "";
        txtSigners.Text = "";
        lblMessage.Text = "";
        txtKeywords.Text = "";
        
        ddlDocTypes.SelectedIndex = -1;
        ddlEffectStatus.SelectedIndex = -1;
        ddlUseStatus.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        tblDocshowByIdentity.Visible = false;
    }
    protected void SaveData()
    {
        try
        {
            short SysMessageId = 0;
            Docs m_Docs = new Docs();
            DocFields m_DocFields= new DocFields();
            DocOrgans m_DocOrgans = new DocOrgans();
            DocSigners m_DocSigners = new DocSigners();
            if (DocId == 0)
            {
                if (ddlSelectDocs.Items.Count > 0)
                {
                    DocId = int.Parse(ddlSelectDocs.SelectedValue);
                }
                else
                {
                    if (byte.Parse(ddlLanguage.SelectedValue) == 2)
                    {
                        //lblMessage.Text = "Bạn phải chọn văn bản tiếng việt trước khi thêm mới";
                        tblMsg.Visible = false;
                        JSAlertHelpers.ShowNotify("15", "warning", "Bạn phải chọn văn bản tiếng việt trước khi thêm mới", this);
                        return;
                    }
                }
            }
            
            m_Docs.DocId = DocId;
            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Docs = m_Docs.Get();
            m_Docs.DocName = txtDocName.Text.ToString();
            m_Docs.DocIdentity = txtDocIdentity.Text;
            m_Docs.DocIdentityClear = sms.utils.StringUtil.RemoveSign4VietnameseString(txtDocIdentity.Text);
            m_Docs.DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            m_Docs.IssueDate = (txtIssueDate.Text !="" ? Convert.ToDateTime(txtIssueDate.Text) : DateTime.MinValue);
            m_Docs.EffectDate = (txtEffectDate.Text != "" ? Convert.ToDateTime(txtEffectDate.Text) : DateTime.MinValue);
            m_Docs.ExpireDate = (txtExpireDate.Text != "" ? Convert.ToDateTime(txtExpireDate.Text) : DateTime.MinValue);
            m_Docs.GazetteNumber = txtGazetteNumber.Text;
            m_Docs.GazetteDate = (txtGazetteDate.Text != "" ? Convert.ToDateTime(txtGazetteDate.Text) : DateTime.MinValue);
            
            m_Docs.EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            m_Docs.UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            m_Docs.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            
            m_Docs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (byte.Parse(ddlLanguage.SelectedValue)==1)
            {
                m_DocFields.DocId = m_Docs.DocId;
                m_DocFields.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocFields.InsertByFieldName(ConstantHelpers.Replicated, ActUserId, txtFields.Text.Trim());
            }
            
            if ( byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocSigners.DocId = m_Docs.DocId;
                m_DocSigners.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocSigners.InsertBySignerName(ConstantHelpers.Replicated, ActUserId, txtSigners.Text.Trim());
            }
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocOrgans.DocId = m_Docs.DocId;
                m_DocOrgans.OrganTypeId = 1;
                m_DocOrgans.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocOrgans.InsertByOrganName(ConstantHelpers.Replicated, ActUserId, txtOrgans.Text.Trim());
            }

            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                DocProvinces m_DocProvinces = new DocProvinces();
                m_DocProvinces.DocId = DocId;
                m_DocProvinces.CrDatetime = DateTime.Now;
                m_DocProvinces.InsertByProvinceName(ConstantHelpers.Replicated, ActUserId, txtProvices.Text.Trim());
            }
            //Insert DocKeyword
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                DocKeywords m_DocKeywords = new DocKeywords();
                m_DocKeywords.DocId = m_Docs.DocId;
                m_DocKeywords.DeleteByDocId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (!string.IsNullOrEmpty(txtKeywords.Text))
                {
                    m_DocKeywords.InsertByString(ConstantHelpers.Replicated, ActUserId, txtKeywords.Text.Replace(";", ","));
                }
            }
            JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thuộc tính văn bản thành công", this);
            //JSAlertHelpers.Alert("Cập nhật thuộc tính văn bản thành công", this);
            //lblMessage.Text = "Cập nhật thuộc tính văn bản thành công";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lnkSearchIdentity_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFormEn();
            ddlSelectDocs.Items.Clear();
            int RowCount = 0;
            Docs m_Docs = new Docs();
            string identity = txtDocIdentity.Text.Trim();
            byte m_IsSearchExact = 1;            
            byte m_HighlightKeyword = 1;
            byte m_HasDocFile = 0;
            identity = identity.Replace("Đ", "D");
            identity = identity.Replace("đ", "d");
            identity = identity.Replace("u", "ư");
            identity = identity.Replace("U", "Ư");
            l_Docs = m_Docs.GetPage(ActUserId, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_DOCS_SELECTED"]), 0, "DocName", 1, "", m_IsSearchExact, m_HighlightKeyword, 0, "", "", identity, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, m_HasDocFile, 0, "", "", "", ref RowCount);
            if (RowCount > 0)
            {
                ddlSelectDocs.DataSource = l_Docs;
                ddlSelectDocs.DataBind();
                tblDocshowByIdentity.Visible = true;
                tblMsg.Visible = false;
            }
            else
            {
                tblMsg.Visible = true;
                tblDocshowByIdentity.Visible = false;
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void GetPropertiesByDocIdAndLanguageId(int DocId, byte LanguageId)
    {
     try
        {
                Docs m_Docs = new Docs();
                DocFields m_DocFields = new DocFields();
                DocOrgans m_DocOrgans = new DocOrgans();
                DocSigners m_DocSigners = new DocSigners();
                string FieldName="";
                string OrganName = "";
                string SignerName = "";
                m_Docs = m_Docs.Get(DocId, LanguageId);
                if (m_Docs.DocId > 0)
                {
                    txtDocName.Text = m_Docs.DocName;
                    txtDocIdentity.Text = m_Docs.DocIdentity;
                    txtIssueDate.Text = (m_Docs.IssueDate == DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                    txtEffectDate.Text = (m_Docs.EffectDate == DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                    txtExpireDate.Text = (m_Docs.ExpireDate == DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                    txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref FieldName);
                    txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref OrganName);
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref SignerName);
                    
                    DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void ddlSelectDocs_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPropertiesByDocIdAndLanguageId(int.Parse(ddlSelectDocs.SelectedValue), 1);
    }
    protected void lnkSelectDocs_Click(object sender, EventArgs e)
    {
        GetPropertiesByDocIdAndLanguageId(int.Parse(ddlSelectDocs.SelectedValue), 1);
    }
    
}