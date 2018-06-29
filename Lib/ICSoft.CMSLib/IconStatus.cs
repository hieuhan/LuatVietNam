
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
    public class IconStatus
    {
        private byte _IconStatusId;
        private string _IconStatusName;
        private string _IconStatusDesc;
        private string _IconPath;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public IconStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public IconStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~IconStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte IconStatusId
        {
            get { return _IconStatusId; }
            set { _IconStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string IconStatusName
        {
            get { return _IconStatusName; }
            set { _IconStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string IconStatusDesc
        {
            get { return _IconStatusDesc; }
            set { _IconStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public string IconPath
        {
            get { return _IconPath; }
            set { _IconPath = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<IconStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<IconStatus> l_IconStatus = new List<IconStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    IconStatus m_IconStatus = new IconStatus(db.ConnectionString);
                    m_IconStatus.IconStatusId = smartReader.GetByte("IconStatusId");
                    m_IconStatus.IconStatusName = smartReader.GetString("IconStatusName");
                    m_IconStatus.IconStatusDesc = smartReader.GetString("IconStatusDesc");
                    m_IconStatus.IconPath = smartReader.GetString("IconPath");
                    m_IconStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_IconStatus.Add(m_IconStatus);
                }
                reader.Close();
                return l_IconStatus;
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
        public List<IconStatus> GetList()
        {
            List<IconStatus> RetVal = new List<IconStatus>();
            try
            {
                string sql = "SELECT * FROM IconStatus ORDER BY DisplayOrder, IconStatusName";
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
        public List<IconStatus> GetList(string TextOptionAll = "...")
        {
            List<IconStatus> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                IconStatus m_IconStatus = new IconStatus();
                m_IconStatus.IconStatusName = TextOptionAll;
                m_IconStatus.IconStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_IconStatus);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<IconStatus> GetListOrderBy(string OrderBy)
        {
            List<IconStatus> RetVal = new List<IconStatus>();
            try
            {
                string sql = "SELECT * FROM IconStatus ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<IconStatus> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<IconStatus> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                IconStatus m_IconStatus = new IconStatus();
                m_IconStatus.IconStatusName = TextOptionAll;
                m_IconStatus.IconStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_IconStatus);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<IconStatus> GetListByIconStatusId(byte IconStatusId)
        {
            List<IconStatus> RetVal = new List<IconStatus>();
            try
            {
                if (IconStatusId > 0)
                {
                    string sql = "SELECT * FROM IconStatus WHERE (IconStatusId=" + IconStatusId.ToString() + ")";
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
        public IconStatus Get(byte IconStatusId)
        {
            IconStatus RetVal = new IconStatus();
            try
            {
                List<IconStatus> list = GetListByIconStatusId(IconStatusId);
                if (list.Count > 0)
                {
                    RetVal = (IconStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static IconStatus Static_Get(byte IconStatusId, List<IconStatus> list)
        {
            IconStatus RetVal = new IconStatus();
            try
            {
                RetVal = list.Find(i => i.IconStatusId == IconStatusId);
                if (RetVal == null) RetVal = new IconStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public IconStatus Get()
        {
            return Get(this.IconStatusId);
        }
        //-------------------------------------------------------------- 
        public static IconStatus Static_Get(string Constr, byte IconStatusId)
        {
            IconStatus m_IconStatus = new IconStatus(Constr);
            return m_IconStatus.Get(IconStatusId);
        }
        //-------------------------------------------------------------- 
        public static IconStatus Static_Get(byte IconStatusId)
        {
            return Static_Get("", IconStatusId);
        }
        //-------------------------------------------------------------- 
        public static List<IconStatus> Static_GetList(string ConStr)
        {
            List<IconStatus> RetVal = new List<IconStatus>();
            try
            {
                IconStatus m_IconStatus = new IconStatus(ConStr);
                RetVal = m_IconStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<IconStatus> Static_GetList()
        {
            return Static_GetList("");
        }
    }
}

