using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class ExtractorHelpers
    {
        #region private properties
       
        #endregion

        #region public properties
        
        #endregion
        public ExtractorHelpers()
        {
            
        }
        //==========================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Publisher"></param>
        /// <returns></returns>
        public static string ValidatePublisher(string Publisher)
        {
            string RetVal = Publisher.Trim();

            return RetVal;
        }
        //==========================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Publisher"></param>
        /// <returns></returns>
        public static string ValidateLawdocType(string LawdocType)
        {
            string RetVal = LawdocType.Trim();

            return RetVal;
        }
        //==========================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="l_Content"></param>
        public static void ValidateContentTree(List<ContentItems> l_Content)
        {
            ValidateTreeLevel(l_Content);
            
            foreach (ContentItems m_ContentItemLevel1 in l_Content)
            {
                #region if level 1 not is article                
                if (m_ContentItemLevel1.ItemLevel < RegexLevels.Article)
                {                    
                    foreach (ContentItems m_ContentItemLevel2 in m_ContentItemLevel1.l_Children)
                    {                       
                        #region if level 2 not is article
                        if (m_ContentItemLevel2.ItemLevel < RegexLevels.Article)
                        {
                            foreach (ContentItems m_ContentItemLevel3 in m_ContentItemLevel2.l_Children)
                            {
                                #region if level 3 not is article
                                if (m_ContentItemLevel3.ItemLevel < RegexLevels.Article)
                                {
                                    foreach (ContentItems m_ContentItemLevel4 in m_ContentItemLevel3.l_Children)
                                    {
                                        #region if level 4 not is article
                                        if (m_ContentItemLevel4.ItemLevel < RegexLevels.Article)
                                        {
                                            foreach (ContentItems m_ContentItemLevel5 in m_ContentItemLevel4.l_Children)
                                            {
                                                
                                                if (m_ContentItemLevel5.ItemLevel == RegexLevels.Article)
                                                {
                                                    if (m_ContentItemLevel5.ItemDesc.Length > 0 && m_ContentItemLevel5.ItemValue.Length < m_ContentItemLevel5.ItemDesc.Length)
                                                    {
                                                        if (String.IsNullOrEmpty(m_ContentItemLevel5.ItemValue))
                                                        {
                                                            m_ContentItemLevel5.ItemValue = m_ContentItemLevel5.ItemDesc;
                                                            m_ContentItemLevel5.ItemDesc = "";
                                                        }
                                                        else
                                                        {
                                                            m_ContentItemLevel5.ItemValue = m_ContentItemLevel5.ItemDesc + System.Environment.NewLine + m_ContentItemLevel5.ItemValue;
                                                            m_ContentItemLevel5.ItemDesc = "";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region level 4 is article
                                        else
                                        {
                                            if (m_ContentItemLevel4.ItemDesc.Length > 0 && m_ContentItemLevel4.ItemValue.Length < m_ContentItemLevel4.ItemDesc.Length)
                                            {
                                                if (String.IsNullOrEmpty(m_ContentItemLevel4.ItemValue))
                                                {
                                                    m_ContentItemLevel4.ItemValue = m_ContentItemLevel4.ItemDesc;
                                                    m_ContentItemLevel4.ItemDesc = "";
                                                }
                                                else
                                                {
                                                    m_ContentItemLevel4.ItemValue = m_ContentItemLevel4.ItemDesc + System.Environment.NewLine + m_ContentItemLevel4.ItemValue;
                                                    m_ContentItemLevel4.ItemDesc = "";
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                                #region level 3 is article
                                else
                                {
                                    if (m_ContentItemLevel3.ItemDesc.Length > 0 && m_ContentItemLevel3.ItemValue.Length < m_ContentItemLevel3.ItemDesc.Length)
                                    {
                                        if (String.IsNullOrEmpty(m_ContentItemLevel3.ItemValue))
                                        {
                                            m_ContentItemLevel3.ItemValue = m_ContentItemLevel3.ItemDesc;
                                            m_ContentItemLevel3.ItemDesc = "";
                                        }
                                        else
                                        {
                                            m_ContentItemLevel3.ItemValue = m_ContentItemLevel3.ItemDesc + System.Environment.NewLine + m_ContentItemLevel3.ItemValue;
                                            m_ContentItemLevel3.ItemDesc = "";
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        #region level 2 is article
                        else
                        {
                            if (m_ContentItemLevel2.ItemDesc.Length > 0 && m_ContentItemLevel2.ItemValue.Length < m_ContentItemLevel2.ItemDesc.Length)
                            {
                                if (String.IsNullOrEmpty(m_ContentItemLevel2.ItemValue))
                                {
                                    m_ContentItemLevel2.ItemValue = m_ContentItemLevel2.ItemDesc;
                                    m_ContentItemLevel2.ItemDesc = "";
                                }
                                else
                                {
                                    m_ContentItemLevel2.ItemValue = m_ContentItemLevel2.ItemDesc + System.Environment.NewLine + m_ContentItemLevel2.ItemValue;
                                    m_ContentItemLevel2.ItemDesc = "";
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                #region level 1 is article
                else
                {
                    if (m_ContentItemLevel1.ItemDesc.Length > 0 && m_ContentItemLevel1.ItemValue.Length < m_ContentItemLevel1.ItemDesc.Length)
                    {
                        if (String.IsNullOrEmpty(m_ContentItemLevel1.ItemValue))
                        {
                            m_ContentItemLevel1.ItemValue = m_ContentItemLevel1.ItemDesc;
                            m_ContentItemLevel1.ItemDesc = "";
                        }
                        else
                        {
                            m_ContentItemLevel1.ItemValue = m_ContentItemLevel1.ItemDesc + System.Environment.NewLine + m_ContentItemLevel1.ItemValue;
                            m_ContentItemLevel1.ItemDesc = "";
                        }
                    }
                }
                #endregion
            }
        }
        //==========================================================
        /// <summary>
        /// ValidateTreeLevel
        /// </summary>
        /// <param name="l_Content"></param>
        public static List<ContentItems> ValidateTreeLevel(List<ContentItems> l_Content)
        {
            
            int MaxLevel = 0;
            int Index = 0;
            ContentItems m_ContentItemsValid = null;
           
            foreach (ContentItems m_ContentItemLevel1 in l_Content)
            {
                if (m_ContentItemLevel1.ItemLevel > MaxLevel)
                {
                    MaxLevel = m_ContentItemLevel1.ItemLevel;
                }
               
                // level 2
                foreach (ContentItems m_ContentItemLevel2 in m_ContentItemLevel1.l_Children)
                {

                    if (m_ContentItemLevel2.ItemLevel > MaxLevel)
                    {
                        MaxLevel = m_ContentItemLevel2.ItemLevel;
                    }
                   
                    // level 3
                    foreach (ContentItems m_ContentItemLevel3 in m_ContentItemLevel2.l_Children)
                    {
                        if (m_ContentItemLevel3.ItemLevel > MaxLevel)
                        {
                            MaxLevel = m_ContentItemLevel3.ItemLevel;
                        }
                        
                        // level 4
                        foreach (ContentItems m_ContentItemLevel4 in m_ContentItemLevel3.l_Children)
                        {
                            if (m_ContentItemLevel4.ItemLevel > MaxLevel)
                            {
                                MaxLevel = m_ContentItemLevel4.ItemLevel;
                            }
                            
                            // level 5
                            foreach (ContentItems m_ContentItemLevel5 in m_ContentItemLevel4.l_Children)
                            {
                                if (m_ContentItemLevel5.ItemLevel > MaxLevel)
                                {
                                    MaxLevel = m_ContentItemLevel5.ItemLevel;
                                }                                
                            }
                        }
                    }
                }
            }
            // End level1
            if (MaxLevel != RegexLevels.Article)
            {
                foreach (ContentItems m_ContentItemLevel1 in l_Content)
                {
                    if (m_ContentItemLevel1.ItemLevel == MaxLevel)
                    {
                        m_ContentItemLevel1.ItemLevel = RegexLevels.Article;
                    }                    
                    // level 2
                    foreach (ContentItems m_ContentItemLevel2 in m_ContentItemLevel1.l_Children)
                    {
                        if (m_ContentItemLevel2.ItemLevel == MaxLevel)
                        {
                            m_ContentItemLevel2.ItemLevel = RegexLevels.Article;
                        }                       
                        // level 3
                        foreach (ContentItems m_ContentItemLevel3 in m_ContentItemLevel2.l_Children)
                        {
                            if (m_ContentItemLevel3.ItemLevel == MaxLevel)
                            {
                                m_ContentItemLevel3.ItemLevel = RegexLevels.Article;
                            }                            
                            // level 4
                            foreach (ContentItems m_ContentItemLevel4 in m_ContentItemLevel3.l_Children)
                            {
                                if (m_ContentItemLevel4.ItemLevel == MaxLevel)
                                {
                                    m_ContentItemLevel4.ItemLevel = RegexLevels.Article;
                                }
                                // level 5
                                foreach (ContentItems m_ContentItemLevel5 in m_ContentItemLevel4.l_Children)
                                {
                                    if (m_ContentItemLevel5.ItemLevel == MaxLevel)
                                    {
                                        m_ContentItemLevel5.ItemLevel = RegexLevels.Article;
                                    }
                                }
                            }
                        }
                    }
                }
                // End level1
            }
            // Validate Parent   
            List<ContentItems> l_DeleteItem = new List<ContentItems>();
            List<ContentItems> l_AddItem = new List<ContentItems>();
            bool IsValid = true;
            for (Index = 0; Index < l_Content.Count; Index++)
            {
                ContentItems m_ContentItemLevel1 = l_Content[Index];
                if (String.IsNullOrEmpty(m_ContentItemLevel1.ItemValue) == false && m_ContentItemLevel1.ItemLevel < RegexLevels.Article && m_ContentItemLevel1.l_Children.Count == 0)
                {
                    IsValid = false;
                    break;
                }
            }
            if (!IsValid)
            {
                for (Index = 0; Index < l_Content.Count; Index++)
                {
                    ContentItems m_ContentItemLevel1 = l_Content[Index];

                    if (m_ContentItemLevel1.ItemLevel == RegexLevels.Article)
                    {
                        m_ContentItemsValid = m_ContentItemLevel1;
                    }

                    if (String.IsNullOrEmpty(m_ContentItemLevel1.ItemValue) == false && m_ContentItemLevel1.ItemLevel < RegexLevels.Article )
                    {
                        if (m_ContentItemsValid != null)
                        {
                            m_ContentItemsValid.ItemValue += System.Environment.NewLine + m_ContentItemLevel1.ItemName + m_ContentItemLevel1.ItemDesc;
                            m_ContentItemsValid.ItemValue += System.Environment.NewLine + m_ContentItemLevel1.ItemValue;
                            l_DeleteItem.Add(m_ContentItemLevel1);
                            for (int IndexLevel2 = 0; IndexLevel2 < m_ContentItemLevel1.l_Children.Count; IndexLevel2++)
                            {
                                ContentItems m_ContentItemLevel2 = m_ContentItemLevel1.l_Children[IndexLevel2];
                                m_ContentItemLevel2.ItemLevel = RegexLevels.Article;
                                m_ContentItemLevel2.DocLevel = Index + IndexLevel2 + 1;
                                l_AddItem.Add(m_ContentItemLevel2);
                            }

                        }
                    }

                }
            }
            foreach (ContentItems m_AddItem in l_AddItem)
            {
                l_Content.Insert(m_AddItem.DocLevel, m_AddItem);
            }
            foreach (ContentItems m_DeleteItem in l_DeleteItem)
            {
                l_Content.Remove(m_DeleteItem);
            }
            // End validate parent
            return l_Content;
        }
        //==========================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="replaceString"></param>
        /// <returns></returns>
        public static string CutStringByWord(string str, int maxLength, string replaceString)
        {
            try
            {
                if (str.Length > maxLength)
                {
                    str = str.Substring(0, maxLength);
                    str = str.Substring(0, str.LastIndexOf(" ")) + replaceString;
                }
                return str;
            }
            catch
            {
                return str;
            }
        }
    }

}
