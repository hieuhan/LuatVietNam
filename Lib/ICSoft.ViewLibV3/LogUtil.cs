using System.Diagnostics;
using System;
using System.Data;
using System.Collections;
using System.IO;

namespace ICSoft.ViewLibV3
{
    public class LogUtil
    {

        public static void WriteLog(string logContent, string methodName)
        {
            //get filename
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string dirPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            if (!string.IsNullOrEmpty(Constants.LOG_DIR)) dirPath = Constants.LOG_DIR;
            string strFileName = dirPath + "\\" + currentDate + "_Log.txt";
            string strLogString;
            strLogString = "==============  " + DateTime.Now.ToString() + "  " + methodName + "  ==============" + "\r\n";
            strLogString += logContent;
            try
            {
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
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
