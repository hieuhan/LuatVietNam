
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class DeliveryStatus
    {
        private byte _DeliveryStatusId;
        private string _DeliveryStatusName;
        private string _DeliveryStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DeliveryStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DeliveryStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DeliveryStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte DeliveryStatusId
        {
            get { return _DeliveryStatusId; }
            set { _DeliveryStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string DeliveryStatusName
        {
            get { return _DeliveryStatusName; }
            set { _DeliveryStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string DeliveryStatusDesc
        {
            get { return _DeliveryStatusDesc; }
            set { _DeliveryStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<DeliveryStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DeliveryStatus> l_DeliveryStatus = new List<DeliveryStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DeliveryStatus m_DeliveryStatus = new DeliveryStatus(db.ConnectionString);
                    m_DeliveryStatus.DeliveryStatusId = smartReader.GetByte("DeliveryStatusId");
                    m_DeliveryStatus.DeliveryStatusName = smartReader.GetString("DeliveryStatusName");
                    m_DeliveryStatus.DeliveryStatusDesc = smartReader.GetString("DeliveryStatusDesc");

                    l_DeliveryStatus.Add(m_DeliveryStatus);
                }
                reader.Close();
                return l_DeliveryStatus;
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
        public List<DeliveryStatus> GetList()
        {
            List<DeliveryStatus> RetVal = new List<DeliveryStatus>();
            try
            {
                string sql = "SELECT * FROM DeliveryStatus ORDER BY DeliveryStatusName";
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
        public List<DeliveryStatus> GetListByDeliveryStatusId(byte DeliveryStatusId)
        {
            List<DeliveryStatus> RetVal = new List<DeliveryStatus>();
            try
            {
                if (DeliveryStatusId > 0)
                {
                    string sql = "SELECT * FROM DeliveryStatus WHERE (DeliveryStatusId=" + DeliveryStatusId.ToString() + ")";
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
        public DeliveryStatus Get(byte DeliveryStatusId)
        {
            DeliveryStatus RetVal = new DeliveryStatus();
            try
            {
                List<DeliveryStatus> list = GetListByDeliveryStatusId(DeliveryStatusId);
                if (list.Count > 0)
                {
                    RetVal = (DeliveryStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DeliveryStatus Static_Get(byte DeliveryStatusId, List<DeliveryStatus> list)
        {
            DeliveryStatus RetVal = new DeliveryStatus();
            try
            {
                RetVal = list.Find(i => i.DeliveryStatusId == DeliveryStatusId);
                if (RetVal == null) RetVal = new DeliveryStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DeliveryStatus Get()
        {
            return Get(this.DeliveryStatusId);
        }
        //-------------------------------------------------------------- 
        public static DeliveryStatus Static_Get(byte DeliveryStatusId)
        {
            return new DeliveryStatus().Get(DeliveryStatusId);
        }
        //--------------------------------------------------------------     
        public static List<DeliveryStatus> Static_GetList()
        {
            return new DeliveryStatus().GetList();
        }
        //--------------------------------------------------------------     
        
    }
}

