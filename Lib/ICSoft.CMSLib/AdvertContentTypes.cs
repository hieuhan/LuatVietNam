
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
    public class AdvertContentTypes
    {
        private byte _AdvertContentTypeId;
        private string _AdvertContentTypeName;
        private string _AdvertContentTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertContentTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertContentTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertContentTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte AdvertContentTypeId
        {
            get { return _AdvertContentTypeId; }
            set { _AdvertContentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertContentTypeName
        {
            get { return _AdvertContentTypeName; }
            set { _AdvertContentTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertContentTypeDesc
        {
            get { return _AdvertContentTypeDesc; }
            set { _AdvertContentTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<AdvertContentTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertContentTypes> l_AdvertContentTypes = new List<AdvertContentTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertContentTypes m_AdvertContentTypes = new AdvertContentTypes(db.ConnectionString);
                    m_AdvertContentTypes.AdvertContentTypeId = smartReader.GetByte("AdvertContentTypeId");
                    m_AdvertContentTypes.AdvertContentTypeName = smartReader.GetString("AdvertContentTypeName");
                    m_AdvertContentTypes.AdvertContentTypeDesc = smartReader.GetString("AdvertContentTypeDesc");

                    l_AdvertContentTypes.Add(m_AdvertContentTypes);
                }
                reader.Close();
                return l_AdvertContentTypes;
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
        public List<AdvertContentTypes> GetList()
        {
            List<AdvertContentTypes> RetVal = new List<AdvertContentTypes>();
            try
            {
                string sql = "SELECT * FROM V$AdvertContentTypes";
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
        public static List<AdvertContentTypes> Static_GetList(string ConStr)
        {
            List<AdvertContentTypes> RetVal = new List<AdvertContentTypes>();
            try
            {
                AdvertContentTypes m_AdvertContentTypes = new AdvertContentTypes(ConStr);
                RetVal = m_AdvertContentTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<AdvertContentTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<AdvertContentTypes> GetListByAdvertContentTypeId(byte AdvertContentTypeId)
        {
            List<AdvertContentTypes> RetVal = new List<AdvertContentTypes>();
            try
            {
                if (AdvertContentTypeId > 0)
                {
                    string sql = "SELECT * FROM V$AdvertContentTypes WHERE (AdvertContentTypeId=" + AdvertContentTypeId.ToString() + ")";
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
        public AdvertContentTypes Get(byte AdvertContentTypeId)
        {
            AdvertContentTypes RetVal = new AdvertContentTypes(db.ConnectionString);
            try
            {
                List<AdvertContentTypes> list = GetListByAdvertContentTypeId(AdvertContentTypeId);
                if (list.Count > 0)
                {
                    RetVal = (AdvertContentTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public AdvertContentTypes Get()
        {
            return Get(this.AdvertContentTypeId);
        }
        //-------------------------------------------------------------- 
        public static AdvertContentTypes Static_Get(byte AdvertContentTypeId)
        {
            return Static_Get(AdvertContentTypeId);
        }
        //--------------------------------------------------------------     
        public static AdvertContentTypes Static_Get(byte AdvertContentTypeId, List<AdvertContentTypes> list)
        {
            AdvertContentTypes RetVal=new AdvertContentTypes();
            RetVal = list.Find(i => i.AdvertContentTypeId == AdvertContentTypeId);
            if (RetVal == null) RetVal = new AdvertContentTypes();
            return RetVal;
        }
        //--------------------------------------------------------------  
    }
}

