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

public partial class Admin_NewslettersEdit : System.Web.UI.Page
{
    private int NewsletterId = 0;
   // private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["NewsletterId"] != null && Request.Params["NewsletterId"].ToString() != "")
        {
            NewsletterId = short.Parse(Request.Params["NewsletterId"].ToString());
        }
        if (!IsPostBack)
        {
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, m_NewsletterGroups.NewsletterGroups_GetList(ActUserId,"","",0), "");
            DropDownListHelpers.DDLNewsletterStatus_BindByCulture(ddlNewsletterStatus, NewsletterStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void bindFiles()
    {
        NewsletterFiles m_NewsletterFiles = new NewsletterFiles();
        List<NewsletterFiles> l_NewsletterFiles = new List<NewsletterFiles>();
        try
        {
            if (Request.Params["NewsletterId"] != null && Request.Params["NewsletterId"].ToString() != "")
            {
                NewsletterId = short.Parse(Request.Params["NewsletterId"].ToString());
            }
            l_NewsletterFiles = m_NewsletterFiles.NewsletterFiles_GetList(ActUserId, "FileName", NewsletterId);
            m_grid_File.DataSource = l_NewsletterFiles;
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
            if (NewsletterId > 0)
            {
                Newsletters m_Newsletters = new Newsletters();
                m_Newsletters = m_Newsletters.Get(NewsletterId);
                if (m_Newsletters.NewsletterId > 0)
                {
                    txtSendFrom.Text = m_Newsletters.SendFrom;
                    txtTitle.Text = m_Newsletters.Title.ToString();
                    txtMsgContent.Text = m_Newsletters.MsgContent.ToString();
                    txtScheduleSendTime.Text = m_Newsletters.ScheduleSendTime.ToString("dd/MM/yyyy HH:mm");
                    DropDownListHelpers.DDL_SetSelected(ddlNewsletterStatus, m_Newsletters.NewsletterStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlNewsletterGroups, m_Newsletters.NewsletterGroupId.ToString());
                    bindFiles();
                }
                else
                {
                    txtSendFrom.Text = "";
                    txtTitle.Text = "";
                    txtMsgContent.Text = "";
                    txtScheduleSendTime.Text = "";
                    ddlNewsletterGroups.SelectedIndex = -1;
                    ddlNewsletterStatus.SelectedIndex = -1;                    
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
        NewsletterFiles m_NewsletterFiles = new NewsletterFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            if (delId > 0)
            {
                m_NewsletterFiles.NewsletterFileId = delId;
                m_NewsletterFiles = m_NewsletterFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_NewsletterFiles.FilePath.ToString().Replace("/", "\\");
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                SysMessageTypeId=m_NewsletterFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
            Newsletters m_Newsletters = new Newsletters();
            m_Newsletters.NewsletterId = NewsletterId;
            m_Newsletters.SendFrom = txtSendFrom.Text.ToString();
            m_Newsletters.Title = txtTitle.Text.ToString();
            m_Newsletters.MsgContent = txtMsgContent.Text;
            if (txtScheduleSendTime.Text.Trim().Length > 0)
                m_Newsletters.ScheduleSendTime = sms.utils.StringUtil.ConvertToDateTime(txtScheduleSendTime.Text);
            else
                m_Newsletters.ScheduleSendTime = DateTime.Now;
            m_Newsletters.NewsletterStatusId = byte.Parse(ddlNewsletterStatus.SelectedValue);
            m_Newsletters.NewsletterGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            m_Newsletters.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (m_Newsletters.NewsletterId > 0)
            {
                HttpFileCollection files = Request.Files;
                string VirtualPath = ConfigurationManager.AppSettings["DirectoryPath"] + "newsletters/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string fileName = "";
                string DirectoryPath = "";
                string FilePath = "";
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    if (file.ContentLength > 0)
                    {
                        NewsletterFiles m_NewsletterFiles = new NewsletterFiles();
                        fileName = file.FileName; //.Substring(file.FileName.LastIndexOf("\\"));
                        if (file.FileName.LastIndexOf("\\") > 0) fileName = file.FileName.Substring(fileName.LastIndexOf("\\") + 1);

                        m_NewsletterFiles.FileName = fileName;
                        fileName = fileName.Replace(" ", "_").Replace("#", "").Replace("$", "").Replace("@", "");
                        fileName = fileName.Insert(fileName.LastIndexOf("."), "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

                        m_NewsletterFiles.NewsletterId = m_Newsletters.NewsletterId;
                        m_NewsletterFiles.FilePath = VirtualPath + fileName;
                        m_NewsletterFiles.FileSize = file.ContentLength;
                        m_NewsletterFiles.CrUserId = ActUserId;
                        m_NewsletterFiles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

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
            if (cblAddOther.Checked)
            {
                if (NewsletterId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật bản tin thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới bản tin thành công.", this); }
                clearForm();
                return;
            }
            if (NewsletterId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật bản tin thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới bản tin thành công.", this); }

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
    private void clearForm()
    {
        txtMsgContent.Text = "";
        txtScheduleSendTime.Text = "";
        txtSendFrom.Text = "";
        txtTitle.Text = "";
        ddlNewsletterGroups.SelectedIndex = -1;
        ddlNewsletterStatus.SelectedIndex = -1;
    }
}