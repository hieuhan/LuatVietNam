using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_FaqsEdit : System.Web.UI.Page
{
    private int FaqId = 0;
    private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["FaqId"] != null && Request.Params["FaqId"].ToString() != "")
        {
            FaqId = int.Parse(Request.Params["FaqId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLFaqGroups_BindByCulture(ddlFaqGroups, FaqGroups.Static_GetList(), "");
            DropDownListHelpers.DDLLawers_BindByCulture(ddlLawers, Lawers.Static_GetList(), "...");
            DropDownListHelpers.DDLFaqTypes_BindByCulture(ddlFaqTypes, FaqTypes.Static_GetList(), "");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, ICSoft.CMSLib.DataSources.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void bindFiles()
    {
        FaqFiles m_FaqFiles = new FaqFiles();
        List<FaqFiles> l_FaqFiles = new List<FaqFiles>();
        try
        {
            if (Request.Params["FaqId"] != null && Request.Params["FaqId"].ToString() != "")
            {
                FaqId = int.Parse(Request.Params["FaqId"].ToString());
            }
            l_FaqFiles = m_FaqFiles.FaqFiles_GetList(ActUserId, "FileName", FaqId);
            m_grid_File.DataSource = l_FaqFiles;
            m_grid_File.DataBind();
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            throw ex;
        }
    }
    private void BindData()
    {
        try
        {
            if (FaqId > 0)
            {
                Faqs m_Faqs = new Faqs();
                m_Faqs = m_Faqs.Get(FaqId);
                if (m_Faqs.FaqId > 0)
                {
                    txtTitle.Text = m_Faqs.Question;
                    txtMsgContent.Text = m_Faqs.Answer.ToString();
                    txtFaqCode.Text = m_Faqs.FaqCode.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlLanguage, m_Faqs.LanguageId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlFaqGroups, m_Faqs.FaqGroupId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlFaqTypes, m_Faqs.FaqTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlFields, m_Faqs.FieldId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlLawers, m_Faqs.LawerId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDataSources, m_Faqs.DataSourceId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Faqs.ReviewStatusId.ToString());
                    bindFiles();
                }
                else
                {
                    txtTitle.Text = "";
                    txtMsgContent.Text = "";
                    txtFaqCode.Text = "";
                    ddlLanguage.SelectedIndex = -1;
                    ddlFaqGroups.SelectedIndex = -1;
                    ddlFaqTypes.SelectedIndex = -1;
                    ddlFields.SelectedIndex = -1;
                    ddlLawers.SelectedIndex = -1;
                    ddlDataSources.SelectedIndex = -1;
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

    protected void m_grid_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        FaqFiles m_FaqFiles = new FaqFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            if (delId > 0)
            {
                m_FaqFiles.FaqFileId = delId;
                m_FaqFiles = m_FaqFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_FaqFiles.FilePath.ToString().Replace("/", "\\");
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                SysMessageTypeId=m_FaqFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                bindFiles();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            Faqs m_Faqs = new Faqs();
            m_Faqs.FaqId = FaqId;
            m_Faqs.Question = txtTitle.Text.ToString();
            m_Faqs.Answer = txtMsgContent.Text.ToString();
            m_Faqs.FaqCode = txtFaqCode.Text;
            m_Faqs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Faqs.FaqTypeId = byte.Parse(ddlFaqTypes.SelectedValue);
            m_Faqs.FaqGroupId = byte.Parse(ddlFaqGroups.SelectedValue);
            m_Faqs.FieldId = short.Parse(ddlFields.SelectedValue);
            m_Faqs.LawerId = int.Parse(ddlLawers.SelectedValue);
            m_Faqs.DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            m_Faqs.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Faqs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (m_Faqs.FaqId > 0)
            {
                HttpFileCollection files = Request.Files;
                string VirtualPath = ConfigurationManager.AppSettings["DirectoryPath"] + "FAQS/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string fileName = "";
                string DirectoryPath = "";
                string FilePath = "";
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    if (file.ContentLength > 0)
                    {
                        FaqFiles m_FaqFiles = new FaqFiles();
                        fileName = file.FileName; //.Substring(file.FileName.LastIndexOf("\\"));
                        if (file.FileName.LastIndexOf("\\") > 0) fileName = file.FileName.Substring(fileName.LastIndexOf("\\") + 1);

                        m_FaqFiles.FileName = fileName;
                        fileName = fileName.Replace(" ", "_").Replace("#", "").Replace("$", "").Replace("@", "");
                        fileName = fileName.Insert(fileName.LastIndexOf("."), "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

                        m_FaqFiles.FaqId = m_Faqs.FaqId;
                        m_FaqFiles.FilePath = VirtualPath + fileName;
                        m_FaqFiles.FileSize = file.ContentLength;
                        m_FaqFiles.CrUserId = ActUserId;
                        m_FaqFiles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                        DirectoryPath = Request.PhysicalApplicationPath + VirtualPath.Replace("/", "\\");
                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }

                        FilePath = DirectoryPath + fileName;
                        file.SaveAs(FilePath);
                    }
                }
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
}