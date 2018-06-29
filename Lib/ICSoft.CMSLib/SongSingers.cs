
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
    public class SongSingers
    {
        private int _SongSingerId;
        private int _SongId;
        private string _SongSingerName;
        private byte _ReviewStatusId;
        private string _SingerList;
        private int _HashCode;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _CrUserId;
        public byte VideoStatusId { get; set; }
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingers()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingers(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------
        public string SongSingerName
        {
            get { return _SongSingerName; }
            set { _SongSingerName = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string SingerList
        {
            get { return _SingerList; }
            set { _SingerList = value; }
        }
        //-----------------------------------------------------------------
        public int HashCode
        {
            get { return _HashCode; }
            set { _HashCode = value; }
        }
        //-----------------------------------------------------------------
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------
        public int DownloadCount
        {
            get { return _DownloadCount; }
            set { _DownloadCount = value; }
        }
        //-----------------------------------------------------------------
        public int ShareCount
        {
            get { return _ShareCount; }
            set { _ShareCount = value; }
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

        private List<SongSingers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingers> l_SongSingers = new List<SongSingers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingers m_SongSingers = new SongSingers(db.ConnectionString);
                    m_SongSingers.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingers.SongId = smartReader.GetInt32("SongId");
                    m_SongSingers.SongSingerName = smartReader.GetString("SongSingerName");
                    m_SongSingers.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_SongSingers.VideoStatusId = smartReader.GetByte("VideoStatusId");
                    m_SongSingers.SingerList = smartReader.GetString("SingerList");
                    m_SongSingers.HashCode = smartReader.GetInt32("HashCode");
                    m_SongSingers.ViewCount = smartReader.GetInt32("ViewCount");
                    m_SongSingers.DownloadCount = smartReader.GetInt32("DownloadCount");
                    m_SongSingers.ShareCount = smartReader.GetInt32("ShareCount");
                    m_SongSingers.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingers.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingers.Add(m_SongSingers);
                }
                reader.Close();
                return l_SongSingers;
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
        public int GetSongSingerIdBy(string SongName, string SingerName, string RBTCode)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_GetIdBy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongName", SongName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", RBTCode));
                cmd.Parameters.Add("@SongSingerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToInt32((cmd.Parameters["@SongSingerId"].Value == null) ? "0" : cmd.Parameters["@SongSingerId"].Value);
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
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
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
        public byte UpdateVideoStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_UpdateVideoStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@VideoStatusId", this.VideoStatusId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
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
                SqlCommand cmd = new SqlCommand("SongSingers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
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
        public byte DeleteVideo(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_DeleteVideo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
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
    }
}

