using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ICSoft.ExtractWordLib
{
    public class ConstantExtractors        
    {
        public static string Spliter = String.IsNullOrEmpty(ConfigurationManager.AppSettings["Spliter"]) ? "split" : ConfigurationManager.AppSettings["Spliter"];
        //article
        public static string RegexRulesArticles = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesArticles"]) ? @"^(Điều )([0-9]+)(\.|:)split^(Điều )(một|hai|ba|bốn)(\.|:)split^(Điều thứ )(nhất|hai|ba|bốn|[0-9]+)(\.|:)split^(Khoản )([0-9]+)(\.|:)split^(Khoản )(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)(\.|:)" : ConfigurationManager.AppSettings["RegexRulesArticles"];
        public static string RegexRulesArticleGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesArticleGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesArticleGroup"];
        public static string RegexRulesArticleMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesArticleMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesArticleMultiline"];
        public static string RegexRulesArticleIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesArticleIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesArticleIgnoreCase"];
        //subsection
        public static string RegexRulesSubSections = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSubSections"]) ? @"^(Tiểu Mục )([0-9]+)split^(Tiểu Mục )(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)\s+split^(Tiểu Mục thứ )(nhất|hai|ba|bốn|[0-9]+)split^(Tiểu Mục )([\w\s]+\s+\r?)$" : ConfigurationManager.AppSettings["RegexRulesSubSections"];
        public static string RegexRulesSubSectionGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSubSectionGroup"]) ? "2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesSubSectionGroup"];
        public static string RegexRulesSubSectionMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSubSectionMultiline"]) ? "truesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesSubSectionMultiline"];
        public static string RegexRulesSubSectionIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSubSectionIgnoreCase"]) ? "truesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesSubSectionIgnoreCase"];
        //section
        public static string RegexRulesSections = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSections"]) ? @"^(Mục )([0-9]+)split^(Mục )(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)\s+split^(Mục thứ )(nhất|hai|ba|bốn|[0-9]+)split^(Mục )([\w\s]+\s+\r?)$" : ConfigurationManager.AppSettings["RegexRulesSections"];
        public static string RegexRulesSectionGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSectionGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesSectionGroup"];
        public static string RegexRulesSectionMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSectionMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesSectionMultiline"];
        public static string RegexRulesSectionIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSectionIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesSectionIgnoreCase"];
        //chapter
        public static string RegexRulesChapters = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChapters"]) ? @"^(Chương )([0-9]+)split^(Chương )(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)\s+split^(Chương thứ )(nhất|hai|ba|bốn|[0-9]+)split^(Chương )([\w\s]+\s+\r?)$" : ConfigurationManager.AppSettings["RegexRulesChapters"];
        public static string RegexRulesChapterGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChapterGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesChapterGroup"];
        public static string RegexRulesChapterMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChapterMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesChapterMultiline"];
        public static string RegexRulesChapterIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChapterIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesChapterIgnoreCase"];
        //part
        public static string RegexRulesParts = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesParts"]) ? @"^(Phần )([0-9]+)split^(Phần )(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)\s+split ^(Phần thứ )(nhất|hai|ba|bốn|[0-9]+)split^(Khoản thứ )(nhất|hai|ba|bốn|[0-9]+)split^(PHẦN )([\w\s]+\s+\r?)$" : ConfigurationManager.AppSettings["RegexRulesParts"];
        public static string RegexRulesPartGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPartGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesPartGroup"];
        public static string RegexRulesPartMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPartMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesPartMultiline"];
        public static string RegexRulesPartIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPartIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesPartIgnoreCase"];
        //parent
        public static string RegexRulesParents = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesParents"]) ? @"^(XV[I]+|X[I]+V|[I]+V|V[I]+|[I]+X|X[I]+|[I]+|[X]+|V)\s+\r?$" : ConfigurationManager.AppSettings["RegexRulesParents"];
        public static string RegexRulesParentGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesParentGroup"]) ? "1" : ConfigurationManager.AppSettings["RegexRulesParentGroup"];
        public static string RegexRulesParentMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesParentMultiline"]) ? "true" : ConfigurationManager.AppSettings["RegexRulesParentMultiline"];
        public static string RegexRulesParentIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesParentIgnoreCase"]) ? "false" : ConfigurationManager.AppSettings["RegexRulesParentIgnoreCase"];
        //children
        public static string RegexRulesChildrens = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChildrens"]) ? @"^([0-9]+)(/|-|\.|\))" : ConfigurationManager.AppSettings["RegexRulesChildrens"];
        public static string RegexRulesChildrenGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChildrenGroup"]) ? "1" : ConfigurationManager.AppSettings["RegexRulesChildrenGroup"];
        public static string RegexRulesChildrenMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChildrenMultiline"]) ? "true" : ConfigurationManager.AppSettings["RegexRulesChildrenMultiline"];
        public static string RegexRulesChildrenIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesChildrenIgnoreCase"]) ? "true" : ConfigurationManager.AppSettings["RegexRulesChildrenIgnoreCase"];
        //footer
        public static string RegexRulesFooters = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesFooters"]) ? @"^(Nơi nhận:)\s+\r?$" : ConfigurationManager.AppSettings["RegexRulesFooters"];
        public static string RegexRulesFooterGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesFooterGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesFooterGroup"];
        public static string RegexRulesFooterMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesFooterMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesFooterMultiline"];
        public static string RegexRulesFooterIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesFooterIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesFooterIgnoreCase"];
        public static string RegexRulesFooterEndOfPreContent = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesFooterEndOfPreContent"]) ? "falsesplitfalsesplitfalsesplitfalsesplittruesplitfalsesplitfalsesplitfalsesplittrue" : ConfigurationManager.AppSettings["RegexRulesFooterEndOfPreContent"];
        //appendix
        public static string RegexRulesAppendices = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendices"]) ? @"^(Quy chế|Quy định|Biểu mẫu|Phụ lục|Kế hoạch|danh sách)(.+\s*.+\s*.+\s*.+\s*)(.+\s*ban hành kèm theo.+\s*của.+)" : ConfigurationManager.AppSettings["RegexRulesAppendices"];
        public static string RegexRulesAppendiceGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesAppendiceGroup"];
        public static string RegexRulesAppendiceMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesAppendiceMultiline"];
        public static string RegexRulesAppendiceIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesAppendiceIgnoreCase"];
        //appendix
        public static string RegexRulesAppendices2 = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendices2"]) ? @"^(Quy chế|Quy định|Biểu mẫu|Phụ lục|Kế hoạch|danh sách)(.+\s*.+\s*.+\s*.+\s*)(.+\s*ban hành kèm theo.+\s*của.+)" : ConfigurationManager.AppSettings["RegexRulesAppendices2"];
        public static string RegexRulesAppendiceGroup2 = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceGroup2"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesAppendiceGroup2"];
        public static string RegexRulesAppendiceMultiline2 = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceMultiline2"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesAppendiceMultiline2"];
        public static string RegexRulesAppendiceIgnoreCase2 = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesAppendiceIgnoreCase2"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesAppendiceIgnoreCase2"];
        //publisher
        public static string RegexRulesPublishers = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPublishers"]) ? @"(của\s*)(.+\s*.+\s*.)(\s*)(số.+\s*ngày.+)split(của\s*)(.+\s*.+\s*.)(\s*)(ngày.+)" : ConfigurationManager.AppSettings["RegexRulesPublishers"];
        public static string RegexRulesPublisherGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPublisherGroup"]) ? "2split2split2split2split2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesPublisherGroup"];
        public static string RegexRulesPublisherMultiline = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPublisherMultiline"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesPublisherMultiline"];
        public static string RegexRulesPublisherIgnoreCase = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPublisherIgnoreCase"]) ? "truesplittruesplittruesplittruesplittruesplittruesplittruesplittruesplittrue" : ConfigurationManager.AppSettings["RegexRulesPublisherIgnoreCase"];
        //publicdate
        public static string RegexRulesPublicDate = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesPublicDate"]) ? @"(Ngày\s+)([0-9]+)(\s+Tháng\s+)([0-9]+)(\s+Năm\s+)([0-9]+)split(dated\s+|.+\,\s*)([0-9]+|January|February|March|April|May|June|July|August|September|October|November|December)(\s+)([0-9]+)(\,\s*|th\s*)([0-9]+)split(dated\s+|.+\,\s*)([0-9]+)(\,\s*|th\s*|\s+)([0-9]+|January|February|March|April|May|June|July|August|September|October|November|December)(\s+)([0-9]+)" : ConfigurationManager.AppSettings["RegexRulesPublicDate"];
        //publicdate
        public static string RegexRulesLawdocNO = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesLawdocNO"]) ? @"(Số\s+|:\s+)([0-9]+\s+[0-9|a-z|/|\-|\\|đ|ư]+)(\s*ngày)split(Số\s+|:\s+)([0-9|a-z|/|\-|\\|đ|ư]+)" : ConfigurationManager.AppSettings["RegexRulesLawdocNO"];
        public static string RegexRulesLawdocNOGroup = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesLawdocNOGroup"]) ? "2split2split2split2split2" : ConfigurationManager.AppSettings["RegexRulesLawdocNOGroup"];
        //new doc or old doc
        public static string RegexRulesNewDocs = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesNewDocs"]) ? @"(Cộng\s+hòa\s+xã\s+hội\s+chủ\s+nghĩa\s+Việt\s+Nam |độc lập.+tự do.+hạnh phúc)" : ConfigurationManager.AppSettings["RegexRulesNewDocs"];
        public static string RegexRulesSaparators = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesSaparators"]) ? @"^-{3,}" : ConfigurationManager.AppSettings["RegexRulesSaparators"];
        //End of doc name
        public static string RegexRulesEndDocName = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesEndDocName"]) ? @"^(cqbh)" : ConfigurationManager.AppSettings["RegexRulesEndDocName"];
        //is Vietnamese
        public static string RegexRulesVietnames = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesVietnames"]) ? @"(ă|â|ắ|ằ|ẳ|ặ|ấ|ầ|ậ|ẩ|ê|ề|ế|ệ|ể|ô|ổ|ố|ồ|ộ|ơ|ờ|ớ|ợ|ư|ự|ứ|ừ|ử)" : ConfigurationManager.AppSettings["RegexRulesVietnames"];
        //is Bullet
        public static string RegexRulesBullet = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesBullet"]) ? @"^([0-9]\.|[0-9]\)|[a-z]\.|[a-z]\)|-|\+)" : ConfigurationManager.AppSettings["RegexRulesBullet"];
        //max lenth
        public static int MaxLengthOfArticleDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfArticleDesc"]) ? 66 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfArticleDesc"]);
        public static int MaxLengthOfSubSectionDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfSubSectionDesc"]) ? 66 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfSubSectionDesc"]);
        public static int MaxLengthOfSectionDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfSectionDesc"]) ? 66 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfSectionDesc"]);
        public static int MaxLengthOfChapterDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfChapterDesc"]) ? 66 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfChapterDesc"]);
        public static int MaxLengthOfPartDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfPartDesc"]) ? 66 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfPartDesc"]);
        public static int MaxLengthOfAppendix = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthOfAppendix"]) ? 220 : int.Parse(ConfigurationManager.AppSettings["MaxLengthOfAppendix"]);
        //min lenth
        public static int MinLengthOfArticleDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MinLengthOfArticleDesc"]) ? 15 : int.Parse(ConfigurationManager.AppSettings["MinLengthOfArticleDesc"]);
        public static int MinLengthOfPartDesc = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MinLengthOfPartDesc"]) ? 15 : int.Parse(ConfigurationManager.AppSettings["MinLengthOfPartDesc"]);
        //table and appendix
        public static int IsProcessTable = String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsProcessTable"]) ? 0 : int.Parse(ConfigurationManager.AppSettings["IsProcessTable"]);
        public static int IsProcessAppendix = String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsProcessAppendix"]) ? 0 : int.Parse(ConfigurationManager.AppSettings["IsProcessAppendix"]);
        public static int IsAddWatterMark = String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsAddWatterMark"]) ? 1 : int.Parse(ConfigurationManager.AppSettings["IsAddWatterMark"]);
        public static int IsAddLine = String.IsNullOrEmpty(ConfigurationManager.AppSettings["IsAddLine"]) ? 1 : int.Parse(ConfigurationManager.AppSettings["IsAddLine"]);
        public static string UnicodeFont = String.IsNullOrEmpty(ConfigurationManager.AppSettings["UnicodeFont"]) ? "Time New Roman" : ConfigurationManager.AppSettings["UnicodeFont"];
        //English LawdocType
        public static string RegexRulesLawdocTypeEn = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RegexRulesLawdocTypeEn"]) ? @"^(cqbh)(.+)(No\.|No\:|No\s+)" : ConfigurationManager.AppSettings["RegexRulesLawdocTypeEn"];
    }
}
