
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
    public class SongSingerAlbumRBTs
    {
        private int _SongSingerAlbumRBTId;
        private int _SongSingerRBTId;
        private int _AlbumId;
        private int _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerAlbumRBTs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerAlbumRBTs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerAlbumRBTs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerAlbumRBTId
        {
            get { return _SongSingerAlbumRBTId; }
            set { _SongSingerAlbumRBTId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<SongSingerAlbumRBTs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerAlbumRBTs> l_SongSingerAlbumRBTs = new List<SongSingerAlbumRBTs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerAlbumRBTs m_SongSingerAlbumRBTs = new SongSingerAlbumRBTs(db.ConnectionString);
                    m_SongSingerAlbumRBTs.SongSingerAlbumRBTId = smartReader.GetInt32("SongSingerAlbumRBTId");
                    m_SongSingerAlbumRBTs.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                    m_SongSingerAlbumRBTs.AlbumId = smartReader.GetInt32("AlbumId");
                    m_SongSingerAlbumRBTs.DisplayOrder = smartReader.GetInt32("DisplayOrder");

                    l_SongSingerAlbumRBTs.Add(m_SongSingerAlbumRBTs);
                }
                reader.Close();
                return l_SongSingerAlbumRBTs;
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
                SqlCommand cmd = new SqlCommand("SongSingerAlbumRBTs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", this.AlbumId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@SongSingerAlbumRBTId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerAlbumRBTId = (cmd.Parameters["@SongSingerAlbumRBTId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerAlbumRBTId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerAlbumRBTs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", this.AlbumId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SongSingerAlbumRBTId", this.SongSingerAlbumRBTId));
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
                SqlCommand cmd = new SqlCommand("SongSingerAlbumRBTs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", this.AlbumId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SongSingerAlbumRBTId", this.SongSingerAlbumRBTId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerAlbumRBTId = (cmd.Parameters["@SongSingerAlbumRBTId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerAlbumRBTId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerAlbumRBTs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerAlbumRBTId", this.SongSingerAlbumRBTId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", this.AlbumId));
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
        public byte DeleteBySongSingerRBTId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerAlbumRBTs_DeleteBySongSingerRBTId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
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
        public string GetAlbumBySongSingerRBTId(int SongSingerRBTId)
        {
            string RetVal = "";
            try
            {
                string sql = "SELECT Title FROM Articles WHERE ArticleId IN (SELECT AlbumId FROM SongSingerAlbumRBTs WHERE SongSingerRBTId=" + SongSingerRBTId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                DataTable dt = db.getDataTable(cmd);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (RetVal != "") RetVal = RetVal + "; ";
                        RetVal = RetVal + dt.Rows[i]["Title"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongSingerAlbumRBTs> GetList(int SongSingerRBTId)
        {
            List<SongSingerAlbumRBTs> RetVal = new List<SongSingerAlbumRBTs>();
            try
            {
                string sql = "SELECT * FROM SongSingerAlbumRBTs WHERE SongSingerRBTId=" + SongSingerRBTId.ToString() + " ORDER BY DisplayOrder";
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
        public static List<SongSingerAlbumRBTs> Static_GetList(int SongSingerRBTId)
        {
            return new SongSingerAlbumRBTs().GetList(SongSingerRBTId);
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerAlbumRBTs> Static_GetList(int SongSingerRBTId, List<SongSingerAlbumRBTs> list)
        {
            List<SongSingerAlbumRBTs> RetVal = new List<SongSingerAlbumRBTs>();
            try
            {
                RetVal = list.FindAll(i => i.SongSingerRBTId == SongSingerRBTId);
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

