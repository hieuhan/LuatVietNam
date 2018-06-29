using HtmlAgilityPack;
using ICSoft.CMSLib;
using sms.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace ICSoft.HelperLib
{
    public class InternalLinkHelper
    {
        public static string getContentInternalLinks(short siteId, string content)
        {
            string RetVal = string.Empty;
            try
            {
                //Log.writeLog("Start insert internallink", ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                InternalLinks mInternalLinks = new InternalLinks();
                List<InternalLinks> lInternalLinks = new List<InternalLinks>();
                IList lIlist = mInternalLinks.GetListToInsert(siteId, "LinkCounter ASC, LEN(InternalLinkName) DESC", 1);
                foreach (InternalLinks item in lIlist)
                {
                    lInternalLinks.Add(item);
                }

                int _imax = 3, _iloop = 1;
                string _nofollow = "0", _target_blank = "0", _domain = "", _saveil = "0", _keywords = "";
                _imax = int.Parse(getInternalLinkSave("icp_maxlink"));
                _iloop = int.Parse(getInternalLinkSave("icp_looplink"));
                _nofollow = getInternalLinkSave("icp_nofollow");
                _target_blank = getInternalLinkSave("icp_targetblank");
                _domain = getInternalLinkSave("icp_domain");
                _keywords = getInternalLinkSave("icp_keywords");
                _saveil = getInternalLinkSave("icp_saveil");

                if (!string.IsNullOrEmpty(_domain) && !(lInternalLinks.Count == 0) && !string.IsNullOrEmpty(content.Trim()))
                {
                    RetVal = insertInternalLink(content.Trim(), _imax, _iloop, _target_blank, _nofollow, _domain, _saveil, lInternalLinks);
                }
                else
                {
                    RetVal = content;
                }
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString() + ":" +  ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return RetVal;
        }
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// Thêm Internal Link vào bài viết
        /// 4 trường hợp:
        ///     - Bài viết có chứa keyword
        ///         + Bài viết có chứa Internal Link
        ///             * Giữ lại Internal Link
        ///             * Xóa Internal Link cũ và thêm mới
        ///         + Bài viết chưa có Internal Link
        ///     - Bài viết không chứa keyword
        /// </summary>
        /// <param name="Val">Content</param>
        /// <param name="iMax">Số link tối đa trong 1 bài viết</param>
        /// <param name="iLoop">Số lần lặp tối đa của 1 link</param>
        /// <param name="target">Open in new window (1)</param>
        /// <param name="nofollow">Add nofollow attribute in tag a (1)</param>
        /// <param name="domain">domain của trang đang chạy để kiểm tra số Internal Link đã tồn tại trong bài viết</param>
        /// <param name="saveIL">Giữ lại Internal Link đã tồn tại (1)</param> 
        /// <param name="keywords">Keywords xác định internal links</param> 
        /// <returns>Content gán Internal Link</returns>
        protected static string insertInternalLink(string Val, int iMax, int iLoop, string target, string nofollow, string domain, string saveIL, List<InternalLinks> lInternalLinks)
        {
            List<string> LThayThe = new List<string>();
            List<InternalLinks> l_InternalLinks_Insert = new List<InternalLinks>();
            string lInternalLinkId = string.Empty;
            int idemExitIL = 0;
            string RetVal = string.Empty;
            int idemLink = 0;
            RetVal = Val;
            try
            {
                if (lInternalLinks.Count > 0)
                {
                    string strVal = convertToUnSign3(Val);
                    bool flagExitKey = false;
                    // Kiểm tra xem trong Content có keyword không
                    foreach (var item in lInternalLinks)
                    {
                        if (strVal.IndexOf(convertToUnSign3(item.InternalLinkName)) >= 0)
                        {
                            flagExitKey = true;
                            break;
                        }
                        flagExitKey = false;
                    }
                    if (flagExitKey)//Tồn tại keyword
                    {
                        //Kiểm tra có chứa internal link không?
                        bool flagExitIL = false;
                        var doc = new HtmlDocument();
                        //Thay thế các thẻ ngày thẻ a có attr title
                        doc.LoadHtml(RetVal);
                        foreach (var itemEle in doc.DocumentNode.DescendantNodes().Where(n => n.Attributes.Count(s => s.Name.ToLower() == "title") > 0))
                        {
                            if (!string.IsNullOrEmpty(itemEle.Attributes["title"].Value) && (itemEle.Name.ToLower() != "a"))
                            {
                                string KeyA = "key_il_" + Guid.NewGuid();
                                LThayThe.Add(KeyA + "<<key_il>>" + itemEle.Attributes["title"].Value);
                                itemEle.Attributes["title"].Value = KeyA;
                            }
                        }
                        foreach (var itemEle in doc.DocumentNode.DescendantNodes().Where(n => n.Attributes.Count(s => s.Name.ToLower() == "alt") > 0))
                        {
                            if (!string.IsNullOrEmpty(itemEle.Attributes["alt"].Value) && (itemEle.Name.ToLower() != "a"))
                            {
                                string KeyA = "key_il_" + Guid.NewGuid();
                                LThayThe.Add(KeyA + "<<key_il>>" + itemEle.Attributes["alt"].Value);
                                itemEle.Attributes["alt"].Value = KeyA;
                            }
                        }
                        foreach (var itemEle in doc.DocumentNode.DescendantNodes().Where(n => n.Attributes.Count(s => s.Name.ToLower() == "src") > 0))
                        {
                            if (!string.IsNullOrEmpty(itemEle.Attributes["src"].Value) && (itemEle.Name.ToLower() != "a"))
                            {
                                string KeyA = "key_il_" + Guid.NewGuid();
                                LThayThe.Add(KeyA + "<<key_il>>" + itemEle.Attributes["src"].Value);
                                itemEle.Attributes["src"].Value = KeyA;
                            }
                        }
                        RetVal = doc.DocumentNode.OuterHtml;

                        //Kiểm tra xem đã tồn tại Internal Link trong bài viết chưa
                        if (doc.DocumentNode.Descendants("a").Select(x => x.Attributes["href"]).ToList().Count > 0)
                        {
                            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                            {
                                HtmlAttribute attr_href = link.Attributes["href"];
                                HtmlAttribute attr_class = link.Attributes["class"];
                                if (attr_class != null && attr_href.Value.ToString().ToLower().IndexOf(domain.ToLower()) >= 0 && attr_class.Value.ToString().Trim() == "icp-internal-links")
                                {
                                    flagExitIL = true;
                                    break;
                                }
                                flagExitIL = false;
                            }
                        }

                        if (flagExitIL)// Có tồn tại Internal Link
                        {
                            if (saveIL.ToLower() == "1")//Giữ lại Internal Link đã có
                            {
                                doc.LoadHtml(RetVal);
                                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                                {
                                    HtmlAttribute attr_href = link.Attributes["href"];
                                    HtmlAttribute attr_class = link.Attributes["class"];
                                    string KeyA = "key_il_" + Guid.NewGuid();
                                    LThayThe.Add(KeyA + "<<key_il>>" + link.OuterHtml);
                                    var newHeadNode = HtmlNode.CreateNode(KeyA);
                                    link.ParentNode.ReplaceChild(newHeadNode.ParentNode, link);

                                    if (attr_class != null && attr_href.Value.ToString().ToLower().IndexOf(domain.ToLower()) >= 0 && attr_class.Value.ToString().Trim() == "icp-internal-links")
                                    {
                                        idemExitIL++;
                                    }
                                }
                                RetVal = doc.DocumentNode.OuterHtml;
                                //Nếu số Internal Link tồn tại nhỏ hơn số Internal Link Max
                                if (idemExitIL < iMax)
                                {
                                    strVal = convertToUnSign3(RetVal);
                                    bool flag = false;
                                    idemLink = idemExitIL;
                                    do
                                    {
                                        #region Insert InternalLink to Content

                                        for (int m = 0; m < lInternalLinks.Count; m++)
                                        {
                                            InternalLinks iInternalLinks = lInternalLinks[m];
                                            List<InternalLinks> iL_InternalLinks_Insert = l_InternalLinks_Insert.Where(s => s.InternalLinkUrl.Trim() == iInternalLinks.InternalLinkUrl.Trim()).ToList();

                                            if (iL_InternalLinks_Insert.Count < iLoop)
                                            {
                                                if (idemLink >= iMax)
                                                {
                                                    break;
                                                }
                                                string pattern = convertToUnSign3(iInternalLinks.InternalLinkName);
                                                if (strVal.IndexOf(pattern) >= 0)
                                                {
                                                    string sentence = strVal;
                                                    IList<int> indeces = new List<int>();
                                                    foreach (Match match in Regex.Matches(sentence, pattern))
                                                    {
                                                        indeces.Add(match.Index);
                                                    }
                                                    IList<int> indeces_2 = new List<int>();
                                                    for (int l = 0; l < indeces.Count; l++)
                                                    {
                                                        int gt = 0;
                                                        int sp = 0;
                                                        do
                                                        {
                                                            if ((int)Encoding.UTF8.GetBytes(RetVal[sp].ToString())[0] != 204)
                                                            {
                                                                gt++;
                                                            }
                                                            sp++;

                                                        } while (gt < indeces[l]);
                                                        indeces_2.Add(sp);
                                                    }
                                                    for (int l = 0; l < indeces.Count; l++)
                                                    {
                                                        string valA = RetVal.Substring(indeces_2[l], pattern.Length);
                                                        string symbolB = indeces[l] - 1 < 0 ? "" : strVal[indeces[l] - 1].ToString();
                                                        string symbolA = indeces[l] + pattern.Length + 1 > strVal.Length ? "" : strVal[indeces[l] + pattern.Length].ToString();
                                                        if (!(symbolA != "" && Regex.IsMatch(symbolA, @"^([a-zA-Z])$")) && !(symbolB != "" && Regex.IsMatch(symbolB, @"^([a-zA-Z])$")))
                                                        {
                                                            flag = true;
                                                            idemLink++;
                                                            string keyA = "key_il_" + Guid.NewGuid();
                                                            LThayThe.Add(keyA + "<<key_il>>" + "<a  class='icp-internal-links' href='" + iInternalLinks.InternalLinkUrl + "' " + (target == "1" ? "target='_blank'" : "") + " " + (nofollow == "1" ? "rel='nofollow'" : "") + " title='" + iInternalLinks.InternalLinkName + "'>" + valA + "</a>");
                                                            RetVal = RetVal.Remove(indeces_2[l], pattern.Length).Insert(indeces_2[l], keyA);
                                                            strVal = convertToUnSign3(RetVal);
                                                            l_InternalLinks_Insert.Add(iInternalLinks);
                                                            if (!string.IsNullOrEmpty(lInternalLinkId)) lInternalLinkId += ",";
                                                            lInternalLinkId += iInternalLinks.InternalLinkId;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            flag = false;
                                                        }
                                                    }
                                                }
                                                flag = false;
                                            }
                                            else
                                            {
                                                flag = false;
                                            }
                                        }
                                        #endregion
                                    } while (idemLink < iMax && flag == true);
                                }
                            }
                            else//Xóa Internal Link đã có
                            {
                                doc.LoadHtml(RetVal);
                                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                                {
                                    HtmlAttribute attr_href = link.Attributes["href"];
                                    HtmlAttribute attr_class = link.Attributes["class"];
                                    if (attr_class != null && attr_href.Value.ToString().ToLower().IndexOf(domain.ToLower()) >= 0 && attr_class.Value.ToString().Trim() == "icp-internal-links")
                                    {
                                        var newHeadNode = HtmlNode.CreateNode(link.InnerHtml);
                                        link.ParentNode.ReplaceChild(newHeadNode.ParentNode, link);
                                    }
                                    else
                                    {
                                        string KeyA = "key_il_" + Guid.NewGuid();
                                        LThayThe.Add(KeyA + "<<key_il>>" + link.OuterHtml);
                                        var newHeadNode = HtmlNode.CreateNode(KeyA);
                                        link.ParentNode.ReplaceChild(newHeadNode.ParentNode, link);
                                    }
                                }
                                RetVal = doc.DocumentNode.OuterHtml;
                                strVal = convertToUnSign3(RetVal);
                                bool flag = false;
                                idemLink = 0;
                                do
                                {
                                    #region Insert InternalLink to Content

                                    for (int m = 0; m < lInternalLinks.Count; m++)
                                    {
                                        InternalLinks iInternalLinks = lInternalLinks[m];
                                        List<InternalLinks> iL_InternalLinks_Insert = l_InternalLinks_Insert.Where(s => s.InternalLinkUrl.Trim() == iInternalLinks.InternalLinkUrl.Trim()).ToList();

                                        if (iL_InternalLinks_Insert.Count < iLoop)
                                        {
                                            if (idemLink >= iMax)
                                            {
                                                break;
                                            }
                                            string pattern = convertToUnSign3(iInternalLinks.InternalLinkName);
                                            if (strVal.IndexOf(pattern) >= 0)
                                            {
                                                string sentence = strVal;

                                                IList<int> indeces = new List<int>();
                                                foreach (Match match in Regex.Matches(sentence, pattern))
                                                {
                                                    indeces.Add(match.Index);
                                                }
                                                IList<int> indeces_2 = new List<int>();
                                                for (int l = 0; l < indeces.Count; l++)
                                                {
                                                    int gt = 0;
                                                    int sp = 0;
                                                    do
                                                    {
                                                        if ((int)Encoding.UTF8.GetBytes(RetVal[sp].ToString())[0] != 204)
                                                        {
                                                            gt++;
                                                        }
                                                        sp++;

                                                    } while (gt < indeces[l]);
                                                    indeces_2.Add(sp);
                                                }
                                                for (int l = 0; l < indeces.Count; l++)
                                                {
                                                    string valA = RetVal.Substring(indeces_2[l], pattern.Length);
                                                    string symbolB = indeces[l] - 1 < 0 ? "" : strVal[indeces[l] - 1].ToString();
                                                    string symbolA = indeces[l] + pattern.Length + 1 > strVal.Length ? "" : strVal[indeces[l] + pattern.Length].ToString();
                                                    if (!(symbolA != "" && Regex.IsMatch(symbolA, @"^([a-zA-Z])$")) && !(symbolB != "" && Regex.IsMatch(symbolB, @"^([a-zA-Z])$")))
                                                    {
                                                        flag = true;
                                                        idemLink++;
                                                        string keyA = "key_il_" + Guid.NewGuid();
                                                        LThayThe.Add(keyA + "<<key_il>>" + "<a  class='icp-internal-links' href='" + iInternalLinks.InternalLinkUrl + "' " + (target == "1" ? "target='_blank'" : "") + " " + (nofollow == "1" ? "rel='nofollow'" : "") + " title='" + iInternalLinks.InternalLinkName + "'>" + valA + "</a>");
                                                        RetVal = RetVal.Remove(indeces_2[l], pattern.Length).Insert(indeces_2[l], keyA);
                                                        strVal = convertToUnSign3(RetVal);
                                                        l_InternalLinks_Insert.Add(iInternalLinks);
                                                        if (!string.IsNullOrEmpty(lInternalLinkId)) lInternalLinkId += ",";
                                                        lInternalLinkId += iInternalLinks.InternalLinkId;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        flag = false;
                                                    }
                                                }
                                            }
                                            flag = false;
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    #endregion
                                } while (idemLink < iMax && flag == true);
                            }
                        }
                        else// Không tồn tại Internal Link
                        {
                            doc.LoadHtml(RetVal);
                            if (doc.DocumentNode.Descendants("a").Select(x => x.Attributes["href"]).ToList().Count > 0)
                            {
                                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                                {
                                    HtmlAttribute attr_href = link.Attributes["href"];
                                    string KeyA = "key_il_" + Guid.NewGuid();
                                    LThayThe.Add(KeyA + "<<key_il>>" + link.OuterHtml);
                                    var newHeadNode = HtmlNode.CreateNode(KeyA);
                                    link.ParentNode.ReplaceChild(newHeadNode.ParentNode, link);
                                }
                            }
                            RetVal = doc.DocumentNode.OuterHtml;
                            strVal = convertToUnSign3(RetVal);
                            bool flag = false;
                            do
                            {
                                #region Insert InternalLink to Content

                                for (int m = 0; m < lInternalLinks.Count; m++)
                                {
                                    InternalLinks iInternalLinks = lInternalLinks[m];
                                    List<InternalLinks> iL_InternalLinks_Insert = l_InternalLinks_Insert.Where(s => s.InternalLinkUrl.Trim() == iInternalLinks.InternalLinkUrl.Trim()).ToList();

                                    if (iL_InternalLinks_Insert.Count < iLoop)
                                    {
                                        if (idemLink >= iMax)
                                        {
                                            break;
                                        }
                                        string pattern = convertToUnSign3(iInternalLinks.InternalLinkName);
                                        if (strVal.IndexOf(pattern) >= 0)
                                        {
                                            string sentence = strVal;
                                            IList<int> indeces = new List<int>();
                                            foreach (Match match in Regex.Matches(sentence, pattern))
                                            {
                                                indeces.Add(match.Index);
                                            }
                                            IList<int> indeces_2 = new List<int>();
                                            for (int l = 0; l < indeces.Count; l++)
                                            {
                                                int gt = 0;
                                                int sp = 0;
                                                do
                                                {
                                                    if ((int)Encoding.UTF8.GetBytes(RetVal[sp].ToString())[0] != 204)
                                                    {
                                                        gt++;
                                                    }
                                                    sp++;

                                                } while (gt < indeces[l]);
                                                indeces_2.Add(sp);
                                            }
                                            for (int l = 0; l < indeces.Count; l++)
                                            {
                                                string valA = RetVal.Substring(indeces_2[l], pattern.Length);
                                                string symbolB = indeces[l] - 1 < 0 ? "" : strVal[indeces[l] - 1].ToString();
                                                string symbolA = indeces[l] + pattern.Length + 1 > strVal.Length ? "" : strVal[indeces[l] + pattern.Length].ToString();
                                                if (!(symbolA != "" && Regex.IsMatch(symbolA, @"^([a-zA-Z])$")) && !(symbolB != "" && Regex.IsMatch(symbolB, @"^([a-zA-Z])$")))
                                                {
                                                    flag = true;
                                                    idemLink++;
                                                    string keyA = "key_il_" + Guid.NewGuid();
                                                    LThayThe.Add(keyA + "<<key_il>>" + "<a  class='icp-internal-links' href='" + iInternalLinks.InternalLinkUrl + "' " + (target == "1" ? "target='_blank'" : "") + " " + (nofollow == "1" ? "rel='nofollow'" : "") + " title='" + iInternalLinks.InternalLinkName + "'>" + valA + "</a>");
                                                    RetVal = RetVal.Remove(indeces_2[l], pattern.Length).Insert(indeces_2[l], keyA);
                                                    strVal = convertToUnSign3(RetVal);
                                                    l_InternalLinks_Insert.Add(iInternalLinks);
                                                    if (!string.IsNullOrEmpty(lInternalLinkId)) lInternalLinkId += ",";
                                                    lInternalLinkId += iInternalLinks.InternalLinkId;
                                                    break;
                                                }
                                                else
                                                {
                                                    flag = false;
                                                }
                                            }
                                        }
                                        flag = false;
                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                }
                                #endregion
                            } while (idemLink < iMax && flag == true);
                        }
                    }
                    for (int s = LThayThe.Count - 1; s >= 0; s--)
                    {
                        string item = LThayThe[s];
                        string[] litem = item.Split(new string[] { "<<key_il>>" }, StringSplitOptions.None);
                        RetVal = RetVal.Replace(litem[0], litem[1]);
                    }
                    //Log.writeLog("lInternalLinkId Update counter: " + lInternalLinkId, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    if (!string.IsNullOrEmpty(lInternalLinkId))
                    {
                        //lInternalLinkId = lInternalLinkId.Substring(1);
                        InternalLinks mInternalLinks = new InternalLinks();
                        mInternalLinks.UpdateCounter(lInternalLinkId, 1);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return RetVal;
        }

        public static string getInternalLinkSave(string key)
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
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// Convert từ có dấu sang không dấu, in hoa sang thường
        /// </summary>
        /// <param name="s">chuỗi có dấu</param>
        /// <returns>chuỗi không dấu, in thường</returns>
        public static string convertToUnSign3(string s)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = s.Normalize(NormalizationForm.FormD);
                return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return string.Empty;
        }
    }
   
}
