using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string FilePath = Request["file"];
            string FileName = Request["filename"];
            string RequestFilePath = Request["file"];
            int DocId = Request["DocId"] == null ? 0 : int.Parse(Request["DocId"]);
            string DocHexId = Request["DocHexId"] == null ? "" : Request["DocHexId"];
            if (DocId == 0 && !String.IsNullOrEmpty(DocHexId))
            {
                DocId = int.Parse(DocHexId, System.Globalization.NumberStyles.HexNumber);
            }
            FilePath = Server.MapPath(FilePath);
            
            FileInfo m_FileInfo = new FileInfo(FilePath);
            if (!m_FileInfo.Exists)
            {
                FilePath = WebConstans.DATA_PATH + RequestFilePath;
                m_FileInfo = new FileInfo(FilePath);
            }
            if (!m_FileInfo.Exists)
            {
                FilePath = FilePath.Replace("LawData", "LawDataEN");
                m_FileInfo = new FileInfo(FilePath);
            }
            if (m_FileInfo.Exists)
            {
                //log download
                short SysMessageId = 0;
                DocViewLogs m_DocViewLogs = new DocViewLogs();
                m_DocViewLogs.ActionTypeId = 7;
                m_DocViewLogs.ApplicationTypeId = 1;
                m_DocViewLogs.CustomerId = 0;
                m_DocViewLogs.DocFileId = 0;
                m_DocViewLogs.DocGroupId = 1;
                m_DocViewLogs.DocId = DocId;
                m_DocViewLogs.DocName = "";
                m_DocViewLogs.FromIP = Request.UserHostAddress;
                m_DocViewLogs.LanguageId = 1;
                m_DocViewLogs.RefererFrom = Request.UrlReferrer == null ? "" : Request.UrlReferrer.AbsolutePath;
                m_DocViewLogs.UserAgent = Request.UserAgent;
                m_DocViewLogs.Insert(0, 0, ref SysMessageId);
                //================
                FileName = String.IsNullOrEmpty(FileName) ? m_FileInfo.Name : FileName + m_FileInfo.Extension;
                byte[] Content = File.ReadAllBytes(FilePath);
                Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BufferOutput = true;
                Response.OutputStream.Write(Content, 0, Content.Length);
                Response.End();
            }
            else
            {
                Response.Write("Rất tiếc, file bạn yêu cầu (" + Request["file"] + ") không tồn tại!");
            }
        }
        catch(Exception ex)
        {
            if (!ex.Message.Contains("Thread was being aborted."))
            {
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            Response.Write("Rất tiếc, file bạn yêu cầu (" + Request["file"] + ") không tồn tại!");
        }
    }
}