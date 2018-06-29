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
using ICSoft.LawDocsLib;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SendMTGateway
{
	/// <summary>
	/// Summary description for SampleWorkItem.
	/// </summary>
	public class ProcessVideoFileWorkItem : WorkItem
	{
        private SendMTGatewayForm m_Form;
        private static Random random = new Random();

        public ProcessVideoFileWorkItem(SendMTGatewayForm myForm)
        {
            m_Form = myForm;
        }

        public override void Perform()
        {
            m_Form.m_ProcessVideoFileRun = true;
            try
            {
                m_Form.WriteLog("Begin ProcessVideoFile");
                string m_SourceDir = ConfigurationManager.AppSettings["PROCESS_VIDEO_SOURCE_DIR"].ToString();
                string m_DestDir = ConfigurationManager.AppSettings["PROCESS_VIDEO_DESTINATION_DIR"].ToString();
                string m_WebPath = ConfigurationManager.AppSettings["PROCESS_VIDEO_WEB_PATH"].ToString();
                string m_CreateM3u8 = ConfigurationManager.AppSettings["PROCESS_VIDEO_CREATE_M3U8"].ToString();
                int m_CreateM3u8FileSize = int.Parse(ConfigurationManager.AppSettings["PROCESS_VIDEO_CREATE_M3U8_FILESIZE"].ToString());
                string m_M3u8SegmentLen = ConfigurationManager.AppSettings["PROCESS_VIDEO_M3U8_SEGMENT_LEN"].ToString();
                string[] l_filePath = Directory.GetFiles(m_SourceDir);
                foreach (string m_FilePath in l_filePath)
                {
                    try
                    {
                        m_Form.WriteLog("Process file: " + m_FilePath);
                        if (ICSoft.HelperLib.FileUploadHelpers.FileInUse(m_FilePath))
                        {
                            m_Form.WriteLog("File in used!");
                        }
                        else
                        {
                            string m_FileName = ICSoft.HelperLib.FileUploadHelpers.GetFileName(m_FilePath);
                            m_Form.WriteLog(m_FileName);
                            m_FileName = m_FileName.Replace(" ", "_");
                            m_FileName = Regex.Replace(m_FileName, "[^0-9a-zA-Z-_.]+", "");
                            string m_WebPathDest = Path.Combine(ICSoft.HelperLib.FileUploadHelpers.GetDirByFileType(m_FilePath), DateTime.Now.ToString("yyyy/MM/dd"));
                            string m_DirPathDest = Path.Combine(m_DestDir, m_WebPathDest).Replace("/", "\\");
                            if (!Directory.Exists(m_DirPathDest))
                            {
                                Directory.CreateDirectory(m_DirPathDest);
                            }
                            string m_FilePathDest = Path.Combine(m_DirPathDest, m_FileName);
                            m_Form.WriteLog(m_FilePathDest);
                            if (!File.Exists(m_FilePathDest))
                            {
                                File.Move(m_FilePath, m_FilePathDest);
                                //update to DB
                                short SysMessageId = 0;
                                ICSoft.CMSLib.Medias m_Medias = new ICSoft.CMSLib.Medias();
                                m_Medias.MediaName = m_FileName.Replace("_", " ").Replace("-"," ");
                                m_Medias.MediaDesc = m_Medias.MediaName;
                                m_Medias.MediaGroupId = 0;
                                m_Medias.MediaTypeId = ICSoft.HelperLib.FileUploadHelpers.GetFileTypeId(m_FilePath);
                                m_Medias.FilePath = Path.Combine(m_WebPath, m_WebPathDest, m_FileName).Replace("\\", "/");
                                m_Medias.Insert(1, 1, ref SysMessageId);
                                m_Form.WriteLog(m_Medias.FilePath);

                                if (m_CreateM3u8 == "1" && m_FileName.EndsWith("mp4"))
                                {
                                    FileInfo m_FileInfo = new FileInfo(m_FilePathDest);
                                    if (m_FileInfo.Length > m_CreateM3u8FileSize)
                                    {
                                        m_WebPathDest = Path.Combine(ICSoft.HelperLib.FileUploadHelpers.GetDirByFileType(m_FilePath.Replace(".mp4", ".m3u8")), DateTime.Now.ToString("yyyy/MM/dd"));
                                        m_DirPathDest = Path.Combine(m_DestDir, m_WebPathDest).Replace("/", "\\");
                                        string m_M3u8File = ProcessHelpers.ConvertToM3u8(m_FilePathDest, m_DirPathDest, m_M3u8SegmentLen, m_Form);
                                        if (File.Exists(m_M3u8File))
                                        {
                                            //update to DB
                                            m_FileName = m_FileName.Replace(".mp4", ".m3u8");
                                            m_Medias.MediaId = 0;
                                            m_Medias.MediaName = m_FileName;
                                            m_Medias.MediaDesc = m_Medias.MediaName;
                                            m_Medias.MediaGroupId = 0;
                                            m_Medias.MediaTypeId = ICSoft.HelperLib.FileUploadHelpers.GetFileTypeId(m_FilePath);
                                            m_Medias.FilePath = m_M3u8File.Replace(m_DestDir, m_WebPath).Replace("\\", "/");
                                            m_Medias.Insert(1, 1, ref SysMessageId);
                                            m_Form.WriteLog(m_Medias.FilePath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        m_Form.WriteLog_Error(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
            }
            m_Form.m_ProcessVideoFileRun = false;

        }
	}
}
