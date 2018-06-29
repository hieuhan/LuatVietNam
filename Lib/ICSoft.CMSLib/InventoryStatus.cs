
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
    public class InventoryStatus
    {
        private byte _InventoryStatusId;
        private string _InventoryStatusName;
        private string _InventoryStatusDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public InventoryStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public InventoryStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~InventoryStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte InventoryStatusId
        {
            get { return _InventoryStatusId; }
            set { _InventoryStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string InventoryStatusName
        {
            get { return _InventoryStatusName; }
            set { _InventoryStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string InventoryStatusDesc
        {
            get { return _InventoryStatusDesc; }
            set { _InventoryStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<InventoryStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<InventoryStatus> l_InventoryStatus = new List<InventoryStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InventoryStatus m_InventoryStatus = new InventoryStatus(db.ConnectionString);
                    m_InventoryStatus.InventoryStatusId = smartReader.GetByte("InventoryStatusId");
                    m_InventoryStatus.InventoryStatusName = smartReader.GetString("InventoryStatusName");
                    m_InventoryStatus.InventoryStatusDesc = smartReader.GetString("InventoryStatusDesc");
                    m_InventoryStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_InventoryStatus.Add(m_InventoryStatus);
                }
                reader.Close();
                return l_InventoryStatus;
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
        public List<InventoryStatus> GetList()
        {
            List<InventoryStatus> RetVal = new List<InventoryStatus>();
            try
            {
                string sql = "SELECT * FROM InventoryStatus ORDER BY DisplayOrder, InventoryStatusName";
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
        public List<InventoryStatus> GetList(string TextOptionAll = "...")
        {
            List<InventoryStatus> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                InventoryStatus m_InventoryStatus = new InventoryStatus();
                m_InventoryStatus.InventoryStatusName = TextOptionAll;
                m_InventoryStatus.InventoryStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_InventoryStatus);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<InventoryStatus> GetListOrderBy(string OrderBy)
        {
            List<InventoryStatus> RetVal = new List<InventoryStatus>();
            try
            {
                string sql = "SELECT * FROM InventoryStatus ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<InventoryStatus> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<InventoryStatus> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                InventoryStatus m_InventoryStatus = new InventoryStatus();
                m_InventoryStatus.InventoryStatusName = TextOptionAll;
                m_InventoryStatus.InventoryStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_InventoryStatus);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<InventoryStatus> GetListByInventoryStatusId(byte InventoryStatusId)
        {
            List<InventoryStatus> RetVal = new List<InventoryStatus>();
            try
            {
                if (InventoryStatusId > 0)
                {
                    string sql = "SELECT * FROM InventoryStatus WHERE (InventoryStatusId=" + InventoryStatusId.ToString() + ")";
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
        public InventoryStatus Get(byte InventoryStatusId)
        {
            InventoryStatus RetVal = new InventoryStatus();
            try
            {
                List<InventoryStatus> list = GetListByInventoryStatusId(InventoryStatusId);
                if (list.Count > 0)
                {
                    RetVal = (InventoryStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static InventoryStatus Static_Get(byte InventoryStatusId, List<InventoryStatus> list)
        {
            InventoryStatus RetVal = new InventoryStatus();
            try
            {
                RetVal = list.Find(i => i.InventoryStatusId == InventoryStatusId);
                if (RetVal == null) RetVal = new InventoryStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public InventoryStatus Get()
        {
            return Get(this.InventoryStatusId);
        }
        //-------------------------------------------------------------- 
        public static InventoryStatus Static_Get(string Constr, byte InventoryStatusId)
        {
            InventoryStatus m_InventoryStatus = new InventoryStatus(Constr);
            return m_InventoryStatus.Get(InventoryStatusId);
        }
        //-------------------------------------------------------------- 
        public static InventoryStatus Static_Get(byte InventoryStatusId)
        {
            return Static_Get("", InventoryStatusId);
        }
        //-------------------------------------------------------------- 
        public static List<InventoryStatus> Static_GetList(string ConStr)
        {
            List<InventoryStatus> RetVal = new List<InventoryStatus>();
            try
            {
                InventoryStatus m_InventoryStatus = new InventoryStatus(ConStr);
                RetVal = m_InventoryStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<InventoryStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        
    }
}

