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
using HtmlAgilityPack;
public partial class Admin_DocsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    public int DocId = 0;
    protected byte ReviewStatusId = 0;
    private int ActUserId = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    private const byte DocNotContent = 8;
    bool isUpdateSynStatusId = true;
    protected void Page_Load(object sender, EventArgs e)
    {

        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (LanguageId == 2)
        {
            Response.Redirect(Request.RawUrl.Replace("DocsEdit.aspx", "DocsEditEnglish.aspx"));
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            short docgroupId = short.Parse(ddlDocGroups.SelectedValue);
            short displayTypeId = (short)(docgroupId == 1 ? 62 : (docgroupId == 2 ? 5 : (docgroupId == 5 ? 54 : (docgroupId == 6 ? 67 : 61))));
            DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
            //DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "...");
            //DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "");
            //DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList(), "Chưa chọn");DocTypeDisplays.Static_GetList_ByDisplayTypeId((short)62)
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
        l_DataSources = m_DataSources.GetList();
        List<DocFiles> l_DocFiles = new List<DocFiles>();
        try
        {
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
            }
            m_DocFiles.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            l_DocFiles = m_DocFiles.DocFiles_GetList(ActUserId, "DocFileId", DocId);
            m_grid_File.DataSource = l_DocFiles;
            m_grid_File.DataBind();
            txtDocFileName.Text = (l_DocFiles.Count > 0) ? l_DocFiles[0].DocFileName : "";
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
                cbAutoProcessFootNote.Checked = false;
                cbAutoProcessImage.Checked = false;
                cbAutoProcessTable.Checked = false;
                cbAutoRemoveNeo.Checked = false;
                Docs m_Docs = new Docs();
                DocFields m_DocFields = new DocFields();
                DocOrgans m_DocOrgans = new DocOrgans();
                DocSigners m_DocSigners = new DocSigners();
                string FieldName = "";
                string OrganName = "";
                string SignerName = "";
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Docs.DocId > 0)
                {
                    txtDocName.Text = m_Docs.DocName;
                    txtDocIdentity.Text = m_Docs.DocIdentity;
                    txtIssueDate.Text = (m_Docs.IssueDate == DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                    txtEffectDate.Text = (m_Docs.EffectDate == DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                    txtExpireDate.Text = (m_Docs.ExpireDate == DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                    txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                    txtDocContent.Text = m_Docs.DocContent;
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(1, DocId, ref FieldName);
                    txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId, ref OrganName);
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(1, DocId, ref SignerName);
                    txtProvices.Text = DocProvinces.Static_GetProviceDesc(DocId);
                    //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDocGroups, m_Docs.DocGroupId.ToString());

                    short docgroupId = m_Docs.DocGroupId;
                    short displayTypeId = (short)(docgroupId == 1 ? 62 : (docgroupId == 2 ? 5 : (docgroupId == 5 ? 54 : (docgroupId == 6 ? 67 : 61))));
                    DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
                    DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());

                    rfvSigners.Enabled = true;
                    SignerRequired.Visible = true;
                    rfvFields.Enabled = true;
                    fieldRequired.Visible = true;
                    rqEffectDate.Visible = true;
                    RequiredFieldValidator1.Enabled = true;
                    RequiredFieldValidator1.IsValid = true;

                    if (m_Docs.DocGroupId == DocNotContent)
                    {
                        rfvSigners.Enabled = false;
                        SignerRequired.Visible = false;
                        rfvFields.Enabled = false;
                        fieldRequired.Visible = false;
                        rqEffectDate.Visible = false;
                        RequiredFieldValidator1.Enabled = false;
                        RequiredFieldValidator1.IsValid = false;
                    }
                    
                    txtDocContent.Text = m_Docs.DocContent;
                    if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]))
                    {
                        ReviewStatusId = m_Docs.ReviewStatusId;
                        btnSave.Enabled = false;
                        btnSave.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        btnSaveAndNew.Enabled = false;
                        btnSaveAndNew.ToolTip = "Không thể sửa văn bản khi đã duyệt ";
                        m_grid_File.Columns[2].Visible = false;
                        JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa văn bản khi đã duyệt.", this);
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
                    if(lDocKeywords!= null&& lDocKeywords.Any())
                    {
                        foreach(DocKeywords myDocKeywords in lDocKeywords)
                        {
                            Keywords mKeywords = m_Keywords.Get(myDocKeywords.KeywordId, LanguageId);
                            if(string.IsNullOrEmpty(m_KeywordsString))
                            {
                                m_KeywordsString = mKeywords.KeywordName;
                            }
                            else
                            {
                                m_KeywordsString +="; "+ mKeywords.KeywordName;
                            }
                        }
                    }
                    txtKeywords.Text = m_KeywordsString;
                }
                else
                {
                    ClearForm();
                }
            }
            else
            {
                if (ddlDocGroups.SelectedValue == "8")
                {
                    ddlUseStatus.SelectedValue = "3";
                }
                cbAutoProcessFootNote.Checked = true;
                cbAutoProcessImage.Checked = true;
                cbAutoProcessTable.Checked = true;
                cbAutoRemoveNeo.Checked = true;
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
                m_DocFiles.DocId = delId;
                m_DocFiles = m_DocFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_DocFiles.FilePath.ToString().Replace("/", "\\");
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                JSAlertHelpers.ShowNotify("15", "success", "Xóa file đính kèm thành công.", this);
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
        int RowCount = 0;
        lnkSaveSHVB.Visible = false;
        string identity = txtDocIdentity.Text.Trim();
        Docs m_Docs = new Docs();
        identity = identity.Replace("Đ", "D");
        identity = identity.Replace("đ", "d");
        identity = identity.Replace("u", "ư");
        identity = identity.Replace("U", "Ư");
        //List<Docs> listDocs = new List<Docs>();
        //int RowAmount = 10; string OrderBy = string.Empty;
        //byte LanguageVNId = 1, ReviewStatusId = 0;
        //if (!string.IsNullOrEmpty(txtIssueDate.Text.Trim()))
        //{
        //    //listDocs = m_Docs.Docs_GetListByDocIdentityAndIssueDate(ActUserId, RowAmount, OrderBy, LanguageVNId, 0, identity, issueDate, ReviewStatusId);
        //    listDocs = m_Docs.GetPage(ActUserId, RowAmount, 0, OrderBy, LanguageVNId, "", 0, 0,
        //                                        0, "", "", identity, 0, 0, 0, 0, 0, 0, 0,
        //                                        0, 0, 0, ReviewStatusId, 0, 0, 0, 0, 0, "IssueDate", txtIssueDate.Text.Trim(), txtIssueDate.Text.Trim(), ref RowCount);
        //}
        //if (listDocs.Count > 0 && (Request.Params["DocId"] == null || Request.Params["DocId"].ToString() == ""))
        //{
        //    lnkSaveSHVB.Visible = true;
        //    btnSave.Visible = false;
        //    btnSaveAndNew.Visible = false;
        //    JSAlertHelpers.ShowNotify("15", "warning", "Có" + listDocs.Count.ToString() + " văn bản có số hiệu " + txtDocIdentity.Text + ". Nếu bạn muốn thêm mới vui lòng ấn nút Lưu ở bên dưới để lưu văn bản.", this);
        //}
        //else
        //{
            int DocId = SaveData();
            if (Request.Params["DocId"] == null || Request.Params["DocId"].ToString() == "")
            {
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới văn bản thành công.", this);
                if (!String.IsNullOrEmpty(Request["backUrl"]))
                {
                    string backUrl = Request["backUrl"];
                    Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
                    Response.Redirect("DocsEdit.aspx?DocId=" + DocId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&backUrl=" + backUrl);
                }
                else
                {
                    Response.Redirect("DocsEdit.aspx?DocId=" + DocId.ToString() + "&LanguageId=" + LanguageId.ToString());
                }
            }
            else
            {
                BindData();
            }
        //}

    }
    
    protected void lnkSaveSHVB_Click(object sender, EventArgs e)
    {

        int DocId = SaveData();
        if (Request.Params["DocId"] == null || Request.Params["DocId"].ToString() == "")
        {
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới văn bản thành công.", this);
            if (!String.IsNullOrEmpty(Request["backUrl"]))
            {
                string backUrl = Request["backUrl"];
                Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
                Response.Redirect("DocsEdit.aspx?DocId=" + DocId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&backUrl=" + backUrl);
            }
            else
            {
                Response.Redirect("DocsEdit.aspx?DocId=" + DocId.ToString() + "&LanguageId=" + LanguageId.ToString());
            }
        }
        else
        {
            BindData();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Docs m_Docs = new Docs();
        m_Docs = m_Docs.Get(DocId, LanguageId);
        if (!String.IsNullOrEmpty(Request["backUrl"]))
        {
            string backUrl = Request["backUrl"];
            Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
        }
        else
        {
            if (m_Docs.DocGroupId == 3)
                Response.Redirect("VietNamStandards.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 4)
                Response.Redirect("DocsTTHC.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 5)
                Response.Redirect("DocsConsolidation.aspx?LanguageId=" + LanguageId.ToString());
            else Response.Redirect("Docs.aspx?LanguageId=" + LanguageId.ToString());
        }
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        int RowCount = 0;
        lnkSaveSHVB.Visible = false;
        string identity = txtDocIdentity.Text.Trim();
        Docs m_Docs = new Docs();
        identity = identity.Replace("Đ", "D");
        identity = identity.Replace("đ", "d");
        identity = identity.Replace("u", "ư");
        identity = identity.Replace("U", "Ư");
        //List<Docs> listDocs = new List<Docs>();
        //int RowAmount = 10; string OrderBy = string.Empty;
        //byte LanguageVNId = 1, ReviewStatusId = 0;
        //if (!string.IsNullOrEmpty(txtIssueDate.Text.Trim()))
        //{
        //    listDocs = m_Docs.GetPage(ActUserId, RowAmount, 0, OrderBy, LanguageVNId, "", 0, 0,
        //                                                    0, "", "", identity, 0, 0, 0, 0, 0, 0, 0,
        //                                                    0, 0, 0, ReviewStatusId, 0, 0, 0, 0, 0, "IssueDate", txtIssueDate.Text.Trim(), txtIssueDate.Text.Trim(), ref RowCount);
        //}
        ////listDocs = m_Docs.Docs_GetListByDocIdentity(ActUserId, RowAmount, OrderBy, LanguageVNId, identity, ReviewStatusId);
        //if (listDocs.Count > 0 && (Request.Params["DocId"] == null || Request.Params["DocId"].ToString() == ""))
        //{
        //    lnkSaveSHVB.Visible = true;
        //    btnSave.Visible = false;
        //    btnSaveAndNew.Visible = false;
        //    JSAlertHelpers.ShowNotify("15", "success", "Có" + listDocs.Count.ToString() + " văn bản có số hiệu " + txtDocIdentity.Text + ". Nếu bạn muốn thêm mới vui lòng ấn nút Lưu ở bên dưới để lưu văn bản.", this);
        //}
        //else
        //{
            SaveData();
            ClearForm();
            if (DocId > 0)
            {
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật văn bản thành công.", this);
                Response.Redirect("DocsEdit.aspx?LanguageId=" + LanguageId.ToString());
            }
        //}
        //JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới văn bản thành công.", this);
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
        txtIssueDate.Text = "";
        txtEffectDate.Text = "";
        txtExpireDate.Text = "";
        txtGazetteNumber.Text = "";
        txtGazetteDate.Text = "";
        txtFields.Text = "";
        txtOrgans.Text = "";
        txtSigners.Text = "";
        lblMessage.Text = "";
        txtProvices.Text = "";
        txtDocContent.Text = "";
        txtKeywords.Text = "";
        //ddlDocGroups.SelectedIndex = -1;
        //ddlDataSources.SelectedIndex = -1;
        ddlDocTypes.SelectedIndex = -1;
        ddlEffectStatus.SelectedIndex = -1;
        ddlUseStatus.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        tblDocshowByIdentity.Visible = false;
    }
    private void ProcessFootNote()
    {
        HtmlDocument m_HtmlDocument = new HtmlDocument();
        m_HtmlDocument.LoadHtml(txtDocContent.Text);
        HtmlNodeCollection l_NodeA = m_HtmlDocument.DocumentNode.SelectNodes("//a");
        if (l_NodeA != null && cbAutoProcessFootNote.Checked)
        {
            for (int indexNode = 0; indexNode < l_NodeA.Count; indexNode++)
            {
                HtmlNode m_HtmlNode = l_NodeA[indexNode];
                if (m_HtmlNode.Attributes["href"] == null)
                    continue;
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
        }
        // add process table
        if (m_HtmlDocument.DocumentNode.SelectNodes("//table") != null && cbAutoProcessTable.Checked)
        {
            foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("//table"))
            {
                string tableStyle = "";
                if (m_HtmlNode.Attributes["style"] != null)
                    tableStyle = m_HtmlNode.Attributes["style"].Value;
                // remove all style
                if (!String.IsNullOrEmpty(tableStyle))
                {
                    string[] l_TableStyle = tableStyle.Split(';');
                    foreach (string m_Style in l_TableStyle)
                    {
                        if (m_Style.Contains("border"))
                        {
                            if (!String.IsNullOrEmpty(tableStyle))
                            {
                                tableStyle = m_Style;
                            }
                            else
                            {
                                tableStyle += ";" + m_Style;
                            }
                        }
                    }
                }
                m_HtmlNode.SetAttributeValue("style", tableStyle);
                m_HtmlNode.SetAttributeValue("width", "100%");
            }
        }
        // process image
        if (m_HtmlDocument.DocumentNode.SelectNodes("//img") != null && cbAutoProcessImage.Checked)
        {
            foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("//img"))
            {
                string ImageUrl = "";
                if (m_HtmlNode.Attributes["src"] != null)
                    ImageUrl = m_HtmlNode.Attributes["src"].Value;
                // remove all style
                if (String.IsNullOrEmpty(ImageUrl) || ImageUrl.Contains("file:///"))
                {
                    sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t remove: " + ImageUrl);
                    m_HtmlNode.Remove();
                }
            }
        }
        // process NEO
        if (m_HtmlDocument.DocumentNode.SelectNodes("//a") != null && cbAutoRemoveNeo.Checked)
        {
            foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("//a"))
            {               
                if (m_HtmlNode.Attributes["href"] == null)
                {
                    HtmlNodeCollection children = m_HtmlNode.ChildNodes; //get <removeme>'s children
                    HtmlNode parent = m_HtmlNode.ParentNode; //get <removeme>'s parent
                    m_HtmlNode.Remove(); //remove <removeme>
                    parent.AppendChildren(children); //append the children to the parent
                }                
            }
        }
        txtDocContent.Text = m_HtmlDocument.DocumentNode.InnerHtml;

    }
    protected int SaveData()
    {
        int RetVal = DocId;
        try
        {
            ProcessFootNote();
            short SysMessageId = 0;
            Docs m_Docs = new Docs();
            DocFields m_DocFields = new DocFields();
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
                        JSAlertHelpers.Alert("Bạn cần chọn văn bản tiếng Việt trước khi thêm mới văn bản ngông ngữ khác.", this);
                        tblMsg.Visible = false;
                        return DocId;
                    }
                }
            }

            m_Docs.DocId = DocId;
            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            if (DocId > 0)
                m_Docs = m_Docs.Get();
            m_Docs.DocName = txtDocName.Text.ToString();
            m_Docs.DocIdentity = txtDocIdentity.Text;
            m_Docs.DocIdentityClear = sms.utils.StringUtil.RemoveSign4VietnameseString(txtDocIdentity.Text);
            m_Docs.DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            m_Docs.IssueDate = (txtIssueDate.Text != "" ? Convert.ToDateTime(txtIssueDate.Text) : DateTime.MinValue);
            m_Docs.EffectDate = (txtEffectDate.Text != "" ? Convert.ToDateTime(txtEffectDate.Text) : DateTime.MinValue);
            m_Docs.ExpireDate = (txtExpireDate.Text != "" ? Convert.ToDateTime(txtExpireDate.Text) : DateTime.MinValue);
            m_Docs.GazetteNumber = (txtGazetteNumber.Text != "" ? txtGazetteNumber.Text : "");
            m_Docs.GazetteDate = (txtGazetteDate.Text != "" ? Convert.ToDateTime(txtGazetteDate.Text) : DateTime.MinValue);
            //m_Docs.DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            m_Docs.EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            m_Docs.HasContent = txtDocContent.Text.Trim() != "" ? (byte)1 : (byte)0;
            m_Docs.UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            m_Docs.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Docs.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
            m_Docs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            RetVal = m_Docs.DocId;
            if (txtFields.Text.Trim() != "" && byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocFields.DocId = m_Docs.DocId;
                m_DocFields.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocFields.InsertByFieldName(ConstantHelpers.Replicated, ActUserId, txtFields.Text.Trim());
            }
            if (txtOrgans.Text.Trim() != "" && byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocOrgans.DocId = m_Docs.DocId;
                m_DocOrgans.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocOrgans.InsertByOrganName(ConstantHelpers.Replicated, ActUserId, txtOrgans.Text.Trim());
            }
            if (txtSigners.Text.Trim() != "" && byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                m_DocSigners.DocId = m_Docs.DocId;
                m_DocSigners.DeleteByDocId(ConstantHelpers.Replicated, ActUserId);
                m_DocSigners.InsertBySignerName(ConstantHelpers.Replicated, ActUserId, txtSigners.Text.Trim());
            }
            if (byte.Parse(ddlLanguage.SelectedValue) == 1)
            {
                DocProvinces m_DocProvinces = new DocProvinces();
                m_DocProvinces.DocId = m_Docs.DocId;
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
            if (m_Docs.DocId > 0)
            {
                // Cập nhật nội dung văn bản
                // m_Docs.DocId = DocId;
                m_Docs.DocContent = txtDocContent.Text.ToString();
                if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
                {
                    LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
                }
                else
                {
                    LanguageId = 1;
                }
                m_Docs.LanguageId = LanguageId;
                if (DocId == 0) {
                    m_Docs.InsertContent(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
                else
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
                        m_DocFiles.FileTypeId = 1;
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
                            m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 0);
                            DocFiles.PutNofitySync(m_DocFiles.DocFileId);
                        }
                        else
                        {
                            m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 1);
                        }
                    }
                }
                if (DocId == 0)
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Thêm mới văn bản thành công.", this);
                    //lblMessage.Text = "Thêm mới văn bản thành công.";
                }
                else
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Cập nhật văn bản thành công.", this);
                    //lblMessage.Text = "Cập nhật văn bản thành công.";
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
        return RetVal;
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
            List<Docs> listDocs = new List<Docs>();
            int RowAmount = 10; string OrderBy = string.Empty;
            byte LanguageId = 1, ReviewStatusId = 0;
            listDocs = m_Docs.Docs_GetListByDocIdentity(ActUserId, RowAmount, OrderBy, LanguageId, identity, ReviewStatusId);
            if (listDocs != null && listDocs.Any())
            {
                DocFields m_DocFields = new DocFields();
                DocOrgans m_DocOrgans = new DocOrgans();
                DocSigners m_DocSigners = new DocSigners();
                string FieldName = "";
                string OrganName = "";
                string SignerName = "";
                m_Docs = listDocs[0];
                txtDocName.Text = m_Docs.DocName;
                txtIssueDate.Text = m_Docs.IssueDate.ToString("dd/MM/yyyy");
                txtEffectDate.Text = m_Docs.EffectDate.ToString("dd/MM/yyyy");
                txtExpireDate.Text = m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString(); ;
                txtGazetteDate.Text = m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                txtIssueDate.Text = (m_Docs.IssueDate == DateTime.MinValue) ? "" : m_Docs.IssueDate.ToString("dd/MM/yyyy");
                txtEffectDate.Text = (m_Docs.EffectDate == DateTime.MinValue) ? "" : m_Docs.EffectDate.ToString("dd/MM/yyyy");
                txtExpireDate.Text = (m_Docs.ExpireDate == DateTime.MinValue) ? "" : m_Docs.ExpireDate.ToString("dd/MM/yyyy");
                txtGazetteNumber.Text = m_Docs.GazetteNumber.ToString();
                txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                txtFields.Text = m_DocFields.DocFields_GetFieldName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref FieldName);
                txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref OrganName);
                txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(byte.Parse(ddlLanguage.SelectedValue), m_Docs.DocId, ref SignerName);
                //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
            }

          
            //l_Docs = m_Docs.GetPage(ActUserId, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_DOCS_SELECTED"]), 0, "DocName", 1, "", m_IsSearchExact, m_HighlightKeyword, 0, "", "", identity, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, m_HasDocFile, 0, "", "", "", ref RowCount);
            //if (RowCount > 0)
            //{
            //    ddlSelectDocs.DataSource = l_Docs;
            //    ddlSelectDocs.DataBind();
            //    tblDocshowByIdentity.Visible = true;
            //    tblMsg.Visible = false;
            //}
            //else
            //{
            //    tblMsg.Visible = true;
            //    tblDocshowByIdentity.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    //public string GetLawdocNo(string Header, out string LawdocNo)
    //{
    //    string RetVal = "";
    //    LawdocNo = "";
    //    string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
    //    List<RegexRules> l_RegexRulesLawdocNo = RegexRules.GetRegexRulesLawdocNo();
    //    foreach (string m_Lines in l_Lines)
    //    {
    //        if (String.IsNullOrEmpty(RetVal) == false)
    //            break;
    //        foreach (RegexRules m_RegexRule in l_RegexRulesLawdocNo)
    //        {
    //            RegexOptions m_RegexOptions = RegexOptions.None;
    //            if (m_RegexRule.RegexRuleMultiline)
    //            {
    //                if (m_RegexRule.RegexRuleIgnoreCase)
    //                {
    //                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
    //                }
    //                else
    //                {
    //                    m_RegexOptions = RegexOptions.Multiline;
    //                }
    //            }
    //            else if (m_RegexRule.RegexRuleIgnoreCase)
    //            {
    //                m_RegexOptions = RegexOptions.IgnoreCase;
    //            }
    //            Match m_Match = Regex.Match(Header, m_RegexRule.RegexRuleText, m_RegexOptions);
    //            if (m_Match.Success)
    //            {
    //                RetVal = m_Match.Groups[0].Value;
    //                LawdocNo = m_Match.Groups[int.Parse(m_RegexRule.RegexRulesGroup)].Value;
    //                break;
    //            }
    //        }
    //    }
    //    return RetVal;
    //}
    protected void GetPropertiesByDocIdAndLanguageId(int DocId, byte LanguageId)
    {
        try
        {
            Docs m_Docs = new Docs();
            DocFields m_DocFields = new DocFields();
            DocOrgans m_DocOrgans = new DocOrgans();
            DocSigners m_DocSigners = new DocSigners();
            string FieldName = "";
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
                //DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Docs.DataSourceId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_Docs.DocTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlEffectStatus, m_Docs.EffectStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlUseStatus, m_Docs.UseStatusId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Docs.ReviewStatusId.ToString());
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
        rfvSigners.Enabled = true;
        SignerRequired.Visible = true;
        rfvFields.Enabled = true;
        fieldRequired.Visible = true;
        RequiredFieldValidator1.Enabled = true;
        rqEffectDate.Visible = true;

        if (docgroupId == DocNotContent)
        {
            rfvSigners.Enabled = false;
            SignerRequired.Visible = false;
            rfvFields.Enabled = false;
            fieldRequired.Visible = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator1.IsValid = false;
            rqEffectDate.Visible = false;
        }
        else
        {
            if (String.IsNullOrEmpty(txtEffectDate.Text) && !RequiredFieldValidator1.IsValid)
            {
                RequiredFieldValidator1.IsValid = true;
            }
        }
        string m_DocTypeCurrent = ddlDocTypes.SelectedValue;
        short displayTypeId = (short)(docgroupId == 1 ? 62 : (docgroupId == 2 ? 5 : (docgroupId == 5 ? 54 : (docgroupId == 6 ? 67 : 61))));
        DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
        DropDownListHelpers.DDL_SetSelected(ddlDocTypes, m_DocTypeCurrent);
        //BindData();
    }

}