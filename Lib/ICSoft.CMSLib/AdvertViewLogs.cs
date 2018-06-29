
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class AdvertViewLogs
    {
        private int _AdvertViewLogId;
        private int _AdvertId;
        private int _AdvertPositionId;
        private short _CategoryId;
        private string _UserAgent;
        private int _CustomerId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private string _AdvertName;
        private string _PositionName;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertViewLogs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertViewLogs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertViewLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertViewLogId
        {
            get { return _AdvertViewLogId; }
            set { _AdvertViewLogId = value; }
        }
        //-----------------------------------------------------------------
        public int AdvertId
        {
            get { return _AdvertId; }
            set { _AdvertId = value; }
        }
        //-----------------------------------------------------------------
        public int AdvertPositionId
        {
            get { return _AdvertPositionId; }
            set { _AdvertPositionId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
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
        public string AdvertName
        {
            get { return _AdvertName; }
            set { _AdvertName = value; }
        }
        //-----------------------------------------------------------------
        public string PositionName
        {
            get { return _PositionName; }
            set { _PositionName = value; }
        }
        //-----------------------------------------------------------------

        private List<AdvertViewLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertViewLogs> l_AdvertViewLogs = new List<AdvertViewLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertViewLogs m_AdvertViewLogs = new AdvertViewLogs(db.ConnectionString);
                    m_AdvertViewLogs.AdvertViewLogId = smartReader.GetInt32("AdvertViewLogId");
                    m_AdvertViewLogs.AdvertId = smartReader.GetInt32("AdvertId");
                    m_AdvertViewLogs.AdvertPositionId = smartReader.GetInt32("AdvertPositionId");
                    m_AdvertViewLogs.CategoryId = smartReader.GetInt16("CategoryId");
                    m_AdvertViewLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_AdvertViewLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_AdvertViewLogs.FromIP = smartReader.GetString("FromIP");
                    m_AdvertViewLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_AdvertViewLogs.AdvertName = smartReader.GetString("AdvertName");
                    m_AdvertViewLogs.PositionName = smartReader.GetString("PositionName");
                    l_AdvertViewLogs.Add(m_AdvertViewLogs);
                }
                reader.Close();
                return l_AdvertViewLogs;
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
                SqlCommand cmd = new SqlCommand("AdvertViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@AdvertViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.AdvertViewLogId = Convert.ToInt32((cmd.Parameters["@AdvertViewLogId"].Value == null) ? 0 : (cmd.Parameters["@AdvertViewLogId"].Value));
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
        public List<AdvertViewLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int AdvertId, int AdvertPositionId, string UserAgent, int CustomerId,
                                            string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            List<AdvertViewLogs> RetVal = new List<AdvertViewLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("AdvertViewLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", AdvertId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
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

