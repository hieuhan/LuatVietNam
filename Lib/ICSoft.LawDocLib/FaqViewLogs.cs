
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
    /// class FaqViewLogs
    /// </summary>
    public class FaqViewLogs
    {
        private int _FaqViewLogId;
        private int _FaqId;
        private string _RefererFrom;
        private string _UserAgent;
        private int _CustomerId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private string _Question;
        private byte _LanguageId;
        private byte _FaqTypeId;
        private byte _FaqGroupId;
        private short _FieldId;
        private short _DataSourceId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqViewLogs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqViewLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqViewLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int FaqViewLogId
        {
            get { return _FaqViewLogId; }
            set { _FaqViewLogId = value; }
        }
        //-----------------------------------------------------------------
        public int FaqId
        {
            get { return _FaqId; }
            set { _FaqId = value; }
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
        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public byte FaqTypeId
        {
            get { return _FaqTypeId; }
            set { _FaqTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte FaqGroupId
        {
            get { return _FaqGroupId; }
            set { _FaqGroupId = value; }
        }
        //-----------------------------------------------------------------
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------

        private List<FaqViewLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqViewLogs> l_FaqViewLogs = new List<FaqViewLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqViewLogs m_FaqViewLogs = new FaqViewLogs(db.ConnectionString);
                    m_FaqViewLogs.FaqViewLogId = smartReader.GetInt32("FaqViewLogId");
                    m_FaqViewLogs.FaqId = smartReader.GetInt32("FaqId");
                    m_FaqViewLogs.RefererFrom = smartReader.GetString("RefererFrom");
                    m_FaqViewLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_FaqViewLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_FaqViewLogs.FromIP = smartReader.GetString("FromIP");
                    m_FaqViewLogs.Question = smartReader.GetString("Question");
                    m_FaqViewLogs.LanguageId = smartReader.GetByte("LanguageId");
                    m_FaqViewLogs.FaqTypeId = smartReader.GetByte("FaqTypeId");
                    m_FaqViewLogs.FaqGroupId = smartReader.GetByte("FaqGroupId");
                    m_FaqViewLogs.FieldId = smartReader.GetInt16("FieldId");
                    m_FaqViewLogs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    l_FaqViewLogs.Add(m_FaqViewLogs);
                }
                reader.Close();
                return l_FaqViewLogs;
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
                SqlCommand cmd = new SqlCommand("FaqViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", this.RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@FaqViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FaqViewLogId = Convert.ToInt32((cmd.Parameters["@FaqViewLogId"].Value == null) ? 0 : (cmd.Parameters["@FaqViewLogId"].Value));
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
        public List<FaqViewLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int FaqId, string RefererFrom, string UserAgent, int CustomerId, 
                                            string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            List<FaqViewLogs> RetVal = new List<FaqViewLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("FaqViewLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@FaqId", FaqId));
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
       
    }
}