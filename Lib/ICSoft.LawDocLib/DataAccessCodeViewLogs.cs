
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
    public class DataAccessCodeViewLogs
    {
        private int _DataAccessCodeViewLogId;
        private int _DataId;
        private byte _DataTypeId;
        private string _AccessCode;
        private string _FromIP;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DataAccessCodeViewLogs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DataAccessCodeViewLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DataAccessCodeViewLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DataAccessCodeViewLogId
        {
            get { return _DataAccessCodeViewLogId; }
            set { _DataAccessCodeViewLogId = value; }
        }
        //-----------------------------------------------------------------
        public int DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string AccessCode
        {
            get { return _AccessCode; }
            set { _AccessCode = value; }
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

        private List<DataAccessCodeViewLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DataAccessCodeViewLogs> l_DataAccessCodeViewLogs = new List<DataAccessCodeViewLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DataAccessCodeViewLogs m_DataAccessCodeViewLogs = new DataAccessCodeViewLogs(db.ConnectionString);
                    m_DataAccessCodeViewLogs.DataAccessCodeViewLogId = smartReader.GetInt32("DataAccessCodeViewLogId");
                    m_DataAccessCodeViewLogs.DataId = smartReader.GetInt32("DataId");
                    m_DataAccessCodeViewLogs.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_DataAccessCodeViewLogs.AccessCode = smartReader.GetString("AccessCode");
                    m_DataAccessCodeViewLogs.FromIP = smartReader.GetString("FromIP");
                    m_DataAccessCodeViewLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DataAccessCodeViewLogs.Add(m_DataAccessCodeViewLogs);
                }
                reader.Close();
                return l_DataAccessCodeViewLogs;
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
                SqlCommand cmd = new SqlCommand("DataAccessCodeViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataId", this.DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@AccessCode", this.AccessCode));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@DataAccessCodeViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DataAccessCodeViewLogId = Convert.ToInt32((cmd.Parameters["@DataAccessCodeViewLogId"].Value == null) ? 0 : (cmd.Parameters["@DataAccessCodeViewLogId"].Value));
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
        public List<DataAccessCodeViewLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DataId, byte DataTypeId, string AccessCode, string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DataAccessCodeViewLogs> RetVal = new List<DataAccessCodeViewLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("DataAccessCodeViewLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DataId", DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@AccessCode",  StringUtil.InjectionString(AccessCode)));
                cmd.Parameters.Add(new SqlParameter("@FromIP",  StringUtil.InjectionString(FromIP)));
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
        public List<DataAccessCodeViewLogs> Search(int ActUserId, string OrderBy, int DataId, byte DataTypeId, string AccessCode, string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DataId, DataTypeId, AccessCode, FromIP, DateFrom, DateTo, ref RowCount);
        }
    }
}