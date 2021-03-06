
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
    /// class DocRelates
    /// </summary>
    public class DocRelates
    {
        private int _DocRelateId;
        private int _DocReferenceId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _RevUserId;
        private DateTime _RevDateTime;
        private int _DocId;
        private byte _RelateTypeId;
        private string _DocName;
        private string _DocNameRef;
        private string _DocIdentity;
        private DateTime _IssueDate;
        private DateTime _EffectDate;
        private string _DocIdentityRef;
        private DateTime _IssueDateRef;
        private DateTime _EffectDateRef;
        private string _FieldDesc;
        private int _TotalDoc;
        private short _FieldId;
        private string _RelateTypeName;
        private DBAccess db;

        private byte _effectStatusIdRef;
        private byte _effectStatusId;
        private string _effectStatusName;
        private string _DisplayPosition;
        private string _DocUrl;
        private string _MetaTitle;
        private string _MetaTitleRef;

        private byte _docGroupId;
        private byte _languageId;
        private short _issueYear;

        //-----------------------------------------------------------------
        public DocRelates()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocRelates(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocRelates()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocRelateId
        {
            get { return _DocRelateId; }
            set { _DocRelateId = value; }
        }
        //-----------------------------------------------------------------
        public int DocReferenceId
        {
            get { return _DocReferenceId; }
            set { _DocReferenceId = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
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
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public byte RelateTypeId
        {
            get { return _RelateTypeId; }
            set { _RelateTypeId = value; }
        }

        public string RelateTypeName
        {
            get { return _RelateTypeName; }
            set { _RelateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }
        //-----------------------------------------------------------------
        public string DocNameRef
        {
            get { return _DocNameRef; }
            set { _DocNameRef = value; }
        }
        //-----------------------------------------------------------------
        public string DocIdentity
        {
            get { return _DocIdentity; }
            set { _DocIdentity = value; }
        }
        //-----------------------------------------------------------------
        public string DocIdentityRef
        {
            get { return _DocIdentityRef; }
            set { _DocIdentityRef = value; }
        }
        //-----------------------------------------------------------------
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        //-----------------------------------------------------------------
        public DateTime IssueDateRef
        {
            get { return _IssueDateRef; }
            set { _IssueDateRef = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EffectDateRef
        {
            get { return _EffectDateRef; }
            set { _EffectDateRef = value; }
        }
        //-----------------------------------------------------------------
        public string FieldDesc
        {
            get { return _FieldDesc; }
            set { _FieldDesc = value; }
        }
        //-----------------------------------------------------------------
        public int TotalDoc
        {
            get { return _TotalDoc; }
            set { _TotalDoc = value; }
        }
        //-----------------------------------------------------------------
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        //-----------------------------------------------------------------
        public byte EffectStatusIdRef
        {
            get { return _effectStatusIdRef; }
            set { _effectStatusIdRef = value; }
        }
        //-----------------------------------------------------------------
        public byte EffectStatusId
        {
            get { return _effectStatusId; }
            set { _effectStatusId = value; }
        }

        //-----------------------------------------------------------------
        public string EffectStatusName
        {
            get { return _effectStatusName; }
            set { _effectStatusName = value; }
        }

        public string DisplayPosition
        {
            get { return _DisplayPosition; }
            set { _DisplayPosition = value; }
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
        public string MetaTitleRef
        {
            get
            {
                return _MetaTitleRef;
            }

            set
            {
                _MetaTitleRef = value;
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

        public byte DocGroupId
        {
            get { return _docGroupId; }
            set { _docGroupId = value; }
        }

        public byte LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }

        public short IssueYear
        {
            get { return _issueYear; }
            set { _issueYear = value; }
        }

        //-----------------------------------------------------------------
        private List<DocRelates> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocRelates> l_DocRelates = new List<DocRelates>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocRelates m_DocRelates = new DocRelates(db.ConnectionString);
                    m_DocRelates.DocRelateId = smartReader.GetInt32("DocRelateId");
                    m_DocRelates.DocId = smartReader.GetInt32("DocId");
                    m_DocRelates.DocReferenceId = smartReader.GetInt32("DocReferenceId");
                    m_DocRelates.RelateTypeId = smartReader.GetByte("RelateTypeId");
                    m_DocRelates.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocRelates.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocRelates.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocRelates.RevUserId = smartReader.GetInt32("RevUserId");
                    m_DocRelates.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    m_DocRelates.DocName = smartReader.GetString("DocName");
                    m_DocRelates.DocNameRef = smartReader.GetString("DocNameRef");
                    m_DocRelates.DocIdentity = smartReader.GetString("DocIdentity");
                    m_DocRelates.DocIdentityRef = smartReader.GetString("DocIdentityRef");
                    m_DocRelates.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_DocRelates.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_DocRelates.IssueDateRef = smartReader.GetDateTime("IssueDateRef");
                    m_DocRelates.EffectDateRef = smartReader.GetDateTime("EffectDateRef");
                    m_DocRelates.FieldDesc = smartReader.GetString("FieldDesc");
                    m_DocRelates.TotalDoc = smartReader.GetInt32("TotalDoc");
                    m_DocRelates.DocUrl = smartReader.GetString("DocUrl");
                    m_DocRelates.MetaTitle = smartReader.GetString("MetaTitle");
                    m_DocRelates.MetaTitleRef = smartReader.GetString("MetaTitleRef");
                    m_DocRelates.FieldId = smartReader.GetInt16("FieldId");
                    m_DocRelates.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_DocRelates.EffectStatusIdRef = smartReader.GetByte("EffectStatusIdRef");
                    l_DocRelates.Add(m_DocRelates);
                }
                reader.Close();
                return l_DocRelates;
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
        //-----------------------------------------------------------------
        private List<DocRelates> Init1(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocRelates> l_DocRelates = new List<DocRelates>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocRelates m_DocRelates = new DocRelates(db.ConnectionString);
                    m_DocRelates.DisplayPosition = smartReader.GetString("DisplayPosition");
                    m_DocRelates.DocRelateId = smartReader.GetInt32("DocRelateId");
                    m_DocRelates.DocId = smartReader.GetInt32("DocId");
                    m_DocRelates.DocReferenceId = smartReader.GetInt32("DocReferenceId");
                    m_DocRelates.RelateTypeId = smartReader.GetByte("RelateTypeId");
                    m_DocRelates.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocRelates.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocRelates.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocRelates.DocName = smartReader.GetString("DocName");
                    m_DocRelates.DocNameRef = smartReader.GetString("DocNameRef");
                    m_DocRelates.DocIdentity = smartReader.GetString("DocIdentity");
                    m_DocRelates.DocIdentityRef = smartReader.GetString("DocIdentityRef");
                    m_DocRelates.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_DocRelates.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_DocRelates.IssueDateRef = smartReader.GetDateTime("IssueDateRef");
                    m_DocRelates.EffectDateRef = smartReader.GetDateTime("EffectDateRef");
                    m_DocRelates.FieldDesc = smartReader.GetString("FieldDesc");
                    m_DocRelates.TotalDoc = smartReader.GetInt32("TotalDoc");
                    m_DocRelates.DocUrl = smartReader.GetString("DocUrl");
                    m_DocRelates.FieldId = smartReader.GetInt16("FieldId");
                    l_DocRelates.Add(m_DocRelates);
                }
                reader.Close();
                return l_DocRelates;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId, byte LanguageId = 1)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", this.DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@DocRelateId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocRelateId = Convert.ToInt32((cmd.Parameters["@DocRelateId"].Value == null) ? 0 : (cmd.Parameters["@DocRelateId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", this.DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", this.DocRelateId));
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
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            //try
            //{
                SqlCommand cmd = new SqlCommand("DocRelates_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                //cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", this.DocRelateId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return RetVal;
        }
        public byte UpdateRelateTypeId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_UpdateRelateTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", this.DocRelateId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", this.DocRelateId));
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
        public List<DocRelates> GetList()
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                string sql = "SELECT * FROM V$DocRelates";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<DocRelates> Static_GetList(string ConStr)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                DocRelates m_DocRelates = new DocRelates(ConStr);
                RetVal = m_DocRelates.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocRelates> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<DocRelates> GetListByDocRelateId(int DocRelateId)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                if (DocRelateId > 0)
                {
                    string sql = "SELECT * FROM V$DocRelates WHERE (DocRelateId=" + DocRelateId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public DocRelates Get(int DocRelateId)
        {
            DocRelates RetVal = new DocRelates(db.ConnectionString);
            try
            {
                List<DocRelates> list = GetListByDocRelateId(DocRelateId);
                if (list.Count > 0)
                {
                    RetVal = (DocRelates)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocRelates Get()
        {
            return Get(this.DocRelateId);
        }
        //-------------------------------------------------------------- 
        public static DocRelates Static_Get(int DocRelateId)
        {
            return Static_Get(DocRelateId);
        }
        //--------------------------------------------------------------     
        public DataSet DocRelates_StatisticByCrDateTime(int ActUserId, string RelateTypeId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_StatisticByCrDateTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", StringUtil.InjectionString(RelateTypeId)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public DataSet DocRelates_StatisticByCrUserId(int ActUserId, string RelateTypeId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_StatisticByCrUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", StringUtil.InjectionString(RelateTypeId)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public DataSet DocRelates_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short FieldId, int DocId, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet DocRelates_StatisticByRelateTypeId(int ActUserId, string RelateTypeId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_StatisticByRelateTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", StringUtil.InjectionString(RelateTypeId)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public List<DocRelates> DocRelates_GetField(int ActUserId, string OrderBy, byte LanguageId, int DocId, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                                    int CrUserId, string SearchByDate, string DateFrom, string DateTo)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetField");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", StringUtil.InjectionString(RelateTypeId)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocRelates> DocRelates_GetList(int ActUserId, string OrderBy, int DocId, int DocReferenceId, byte RelateTypeId, byte ReviewStatusId, string DateFrom, string DateTo)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocRelates> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short FieldId, int DocId, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {

            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId,0 , FieldId, DocId, "", DocReferenceId, RelateTypeId, ReviewStatusId,
                                        CrUserId, SearchByDate, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------- with docidentity------------------------------------    
        public List<DocRelates> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short FieldId, int DocId, string DocIdentity, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, 0, FieldId, DocId, DocIdentity, DocReferenceId, RelateTypeId, ReviewStatusId,
                                        CrUserId, SearchByDate, DateFrom, DateTo, ref RowCount);
        }
        public List<DocRelates> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, short FieldId, int DocId, string DocIdentity, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
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
        public List<DocRelates> DocRelates_GetPageByDocId(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int DocId, string RelateTypeId,
                                                            byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetPageByDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public List<DocRelates> Docs_GetViewRelates(byte LanguageId, int DocId, short RelateTypeId, string DisplayPosition, byte ReviewStatusId, int RowAmount, int PageIndex, byte GetCountByRelateType, ref int RowCount)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewRelates");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayPosition", DisplayPosition));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetCountByRelateType", GetCountByRelateType));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init1(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocRelates> Search(int ActUserId, string OrderBy, byte LanguageId, short FieldId, int DocId, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, FieldId, DocId, DocReferenceId, RelateTypeId, ReviewStatusId, 
                                        CrUserId, SearchByDate, DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------            
        public void DocRelateArticles_Delete_Quick(int ActUserId, int ArticleId, int DocRelateId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_Delete_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------            
        public void DocRelateArticles_Insert_Quick(int ActUserId, int ArticleId, int DocRelateId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_Insert_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------            
        public void DocRelateArticles_UpdateArticleId_Quick(int ActUserId, int ArticleId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_UpdateArticleId_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------     
        public void DocRelateArticles_Create(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short FieldId, int ArticleId, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_Create");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------     
        public List<DocRelates> DocRelateArticles_GetField(byte LanguageId, int ArticleId)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_GetField");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet DocRelateArticles_GetByFieldId(byte LanguageId, int ArticleId, int FieldId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_GetByFieldId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocRelates> DocRelateArticles_SearchDocRelate(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short FieldId, int ArticleId, string DocIdentity, int DocReferenceId, string RelateTypeId, byte ReviewStatusId,
                                        int CrUserId, string SearchByDate, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocRelates> RetVal = new List<DocRelates>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelateArticles_SearchDocRelate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@DocReferenceId", DocReferenceId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
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
    }
}