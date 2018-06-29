
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{

    /// <summary>
    /// class DocViewCount
    /// </summary>
    public class DocViewCount
    {
        private int _DocViewCountId;
        private int _ViewCount;
        private DateTime _LastViewTime;
        private byte _LanguageId;
        private int _DocId;
        private string _DocGUId;
        private string _DocName;
        private string _DocIdentity;
        private string _DocIdentityClear;
        private string _DocViewCountummary;
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
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _DocTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocViewCount()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocViewCount(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocViewCount()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocViewCountId
        {
            get { return _DocViewCountId; }
            set { _DocViewCountId = value; }
        }
        //-----------------------------------------------------------------    
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------    
        public DateTime LastViewTime
        {
            get { return _LastViewTime; }
            set { _LastViewTime = value; }
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
        public string DocViewCountummary
        {
            get { return _DocViewCountummary; }
            set { _DocViewCountummary = value; }
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
        /// <summary>
        /// Trả dữ liệu về kiểu List.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Data error:  + err.Message</exception>
        private List<DocViewCount> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocViewCount> l_DocViewCount = new List<DocViewCount>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocViewCount m_DocViewCount = new DocViewCount(db.ConnectionString);
                    m_DocViewCount.DocViewCountId = smartReader.GetInt32("DocViewCountId");
                    m_DocViewCount.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocViewCount.DocId = smartReader.GetInt32("DocId");
                    m_DocViewCount.DocGUId = smartReader.GetString("DocGUId");
                    m_DocViewCount.DocName = smartReader.GetString("DocName");
                    m_DocViewCount.ViewCount = smartReader.GetInt32("ViewCount");
                    m_DocViewCount.DocIdentity = smartReader.GetString("DocIdentity");
                    m_DocViewCount.DocIdentityClear = smartReader.GetString("DocIdentityClear");
                    m_DocViewCount.DocViewCountummary = smartReader.GetString("DocViewCountummary");
                    m_DocViewCount.DocContent = smartReader.GetString("DocContent");
                    m_DocViewCount.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocViewCount.LastViewTime = smartReader.GetDateTime("LastViewTime");
                    m_DocViewCount.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_DocViewCount.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_DocViewCount.ExpireDate = smartReader.GetDateTime("ExpireDate");
                    m_DocViewCount.GazetteNumber = smartReader.GetString("GazetteNumber");
                    m_DocViewCount.GazetteDate = smartReader.GetDateTime("GazetteDate");
                    m_DocViewCount.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_DocViewCount.UseStatusId = smartReader.GetByte("UseStatusId");
                    m_DocViewCount.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_DocViewCount.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocViewCount.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocViewCount.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_DocViewCount.Add(m_DocViewCount);
                }
                reader.Close();
                return l_DocViewCount;
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
        //--------------------------------------------------------------     
        /// <summary>
        /// DocViewCount_GetPage Lấy ra từng trang kiểu List.
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
        /// <returns> Trả về kiểu List </returns>
        public List<DocViewCount> DocViewCount_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, int DocId, string DocName, 
                                                string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, 
                                                byte EffectStatusId, byte ReviewStatusId, int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocViewCount> RetVal = new List<DocViewCount>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocViewCount_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));                
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
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
        //--------------------------------------------------------------     

    }
}