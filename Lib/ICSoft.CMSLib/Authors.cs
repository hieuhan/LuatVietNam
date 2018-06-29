
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
    public class Authors
    {
        private int _ItemId;
        private string _Title;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Authors()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Authors(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Authors()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------   
        public int ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }
        //--------------------------------------------------------------   
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //--------------------------------------------------------------   
        private List<Authors> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Authors> l_Authors = new List<Authors>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Authors m_Authors = new Authors(db.ConnectionString);
                    m_Authors.Title = smartReader.GetString("Title");

                    l_Authors.Add(m_Authors);
                }
                reader.Close();
                return l_Authors;
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
        //-----------------------------------------------------------------
        public List<string> GetAuthorList(string Name, int RowAmount)
        {
            List<string> RetVal = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand("Authors_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RetVal.Add(smartReader.GetString("Title"));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public List<string> GetAlbumList(string Name, int RowAmount)
        {
            List<string> RetVal = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand("Albums_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RetVal.Add(smartReader.GetString("Title"));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public List<string> GetSingerList(string Name, int RowAmount)
        {
            List<string> RetVal = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand("Singers_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RetVal.Add(smartReader.GetString("Title"));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public List<Authors> GetSongList(string Name, int RowAmount)
        {
            List<Authors> RetVal = new List<Authors>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Authors m_Authors = new Authors();
                    m_Authors.ItemId = smartReader.GetInt32("SongId");
                    m_Authors.Title = smartReader.GetString("SongDesc");
                    RetVal.Add(m_Authors);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public int GetSingerIdByName(string Name)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Singers_GetIdBySingerName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add("@SingerId", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                RetVal = int.Parse(cmd.Parameters["@SingerId"].Value.ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
    }
}

