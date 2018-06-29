using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;

public partial class Admin_DocsFielsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    private int ActUserId = 0;
    protected int WordFileId = 0, PdfFileId = 0, WordFileIdEn = 0, PdfFileIdEn = 0;
    private byte RolesIDHavePermission = 10;//Roles: Docs admin
    protected List<DataSources> l_DataSources = new List<DataSources>();

    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    private bool isrunning = false;
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
        if (!IsPostBack)
        {
            

            bindFiles();
        }
    }

   private void bindFiles()
    {
        DocFiles m_DocFiles = new DocFiles();
        DataSources m_DataSources = new DataSources();
        List<DocFiles> l_DocFiles = new List<DocFiles>();
        l_DataSources = m_DataSources.GetList();
        l_ReviewStatus = ReviewStatus.Static_GetList();
        m_DocFiles.LanguageId = LanguageId;
        try
        {
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Get(DocId, LanguageId);
                if (m_Docs.ReviewStatusId == 2)
                {
                    m_grid_File.Columns[6].Visible = false;
                }
                else
                {
                    m_grid_File.Columns[6].Visible = true;
                }
            }
            l_DocFiles = m_DocFiles.GetAllByDocId(DocId);
            m_grid_File.DataSource = l_DocFiles;
            m_grid_File.DataBind();
            lblTong.Text = l_DocFiles.Count.ToString();
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
        string deleteMessage = string.Empty;
        DocFiles m_DocFiles = new DocFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            if (delId > 0)
            {
                Label lblDocFileName = (Label)m_grid_File.Rows[e.RowIndex].FindControl("lblDocFileName");
                m_DocFiles.LanguageId = LanguageId;
                m_DocFiles.DocFileId = delId;
                m_DocFiles = m_DocFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_DocFiles.FilePath.ToString().Replace("/", "\\");
                SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                switch (SysMessageTypeId)
                {
                    case 1:
                        deleteMessage = string.Format("Xóa file : {0} không thành công.", !string.IsNullOrEmpty(lblDocFileName.Text) ? lblDocFileName.Text : "");
                        break;
                    case 0:
                    case 2:
                        deleteMessage = string.Format("Xóa file <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblDocFileName.Text) ? lblDocFileName.Text : "");
                        break;
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
                bindFiles();

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

   protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!isrunning)
        {
            isrunning = true;
            HttpPostedFile file = FileUpload1.PostedFile;
            string VirtualPath = ConfigurationManager.AppSettings["DirectoryPath"] + "VIETLAWFILE/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            string messages = string.Empty;
            string DirectoryPath = "";
            string FilePath = "";
            if (file.ContentLength > 0)
            {
                DocFiles m_DocFiles = new DocFiles();
                string fileName = file.FileName; //.Substring(file.FileName.LastIndexOf("\\"));
                if (file.FileName.LastIndexOf("\\") > 0) fileName = file.FileName.Substring(fileName.LastIndexOf("\\") + 1);

                //m_DocFiles.FileName = fileName;
                fileName = fileName.Replace(" ", "_").Replace("#", "").Replace("$", "").Replace("@", "");
                fileName = fileName.Insert(fileName.LastIndexOf("."), "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

                m_DocFiles.DocId = DocId;
                m_DocFiles.DocFileName = String.IsNullOrEmpty(TextBox1.Text) ? fileName : TextBox1.Text;
                if(LanguageId == 2)
                { m_DocFiles.FileTypeId = fileName.Contains(".pdf") ? (byte)2 : (byte)3; }
                else m_DocFiles.FileTypeId = 6;
                m_DocFiles.DataSourceId = short.Parse(DropDownList1.SelectedValue);
                m_DocFiles.LanguageId = LanguageId;
                m_DocFiles.FilePath = VirtualPath + fileName;
                m_DocFiles.FileSize = file.ContentLength;
                m_DocFiles.ReviewStatusId = 1;//cho duyet
                SysMessageTypeId = m_DocFiles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                
                DirectoryPath = Request.PhysicalApplicationPath + VirtualPath.Replace("/", "\\");
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                FilePath = DirectoryPath + fileName;
                file.SaveAs(FilePath);
                //Put nofity sync file

                if (isUpdateSynStatusId && SysMessageTypeId != 1)
                {
                    //m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 0);
                    //DocFiles.PutNofitySync(m_DocFiles.DocFileId);
                }
                else
                {
                    //m_DocFiles.UpdateSyncStatus(m_DocFiles.DocFileId, 1);
                }
                switch (SysMessageTypeId)
                {
                    case 1:
                        messages = string.Format("Lỗi khi lưu file : {0}", SysMessages.Static_GetDesc(SysMessageId));
                        break;
                    case 0:
                    case 2:
                        messages = string.Format("Lưu file <i>{0}</i> thành công.", m_DocFiles.DocFileName);
                        TextBox1.Text = "";
                        DropDownList1.SelectedIndex = 0;
                        break;
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
                bindFiles();
            }
            else JSAlertHelpers.ShowNotify("15", "warning", "Bạn chưa chọn file cần upload.", this);
            isrunning = false;
        }
    }
   private void Review_Click(byte ReviewStatusId)
   {
       byte SysMessageTypeId = 0;
       short SysMessageId = 0;
       int CountSuc = 0;
       string Message = "";
       try
       {
           DocFiles m_DocFiles = new DocFiles();
           foreach (GridViewRow m_Row in m_grid_File.Rows)
           {
               CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
               if (chkAction != null)
               {
                   if (chkAction.Checked)
                   {
                       m_DocFiles.DocFileId = int.Parse(m_grid_File.DataKeys[m_Row.RowIndex].Value.ToString());
                       m_DocFiles.DocId = m_DocFiles.DocFileId;
                       m_DocFiles.ReviewStatusId = ReviewStatusId;
                       SysMessageTypeId = m_DocFiles.UpdateReviewStatusId(ActUserId, ref SysMessageId);
                       CountSuc++;
                   }
               }
           }
       }
       catch (Exception ex)
       {
           sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
           JSAlertHelpers.Alert(ex.Message, this);
       }
       bindFiles();
       Message = "Duyệt thành công " + CountSuc.ToString() + " file. ";
       if (ReviewStatusId != 2)
           Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " file. ";
       //lblMsg.Text = Message;
       JSAlertHelpers.ShowNotify("15", "success", Message, this);
   }
   protected void lbCheckfile_Click(object sender, EventArgs e)
   {
       if (!UserHelpers.isInRole(ActUserId, RolesIDHavePermission))
       {
           JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa được cấp quyền duyệt file", this);
           bindFiles();
           return;
       }
       Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
   }
   protected void lbUnCheck_Click(object sender, EventArgs e)
   {
       if (!UserHelpers.isInRole(ActUserId, RolesIDHavePermission))
       {
           JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa được cấp quyền duyệt file", this);
           bindFiles();
           return;
       }
       Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
   }
   protected void lbDelete_Click(object sender, EventArgs e)
   {
       byte SysMessageTypeId = 0;
       short SysMessageId = 0;
       int CountSuc = 0;
       int CountFalse = 0;
       try
       {
           DocFiles m_DocFiles = new DocFiles();
           foreach (GridViewRow m_Row in m_grid_File.Rows)
           {
               CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
               if (chkAction != null)
               {
                   if (chkAction.Checked)
                   {
                       m_DocFiles.LanguageId = LanguageId;
                       m_DocFiles.DocFileId = int.Parse(m_grid_File.DataKeys[m_Row.RowIndex].Value.ToString());
                       m_DocFiles = m_DocFiles.Get();
                       if (m_DocFiles.ReviewStatusId == 2 && !UserHelpers.isInRole(ActUserId, RolesIDHavePermission))
                       {
                           CountFalse++;
                           continue;
                       }
                       SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                       CountSuc++;
                   }
               }
           }
       }
       catch (Exception ex)
       {
           sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
           JSAlertHelpers.Alert(ex.Message, this);
       }
       bindFiles();
       string Message = "Xóa thành công " + CountSuc.ToString() + " file. ";
       if (CountFalse > 0)
       {
           Message += "Xóa thất bại " + CountFalse.ToString() + " file do bạn chưa được cấp quyền xóa file ở trạng thái đã duyệt. ";
       }
       JSAlertHelpers.ShowNotify("15", "success", Message, this);
   }
}