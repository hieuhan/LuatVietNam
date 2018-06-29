
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
    public class CrawlerContentTypes
    {
        private byte _CrawlerContentTypeId;
        private string _CrawlerContentTypeName;
        private string _CrawlerContentTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CrawlerContentTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CrawlerContentTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CrawlerContentTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte CrawlerContentTypeId
        {
            get { return _CrawlerContentTypeId; }
            set { _CrawlerContentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string CrawlerContentTypeName
        {
            get { return _CrawlerContentTypeName; }
            set { _CrawlerContentTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string CrawlerContentTypeDesc
        {
            get { return _CrawlerContentTypeDesc; }
            set { _CrawlerContentTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<CrawlerContentTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CrawlerContentTypes> l_CrawlerContentTypes = new List<CrawlerContentTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CrawlerContentTypes m_CrawlerContentTypes = new CrawlerContentTypes(db.ConnectionString);
                    m_CrawlerContentTypes.CrawlerContentTypeId = smartReader.GetByte("CrawlerContentTypeId");
                    m_CrawlerContentTypes.CrawlerContentTypeName = smartReader.GetString("CrawlerContentTypeName");
                    m_CrawlerContentTypes.CrawlerContentTypeDesc = smartReader.GetString("CrawlerContentTypeDesc");

                    l_CrawlerContentTypes.Add(m_CrawlerContentTypes);
                }
                reader.Close();
                return l_CrawlerContentTypes;
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
        public List<CrawlerContentTypes> GetList()
        {
            List<CrawlerContentTypes> RetVal = new List<CrawlerContentTypes>();
            try
            {
                string sql = "SELECT * FROM CrawlerContentTypes";
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
        public List<CrawlerContentTypes> GetListByCrawlerContentTypeId(byte CrawlerContentTypeId)
        {
            List<CrawlerContentTypes> RetVal = new List<CrawlerContentTypes>();
            try
            {
                if (CrawlerContentTypeId > 0)
                {
                    string sql = "SELECT * FROM CrawlerContentTypes WHERE (CrawlerContentTypeId=" + CrawlerContentTypeId.ToString() + ")";
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
        public CrawlerContentTypes Get(byte CrawlerContentTypeId)
        {
            CrawlerContentTypes RetVal = new CrawlerContentTypes();
            try
            {
                List<CrawlerContentTypes> list = GetListByCrawlerContentTypeId(CrawlerContentTypeId);
                if (list.Count > 0)
                {
                    RetVal = (CrawlerContentTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static CrawlerContentTypes Static_Get(byte CrawlerContentTypeId, List<CrawlerContentTypes> list)
        {
            CrawlerContentTypes RetVal = new CrawlerContentTypes();
            try
            {
                RetVal = list.Find(i => i.CrawlerContentTypeId == CrawlerContentTypeId);
                if (RetVal == null) RetVal = new CrawlerContentTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CrawlerContentTypes Get()
        {
            return Get(this.CrawlerContentTypeId);
        }
        //-------------------------------------------------------------- 
        public static CrawlerContentTypes Static_Get(byte CrawlerContentTypeId)
        {
            return new CrawlerContentTypes().Get(CrawlerContentTypeId);
        }
        //-------------------------------------------------------------- 
        public static List<CrawlerContentTypes> Static_GetList()
        {
            return new CrawlerContentTypes().GetList();
        }
        //--------------------------------------------------------------     
    }
}

