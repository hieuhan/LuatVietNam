
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
    public class SongSingerSingers
    {
        private int _SongSingerSingerId;
        private int _SongSingerId;
        private int _SingerId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerSingers()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerSingers(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerSingers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerSingerId
        {
            get { return _SongSingerSingerId; }
            set { _SongSingerSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int SingerId
        {
            get { return _SingerId; }
            set { _SingerId = value; }
        }
        //-----------------------------------------------------------------

        private List<SongSingerSingers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerSingers> l_SongSingerSingers = new List<SongSingerSingers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerSingers m_SongSingerSingers = new SongSingerSingers(db.ConnectionString);
                    m_SongSingerSingers.SongSingerSingerId = smartReader.GetInt32("SongSingerSingerId");
                    m_SongSingerSingers.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerSingers.SingerId = smartReader.GetInt32("SingerId");

                    l_SongSingerSingers.Add(m_SongSingerSingers);
                }
                reader.Close();
                return l_SongSingerSingers;
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
        public string GetSingerBySongSingerId(int SongSingerId)
        {
            string RetVal = "";
            try
            {
                string sql = "SELECT Title FROM Articles WHERE ArticleId IN (SELECT SingerId FROM SongSingerSingers WHERE SongSingerId=" + SongSingerId.ToString() + ")";
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
        public List<SongSingerSingers> GetList(int SongSingerId)
        {
            List<SongSingerSingers> RetVal = new List<SongSingerSingers>();
            try
            {
                string sql = "SELECT * FROM SongSingerSingers WHERE SongSingerId=" + SongSingerId.ToString();
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
        public static List<SongSingerSingers> Static_GetList(int SongSingerId)
        {
            return new SongSingerSingers().GetList(SongSingerId);
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerSingers> Static_GetList(int SongSingerId, List<SongSingerSingers> list)
        {
            List<SongSingerSingers> RetVal = new List<SongSingerSingers>();
            try
            {
                RetVal = list.FindAll(i => i.SongSingerId == SongSingerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

