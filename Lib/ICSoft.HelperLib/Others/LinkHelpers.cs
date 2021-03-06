using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ICSoft.CMSLib;
using sms.utils;
/// <summary>
/// Summary description for StringUtil
/// </summary>
namespace ICSoft.HelperLib
{
    public class LinkHelpers
    {        
        public LinkHelpers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //----------------------------------------------------------------------------------------------
        public static string StripTags(string HTML, bool RetainSpace)
        {
            
            string RepString;
            if (RetainSpace)
                RepString = " ";
            else
                RepString = "";

            //'Replace Tags by replacement String and return mofified string
            return System.Text.RegularExpressions.Regex.Replace(HTML, "<[^>]*>", RepString);
        }
        //----------------------------------------------------------------------------------------------
        public static string StripTags(string HTML, string UnremoveTag, bool RetainSpace)
        {
            string[] l_UnremoveTags = UnremoveTag.Split(',');
            string RegxUnremove = "(?!";
            for (int index = 0; index < l_UnremoveTags.Length; index++)
            {
                if (index == 0)
                    RegxUnremove += l_UnremoveTags[index] + "|/" + l_UnremoveTags[index];
                else
                    RegxUnremove += "|" + l_UnremoveTags[index] + "|/" + l_UnremoveTags[index];
            }
            RegxUnremove += ")";
            //UnremoveTag = "(?!" + UnremoveTag + "|/" + UnremoveTag + ")";
            //'Set up Replacement String
            string RepString;
            if (RetainSpace)
                RepString = " ";
            else
                RepString = "";

            //'Replace Tags by replacement String and return mofified string
            return System.Text.RegularExpressions.Regex.Replace(HTML, "<" + RegxUnremove + "[^>]*>", RepString);
        }
        //-----------------------------------------------------------------------
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
        public static string RemoveSpecialCharacters(string input)
        {
            try
            {
                Regex r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                return r.Replace(input, String.Empty);

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
           
        }
        //-----------------------------------------------------------------------
        public static string RemoveSpecialCharactersNotSpace(string input)
        {
            try
            {

                Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                return r.Replace(input, String.Empty);

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static string getRewriteUrlPages(string PageTypeLink, string PageName, string PageId)
        {
            try
            {
                if (PageTypeLink.Contains("http") || PageTypeLink.Contains("$") == false)
                    return PageTypeLink;
                if (PageName.Length > 0)
                {
                    PageName =  StringUtil.RemoveSignature(PageName);
                    PageName = RemoveSpecialCharactersNotSpace(PageName);
                    PageName = PageName.Replace(" ", ConstantHelpers.RewriteSpace);
                }
                string reVal = "";

                reVal += PageTypeLink.Replace("$1", PageName).Replace("$2", PageId);
                reVal += ConstantHelpers.RewriteExt;
                
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static string getRewriteUrlItems(string PageTypeId, string PageName, string PageId, string ItemName, string ItemId)
        {
            try
            {
               
                if (!string.IsNullOrEmpty(PageName))
                {
                    
                    PageName = StringUtil.RemoveSignature(PageName);
                    PageName = RemoveSpecialCharactersNotSpace(PageName);
                    PageName = PageName.Replace(" ", ConstantHelpers.RewriteSpace);
                }
                if (!string.IsNullOrEmpty(ItemName))
                {
                    ItemName = StripTags(ItemName, false);
                    ItemName = StringUtil.RemoveSignature(ItemName);
                    ItemName = RemoveSpecialCharactersNotSpace(ItemName);
                    ItemName = ItemName.Replace(" ", ConstantHelpers.RewriteSpace);
                    if (ItemName.Length > 180)
                    {
                        ItemName = ItemName.Substring(0, 180);
                    }
                }
                string reVal = CmsConstants.ROOT_PATH;
                string NewPageParam;// = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_ITEM_PREFIX + ItemId;
                string ItemPrefix = ConstantHelpers.REWRITE_ITEM_PREFIX;
                if (PageTypeId == PageTypeHelpers.DocDetail.ToString())
                {
                    ItemPrefix = ConstantHelpers.REWRITE_DOCITEM_PREFIX;
                }
                if (PageId != "0")
                {
                    NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ItemPrefix + ItemId;
                }
                else
                {
                    NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ItemPrefix + ItemId;
                }
                if (string.IsNullOrEmpty(PageName))
                {
                    reVal += ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                else
                {
                    reVal += PageName + ConstantHelpers.RewriteSaparator + ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                reVal += ConstantHelpers.RewriteExt;
                //Log.writeLog(SiteCategoryName + ":" + SiteCategoryId.ToString(), "");
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        /// <summary>
        /// for Doc with language
        /// </summary>
        /// <param name="PageTypeId"></param>
        /// <param name="PageName"></param>
        /// <param name="PageId"></param>
        /// <param name="ItemName"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public static string getRewriteUrlItemWithLang(string PageTypeId, string PageName, string PageId, string ItemName, string ItemId, string DocLanguageId)
        {
            try
            {

                if (!string.IsNullOrEmpty(PageName))
                {

                    PageName = StringUtil.RemoveSignature(PageName);
                    PageName = RemoveSpecialCharactersNotSpace(PageName);
                    PageName = PageName.Replace(" ", ConstantHelpers.RewriteSpace);
                }
                if (!string.IsNullOrEmpty(ItemName))
                {
                    ItemName = StripTags(ItemName, false);
                    ItemName = StringUtil.RemoveSignature(ItemName);
                    ItemName = RemoveSpecialCharactersNotSpace(ItemName);
                    ItemName = ItemName.Replace(" ", ConstantHelpers.RewriteSpace);
                    if (ItemName.Length > 180)
                    {
                        ItemName = ItemName.Substring(0, 180);
                    }
                }
                string reVal = CmsConstants.ROOT_PATH;
                string NewPageParam;// = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_ITEM_PREFIX + ItemId;
                string ItemPrefix = ConstantHelpers.REWRITE_ITEM_PREFIX;
                if (PageTypeId == PageTypeHelpers.DocDetail.ToString())
                {
                    ItemPrefix = ConstantHelpers.REWRITE_DOCITEM_PREFIX;
                }
                if (PageId != "0")
                {
                    NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ItemPrefix + ItemId;
                }
                else
                {
                    NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ItemPrefix + ItemId;
                }
                if (string.IsNullOrEmpty(PageName))
                {
                    reVal += ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                else
                {
                    reVal += PageName + ConstantHelpers.RewriteSaparator + ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                reVal += ConstantHelpers.RewriteExt + "?DocLanguageId=" + DocLanguageId;

                //Log.writeLog(SiteCategoryName + ":" + SiteCategoryId.ToString(), "");
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static string getRewriteUrlItemJsons(string PageTypeId, string PageName, string PageId, string ItemName, string ItemId)
        {
            try
            {

                
                string reVal = "Item";
                string NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_ITEM_PREFIX + ItemId;
                reVal += ConstantHelpers.RewriteSaparator + NewPageParam + "/";
                
                //Log.writeLog(SiteCategoryName + ":" + SiteCategoryId.ToString(), "");
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static string getRewriteUrlItems(string PageTypeId, string PageName, string PageId, string ItemName, string ItemId, string SourceItemUrl)
        {
            try
            {
                if(!string.IsNullOrEmpty(SourceItemUrl) )
                    return SourceItemUrl;
                
                if (!string.IsNullOrEmpty(PageName))
                {
                    PageName = StringUtil.RemoveSignature(PageName);
                    PageName = RemoveSpecialCharactersNotSpace(PageName);
                    PageName = PageName.Replace(" ", ConstantHelpers.RewriteSpace);
                }
                if (!string.IsNullOrEmpty(ItemName))
                {
                    ItemName = StripTags(ItemName, false);
                    ItemName = StringUtil.RemoveSignature(ItemName);
                    ItemName = RemoveSpecialCharactersNotSpace(ItemName);
                    ItemName = ItemName.Replace(" ", ConstantHelpers.RewriteSpace);
                    if (ItemName.Length > 180)
                    {
                        ItemName = ItemName.Substring(0, 180);
                    }
                }
                string reVal = CmsConstants.ROOT_PATH;
                string NewPageParam = ConstantHelpers.REWRITE_PAGETYPE_PREFIX + PageTypeId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_PAGE_PREFIX + PageId + ConstantHelpers.RewriteSaparator + ConstantHelpers.REWRITE_ITEM_PREFIX + ItemId;
                if (string.IsNullOrEmpty(PageName))
                {
                    reVal += ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                else
                {
                    reVal += PageName + ConstantHelpers.RewriteSaparator + ItemName + ConstantHelpers.RewriteSaparator + NewPageParam;
                }
                reVal += ConstantHelpers.RewriteExt;
                //Log.writeLog(SiteCategoryName + ":" + SiteCategoryId.ToString(), "");
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static string getRewriteUrlCategory(string CategoryLink, string SiteCategoryName, string SiteCategoryId)
        {
            try
            {
                if (CategoryLink.Contains("http") || CategoryLink.Contains("$") == false)
                    return CategoryLink;
                if (SiteCategoryName.Length > 0)
                {
                    SiteCategoryName = StringUtil.RemoveSignature(SiteCategoryName);
                    SiteCategoryName = RemoveSpecialCharactersNotSpace(SiteCategoryName);
                    SiteCategoryName = SiteCategoryName.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                else
                    reVal += "en/";
                if (CategoryLink.Length > 0)
                {
                    reVal += CategoryLink.Replace("$1", SiteCategoryName).Replace("$2", SiteCategoryId);
                }
                else
                {
                    reVal += SiteCategoryName + "-c" + SiteCategoryId;
                }
                reVal += ConstantHelpers.RewriteExt;
                //Log.writeLog(SiteCategoryName + ":" + SiteCategoryId.ToString(), "");
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        
        public static string getRewriteUrlCategory(string CategoryLink, string SiteCategoryName, string SiteCategoryId, string SiteCategoryParentId)
        {
            try
            {
                if (CategoryLink.Contains("http") || CategoryLink.Contains("$") == false)
                    return CategoryLink;
                if (SiteCategoryName.Length > 0)
                {
                    SiteCategoryName = StringUtil.RemoveSignature(SiteCategoryName);
                    SiteCategoryName = RemoveSpecialCharactersNotSpace(SiteCategoryName);
                    SiteCategoryName = SiteCategoryName.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                else
                    reVal += "en/";
                if (CategoryLink.Length > 0)
                {
                    reVal += CategoryLink.Replace("$1", SiteCategoryName).Replace("$2", SiteCategoryParentId).Replace("$3", SiteCategoryId);
                }
                else
                {
                    reVal += SiteCategoryName + "-c" + SiteCategoryId;
                }
                reVal += ConstantHelpers.RewriteExt;
                return reVal;

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        
        public static string getRewriteUrlArticleLink(string ArticleTitle,string ArticleId, string SiteCategoryName, string SiteCategoryId)
        {
            try
            {
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                {
                    SiteCategoryName = StringUtil.RemoveSignature(SiteCategoryName);
                    SiteCategoryName = RemoveSpecialCharactersNotSpace(SiteCategoryName);
                    SiteCategoryName = SiteCategoryName.Replace(" ", "-");
                }
                if (ArticleTitle.Length > 0)
                {
                    ArticleTitle = StringUtil.RemoveSignature(ArticleTitle);
                    ArticleTitle = RemoveSpecialCharactersNotSpace(ArticleTitle);
                    ArticleTitle = ArticleTitle.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                    reVal += SiteCategoryName + "/";
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                reVal += ArticleTitle + "-a" + ArticleId + "-c" + SiteCategoryId;
                reVal += ConstantHelpers.RewriteExt;

                return reVal;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        public static string getRewriteUrlArticleLink(string ArticleTitle, string ArticleId, string SiteCategoryName, string SiteCategoryId, string ArticleLink)
        {
            try
            {
                if (string.IsNullOrEmpty(ArticleLink) == false && ArticleLink.Contains("http"))
                    return ArticleLink + "\"" + " target= \"_blank";
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                {
                    SiteCategoryName = StringUtil.RemoveSignature(SiteCategoryName);
                    SiteCategoryName = RemoveSpecialCharactersNotSpace(SiteCategoryName);
                    SiteCategoryName = SiteCategoryName.Replace(" ", "-");
                }
                if (ArticleTitle.Length > 0)
                {
                    ArticleTitle = StringUtil.RemoveSignature(ArticleTitle);
                    ArticleTitle = RemoveSpecialCharactersNotSpace(ArticleTitle);
                    ArticleTitle = ArticleTitle.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                    reVal += SiteCategoryName + "/";
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                reVal += ArticleTitle + "-a" + ArticleId + "-c" + SiteCategoryId;
                reVal += ConstantHelpers.RewriteExt;
                
                return reVal;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        public static string getRewriteUrlMeetingLink(string MeetingTitle, string MeetingId, string SiteCategoryName, string SiteCategoryId)
        {
            try
            {
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                {
                    SiteCategoryName = StringUtil.RemoveSignature(SiteCategoryName);
                    SiteCategoryName = RemoveSpecialCharactersNotSpace(SiteCategoryName);
                    SiteCategoryName = SiteCategoryName.Replace(" ", "-");
                }
                if (MeetingTitle.Length > 0)
                {
                    MeetingTitle = StringUtil.RemoveSignature(MeetingTitle);
                    MeetingTitle = RemoveSpecialCharactersNotSpace(MeetingTitle);
                    MeetingTitle = MeetingTitle.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                if (SiteCategoryName != null && SiteCategoryName.Length > 0)
                    reVal += SiteCategoryName + "/";
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                reVal += MeetingTitle + "-m" + MeetingId + "-c" + SiteCategoryId;
                reVal += ConstantHelpers.RewriteExt;

                return reVal;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        public static string getRewriteUrlMemberLink(string MemberTitle, string MemberId, string SiteCategoryId = "")
        {
            try
            {
                
                if (MemberTitle.Length > 0)
                {
                    MemberTitle = StringUtil.RemoveSignature(MemberTitle);
                    MemberTitle = RemoveSpecialCharactersNotSpace(MemberTitle);
                    MemberTitle = MemberTitle.Replace(" ", "-");
                }
                string reVal = CmsConstants.ROOT_PATH;
                
                HttpRequest Request = HttpContext.Current.Request;
                if (Request["LanguageId"] != null)
                    reVal += Request["LanguageId"].Substring(0, 2) + "/";
                reVal += MemberTitle + "-mb" + MemberId;
                if (string.IsNullOrEmpty(SiteCategoryId) == false && SiteCategoryId != "0")
                    reVal += "-c" + SiteCategoryId;
                reVal += ConstantHelpers.RewriteExt;

                return reVal;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";
            }
        }
        public static string getSaparator(int index, int maxIndex)
        {
            if (index < maxIndex)
                return ", ";
            else
                return "";
        }
        
    }
}
