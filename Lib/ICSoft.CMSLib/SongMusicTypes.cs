
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
    public class SongMusicTypes
    {
        private int _SongMusicTypeId;
        private int _SongId;
        private short _MusicTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongMusicTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongMusicTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongMusicTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongMusicTypeId
        {
            get { return _SongMusicTypeId; }
            set { _SongMusicTypeId = value; }
        }
        //-----------------------------------------------------------------
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------
        public short MusicTypeId
        {
            get { return _MusicTypeId; }
            set { _MusicTypeId = value; }
        }
        //-----------------------------------------------------------------

        private List<SongMusicTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongMusicTypes> l_SongMusicTypes = new List<SongMusicTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongMusicTypes m_SongMusicTypes = new SongMusicTypes(db.ConnectionString);
                    m_SongMusicTypes.SongMusicTypeId = smartReader.GetInt32("SongMusicTypeId");
                    m_SongMusicTypes.SongId = smartReader.GetInt32("SongId");
                    m_SongMusicTypes.MusicTypeId = smartReader.GetInt16("MusicTypeId");

                    l_SongMusicTypes.Add(m_SongMusicTypes);
                }
                reader.Close();
                return l_SongMusicTypes;
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
                SqlCommand cmd = new SqlCommand("SongMusicTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", this.MusicTypeId));
                cmd.Parameters.Add("@SongMusicTypeId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongMusicTypeId = (cmd.Parameters["@SongMusicTypeId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongMusicTypeId"].Value);
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
        public byte InsertByRBTCode(byte Replicated, int ActUserId, string RBTCode, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongMusicTypes_InsertByRBTCode");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", RBTCode));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", this.MusicTypeId));
                cmd.Parameters.Add("@SongMusicTypeId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongMusicTypeId = (cmd.Parameters["@SongMusicTypeId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongMusicTypeId"].Value);
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
        public byte DeleteBySongId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongMusicTypes_DeleteBySongId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
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
        public List<SongMusicTypes> GetListBySongId(int SongId)
        {
            List<SongMusicTypes> RetVal = new List<SongMusicTypes>();
            try
            {
                string sql = "SELECT * FROM SongMusicTypes WHERE SongId=" + SongId.ToString();
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
        public static List<SongMusicTypes> Static_GetListBySongId(int SongId)
        {
            return new SongMusicTypes().GetListBySongId(SongId);
        }
        //-------------------------------------------------------------- 
        public static List<SongMusicTypes> Static_GetList(int SongId, List<SongMusicTypes> list)
        {
            List<SongMusicTypes> RetVal = new List<SongMusicTypes>();
            try
            {
                RetVal = list.FindAll(i => i.SongId == SongId);
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

