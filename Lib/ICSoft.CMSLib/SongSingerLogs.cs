
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;

namespace ICSoft.CMSLib
{
    public class SongSingerLogs
    {
        private int _SongSingerLogId;
        private int _SongSingerId;
        private int _SongSingerFileId;
        private int _SongSingerRBTId;
        private byte _ActionTypeId;
        private string _UserAgent;
        private string _FromIP;
        private int _UserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerLogs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerLogs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerLogId
        {
            get { return _SongSingerLogId; }
            set { _SongSingerLogId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerFileId
        {
            get { return _SongSingerFileId; }
            set { _SongSingerFileId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public byte ActionTypeId
        {
            get { return _ActionTypeId; }
            set { _ActionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<SongSingerLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerLogs> l_SongSingerLogs = new List<SongSingerLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerLogs m_SongSingerLogs = new SongSingerLogs(db.ConnectionString);
                    m_SongSingerLogs.SongSingerLogId = smartReader.GetInt32("SongSingerLogId");
                    m_SongSingerLogs.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerLogs.SongSingerFileId = smartReader.GetInt32("SongSingerFileId");
                    m_SongSingerLogs.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                    m_SongSingerLogs.ActionTypeId = smartReader.GetByte("ActionTypeId");
                    m_SongSingerLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_SongSingerLogs.FromIP = smartReader.GetString("FromIP");
                    m_SongSingerLogs.UserId = smartReader.GetInt32("UserId");
                    m_SongSingerLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerLogs.Add(m_SongSingerLogs);
                }
                reader.Close();
                return l_SongSingerLogs;
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
                SqlCommand cmd = new SqlCommand("SongSingerLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerFileId", this.SongSingerFileId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add("@SongSingerLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerLogId = (cmd.Parameters["@SongSingerLogId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerLogId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

