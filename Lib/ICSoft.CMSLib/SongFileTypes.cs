
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
    public class SongFileTypes
    {
        private byte _SongFileTypeId;
        private string _SongFileTypeName;
        private string _SongFileTypeDesc;
        private byte _IsVideo;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongFileTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongFileTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongFileTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SongFileTypeId
        {
            get { return _SongFileTypeId; }
            set { _SongFileTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string SongFileTypeName
        {
            get { return _SongFileTypeName; }
            set { _SongFileTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string SongFileTypeDesc
        {
            get { return _SongFileTypeDesc; }
            set { _SongFileTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte IsVideo
        {
            get { return _IsVideo; }
            set { _IsVideo = value; }
        }
        //-----------------------------------------------------------------

        private List<SongFileTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongFileTypes> l_SongFileTypes = new List<SongFileTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongFileTypes m_SongFileTypes = new SongFileTypes(db.ConnectionString);
                    m_SongFileTypes.SongFileTypeId = smartReader.GetByte("SongFileTypeId");
                    m_SongFileTypes.SongFileTypeName = smartReader.GetString("SongFileTypeName");
                    m_SongFileTypes.SongFileTypeDesc = smartReader.GetString("SongFileTypeDesc");
                    m_SongFileTypes.IsVideo = smartReader.GetByte("IsVideo");

                    l_SongFileTypes.Add(m_SongFileTypes);
                }
                reader.Close();
                return l_SongFileTypes;
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
        public List<SongFileTypes> GetList()
        {
            List<SongFileTypes> RetVal = new List<SongFileTypes>();
            try
            {
                string sql = "SELECT * FROM SongFileTypes ORDER BY DisplayOrder";
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
        public List<SongFileTypes> GetListBySongFileTypeId(byte SongFileTypeId)
        {
            List<SongFileTypes> RetVal = new List<SongFileTypes>();
            try
            {
                if (SongFileTypeId > 0)
                {
                    string sql = "SELECT * FROM SongFileTypes WHERE (SongFileTypeId=" + SongFileTypeId.ToString() + ")";
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
        public SongFileTypes Get(byte SongFileTypeId)
        {
            SongFileTypes RetVal = new SongFileTypes();
            try
            {
                List<SongFileTypes> list = GetListBySongFileTypeId(SongFileTypeId);
                if (list.Count > 0)
                {
                    RetVal = (SongFileTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongFileTypes Static_Get(byte SongFileTypeId, List<SongFileTypes> list)
        {
            SongFileTypes RetVal = new SongFileTypes();
            try
            {
                RetVal = list.Find(i => i.SongFileTypeId == SongFileTypeId);
                if (RetVal == null) RetVal = new SongFileTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongFileTypes Get()
        {
            return Get(this.SongFileTypeId);
        }
        //-------------------------------------------------------------- 
        public static SongFileTypes Static_Get(byte SongFileTypeId)
        {
            return new SongFileTypes().Get(SongFileTypeId);
        }
        //-------------------------------------------------------------- 
        public static List<SongFileTypes> Static_GetList()
        {
            return new SongFileTypes().GetList();
        }
        //--------------------------------------------------------------     
        
    }
}

