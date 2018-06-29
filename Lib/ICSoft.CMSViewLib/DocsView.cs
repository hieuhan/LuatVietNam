
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{

    /// <summary>
    /// class Docs
    /// </summary>
    public class DocsView
    {
        private byte _LanguageId;
        private int _DocId;
        private string _DocGUId;
        private string _DocName;
        private string _DocIdentity;
        private string _DocIdentityClear;
        private string _DocSummary;
        private string _DocContent;
        private DateTime _IssueDate;
        private DateTime _EffectDate;
        private DateTime _ExpireDate;
        private string _GazetteNumber;
        private DateTime _GazetteDate;
        private short _DataSourceId;
        private byte _UseStatusId;
        private byte _EffectStatusId;
        private byte _ReviewStatusId;
        private string _DocObject;
        private string _DocScopes;
        private byte _GrantLevelId;
        private string _PerformMethod;
        private byte _NumDossier;
        private string _Fee;
        private string _ElementDossier;
        private string _SettlementTime;
        private string _Result;
        private DateTime _ConfirmSignerDate;
        private byte _DocGroupId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _UpdUserId;
        private DateTime _UpdDateTime;
        private int _RevUserId;
        private DateTime _RevDateTime;
        private byte _DocTypeId;
        private int _DownloadCount;
        private int _SumByStatus1;
        private int _SumByStatus2;
        private int _SumByStatus3;
        private int _SumBySource1;
        private int _SumBySource2;
        private int _SumByDownload1;
        private int _SumByDownload2;
        private int _SumByView1;
        private int _SumByView2;
        private string _FieldsText;
        private string _SignersText;
        private string _OrgansText;
        private string _DocTypesText;
        private string _EffectStatusName;
        private string _DocUrl;
        
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private string _H1Tag;
        private string _SeoHeader;
        private string _SeoFooter;
        private string _SocialTitle;
        private string _SocialDesc;
        private string _SocialImage;
        public string DocUrl
        {
            get { return _DocUrl.StartsWith("/") || _DocUrl.StartsWith("http://") || _DocUrl.StartsWith("https://") ? _DocUrl : CmsConstants.ROOT_PATH + _DocUrl; }
            set { _DocUrl = value; }
        }

        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }
        public string H1Tag { get { return _H1Tag; } set { _H1Tag = value; } }
        public string SeoHeader { get { return _SeoHeader; } set { _SeoHeader = value; } }
        public string SeoFooter { get { return _SeoFooter; } set { _SeoFooter = value; } }
        public string SocialTitle { get { return _SocialTitle; } set { _SocialTitle = value; } }
        public string SocialDesc { get { return _SocialDesc; } set { _SocialDesc = value; } }
        public string SocialImage { get { return _SocialImage; } set { _SocialImage = value; } }

        public string EffectStatusName
        {
            get { return _EffectStatusName; }
            set { _EffectStatusName = value; }
        }

        public short IssueYear
        {
            get { return _issueYear; }
            set { _issueYear = value; }
        }

        private DBAccess db;
        private short _issueYear;
        private int _soLuong;

        //-----------------------------------------------------------------
        public DocsView()
        {

        }
        //-----------------------------------------------------------------        
        public DocsView(string constr)
        {

        }
        //-----------------------------------------------------------------
        ~DocsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        /// <summary>
        /// Gets or sets Id Ngôn ngữ.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu byte.
        /// </value>
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        /// <summary>
        /// Gets or sets Id Văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu int.
        /// </value>
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets DocGUId.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocGUId
        {
            get { return _DocGUId; }
            set { _DocGUId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Tên văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Số hiệu văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocIdentity
        {
            get { return _DocIdentity; }
            set { _DocIdentity = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Số hiệu văn bản lọc bỏ dấu.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocIdentityClear
        {
            get { return _DocIdentityClear; }
            set { _DocIdentityClear = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Tríc dẫn văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocSummary
        {
            get { return _DocSummary; }
            set { _DocSummary = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Nội dung văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string DocContent
        {
            get { return _DocContent; }
            set { _DocContent = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày ban hành.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày có hiệu lực.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày hết hiệu lực.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Số công báo.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu string.
        /// </value>
        public string GazetteNumber
        {
            get { return _GazetteNumber; }
            set { _GazetteNumber = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày công báo.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime GazetteDate
        {
            get { return _GazetteDate; }
            set { _GazetteDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Id Nguồn văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu short.
        /// </value>
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Id trạng thái sử dụng.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu byte.
        /// </value>
        public byte UseStatusId
        {
            get { return _UseStatusId; }
            set { _UseStatusId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Id trạng thái hiệu lực.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu byte.
        /// </value>
        public byte EffectStatusId
        {
            get { return _EffectStatusId; }
            set { _EffectStatusId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Id trạng thái duyệt.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu byte.
        /// </value>
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        public string DocObject
        {
            get
            {
                return _DocObject;
            }
            set
            {
                _DocObject = value;
            }
        }
        public string DocScopes
        {
            get
            {
                return _DocScopes;
            }
            set
            {
                _DocScopes = value;
            }
        }
        public byte GrantLevelId
        {
            get
            {
                return _GrantLevelId;
            }
            set
            {
                _GrantLevelId = value;
            }
        }
        public string PerformMethod
        {
            get
            {
                return _PerformMethod;
            }
            set
            {
                _PerformMethod = value;
            }
        }
        public byte NumDossier
        {
            get
            {
                return _NumDossier;
            }
            set
            {
                _NumDossier = value;
            }
        }
        public string Fee
        {
            get
            {
                return _Fee;
            }
            set
            {
                _Fee = value;
            }
        }
        public string ElementDossier
        {
            get
            {
                return _ElementDossier;
            }
            set
            {
                _ElementDossier = value;
            }
        }
        public string SettlementTime
        {
            get
            {
                return _SettlementTime;
            }
            set
            {
                _SettlementTime = value;
            }
        }
        public string Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }
        public DateTime ConfirmSignerDate
        {
            get
            {
                return _ConfirmSignerDate;
            }
            set
            {
                _ConfirmSignerDate = value;
            }
        }
        public byte DocGroupId
        {
            get
            {
                return _DocGroupId;
            }
            set
            {
                _DocGroupId = value;
            }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Id người tạo.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu int.
        /// </value>
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày tạo.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        public int UpdUserId
        {
            get { return _UpdUserId; }
            set { _UpdUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime UpdDateTime
        {
            get { return _UpdDateTime; }
            set { _UpdDateTime = value; }
        }
        //-----------------------------------------------------------------

        public int RevUserId
        {
            get { return _RevUserId; }
            set { _RevUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime RevDateTime
        {
            get { return _RevDateTime; }
            set { _RevDateTime = value; }
        }
        //-----------------------------------------------------------------
        public int DownloadCount
        {
            get { return _DownloadCount; }
            set { _DownloadCount = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus1
        {
            get { return _SumByStatus1; }
            set { _SumByStatus1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus2
        {
            get { return _SumByStatus2; }
            set { _SumByStatus2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus3
        {
            get { return _SumByStatus3; }
            set { _SumByStatus3 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource1
        {
            get { return _SumBySource1; }
            set { _SumBySource1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource2
        {
            get { return _SumBySource2; }
            set { _SumBySource2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload1
        {
            get { return _SumByDownload1; }
            set { _SumByDownload1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload2
        {
            get { return _SumByDownload2; }
            set { _SumByDownload2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView1
        {
            get { return _SumByView1; }
            set { _SumByView1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView2
        {
            get { return _SumByView2; }
            set { _SumByView2 = value; }
        }
        //-----------------------------------------------------------------

        /// <summary>
        /// Gets or sets Id loại văn bản.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu byte.
        /// </value>
        public byte DocTypeId
        {
            get { return _DocTypeId; }
            set { _DocTypeId = value; }
        }

        public string FieldsText
        {
            get { return _FieldsText; }
            set { _FieldsText = value; }
        }

        public string SignersText
        {
            get { return _SignersText; }
            set { _SignersText = value; }
        }

        public string OrgansText
        {
            get { return _OrgansText; }
            set { _OrgansText = value; }
        }
        public string DocTypesText
        {
            get { return _DocTypesText; }
            set { _DocTypesText = value; }
        }

        public string MetaTitle
        {
            get
            {
                return _MetaTitle;
            }

            set
            {
                _MetaTitle = value;
            }
        }

        public string MetaDesc
        {
            get
            {
                return _MetaDesc;
            }

            set
            {
                _MetaDesc = value;
            }
        }

        public string MetaKeyword
        {
            get
            {
                return _MetaKeyword;
            }

            set
            {
                _MetaKeyword = value;
            }
        }

        //-------------------------------------------------------------- 
        public string TruncateDocName(int mLengthRemain)
        {
            if (!string.IsNullOrEmpty(_DocName))
            {
                string RetVal = _DocName;
                if (_DocName.Length > mLengthRemain)
                {
                    RetVal = _DocName.Substring(0, mLengthRemain);
                    string nextChar = _DocName.Substring(mLengthRemain, 1);
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
            string RetVal = this.DocUrl;
            if (!RetVal.StartsWith("http://") && !RetVal.StartsWith("https://") && !RetVal.StartsWith("/")) RetVal = string.Concat(CmsConstants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetDocUrl(string TabName)
        {
            string RetVal = this.DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http://") && !RetVal.StartsWith("https://") && !RetVal.StartsWith("/")) RetVal = string.Concat(CmsConstants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetDocUrl(string DocUrl, string TabName)
        {
            string RetVal = DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http://") && !RetVal.StartsWith("https://") && !RetVal.StartsWith("/")) RetVal = string.Concat(CmsConstants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetDownloadUrl(string MediaDomain,string DocUrl, string FilePath)
        {
            string RetVal = MediaDomain;
            string[] l_Url = DocUrl.Split('/');
            string docUrlNotPath = "";
            for(int index=0; index < l_Url.Length; index++)
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
            string RetVal = sms.utils.StringUtil.RemoveSign4VietnameseString(DocName);
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
            if (!RetVal.StartsWith("http://") && !RetVal.StartsWith("https://") && !RetVal.StartsWith("/")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }

        public static string GetDocUrl(int DocId, string DocName, string UrlType)
        {
            string RetVal = sms.utils.StringUtil.RemoveSign4VietnameseString(DocName);
            RetVal = RetVal.Replace(" ", "-");
            RetVal = RetVal.Replace("/", "-");
            RetVal = RetVal.Replace("?", "");
            RetVal = RetVal.Replace(":", "");
            RetVal = RetVal.Replace(",", "");
            while (RetVal.Contains("--"))
            {
                RetVal = RetVal.Replace("--", "-");
            }
            RetVal = string.Concat(RetVal , "-" , DocId, "-", UrlType, ConstantHelpers.RewriteExt) ;
            if (!RetVal.StartsWith("http://") && !RetVal.StartsWith("https://") && !RetVal.StartsWith("/")) RetVal = string.Concat(CmsConstants.ROOT_PATH, RetVal);
            return RetVal;
        }

        private List<Docs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Docs> l_Docs = new List<Docs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Docs m_Docs = new Docs(db.ConnectionString);
                    m_Docs.LanguageId = smartReader.GetByte("LanguageId");
                    m_Docs.DocId = smartReader.GetInt32("DocId");
                    m_Docs.DocGUId = smartReader.GetString("DocGUId");
                    m_Docs.DocName = smartReader.GetString("DocName");
                    m_Docs.DocIdentity = smartReader.GetString("DocIdentity");
                    m_Docs.DocIdentityClear = smartReader.GetString("DocIdentityClear");
                    m_Docs.DocSummary = smartReader.GetString("DocSummary");
                    m_Docs.DocContent = smartReader.GetString("DocContent");
                    m_Docs.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_Docs.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_Docs.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_Docs.ExpireDate = smartReader.GetDateTime("ExpireDate");
                    m_Docs.GazetteNumber = smartReader.GetString("GazetteNumber");
                    m_Docs.GazetteDate = smartReader.GetDateTime("GazetteDate");
                    m_Docs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Docs.UseStatusId = smartReader.GetByte("UseStatusId");
                    m_Docs.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_Docs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Docs.DocObject = smartReader.GetString("DocObject");
                    m_Docs.DocScopes = smartReader.GetString("DocScopes");
                    m_Docs.GrantLevelId = smartReader.GetByte("GrantLevelId");
                    m_Docs.PerformMethod = smartReader.GetString("PerformMethod");
                    m_Docs.NumDossier = smartReader.GetByte("NumDossier");
                    m_Docs.Fee = smartReader.GetString("Fee");
                    m_Docs.ElementDossier = smartReader.GetString("ElementDossier");
                    m_Docs.SettlementTime = smartReader.GetString("SettlementTime");
                    m_Docs.Result = smartReader.GetString("Result");
                    m_Docs.ConfirmSignerDate = smartReader.GetDateTime("ConfirmSignerDate");
                    m_Docs.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_Docs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Docs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Docs.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_Docs.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_Docs.RevUserId = smartReader.GetInt32("RevUserId");
                    m_Docs.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    m_Docs.IssueYear = smartReader.GetInt16("IssueYear");
                    l_Docs.Add(m_Docs);
                }
                reader.Close();
                return l_Docs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        /// <summary>
        /// GetPage Lấy ra từng trang kiểu List.
        /// </summary>
        /// <param name="ActUserId">Xác định hành động của UserId nào.</param>
        /// <param name="RowAmount">Số dòng cần lấy.</param>
        /// <param name="PageIndex">Index bắt đầu bằng 0.</param>
        /// <param name="OrderBy">Sắp xếp theo.</param>
        /// <param name="LanguageId">Id ngôn ngữ.</param>
        /// <param name="SearchKeyword">Từ khóa cần tìm kiếm.</param>
        /// <param name="DocId">Id văn bản.</param>
        /// <param name="DocGUId">GUId của văn bản dùng hiển thị ngoài trang chủ ko để lộ Id văn bản.</param>
        /// <param name="DocName">Tên văn bản.</param>
        /// <param name="DocIdentity">Số hiệu văn bản.</param>
        /// <param name="DocTypeId">Kiểu văn bản: như luật, Quyết định,...</param>
        /// <param name="DataSourceId">Nguồn văn bản.</param>
        /// <param name="FieldId">Id lĩnh vực.</param>
        /// <param name="OrganId">Id Cơ quan ban hành.</param>
        /// <param name="SignerId">Id Người ký.</param>
        /// <param name="DocRelateId">Id văn bản liên quan.</param>
        /// <param name="DisplayTypeId">Id kiểu hiển thị.</param>
        /// <param name="UseStatusId">Id trạng thái sử dụng.</param>
        /// <param name="EffectStatusId">Id trạng thái hiệu lực.</param>
        /// <param name="ReviewStatusId">Id trạng thái duyệt.</param>
        /// <param name="CrUserId">Id người tạo.</param>
        /// <param name="SearchByDate">Tìm kiếm theo kiểu ngày như: CrDateTime,...</param>
        /// <param name="DateFrom">Từ ngày.</param>
        /// <param name="DateTo">Đến ngày.</param>
        /// <param name="RowCount">Tổng số dòng.</param>
        /// <param name="DocGroupId">Thêm docgroupid.</param>
        /// <returns> Trả về kiểu List </returns>
        public List<Docs> MyDocs_View_GetPage(int ActUserId, int RowAmount, int PageIndex, int DocId, string DocName, string DocIdentity, short FieldId, byte EffectStatusId,
                                  byte DocTypeId, short OrganId, short RegistTypeId, int CustomerId, byte OrganTypeId,string OrderBy,short DocGroupId,string SearchKeyword,
                                  byte HighlightKeyword, string SearchByDate,string DateFrom, string DateTo, ref int RowCount)
          {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewMyDocs");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        // get top docs view
        public List<Docs> Docs_GetTopView(byte LanguageId, byte DocGroupId, byte DocTypeId, int CustomerId, byte RegistTypeId, int RowAmount, string OrderBy, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetTopView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = InitDocs(smartReader);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        private List<Docs> InitDocs(SmartDataReader smartReader)
        {
            List<Docs> l_Docs = new List<Docs>();
            Docs m_Docs;
            try
            {
                while (smartReader.Read())
                {
                    m_Docs = new Docs();
                    m_Docs.LanguageId = smartReader.GetByte("LanguageId");
                    m_Docs.DocId = smartReader.GetInt32("DocId");
                    m_Docs.DocGUId = smartReader.GetString("DocGUId");
                    m_Docs.DocName = smartReader.GetString("DocName");
                    m_Docs.DocIdentity = smartReader.GetString("DocIdentity");
                    m_Docs.DocIdentityClear = smartReader.GetString("DocIdentityClear");
                    m_Docs.DocSummary = smartReader.GetString("DocSummary");
                    m_Docs.DocContent = smartReader.GetString("DocContent");
                    m_Docs.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_Docs.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_Docs.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_Docs.ExpireDate = smartReader.GetDateTime("ExpireDate");
                    m_Docs.GazetteNumber = smartReader.GetString("GazetteNumber");
                    m_Docs.GazetteDate = smartReader.GetDateTime("GazetteDate");
                    m_Docs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Docs.UseStatusId = smartReader.GetByte("UseStatusId");
                    m_Docs.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_Docs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Docs.DocObject = smartReader.GetString("DocObject");
                    m_Docs.DocScopes = smartReader.GetString("DocScopes");
                    m_Docs.GrantLevelId = smartReader.GetByte("GrantLevelId");
                    m_Docs.PerformMethod = smartReader.GetString("PerformMethod");
                    m_Docs.NumDossier = smartReader.GetByte("NumDossier");
                    m_Docs.Fee = smartReader.GetString("Fee");
                    m_Docs.ElementDossier = smartReader.GetString("ElementDossier");
                    m_Docs.SettlementTime = smartReader.GetString("SettlementTime");
                    m_Docs.Result = smartReader.GetString("Result");
                    m_Docs.ConfirmSignerDate = smartReader.GetDateTime("ConfirmSignerDate");
                    m_Docs.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_Docs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Docs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Docs.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_Docs.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_Docs.RevUserId = smartReader.GetInt32("RevUserId");
                    m_Docs.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    l_Docs.Add(m_Docs);
                }
                return l_Docs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}