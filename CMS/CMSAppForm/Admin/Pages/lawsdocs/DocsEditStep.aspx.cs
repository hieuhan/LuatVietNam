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
using sms.common;

public partial class Admin_DocsEditStep : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    protected byte ReviewStatusId = 0;
    private int ActUserId = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    protected int StepCount = 1;
    protected bool isDisableStep = false;
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
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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
                    txtGazetteDate.Text = (m_Docs.GazetteDate == DateTime.MinValue) ? "" : m_Docs.GazetteDate.ToString("dd/MM/yyyy");
                    txtFields.Text = m_DocFields.DocFields_GetFieldName(1, DocId, ref FieldName);
                    txDocObject.Text = m_Docs.DocObject;
                    txSettlementTime.Text = m_Docs.SettlementTime;
                    txFee.Text = m_Docs.Fee;
                    txElementDossier.Text = m_Docs.ElementDossier;
                    txPerformMethod.Text = m_Docs.PerformMethod;
                    txResult.Text = m_Docs.Result;
                    txtSigners.Text = m_DocSigners.DocSigners_GetSignerName(1, DocId, ref SignerName);

                    txtOrgans.Text = m_DocOrgans.DocOrgans_GetOrganName(1, DocId,1, ref OrganName);
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
                    //    btnSave.ToolTip = "Không thể sửa thủ tục hành chính khi đã duyệt ";
                    //    m_grid_File.Columns[2].Visible = false;
                    //    gvDocStep.Columns[2].Visible = false;
                    //    isDisableStep = true;
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa thủ tục hành chính khi đã duyệt", this);
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                    //    m_grid_File.Columns[2].Visible = true;
                    //    gvDocStep.Columns[2].Visible = true;
                    //    isDisableStep = false;
                    //}
                    DocSteps m_DocSteps = new DocSteps();
                    List<DocSteps> l_DocSteps = new List<DocSteps>();
                    string DateFrom = "";
                    string DateTo = "";
                    string OrderBy = "";
                    int PageSize = 100;
                    int PageNumber = 0;
                    int RowCount = 0;
                    m_DocSteps.DocId = m_Docs.DocId;
                    l_DocSteps = m_DocSteps.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                    if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]) && l_DocSteps.Count > 0)
                    {
                        ReviewStatusId = m_Docs.ReviewStatusId;
                        btnSave.Enabled = false;
                        btnSave.ToolTip = "Không thể sửa các bươc thực hiện khi thủ tục hành chính đã duyệt ";
                        m_grid_File.Columns[2].Visible = false;
                        gvDocStep.Columns[2].Visible = false;
                        isDisableStep = true;
                        JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa các bươc thực hiện khi thủ tục hành chính đã duyệt", this);
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        m_grid_File.Columns[2].Visible = true;
                        gvDocStep.Columns[2].Visible = true;
                        isDisableStep = false;
                    }
                    bindFiles();
                    
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
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
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
                switch (SysMessageTypeId)
                {
                    case 1:
                        messages = string.Format("Cập nhật cách thức thực hiện cho văn bản không thành công : {0}", SysMessages.Static_GetDesc(SysMessageId));
                        break;
                    case 0:
                    case 2:
                        messages = "Cập nhật thức thực hiện cho văn bản thành công";
                        break;
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
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
        
        SaveData();
        bindDocSteps();
        

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
            string Mesage = "";
            int CountSuc = 0;
            int CountFail = 0;
            short SysMessageId = 0;
            Docs m_Docs = new Docs();
            DocFields m_DocFields= new DocFields();
            DocOrgans m_DocOrgans = new DocOrgans();
            DocSigners m_DocSigners = new DocSigners();
            m_Docs.DocId = DocId;
            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            if (m_Docs.DocId > 0)
            {
               
                //add docstep
                //Response.Write(gvDocStep.Rows.Count.ToString() + ":" + Request.Form["DocStepOrder"]);
                //return;
                int SysMessageIdInt = 0;
                byte StepOrder = 0;
                DocSteps m_DocSteps = new DocSteps();
                m_DocSteps.DocId = DocId;
                m_DocSteps.DeleteByDocId(ConstantHelpers.Replicated, ActUserId, ref SysMessageIdInt);
                for (int index = 0; index < gvDocStep.Rows.Count; index++)
                {
                    GridViewRow m_GridViewRow = gvDocStep.Rows[index];
                    TextBox tbStepOrder = (TextBox)m_GridViewRow.FindControl("tbStepOrder");
                    TextBox tbStepContent = (TextBox)m_GridViewRow.FindControl("tbStepContent");
                    if (tbStepOrder != null)
                    {
                        if (!String.IsNullOrEmpty(tbStepOrder.Text) && !String.IsNullOrEmpty(tbStepContent.Text))
                        {
                            if (byte.TryParse(tbStepOrder.Text, out StepOrder))
                            {
                                m_DocSteps = new DocSteps();
                                m_DocSteps.DocId = DocId;
                                m_DocSteps.StepOrder = StepOrder;
                                m_DocSteps.StepContent = tbStepContent.Text;
                                m_DocSteps.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageIdInt);
                                CountSuc++;
                            }
                            else
                            {
                                CountFail++;
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(Request.Form["DocStepOrder"]))
                {
                    string[] l_StepOrder = Request.Form["DocStepOrder"].Split(',');
                    string[] l_StepContent = Request.Form["DocStepContent"].Split(',');
                    for (int index = 0; index < l_StepOrder.Length; index++)
                    {
                        if (byte.TryParse(l_StepOrder[index], out StepOrder) && !String.IsNullOrEmpty(l_StepContent[index]))
                        {
                            m_DocSteps = new DocSteps();
                            m_DocSteps.DocId = DocId;
                            m_DocSteps.StepOrder = StepOrder;
                            m_DocSteps.StepContent = l_StepContent[index];
                            m_DocSteps.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageIdInt);
                            CountSuc++;
                        }
                        else
                        {
                            CountFail++;
                        }
                    }
                }
            }
            if(CountFail==0)
            {
                Mesage = "Lưu các bước thực hiện thành công.";
            }
            else
            {
                Mesage = "Lưu thành công " + CountSuc.ToString() + " bước thực hiện. Bỏ qua " + CountFail.ToString() + " bước thực hiện do số thứ tự không hợp lệ, hoặc nội dung trống.";
            }
            JSAlertHelpers.ShowNotify("15", "success", Mesage, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
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
            JSAlertHelpers.AlertException(ex.Message, this);
        }
    }
}