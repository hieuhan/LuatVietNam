using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using NetOffice.WordApi;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;

namespace ICSoft.ExtractWordLib
{
    public class WordReaders
    {
        static List<Word.Range> l_TablesRanges;
        static List<Word.Table> l_Tables;
        public static string ReadFileContentPlanText(string path, out string Error)
        {
            string RetVal = "";
            List<string> l_HtmlTable = new List<string>();
            if (ConstantExtractors.IsProcessTable > 0)
            {
                l_HtmlTable= GetHtmlTable(path);
            }
            Word.Application wordApp = new Word.Application();
            
            object file = path;
            object Confirm = false;
            object Readonly = true;
            Error = "";
            Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, false);
            try
            {
                
                
               
                //===================
                StringBuilder m_StringBuilder = new StringBuilder();
                
               // init table
                Extractors m_Extractors = new Extractors();
                if (ConstantExtractors.IsProcessTable > 0)
                {
                    l_Tables = new List<Table>();
                    l_TablesRanges = new List<Range>();
                    for (int iCounter = 1; iCounter <= doc.Tables.Count; iCounter++)
                    {
                        Word.Range TRange = doc.Tables[iCounter].Range;
                        // ignore header table for new document
                        //if (m_Extractors.IsNewDocument(TRange.Text))
                        //    continue;
                        l_TablesRanges.Add(TRange);
                        l_Tables.Add(doc.Tables[iCounter]);
                    }
                }
                //========= read by paragraph ==============================================
               
                m_StringBuilder = new StringBuilder();
                int CurentTableIndex = -1;
                int ProcessedTableIndex = -1;
                foreach (Word.Paragraph m_Paragraph in doc.Paragraphs)
                {
                   
                    string strParagraph = "";
                    if (ConstantExtractors.IsProcessTable > 0)
                    {
                        if (IsInTatble(m_Paragraph, out CurentTableIndex) == false)
                        {
                            strParagraph = m_Paragraph.Range.Text;
                            strParagraph = Converter.TCVN3ToUnicode(strParagraph, m_Paragraph.Range.Font.Name);
                            strParagraph = Regex.Replace(strParagraph, @"[\x0B]", System.Environment.NewLine);
                            strParagraph = Regex.Replace(strParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                            if (String.IsNullOrEmpty(strParagraph.Trim()) && String.IsNullOrEmpty(m_StringBuilder.ToString()))
                                continue;
                            m_StringBuilder.AppendLine(strParagraph);
                        }
                        else
                        {
                            if (ProcessedTableIndex != CurentTableIndex && CurentTableIndex >= 0 && CurentTableIndex < l_Tables.Count && CurentTableIndex < l_HtmlTable.Count)
                            {
                                Word.Table m_Table = l_Tables[CurentTableIndex];
                                if (m_Extractors.IsNewDocument(m_Table.Range.Text))
                                {
                                    foreach (Paragraph m_ParagraphTemp in m_Table.Range.Paragraphs)
                                    {
                                        strParagraph = m_ParagraphTemp.Range.Text;
                                        strParagraph = Converter.TCVN3ToUnicode(strParagraph, m_Paragraph.Range.Font.Name);
                                        strParagraph = Regex.Replace(strParagraph, @"[\x0B]", System.Environment.NewLine);
                                        strParagraph = Regex.Replace(strParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                                        m_StringBuilder.AppendLine(strParagraph);
                                    }
                                }
                                else
                                {
                                    m_StringBuilder.AppendLine("");
                                    strParagraph = l_HtmlTable[CurentTableIndex];
                                    m_StringBuilder.AppendLine(strParagraph);
                                    m_StringBuilder.AppendLine("");
                                }
                                ProcessedTableIndex = CurentTableIndex;
                            }

                        }
                    }
                    else
                    {
                        strParagraph = m_Paragraph.Range.Text;
                        strParagraph = Converter.TCVN3ToUnicode(strParagraph, m_Paragraph.Range.Font.Name);
                        strParagraph = Regex.Replace(strParagraph, @"[\x0B]", System.Environment.NewLine);
                        strParagraph = Regex.Replace(strParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                        if (String.IsNullOrEmpty(strParagraph.Trim()) && String.IsNullOrEmpty(m_StringBuilder.ToString()))
                            continue;
                        m_StringBuilder.AppendLine(strParagraph);
                    }
                }
                RetVal = m_StringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                doc.Close(false);
                wordApp.Quit(false);
            }
            return RetVal;
        }
        //==================================================
        /// <summary>
        /// Save doc to html and insert bookmark, return the menu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static List<WordBookmarks> SaveAsHtml(string path, out string Error, string PathSaveAs = "", string ContentFileInput = "")
        {
            List<WordBookmarks> RetVal = new List<WordBookmarks>();
            Extractors m_Extractors = new Extractors();
            List<Appendices> l_AppendicesTemp = new List<Appendices>();
            string ContentFile = "";
            string Content, Header, Footer;
            if (String.IsNullOrEmpty(ContentFileInput))
            {
                ContentFile = ReadFileContentPlanText(path, out Error);
                m_Extractors.SplitContent(ContentFile, out Header, out Content, out Footer, ref l_AppendicesTemp);
            }
            else
            {
                ContentFile = ContentFileInput;
                m_Extractors.SplitContent(ContentFile, out Header, out Content, out Footer, ref l_AppendicesTemp);
            }
            
            List<string> l_AppandixName = new List<string>();
            int IndexAppendix = 0;
            bool IsHaveAppendix = m_Extractors.IsAppendices(ContentFile, ref l_AppandixName);
            bool IsTCVN3 = false;
            Word.Application wordApp = new Word.Application();
            object file = path;
            object Confirm = false;
            object Readonly = false;
            object AddToRecentFile = false;
            Error = "";
            Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, AddToRecentFile);
            string outputFileName = file + ".html";
            if (PathSaveAs != "")
            {
                outputFileName = PathSaveAs;
            }
            try
            {
                
                //===================
                if (ConstantExtractors.IsProcessTable > 0)
                {
                    l_Tables = new List<Table>();
                    l_TablesRanges = new List<Range>();
                    for (int iCounter = 1; iCounter <= doc.Tables.Count; iCounter++)
                    {
                        Word.Range TRange = doc.Tables[iCounter].Range;
                        // ignore header table for new document
                        if (m_Extractors.IsNewDocument(TRange.Text))
                            continue;
                        
                        l_TablesRanges.Add(TRange);
                        l_Tables.Add(doc.Tables[iCounter]);
                    }
                }
                //========= read by paragraph ==============================================
                int ParentIndex = 1;
                int ChildrentIndex = 1;
                int IndexRule = -1;
                int IndexItem = -1;
                int IndexRuleTemp = -1;
                int IndexItemTemp = -1;
                int IndexParagraph = -1;
                string BoomarkName = "";
                string ParagraphText = "";
                string MenuName = "";
                bool IsCheckParent = false, IsCheckChildren = false;
                bool IsFooter = false;
                int IndexTable = -1;
                RegexRules m_RegexRulesPart, m_RegexRulesChapter, m_RegexRulesSection, m_RegexRulesSubSection, m_RegexRulesArticle;
                IsCheckParent = m_Extractors.HaveParent(Content, out m_RegexRulesPart, out m_RegexRulesChapter, out m_RegexRulesSection, out m_RegexRulesSubSection);

                IsCheckChildren = m_Extractors.HaveChildren(Content, out m_RegexRulesArticle);
                object oMissing = System.Reflection.Missing.Value;
                foreach (Word.Paragraph m_Paragraph in doc.Paragraphs)
                {
                    try
                    {
                        if (IsInTatble(m_Paragraph, out IndexTable))
                            continue;
                        ParagraphText = m_Paragraph.Range.Text;
                        ParagraphText = Converter.TCVN3ToUnicode(ParagraphText, m_Paragraph.Range.Font.Name);                        
                        if (String.IsNullOrEmpty(ParagraphText.Trim()))
                            continue;
                        if (IsTCVN3 == false)
                            IsTCVN3 = Converter.IsTCVN3(m_Paragraph.Range.Font.Name);
                        if (IsTCVN3)// not good: lose center align
                        {

                            //ParagraphFormat m_ParagraphFormatOld = m_Paragraph.Format.Duplicate;
                            //ParagraphFormat m_ParagraphTextFormatOld = m_Paragraph.Range.FormattedText.ParagraphFormat.Duplicate;
                            //WdParagraphAlignment m_WdParagraphAlignmentOld = m_Paragraph.Format.Alignment;
                            // set new font and text
                            m_Paragraph.Range.Font.Name = ConstantExtractors.UnicodeFont;
                            m_Paragraph.Range.Text = ParagraphText;
                            
                            //==========================
                            //FindAndReplace(wordApp, m_Paragraph.Range.Text, ParagraphText);
                            //==================================
                            //m_Paragraph.Format = m_ParagraphFormatOld;
                            //m_Paragraph.Format.Alignment = m_WdParagraphAlignmentOld;
                            //m_Paragraph.Range.ParagraphFormat.SpaceAfter = 1;
                            //m_Paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            //m_Paragraph.Range.FormattedText.ParagraphFormat = m_ParagraphTextFormatOld;
                        }     
                        ParagraphText = Regex.Replace(ParagraphText, @"[\x0B]", System.Environment.NewLine);
                        ParagraphText = Regex.Replace(ParagraphText, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");

                        if (String.IsNullOrEmpty(ParagraphText.Trim()))
                        {
                            IndexParagraph++;
                            continue;
                        }
                        if (IsFooter == false)
                            IsFooter = m_Extractors.IsFooter(ParagraphText, out IndexRule, out IndexItem);
                        //================================================================
                        if (IsFooter == false)
                        {
                            if (m_Extractors.IsPart(ParagraphText, m_RegexRulesPart))
                            {
                                BoomarkName = "Parent_" + RegexLevels.Part.ToString() + "_" + ParentIndex.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfChapterDesc, "...");
                                if (MenuName.Length < ConstantExtractors.MinLengthOfPartDesc)
                                {
                                    Word.Paragraph m_ParagraphNext = m_Paragraph.Next();
                                    if (m_ParagraphNext == null)
                                        break;
                                    string NextParagraph = m_ParagraphNext.Range.Text;
                                    NextParagraph = Converter.TCVN3ToUnicode(NextParagraph, m_ParagraphNext.Range.Font.Name);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\x0B]", System.Environment.NewLine);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                                    if (m_Extractors.IsParent(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false && m_Extractors.IsChildren(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false)
                                    {
                                        MenuName += " " + NextParagraph;
                                    }
                                    else
                                    {
                                        m_ParagraphNext.Previous();

                                    }

                                }
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                ParentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = RegexLevels.Part;
                                RetVal.Add(m_WordBookmarks);
                            } //end part
                            //is chapter
                            else if (m_Extractors.IsChapter(ParagraphText, m_RegexRulesChapter))
                            {
                                BoomarkName = "Parent_" + RegexLevels.Chapter.ToString() + "_" + ParentIndex.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfChapterDesc, "...");
                                if (MenuName.Length < ConstantExtractors.MinLengthOfPartDesc)
                                {
                                    Word.Paragraph m_ParagraphNext = m_Paragraph.Next();
                                    if (m_ParagraphNext == null)
                                        break;
                                    string NextParagraph = m_ParagraphNext.Range.Text;
                                    NextParagraph = Converter.TCVN3ToUnicode(NextParagraph, m_ParagraphNext.Range.Font.Name);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\x0B]", System.Environment.NewLine);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                                    if (m_Extractors.IsParent(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false && m_Extractors.IsChildren(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false)
                                    {
                                        MenuName += " " + NextParagraph;
                                    }
                                    else
                                    {
                                        m_ParagraphNext.Previous();

                                    }

                                }
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                ParentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = RegexLevels.Chapter;
                                RetVal.Add(m_WordBookmarks);
                            }
                            // end chapter
                            // is section 
                            else if (m_Extractors.IsSection(ParagraphText, m_RegexRulesSection))
                            {
                                BoomarkName = "Parent_" + RegexLevels.Section.ToString() + "_" + ParentIndex.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfChapterDesc, "...");
                                if (MenuName.Length < ConstantExtractors.MinLengthOfPartDesc)
                                {
                                    Word.Paragraph m_ParagraphNext = m_Paragraph.Next();
                                    if (m_ParagraphNext == null)
                                        break;
                                    string NextParagraph = m_ParagraphNext.Range.Text;
                                    NextParagraph = Converter.TCVN3ToUnicode(NextParagraph, m_ParagraphNext.Range.Font.Name);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\x0B]", System.Environment.NewLine);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                                    if (m_Extractors.IsParent(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false && m_Extractors.IsChildren(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false)
                                    {
                                        MenuName += " " + NextParagraph;
                                    }
                                    else
                                    {
                                        m_ParagraphNext.Previous();

                                    }

                                }
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                ParentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = RegexLevels.Section;
                                RetVal.Add(m_WordBookmarks);
                            }
                            // end section
                            // is sub section 
                            else if (m_Extractors.IsSubSection(ParagraphText, m_RegexRulesSubSection))
                            {
                                BoomarkName = "Parent_" + RegexLevels.SubSection.ToString() + "_" + ParentIndex.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfChapterDesc, "...");
                                if (MenuName.Length < ConstantExtractors.MinLengthOfPartDesc)
                                {
                                    Word.Paragraph m_ParagraphNext = m_Paragraph.Next();
                                    if (m_ParagraphNext == null)
                                        break;
                                    string NextParagraph = m_ParagraphNext.Range.Text;
                                    NextParagraph = Converter.TCVN3ToUnicode(NextParagraph, m_ParagraphNext.Range.Font.Name);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\x0B]", System.Environment.NewLine);
                                    NextParagraph = Regex.Replace(NextParagraph, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                                    if (m_Extractors.IsParent(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false && m_Extractors.IsChildren(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false)
                                    {
                                        MenuName += " " + NextParagraph;
                                    }
                                    else
                                    {
                                        m_ParagraphNext.Previous();

                                    }

                                }
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                ParentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = RegexLevels.SubSection;
                                RetVal.Add(m_WordBookmarks);
                            }
                            // end sub section                    
                            // is Article
                            else if (m_Extractors.IsArticle(ParagraphText, m_RegexRulesArticle))
                            {
                                BoomarkName = "Children_" + RegexLevels.Article.ToString() + "_" + ChildrentIndex.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfArticleDesc, "...");
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                ChildrentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = RegexLevels.Article;
                                RetVal.Add(m_WordBookmarks);
                            }
                        }
                        else if (IsHaveAppendix && l_AppandixName.Count > IndexAppendix && String.IsNullOrEmpty(ParagraphText) == false)
                        {
                            string FirtLineAppendixName = l_AppandixName[IndexAppendix].Split(new[] { System.Environment.NewLine }, StringSplitOptions.None)[0];
                            if (FirtLineAppendixName == ParagraphText)
                            {
                                BoomarkName = "Appendix_" + (IndexAppendix + 1).ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(l_AppandixName[IndexAppendix], ConstantExtractors.MaxLengthOfAppendix, "...");
                                m_Paragraph.Range.Bookmarks.Add(BoomarkName);
                                IndexAppendix++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = 10;
                                RetVal.Add(m_WordBookmarks);
                            }
                        }
                        IndexParagraph++;
                        
                    }
                    catch (Exception ex)
                    {
                        IndexParagraph++;
                        sms.utils.LogFiles.LogError(ex.ToString());
                        continue;
                    }
                }
                
                // save as html 
                WdSaveFormat format = WdSaveFormat.wdFormatFilteredHTML;
                object fileFormat = format;
                
                object SaveChange = false;
                
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }
                doc.SaveAs(outputFileName,
                           fileFormat, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing);
                
                
               
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                doc.Close(false);
                wordApp.Quit(false);
            }
            // reopen to convert to unicode
            if (File.Exists(outputFileName))
            {
                string html = System.IO.File.ReadAllText(outputFileName);
                html = System.Net.WebUtility.HtmlDecode(html);
                HtmlDocument m_HtmlDocument = new HtmlDocument();
                m_HtmlDocument.LoadHtml(html);
                html = "";
                foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("/html/head/style"))
                {
                    html +=  m_HtmlNode.OuterHtml;
                }
                foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("/html/body"))
                {
                    html += m_HtmlNode.InnerHtml;
                }

                File.WriteAllText(outputFileName, html, Encoding.UTF8);
            }
                
            return RetVal;
        }

         //==================================================
        /// <summary>
        /// Save doc to html and insert bookmark, return the menu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static List<WordBookmarks> ReadAsHtml(string path, int DocId, out string Error, out string htmlContent, string PathSaveAs = "", string ContentFileInput = "", List<string> l_PathSaveAs = null)
        {
            List<WordBookmarks> RetVal = new List<WordBookmarks>();
            Extractors m_Extractors = new Extractors();
            List<Appendices> l_AppendicesTemp = new List<Appendices>();
            string ContentFile = "";
            htmlContent = "";
            string Content, Header, Footer;
            if (String.IsNullOrEmpty(ContentFileInput))
            {
                ContentFile = ReadFileContentPlanText(path, out Error);
                m_Extractors.SplitContent(ContentFile, out Header, out Content, out Footer, ref l_AppendicesTemp);
            }
            else
            {
                ContentFile = ContentFileInput;
                m_Extractors.SplitContent(ContentFile, out Header, out Content, out Footer, ref l_AppendicesTemp);
            }

            List<string> l_AppandixName = new List<string>();
            int IndexAppendix = 0;
            bool IsHaveAppendix = false;// m_Extractors.IsAppendices(ContentFile, ref l_AppandixName);
            bool IsTCVN3 = false;
            Word.Application wordApp = new Word.Application();
            object file = path;
            object Confirm = false;
            object Readonly = false;
            object AddToRecentFile = false;
            Error = "";
            Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, AddToRecentFile);
            string outputFileName = file + ".html";
            
            int ParentIndex = 1;
            int ChildrentIndex = 1;
            int IndexRule = -1;
            int IndexItem = -1;
            int IndexRuleTemp = -1;
            int IndexItemTemp = -1;
            int IndexParagraph = -1;
            string BoomarkName = "";
            string ParagraphText = "";
            string MenuName = "";
            bool IsCheckParent = false, IsCheckChildren = false;
            bool IsFooter = false;
            int IndexTable = -1;
            RegexRules m_RegexRulesPart, m_RegexRulesChapter, m_RegexRulesSection, m_RegexRulesSubSection, m_RegexRulesArticle;
            IsCheckParent = m_Extractors.HaveParent(ContentFile, out m_RegexRulesPart, out m_RegexRulesChapter, out m_RegexRulesSection, out m_RegexRulesSubSection);

            IsCheckChildren = m_Extractors.HaveChildren(ContentFile, out m_RegexRulesArticle);
            if (PathSaveAs != "")
            {
                outputFileName = PathSaveAs;
            }
            try
            {
                l_Tables = new List<Table>();
                l_TablesRanges = new List<Range>();
                //===================
                if (ConstantExtractors.IsProcessTable > 0)
                {
                   
                    for (int iCounter = 1; iCounter <= doc.Tables.Count; iCounter++)
                    {
                        Word.Range TRange = doc.Tables[iCounter].Range;
                        // ignore header table for new document
                        if (m_Extractors.IsNewDocument(TRange.Text))
                            continue;
                        
                        l_TablesRanges.Add(TRange);
                        l_Tables.Add(doc.Tables[iCounter]);
                    }
                }
                //========= read by paragraph ==============================================
               
                
                
                object oMissing = System.Reflection.Missing.Value;
                foreach (Word.Paragraph m_Paragraph in doc.Paragraphs)
                {
                    try
                    {
                        if (IsInTatble(m_Paragraph, out IndexTable))
                            continue;
                        ParagraphText = m_Paragraph.Range.Text;
                                                
                        if (String.IsNullOrEmpty(ParagraphText.Trim()))
                            continue;
                        if (IsTCVN3 == false)
                            IsTCVN3 = Converter.IsTCVN3(m_Paragraph.Range.Font.Name);
                        if (IsTCVN3)// not good: lose center align
                        {
                            ParagraphText = Converter.TCVN3ToUnicode(ParagraphText, m_Paragraph.Range.Font.Name);

                            m_Paragraph.Range.Font.Name = ConstantExtractors.UnicodeFont;

                        }
                        ParagraphText = Regex.Replace(ParagraphText, @"[\x0B]", System.Environment.NewLine);
                        ParagraphText = Regex.Replace(ParagraphText, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");

                        if (String.IsNullOrEmpty(ParagraphText.Trim()))
                        {
                            IndexParagraph++;
                            continue;
                        }
                        if (IsFooter == false)
                            IsFooter = m_Extractors.IsFooter(ParagraphText, out IndexRule, out IndexItem);
                        //================================================================
                        
                    }
                    catch (Exception ex)
                    {
                        IndexParagraph++;
                        sms.utils.LogFiles.LogError(ex.ToString());
                        continue;
                    }
                }
                
                // save as html 
                WdSaveFormat format = WdSaveFormat.wdFormatFilteredHTML;
                object fileFormat = format;
                
                object SaveChange = false;
                
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }

                doc.SaveAs(outputFileName,
                           fileFormat, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing);
                
                
               
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                doc.Close(false);
                wordApp.Quit(false);
            }
            // reopen to convert to unicode
            if (File.Exists(outputFileName))
            {
                string html = "";// System.IO.File.ReadAllText(outputFileName, UTF8Encoding.UTF8);
                //sms.utils.LogFiles.WriteLog(html);
                //html = System.Net.WebUtility.HtmlDecode(html);
                HtmlDocument m_HtmlDocument = new HtmlDocument();
                m_HtmlDocument.Load(outputFileName, UTF8Encoding.UTF8);
                HtmlNodeCollection l_NodePTest = m_HtmlDocument.DocumentNode.SelectNodes("//p");
                bool isReload = false;
                if (l_NodePTest != null)
                {
                    for (int indexNode = 0; indexNode < l_NodePTest.Count; indexNode++)
                    {
                        HtmlNode m_HtmlNode = l_NodePTest[indexNode];
                        if (indexNode >= 10)
                            break;
                        if (m_HtmlNode.InnerText.Contains("�"))
                        {
                            isReload = true;
                            break;
                        }
                    }
                }
                if (isReload)
                {
                    m_HtmlDocument.Load(outputFileName);
                }
                IndexParagraph = 0;
                short indexBookmack = 0;
                HtmlNodeCollection l_NodeP = m_HtmlDocument.DocumentNode.SelectNodes("//p");
                if(l_NodeP != null)
                {
                    for (int indexNode = 0; indexNode < l_NodeP.Count; indexNode++)
                    {
                        HtmlNode m_HtmlNode = l_NodeP[indexNode];
                        try
                        {


                            ParagraphText = System.Net.WebUtility.HtmlDecode(m_HtmlNode.InnerText);
                            ParagraphText = ParagraphText.Replace(Environment.NewLine, " ");
                            if (String.IsNullOrEmpty(ParagraphText.Trim()))
                                continue;
                            //sms.utils.LogFiles.LogError(System.Net.WebUtility.HtmlDecode(m_HtmlNode.InnerHtml));
                            m_HtmlNode.InnerHtml = System.Net.WebUtility.HtmlDecode(m_HtmlNode.InnerHtml);
                            m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml.Replace(Environment.NewLine, " ").Trim();
                            m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml.Replace("\r", " ").Trim();
                            m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml.Replace("\n", " ").Trim();
                            //sms.utils.LogFiles.LogError(ParagraphText);
                            //sms.utils.LogFiles.LogError(m_HtmlNode.InnerHtml);
                            //================================================================

                            string bookmarkClass = "demuc1";
                            bool isPart = false, IsChapter = false, IsSection = false, IsSubSection = false, IsArticle = false;
                            isPart = m_Extractors.IsPart(ParagraphText, m_RegexRulesPart);
                            if (!isPart)
                            {
                                bookmarkClass = "demuc2";
                                IsChapter = m_Extractors.IsChapter(ParagraphText, m_RegexRulesChapter);
                                
                            }

                            if (!IsChapter && !isPart)
                            {
                                bookmarkClass = "demuc3";
                                IsSection = m_Extractors.IsSection(ParagraphText, m_RegexRulesSection);
                            }
                            if (!IsChapter && !isPart && !IsSection)
                            {
                                bookmarkClass = "demuc4";
                                IsSubSection = m_Extractors.IsSubSection(ParagraphText, m_RegexRulesSubSection);
                            }
                            if (!IsChapter && !isPart && !IsSection && !IsSubSection)
                            {
                                bookmarkClass = "demuc4";
                                IsArticle = m_Extractors.IsArticle(ParagraphText, m_RegexRulesArticle);
                            }
                            if (isPart || IsChapter || IsSection || IsSubSection || IsArticle)
                            {
                                BoomarkName = "demuc" + DocId.ToString() + indexBookmack.ToString();
                                MenuName = ExtractorHelpers.CutStringByWord(ParagraphText, ConstantExtractors.MaxLengthOfChapterDesc, "...");

                                // if not is article check content in next p
                                if (MenuName.Length < ConstantExtractors.MinLengthOfPartDesc && IsArticle == false)
                                {
                                    indexNode++;
                                    if (indexNode >= l_NodeP.Count)
                                        break;
                                    HtmlNode m_HtmlNodeNext = l_NodeP[indexNode];
                                    if (m_HtmlNodeNext == null)
                                        break;
                                    string NextParagraph = "";
                                    NextParagraph = System.Net.WebUtility.HtmlDecode(m_HtmlNodeNext.InnerText);
                                    NextParagraph = NextParagraph.Replace(Environment.NewLine, " ").Trim();

                                    if (m_Extractors.IsParent(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false && m_Extractors.IsChildren(NextParagraph, out IndexRuleTemp, out IndexItemTemp) == false)
                                    {
                                        string styleAtt = "";
                                        if (m_HtmlNodeNext.Attributes["style"] != null)
                                            styleAtt = m_HtmlNodeNext.Attributes["style"].Value;
                                        m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml + "<p style='" + styleAtt + "'>" + m_HtmlNodeNext.InnerHtml + "</p>";
                                        m_HtmlNodeNext.Remove();
                                        MenuName += " " + NextParagraph;
                                    }
                                    else
                                    {
                                        indexNode--;

                                    }

                                }
                                m_HtmlNode.InnerHtml = "<span class=\"" + bookmarkClass + "\" id=\"" + BoomarkName + "\">" + m_HtmlNode.InnerHtml + "</span>";
                                ParentIndex++;
                                WordBookmarks m_WordBookmarks = new WordBookmarks();
                                m_WordBookmarks.BookmarkName = BoomarkName;
                                m_WordBookmarks.BookmarkDesc = MenuName;
                                m_WordBookmarks.BookmarkLevel = indexBookmack;
                                RetVal.Add(m_WordBookmarks);
                                indexBookmack++;
                            } //end part
                            IndexParagraph++;

                        }
                        catch (Exception ex)
                        {
                            IndexParagraph++;
                            sms.utils.LogFiles.LogError(ex.ToString());
                            continue;
                        }

                    }
                }
                if(m_HtmlDocument.DocumentNode.SelectNodes("//table") != null)
                {
                    foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("//table"))
                    {
                        string tableStyle = "";
                        if (m_HtmlNode.Attributes["style"] != null)
                            tableStyle = m_HtmlNode.Attributes["style"].Value;
                        int indexOfWidth = tableStyle.IndexOf("width");
                        if (indexOfWidth >= 0)
                        {
                            indexOfWidth = tableStyle.IndexOf(";", indexOfWidth);
                            if (indexOfWidth >= 0 && indexOfWidth < tableStyle.Length)
                            {
                                tableStyle = tableStyle.Substring(indexOfWidth + 1);
                                m_HtmlNode.SetAttributeValue("style", tableStyle);
                            }
                        }
                        m_HtmlNode.SetAttributeValue("width", "100%");
                    }
                }
                if(m_HtmlDocument.DocumentNode.SelectNodes("/html/body") != null)
                {
                    foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("/html/body"))
                    {


                        html += m_HtmlNode.InnerHtml;
                    }
                }
                //if (m_HtmlDocument.DocumentNode.SelectNodes("/html/head/style") != null)
                //{
                //    foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("/html/head/style"))
                //    {
                //        html += m_HtmlNode.OuterHtml;
                //    }
                //}
                    
                htmlContent = html;
                //File.WriteAllText(outputFileName, html, Encoding.UTF8);
            }
                
            return RetVal;
        }
        //==================================================
        /// <summary>
        /// Save doc to html and insert bookmark, return the menu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static List<string> GetHtmlTable(string path)
        {
            List<string> RetVal = new List<string>();
            
            Word.Application wordApp = new Word.Application();
            object file = path;
            object Confirm = false;
            object Readonly = false;
            object AddToRecentFile = false;
            
            Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, AddToRecentFile);
            string outputFileName = file + "temp.html";
            
            try
            {

                object oMissing = System.Reflection.Missing.Value;

                // save as html 
                WdSaveFormat format = WdSaveFormat.wdFormatFilteredHTML;
                object fileFormat = format;

                object SaveChange = false;

                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }
                doc.SaveAs(outputFileName,
                           fileFormat, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing);
                doc.Close(false);
                // reopen and get html table
                if (File.Exists(outputFileName))
                {
                    //Extractors m_Extractors = new Extractors();
                    string html = "";// File.ReadAllText(outputFileName);
                    //html = System.Net.WebUtility.HtmlDecode(html);
                    HtmlDocument m_HtmlDocument = new HtmlDocument();
                    m_HtmlDocument.Load(outputFileName);
                    html = "";
                    foreach (HtmlNode m_HtmlNode in m_HtmlDocument.DocumentNode.SelectNodes("//table"))
                    {
                        //if (m_Extractors.IsNewDocument(m_HtmlNode.InnerText))
                        //    continue;
                        html = m_HtmlNode.OuterHtml;
                        RetVal.Add(html);
                    }

                    //File.Delete(outputFileName);
                }

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                
                wordApp.Quit(false);
            }
            

            return RetVal;
        }
        //==================================================
        private static void FindAndReplace(Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(findText, matchCase, matchWholeWord,
                matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, replaceWithText, replace,
                matchKashida, matchDiacritics, matchAlefHamza, matchControl);
        }
        
        public static string SaveAsPdf(string path, bool AddToHeader = false, string WatterMarkImage = "", string WatterMarkText= "", string PathSaveAs = "")
        {
            string RetVal = "";
            object file = path;
            object Confirm = false;
            object Readonly = false;
            object AddToRecentFile = false;
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, AddToRecentFile);
            try
            {

                // change font to unicode

                foreach (Word.Paragraph m_Paragraph in doc.Paragraphs)
                {
                    if (m_Paragraph.Range.IsEndOfRowMark )
                        continue;
                    string ParagraphText = m_Paragraph.Range.Text;
                    if (String.IsNullOrEmpty(ParagraphText.Trim()))
                        continue;
                    if (Converter.IsTCVN3(m_Paragraph.Range.Font.Name))
                    {
                        ParagraphText = Converter.TCVN3ToUnicode(ParagraphText, m_Paragraph.Range.Font.Name);
                        try
                        {
                            ParagraphFormat m_ParagraphFormatOld = m_Paragraph.Range.ParagraphFormat.Duplicate;
                            m_Paragraph.Range.Font.Name = ConstantExtractors.UnicodeFont;
                            m_Paragraph.Range.Text = ParagraphText;
                            m_Paragraph.Range.ParagraphFormat = m_ParagraphFormatOld;
                        }
                        catch (Exception ex)
                        {
                            sms.utils.LogFiles.WriteLog(ex.ToString());
                            continue;
                        }
                    }
                }
                // insert wartter mark
                object LinkToFile = false;
                object SaveWidthDoc = true;
                
                if (ConstantExtractors.IsAddWatterMark > 0 && (String.IsNullOrEmpty(WatterMarkImage) == false || String.IsNullOrEmpty(WatterMarkText) == false))
                {
                    RemoveHeaderFooter(doc.Sections);
                    foreach (Word.Section wordSection in doc.Sections)
                    {
                        Word.Range WatterRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range;
                        if (AddToHeader)
                        {
                            WatterRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range;
                        }
                        if (File.Exists(WatterMarkImage))
                        {
                            if (ConstantExtractors.IsAddLine > 0)
                                WatterRange.InlineShapes.AddHorizontalLineStandard();
                            WatterRange.InlineShapes.AddPicture(WatterMarkImage, LinkToFile, SaveWidthDoc);
                            
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(WatterMarkImage) == false)
                                sms.utils.LogFiles.LogInfo(WatterMarkImage);
                            if (String.IsNullOrEmpty(WatterMarkText) == false)
                            {
                                
                                WatterRange.Font.ColorIndex = WdColorIndex.wdGray50;
                                WatterRange.Font.Size = 16;
                                WatterRange.Text = WatterMarkText;
                                if (ConstantExtractors.IsAddLine > 0)
                                    WatterRange.InlineShapes.AddHorizontalLineStandard();
                            }
                        }
                    }
                   
                }
                //===============================
                WdSaveFormat format = WdSaveFormat.wdFormatPDF;
                object fileFormat = format;
                object oMissing = System.Reflection.Missing.Value;
                string outputFileName = file + ".pdf";                
                object SaveChange = false;
                
                if (PathSaveAs != "")
                {
                    outputFileName = PathSaveAs;
                }
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }
                doc.SaveAs(outputFileName,
                           fileFormat, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing);
                
                //===================
               

            }
            catch (Exception ex)
            {
                
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                doc.Close(false);
                wordApp.Quit(false);
            }
            return RetVal;
        }
        public static string SaveAsDocx(string path, bool AddToHeader = false, string WatterMarkImage = "", string WatterMarkText = "", string PathSaveAs = "")
        {
            string RetVal = "";
            object file = path;
            object Confirm = false;
            object Readonly = false;
            object AddToRecentFile = false;
            Word.Application wordApp = new Word.Application();
            try
            {

                Word.Document doc = wordApp.Documents.Open(file, Confirm, Readonly, AddToRecentFile);
                // insert wartter mark
                object LinkToFile = false;
                object SaveWidthDoc = true;
                foreach (Word.Paragraph m_Paragraph in doc.Paragraphs)
                {
                    
                    string ParagraphText = m_Paragraph.Range.Text;
                    ParagraphText = Converter.TCVN3ToUnicode(ParagraphText, m_Paragraph.Range.Font.Name);                    
                    if (Converter.IsTCVN3(m_Paragraph.Range.Font.Name))
                    {
                        m_Paragraph.Range.Font.Name = "Time New Roman";
                        m_Paragraph.Range.Text = ParagraphText;
                    }
                }
                //===============================
                if (ConstantExtractors.IsAddWatterMark > 0 && (String.IsNullOrEmpty(WatterMarkImage) == false || String.IsNullOrEmpty(WatterMarkText) == false))
                {
                    RemoveHeaderFooter(doc.Sections);
                    foreach (Word.Section wordSection in doc.Sections)
                    {
                        
                        Word.Range WatterRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range;
                        if (AddToHeader)
                        {
                            WatterRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range;
                        }

                        if (File.Exists(WatterMarkImage))
                        {
                            WatterRange.InlineShapes.AddPicture(WatterMarkImage, LinkToFile, SaveWidthDoc);
                            if (ConstantExtractors.IsAddLine > 0)
                                WatterRange.InlineShapes.AddHorizontalLineStandard();
                            if (String.IsNullOrEmpty(WatterMarkText) == false)
                            {
                                WatterRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                                if (AddToHeader)
                                {
                                    WatterRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                                }
                                WatterRange.Font.ColorIndex = WdColorIndex.wdGray50;
                                WatterRange.Font.Size = 16;
                                WatterRange.Text = WatterMarkText;
                            }
                        }
                        else
                        {
                            sms.utils.LogFiles.LogInfo(WatterMarkImage);
                            if (String.IsNullOrEmpty(WatterMarkText) == false)
                            {
                                if (ConstantExtractors.IsAddLine > 0)
                                    WatterRange.InlineShapes.AddHorizontalLineStandard();
                                WatterRange.Font.ColorIndex = WdColorIndex.wdGray50;
                                WatterRange.Font.Size = 16;
                                WatterRange.Text = WatterMarkText;
                            }
                        }
                    }

                }
                //===============================
                WdSaveFormat format = WdSaveFormat.wdFormatDocumentDefault;
                object fileFormat = format;
                object oMissing = System.Reflection.Missing.Value;
                object outputFileName = file + ".docx";
                object SaveChange = false;

                if (PathSaveAs != "")
                {
                    outputFileName = PathSaveAs;
                }
                if (File.Exists(file + ".docx"))
                {
                    File.Delete(file + ".docx");
                }
                doc.SaveAs(outputFileName,
                           fileFormat, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing,
                           oMissing, oMissing, oMissing, oMissing);
                doc.Close(SaveChange);
                //===================
               

            }
            catch (Exception ex)
            {

                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            finally
            {
                wordApp.Quit(false);
            }
            return RetVal;
        }
        private static string ProcessTable(Table m_Table)
        {
            string RetVal = "";
            string TableSize = "";
            string TableBorder = "";
            string TableAlign = "";
            string CellSize = "";
            string CellText = "";
            string CellStyle = "";
            try
            {
                
                if(m_Table.Rows.Count <= 0)
                    return RetVal;
                if (m_Table.AllowAutoFit)
                {
                    TableSize = "width='100%'";
                }
                if (m_Table.Borders.InsideLineStyle == WdLineStyle.wdLineStyleNone && m_Table.Borders.OutsideLineStyle == WdLineStyle.wdLineStyleNone)
                {
                    TableBorder = "";
                }
                else
                {
                    TableBorder = "style='border-collapse:collapse' border='1' ";
                }
                if (m_Table.Rows.Alignment == WdRowAlignment.wdAlignRowCenter)
                {
                    TableAlign = " align='center' ";
                }
                else if (m_Table.Rows.Alignment == WdRowAlignment.wdAlignRowRight)
                {
                    TableAlign = " align='right' ";
                }
                else
                {
                    TableAlign = " align='left' ";
                }
                RetVal += "<table " + TableBorder + TableSize + TableAlign + " >";
                foreach (Row m_Row in m_Table.Rows)
                {
                    RetVal += "<tr>";
                    foreach (Cell m_Cell in m_Row.Cells)
                    {
                        CellSize = "";
                        CellStyle = "";
                        // cell width
                        if (m_Cell.PreferredWidthType == WdPreferredWidthType.wdPreferredWidthAuto)
                        {
                            CellSize += " width='auto' ";
                        }
                        else if (m_Cell.PreferredWidthType == WdPreferredWidthType.wdPreferredWidthPercent)
                        {
                            CellSize += " width='" + m_Cell.PreferredWidth.ToString() + "%' ";
                        }
                        else
                        {
                            CellSize += " width='" + m_Cell.PreferredWidth.ToString() + "px' ";
                        }
                        // cell height
                        if (m_Cell.HeightRule == WdRowHeightRule.wdRowHeightAuto)
                        {
                            CellSize += " height='auto' ";
                        }
                        else if (m_Cell.HeightRule == WdRowHeightRule.wdRowHeightAtLeast)
                        {
                            CellSize += " min-height='" + m_Cell.Height.ToString() + "px' ";
                        }
                        else
                        {
                            CellSize += " height='" + m_Cell.Height.ToString() + "px' ";
                        }
                        // alight 
                        WdParagraphAlignment m_CellAlignment = m_Cell.Range.Paragraphs.Alignment;
                        WdCellVerticalAlignment m_CellVerticalAlign = m_Cell.VerticalAlignment;
                        if (m_CellAlignment == WdParagraphAlignment.wdAlignParagraphCenter)
                        {
                            CellStyle += " style='text-align:center; ";
                        }
                        else if (m_CellAlignment == WdParagraphAlignment.wdAlignParagraphLeft)
                        {
                            CellStyle += " style='text-align:left; ";
                        }
                        else if (m_CellAlignment == WdParagraphAlignment.wdAlignParagraphRight)
                        {
                            CellStyle += " style='text-align:right; ";
                        }
                        else
                        {
                            CellStyle += " style='text-align:justify; ";
                        }
                        if (m_CellVerticalAlign == WdCellVerticalAlignment.wdCellAlignVerticalBottom)
                        {
                            CellStyle += " vertical-align:bottom;' ";
                        }
                        else if (m_CellVerticalAlign == WdCellVerticalAlignment.wdCellAlignVerticalCenter)
                        {
                            CellStyle += " vertical-align:middle;' ";
                        }                        
                        else
                        {
                            CellStyle += " vertical-align:top;' ";
                        }
                        RetVal += "<td " + CellSize + CellStyle + " >";
                        CellText = m_Cell.Range.Text;
                        CellText = Converter.TCVN3ToUnicode(CellText, m_Cell.Range.Font.Name);
                        CellText = Regex.Replace(CellText, @"[\x0B]", System.Environment.NewLine);
                        CellText = Regex.Replace(CellText, @"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", "");
                        RetVal += CellText;
                        RetVal += "</td>";
                    }
                    RetVal += "</tr>";
                }
                RetVal += "</table>" + System.Environment.NewLine;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            return RetVal;
        }
        private static bool IsInTatble(Paragraph m_Paragraph, out int IndexTable)
        {
            bool RetVal = false;
            IndexTable = -1;
            try
            {
                
                foreach (Word.Range range in l_TablesRanges)
                {
                    IndexTable++;
                    if (m_Paragraph.Range.Start >= range.Start && m_Paragraph.Range.Start <= range.End)
                    {
                        RetVal = true;
                        break;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                RetVal = false;
                sms.utils.LogFiles.WriteLog(ex.ToString());
            }
            return RetVal;
        }
        private static void RemoveHeaderFooter(Word.Sections m_Sections)
        {
            foreach (Word.Section wordSection in m_Sections)
            {
                wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "";
                wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].Range.Text = "";
                wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "";
                wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "";
                wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].Range.Text = "";
                wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "";
                foreach (HeaderFooter m_HeaderFooter in wordSection.Headers)
                {
                    foreach (Shape m_ShapRange in m_HeaderFooter.Shapes)
                    {
                        m_ShapRange.Delete();
                    }
                }
                foreach (HeaderFooter m_HeaderFooter in wordSection.Footers)
                {
                    foreach (Shape m_ShapRange in m_HeaderFooter.Shapes)
                    {
                        m_ShapRange.Delete();
                    }
                }
            }
        }
    }
}
