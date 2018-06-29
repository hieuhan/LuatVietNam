
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
    public class AdvertStatus
    {
        private byte _AdvertStatusId;
        private string _AdvertStatusName;
        private string _AdvertStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte AdvertStatusId
        {
            get { return _AdvertStatusId; }
            set { _AdvertStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertStatusName
        {
            get { return _AdvertStatusName; }
            set { _AdvertStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertStatusDesc
        {
            get { return _AdvertStatusDesc; }
            set { _AdvertStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<AdvertStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertStatus> l_AdvertStatus = new List<AdvertStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertStatus m_AdvertStatus = new AdvertStatus(db.ConnectionString);
                    m_AdvertStatus.AdvertStatusId = smartReader.GetByte("AdvertStatusId");
                    m_AdvertStatus.AdvertStatusName = smartReader.GetString("AdvertStatusName");
                    m_AdvertStatus.AdvertStatusDesc = smartReader.GetString("AdvertStatusDesc");

                    l_AdvertStatus.Add(m_AdvertStatus);
                }
                reader.Close();
                return l_AdvertStatus;
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
        public List<AdvertStatus> GetList()
        {
            List<AdvertStatus> RetVal = new List<AdvertStatus>();
            try
            {
                string sql = "SELECT * FROM V$AdvertStatus";
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
        public static List<AdvertStatus> Static_GetList(string ConStr)
        {
            List<AdvertStatus> RetVal = new List<AdvertStatus>();
            try
            {
                AdvertStatus m_AdvertStatus = new AdvertStatus(ConStr);
                RetVal = m_AdvertStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<AdvertStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<AdvertStatus> GetListByAdvertStatuId(byte AdvertStatuId)
        {
            List<AdvertStatus> RetVal = new List<AdvertStatus>();
            try
            {
                if (AdvertStatuId > 0)
                {
                    string sql = "SELECT * FROM V$AdvertStatus WHERE (AdvertStatuId=" + AdvertStatuId.ToString() + ")";
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
        public AdvertStatus Get(byte AdvertStatuId)
        {
            AdvertStatus RetVal = new AdvertStatus(db.ConnectionString);
            try
            {
                List<AdvertStatus> list = GetListByAdvertStatuId(AdvertStatuId);
                if (list.Count > 0)
                {
                    RetVal = (AdvertStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public AdvertStatus Get()
        {
            return Get(this.AdvertStatusId);
        }
        //-------------------------------------------------------------- 
        public static AdvertStatus Static_Get(byte AdvertStatusId)
        {
            return Static_Get(AdvertStatusId);
        }
        //--------------------------------------------------------------     
        public static AdvertStatus Static_Get(byte AdvertStatusId, List<AdvertStatus> list)
        {
            AdvertStatus RetVal=new AdvertStatus();
            RetVal = list.Find(i => i.AdvertStatusId == AdvertStatusId);
            if (RetVal == null) RetVal = new AdvertStatus();
            return RetVal;
        }
        //--------------------------------------------------------------  
    }
}

