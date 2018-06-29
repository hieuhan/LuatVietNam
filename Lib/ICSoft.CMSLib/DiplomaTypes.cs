
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
    public class DiplomaTypes
    {
        private byte _DiplomaTypeId;
        private string _DiplomaTypeName;
        private string _DiplomaTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DiplomaTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DiplomaTypes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~DiplomaTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte DiplomaTypeId
        {
            get { return _DiplomaTypeId; }
            set { _DiplomaTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string DiplomaTypeName
        {
            get { return _DiplomaTypeName; }
            set { _DiplomaTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string DiplomaTypeDesc
        {
            get { return _DiplomaTypeDesc; }
            set { _DiplomaTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<DiplomaTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DiplomaTypes> l_DiplomaTypes = new List<DiplomaTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DiplomaTypes m_DiplomaTypes = new DiplomaTypes(db.ConnectionString);
                    m_DiplomaTypes.DiplomaTypeId = smartReader.GetByte("DiplomaTypeId");
                    m_DiplomaTypes.DiplomaTypeName = smartReader.GetString("DiplomaTypeName");
                    m_DiplomaTypes.DiplomaTypeDesc = smartReader.GetString("DiplomaTypeDesc");

                    l_DiplomaTypes.Add(m_DiplomaTypes);
                }
                reader.Close();
                return l_DiplomaTypes;
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
        public List<DiplomaTypes> GetList()
        {
            List<DiplomaTypes> RetVal = new List<DiplomaTypes>();
            try
            {
                string sql = "SELECT * FROM DiplomaTypes";
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
        public List<DiplomaTypes> GetListOrderBy(string OrderBy)
        {
            List<DiplomaTypes> RetVal = new List<DiplomaTypes>();
            try
            {
                string sql = "SELECT * FROM DiplomaTypes ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<DiplomaTypes> GetListByDiplomaTypeId(byte DiplomaTypeId)
        {
            List<DiplomaTypes> RetVal = new List<DiplomaTypes>();
            try
            {
                if (DiplomaTypeId > 0)
                {
                    string sql = "SELECT * FROM DiplomaTypes WHERE (DiplomaTypeId=" + DiplomaTypeId.ToString() + ")";
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
        public DiplomaTypes Get(byte DiplomaTypeId)
        {
            DiplomaTypes RetVal = new DiplomaTypes();
            try
            {
                List<DiplomaTypes> list = GetListByDiplomaTypeId(DiplomaTypeId);
                if (list.Count > 0)
                {
                    RetVal = (DiplomaTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DiplomaTypes Static_Get(byte DiplomaTypeId, List<DiplomaTypes> list)
        {
            DiplomaTypes RetVal = new DiplomaTypes();
            try
            {
                RetVal = list.Find(i => i.DiplomaTypeId == DiplomaTypeId);
                if (RetVal == null) RetVal = new DiplomaTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DiplomaTypes Get()
        {
            return Get(this.DiplomaTypeId);
        }
        //-------------------------------------------------------------- 
        public static DiplomaTypes Static_Get(byte DiplomaTypeId)
        {
            return Static_Get(DiplomaTypeId);
        }
        //--------------------------------------------------------------     
        public static List<DiplomaTypes> Static_GetList()
        {
            List<DiplomaTypes> RetVal = new List<DiplomaTypes>();
            try
            {
                DiplomaTypes m_DiplomaTypes = new DiplomaTypes();
                RetVal = m_DiplomaTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

