using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Docs
    {
        public byte LanguageId { get; set; }
        public int DocId { get; set; }
        public string DocGUId { get; set; }
        public string DocName { get; set; }
        public string DocIdentity { get; set; }
        public string DocIdentityClear { get; set; }
        public string DocSummary { get; set; }
        public string DocContent { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime EffectDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string GazetteNumber { get; set; }
        public DateTime GazetteDate { get; set; }
        public byte EffectStatusId { get; set; }
        public byte ReviewStatusId { get; set; }
        public string DocUrl { get; set; }
        public string DocScopes { get; set; }
        public byte GrantLevelId { get; set; }
        public string PerformMethod { get; set; }
        public byte NumDossier { get; set; }
        public string Fee { get; set; }
        public string ElementDossier { get; set; }
        public string SettlementTime { get; set; }
        public string Result { get; set; }
        public DateTime ConfirmSignerDate { get; set; }
        public byte DocGroupId { get; set; }
        public byte DocTypeId { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string H1Tag { get; set; }
        public short IssueYear { get; set; }
        public string EffectStatusName { get; set; }
        public string DocTypeName { get; set; }
        public string OrganName { get; set; }
        public string SignerName { get; set; }
        public string FieldName { get; set; }
        public List<DocFields> lDocFields { get; set; }
        public List<DocFiles> lDocFiles { get; set; }
        public List<DocIndexes> lDocIndexes { get; set; }

        //-------------------------------------------------------------- 
        public string TruncateDocName(int mLengthRemain)
        {
            if (!string.IsNullOrEmpty(DocName))
            {
                string RetVal = DocName;
                if (DocName.Length > mLengthRemain)
                {
                    RetVal = DocName.Substring(0, mLengthRemain);
                    string nextChar = DocName.Substring(mLengthRemain, 1);
                    if (nextChar != " ") RetVal = RetVal.Substring(0, RetVal.LastIndexOf(" "));
                    RetVal = RetVal + " ...";
                }
                return RetVal.Trim();
            }
            return String.Empty;
        }
        //-------------------------------------------------------------- 
        public string GetDocUrl()
        {
            string RetVal = DocUrl;
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = string.Concat(Constants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetDocUrl(string TabName)
        {
            string RetVal = this.DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = string.Concat(Constants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetDocUrl(string DocUrl, string TabName)
        {
            string RetVal = DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = string.Concat(Constants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetDownloadUrl(string MediaDomain, string DocUrl, string FilePath)
        {
            string RetVal = MediaDomain;
            string[] l_Url = DocUrl.Split('/');
            string docUrlNotPath = "";
            for (int index = 0; index < l_Url.Length; index++)
            {
                if (l_Url[index].Contains(".html"))
                {
                    docUrlNotPath = l_Url[index];
                    break;
                }
            }
            RetVal += "tai-file-" + docUrlNotPath.Replace(".html", "") + "/" + FilePath + ".aspx";
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetViewUrl(string MediaDomain, string DocUrl, string FilePath)
        {
            string RetVal = MediaDomain;
            string[] l_Url = DocUrl.Split('/');
            string docUrlNotPath = "";
            for (int index = 0; index < l_Url.Length; index++)
            {
                if (l_Url[index].Contains(".html"))
                {
                    docUrlNotPath = l_Url[index];
                    break;
                }
            }
            RetVal += "xem-file-" + docUrlNotPath.Replace(".html", "") + "/" + FilePath + ".aspx";
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string GetDocUrl(int DocId, string DocName)
        {
            string RetVal = StringUtil.RemoveSign4VietnameseString(DocName);
            RetVal = RetVal.Replace(" ", "-");
            RetVal = RetVal.Replace("/", "-");
            RetVal = RetVal.Replace("?", "");
            RetVal = RetVal.Replace(":", "");
            RetVal = RetVal.Replace(",", "");
            while (RetVal.Contains("--"))
            {
                RetVal = RetVal.Replace("--", "-");
            }
            RetVal = RetVal + "-" + DocId.ToString() + ".html";
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = Constants.ROOT_PATH + RetVal;
            return RetVal;
        }
    }
}
