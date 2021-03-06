
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
    /// class DocSearchLogs
    /// </summary>
    public class DocSearchLogs
    {
        private int _DocSearchLogId;
        private string _SearchKeyword;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private byte _DocTypeId;
        private short _OrganId;
        private short _SignerId;
        private short _FieldId;
        private byte _LanguageId;
        private int _CustomerId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocSearchLogs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocSearchLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocSearchLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocSearchLogId
        {
            get { return _DocSearchLogId; }
            set { _DocSearchLogId = value; }
        }
        //-----------------------------------------------------------------
        public string SearchKeyword
        {
            get { return _SearchKeyword; }
            set { _SearchKeyword = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }
        //-----------------------------------------------------------------
        public byte DocTypeId
        {
            get { return _DocTypeId; }
            set { _DocTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short OrganId
        {
            get { return _OrganId; }
            set { _OrganId = value; }
        }
        //-----------------------------------------------------------------
        public short SignerId
        {
            get { return _SignerId; }
            set { _SignerId = value; }
        }
        //-----------------------------------------------------------------
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<DocSearchLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocSearchLogs> l_DocSearchLogs = new List<DocSearchLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocSearchLogs m_DocSearchLogs = new DocSearchLogs(db.ConnectionString);
                    m_DocSearchLogs.DocSearchLogId = smartReader.GetInt32("DocSearchLogId");
                    m_DocSearchLogs.SearchKeyword = smartReader.GetString("SearchKeyword");
                    m_DocSearchLogs.DateFrom = smartReader.GetDateTime("DateFrom");
                    m_DocSearchLogs.DateTo = smartReader.GetDateTime("DateTo");
                    m_DocSearchLogs.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocSearchLogs.OrganId = smartReader.GetInt16("OrganId");
                    m_DocSearchLogs.SignerId = smartReader.GetInt16("SignerId");
                    m_DocSearchLogs.FieldId = smartReader.GetInt16("FieldId");
                    m_DocSearchLogs.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocSearchLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_DocSearchLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocSearchLogs.Add(m_DocSearchLogs);
                }
                reader.Close();
                return l_DocSearchLogs;
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
                SqlCommand cmd = new SqlCommand("DocSearchLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", this.SearchKeyword));
                if (this.DateFrom == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", this.DateFrom));

                if (this.DateTo == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateTo", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateTo", this.DateTo));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", this.SignerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add("@DocSearchLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocSearchLogId = Convert.ToInt32((cmd.Parameters["@DocSearchLogId"].Value == null) ? 0 : (cmd.Parameters["@DocSearchLogId"].Value));
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
                SqlCommand cmd = new SqlCommand("DocSearchLogs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", this.SearchKeyword));
                if (this.DateFrom == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", this.DateFrom));

                if (this.DateTo == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateTo", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateTo", this.DateTo));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", this.SignerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocSearchLogId", this.DocSearchLogId));
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
                SqlCommand cmd = new SqlCommand("DocSearchLogs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocSearchLogId", this.DocSearchLogId));
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
        public List<DocSearchLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string SearchKeyword, byte DocTypeId, short OrganId, short SignerId, 
                                            short FieldId, byte LanguageId, int CustomerId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocSearchLogs> RetVal = new List<DocSearchLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocSearchLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
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
        public List<DocSearchLogs> Search(int ActUserId, string OrderBy, string SearchKeyword, byte DocTypeId, short OrganId, short SignerId, short FieldId, byte LanguageId, 
                                            int CustomerId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SearchKeyword, DocTypeId, OrganId, SignerId, FieldId, LanguageId, CustomerId, DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------            
        public DataSet GetTopKeyword(string DateFrom, string DateTo, byte LanguageId)
        {
            DataSet RetVal=new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocSearchLogs_TopKeyword");
                cmd.CommandType = CommandType.StoredProcedure;
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}