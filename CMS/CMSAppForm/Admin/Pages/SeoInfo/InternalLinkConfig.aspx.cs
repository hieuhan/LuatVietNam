using sms.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_SeoInfo_InternalLinkConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                icp_keywords.Text = getInternalLinkSave("icp_keywords");
                icp_domain.Text = getInternalLinkSave("icp_domain");
                icp_maxlink.Text = getInternalLinkSave("icp_maxlink");
                icp_looplink.Text = getInternalLinkSave("icp_looplink");
                if (getInternalLinkSave("icp_nofollow") == "1")
                { icp_nofollow.Checked = true; }
                if (getInternalLinkSave("icp_targetblank") == "1")
                { icp_targetblank.Checked = true; }
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string fileName, filePath;
        int _imax = 3, _iloop = 1;
        string _nofollow = "0", _target_blank = "0", _domain = "", _saveil = "0", _keywords = "";
        try
        {
            if (string.IsNullOrEmpty(icp_domain.Text) || string.IsNullOrEmpty(icp_keywords.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Keywords và Domain không được trống')", true);
            }
            else
            {
                if (icp_nofollow.Checked == true)
                {
                    _nofollow = "1";
                }
                if (icp_targetblank.Checked == true)
                {
                    _target_blank = "1";
                }
                _domain = icp_domain.Text.Trim();
                _keywords = icp_keywords.Text.Trim();
                _imax = string.IsNullOrEmpty(icp_maxlink.Text) ? 3 : int.Parse(icp_maxlink.Text);
                _iloop = string.IsNullOrEmpty(icp_looplink.Text) ? 1 : int.Parse(icp_looplink.Text);

                fileName = "SaveInternalLinks.txt";
                filePath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
                filePath += fileName;
                Dictionary<string, string> l_Key = new Dictionary<string, string>();
                l_Key.Add("icp_maxlink", _imax.ToString());
                l_Key.Add("icp_looplink", _iloop.ToString());
                l_Key.Add("icp_domain", _domain);
                l_Key.Add("icp_keywords", _keywords);
                l_Key.Add("icp_saveil", _saveil);
                l_Key.Add("icp_nofollow", _nofollow);
                l_Key.Add("icp_targetblank", _target_blank);
                string json = new JavaScriptSerializer().Serialize(l_Key);
                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
                //string strJS = "";
                //strJS += "<script type=" + (char)34 + "text/javascript" + (char)34 + ">";
                //strJS += "window.parent.Cancel();";
                //strJS += "</script>";
                //this.ClientScript.RegisterStartupScript(this.GetType(), "VoteReturn", strJS);

                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cập nhật thành công')", true);
            }
        }
        catch (Exception ex)
        {
            LogFiles.WriteLog(ex.ToString() + "\t" + ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Lỗi cập nhật')", true);
        }
    }
    public string getInternalLinkSave(string key)
    {
        string fileName, filePath;
        try
        {
            fileName = "SaveInternalLinks.txt";
            filePath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
            filePath += fileName;
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }
            else
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonData = string.Empty;
                string stringFromFile = sr.ReadLine();
                while (!string.IsNullOrEmpty(stringFromFile))
                {
                    jsonData += stringFromFile;
                    stringFromFile = sr.ReadLine();
                }
                Dictionary<string, string> l_Key = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonData);
                sr.Close();
                sr.Dispose();
                foreach (var item in l_Key)
                {
                    if (item.Key == key)
                    {
                        return item.Value;
                    }
                }
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
        return string.Empty;
    }
}