using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Windows;
using System.IO;
using System.Text;
using System.Reflection;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_DocsEditEnglish : System.Web.UI.Page
{
    private byte LanguageId = 2;
    private int DocId = 0;
    protected byte ReviewStatusId = 0;
    private int ActUserId = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    bool isUpdateSynStatusId = true;
    protected void Page_Load(object sender, EventArgs e)
    {

        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        
        if (!IsPostBack)
        {
            //if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            //{
            //    LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            //}
           
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            //DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "");
            DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId((short)62), "Chưa chọn");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetListOrderBy("DisplayOrder"), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            if (ddlLanguage.SelectedValue == "2")
            {
                if (DocId > 0)
                {
                    lnkSearchIdentity.Visible = false;
                    ClearFormEn();
                }
                else
                {
                    lnkSearchIdentity.Visible = true;
                    ClearFormEn();
                }
            }
            else
            {
                lnkSearchIdentity.Visible = false;
            }
            BindData();
        }
        
    }
    private void bindFiles(int DocInputId = 0)
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
            if (DocInputId == 0)
                DocInputId = DocId;
            m_DocFiles.LanguageId = LanguageId;
            if (DocInputId > 0)
                l_DocFiles = m_DocFiles.DocFiles_GetList(ActUserId, "DocFileId", DocInputId);
            m_grid_File.DataSource = l_DocFiles;
            m_grid_File.DataBind();
            txtDocFileName.Text = (l_DocFiles.Count>0) ? l_DocFiles[0].DocFileName:"";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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
                    txtDocContent.Text = m_Docs.DocContent;
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(1, DocId, ref FieldName);
                    txtOrgans.Text =  m_DocOrgans.DocOrgans_GetOrganName(1, DocId, ref OrganName);
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(1, DocId, ref SignerName);
                    txtProvices.Text = DocProvinces.Static_GetProviceDesc(DocId);
                    //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDocGroups, m_Docs.DocGroupId.ToString());
                    txtDocContent.Text = m_Docs.DocContent;
                    if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    {
                        ReviewStatusId = m_Docs.ReviewStatusId;
                        btnSave.Enabled = false;
                        btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        btnSaveAndNew.Enabled = false;
                        btnSaveAndNew.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        m_grid_File.Columns[2].Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                        btnSaveAndNew.Enabled = true;
                        btnSaveAndNew.ToolTip = "Lưu và thêm mới văn bản khác";
                        m_grid_File.Columns[2].Visible = true;
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
                                m_KeywordsString += ", " + mKeywords.KeywordName;
                            }
                        }
                    }
                    txtKeywords.Text = m_KeywordsString;
                    if (LanguageId == 2)
                    {
                        Docs m_DocsTemp = m_Docs.Get(DocId, 1);
                        txtDocNameVN.Text = m_DocsTemp.DocName;
                    }
                }
                else
                {
                    ClearForm();
                }
            }  
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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
                m_DocFiles.DocFileId = delId;
                m_DocFiles = m_DocFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_DocFiles.FilePath.ToString().Replace("/", "\\");
                //if (System.IO.File.Exists(filepath))
                //{
                //    System.IO.File.Delete(filepath);
                //}
                SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                bindFiles();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message, this);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int DocId = SaveData();
        if (DocId == 0)
            return;
        
        if (Request.Params["DocId"] == null || Request.Params["DocId"].ToString() == "")
        {
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới văn bản thành công.", this);
            Response.Redirect("DocsEditEnglish.aspx?DocId=" + DocId.ToString() + "&LanguageId=2");
        }
       else
        {
            BindData();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        Docs m_Docs = new Docs();
        m_Docs = m_Docs.Get(DocId, LanguageId);
        if (!String.IsNullOrEmpty(Request["backUrl"]))
        {
            string backUrl = Request["backUrl"];
            Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
        }
        else
        {
            if (m_Docs.DocGroupId <= 2)
                Response.Redirect("Docs.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 3)
                Response.Redirect("VietNamStandards.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 4)
                Response.Redirect("DocsTTHC.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 5)
                Response.Redirect("DocsConsolidation.aspx?LanguageId=" + LanguageId.ToString());
        }
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        ClearFormEn();
        if(DocId > 0)
        {
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật văn bản thành công.", this);
            Response.Redirect("DocsEditEnglish.aspx?LanguageId=2");
        }
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
        txtProvices.Text = "";
        txtDocContent.Text = "";
        txtKeywords.Text = "";
        //ddlDataSources.SelectedIndex = -1;
        //ddlDocGroups.SelectedIndex = -1;
        ddlDocTypes.SelectedIndex = -1;
        ddlEffectStatus.SelectedIndex = -1;
        ddlUseStatus.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        tblDocshowByIdentity.Visible = false;
    }
    private void ClearFormEn()
    {
        txtDocName.Text = "";
        txtDocNameVN.Enabled = false;
        txtIssueDate.Text = "";
        txtIssueDate.Enabled = false;
        txtEffectDate.Text = "";
        txtEffectDate.Enabled = false;
        txtExpireDate.Text = "";
        txtExpireDate.Enabled = false;
        txtGazetteNumber.Text = "";
        txtKeywords.Text = "";
        //txtGazetteNumber.Enabled = false;
        txtGazetteDate.Text = "";
        //txtGazetteDate.Enabled = false;
        txtFields.Text = "";
        txtFields.Enabled = false;
        txtOrgans.Text = "";
        txtOrgans.Enabled = false;
        txtSigners.Text = "";
        txtSigners.Enabled = false;
        lblMessage.Text = "";
        txtProvices.Text = "";
        txtProvices.Enabled = false;
        txtDocContent.Text = "";
        ddlDocGroups.SelectedIndex = -1;
        ddlDocGroups.Enabled = false;
        //ddlDataSources.SelectedIndex = -1;
        ddlDocTypes.SelectedIndex = -1;
        ddlDocTypes.Enabled = false;
        ddlEffectStatus.SelectedIndex = -1;

        ddlUseStatus.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        tblDocshowByIdentity.Visible = false;
        if(DocId == 0)
        {
            btnSave.Enabled = false;
            btnSaveAndNew.Enabled = false;
            btnSave.ToolTip = "Tìm VB tiếng Việt trước sau đó nhập nội dung tiếng Anh";
            btnSaveAndNew.ToolTip = "Tìm VB tiếng Việt trước sau đó nhập nội dung tiếng Anh";
        }
        
    }
    protected int SaveData()
    {
        try
        {
            ProcessFootNote();
            short SysMessageId = 0;
            Docs m_Docs = new Docs();
            DocFields m_DocFields= new DocFields();
            DocOrgans m_DocOrgans = new DocOrgans();
            DocSigners m_DocSigners = new DocSigners();
            byte ReviewStatusId = 1;
            if (DocId == 0)
            {
                ReviewStatusId = 1;
                if (ddlSelectDocs.Items.Count > 0)
                {
                    DocId = int.Parse(ddlSelectDocs.SelectedValue);
                }
                else
                {
                    if (byte.Parse(ddlLanguage.SelectedValue) == 2)
                    {
                        lblMessage.Text = "Bạn phải chọn văn bản tiếng việt trước khi thêm mới";
                        tblMsg.Visible = false;
                        return DocId;
                    }
                }
            }
            
            m_Docs.DocId = DocId;
            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            if (DocId > 0)
            {
                m_Docs = m_Docs.Get();
                m_Docs.LanguageId = 2;
                ReviewStatusId = m_Docs.ReviewStatusId;
            }
                
            if (m_Docs.DocId == 0)
            {
                m_Docs.DocId = DocId;
                m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                ReviewStatusId = 1;
            }
            
            m_Docs.DocName = txtDocName.Text.ToString();
            m_Docs.DocIdentity = txtDocIdentity.Text;
            m_Docs.DocIdentityClear = sms.utils.StringUtil.RemoveSign4VietnameseString(txtDocIdentity.Text);
            m_Docs.DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            m_Docs.IssueDate = (txtIssueDate.Text !="" ? Convert.ToDateTime(txtIssueDate.Text) : DateTime.MinValue);
            m_Docs.EffectDate = (txtEffectDate.Text != "" ? Convert.ToDateTime(txtEffectDate.Text) : DateTime.MinValue);
            m_Docs.ExpireDate = (txtExpireDate.Text != "" ? Convert.ToDateTime(txtExpireDate.Text) : DateTime.MinValue);
            m_Docs.GazetteNumber = (txtGazetteNumber.Text!="" ? txtGazetteNumber.Text : "");
            m_Docs.GazetteDate = (txtGazetteDate.Text != "" ? Convert.ToDateTime(txtGazetteDate.Text) : DateTime.MinValue);
            //m_Docs.DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            m_Docs.HasContent = txtDocContent.Text.Trim() != "" ? (byte)1 : (byte)0;
            m_Docs.EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            m_Docs.UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            m_Docs.ReviewStatusId = ReviewStatusId;
            m_Docs.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
            m_Docs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            DocId = m_Docs.DocId;
            //Insert DocKeyword
            if (txtKeywords.Text.Trim() != "")
            {
                DocKeywords m_DocKeywords = new DocKeywords();
                m_DocKeywords.DocId = m_Docs.DocId;
                m_DocKeywords.DeleteByDocId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (!string.IsNullOrEmpty(txtKeywords.Text))
                {
                    m_DocKeywords.InsertByString(ConstantHelpers.Replicated, ActUserId, txtKeywords.Text.Replace(";", ","));
                }
            }
            if (m_Docs.DocId > 0)
            {
                // Cập nhật nội dung văn bản
               // m_Docs.DocId = DocId;
                m_Docs.DocContent = txtDocContent.Text.ToString();
                
                m_Docs.LanguageId = LanguageId;
                m_Docs.UpdateContent(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                HttpFileCollection files = Request.Files;
                string VirtualPath = ConfigurationManager.AppSettings["DirectoryPath"] + "VIETLAWFILE/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string fileName = "";
                string DirectoryPath = "";
                string FilePath = "";
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    if (file.ContentLength > 0)
                    {
                        DocFiles m_DocFiles = new DocFiles();
                        fileName = file.FileName; //.Substring(file.FileName.LastIndexOf("\\"));
                        if (file.FileName.LastIndexOf("\\") > 0) fileName = file.FileName.Substring(fileName.LastIndexOf("\\") + 1);

                        //m_DocFiles.FileName = fileName;
                        fileName = fileName.Replace(" ", "_").Replace("#", "").Replace("$", "").Replace("@", "");
                        fileName = fileName.Insert(fileName.LastIndexOf("."), "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

                        m_DocFiles.DocId = m_Docs.DocId;
                        m_DocFiles.FileTypeId = fileName.Contains(".pdf") ? (byte)2 : (byte)3;
                        m_DocFiles.DocFileName = txtDocFileName.Text;
                        m_DocFiles.DataSourceId = short.Parse(Request.Form["selSelect" + (i + 1).ToString()]);
                        m_DocFiles.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_DocFiles.FilePath = VirtualPath + fileName;
                        m_DocFiles.FileSize = file.ContentLength;
                        //m_DocFiles.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue) ;
                        m_DocFiles.ReviewStatusId = 1;//cho duyet
                        m_DocFiles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                        DirectoryPath = Request.PhysicalApplicationPath + VirtualPath.Replace("/", "\\");
                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }

                        FilePath = DirectoryPath + fileName;
                        file.SaveAs(FilePath);
                        //Put nofity sync file

                        if (isUpdateSynStatusId)
                        {
                            //m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 0);
                            //DocFiles.PutNofitySync(m_DocFiles.DocFileId);
                        }
                        else
                        {
                            //m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 1);
                        }
                    }
                }
                if (DocId == 0)
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Thêm mới văn bản thành công.", this);
                }
                else
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Cập nhật văn bản thành công.", this);
                }
            }
            //JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
        return DocId;
    }
    private void ProcessFootNote()
    {
        HtmlDocument m_HtmlDocument = new HtmlDocument();
        m_HtmlDocument.LoadHtml(txtDocContent.Text);
        HtmlNodeCollection l_NodeA = m_HtmlDocument.DocumentNode.SelectNodes("//a");
        if (l_NodeA != null)
        {
            for (int indexNode = 0; indexNode < l_NodeA.Count; indexNode++)
            {
                HtmlNode m_HtmlNode = l_NodeA[indexNode];
                try
                {
                    if (m_HtmlNode.Attributes["href"].Value.ToString().Contains("#") && !m_HtmlNode.InnerHtml.Contains("<sup") && m_HtmlNode.InnerText.Contains("[") && m_HtmlNode.InnerText.Contains("]"))
                    {
                        m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml.Replace(m_HtmlNode.InnerText, "<sup>" + m_HtmlNode.InnerText + "</sup>");
                    }

                }
                catch (Exception ex)
                {
                    sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                }
            }

            txtDocContent.Text = m_HtmlDocument.DocumentNode.InnerHtml;
        }
    }
    protected void lnkSearchIdentity_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFormEn();            
            tblMsg.Visible = false;           
            tblDocshowByIdentity.Visible = false;

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
            List<Docs> listDocs = new List<Docs>();
            int RowAmount = 100; string OrderBy = string.Empty;
            byte LanguageVNId =1 , ReviewStatusId = 0;
            listDocs = m_Docs.Docs_GetListByDocIdentity(ActUserId, RowAmount, OrderBy, LanguageVNId, identity, ReviewStatusId);
            ddlSelectDocs.DataSource = listDocs;
            ddlSelectDocs.DataBind();
            if (listDocs.Count ==1)
            {
                DocFields m_DocFields = new DocFields();
                DocOrgans m_DocOrgans = new DocOrgans();
                DocSigners m_DocSigners = new DocSigners();
                string FieldName = "";
                string OrganName = "";
                string SignerName = "";
                m_Docs = listDocs[0];
                txtDocNameVN.Text = m_Docs.DocName;
                txtIssueDate.Text = m_Docs.IssueDate.ToString("dd/MM/yyyy");
                txtEffectDate.Text = m_Docs.EffectDate.ToString("dd/MM/yyyy");
                txtExpireDate.Text = m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                
                txtIssueDate.Text = (m_Docs.IssueDate == DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                txtEffectDate.Text = (m_Docs.EffectDate == DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                txtExpireDate.Text = (m_Docs.ExpireDate == DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                txtFields.Text = m_DocFields.DocFields_GetFieldName(LanguageVNId, m_Docs.DocId, ref FieldName);
                txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(LanguageVNId, m_Docs.DocId, ref OrganName);
                txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(LanguageVNId, m_Docs.DocId, ref SignerName);
                txtProvices.Text = DocProvinces.Static_GetProviceDesc(m_Docs.DocId);
                //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocGroups, m_Docs.DocGroupId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                //if exist enlish
                Docs m_DocsTemp = new Docs();
                m_DocsTemp = m_DocsTemp.Get(m_Docs.DocId, byte.Parse(ddlLanguage.SelectedValue));
                if(m_DocsTemp.DocId > 0)
                {
                    txtDocName.Text = m_DocsTemp.DocName;
                    txtGazetteNumber.Text = m_DocsTemp.GazetteNumber.ToString();
                    txtGazetteDate.Text = (m_DocsTemp.GazetteDate == DateTime.MinValue) ? "" : m_DocsTemp.GazetteDate.ToString("dd/MM/yyyy");
                    
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_DocsTemp.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_DocsTemp.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_DocsTemp.ReviewStatusId.ToString());
                    txtDocContent.Text = m_DocsTemp.DocContent;
                    bindFiles(m_DocsTemp.DocId);
                    JSAlertHelpers.ShowNotify("15", "success", "Văn bản tiếng Việt có số hiệu " + txtDocIdentity.Text + " đã tồn tại bản tiếng Anh.", this);
                    if (m_DocsTemp.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    {
                        ReviewStatusId = m_Docs.ReviewStatusId;
                        btnSave.Enabled = false;
                        btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        btnSaveAndNew.Enabled = false;
                        btnSaveAndNew.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        m_grid_File.Columns[2].Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                        btnSaveAndNew.Enabled = true;
                        btnSaveAndNew.ToolTip = "Lưu và thêm mới văn bản khác";
                        m_grid_File.Columns[2].Visible = true;
                        
                    }

                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                    btnSaveAndNew.Enabled = true;
                    btnSaveAndNew.ToolTip = "Lưu và thêm mới văn bản khác";
                    m_grid_File.Columns[2].Visible = true;
                    bindFiles(); 
                }
               
            }
            else if(listDocs.Count == 0)
            {
                tblMsg.Visible = true;
            }
            else
            {
                tblDocshowByIdentity.Visible = true;
                JSAlertHelpers.ShowNotify("15", "success", "Tìm thấy " + listDocs.Count.ToString() + " văn bản tiếng Việt có số hiệu " + txtDocIdentity.Text + ". Mời bạn chọn văn bản cần nhập tiếng Anh từ danh sách dưới nút Tìm kiếm.", this);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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
            byte LanguageVNId = 1, ReviewStatusId = 0;
            m_Docs = m_Docs.Get(DocId, LanguageId);
            if (m_Docs.DocId > 0)
            {
                txtDocNameVN.Text = m_Docs.DocName;
                txtDocIdentity.Text = m_Docs.DocIdentity;
                txtIssueDate.Text = (m_Docs.IssueDate == DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                txtEffectDate.Text = (m_Docs.EffectDate == DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                txtExpireDate.Text = (m_Docs.ExpireDate == DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                txtFields.Text = m_DocFields.DocFields_GetFieldName(LanguageVNId, m_Docs.DocId, ref FieldName);
                txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(LanguageVNId, m_Docs.DocId, ref OrganName);
                txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(LanguageVNId, m_Docs.DocId, ref SignerName);
                txtProvices.Text = DocProvinces.Static_GetProviceDesc(m_Docs.DocId);
                //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocGroups, m_Docs.DocGroupId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                btnSave.Enabled = true;
                btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                btnSaveAndNew.Enabled = true;
                btnSaveAndNew.ToolTip = "Lưu và thêm mới văn bản khác";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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

    protected void ddlDocGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        short docgroupId = short.Parse(ddlDocGroups.SelectedValue);
        short displayTypeId = (short)(docgroupId == 1 ? 62 : (docgroupId == 2 ? 5 : (docgroupId == 5 ? 54 : (docgroupId == 7 ? 63 : 61))));
        DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "");
    }
}