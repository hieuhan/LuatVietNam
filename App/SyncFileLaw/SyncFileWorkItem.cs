#region Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
/*
   Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
   All rights are reserved.

   Permission to use, copy, modify, and distribute this software 
   for any purpose and without any fee is hereby granted, 
   provided this notice is included in its entirety in the 
   documentation and in the source files.
  
   This software and any related documentation is provided "as is" 
   without any warranty of any kind, either express or implied, 
   including, without limitation, the implied warranties of 
   merchantibility or fitness for a particular purpose. The entire 
   risk arising out of use or performance of the software remains 
   with you. 
   
   In no event shall Richard Schneider, Black Hen Limited, or their agents 
   be liable for any cost, loss, or damage incurred due to the 
   use, malfunction, or misuse of the software or the inaccuracy 
   of the documentation associated with the software. 
*/
#endregion

using BlackHen.Threading;
using System;
using System.Configuration;
using System.Threading;
using System.Reflection;
using UploadFileClient.ServiceUpload;
using System.IO;
using System.Data;

namespace SyncMediaFile
{
    /// <summary>
    /// Summary description for SampleWorkItem.
    /// </summary>
    public class SyncFileWorkItem : WorkItem
    {
        private SyncFileInfo m_DataObject;
        private MainForm m_Form;
        private static Random random = new Random();

        public SyncFileWorkItem(SyncFileInfo myData, MainForm myForm)
        {
            m_DataObject = myData;
            m_Form = myForm;
        }

        public override void Perform()
        {
            try
            {
                //Thread.Sleep(random.Next(80, 120));
                // get last server
                string[] l_Server = ConfigurationManager.AppSettings["ServiceAddress"].ToString().Split(';');
                if (m_DataObject.FileType != "Image") //process thumnail image
                {
                    l_Server = ConfigurationManager.AppSettings["ServiceAddressOther"].ToString().Split(';');
                }
                bool UploadSuc = false;
                //end get last server
                string fileLocal = Path.Combine(m_DataObject.LocalRootDir, m_DataObject.LocalRelativeFilePath);
                m_Form.WriteLog("Processing: " + fileLocal);
                if (File.Exists(fileLocal))
                {
                    FileUploadServiceClient client = new FileUploadServiceClient();
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(m_DataObject.ServiceAddress);
                    string fileName = fileLocal.Substring(fileLocal.LastIndexOf("\\") + 1);
                    string subFolder = m_DataObject.LocalRelativeFilePath.Replace(fileName, "");
                    string fileType = m_DataObject.FileType;
                    string username = m_DataObject.Username;
                    string password = m_DataObject.Password;
                    string message = "";
                    System.IO.FileStream stream = new System.IO.FileStream(fileLocal, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    UploadSuc = client.Upload(username, password, fileName, fileType, subFolder, stream, out message);
                    m_Form.WriteLog("Processing: " + fileLocal + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                    if (!UploadSuc)
                    {
                        writeLog(message, "File: " + fileLocal + " >> " + m_DataObject.ServiceAddress);
                    }
                    if (m_DataObject.FileType == "Image" && UploadSuc) //process thumnail image
                    {
                        string fileThum = fileLocal.Replace("original\\", "icon\\");
                        string subFolderThum = subFolder.Replace("original\\", "icon\\");
                        if (fileThum != fileLocal && File.Exists(fileThum))
                        {
                            stream = new System.IO.FileStream(fileThum, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            UploadSuc = client.Upload(username, password, fileName, fileType, subFolderThum, stream, out message);
                            m_Form.WriteLog("Processing: " + fileThum + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                            if (!UploadSuc)
                            {
                                writeLog(message, "File: " + fileThum + " >> " + m_DataObject.ServiceAddress);
                            }
                        }

                        fileThum = fileLocal.Replace("original\\", "mobile\\");
                        subFolderThum = subFolder.Replace("original\\", "mobile\\");
                        if (fileThum != fileLocal && File.Exists(fileThum) && UploadSuc)
                        {
                            stream = new System.IO.FileStream(fileThum, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            UploadSuc = client.Upload(username, password, fileName, fileType, subFolderThum, stream, out message);
                            m_Form.WriteLog("Processing: " + fileThum + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                            if (!UploadSuc)
                            {
                                writeLog(message, "File: " + fileThum + " >> " + m_DataObject.ServiceAddress);
                            }
                        }

                        fileThum = fileLocal.Replace("original\\", "standard\\");
                        subFolderThum = subFolder.Replace("original\\", "standard\\");
                        if (fileThum != fileLocal && File.Exists(fileThum) && UploadSuc)
                        {
                            stream = new System.IO.FileStream(fileThum, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            UploadSuc = client.Upload(username, password, fileName, fileType, subFolderThum, stream, out message);
                            m_Form.WriteLog("Processing: " + fileThum + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                            if (!UploadSuc)
                            {
                                writeLog(message, "File: " + fileThum + " >> " + m_DataObject.ServiceAddress);
                            }
                        }

                        fileThum = fileLocal.Replace("original\\", "thumnail\\");
                        subFolderThum = subFolder.Replace("original\\", "thumnail\\");
                        if (fileThum != fileLocal && File.Exists(fileThum) && UploadSuc)
                        {
                            stream = new System.IO.FileStream(fileThum, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            UploadSuc = client.Upload(username, password, fileName, fileType, subFolderThum, stream, out message);
                            m_Form.WriteLog("Processing: " + fileThum + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                            if (!UploadSuc)
                            {
                                writeLog(message, "File: " + fileThum + " >> " + m_DataObject.ServiceAddress);
                            }
                        }
                        // facebook
                        fileThum = fileLocal.Replace("original\\", "facebook\\");
                        subFolderThum = subFolder.Replace("original\\", "facebook\\");
                        if (fileThum != fileLocal && File.Exists(fileThum) && UploadSuc)
                        {
                            stream = new System.IO.FileStream(fileThum, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            UploadSuc = client.Upload(username, password, fileName, fileType, subFolderThum, stream, out message);
                            m_Form.WriteLog("Processing: " + fileThum + " >> " + m_DataObject.ServiceAddress + " >> " + message);
                            if (!UploadSuc)
                            {
                                writeLog(message, "File: " + fileThum + " >> " + m_DataObject.ServiceAddress);
                            }
                        }
                    }

                    client.Close();
                    if (UploadSuc)
                    {
                        //Update status DB
                        MediasDB m_Medias = new MediasDB();
                        //m_Medias.UpdateUpdateSyncStatusOK(m_DataObject.MediaId); // now update server syncount
                        m_Medias.UpdateSyncServerCount(m_DataObject.MediaId);
                        // check for last server
                        DataSet m_DataSet = m_Medias.GetFilesById(m_DataObject.MediaId);
                        if (m_DataSet.Tables[0].Rows.Count > 0)
                        {
                            int SynSuc = int.Parse(m_DataSet.Tables[0].Rows[0]["SyncServerCount"].ToString());
                            if (SynSuc >= l_Server.Length || l_Server.Length == 1)
                            {
                                m_Form.WriteLog("UpdateUpdateSyncStatusOK: SyncServerCount:" + SynSuc.ToString() + " >> l_Server: " + l_Server.Length.ToString());
                                m_Medias.UpdateSyncStatus(m_DataObject.MediaId, 1);
                            }
                            else
                            {
                                m_Form.WriteLog("Not UpdateUpdateSyncStatusOK SyncServerCount:" + SynSuc.ToString() + " >> l_Server: " + l_Server.Length.ToString());
                            }
                        }
                    }
                    else
                    {
                        m_Form.WriteLog("Processing: " + fileLocal + " >>Upload fail");
                        //Update status DB
                        MediasDB m_Medias = new MediasDB();
                        m_Medias.UpdateSyncStatus(m_DataObject.MediaId, 2);
                    }
                }
                else
                {
                    m_Form.WriteLog("Processing: " + fileLocal + " >> File not found");
                    writeLog("File not found", "File: " + fileLocal + " >> " + m_DataObject.ServiceAddress);
                    //Update status DB
                    MediasDB m_Medias = new MediasDB();
                    m_Medias.UpdateSyncStatus(m_DataObject.MediaId, 2);
                }


            }
            catch (Exception ex)
            {
                m_Form.WriteLog("Processing: " + m_DataObject.LocalRelativeFilePath + " >> " + m_DataObject.ServiceAddress + " >> Fail");
                writeLog(ex.ToString(), "File: " + m_DataObject.LocalRelativeFilePath + " >> " + m_DataObject.ServiceAddress);
                m_Form.WriteLog_Error(ex.ToString());
                //Update status DB
                MediasDB m_Medias = new MediasDB();
                m_Medias.UpdateSyncStatus(m_DataObject.MediaId, 2);
            }
        }

        //=============================================
        public static void writeLog(string logContent, string methodName)
        {
            //get filename
            string currentDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");
            string strFileName = AppDomain.CurrentDomain.BaseDirectory + "Logs\\" + currentDate + "Log.txt";
            string strLogString;
            strLogString = "\r\n";
            strLogString += "===========  " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "  " + methodName + "  ==============" + "\r\n";
            strLogString += logContent + "\r\n";
            strLogString += "===================================================================";
            try
            {
                System.Text.Encoding charset = System.Text.Encoding.GetEncoding(65001); //"UTF-8"
                if (!File.Exists(strFileName))
                {
                    FileStream oFile;
                    oFile = File.Create(strFileName);
                    StreamWriter oReader = new StreamWriter(oFile, charset);
                    oReader.WriteLine(strLogString);
                    oReader.Close();
                    oFile.Close();
                }
                else
                {
                    // Append text in file when file exitsed
                    FileStream oFile1 = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    if (oFile1.Length > 1048576)
                    {
                        oFile1.Close();
                        File.Delete(strFileName);
                    }
                    else
                    {
                        oFile1.Close();
                    }
                    StreamWriter oReader = new StreamWriter(strFileName, true, charset);
                    oReader.WriteLine(strLogString);
                    oReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
