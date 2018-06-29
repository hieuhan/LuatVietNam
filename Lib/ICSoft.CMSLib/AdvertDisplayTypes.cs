
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
    public class AdvertDisplayTypes
    {
        private byte _AdvertDisplayTypeId;
        private string _AdvertDisplayTypeName;
        private string _AdvertDisplayTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertDisplayTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertDisplayTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertDisplayTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte AdvertDisplayTypeId
        {
            get { return _AdvertDisplayTypeId; }
            set { _AdvertDisplayTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertDisplayTypeName
        {
            get { return _AdvertDisplayTypeName; }
            set { _AdvertDisplayTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertDisplayTypeDesc
        {
            get { return _AdvertDisplayTypeDesc; }
            set { _AdvertDisplayTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<AdvertDisplayTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertDisplayTypes> l_AdvertDisplayTypes = new List<AdvertDisplayTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertDisplayTypes m_AdvertDisplayTypes = new AdvertDisplayTypes(db.ConnectionString);
                    m_AdvertDisplayTypes.AdvertDisplayTypeId = smartReader.GetByte("AdvertDisplayTypeId");
                    m_AdvertDisplayTypes.AdvertDisplayTypeName = smartReader.GetString("AdvertDisplayTypeName");
                    m_AdvertDisplayTypes.AdvertDisplayTypeDesc = smartReader.GetString("AdvertDisplayTypeDesc");

                    l_AdvertDisplayTypes.Add(m_AdvertDisplayTypes);
                }
                reader.Close();
                return l_AdvertDisplayTypes;
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
        public List<AdvertDisplayTypes> GetList()
        {
            List<AdvertDisplayTypes> RetVal = new List<AdvertDisplayTypes>();
            try
            {
                string sql = "SELECT * FROM V$AdvertDisplayTypes";
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
        public static List<AdvertDisplayTypes> Static_GetList(string ConStr)
        {
            List<AdvertDisplayTypes> RetVal = new List<AdvertDisplayTypes>();
            try
            {
                AdvertDisplayTypes m_AdvertDisplayTypes = new AdvertDisplayTypes(ConStr);
                RetVal = m_AdvertDisplayTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<AdvertDisplayTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<AdvertDisplayTypes> GetListByAdvertDisplayTypeId(byte AdvertDisplayTypeId)
        {
            List<AdvertDisplayTypes> RetVal = new List<AdvertDisplayTypes>();
            try
            {
                if (AdvertDisplayTypeId > 0)
                {
                    string sql = "SELECT * FROM V$AdvertDisplayTypes WHERE (AdvertDisplayTypeId=" + AdvertDisplayTypeId.ToString() + ")";
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
        public AdvertDisplayTypes Get(byte AdvertDisplayTypeId)
        {
            AdvertDisplayTypes RetVal = new AdvertDisplayTypes(db.ConnectionString);
            try
            {
                List<AdvertDisplayTypes> list = GetListByAdvertDisplayTypeId(AdvertDisplayTypeId);
                if (list.Count > 0)
                {
                    RetVal = (AdvertDisplayTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public AdvertDisplayTypes Get()
        {
            return Get(this.AdvertDisplayTypeId);
        }
        //-------------------------------------------------------------- 
        public static AdvertDisplayTypes Static_Get(byte AdvertDisplayTypeId)
        {
            return Static_Get(AdvertDisplayTypeId);
        }
        //--------------------------------------------------------------     
        public static AdvertDisplayTypes Static_Get(byte AdvertDisplayTypeId, List<AdvertDisplayTypes> list)
        {
            AdvertDisplayTypes RetVal=new AdvertDisplayTypes();
            RetVal = list.Find(i => i.AdvertDisplayTypeId == AdvertDisplayTypeId);
            if (RetVal == null) RetVal = new AdvertDisplayTypes();
            return RetVal;
        }
        //--------------------------------------------------------------  
    }
}

