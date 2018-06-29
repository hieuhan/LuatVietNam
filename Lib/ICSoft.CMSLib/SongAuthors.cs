
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
    public class SongAuthors
    {
        private int _SongAuthorId;
        private int _SongId;
        private int _AuthorId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongAuthors()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongAuthors(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongAuthors()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongAuthorId
        {
            get { return _SongAuthorId; }
            set { _SongAuthorId = value; }
        }
        //-----------------------------------------------------------------
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------
        public int AuthorId
        {
            get { return _AuthorId; }
            set { _AuthorId = value; }
        }
        //-----------------------------------------------------------------

        private List<SongAuthors> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongAuthors> l_SongAuthors = new List<SongAuthors>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongAuthors m_SongAuthors = new SongAuthors(db.ConnectionString);
                    m_SongAuthors.SongAuthorId = smartReader.GetInt32("SongAuthorId");
                    m_SongAuthors.SongId = smartReader.GetInt32("SongId");
                    m_SongAuthors.AuthorId = smartReader.GetInt32("AuthorId");

                    l_SongAuthors.Add(m_SongAuthors);
                }
                reader.Close();
                return l_SongAuthors;
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
                SqlCommand cmd = new SqlCommand("SongAuthors_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@AuthorId", this.AuthorId));
                cmd.Parameters.Add("@SongAuthorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongAuthorId = (cmd.Parameters["@SongAuthorId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongAuthorId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongAuthors_DeleteBySongId");
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
        public string GetAuthorBySongId(int SongId)
        {
            string RetVal = "";
            try
            {
                string sql = "SELECT Title FROM Articles WHERE ArticleId IN (SELECT AuthorId FROM SongAuthors WHERE SongId=" + SongId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                DataTable dt=db.getDataTable(cmd);
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
        public List<SongAuthors> GetListBySongId(int SongId)
        {
            List<SongAuthors> RetVal = new List<SongAuthors>();
            try
            {
                string sql = "SELECT * FROM SongAuthors WHERE SongId=" + SongId.ToString();
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
        public static List<SongAuthors> Static_GetListBySongId(int SongId)
        {
            return new SongAuthors().GetListBySongId(SongId);
        }
        //-------------------------------------------------------------- 
        public static List<SongAuthors> Static_GetList(int SongId, List<SongAuthors> list)
        {
            List<SongAuthors> RetVal = new List<SongAuthors>();
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

