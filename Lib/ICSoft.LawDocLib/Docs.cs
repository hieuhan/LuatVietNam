
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
    /// class Docs
    /// </summary>
    public class Docs
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
        private string _DocUrl;
        private string _DocScopes;
        private byte _GrantLevelId;
        private string _PerformMethod;
        private byte _NumDossier;
        private string _Fee;
        private string _ElementDossier;
        private string _SettlementTime;
        private string _Result;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private string _H1Tag;
        private string _SeoHeader;
        private string _SeoFooter;
        private string _SocialTitle;
        private string _SocialDesc;
        private string _SocialImage;
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

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string OrganName
        {
            get { return _organName; }
            set { _organName = value; }
        }

        public byte HasContent
        {
            get { return _hasContent; }
            set { _hasContent = value; }
        }

        public byte HasFile
        {
            get { return _hasFile; }
            set { _hasFile = value; }
        }

        public byte HasDocIndex
        {
            get { return _hasDocIndex; }
            set { _hasDocIndex = value; }
        }

        public byte HasDocRelate
        {
            get { return _hasDocRelate; }
            set { _hasDocRelate = value; }
        }

        public short IssueYear
        {
            get { return _issueYear; }
            set { _issueYear = value; }
        }

        private DBAccess db;

        private short _issueYear;
        private byte _hasContent;
        private byte _hasFile;
        private byte _hasDocIndex;
        private byte _hasDocRelate;
        private string _fieldName;
        private string _organName;

        //-----------------------------------------------------------------
        public Docs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Docs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Docs()
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
        public string DocUrl
        {
            get
            {
                return _DocUrl;
            }
            set
            {
                _DocUrl = value;
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
        public string MetaTitle { get { return _MetaTitle; } set { _MetaTitle = value; } }
        public string MetaDesc { get { return _MetaDesc; } set { _MetaDesc = value; } }
        public string MetaKeyword { get { return _MetaKeyword; } set { _MetaKeyword = value; } }
        public string H1Tag { get { return _H1Tag; } set { _H1Tag = value; } }
        public string SeoHeader { get { return _SeoHeader; } set { _SeoHeader = value; } }
        public string SeoFooter { get { return _SeoFooter; } set { _SeoFooter = value; } }
        public string SocialTitle { get { return _SocialTitle; } set { _SocialTitle = value; } }
        public string SocialDesc { get { return _SocialDesc; } set { _SocialDesc = value; } }
        public string SocialImage { get { return _SocialImage; } set { _SocialImage = value; } }

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
        /// <summary>
        /// Trả dữ liệu về kiểu List.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Data error:  + err.Message</exception>
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
                    m_Docs.DocUrl = smartReader.GetString("DocUrl");
                    m_Docs.DocScopes = smartReader.GetString("DocScopes");
                    m_Docs.GrantLevelId = smartReader.GetByte("GrantLevelId");
                    m_Docs.PerformMethod = smartReader.GetString("PerformMethod");
                    m_Docs.NumDossier = smartReader.GetByte("NumDossier");
                    m_Docs.Fee = smartReader.GetString("Fee");
                    m_Docs.ElementDossier = smartReader.GetString("ElementDossier");
                    m_Docs.SettlementTime = smartReader.GetString("SettlementTime");
                    m_Docs.Result = smartReader.GetString("Result");
                    m_Docs.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Docs.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Docs.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Docs.H1Tag = smartReader.GetString("H1Tag");
                    m_Docs.SeoHeader = smartReader.GetString("SeoHeader");
                    m_Docs.SeoFooter = smartReader.GetString("SeoFooter");
                    m_Docs.SocialTitle = smartReader.GetString("SocialTitle");
                    m_Docs.SocialDesc = smartReader.GetString("SocialDesc");
                    m_Docs.SocialImage = smartReader.GetString("SocialImage");
                    m_Docs.ConfirmSignerDate = smartReader.GetDateTime("ConfirmSignerDate");
                    m_Docs.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_Docs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Docs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Docs.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_Docs.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_Docs.RevUserId = smartReader.GetInt32("RevUserId");
                    m_Docs.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    m_Docs.IssueYear = smartReader.GetInt16("IssueYear");
                    m_Docs.HasContent = smartReader.GetByte("HasContent");
                    m_Docs.HasFile = smartReader.GetByte("HasFile");
                    m_Docs.HasDocIndex = smartReader.GetByte("HasDocIndex");
                    m_Docs.HasDocRelate = smartReader.GetByte("HasDocRelate");
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
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm mới văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Sửa văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm và sửa Văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocName", this.DocName));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", this.DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@DocIdentityClear", this.DocIdentityClear));                
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
                if (this.IssueDate == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", this.IssueDate));

                if (this.ExpireDate == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@ExpireDate", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ExpireDate", this.ExpireDate));

                if (this.EffectDate == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@EffectDate", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@EffectDate", this.EffectDate));

                cmd.Parameters.Add(new SqlParameter("@GazetteNumber", this.GazetteNumber));
                if (this.GazetteDate == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@GazetteDate", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@GazetteDate", this.GazetteDate));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", this.UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", this.EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocObject", this.DocObject));
                cmd.Parameters.Add(new SqlParameter("@DocScopes", this.DocScopes));
                cmd.Parameters.Add(new SqlParameter("@GrantLevelId", this.GrantLevelId));
                cmd.Parameters.Add(new SqlParameter("@PerformMethod", this.PerformMethod));
                cmd.Parameters.Add(new SqlParameter("@NumDossier", this.NumDossier));
                cmd.Parameters.Add(new SqlParameter("@Fee", this.Fee));
                cmd.Parameters.Add(new SqlParameter("@ElementDossier", this.ElementDossier));                
                cmd.Parameters.Add(new SqlParameter("@SettlementTime", this.SettlementTime));
                cmd.Parameters.Add(new SqlParameter("@Result", this.Result));
                if (this.ConfirmSignerDate > DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@ConfirmSignerDate", this.ConfirmSignerDate));
                else
                    cmd.Parameters.Add(new SqlParameter("@ConfirmSignerDate", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocId = Convert.ToInt32((cmd.Parameters["@DocId"].Value == null) ? 0 : (cmd.Parameters["@DocId"].Value));
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                //RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật nội dung văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte UpdateContent(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateContent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocContent", this.DocContent));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public byte InsertContent(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_InsertContent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocContent", this.DocContent));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật trạng thái hiển thị văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật trích dẫn văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte UpdateSummary(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateSummary");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocSummary", this.DocSummary));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public void UpdateResult_Quick(int ActUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateResult_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@Result", this.Result));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật seo văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte UpdateSeo(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateSeo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoHeader", this.SeoHeader));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add(new SqlParameter("@SocialTitle", this.SocialTitle));
                cmd.Parameters.Add(new SqlParameter("@SocialDesc", this.SocialDesc));
                cmd.Parameters.Add(new SqlParameter("@SocialImage", this.SocialImage));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public byte UpdateUrl(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateUrl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocUrl", this.DocUrl));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật trích dẫn và nội dung của văn bản.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte UpdateSummaryAndContent(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateSummaryAndContent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocSummary", this.DocSummary));
                cmd.Parameters.Add(new SqlParameter("@DocContent", this.DocContent));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        public byte UpdateInfo(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_UpdateInfo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocObject", this.DocObject));
                cmd.Parameters.Add(new SqlParameter("@DocScopes", this.DocScopes));
                cmd.Parameters.Add(new SqlParameter("@GrantLevelId", this.GrantLevelId));
                cmd.Parameters.Add(new SqlParameter("@PerformMethod", this.PerformMethod));
                cmd.Parameters.Add(new SqlParameter("@NumDossier", this.NumDossier));
                cmd.Parameters.Add(new SqlParameter("@Fee", this.Fee));
                cmd.Parameters.Add(new SqlParameter("@ElementDossier", this.ElementDossier));
                cmd.Parameters.Add(new SqlParameter("@SettlementTime", this.SettlementTime));
                cmd.Parameters.Add(new SqlParameter("@Result", this.Result));
                cmd.Parameters.Add(new SqlParameter("@ConfirmSignerDate", this.ConfirmSignerDate));                
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        /// <summary>
        /// Deletes - Xóa văn bản bời Id ngôn ngữ và Id của văn bản cần xóa.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        /// <summary>
        /// Deletes - Xóa văn bản bời Id ngôn ngữ và Id của văn bản cần xóa.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">Lưu hành động của UserId nào.</param>
        /// <param name="SysMessageId">Id thông báo lỗi.</param>
        /// <returns></returns>
        public DataSet Docs_GetTopByDocIdentity(int ActUserId, int RowAmount, string OrderBy, byte LanguageId, string DocIdentity, byte ReviewStatusId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetTopByDocIdentity");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public List<Docs> Docs_GetListByDocIdentity(int ActUserId, int RowAmount, string OrderBy, byte LanguageId, string DocIdentity, byte ReviewStatusId)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetTopByDocIdentity");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy về kiểu List 1 văn bản luật.
        /// </summary>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        public List<Docs> GetListByDocId(int DocId, byte LanguageId)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string SearchKeyword="";
                byte IsSearchExact = 1;
                byte HighlightKeyword = 1;
                string DocGUId= "";
                string DocName="";
                string DocIdentity="";
                byte DocTypeId=0;
                short DataSourceId=0;
                short FieldId=0;
                byte OrganTypeId = 0;
                short OrganId=0;
                short SignerId=0;
                int DocRelateId =0;
                short DisplayTypeId=0;
                byte UseStatusId=0;
                byte EffectStatusId=0;
                byte ReviewStatusId=0;
                byte HasDocFile = 0;
                int CrUserId=0;
                string SearchByDate="";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, SearchKeyword, IsSearchExact, HighlightKeyword, DocId, DocGUId, 
                                  DocName, DocIdentity, DocTypeId, DataSourceId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId, 
                                  DisplayTypeId, UseStatusId, EffectStatusId, ReviewStatusId, HasDocFile, CrUserId, SearchByDate, DateFrom, 
                                  DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy về kiểu List 1 văn bản luật.
        /// </summary>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        public List<Docs> GetListByDocGUId(string DocGUId, byte LanguageId)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string SearchKeyword = "";
                byte IsSearchExact = 1;
                byte HighlightKeyword = 1;
                int DocId = 0;
                string DocName = "";
                string DocIdentity = "";
                byte DocTypeId = 0;
                short DataSourceId = 0;
                short FieldId = 0;
                byte OrganTypeId = 0;
                short OrganId = 0;
                short SignerId = 0;
                int DocRelateId = 0;
                short DisplayTypeId = 0;
                byte UseStatusId = 0;
                byte EffectStatusId = 0;
                byte ReviewStatusId = 0;
                byte HasDocFile = 0;
                int CrUserId = 0;
                string SearchByDate = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, SearchKeyword, IsSearchExact, HighlightKeyword, DocId, DocGUId,
                                  DocName, DocIdentity, DocTypeId, DataSourceId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId,
                                  DisplayTypeId, UseStatusId, EffectStatusId, ReviewStatusId, HasDocFile, CrUserId, SearchByDate, DateFrom,
                                  DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy về kiểu List 1 văn bản luật.
        /// </summary>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        //-------------------------------------------------------------- 
        public Docs GetByDocGUId(string DocGUId, byte LanguageId)
        {
            Docs RetVal = new Docs(db.ConnectionString);
            try
            {
                List<Docs> list = GetListByDocGUId(DocGUId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Docs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy 1 bản ghi văn bản luật.
        /// </summary>
        /// <returns></returns>
        public Docs GetByDocGUId()
        {
            return GetByDocGUId(this.DocGUId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy 1 bản ghi văn bản luật.
        /// </summary>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        public Docs Get(int DocId, byte LanguageId)
        {
            Docs RetVal = new Docs(db.ConnectionString);
            try
            {
                List<Docs> list = GetListByDocId(DocId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Docs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy 1 bản ghi văn bản luật.
        /// </summary>
        /// <returns></returns>
        public Docs Get()
        {
            return Get(this.DocId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy tĩnh 1 bản ghi văn bản.
        /// </summary>
        /// <param name="Constr">Chuỗi kết nối Database.</param>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        public static Docs Static_Get(string Constr, int DocId, byte LanguageId)
        {
            Docs m_Docs = new Docs(Constr);
            return m_Docs.Get(DocId, LanguageId);
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Lấy tĩnh 1 bản ghi văn bản.
        /// </summary>
        /// <param name="DocId">Id văn bản luật.</param>
        /// <param name="LanguageId">Id Ngôn ngữ.</param>
        /// <returns></returns>
        public static Docs Static_Get(int DocId, byte LanguageId)
        {
            return Static_Get("", DocId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public DataSet Docs_GetProperties(int ActUserId, byte LanguageId, int DocId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetProperties");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByCrDateTime(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId, 
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, 
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByCrDateTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByCrUserId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByCrUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByDataSourceId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByDataSourceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByDocTypeId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByDocTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByEffectStatusId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByEffectStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        public DataSet StatisticByReviewStatus(ref int TotalCount, byte DocGroupId =0, int ActUserId=1, byte LanguageId=0, string SearchKeyword="", int DocId=0, string DocName="", string DocIdentity="", byte DocTypeId=0,
                                                   short DataSourceId=0, short FieldId = 0, short OrganId = 0, short SignerId = 0, byte UseStatusId = 0, byte EffectStatusId = 0, byte ReviewStatusId = 0,
                                                   int CrUserId = 0, string SearchByDate="", string DateFrom="", string DateTo="")
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByReviewStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet StatisticByReviewStatusV2(ref int TotalCount, byte DocGroupId = 0, int ActUserId = 1, byte LanguageId = 0,
            string SearchKeyword = "", int DocId = 0, string DocName = "", string DocIdentity = "", byte DocTypeId = 0,
            short DataSourceId = 0, short FieldId = 0, short OrganId = 0, short SignerId = 0, byte UseStatusId = 0, byte EffectStatusId = 0,
            byte ReviewStatusId = 0, int CrUserId = 0, int UpdUserId = 0, int RevUserId = 0, string SearchByDate = "", string DateFrom = "", string DateTo = "", int PageIndex = 0, 
            string OrderBy = "",string DocGUId="",byte IsSearchExact = 1, byte HighlightKeyword = 1, byte OrganTypeId = 0, short ProvinceId = 0,
             int DocRelateId = 0, short DisplayTypeId = 0,string ReviewStatusIdList = "", byte HasDocFile = 0)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByReviewStatusV2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));

                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusIdList", ReviewStatusIdList));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));


                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByFieldId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByFieldId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByIssueYear(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByIssueYear");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByOrganId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByOrganId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Docs_StatisticByUseStatusId(int ActUserId, byte LanguageId, string SearchKeyword, int DocId, string DocName, string DocIdentity, byte DocTypeId,
                                                   short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId,
                                                   int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_StatisticByUseStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
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
        public List<Docs> DocViewCount_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, int DocId, string DocName, 
                                                string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, short OrganId, short SignerId, byte UseStatusId, 
                                                byte EffectStatusId, byte ReviewStatusId, int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
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
        /// <returns> Trả về kiểu List </returns>
        public List<Docs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, 
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, string SearchByDate, 
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId)); 
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                //add 
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
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
        public List<Docs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, int CustomerId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount )
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
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

        //24/05/2017 GetPage bo xung them dk: UpdUserId, RevUserId, IssueDate
        public List<Docs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, int UpdUserId, int RevUserId, int IssueDate, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));

                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                //add 
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
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

        //16/11/2017 GetPage bo xung them dk: UpdUserId, RevUserId, IssueDate, FileTypeId
        public List<Docs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, byte FileTypeId, int CrUserId, int UpdUserId, int RevUserId, int IssueDate, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@FileTypeId", FileTypeId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));

                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                //add 
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
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

        //24/05/2017 Docs_GetPageWithProvince bo xung them dk: UpdUserId, RevUserId, IssueDate
        public List<Docs> GetPageWithProvince(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, short ProvinceId, byte HasDocFile, int CrUserId, int UpdUserId, int RevUserId, int IssueDate, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPageWithProvince");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));

                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                //add 
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
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
        /// <summary>
        /// GetPage Lấy ra từng trang kiểu List (Chỉ lấy văn bản quy pham pháp luật)
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
        public List<Docs> GetPageNotDispatch(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPageNotDispatch");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
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
        /// <summary>
        /// GetPage Lấy ra từng trang kiểu List (Chỉ lấy văn bản quy pham pháp luật)
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
        public List<Docs> GetPageNotSummaryNull(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPageNotSummaryNull");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
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
        /// <returns> Trả về kiểu List </returns>
        public List<Docs> GetPageWithStatusList(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int DocId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, string ReviewStatusIdList, byte HasDocFile, int CrUserId, int UpdUserId, int RevUserId, string SearchByDate,
                                  string DateFrom, string DateTo, short ProvinceId, byte DocGroupId, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetPageWithStatusList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusIdList", ReviewStatusIdList));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
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
        //--------------------------------------------------------------     
        /// <summary>
        /// Search tìm kiếm theo các tiêu chí.
        /// </summary>
        /// <param name="ActUserId">Xác định hành động của UserId nào.</param>
        /// <param name="RowAmount">Số dòng cần lấy.</param>
        /// <param name="PageIndex">Index bắt đầu bằng 0.</param>
        /// <param name="OrderBy">Sắp xếp theo.</param>
        /// <param name="LanguageId">Id ngôn ngữ.</param>
        /// <param name="SearchKeyword">Từ khóa cần tìm kiếm.</param>
        /// <param name="DocId">Id văn bản.</param>
        /// <param name="DocGUId">GUId của văn bản dùng hiển thị ngoài trang chủ ko để lộ ID văn bản.</param>
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
        public List<Docs> Search(int ActUserId, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword, int DocId, string DocGUId,
                                  string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
                                  short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, string SearchByDate, 
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, SearchKeyword, IsSearchExact, HighlightKeyword, DocId, DocGUId, DocName, DocIdentity, DocTypeId, 
                            DataSourceId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId, DisplayTypeId, UseStatusId, EffectStatusId, ReviewStatusId, HasDocFile, CrUserId, SearchByDate, 
                            DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------  
        public string DocFields_GetFieldName(byte LanguageId, int DocId, ref string FieldName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_GetFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add("@FieldName", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@FieldName"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string DocOrgans_GetOrganName(byte LanguageId, int DocId, ref string OrganName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_GetOrganName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add("@OrganName", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@OrganName"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Docs Docs_GetSeos(int DocId, byte LanguageId)
        {
            try
            {
                List<Docs> lDocs = new List<Docs>();
                SqlCommand cmd = new SqlCommand("Docs_GetSeos");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                lDocs = Init(cmd);
                if (lDocs.Count > 0)
                {
                    return lDocs[0];
                }
                return new Docs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        private List<Docs> Docs_ReportDetail_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Docs> lDocDetailReport = new List<Docs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Docs mDocDetailReport = new Docs(db.ConnectionString)
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        DocName = smartReader.GetString("DocName"),
                        CrUserId = smartReader.GetInt32("CrUserId"),
                        CrDateTime = smartReader.GetDateTime("CrDateTime"),
                        UpdUserId = smartReader.GetInt32("UpdUserId"),
                        UpdDateTime = smartReader.GetDateTime("UpdDateTime"),
                        RevUserId = smartReader.GetInt32("RevUserId"),
                        RevDateTime = smartReader.GetDateTime("RevDateTime"),
                        DataSourceId = smartReader.GetInt16("DataSourceId"),
                        DownloadCount = smartReader.GetInt32("DownloadCount"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        IssueYear = smartReader.GetInt16("IssueYear"),
                        GazetteDate = smartReader.GetDateTime("GazetteDate"),
                        ReviewStatusId = smartReader.GetByte("ReviewStatusId"),
                        LanguageId = smartReader.GetByte("LanguageId"),
                        FieldName = smartReader.GetString("FieldName"),
                        OrganName = smartReader.GetString("OrganName")
                    };

                    lDocDetailReport.Add(mDocDetailReport);
                }
                reader.Close();
                return lDocDetailReport;
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
        /// Báo cáo văn bản chi tiết
        /// </summary>
        public List<Docs> Docs_ReportDetail_GetPage(int ActUserId, string SearchByDate, short FieldId, short OrganId, byte ReviewStatusId, int CrUserId, int RevUserId, short DataSourceId, string DateFrom, string DateTo, string OrderBy, int PageIndex, int RowAmount, ref int CountPending, ref int CountReviewed, ref int CountUnreview, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_ReportDetail_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RowCountPending", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RowCountReviewed", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RowCountUnreview", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Docs_ReportDetail_Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                CountPending = int.Parse(cmd.Parameters["@RowCountPending"].Value.ToString());
                CountReviewed = int.Parse(cmd.Parameters["@RowCountReviewed"].Value.ToString());
                CountUnreview = int.Parse(cmd.Parameters["@RowCountUnreview"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------  
        private List<Docs> Docs_ReportData_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Docs> lDocDetailReport = new List<Docs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Docs mDocDetailReport =
                        new Docs(db.ConnectionString)
                        {
                            DocGroupId = smartReader.GetByte("DocGroupId"),
                            DocTypeId = smartReader.GetByte("DocTypeId"),
                            DocName = smartReader.GetString("DataName"),
                            SumByStatus1 = smartReader.GetInt32("SumByStatus1"),
                            SumByStatus2 = smartReader.GetInt32("SumByStatus2"),
                            SumByStatus3 = smartReader.GetInt32("SumByStatus3"),
                            SumBySource1 = smartReader.GetInt32("SumBySource1"),
                            SumBySource2 = smartReader.GetInt32("SumBySource2"),
                            SumByDownload1 = smartReader.GetInt32("SumByDownload1"),
                            SumByDownload2 = smartReader.GetInt32("SumByDownload2"),
                            SumByView1 = smartReader.GetInt32("SumByView1"),
                            SumByView2 = smartReader.GetInt32("SumByView2")
                        };
                    lDocDetailReport.Add(mDocDetailReport);
                }
                reader.Close();
                return lDocDetailReport;
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
        /// Báo cáo tổng hợp văn bản
        /// </summary>
        public List<Docs> Docs_ReportData(string dateFrom, string dateTo, string dateFromSame, string dateToSame)
        {
            List<Docs> retVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_ReportData") {CommandType = CommandType.StoredProcedure};
                if (!string.IsNullOrEmpty(dateFrom)) cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(dateFrom)));
                if (!string.IsNullOrEmpty(dateTo)) cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(dateTo)));
                if (!string.IsNullOrEmpty(dateFromSame)) cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(dateFromSame)));
                if (!string.IsNullOrEmpty(dateToSame)) cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(dateToSame)));
                retVal = Docs_ReportData_Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        //---------------------------------------------------------------
        public int GetSiteMapPage(int rowAmount = 5000)
        {
            int retVal = 0;
            try
            {
                string sql = "SELECT COUNT(1) FROM Docs Where ReviewStatusId = 2 ";
                SqlCommand cmd = new SqlCommand(sql) {CommandType = CommandType.Text};
                retVal = db.ExecuteScalar(cmd);
                if (retVal % rowAmount > 0)
                    retVal = retVal / rowAmount + 1;
                else
                    retVal = retVal / rowAmount;
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return retVal;
        }

        //----Lay danh sach van ban trong noi dung chua the <head>-----------------------------------------------------------
        public List<Docs> GetDocContainsHeadMeta(int rowAmount = 1000)
        {
            List<Docs> retVal = new List<Docs>();
            try
            {
                string sql = "SELECT top " + rowAmount + " * FROM Docs WHERE CHARINDEX('<head>',DocContent) > 0";
                SqlCommand cmd = new SqlCommand(sql);
                retVal = Init(cmd);
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return retVal;
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Cập nhật trạng thái của văn bản
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <param name="hasContent">Có nội dung</param>
        /// <param name="hasFile">Có file đính kèm</param>
        /// <param name="hasDocIndex">Có mục lục</param>
        /// <param name="hasDocRelate">Có văn bản liên quan</param>
        /// <returns></returns>
        public byte Update_HasProperties(int docId, byte hasContent = 0, byte hasFile = 0, byte hasDocIndex = 0, byte hasDocRelate = 0)
        {
            byte retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_Update_HasPropeties") {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add(new SqlParameter("@HasContent", hasContent));
                cmd.Parameters.Add(new SqlParameter("@HasFile", hasFile));
                cmd.Parameters.Add(new SqlParameter("@HasDocIndex", hasDocIndex));
                cmd.Parameters.Add(new SqlParameter("@HasDocRelate", hasDocRelate));
                cmd.Parameters.Add(new SqlParameter("@DocId", docId));
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                retVal = Convert.ToByte(cmd.Parameters["@SysMessageTypeId"].Value ?? "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //---------------------------------------------------------------

    }
}