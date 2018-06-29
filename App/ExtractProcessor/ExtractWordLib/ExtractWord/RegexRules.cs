using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace ICSoft.ExtractWordLib
{
    public class RegexRules
    {
        #region private properties
        private string _RegexRuleText;
        private bool _RegexRuleIgnoreCase;
        private bool _RegexRuleMultiline;
        private bool _RegexRuleEndOfPreContent;
        
        private string _RegexRulesGroup;

        
        #endregion


        #region public properties
        public string RegexRuleText
        {
            get { return _RegexRuleText; }
            set { _RegexRuleText = value; }
        }
        

        public bool RegexRuleIgnoreCase
        {
            get { return _RegexRuleIgnoreCase; }
            set { _RegexRuleIgnoreCase = value; }
        }
        

        public bool RegexRuleMultiline
        {
            get { return _RegexRuleMultiline; }
            set { _RegexRuleMultiline = value; }
        }
        public string RegexRulesGroup
        {
            get { return _RegexRulesGroup; }
            set { _RegexRulesGroup = value; }
        }
        public bool RegexRuleEndOfPreContent
        {
            get { return _RegexRuleEndOfPreContent; }
            set { _RegexRuleEndOfPreContent = value; }
        }
        #endregion
        public RegexRules()
        {
            //init properties
            RegexRuleText = "";
            RegexRuleMultiline = true;
            RegexRuleIgnoreCase = true;
            RegexRuleEndOfPreContent = false;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesArticles()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
           
            string RegexRulesArticles = ConstantExtractors.RegexRulesArticles;
            string RegexRulesGroup = ConstantExtractors.RegexRulesArticleGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesArticleMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesArticleIgnoreCase;
            string[] l_RegexRulesArticles = RegexRulesArticles.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesArticleGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesArticles.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesArticles[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesArticleGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesSubSections()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesSubSections = ConstantExtractors.RegexRulesSubSections;
            string RegexRulesGroup = ConstantExtractors.RegexRulesSubSectionGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesSubSectionMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesSubSectionIgnoreCase;
            string[] l_RegexRulesSubSections = RegexRulesSubSections.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesSubSectionGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesSubSections.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesSubSections[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesSubSectionGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesSections()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesSections = ConstantExtractors.RegexRulesSections;
            string RegexRulesGroup = ConstantExtractors.RegexRulesSectionGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesSectionMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesSectionIgnoreCase;
            string[] l_RegexRulesSections = RegexRulesSections.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesSectionGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesSections.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesSections[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesSectionGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesChapters()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesChapters = ConstantExtractors.RegexRulesChapters;
            string RegexRulesGroup = ConstantExtractors.RegexRulesChapterGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesChapterMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesChapterIgnoreCase;
            string[] l_RegexRulesChapters = RegexRulesChapters.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesChapterGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesChapters.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesChapters[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesChapterGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesParts()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesParts = ConstantExtractors.RegexRulesParts;
            string RegexRulesGroup = ConstantExtractors.RegexRulesPartGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesPartMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesPartIgnoreCase;
            string[] l_RegexRulesParts = RegexRulesParts.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesPartGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesParts.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesParts[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesPartGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesParents()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesParents = ConstantExtractors.RegexRulesParents;
            string RegexRulesGroup = ConstantExtractors.RegexRulesParentGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesParentMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesParentIgnoreCase;
            string[] l_RegexRulesParents = RegexRulesParents.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesParentGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesParents.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesParents[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesParentGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesChildrens()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesChildrens = ConstantExtractors.RegexRulesChildrens;
            string RegexRulesGroup = ConstantExtractors.RegexRulesChildrenGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesChildrenMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesChildrenIgnoreCase;
            string[] l_RegexRulesChildrens = RegexRulesChildrens.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesChildrenGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesChildrens.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesChildrens[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesChildrenGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesPublicDate()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesPublicDate = ConstantExtractors.RegexRulesPublicDate;
            
            string[] l_RegexRulesPublicDate = RegexRulesPublicDate.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);

            for (int index = 0; index < l_RegexRulesPublicDate.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesPublicDate[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesPublisher()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesPublishers = ConstantExtractors.RegexRulesPublishers;
            string RegexRulesGroup = ConstantExtractors.RegexRulesPublisherGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesPublisherMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesPublisherIgnoreCase;
            string[] l_RegexRulesPublishers = RegexRulesPublishers.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesPublisherGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            
            for (int index = 0; index < l_RegexRulesPublishers.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesPublishers[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesPublisherGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesLawdocType()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesLawdocNo()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesLawdocNO = ConstantExtractors.RegexRulesLawdocNO;
            string RegexRulesGroup = ConstantExtractors.RegexRulesLawdocNOGroup;
            string[] l_RegexRulesLawdocNO = RegexRulesLawdocNO.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesFooterGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesLawdocNO.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesLawdocNO[index];
                m_RegexRules.RegexRulesGroup = l_RegexRulesFooterGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesFooter()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesFooters = ConstantExtractors.RegexRulesFooters;
            string RegexRulesGroup = ConstantExtractors.RegexRulesFooterGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesFooterMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesFooterIgnoreCase;
            string RegexRulesEndOfPreContent = ConstantExtractors.RegexRulesFooterEndOfPreContent;
            string[] l_RegexRulesFooters = RegexRulesFooters.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesFooterGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesEndOfPreContent = RegexRulesEndOfPreContent.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesFooters.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesFooters[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesFooterGroup[index];
                m_RegexRules.RegexRuleEndOfPreContent = bool.Parse(l_RegexRulesEndOfPreContent[index]);
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesAppendices()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesAppendices = ConstantExtractors.RegexRulesAppendices;
            string RegexRulesGroup = ConstantExtractors.RegexRulesAppendiceGroup;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesAppendiceMultiline;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesAppendiceIgnoreCase;
            string[] l_RegexRulesAppendices = RegexRulesAppendices.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesAppendiceGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesAppendices.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesAppendices[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesAppendiceGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
        //=========================================================
        public static List<RegexRules> GetRegexRulesAppendices2()
        {
            List<RegexRules> RetVal = new List<RegexRules>();
            string RegexRulesAppendices = ConstantExtractors.RegexRulesAppendices2;
            string RegexRulesGroup = ConstantExtractors.RegexRulesAppendiceGroup2;
            string RegexRulesMultiline = ConstantExtractors.RegexRulesAppendiceMultiline2;
            string RegexRulesIgnoreCase = ConstantExtractors.RegexRulesAppendiceIgnoreCase2;
            string[] l_RegexRulesAppendices = RegexRulesAppendices.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesAppendiceGroup = RegexRulesGroup.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesMultiline = RegexRulesMultiline.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            string[] l_RegexRulesIgnoreCase = RegexRulesIgnoreCase.Split(new[] { ConstantExtractors.Spliter }, StringSplitOptions.None);
            for (int index = 0; index < l_RegexRulesAppendices.Length; index++)
            {
                RegexRules m_RegexRules = new RegexRules();
                m_RegexRules.RegexRuleText = l_RegexRulesAppendices[index];
                m_RegexRules.RegexRuleIgnoreCase = bool.Parse(l_RegexRulesIgnoreCase[index]);
                m_RegexRules.RegexRuleMultiline = bool.Parse(l_RegexRulesMultiline[index]);
                m_RegexRules.RegexRulesGroup = l_RegexRulesAppendiceGroup[index];
                RetVal.Add(m_RegexRules);
            }
            return RetVal;
        }
       //==========================================================
    }

}
