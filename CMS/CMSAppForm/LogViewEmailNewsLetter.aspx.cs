using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogViewEmailNewsLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string fileId = String.IsNullOrEmpty(Request["id"]) ? "0": Request["id"];
            string NesTypeId = String.IsNullOrEmpty(Request["NesTypeId"]) ? "0" : Request["NesTypeId"];
            string FilePath = Server.MapPath("~/Images/BanTinVanBanHangTuan.jpg");
            if(NesTypeId == "1")
            {
                FilePath = Server.MapPath("~/Images/BanTinHieuLuc.jpg");
            }
            else if (NesTypeId == "2")
            {
                FilePath = Server.MapPath("~/Images/LegalUpdate.gif");
            }
            else if (NesTypeId == "3")
            {
                FilePath = Server.MapPath("~/Images/logo.png");
            }
            FileInfo m_FileInfo = new FileInfo(FilePath);
            
            if (m_FileInfo.Exists)
            {
                if(Request.UrlReferrer != null)
                {
                    sms.utils.LogFiles.WriteLog(Request.UrlReferrer.OriginalString + Environment.NewLine + Request.UserAgent);
                }
                else
                {
                    sms.utils.LogFiles.WriteLog(Request.UserAgent);
                }
                //log download
                short SysMessageId = 0;
                if(NesTypeId != "3")
                {
                    NewsletterReadLogs m_NewsletterReadLogs = new NewsletterReadLogs();
                    m_NewsletterReadLogs.CrDateTime = DateTime.Now;
                    m_NewsletterReadLogs.Email = NesTypeId;
                    m_NewsletterReadLogs.NewsletterSendId = int.Parse(fileId);
                    m_NewsletterReadLogs.Title = m_FileInfo.Name;
                    m_NewsletterReadLogs.Insert(0, 1, ref SysMessageId);
                    //================
                }
                else
                {
                    int MessageSendId = int.Parse(Request["id"]);
                    int CampaignId = int.Parse(Request["CampaignId"]);
                    MessageSends m_MessageSends = new MessageSends();
                    m_MessageSends.MessageSendId = MessageSendId;
                    m_MessageSends.CampaignId = CampaignId;
                    m_MessageSends.SendTime = DateTime.Now;
                    m_MessageSends.MessageSends_UpdateOpenMailTime_Quick();
                }
                string FileName = m_FileInfo.Name;
                byte[] Content = File.ReadAllBytes(FilePath);
                //Response.AppendHeader("content-disposition", string.Format("inline;FileName=\"{0}\"", FileName));
                if (NesTypeId == "2")
                {
                    Response.ContentType = System.Net.Mime.MediaTypeNames.Image.Gif;
                }
                else if (NesTypeId == "3")
                {
                    Response.ContentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                }
                else
                {
                    Response.ContentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                }
                
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