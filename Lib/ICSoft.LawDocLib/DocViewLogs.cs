
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
    /// class DocViewLogs
    /// </summary>
    public class DocViewLogs
    {
        private int _DocViewLogId;
        private int _DocId;
        private int _DocFileId;
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private byte _ActionTypeId;
        private string _RefererFrom;
        private string _UserAgent;
        private int _CustomerId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private string _DocName;
        private DBAccess db;

        private byte _docGroupId;

        //-----------------------------------------------------------------
        public DocViewLogs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocViewLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocViewLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocViewLogId
        {
            get { return _DocViewLogId; }
            set { _DocViewLogId = value; }
        }
        //-----------------------------------------------------------------
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public int DocFileId
        {
            get { return _DocFileId; }
            set { _DocFileId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte ActionTypeId
        {
            get { return _ActionTypeId; }
            set { _ActionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RefererFrom
        {
            get { return _RefererFrom; }
            set { _RefererFrom = value; }
        }
        //-----------------------------------------------------------------
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }

        //-----------------------------------------------------------------
        public byte DocGroupId
        {
            get { return _docGroupId; }
            set { _docGroupId = value; }
        }
        //-----------------------------------------------------------------

        private List<DocViewLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocViewLogs> l_DocViewLogs = new List<DocViewLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocViewLogs m_DocViewLogs = new DocViewLogs(db.ConnectionString);
                    m_DocViewLogs.DocViewLogId = smartReader.GetInt32("DocViewLogId");
                    m_DocViewLogs.DocId = smartReader.GetInt32("DocId");
                    m_DocViewLogs.DocFileId = smartReader.GetInt32("DocFileId");
                    m_DocViewLogs.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocViewLogs.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_DocViewLogs.ActionTypeId = smartReader.GetByte("ActionTypeId");
                    m_DocViewLogs.RefererFrom = smartReader.GetString("RefererFrom");
                    m_DocViewLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_DocViewLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_DocViewLogs.FromIP = smartReader.GetString("FromIP");
                    m_DocViewLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocViewLogs.DocName = smartReader.GetString("DocName");

                    l_DocViewLogs.Add(m_DocViewLogs);
                }
                reader.Close();
                return l_DocViewLogs;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", this.DocFileId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", this.RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@DocViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocViewLogId = Convert.ToInt32((cmd.Parameters["@DocViewLogId"].Value == null) ? 0 : (cmd.Parameters["@DocViewLogId"].Value));
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
                SqlCommand cmd = new SqlCommand("DocViewLogs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", this.DocFileId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", this.RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@DocViewLogId", this.DocViewLogId));
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
                SqlCommand cmd = new SqlCommand("DocViewLogs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocViewLogId", this.DocViewLogId));
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
        public List<DocViewLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DocId, int DocFileId, byte LanguageId, byte ApplicationTypeId, 
                                         byte ActionTypeId, string RefererFrom, string UserAgent, int CustomerId, string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocViewLogs> RetVal = new List<DocViewLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocViewLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", DocFileId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", StringUtil.InjectionString(RefererFrom)));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", StringUtil.InjectionString(UserAgent)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", StringUtil.InjectionString(FromIP)));
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
        public List<DocViewLogs> Search(int ActUserId, string OrderBy, int DocId, int DocFileId, byte LanguageId, byte ApplicationTypeId,
                                         byte ActionTypeId, string RefererFrom, string UserAgent, int CustomerId, string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocId, DocFileId, LanguageId, ApplicationTypeId, 
                                         ActionTypeId, RefererFrom, UserAgent, CustomerId, FromIP, DateFrom, DateTo, ref RowCount);
        }
    }
}