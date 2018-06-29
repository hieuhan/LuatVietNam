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

public partial class Admin_DocsStandardEditAttribute : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    protected byte ReviewStatusId = 0;
    private int ActUserId = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    protected int StepCount = 1;
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
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.BindDropDownList(ddlGrantLevelId, GrantLevels.Static_GetList(), "");
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
        }
        else 
        {
            lnkSearchIdentity.Visible = false;
        }
       
    }
    private void bindFiles()
    {
        DocFiles m_DocFiles = new DocFiles();
        DataSources m_DataSources = new DataSources();
        l_DataSources=m_DataSources.GetList();
        List<DocFiles> l_DocFiles = new List<DocFiles>();
        try
        {
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
            }
            l_DocFiles = m_DocFiles.DocFiles_GetList(ActUserId, "DocFileId", DocId);
            m_grid_File.DataSource = l_DocFiles;
            m_grid_File.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void bindDocSteps()
    {
        DocSteps m_DocSteps = new DocSteps();
        DataSources m_DataSources = new DataSources();
        l_DataSources = m_DataSources.GetList();
        List<DocSteps> l_DocSteps = new List<DocSteps>();
        try
        {
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
            }
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 100;
            int PageNumber = 0;
            int RowCount = 0;
            m_DocSteps.DocId = DocId;
            l_DocSteps = m_DocSteps.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
            if(l_DocSteps.Count == 0)
            {
                m_DocSteps.StepOrder = 1;
                l_DocSteps.Add(m_DocSteps);

            }
            StepCount = l_DocSteps.Count;
            gvDocStep.DataSource = l_DocSteps;
            gvDocStep.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
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
                    txtDocName.Text = m_Docs.DocName;
                    txtDocIdentity.Text = m_Docs.DocIdentity;
                    txtIssueDate.Text = (m_Docs.IssueDate==DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                    txtEffectDate.Text =(m_Docs.EffectDate==DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                    txtExpireDate.Text = (m_Docs.ExpireDate==DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                    txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(1, DocId, ref FieldName);
                    txDocObject.Text = m_Docs.DocObject;
                    txSettlementTime.Text = m_Docs.SettlementTime;
                    txFee.Text = m_Docs.Fee;
                    txElementDossier.Text = m_Docs.ElementDossier;
                    txPerformMethod.Text = m_Docs.PerformMethod;
                    txResult.Text = m_Docs.Result;
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(1, DocId, ref SignerName);
                    txtFee.Text = m_Docs.Fee;

                    txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId, ref OrganName);
                    //txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId,1, ref OrganName);
                    txtOrgans5.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId,5, ref OrganName);
                    txtOrgans6.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId, 6, ref OrganName);
                    txtProvices.Text = DocProvinces.Static_GetProviceDesc(DocId);
                    DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlGrantLevelId, m_Docs.GrantLevelId.ToString());
                    //if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    //{
                    //    ReviewStatusId = m_Docs.ReviewStatusId;
                    //    btnSave.Enabled = false;
                    //    btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa văn bản khi đã duyệt ", this);
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;

                    //}
                    if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    {
                        if(txtDocName!= null && txtDocName.Text!="")
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
                        if (txtFee != null && txtFee.Text != "")
                        {
                            txtFee.Enabled = false;
                        }
                        if (txtEffectDate != null && txtEffectDate.Text != "")
                        {
                            txtEffectDate.Enabled = false;
                        }
                        if (txtExpireDate != null && txtExpireDate.Text != "")
                        {
                            txtExpireDate.Enabled = false;
                        }
                        if (txtFields != null && txtFields.Text != "")
                        {
                            txtFields.Enabled = false;
                        }
                        if (txtOrgans != null && txtOrgans.Text != "")
                        {
                            txtOrgans.Enabled = false;
                        }
                        if (txtSigners != null && txtSigners.Text != "")
                        {
                            txtSigners.Enabled = false;
                        }
                    }
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

    protected void m_grid_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        DocFiles m_DocFiles = new DocFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            if (delId > 0)
            {
                m_DocFiles.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_DocFiles.DocId = delId;
                m_DocFiles = m_DocFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_DocFiles.FilePath.ToString().Replace("/", "\\");
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                bindFiles();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        SaveData();
        //lblMsg.Text = "Cập nhật thuộc tính văn bản thành công";
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
        txtOrgans5.Text = "";
        txtOrgans6.Text = "";
        txtKeywords.Text = "";
        ddlGrantLevelId.SelectedIndex = -1;
        ddlDataSources.SelectedIndex = -1;
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
        txtKeywords.Text = "";
        txtSigners.Text = "";
        lblMessage.Text = "";
        ddlDataSources.SelectedIndex = -1;
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
                        //tblMsg.Visible = false;
                        JSAlertHelpers.ShowNotify("15", "warning", "Bạn phải chọn văn bản tiếng việt trước khi thêm mới", this);
                        return;
                    }
                }
            }
            
            m_Docs.DocId = DocId;
            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Docs.DocName = txtDocName.Text.ToString();
            m_Docs.DocIdentity = txtDocIdentity.Text;
            m_Docs.DocIdentityClear = sms.utils.StringUtil.RemoveSign4VietnameseString(txtDocIdentity.Text);
            m_Docs.DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            m_Docs.IssueDate = (txtIssueDate.Text !="" ? Convert.ToDateTime(txtIssueDate.Text) : DateTime.MinValue);
            m_Docs.EffectDate = (txtEffectDate.Text != "" ? Convert.ToDateTime(txtEffectDate.Text) : DateTime.MinValue);
            m_Docs.ExpireDate = (txtExpireDate.Text != "" ? Convert.ToDateTime(txtExpireDate.Text) : DateTime.MinValue);
            m_Docs.GazetteNumber = txtGazetteNumber.Text;
            m_Docs.GazetteDate = (txtGazetteDate.Text != "" ? Convert.ToDateTime(txtGazetteDate.Text) : DateTime.MinValue);
            m_Docs.DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            m_Docs.EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            m_Docs.UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            m_Docs.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            //add
            m_Docs.DocObject = txDocObject.Text;
            m_Docs.DocScopes = txDocScopes.Text;

            m_Docs.GrantLevelId = byte.Parse(ddlGrantLevelId.SelectedValue);
            m_Docs.PerformMethod = txPerformMethod.Text;
            m_Docs.NumDossier = byte.Parse(txNumDossier.Text);
            m_Docs.Fee = txtFee.Text;
            m_Docs.ElementDossier = txElementDossier.Text;
            m_Docs.SettlementTime = txSettlementTime.Text;
            m_Docs.Result = txResult.Text;
            m_Docs.ConfirmSignerDate = DateTime.MinValue;
            m_Docs.DocGroupId = 3;
            m_Docs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (byte.Parse(ddlLanguage.SelectedValue)==1)
            {
                m_DocFields.DocId = m_Docs.DocId;
                m_DocFields.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                if (txtFields.Text.Trim() != "")
                m_DocFields.InsertByFieldName(ConstantHelpers.Replicated, ActUserId, txtFields.Text.Trim());
            }
            
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocSigners.DocId = m_Docs.DocId;
                m_DocSigners.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                if (txtSigners.Text.Trim() != "")
                m_DocSigners.InsertBySignerName(ConstantHelpers.Replicated, ActUserId, txtSigners.Text.Trim());
            }
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocOrgans.DocId = m_Docs.DocId;
                //m_DocOrgans.OrganTypeId = 1;
                m_DocOrgans.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                if (txtOrgans.Text.Trim() != "")
                    m_DocOrgans.InsertByOrganName(ConstantHelpers.Replicated, ActUserId, txtOrgans.Text.Trim());
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
            //if (txtOrgans5.Text.Trim() != "" && byte.Parse(ddlLanguage.SelectedValue) == 1)
            //{
            //    m_DocOrgans.DocId = m_Docs.DocId;
            //    m_DocOrgans.OrganTypeId = 5;
            //    m_DocOrgans.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
            //    m_DocOrgans.InsertByOrganName(ConstantHelpers.Replicated, ActUserId, txtOrgans5.Text.Trim());
            //}
            //if (txtOrgans6.Text.Trim() != "" && byte.Parse(ddlLanguage.SelectedValue) == 1)
            //{
            //    m_DocOrgans.DocId = m_Docs.DocId;
            //    m_DocOrgans.OrganTypeId = 6;
            //    m_DocOrgans.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
            //    m_DocOrgans.InsertByOrganName(ConstantHelpers.Replicated, ActUserId, txtOrgans6.Text.Trim());
            //}
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                DocProvinces m_DocProvinces = new DocProvinces();
                m_DocProvinces.DocId = DocId;
                m_DocProvinces.CrDatetime = DateTime.Now;
                m_DocProvinces.DeleteByDocId(DocId);
                if (txtProvices.Text.Trim() != "")
                    m_DocProvinces.InsertByProvinceName(ConstantHelpers.Replicated, ActUserId, txtProvices.Text.Trim());
            }
            JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thuộc tính văn bản thành công", this);
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
                    DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
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

    protected void gvDocStep_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        DocFiles m_DocFiles = new DocFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
}