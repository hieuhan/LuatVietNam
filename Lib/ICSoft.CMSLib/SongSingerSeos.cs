
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
    public class SongSingerSeos
    {
        private int _SongSingerSeoId;
        private int _SongSingerId;
        private byte _SongFileTypeId;
        private string _SeoTitle;
        private string _SeoDesc;
        private string _SeoKeyword;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerSeos()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerSeos(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerSeos()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerSeoId
        {
            get { return _SongSingerSeoId; }
            set { _SongSingerSeoId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public byte SongFileTypeId
        {
            get { return _SongFileTypeId; }
            set { _SongFileTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string SeoTitle
        {
            get { return _SeoTitle; }
            set { _SeoTitle = value; }
        }
        //-----------------------------------------------------------------
        public string SeoDesc
        {
            get { return _SeoDesc; }
            set { _SeoDesc = value; }
        }
        //-----------------------------------------------------------------
        public string SeoKeyword
        {
            get { return _SeoKeyword; }
            set { _SeoKeyword = value; }
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

        private List<SongSingerSeos> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerSeos> l_SongSingerSeos = new List<SongSingerSeos>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerSeos m_SongSingerSeos = new SongSingerSeos(db.ConnectionString);
                    m_SongSingerSeos.SongSingerSeoId = smartReader.GetInt32("SongSingerSeoId");
                    m_SongSingerSeos.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerSeos.SongFileTypeId = smartReader.GetByte("SongFileTypeId");
                    m_SongSingerSeos.SeoTitle = smartReader.GetString("SeoTitle");
                    m_SongSingerSeos.SeoDesc = smartReader.GetString("SeoDesc");
                    m_SongSingerSeos.SeoKeyword = smartReader.GetString("SeoKeyword");
                    m_SongSingerSeos.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingerSeos.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerSeos.Add(m_SongSingerSeos);
                }
                reader.Close();
                return l_SongSingerSeos;
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
                SqlCommand cmd = new SqlCommand("SongSingerSeos_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@SeoTitle", this.SeoTitle));
                cmd.Parameters.Add(new SqlParameter("@SeoDesc", this.SeoDesc));
                cmd.Parameters.Add(new SqlParameter("@SeoKeyword", this.SeoKeyword));
                cmd.Parameters.Add("@SongSingerSeoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerSeoId = (cmd.Parameters["@SongSingerSeoId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerSeoId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerSeos_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@SeoTitle", this.SeoTitle));
                cmd.Parameters.Add(new SqlParameter("@SeoDesc", this.SeoDesc));
                cmd.Parameters.Add(new SqlParameter("@SeoKeyword", this.SeoKeyword));
                cmd.Parameters.Add(new SqlParameter("@SongSingerSeoId", this.SongSingerSeoId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerSeos_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@SeoTitle", this.SeoTitle));
                cmd.Parameters.Add(new SqlParameter("@SeoDesc", this.SeoDesc));
                cmd.Parameters.Add(new SqlParameter("@SeoKeyword", this.SeoKeyword));
                cmd.Parameters.Add(new SqlParameter("@SongSingerSeoId", this.SongSingerSeoId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerSeoId = (cmd.Parameters["@SongSingerSeoId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerSeoId"].Value);
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
        public List<SongSingerSeos> GetListBySongSingerId(int SongSingerId, byte SongFileTypeId)
        {
            List<SongSingerSeos> RetVal = new List<SongSingerSeos>();
            try
            {
                if (SongSingerId > 0)
                {
                    string sql = "SELECT * FROM SongSingerSeos WHERE (SongSingerId=" + SongSingerId.ToString() + ")";
                    if (SongFileTypeId > 0) sql = "SELECT * FROM SongSingerSeos WHERE (SongSingerId=" + SongSingerId.ToString() + ") AND (SongFileTypeId=" + SongFileTypeId.ToString() + ")";
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
        public SongSingerSeos Get(int SongSingerId, byte SongFileTypeId)
        {
            SongSingerSeos RetVal = new SongSingerSeos();
            try
            {
                List<SongSingerSeos> list = GetListBySongSingerId(SongSingerId, SongFileTypeId);
                if (list.Count > 0)
                {
                    RetVal = (SongSingerSeos)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongSingerSeos Static_Get(int SongSingerId)
        {
            SongSingerSeos RetVal = new SongSingerSeos();
            try
            {
                RetVal = RetVal.Get(SongSingerId, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongSingerSeos Get()
        {
            return Get(this.SongSingerId, this.SongFileTypeId);
        }
    }
}

