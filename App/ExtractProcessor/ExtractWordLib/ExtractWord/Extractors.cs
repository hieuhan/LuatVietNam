using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ICSoft.ExtractWordLib
{
    public class Extractors
    {
        #region Public Properties
        // tree item
        public List<RegexRules> l_RegexRulesArticles = RegexRules.GetRegexRulesArticles();
        public List<RegexRules> l_RegexRulesSubSections = RegexRules.GetRegexRulesSubSections();
        public List<RegexRules> l_RegexRulesSections = RegexRules.GetRegexRulesSections();
        public List<RegexRules> l_RegexRulesChapters = RegexRules.GetRegexRulesChapters();
        public List<RegexRules> l_RegexRulesParts = RegexRules.GetRegexRulesParts();
        public List<RegexRules> l_RegexRulesChildrens = RegexRules.GetRegexRulesChildrens();
        public List<RegexRules> l_RegexRulesParents = RegexRules.GetRegexRulesParents();
        // addition item
        public List<RegexRules> l_RegexRulesAppendices = RegexRules.GetRegexRulesAppendices();
        public List<RegexRules> l_RegexRulesAppendices2 = RegexRules.GetRegexRulesAppendices2();
        public List<RegexRules> l_RegexRulesPublicDate = RegexRules.GetRegexRulesPublicDate();
        public List<RegexRules> l_RegexRulesPublisher = RegexRules.GetRegexRulesPublisher();
        public List<RegexRules> l_RegexRulesLawdocType = RegexRules.GetRegexRulesLawdocType();
        public List<RegexRules> l_RegexRulesLawdocNo = RegexRules.GetRegexRulesLawdocNo();
        public List<RegexRules> l_RegexRulesFooter = RegexRules.GetRegexRulesFooter();
        #endregion
        public Extractors()
        {
            //init static var
        }
        #region Public method
        /// <summary>
        /// Split a word document to three content block
        /// </summary>
        /// <param name="FileContent"></param>
        /// <param name="Header"></param>
        /// <param name="Content"></param>
        /// <param name="Footer"></param>
        /// <returns></returns>
        public int SplitContent(string FileContent, out string Header, out string Content, out string Footer, ref List<Appendices> l_Appendices)
        {
            int RetVal = 0;
            int IndexRule = -1;
            int IndexItem = -1;            
            bool IsEndHeader = false;
            bool IsEndContent = false;
            bool IsHaveAppendix = false;
            l_Appendices = new List<Appendices>();
            Header = "";
            Content = "";
            Footer = "";
            string[] l_Lines = FileContent.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string m_Lines in l_Lines)
            {
                // reading header
                if (!IsEndHeader)
                {
                    if (IsParent(m_Lines, out IndexRule, out IndexItem))
                    {
                        IsEndHeader = true;
                        Content += m_Lines;
                        continue;
                    }
                    else if (IsChildren(m_Lines, out IndexRule, out IndexItem))
                    {
                        IsEndHeader = true;
                        Content += m_Lines;
                        continue;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(Header))
                        {
                            Header = m_Lines;
                        }
                        else
                        {
                            Header += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
                // reading content
                if (!IsEndContent && IsEndHeader)
                {
                    if (IsFooter(m_Lines, out IndexRule, out IndexItem))
                    {
                        IsEndContent = true;
                        if (l_RegexRulesFooter[IndexItem].RegexRuleEndOfPreContent)
                        {
                            Content += System.Environment.NewLine + m_Lines;
                        }
                        else
                        {
                            Footer = m_Lines;
                        }
                        continue;
                    }
                    if (String.IsNullOrEmpty(Content))
                    {
                        Content = m_Lines;
                    }
                    else
                    {
                        Content += System.Environment.NewLine + m_Lines;
                    }
                }
                // reading footer
                if (IsEndContent && IsEndHeader)
                {
                    if (String.IsNullOrEmpty(Footer))
                    {
                        Footer = m_Lines;
                    }
                    else
                    {
                        Footer += System.Environment.NewLine + m_Lines;
                    }
                }
            }
            // read appendix
            if (String.IsNullOrEmpty(Footer) == false && ConstantExtractors.IsProcessAppendix > 0)
            {
                List<string> l_AppandixName = new List<string>();

                string CurentAppandixName = "";
                string NextAppandixName = "";
                IsHaveAppendix = IsAppendices(Footer, ref l_AppandixName);

                try
                {
                    if (IsHaveAppendix && l_AppandixName.Count > 0)
                    {

                        string NewFooter = Footer.Substring(0, Footer.IndexOf(l_AppandixName[0]));
                        for (int index = 0; index < l_AppandixName.Count; index++)
                        {

                            int IndexOfCurentName, IndexOfNextName;
                            CurentAppandixName = l_AppandixName[index];
                            IndexOfCurentName = Footer.IndexOf(CurentAppandixName);
                            if (index < l_AppandixName.Count - 1)
                            {
                                NextAppandixName = l_AppandixName[index + 1];
                                IndexOfNextName = Footer.IndexOf(NextAppandixName);
                            }
                            else
                            {
                                IndexOfNextName = Footer.Length;
                            }


                            Appendices m_Appendices = new Appendices();
                            m_Appendices.AppendixName = CurentAppandixName;
                            m_Appendices.AppendixValue = Footer.Substring(IndexOfCurentName, IndexOfNextName - IndexOfCurentName);
                            l_Appendices.Add(m_Appendices);

                        }
                        if (String.IsNullOrEmpty(NewFooter.Trim()) == false)
                        {
                            Footer = NewFooter;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sms.utils.LogFiles.LogError(ex.ToString());
                }

            }
            return RetVal;
        }        
        //=========================================================
        /// <summary>
        /// get NO of lawdoc return full match string and output true No
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="LawdocNo"></param>
        /// <returns></returns>
        public List<ContentItems> ExtractContent(string Content, out string ErrorList)
        {
            List<ContentItems> RetVal = new List<ContentItems>();
            ErrorList = "";
            bool IsCheckParent = false, IsCheckChildren = false;
            
            RegexRules m_RegexRulesPart, m_RegexRulesChapter, m_RegexRulesSection, m_RegexRulesSubSection, m_RegexRulesArticle;
            IsCheckParent = HaveParent(Content, out m_RegexRulesPart, out m_RegexRulesChapter, out m_RegexRulesSection, out m_RegexRulesSubSection);
            IsCheckParent = true;// 
            IsCheckChildren = HaveChildren(Content, out m_RegexRulesArticle);            
            ContentItems m_CurentPart = new ContentItems();
            ContentItems m_CurentChapter = new ContentItems();
            ContentItems m_CurentSection = new ContentItems();
            ContentItems m_CurentSubSection = new ContentItems();
            ContentItems m_CurentArticle = new ContentItems();
            string[] l_Lines = Content.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string m_Lines in l_Lines)
            {

                if (IsCheckParent)
                {
                    // is part
                    if (IsPart(m_Lines, m_RegexRulesPart))
                    {
                        m_CurentPart = new ContentItems();
                        m_CurentPart.DocLevel = RegexLevels.Part;
                        m_CurentPart.ItemLevel = RegexLevels.Part;
                        m_CurentPart.ItemName = GetItemName(m_Lines, m_RegexRulesPart);
                        string ParentDesc = m_Lines.Replace(m_CurentPart.ItemName, "").Trim();
                        if (!String.IsNullOrEmpty(ParentDesc))
                        {
                            m_CurentPart.ItemDesc = ParentDesc;
                        }
                        
                        m_CurentPart.ItemValue = "";

                        // reset child level
                        m_CurentChapter = new ContentItems();
                        m_CurentSection = new ContentItems();
                        m_CurentSubSection = new ContentItems();
                        m_CurentArticle = new ContentItems();
                        // end reset child level
                        RetVal.Add(m_CurentPart);
                    } //end part
                    //is chapter
                    else if (IsChapter(m_Lines, m_RegexRulesChapter))
                    {
                        m_CurentChapter = new ContentItems();
                        m_CurentChapter.DocLevel = RegexLevels.Chapter;
                        m_CurentChapter.ItemLevel = RegexLevels.Chapter;
                        m_CurentChapter.ItemName = GetItemName(m_Lines, m_RegexRulesChapter);
                        string ParentDesc = m_Lines.Replace(m_CurentChapter.ItemName, "").Trim();
                        if (!String.IsNullOrEmpty(ParentDesc))
                        {
                            m_CurentChapter.ItemDesc = ParentDesc;
                        }
                        
                        m_CurentChapter.ItemValue = "";
                        // reset child level                        
                        m_CurentSection = new ContentItems();
                        m_CurentSubSection = new ContentItems();
                        m_CurentArticle = new ContentItems();
                        // end reset child level
                        if (m_CurentPart.ItemLevel == 0)
                        {
                            RetVal.Add(m_CurentChapter);
                        }
                        else
                        {
                            m_CurentPart.l_Children.Add(m_CurentChapter);
                        }
                    }
                    // end chapter
                    // is section 
                    else if (IsSection(m_Lines, m_RegexRulesSection))
                    {
                        m_CurentSection = new ContentItems();
                        m_CurentSection.DocLevel = RegexLevels.Section;
                        m_CurentSection.ItemLevel = RegexLevels.Section;
                        m_CurentSection.ItemName = GetItemName(m_Lines, m_RegexRulesSection);
                        string ParentDesc = m_Lines.Replace(m_CurentSection.ItemName, "").Trim();
                        if (!String.IsNullOrEmpty(ParentDesc))
                        {
                            m_CurentSection.ItemDesc = ParentDesc;
                        }
                        
                        m_CurentSection.ItemValue = "";
                        // reset child level
                        m_CurentSubSection = new ContentItems();
                        m_CurentArticle = new ContentItems();
                        // end reset child level
                        if (m_CurentPart.ItemLevel == 0 && m_CurentChapter.ItemLevel == 0)
                        {
                            RetVal.Add(m_CurentSection);
                        }
                        else
                        {
                            if (m_CurentChapter.ItemLevel > 0)
                            {
                                m_CurentChapter.l_Children.Add(m_CurentSection);
                            }
                            else
                            {
                                m_CurentPart.l_Children.Add(m_CurentSection);
                            }
                        }
                    }
                    // end section
                    // is sub section 
                    else if (IsSubSection(m_Lines, m_RegexRulesSubSection))
                    {
                        m_CurentSubSection = new ContentItems();
                        m_CurentSubSection.DocLevel = RegexLevels.SubSection;
                        m_CurentSubSection.ItemLevel = RegexLevels.SubSection;
                        m_CurentSubSection.ItemName = GetItemName(m_Lines, m_RegexRulesSubSection);
                        string ParentDesc = m_Lines.Replace(m_CurentSubSection.ItemName, "").Trim();
                        if (!String.IsNullOrEmpty(ParentDesc))
                        {
                            m_CurentSubSection.ItemDesc = ParentDesc;
                        }
                        
                        m_CurentSubSection.ItemValue = "";
                        // reset child level
                        m_CurentArticle = new ContentItems();
                        // end reset child level
                        if (m_CurentPart.ItemLevel == 0 && m_CurentChapter.ItemLevel == 0 && m_CurentSection.ItemLevel == 0)
                        {
                            RetVal.Add(m_CurentSubSection);
                        }
                        else
                        {
                            if (m_CurentSection.ItemLevel > 0)
                            {
                                m_CurentSection.l_Children.Add(m_CurentSubSection);
                            }
                            else if (m_CurentChapter.ItemLevel > 0)
                            {
                                m_CurentChapter.l_Children.Add(m_CurentSubSection);
                            }
                            else
                            {
                                m_CurentPart.l_Children.Add(m_CurentSubSection);
                            }
                        }
                    }
                    // end sub section                    
                    // is Article
                    else if (IsArticle(m_Lines, m_RegexRulesArticle))
                    {
                        m_CurentArticle = new ContentItems();
                        m_CurentArticle.DocLevel = RegexLevels.Article;
                        m_CurentArticle.ItemLevel = RegexLevels.Article;
                        m_CurentArticle.ItemName = GetItemName(m_Lines, m_RegexRulesArticle);
                        string ChildrenDesc = m_Lines.Replace(m_CurentArticle.ItemName, "").Trim();
                        if (IsArticleDesc(ChildrenDesc))
                            m_CurentArticle.ItemDesc = ChildrenDesc;
                        else
                            m_CurentArticle.ItemValue = ChildrenDesc;
                        // reset child level
                        // end reset child level
                        if (m_CurentPart.ItemLevel == 0 && m_CurentChapter.ItemLevel == 0 && m_CurentSection.ItemLevel == 0 && m_CurentSubSection.ItemLevel == 0)
                        {
                            RetVal.Add(m_CurentArticle);
                        }
                        else
                        {
                            if (m_CurentSubSection.ItemLevel > 0)
                            {
                                m_CurentSubSection.l_Children.Add(m_CurentArticle);
                            }
                            else if (m_CurentSection.ItemLevel > 0)
                            {
                                m_CurentSection.l_Children.Add(m_CurentArticle);
                            }
                            else if (m_CurentChapter.ItemLevel > 0)
                            {
                                m_CurentChapter.l_Children.Add(m_CurentArticle);
                            }
                            else
                            {
                                m_CurentPart.l_Children.Add(m_CurentArticle);
                            }
                        }
                    }
                    // end article            
                    // is content
                    else
                    {
                        
                        if (m_CurentArticle.ItemLevel > 0)
                        {
                            if (String.IsNullOrEmpty(m_CurentArticle.ItemValue))
                            {
                                m_CurentArticle.ItemValue = m_Lines;
                            }
                            else
                            {
                                m_CurentArticle.ItemValue += System.Environment.NewLine + m_Lines;
                            }
                        }
                        else if (m_CurentSubSection.ItemLevel > 0)
                        {
                            if (String.IsNullOrEmpty(m_CurentSubSection.ItemDesc))
                            {
                                m_CurentSubSection.ItemDesc = m_Lines;
                            }
                            else if (IsSubSectionDesc(m_Lines))
                            {
                                m_CurentSubSection.ItemDesc += System.Environment.NewLine + m_Lines;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(m_CurentSubSection.ItemValue))
                                {
                                    m_CurentSubSection.ItemValue = m_Lines;
                                }
                                else
                                {
                                    m_CurentSubSection.ItemValue += System.Environment.NewLine + m_Lines;
                                }
                            }
                        }
                        else if (m_CurentSection.ItemLevel > 0)
                        {
                            if (String.IsNullOrEmpty(m_CurentSection.ItemDesc))
                            {
                                m_CurentSection.ItemDesc = m_Lines;
                            }
                            else if (IsSectionDesc(m_Lines))
                            {
                                m_CurentSection.ItemDesc += System.Environment.NewLine + m_Lines;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(m_CurentSection.ItemValue))
                                {
                                    m_CurentSection.ItemValue = m_Lines;
                                }
                                else
                                {
                                    m_CurentSection.ItemValue += System.Environment.NewLine + m_Lines;
                                }
                            }
                        }
                        else if (m_CurentChapter.ItemLevel > 0)
                        {
                            if (String.IsNullOrEmpty(m_CurentChapter.ItemDesc))
                            {
                                m_CurentChapter.ItemDesc = m_Lines;
                            }
                            else if (IsChapterDesc(m_Lines))
                            {
                                m_CurentChapter.ItemDesc += System.Environment.NewLine + m_Lines;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(m_CurentChapter.ItemValue))
                                {
                                    m_CurentChapter.ItemValue = m_Lines;
                                }
                                else
                                {
                                    m_CurentChapter.ItemValue += System.Environment.NewLine + m_Lines;
                                }
                            }
                        }
                        else if (m_CurentPart.ItemLevel > 0)
                        {
                            if (String.IsNullOrEmpty(m_CurentPart.ItemDesc))
                            {
                                m_CurentPart.ItemDesc = m_Lines;
                            }
                            else if (IsPartDesc(m_Lines))
                            {
                                m_CurentPart.ItemDesc += System.Environment.NewLine + m_Lines;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(m_CurentPart.ItemValue))
                                {
                                    m_CurentPart.ItemValue = m_Lines;
                                }
                                else
                                {
                                    m_CurentPart.ItemValue += System.Environment.NewLine + m_Lines;
                                }
                            }
                        }
                        else
                        {
                            ErrorList += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// get NO of lawdoc return full match string and output true No
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="LawdocNo"></param>
        /// <returns></returns>
        public string GetLawdocNo(string Header, out string LawdocNo)
        {
            string RetVal = "";
            LawdocNo = "";
            string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string m_Lines in l_Lines)
            {
                if (String.IsNullOrEmpty(RetVal) == false)
                    break;
                foreach (RegexRules m_RegexRule in l_RegexRulesLawdocNo)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }
                    Match m_Match = Regex.Match(Header, m_RegexRule.RegexRuleText, m_RegexOptions);
                    if (m_Match.Success)
                    {
                        RetVal = m_Match.Groups[0].Value;
                        LawdocNo = m_Match.Groups[int.Parse(m_RegexRule.RegexRulesGroup)].Value;
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// get NO of lawdoc return full match string and output true No
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="LawdocNo"></param>
        /// <returns></returns>
        public string GetLawdocNo(string FileContent, out string LawdocNo, ref List<string> l_DocNoRelated)
        {
            string RetVal = "";
            LawdocNo = "";
            l_DocNoRelated = new List<string>();
            string[] l_Lines = FileContent.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string m_Lines in l_Lines)
            {
                if (String.IsNullOrEmpty(RetVal) == false)
                    break;
                foreach (RegexRules m_RegexRule in l_RegexRulesLawdocNo)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }
                    Match m_Match = Regex.Match(FileContent, m_RegexRule.RegexRuleText, m_RegexOptions);
                    while (m_Match.Success)
                    {
                        if (String.IsNullOrEmpty(RetVal))
                        {
                            RetVal = m_Match.Groups[0].Value;
                            
                            LawdocNo = m_Match.Groups[int.Parse(m_RegexRule.RegexRulesGroup)].Value;

                        }
                        else
                        {
                            string DocNoTemp = m_Match.Groups[int.Parse(m_RegexRule.RegexRulesGroup)].Value;
                            if (DocNoTemp.Contains("/") || DocNoTemp.Contains("\\") || DocNoTemp.Contains("-"))
                                l_DocNoRelated.Add(DocNoTemp);
                        }
                        m_Match = m_Match.NextMatch();
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        ///  get public date return full match string and output Public date
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="PublicDate"></param>
        /// <returns></returns>
        public string GetPublicDate(string Header, out DateTime PublicDate)
        {
            string RetVal = "";
            PublicDate = DateTime.MinValue;
            string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string m_Lines in l_Lines)
            {
                if (String.IsNullOrEmpty(RetVal) == false)
                    break;
                foreach (RegexRules m_RegexRule in l_RegexRulesPublicDate)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }
                    Match m_Match = Regex.Match(Header, m_RegexRule.RegexRuleText, m_RegexOptions);
                    if (m_Match.Success)
                    {
                        CultureInfo m_CultureInfo = new CultureInfo("en-US");
                        double MonthTemp;
                        if (double.TryParse(m_Match.Groups[2].Value, out MonthTemp) && double.TryParse(m_Match.Groups[4].Value, out MonthTemp))
                        {
                            RetVal = m_Match.Groups[0].Value;
                            int year, month, day;
                            day = int.Parse(m_Match.Groups[2].Value);
                            month = int.Parse(m_Match.Groups[4].Value);
                            year = int.Parse(m_Match.Groups[6].Value);
                            PublicDate = new DateTime(year, month, day);
                        }
                        else if (double.TryParse(m_Match.Groups[4].Value, out MonthTemp))
                        {
                            RetVal = m_Match.Groups[0].Value;
                            for (int index = 1; index <= 12; index++)
                            {
                                DateTime TempDateTime = new DateTime(2000, index, 15);
                                if (m_Match.Groups[2].Value.ToLower().Contains(TempDateTime.ToString("MMMM", m_CultureInfo).ToLower()))
                                {
                                    int year, day;
                                    day = int.Parse(m_Match.Groups[4].Value);
                                    year = int.Parse(m_Match.Groups[6].Value);
                                    PublicDate = new DateTime(year, index, day);
                                    break;
                                }
                            }
                        }
                        else if (double.TryParse(m_Match.Groups[2].Value, out MonthTemp))
                        {
                            RetVal = m_Match.Groups[0].Value;
                            for (int index = 1; index <= 12; index++)
                            {
                                DateTime TempDateTime = new DateTime(2000, index, 15);
                                if (m_Match.Groups[4].Value.ToLower().Contains(TempDateTime.ToString("MMMM", m_CultureInfo).ToLower()))
                                {
                                    int year, day;
                                    day = int.Parse(m_Match.Groups[2].Value);
                                    year = int.Parse(m_Match.Groups[6].Value);
                                    PublicDate = new DateTime(year, index, day);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// get publisher
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public string GetPublisher(string Header)
        {
            string RetVal = "";
            string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            if (IsNewDocument(Header) && l_Lines.Length > 0)
            {
                RetVal = l_Lines[0];
            }
            else
            {
                if (!IsVietnamese(Header))
                {
                    RetVal = l_Lines[0];
                }
                else
                {
                    foreach (string m_Lines in l_Lines)
                    {
                        if (String.IsNullOrEmpty(RetVal) == false)
                            break;
                        foreach (RegexRules m_RegexRule in l_RegexRulesPublisher)
                        {
                            RegexOptions m_RegexOptions = RegexOptions.None;
                            if (m_RegexRule.RegexRuleMultiline)
                            {
                                if (m_RegexRule.RegexRuleIgnoreCase)
                                {
                                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                                }
                                else
                                {
                                    m_RegexOptions = RegexOptions.Multiline;
                                }
                            }
                            else if (m_RegexRule.RegexRuleIgnoreCase)
                            {
                                m_RegexOptions = RegexOptions.IgnoreCase;
                            }
                            Match m_Match = Regex.Match(Header, m_RegexRule.RegexRuleText, m_RegexOptions);
                            if (m_Match.Success)
                            {
                                RetVal = m_Match.Groups[int.Parse(m_RegexRule.RegexRulesGroup)].Value;
                                int IndexOfNewline = RetVal.IndexOf(System.Environment.NewLine);
                                if (IndexOfNewline > 0)
                                {
                                    if (RetVal.Substring(IndexOfNewline - 1, 1) != " ")
                                    {
                                        RetVal = RetVal.Replace(System.Environment.NewLine, " ");
                                    }
                                    else
                                    {
                                        RetVal = RetVal.Replace(System.Environment.NewLine, "");
                                    }
                                }
                                else if (IndexOfNewline == 0)
                                {
                                    RetVal = RetVal.Replace(System.Environment.NewLine, "");
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return RetVal;
        }
        /// <summary>
        /// get Item Name
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public string GetItemName(string strLine, RegexRules m_RegexRule)
        {
            string RetVal = "";
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }
            Match m_Match = Regex.Match(strLine, m_RegexRule.RegexRuleText, m_RegexOptions);
            if (m_Match.Success)
            {
                RetVal = m_Match.Groups[0].Value;
                
            }
            return RetVal;
        }
        //=========================================================

        /// <summary>
        /// get publisher
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public string GetLawdocName(string Header, out string LawdocType, out string DocNote)
        {
            string RetVal = "";
            LawdocType = "";
            DocNote = "";
            string Publicsher = GetPublisher(Header);
            string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            if (IsNewDocument(Header))
            {
                bool IsStartName = false;
                bool IsDocName = false;
                bool IsEndName = false;
                DateTime PublicDate;
                foreach (string m_Lines in l_Lines)
                {
                    if (String.IsNullOrEmpty(m_Lines.Trim()) && string.IsNullOrEmpty(RetVal))
                        continue;
                    if (IsEndDocName(m_Lines, Publicsher))
                    {
                        if (IsDocName)
                        {
                            IsEndName = true;
                            IsDocName = false;
                        }
                    }
                    if (IsDocSaparator(m_Lines))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(GetPublicDate(m_Lines, out PublicDate)) == false)
                    {
                        if (string.IsNullOrEmpty(RetVal))
                        {
                            IsStartName = true;
                            IsDocName = true;
                            continue;
                        }
                    }
                    if (IsStartName)
                    {
                        LawdocType = m_Lines;
                        RetVal = m_Lines;
                        IsStartName = false;
                    }
                    else if (IsDocName)
                    {
                        RetVal += System.Environment.NewLine + m_Lines;
                    }
                    else if (IsEndName)
                    {
                        if (String.IsNullOrEmpty(DocNote))
                        {
                            DocNote = m_Lines;
                        }
                        else
                        {
                            DocNote += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
            }
            else
            {

                bool IsEndName = false;
                foreach (string m_Lines in l_Lines)
                {

                    if (IsEndDocName(m_Lines, Publicsher))
                    {
                        //RetVal += System.Environment.NewLine + "break " + Publicsher + ": " + m_Lines;
                        IsEndName = true;
                        //break;
                    }

                    if (!IsEndName)
                    {
                        if (String.IsNullOrEmpty(RetVal))
                        {
                            RetVal = m_Lines;
                            LawdocType = m_Lines;
                        }
                        else
                        {
                            RetVal += System.Environment.NewLine + m_Lines;
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(DocNote))
                        {
                            DocNote = m_Lines;
                        }
                        else
                        {
                            DocNote += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
                // english
                if (!IsVietnamese(Header))
                {
                    string DocNameNoNewLine = RetVal.Replace(System.Environment.NewLine, " ");
                    RegexOptions m_RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;
                    string RegexText = ConstantExtractors.RegexRulesLawdocTypeEn.Replace("cqbh", Publicsher);
                    Match m_Match = Regex.Match(DocNameNoNewLine, RegexText, m_RegexOptions);
                    if (m_Match.Success)
                    {
                        LawdocType = m_Match.Groups[2].Value.Trim();
                    }
                }
            }
            return RetVal;
        }
        //=========================================================

        /// <summary>
        /// get publisher
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public string _GetLawdocName(string Header, out string LawdocType, out string DocNote)
        {
            string RetVal = "";
            LawdocType = "";
            DocNote = "";
            string Publicsher = GetPublisher(Header);
            string[] l_Lines = Header.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            if (IsNewDocument(Header))
            {
                bool IsStartName = false;
                bool IsDocName = false;
                bool IsEndName = false;
                DateTime PublicDate;
                foreach (string m_Lines in l_Lines)
                {
                    if (String.IsNullOrEmpty(m_Lines.Trim()) && string.IsNullOrEmpty(RetVal))
                        continue;
                    if (IsEndDocName(m_Lines, Publicsher))
                    {
                        if (IsDocName)
                        {
                            IsEndName = true;
                            IsDocName = false;
                        }
                    }
                    if (IsDocSaparator(m_Lines))
                    {
                        continue;                        
                    }
                    if (string.IsNullOrEmpty(GetPublicDate(m_Lines, out PublicDate)) == false)
                    {
                        if (string.IsNullOrEmpty(RetVal))
                        {
                            IsStartName = true;
                            IsDocName = true;
                            continue;
                        }
                    }
                    if (IsStartName)
                    {
                        LawdocType = m_Lines;
                        RetVal = m_Lines;
                        IsStartName = false;
                    }
                    else if(IsDocName)
                    {
                        RetVal += System.Environment.NewLine + m_Lines;
                    }
                    else if (IsEndName)
                    {
                        if (String.IsNullOrEmpty(DocNote))
                        {
                            DocNote = m_Lines;
                        }
                        else
                        {
                            DocNote += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
            }
            else
            {
                
                bool IsEndName = false;
                foreach (string m_Lines in l_Lines)
                {
                    
                    if (IsEndDocName(m_Lines, Publicsher))
                    {
                        //RetVal += System.Environment.NewLine + "break " + Publicsher + ": " + m_Lines;
                        IsEndName = true;
                        //break;
                    }
                    
                    if (!IsEndName)
                    {
                        if (String.IsNullOrEmpty(RetVal))
                        {
                            RetVal = m_Lines;
                            LawdocType = m_Lines;
                        }
                        else
                        {
                            RetVal += System.Environment.NewLine + m_Lines;
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(DocNote))
                        {
                            DocNote = m_Lines;
                        }
                        else
                        {
                            DocNote += System.Environment.NewLine + m_Lines;
                        }
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return when a line is all upper case
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public bool IsAllUpper(string strLine)
        {
            bool RetVal = strLine.Equals(strLine.ToUpper(),StringComparison.Ordinal);
            if (!RetVal)
            {
                int WordUpperCount = 0;
                string[] l_Word = strLine.Split(new[] { " " }, StringSplitOptions.None);
                foreach (string m_Word in l_Word)
                {
                    if (m_Word.Equals(m_Word.ToUpper(), StringComparison.Ordinal))
                        WordUpperCount++;
                }
                if (l_Word.Length > 0 && WordUpperCount / l_Word.Length > 0.8)
                    RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return when a document is new style
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public bool IsNewDocument(string Header)
        {
            bool RetVal = false;

            RegexOptions m_RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            if (Regex.IsMatch(Header, ConstantExtractors.RegexRulesNewDocs, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return when a document is new style
        /// </summary>
        /// <param name="Header"></param>
        /// <returns></returns>
        public bool IsEndDocName(string strLine, string Publisher)
        {
            bool RetVal = false;
            if(String.IsNullOrEmpty(strLine.Trim()))
            {
                RetVal = true;
            }
            if (IsDocSaparator(strLine))
            {
                RetVal = true;
            }
            if (!RetVal && String.IsNullOrEmpty( Publisher.Trim()) == false)
            {
                string RegexRuleText = ConstantExtractors.RegexRulesEndDocName.Replace("cqbh", Publisher);
                RegexOptions m_RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;

                if (Regex.IsMatch(strLine, RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                }
            }
            
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return when a line is saparator (----)
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsDocSaparator(string strLine)
        {
            bool RetVal = false;

            RegexOptions m_RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;

            if (Regex.IsMatch(strLine, ConstantExtractors.RegexRulesSaparators, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsChildren(string strLine, out int IndexRule, out int IndexItem)
        {
            bool RetVal = false;
            IndexRule = -1;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesArticles)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexRule = RegexLevels.Article;
                    IndexItem = Index;
                    break;
                }
                Index++;
            }
            Index = 0;
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesChildrens)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        IndexRule = RegexLevels.Children;
                        IndexItem = Index;
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool HaveChildren(string Content, out RegexRules m_RegexRuleOut)
        {
            bool RetVal = false;
            m_RegexRuleOut = null;
            foreach (RegexRules m_RegexRule in l_RegexRulesArticles)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    m_RegexRuleOut = m_RegexRule;
                    break;
                }
            }
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesChildrens)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        m_RegexRuleOut = m_RegexRule;
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsChildren(string strLine, int IndexItem)
        {
            bool RetVal = false;
            RegexRules m_RegexRule = l_RegexRulesChildrens[IndexItem];
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }
            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsArticle(string strLine, out int IndexRule, out int IndexItem)
        {
            bool RetVal = false;
            IndexRule = -1;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesArticles)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexRule = RegexLevels.Article;
                    IndexItem = Index;
                    break;
                }
                Index++;
            }
            
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsArticle(string strLine, RegexRules m_RegexRule)
        {
            if (m_RegexRule == null || String.IsNullOrEmpty(strLine.Trim()))
                return false;
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }

            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }

            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsArticleDesc(string strLine)
        {
            bool RetVal = false;
            if (strLine.Length <= ConstantExtractors.MaxLengthOfArticleDesc)
            {
                RetVal = true;
            }

            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of article
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsParent(string strLine, out int IndexRule, out int IndexItem)
        {
            bool RetVal = false;
            IndexRule = -1;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesParts)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexRule = RegexLevels.Part;
                    IndexItem = Index;
                    break;
                }
                Index++;
            }
            Index = 0;
            // chapter
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesChapters)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        IndexRule = RegexLevels.Chapter;
                        IndexItem = Index;
                        break;
                    }
                }
            }
            // section
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesSections)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        IndexRule = RegexLevels.Section;
                        IndexItem = Index;
                        break;
                    }
                }
            }
            // sub section
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesSubSections)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        IndexRule = RegexLevels.SubSection;
                        IndexItem = Index;
                        break;
                    }
                }
            }
            // Parent
            if (!RetVal)
            {
                foreach (RegexRules m_RegexRule in l_RegexRulesParents)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }

                    if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                    {
                        RetVal = true;
                        IndexRule = RegexLevels.Parent;
                        IndexItem = Index;
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of article
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool HaveParent(string Content, out RegexRules m_RegexRulePart, out RegexRules m_RegexRuleChapter, out RegexRules m_RegexRuleSection, out RegexRules m_RegexRuleSubSection)
        {
            bool RetVal = false;
            m_RegexRulePart = null;
            m_RegexRuleChapter = null;
            m_RegexRuleSection = null;
            m_RegexRuleSubSection = null;
            foreach (RegexRules m_RegexRule in l_RegexRulesParts)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    m_RegexRulePart = m_RegexRule;
                    break;
                }
            }
            // chapter
            foreach (RegexRules m_RegexRule in l_RegexRulesChapters)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    m_RegexRuleChapter = m_RegexRule;
                    break;
                }
            }
            // section
            foreach (RegexRules m_RegexRule in l_RegexRulesSections)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    m_RegexRuleSection = m_RegexRule;
                    break;
                }
            }
            // sub section
            foreach (RegexRules m_RegexRule in l_RegexRulesSubSections)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    m_RegexRuleSubSection = m_RegexRule;
                    break;
                }
            }
            // Parent
            foreach (RegexRules m_RegexRule in l_RegexRulesParents)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(Content, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    if (m_RegexRulePart == null)
                    {
                        m_RegexRulePart = m_RegexRule;
                    }
                    else if (m_RegexRuleChapter == null)
                    {
                        m_RegexRuleChapter = m_RegexRule;
                    }
                    else if (m_RegexRuleSection == null)
                    {
                        m_RegexRuleSection = m_RegexRule;
                    }
                    else if (m_RegexRuleSubSection == null)
                    {
                        m_RegexRuleSubSection = m_RegexRule;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of children node (Article node) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsParent(string strLine, int IndexItem)
        {
            bool RetVal = false;
            RegexRules m_RegexRule = l_RegexRulesParents[IndexItem];
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }
            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Part) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsPart(string strLine, out int IndexItem)
        {
            bool RetVal = false;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesParts)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexItem = Index;
                    break;
                }
                Index++;
            }
            
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Part) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsPart(string strLine, RegexRules m_RegexRule)
        {
            if (m_RegexRule == null || String.IsNullOrEmpty(strLine.Trim()))
                return false;
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }

            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }

            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of part
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsPartDesc(string strLine)
        {
            bool RetVal = false;
            if (strLine.Length <= ConstantExtractors.MaxLengthOfPartDesc)
            {
                RetVal = true;
            }
            if (IsAllUpper(strLine))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Chapter) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsChapter(string strLine, out int IndexItem)
        {
            bool RetVal = false;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesChapters)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexItem = Index;
                    break;
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Chapter) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsChapter(string strLine, RegexRules m_RegexRule)
        {
            if (m_RegexRule == null || String.IsNullOrEmpty(strLine.Trim()))
                return false;
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }

            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of chapter
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsChapterDesc(string strLine)
        {
            bool RetVal = false;
            if (strLine.Length <= ConstantExtractors.MaxLengthOfChapterDesc)
            {
                RetVal = true;
            }
            if (IsAllUpper(strLine))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Section) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSection(string strLine, out int IndexItem)
        {
            
            bool RetVal = false;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesSections)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexItem = Index;
                    break;
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Section) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSection(string strLine, RegexRules m_RegexRule)
        {
            if (m_RegexRule == null || String.IsNullOrEmpty(strLine.Trim()))
                return false;
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }

            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of section
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSectionDesc(string strLine)
        {
            bool RetVal = false;
            if (strLine.Length <= ConstantExtractors.MaxLengthOfSectionDesc)
            {
                RetVal = true;
            }
            if (IsAllUpper(strLine))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Sub Section) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSubSection(string strLine, out int IndexItem)
        {
            
            bool RetVal = false;
            IndexItem = -1;
            int Index = 0;
            foreach (RegexRules m_RegexRule in l_RegexRulesSubSections)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexItem = Index;
                    break;
                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of parent node (Sub Section) with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSubSection(string strLine, RegexRules m_RegexRule)
        {
            if (m_RegexRule == null || String.IsNullOrEmpty(strLine.Trim()))
                return false;
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            if (m_RegexRule.RegexRuleMultiline)
            {
                if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                }
                else
                {
                    m_RegexOptions = RegexOptions.Multiline;
                }
            }
            else if (m_RegexRule.RegexRuleIgnoreCase)
            {
                m_RegexOptions = RegexOptions.IgnoreCase;
            }

            if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is description of sub section
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public bool IsSubSectionDesc(string strLine)
        {
            bool RetVal = false;
            if (strLine.Length <= ConstantExtractors.MaxLengthOfSubSectionDesc)
            {
                RetVal = true;
            }
            if (IsAllUpper(strLine))
            {
                RetVal = true;
            }
            return RetVal;
        }
        //=========================================================
       /// <summary>
        /// return a line is start of footer node or end of content node with index of RegexRule match
       /// </summary>
       /// <param name="strLine"></param>
       /// <param name="IndexRule"></param>
       /// <param name="IndexItem"></param>
       /// <returns></returns>
        public bool IsFooter(string strLine, out int IndexRule, out int IndexItem)
        {
            bool RetVal = false;
            int Index = 0;
            IndexRule = 0;
            IndexItem = Index;
            foreach (RegexRules m_RegexRule in l_RegexRulesFooter)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }

                if (Regex.IsMatch(strLine, m_RegexRule.RegexRuleText, m_RegexOptions))
                {
                    RetVal = true;
                    IndexRule = 0;
                    IndexItem = Index;
                    break;
                }
                Index++;
            }
            
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of footer node or end of content node with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <param name="IndexRule"></param>
        /// <param name="IndexItem"></param>
        /// <returns></returns>
        public bool IsAppendices(string FileContent, ref List<string> l_AppendixName)
        {
            bool RetVal = false;
            string[] l_Lines = FileContent.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            l_AppendixName = new List<string>();
            for (int Index = 0; Index < l_Lines.Length; Index++)
            {
                string m_Line = l_Lines[Index];
                foreach (RegexRules m_RegexRule in l_RegexRulesAppendices)
                {
                    RegexOptions m_RegexOptions = RegexOptions.None;
                    if (m_RegexRule.RegexRuleMultiline)
                    {
                        if (m_RegexRule.RegexRuleIgnoreCase)
                        {
                            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                        }
                        else
                        {
                            m_RegexOptions = RegexOptions.Multiline;
                        }
                    }
                    else if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.IgnoreCase;
                    }
                    Match m_Match = Regex.Match(m_Line, m_RegexRule.RegexRuleText, m_RegexOptions);
                    if (m_Match.Success)
                    {
                        string AppendixName = m_Match.Groups[0].Value;
                        string AppendixNameTemp = "";
                        int IndexEnd = 1;
                        for (IndexEnd = 1; IndexEnd <= 6 && Index + IndexEnd < l_Lines.Length; IndexEnd++)
                        {
                            if (IsEndOfAppendices(l_Lines[Index + IndexEnd], out AppendixNameTemp))
                            {
                                AppendixName += System.Environment.NewLine + l_Lines[Index + IndexEnd];
                                l_AppendixName.Add(AppendixName);
                                RetVal = true;
                                Index += IndexEnd;
                                break;
                            }
                            else
                            {
                                AppendixName += System.Environment.NewLine + l_Lines[Index + IndexEnd];
                            }

                        }
                        if (!RetVal)
                            Index += IndexEnd;
                    }

                }
            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of footer node or end of content node with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <param name="IndexRule"></param>
        /// <param name="IndexItem"></param>
        /// <returns></returns>
        public bool IsEndOfAppendices(string strLine, out string AppendixName)
        {
            bool RetVal = false;
            AppendixName = "";
            foreach (RegexRules m_RegexRule in l_RegexRulesAppendices2)
            {
                RegexOptions m_RegexOptions = RegexOptions.None;
                if (m_RegexRule.RegexRuleMultiline)
                {
                    if (m_RegexRule.RegexRuleIgnoreCase)
                    {
                        m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    }
                    else
                    {
                        m_RegexOptions = RegexOptions.Multiline;
                    }
                }
                else if (m_RegexRule.RegexRuleIgnoreCase)
                {
                    m_RegexOptions = RegexOptions.IgnoreCase;
                }
                Match m_Match = Regex.Match(strLine, m_RegexRule.RegexRuleText, m_RegexOptions);
                if (m_Match.Success)
                {
                    AppendixName = m_Match.Groups[0].Value;
                    RetVal = true;
                    break;
                }

            }
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of footer node or end of content node with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <param name="IndexRule"></param>
        /// <param name="IndexItem"></param>
        /// <returns></returns>
        public bool IsVietnamese(string FileContent)
        {
            bool RetVal = false;
            RegexOptions m_RegexOptions = RegexOptions.None;
            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            RetVal = Regex.IsMatch(FileContent, ConstantExtractors.RegexRulesVietnames, m_RegexOptions);
            
            return RetVal;
        }
        //=========================================================
        /// <summary>
        /// return a line is start of footer node or end of content node with index of RegexRule match
        /// </summary>
        /// <param name="strLine"></param>
        /// <param name="IndexRule"></param>
        /// <param name="IndexItem"></param>
        /// <returns></returns>
        public static bool GetBullet(string m_Line, out string Bullet)
        {
            bool RetVal = false;
            Bullet = "";
            RegexOptions m_RegexOptions = RegexOptions.None;
            m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            Match m_Match = Regex.Match(m_Line, ConstantExtractors.RegexRulesBullet, m_RegexOptions);
            if (m_Match.Success)
            {
                RetVal = true;
                Bullet = m_Match.Groups[0].Value;
            }
            return RetVal;
        }
        #endregion
    }

}
