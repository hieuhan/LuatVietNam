using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using ICSoft.CMSLib;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Diagnostics;

namespace SendMTGateway
{
    public class ProcessHelpers
    {
        public static bool ConvertWavToMp3(string LocalDir, string fileSource, string fileDest, SendMTGatewayForm m_Form)
        {
            fileSource = LocalDir + (LocalDir.EndsWith("\\") ? fileSource : "\\" + fileSource);
            fileDest = LocalDir + (LocalDir.EndsWith("\\") ? fileDest : "\\" + fileDest);
            return ConvertWavToMp3(fileSource, fileDest, m_Form);
        }

        public static bool ConvertWavToMp3(string fileSource, string fileDest, SendMTGatewayForm m_Form)
        {
            bool RetVal = false;
            try
            {
                if (File.Exists(fileDest)) return true;

                if (File.Exists(fileSource))
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "ffmpeg.exe";  //ftp.exe -i -s:ftp.cmd
                    startInfo.Arguments = "-i " + fileSource + " -f mp3 " + fileDest;
                    //startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
                RetVal = false;
            }
            RetVal = File.Exists(fileDest);
            return RetVal;
        }

        public static string ConvertToM3u8(string fileSource, string dirDest, string segmentTimeLength, SendMTGatewayForm m_Form)
        {
            string RetVal = "";
            try
            {
                dirDest = Path.Combine(dirDest, DateTime.Now.ToString("HHmmss"));
                if (!Directory.Exists(dirDest)) Directory.CreateDirectory(dirDest);
                //string fileDest = Path.Combine(dirDest, Path.GetFileName(fileSource).Replace(".mp4", ".m3u8"));
                string fileDest = Path.Combine(dirDest, "video.m3u8");
                if (File.Exists(fileDest)) return fileDest;

                if (File.Exists(fileSource))
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "ffmpeg.exe";  //ftp.exe -i -s:ftp.cmd
                    startInfo.Arguments = "-i " + fileSource + " -codec copy -vbsf h264_mp4toannexb -map 0 -f segment -segment_list " + fileDest + " -segment_time " + segmentTimeLength + " " + fileDest.Replace(".m3u8", "%05d.ts");
                    //m_Form.WriteLog_Error(startInfo.Arguments);
                    //startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                    RetVal = fileDest;
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
                RetVal = "";
            }
            return RetVal;
        }

        public static bool DownloadFileFTP(string fileGet, string localDataDir, string remoteDataDir)
        {
            bool RetVal = false;
            try
            {
                if (File.Exists(Path.Combine(localDataDir,fileGet))) return true;
                //if (ConfigurationManager.AppSettings["FTP_DOWNLOAD_FILE"].ToString() == "0") return false;

                string remoteHost = ConfigurationManager.AppSettings["FTP_HOST"].ToString();
                string userName = ConfigurationManager.AppSettings["FTP_USER"].ToString();
                string password = ConfigurationManager.AppSettings["FTP_PASS"].ToString();

                if (!Directory.Exists(localDataDir)) Directory.CreateDirectory(localDataDir);

                StringBuilder ftpCommand = new StringBuilder();
                ftpCommand.AppendLine("open " + remoteHost);
                ftpCommand.AppendLine(userName);
                ftpCommand.AppendLine(password);
                ftpCommand.AppendLine("quote PASV");
                ftpCommand.AppendLine("lcd " + localDataDir);
                //if (!string.IsNullOrEmpty(remoteDataDir)) ftpCommand.AppendLine("cd " + remoteDataDir);
                ftpCommand.AppendLine("binary");
                ftpCommand.AppendLine("get " + remoteDataDir + "/" + fileGet);
                ftpCommand.AppendLine("quit");

                StreamWriter sw = File.CreateText("ftp.cmd");
                sw.Write(ftpCommand.ToString());
                sw.Close();

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "ftp.bat";  //ftp.exe -i -s:ftp.cmd
                //startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                RetVal = false;
            }
            RetVal = File.Exists(Path.Combine(localDataDir, fileGet));
            return RetVal;
        }

        public static string DownloadFile(string url, string rootDir, string virtualPath, bool createImageThumbnail)
        {
            string result = "";
            //string rootDir = "D:/Temp";
            string filename = url.Substring(url.LastIndexOf("/") + 1);
            string localFileDir = Path.Combine(rootDir, virtualPath).Replace("/","\\");
            string localFileName = Path.Combine(localFileDir, filename);
            if (!Directory.Exists(localFileDir)) Directory.CreateDirectory(localFileDir);

            //try
            //{
                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(url), localFileName);
                result = Path.Combine(virtualPath, filename).Replace("\\", "/");
                //Create thumbnail if image
                if (File.Exists(localFileName) & createImageThumbnail && IsImageFile(filename))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"]))
                    {
                        int imgWidth = int.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"].ToString());
                        int imgHeight = int.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_HEIGHT"].ToString());
                        if (imgWidth > 0 && imgHeight > 0)
                        {
                            string fileThumb = localFileName.Replace("Original\\", "Thumb\\");
                            string dirThumb = localFileDir.Replace("Original\\", "Thumb\\");
                            if (!Directory.Exists(dirThumb))
                            {
                                Directory.CreateDirectory(dirThumb);
                            }
                            CreateThumbnail(localFileName, imgWidth, imgHeight).Save(fileThumb, ImageFormat.Jpeg);
                        }
                    }
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MEDIA_WIDTH"]))
                    {
                        int imgWidth = int.Parse(ConfigurationManager.AppSettings["MEDIA_WIDTH"].ToString());
                        int imgHeight = int.Parse(ConfigurationManager.AppSettings["MEDIA_HEIGHT"].ToString());
                        if (imgWidth > 0 && imgHeight > 0)
                        {
                            string fileThumb = localFileName.Replace("Original\\", "Standard\\");
                            string dirThumb = localFileDir.Replace("Original\\", "Standard\\");
                            if (!Directory.Exists(dirThumb))
                            {
                                Directory.CreateDirectory(dirThumb);
                            }
                            CreateThumbnail(localFileName, imgWidth, imgHeight).Save(fileThumb, ImageFormat.Jpeg);
                        }
                    }
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MEDIA_MOBILE_WIDTH"]))
                    {
                        int imgWidth = int.Parse(ConfigurationManager.AppSettings["MEDIA_MOBILE_WIDTH"].ToString());
                        int imgHeight = int.Parse(ConfigurationManager.AppSettings["MEDIA_MOBILE_HEIGHT"].ToString());
                        if (imgWidth > 0 && imgHeight > 0)
                        {
                            string fileThumb = localFileName.Replace("Original\\", "Mobile\\");
                            string dirThumb = localFileDir.Replace("Original\\", "Mobile\\");
                            if (!Directory.Exists(dirThumb))
                            {
                                Directory.CreateDirectory(dirThumb);
                            }
                            CreateThumbnail(localFileName, imgWidth, imgHeight).Save(fileThumb, ImageFormat.Jpeg);
                        }
                    }
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"]))
                    {
                        int imgWidth = int.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"].ToString());
                        int imgHeight = int.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_HEIGHT"].ToString());
                        if (imgWidth > 0 && imgHeight > 0)
                        {
                            string fileThumb = localFileName.Replace("Original\\", "Icon\\");
                            string dirThumb = localFileDir.Replace("Original\\", "Icon\\");
                            if (!Directory.Exists(dirThumb))
                            {
                                Directory.CreateDirectory(dirThumb);
                            }
                            CreateThumbnail(localFileName, imgWidth, imgHeight).Save(fileThumb, ImageFormat.Jpeg);
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    result = "";
            //}
            return result;
        }

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bmpOut;
        }

        public static bool IsImageFile(string FileName)
        {
            bool RetVal = false;
            string fileExt = Path.GetExtension(FileName).ToLower();
            string imageFile = ".jpg;.gif;.png;.bmp";
            if (imageFile.IndexOf(fileExt) >= 0) RetVal = true;
            return RetVal;
        }

        public static void AppendTextFile(string FilePath, string FileContent)
        {
            //using (StreamWriter sw = File.CreateText(FilePath))
            //{
            //    sw.WriteLine(FileContent);
            //    sw.Close();
            //}
            File.AppendAllText(FilePath, FileContent);
        }
    }
}
