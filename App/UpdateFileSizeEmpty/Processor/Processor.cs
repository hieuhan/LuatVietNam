using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using SMSSystem.CommonLibrary;
using System.IO;
using System.IO.Compression;
using log4net;
using ICSoft.ExtractWordLib;
using ICSoft.LawDocsLib;
using Ionic.Zip;
using ICSoft.CMSViewLib;

namespace Processor
{
    public partial class Processor : Form
    {
        //cau hinh processor
        private bool m_isRunning = false;
        private int m_ThreadRunning = 0;
        private byte m_TotalThread = 1;
        private string m_ThreadName = "PhapDien_";
        private int SleepTime;
        private int SleepTimePerFile;      
        public static readonly ILog _logger = LogManager.GetLogger(typeof(Processor).Name);
        private string Connectstr = "";
        public enum FileFolderStatus
        {
            NewInsert = 1, Processing = 2, Updating = 3, Complete = 4
        };
        public enum LegislatedStatus
        {
            Complete = 1, Rebuld = 2, Rebulding = 3
        };
        public Processor()
        {
            InitializeComponent();
            this.Text = (ConfigurationManager.AppSettings["FormTitle"] == null) ? "Process service" : ConfigurationManager.AppSettings["FormTitle"];
            if (ConfigurationManager.AppSettings["ShowSendMessageTab"] == "0") tabControl_Processor.TabPages.Remove(tabControl_Processor.TabPages["tab_SendMessage"]);
            if (ConfigurationManager.AppSettings["ShowSendAlertTab"] == "0") tabControl_Processor.TabPages.Remove(tabControl_Processor.TabPages["tab_SendAlert"]);
            SleepTime = String.IsNullOrEmpty(ConfigurationManager.AppSettings["SleepTime"]) ? 20000 : int.Parse(ConfigurationManager.AppSettings["SleepTime"]);
            Connectstr = ConfigurationManager.AppSettings["CONNECTION_STRING"];
            SleepTimePerFile = String.IsNullOrEmpty(ConfigurationManager.AppSettings["SleepTimePerFile"]) ? 10 : int.Parse(ConfigurationManager.AppSettings["SleepTimePerFile"]);
            txtRootFolder.Text = ConfigurationManager.AppSettings["ROOTFOLDER"];
            txtOutputFolder.Text = ConfigurationManager.AppSettings["CompleteOutputFolder"];
        }
        private void btnStart_Click(object sender, EventArgs e)//on start
        {
            if (m_ThreadRunning > 0)
            {
                LogControl.WriteMessage(txtStatus, "Thread is running...");
                _logger.Info("Thread is running...");
                return;
            }
            LoadSetting();//load setting
            m_ThreadRunning = 0;
            m_isRunning = true;
            for (int i = 0; i < m_TotalThread; i++)
            {
                Thread myThread = new Thread(new ThreadStart(processService));
                myThread.Name = m_ThreadName + i.ToString();
                myThread.IsBackground = true;
                myThread.Start();
            }
            btnStart.Visible = false;
            btnStop.Visible = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            m_isRunning = false;
            btnStart.Visible = true;
            btnStop.Visible = false;
            LogControl.WriteMessage(txtStatus, "Stopping thread...");
            _logger.Info("Stopping thread...");
        }
        private void LoadSetting()
        {
            
        }
        private void processService()//xử lý
        {
            LogControl.WriteMessage(txtStatus, "Start thread " + Thread.CurrentThread.Name);
            _logger.Info("Start thread " + Thread.CurrentThread.Name);
            m_ThreadRunning++;
            while (m_isRunning)
            {
                try
                {

                    ProcessFolder();
                    //Thread.Sleep(SleepTime);
                    //ProcessDB();

                }
                catch (Exception ex)
                {
                    LogControl.WriteMessage(txtStatus, ex.ToString());
                    
                    _logger.Error("Error: " + ex);
                }            
               
                Thread.Sleep(SleepTime);
            }
            m_ThreadRunning--;
            LogControl.WriteMessage(txtStatus, "End thread " + Thread.CurrentThread.Name);
            _logger.Info("End thread " + Thread.CurrentThread.Name);
        }
       
       private void ProcessFolder()
       {
           LogControl.WriteMessage(txtLog, "");
           DocFiles mDocFiles = new DocFiles();
           List<DocFiles> ListDocFile = new List<DocFiles>();
           ListDocFile = mDocFiles.GetListSizeEmpty(200);
           if(ListDocFile.Count>0)
           {
               foreach(DocFiles m_docfile in ListDocFile)
               {
                   UpdateFileSize(m_docfile);
               }
           }

       }
       private void UpdateFileSize(DocFiles m_docfile)
       {
           int filesize = 0;
           short message = 0;
           string path = txtRootFolder.Text;
           try
           {
               path += m_docfile.FilePath;
               path = path.Replace("/", "\\");
               FileInfo m_FileInfo = new FileInfo(path);
                if (m_FileInfo.Exists)
                {
                    WriteStatus(ProcessStatus.Processing, path);
                    LogControl.WriteMessage(txtLog, "Execute File: " + path);
                    LogControl.WriteMessage(txtLog, "FileSize: " + m_FileInfo.Length);
                    filesize = (int)m_FileInfo.Length;
                    m_docfile.DocFileName = m_FileInfo.Name.Replace(m_FileInfo.Extension, "");
                    m_docfile.FileSize = filesize;
                    m_docfile.Update(0, 0, ref message);
                    LogControl.WriteMessage(txtLog, "message Return: " + message);
                }
                else
                {
                    LogControl.WriteMessage(txtLog, "Execute File: " + path + " Not Exists");
                }
               
           }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           }
          

       }
        private void ProcessDB()
        {
            LogControl.WriteMessage(txtLog, "");
            int PageSize = 1;
            int RowCount = 0;
            DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
            m_DocsProcessAuto.ProcessStatusId = 1;
            List<DocsProcessAuto> l_DocsProcessAuto = m_DocsProcessAuto.GetPage("", "", "", PageSize, 0, ref RowCount);
            foreach(DocsProcessAuto m_DocsProcessAutoTemp in l_DocsProcessAuto)
            {
                ProcessFile(m_DocsProcessAutoTemp.DocFilePath, m_DocsProcessAutoTemp);
            }
            
        }
        private void ProcessFileInput(string FilePath)
        {
            try
            {

                if (chkShowLog.Checked)
                    LogControl.WriteMessage(txtLog, "start process: " + FilePath);
                int SysMessageId = 0;
                FileInfo m_FileInfo = new FileInfo(FilePath);
                WriteStatus(ProcessStatus.Processing, FilePath);
                //if (FilePath.EndsWith(".zip"))
                //{

                    LogControl.WriteMessage(txtLog, "Execute File: " + m_FileInfo.Name);
                    LogControl.WriteMessage(txtLog, "FileSize: " + m_FileInfo.Length);


                    //ZipFile zipFile = new ZipFile(FilePath);
                    //zipFile.ExtractAll(txtRootFolder.Text);
                    //zipFile.Dispose();
                    //string MoveFilePath = ConstantProcessors.CompleteOutputFolder + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "\\";
                    //if (Directory.Exists(MoveFilePath) == false)
                    //    Directory.CreateDirectory(MoveFilePath);
                    //MoveFilePath += m_FileInfo.Name;
                    //if (File.Exists(MoveFilePath))
                    //{
                    //    File.Delete(MoveFilePath);
                    //}
                    //File.Move(FilePath, MoveFilePath);
                    //File.Delete(FilePath);
                //}
                //else
                //{
                //    string MoveFilePath = ConstantProcessors.CompleteOutputFolder + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "\\";
                //    if (Directory.Exists(MoveFilePath) == false)
                //        Directory.CreateDirectory(MoveFilePath);
                //    MoveFilePath += m_FileInfo.Name;
                //    DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
                //    m_DocsProcessAuto.DocId = 0;
                //    m_DocsProcessAuto.DocName = "";
                //    m_DocsProcessAuto.DocIdentity = "";
                //    m_DocsProcessAuto.DocFilePath = MoveFilePath;
                //    m_DocsProcessAuto.ProcessStatusId = 1;
                //    m_DocsProcessAuto.InsertOrUpdate(0, 1, ref SysMessageId);
                //    //move to sucess folder
                //    if (File.Exists(MoveFilePath))
                //    {
                //        File.Delete(MoveFilePath);
                //    }
                //    File.Move(FilePath, MoveFilePath);
                //}
                //=======================================
                //WriteStatus(ProcessStatus.Sleeping, "");
                //===================
                if (chkShowLog.Checked)
                    LogControl.WriteMessage(txtLog, "end process: " + FilePath);

            }
            catch (Exception ex)
            {
                LogControl.WriteMessage(txtStatus, ex.ToString());

                _logger.Error("Error: " + ex);
            }

        }
        private void ProcessFile(string FilePath, DocsProcessAuto m_DocsProcessAuto)
       {
            FileInfo m_FileInfo = new FileInfo(FilePath);
            
           try
           {

               if (chkShowLog.Checked)
                   LogControl.WriteMessage(txtLog, "start process: " + FilePath);
                if (!File.Exists(FilePath))
                {
                    if (chkShowLog.Checked)
                        LogControl.WriteMessage(txtLog, "file not found: " + FilePath);
                    return;
                }
               WriteStatus(ProcessStatus.Processing, FilePath);
               string FileContent, Error;
               string OutputPath = "";
               string OutputHtmlFile = "", OutputPdfFile = "", OutputOriginalFile = "";
                string OutputHtmlAbsoluteFile = "", OutputPdfAbsoluteFile = "", OutputOriginalAbsoluteFile = "";
                string CurentFileName, CurentFileNameNoExt;
               
               CurentFileName = m_FileInfo.Name;
               CurentFileNameNoExt = CurentFileName.Replace(m_FileInfo.Extension, "");
               FileContent = WordReaders.ReadFileContentPlanText(FilePath, out Error);
               WordDocuments m_WordDocuments = new WordDocuments();
               m_WordDocuments.ReadContent(FileContent, out Error);
               if (m_WordDocuments.PublicDate == DateTime.MinValue)
               {
                   OutputPath = "\\" + ConstantProcessors.DefaultOutputFolder + "\\";

               }
               else
               {
                   OutputPath = "\\" + m_WordDocuments.PublicDate.ToString("yyyy") + "\\" + m_WordDocuments.PublicDate.ToString("MM") + "\\";

               }
                OutputOriginalAbsoluteFile = "VIETLAWFILE" + OutputPath.Replace("\\","/") + m_FileInfo.Name;
                OutputHtmlAbsoluteFile = "VIETLAWFILE" + OutputPath.Replace("\\", "/") + CurentFileNameNoExt + ".html";
                OutputPdfAbsoluteFile = "VIETLAWFILE" + OutputPath.Replace("\\", "/") + CurentFileNameNoExt + ".pdf";
                OutputPath = txtOutputFolder.Text + OutputPath;
               OutputOriginalFile = OutputPath + m_FileInfo.Name;
               OutputHtmlFile = OutputPath + CurentFileNameNoExt + ".html";
               OutputPdfFile = OutputPath + CurentFileNameNoExt + ".pdf";
               
               if (Directory.Exists(OutputPath) == false)
               {
                   Directory.CreateDirectory(OutputPath);
               }
                // save
                Docs m_Docs = new Docs();
                List<WordBookmarks> l_WordBookmarks = new List<WordBookmarks>();
                if (m_DocsProcessAuto.DocId == 0)
                    SaveToDb(m_WordDocuments, l_WordBookmarks, CurentFileNameNoExt, ref m_Docs);
                else
                {
                    m_Docs.DocId = m_DocsProcessAuto.DocId;
                    m_Docs = m_Docs.Get();
                }
                // save to html
                if(m_Docs.DocId > 0)
                {
                    string htmlContent = "";
                    short SysMessageId = 0;
                    l_WordBookmarks = WordReaders.ReadAsHtml(FilePath, m_Docs.DocId, out Error, out htmlContent, OutputHtmlFile, FileContent);
                    if (l_WordBookmarks.Count > 0)
                    {
                        DocIndexes m_DocIndexes = new DocIndexes();
                        m_DocIndexes.DocId = m_Docs.DocId;
                        m_DocIndexes.Delete_ByDocId(1, 1, ref SysMessageId);
                        foreach (WordBookmarks m_WordBookmarks in l_WordBookmarks)
                        {
                           m_DocIndexes = new DocIndexes();
                            m_DocIndexes.DocId = m_Docs.DocId;
                            m_DocIndexes.Bookmark = m_WordBookmarks.BookmarkName;
                            m_DocIndexes.DisplayOrder = m_WordBookmarks.BookmarkLevel;
                            m_DocIndexes.LevelId = 1;
                            m_DocIndexes.Title = m_WordBookmarks.BookmarkDesc;
                            m_DocIndexes.Insert(0, 1, ref SysMessageId);
                        }
                    }
                    // process docfiles
                    DocFiles m_DocFiles = new DocFiles();
                    
                    m_DocFiles.DocId = m_Docs.DocId;
                    m_DocFiles.DocFileName = CurentFileNameNoExt;
                    m_DocFiles.FileTypeId = 1;//van ban goc
                    m_DocFiles.DataSourceId = 7;//incom
                    m_DocFiles.LanguageId = m_Docs.LanguageId == 0 ? (byte)1 : m_Docs.LanguageId;
                    m_DocFiles.FilePath = OutputOriginalAbsoluteFile;
                    m_DocFiles.FileSize = (int)(m_FileInfo.Length/1024);
                    m_DocFiles.ReviewStatusId = 2;
                    m_DocFiles.InsertOrUpdate(1, 1, ref SysMessageId);
                    //end file
                    if (!String.IsNullOrEmpty(htmlContent))
                    {
                        //process relate
                        // insert referent
                        string processNo = ";";
                        foreach (string RelateNo in m_WordDocuments.l_DocNoRelated)
                        {
                            if (processNo.Contains(";" + RelateNo + ";"))
                                continue;
                            List<Docs> l_Docs = m_Docs.Docs_GetListByDocIdentity(1, 10, "", m_WordDocuments.LanguageId, RelateNo, 0);
                            if (l_Docs.Count == 1)
                            {
                                Docs m_DocsTemp = l_Docs[0];
                                string link = DocsView.GetDocUrl(m_DocsTemp.DocId, m_DocsTemp.DocName);
                                string ReferHtml = "<a class='docnorefer' href='" + link + "'>" + RelateNo + "</a>";
                                htmlContent = htmlContent.Replace(RelateNo, ReferHtml);
                                //insert to relate
                                DocRelates m_DocRelates = new DocRelates();
                                m_DocRelates.DocId = m_Docs.DocId;
                                m_DocRelates.CrUserId = 1;
                                m_DocRelates.DocReferenceId = m_DocsTemp.DocId;
                                m_DocRelates.RelateTypeId = ConstantProcessors.RelateTypeId;
                                m_DocRelates.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                                m_DocRelates.Insert(ConstantProcessors.Replicated, 1, ref SysMessageId);
                                //end
                            }
                            else
                            {
                                string link = ConstantProcessors.DocSearchLink + RelateNo;
                                string ReferHtml = "<a class='docnorefer' href='" + link + "'>" + RelateNo + "</a>";
                                htmlContent = htmlContent.Replace(RelateNo, ReferHtml);
                            }
                            processNo += RelateNo + ";";
                        }
                        //end relate
                        m_Docs.DocContent = htmlContent;
                        m_Docs.UpdateContent(0, 1, ref SysMessageId);
                        LogControl.WriteMessage(txtLog, htmlContent);
                    }
                }
               
               //save to pdf
               string WatterMarkPath = ConstantProcessors.WatterMarkImage;
               string WatterMarkText = ConstantProcessors.WatterMarkText;

                //WordReaders.SaveAsPdf(FilePath, false, WatterMarkPath, WatterMarkText, OutputPdfFile);
                // save to db

                if (String.IsNullOrEmpty(m_Docs.DocName))
                    m_Docs.DocName = "";
                if (String.IsNullOrEmpty(m_Docs.DocIdentity))
                    m_Docs.DocIdentity = "";
                int SysMessgeId = 0;
                if (m_WordDocuments.PublicDate > DateTime.MinValue && String.IsNullOrEmpty(Error))
                {
                    m_DocsProcessAuto.DocId = m_Docs.DocId;
                    m_DocsProcessAuto.DocName = m_Docs.DocName;
                    m_DocsProcessAuto.DocIdentity = m_Docs.DocIdentity;
                    m_DocsProcessAuto.ProcessStatusId = 3;
                    m_DocsProcessAuto.ProcessTime = DateTime.Now;
                    m_DocsProcessAuto.Update(0, 1, ref SysMessgeId);
                }
                else
                {
                    m_DocsProcessAuto.DocId = m_Docs.DocId;
                    m_DocsProcessAuto.DocName = m_Docs.DocName;
                    m_DocsProcessAuto.DocIdentity = m_Docs.DocIdentity;
                    m_DocsProcessAuto.ProcessStatusId = 4;
                    m_DocsProcessAuto.ProcessTime = DateTime.Now;
                    m_DocsProcessAuto.Update(0, 1, ref SysMessgeId);
                    if (chkShowLog.Checked)
                        LogControl.WriteMessage(txtLog, "Error: " + Error);
                }
                //move processed file
                //string MoveFilePath = "";
                //if (m_WordDocuments.PublicDate > DateTime.MinValue && String.IsNullOrEmpty(Error))
                //{
                //    MoveFilePath = ConstantProcessors.CompleteOutputFolder + m_WordDocuments.PublicDate.ToString("yyyy") + "\\" + m_WordDocuments.PublicDate.ToString("MM") + "\\";
                //    if (Directory.Exists(MoveFilePath) == false)
                //        Directory.CreateDirectory(MoveFilePath);
                //    MoveFilePath += CurentFileName;
                //    File.Move(FilePath, MoveFilePath);
                //}
                //else
                //{
                //    MoveFilePath = ConstantProcessors.ErrorOutputFolder;
                //    MoveFilePath += CurentFileName;
                //    File.Move(FilePath, MoveFilePath);
                //    if (chkShowLog.Checked)
                //        LogControl.WriteMessage(txtLog, "Error: " + Error);
                //}
                //=======================================
                WriteStatus(ProcessStatus.Sleeping, "");
               //===================
               if (chkShowLog.Checked)
                   LogControl.WriteMessage(txtLog, "end process: " + FilePath);

           }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           }    
            finally
            {
                m_FileInfo = null;
            }       
           
       }
       private void SaveToDb(WordDocuments m_WordDocuments, List<WordBookmarks> l_WordBookmarks, string FileNameNoExt, ref Docs m_DocsOut)
       {
           try
           {
               if (String.IsNullOrEmpty(m_WordDocuments.DocNo))
                   return;
               int ActUserId = ConstantProcessors.CrUserId;
               byte ReferentToDefaultLanguage =0;
               int RowAmount =100;
               byte StatusId = 0;
               string OrderBy = "";
               short SysMessageId=0;
               Docs m_Docs = new Docs();
               DocTypes m_DocTypes = new DocTypes();
               m_DocTypes= m_DocTypes.DocTypes_GetList(ActUserId,OrderBy,m_WordDocuments.LanguageId, 0, StatusId,0, ReferentToDefaultLanguage).Find(x => x.DocTypeDesc.ToLower().Contains(m_WordDocuments.DocType.ToLower()));
               
               if(m_DocTypes == null)
                   m_DocTypes = new DocTypes();
               // check doc exists
               List<Docs> l_Docs= m_Docs.Docs_GetListByDocIdentity(ActUserId, RowAmount, OrderBy, m_WordDocuments.LanguageId, m_WordDocuments.DocNo, StatusId);
                for(int index=0; index < l_Docs.Count; index++)
                {
                    Docs m_DocsTemp = l_Docs[index];
                    if(m_DocsTemp.DocName.ToLower().Trim() == m_WordDocuments.DocName.ToLower().Trim() || m_DocsTemp.IssueDate.ToString("ddMMyyyy") == m_WordDocuments.PublicDate.ToString("ddMMyyyy"))
                    {
                        m_Docs = m_DocsTemp;
                        break;
                    }
                    else
                    {
                        LogControl.WriteMessage(txtLog, "No: " + m_WordDocuments.DocNo + " | " + m_DocsTemp.IssueDate.ToString("ddMMyyyy") + " | " + m_WordDocuments.PublicDate.ToString("ddMMyyyy"));
                    }
                }
               
               if(m_Docs.DocId == 0)
               {
                    string docName = m_WordDocuments.DocName.Trim();
                    if (docName.Length > 0)
                    {
                        docName = docName.Substring(0, 1) + docName.Substring(1).ToLower();
                    }
                    docName = docName.Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
                    while(docName.Contains("  "))
                    {
                        docName = docName.Replace("  ", " ");
                    }
                    m_Docs.CrUserId = ActUserId;
                   m_Docs.DataSourceId = ConstantProcessors.DataSourceId;
                   m_Docs.DocContent = "";
                   m_Docs.DocIdentity = m_WordDocuments.DocNo;
                   m_Docs.DocIdentityClear = m_WordDocuments.DocNo;
                   m_Docs.DocName = docName;
                   m_Docs.DocSummary = "";
                   m_Docs.DocTypeId = m_DocTypes.DocTypeId;
                   m_Docs.EffectDate = DateTime.MinValue;
                   m_Docs.EffectStatusId = ConstantProcessors.EffectStatusId;
                   m_Docs.ExpireDate = DateTime.MinValue;
                   m_Docs.GazetteDate = DateTime.MinValue;
                   m_Docs.GazetteNumber = "";
                   m_Docs.IssueDate = m_WordDocuments.PublicDate;
                   m_Docs.LanguageId = m_WordDocuments.LanguageId;
                   m_Docs.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                   m_Docs.UseStatusId = ConstantProcessors.UseStatusId;
                    m_Docs.DocGroupId = 1;
                   m_Docs.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                   if (m_Docs.DocId == 0)
                       return;
                    // insert organ
                    if (String.IsNullOrEmpty(m_WordDocuments.Publisher) == false)
                    {
                        DocOrgans m_DocOrgans = new DocOrgans();
                        m_DocOrgans.DocId = m_Docs.DocId;
                        m_DocOrgans.InsertByOrganName(ConstantProcessors.Replicated, ActUserId, m_WordDocuments.Publisher);
                    }
                    // insert referent
                    foreach (string RelateNo in m_WordDocuments.l_DocNoRelated)
                    {
                        Docs m_DocsRelate = new Docs();
                        //m_DocsRelate = m_DocsRelate.Docs_GetTopByDocIdentity(ActUserId, RowAmount, OrderBy, m_WordDocuments.LanguageId, RelateNo, StatusId);
                        if (m_DocsRelate.DocId > 0)
                        {
                            DocRelates m_DocRelates = new DocRelates();
                            m_DocsRelate.DocId = m_Docs.DocId;
                            m_DocRelates.CrUserId = ActUserId;
                            m_DocRelates.DocReferenceId = m_DocsRelate.DocId;
                            m_DocRelates.RelateTypeId = ConstantProcessors.RelateTypeId;
                            m_DocRelates.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                            m_DocRelates.Insert(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                        }
                    }
                    // insert doc file
                    //string FileFolder;
                    //string FileHtmlUrl, FilePdfUrl;
                    //if (m_WordDocuments.PublicDate == DateTime.MinValue)
                    //{
                    //    FileFolder = ConstantProcessors.DefaultOutputFolder.Substring(ConstantProcessors.DefaultOutputFolder.LastIndexOf("\\")) + "/";

                    //}
                    //else
                    //{
                    //    FileFolder = m_WordDocuments.PublicDate.ToString("yyyy") + "/" + m_WordDocuments.PublicDate.ToString("MM") + "/";

                    //}
                    //FileHtmlUrl = FileFolder + FileNameNoExt + ".html";
                    //FilePdfUrl = FileFolder + FileNameNoExt + ".pdf";
                    //FileInfo m_HtmlFileInfo = new FileInfo(txtOutputFolder.Text + "\\" + FileHtmlUrl.Replace("/", "\\"));
                    //FileInfo m_PdfFileInfo = new FileInfo(txtOutputFolder.Text + "\\" + FilePdfUrl.Replace("/", "\\"));
                    //DocFiles m_DocFiles = new DocFiles();
                    //m_DocFiles.DocId = m_Docs.DocId;
                    //m_DocFiles.FilePath = FileHtmlUrl;
                    //m_DocFiles.DataSourceId = ConstantProcessors.DataSourceId;
                    //if (m_HtmlFileInfo.Length < int.MaxValue)
                    //    m_DocFiles.FileSize = int.Parse(m_HtmlFileInfo.Length.ToString());
                    //else
                    //    m_DocFiles.FileSize = 0;
                    //m_DocFiles.FileTypeId = FileTypeHelpers.Html;
                    //m_DocFiles.LanguageId = m_WordDocuments.LanguageId;
                    //m_DocFiles.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                    //m_DocFiles.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                    //// pdf
                    //m_DocFiles.DocFileId = 0;
                    //m_DocFiles.FileTypeId = FileTypeHelpers.Pdf;
                    //m_DocFiles.FilePath = FilePdfUrl;
                    //if (m_PdfFileInfo.Length < int.MaxValue)
                    //    m_DocFiles.FileSize = int.Parse(m_PdfFileInfo.Length.ToString());
                    //else
                    //    m_DocFiles.FileSize = 0;
                    //m_DocFiles.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                    
                }
                else
                {
                    // insert referent
                    foreach (string RelateNo in m_WordDocuments.l_DocNoRelated)
                    {
                        Docs m_DocsRelate = new Docs();
                        //m_DocsRelate = m_DocsRelate.Docs_GetTopByDocIdentity(ActUserId, RowAmount, OrderBy, m_WordDocuments.LanguageId, RelateNo, StatusId);
                        if (m_DocsRelate.DocId > 0)
                        {
                            DocRelates m_DocRelates = new DocRelates();
                            m_DocsRelate.DocId = m_Docs.DocId;
                            m_DocRelates.CrUserId = ActUserId;
                            m_DocRelates.DocReferenceId = m_DocsRelate.DocId;
                            m_DocRelates.RelateTypeId = ConstantProcessors.RelateTypeId;
                            m_DocRelates.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                            m_DocRelates.Insert(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                        }
                    }
                }
                m_DocsOut = m_Docs;
               
            }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           } 
       }
       private void InsertDocIndex(List<WordBookmarks> l_WordBookmarks, int DocId, byte LanguageId)
       {
           int ActUserId = ConstantProcessors.CrUserId;
           short SysMessageId = 0;
           try
           {
               foreach (WordBookmarks m_WordBookmarks in l_WordBookmarks)
               {
                   DocIndexes m_DocIndexes = new DocIndexes();
                   m_DocIndexes.Bookmark = m_WordBookmarks.BookmarkName;
                   m_DocIndexes.CrUserId = ActUserId;
                   m_DocIndexes.DocId = DocId;
                   m_DocIndexes.LanguageId = LanguageId;
                   m_DocIndexes.LevelId = byte.Parse(m_WordBookmarks.BookmarkLevel.ToString());
                   m_DocIndexes.Title = m_WordBookmarks.BookmarkDesc;
                   m_DocIndexes.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
               }
           }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           } 
           
           
       }
       private int InsertDocItem(ContentItems m_ContentItems, int ItemIndex, int DocId, byte LanguageId, int ParentDocItemId)
       {

           int RetVal = 0;
           try
           {
               int ActUserId = ConstantProcessors.CrUserId;
               short SysMessageId = 0;
               DocItems m_DocItems = new DocItems();
               m_DocItems.CrUserId = ActUserId;
               m_DocItems.DocId = DocId;
               m_DocItems.DocItemLead = m_ContentItems.ItemValue;
               m_DocItems.DocItemName = m_ContentItems.ItemName + " " + m_ContentItems.ItemDesc;
               m_DocItems.DocItemLevelId = byte.Parse(m_ContentItems.ItemLevel.ToString());
               
               m_DocItems.DocItemOrder = ItemIndex;
               m_DocItems.LanguageId = LanguageId;
               m_DocItems.ParentDocItemId = ParentDocItemId;
               m_DocItems.ReviewStatusId = ConstantProcessors.ReviewStatusId;
               m_DocItems.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
               RetVal = m_DocItems.DocItemId;
               if (RetVal > 0 && m_ContentItems.ItemLevel == RegexLevels.Article)
               {
                   InsertDocItemContent(m_ContentItems, DocId, LanguageId, RetVal);
               }
           }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           } 
           
           return RetVal;
       }
       private int InsertDocItemContent(ContentItems m_ContentItems,  int DocId, byte LanguageId, int ParentDocItemId)
       {
           
           int RetVal = 0;
           if (String.IsNullOrEmpty(m_ContentItems.ItemValue))
               return RetVal;
           try
           {
               int ActUserId = ConstantProcessors.CrUserId;
               short SysMessageId = 0;
               int ItemIndex = 1;
               string Bullet = "";
               string LineContent = "";
               string[] l_Lines = m_ContentItems.ItemValue.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
               foreach (string m_Line in l_Lines)
               {
                   if (String.IsNullOrEmpty(m_Line.Trim()))
                       continue;
                   Bullet = "";
                   DocItemContents m_DocItemContents = new DocItemContents();
                   if (Extractors.GetBullet(m_Line, out Bullet))
                   {
                       LineContent = m_Line.Substring(Bullet.Length);
                   }
                   else
                       LineContent = m_Line;
                   m_DocItemContents.CrUserId = ActUserId;
                   m_DocItemContents.AfterBullet = "";
                   m_DocItemContents.Bullet = Bullet;
                   m_DocItemContents.DocItemContentOrder = ItemIndex;
                   m_DocItemContents.DocItemId = ParentDocItemId;
                   m_DocItemContents.DocParagraph = LineContent;
                   m_DocItemContents.MediaTypeId = ContentMediaTypes.Text;
                   m_DocItemContents.PlainParagraph = m_Line;
                   m_DocItemContents.ReviewStatusId = ConstantProcessors.ReviewStatusId;
                   m_DocItemContents.InsertOrUpdate(ConstantProcessors.Replicated, ActUserId, ref SysMessageId);
                   RetVal = m_DocItemContents.DocItemContentId;
                   ItemIndex++;
               }
           }
           catch (Exception ex)
           {
               LogControl.WriteMessage(txtStatus, ex.ToString());

               _logger.Error("Error: " + ex);
           } 
           return RetVal;
       }
        private void btBrowseRoot_Click(object sender, EventArgs e)
        {

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtRootFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btBrowseOutput_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void WriteStatus(string Status, string FileProcessing = "")
        {
            if (this.lbStatus.InvokeRequired)
            {
                this.lbStatus.BeginInvoke((MethodInvoker)delegate() { this.lbStatus.Text = Status;});
            }
            else
            {
                this.lbStatus.Text = Status;
            }
            if (this.lbFileProcess.InvokeRequired)
            {
                this.lbFileProcess.BeginInvoke((MethodInvoker)delegate() { this.lbFileProcess.Text = FileProcessing; });
            }
            else
            {
                this.lbFileProcess.Text = FileProcessing;
            }
        }
    }
}
