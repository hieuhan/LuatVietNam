
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
    public class ArticleTypes
    {
        private byte _ArticleTypeId;
        private string _ArticleTypeName;
        private string _ArticleTypeDesc;
        private byte _DataTypeId;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ArticleTypeId
        {
            get { return _ArticleTypeId; }
            set { _ArticleTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleTypeName
        {
            get { return _ArticleTypeName; }
            set { _ArticleTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleTypeDesc
        {
            get { return _ArticleTypeDesc; }
            set { _ArticleTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<ArticleTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleTypes> l_ArticleTypes = new List<ArticleTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleTypes m_ArticleTypes = new ArticleTypes(db.ConnectionString);
                    m_ArticleTypes.ArticleTypeId = smartReader.GetByte("ArticleTypeId");
                    m_ArticleTypes.ArticleTypeName = smartReader.GetString("ArticleTypeName");
                    m_ArticleTypes.ArticleTypeDesc = smartReader.GetString("ArticleTypeDesc");
                    m_ArticleTypes.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_ArticleTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_ArticleTypes.Add(m_ArticleTypes);
                }
                reader.Close();
                return l_ArticleTypes;
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
        public List<ArticleTypes> GetList(byte DataTypeId)
        {
            List<ArticleTypes> RetVal = new List<ArticleTypes>();
            try
            {
                string sql = "SELECT * FROM ArticleTypes";
                if (DataTypeId > 0)
                {
                    sql = sql + " WHERE DataTypeId=" + DataTypeId.ToString();
                }
                sql = sql + " ORDER BY DisplayOrder, ArticleTypeName";
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
        public static List<ArticleTypes> Static_GetList(byte DataTypeId)
        {
            List<ArticleTypes> RetVal = new List<ArticleTypes>();
            try
            {
                ArticleTypes m_ArticleTypes = new ArticleTypes();
                RetVal = m_ArticleTypes.GetList(DataTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------   
        public static List<ArticleTypes> Static_GetListAll(byte DataTypeId, string TextOptionAll = "...")
        {
            ArticleTypes m_ArticleTypes = new ArticleTypes();
            List<ArticleTypes> RetVal = m_ArticleTypes.GetList(DataTypeId);
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ArticleTypes.ArticleTypeName = TextOptionAll;
                m_ArticleTypes.ArticleTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_ArticleTypes);
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        public ArticleTypes Get(byte ArticleTypeId)
        {
            ArticleTypes RetVal = new ArticleTypes();
            try
            {
                string sql = "SELECT * FROM ArticleTypes WHERE ArticleTypeId=" + ArticleTypeId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                List<ArticleTypes> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (ArticleTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleTypes Get()
        {
            return Get(this.ArticleTypeId);
        }
        //-------------------------------------------------------------- 
        public static ArticleTypes Static_Get(byte ArticleTypeId)
        {
            return Static_Get(ArticleTypeId);
        }
        //--------------------------------------------------------------     
        public static ArticleTypes Static_Get(byte ArticleTypeId, List<ArticleTypes> list)
        {
            ArticleTypes RetVal = new ArticleTypes();
            RetVal = list.Find(i => i.ArticleTypeId == ArticleTypeId);
            if (RetVal == null) RetVal = new ArticleTypes();
            return RetVal;
        }
    }
}

