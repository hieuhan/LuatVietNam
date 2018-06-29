
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class ServiceRegistStatus
    {
        private byte _ServiceRegistStatusId;
        private string _ServiceRegistStatusName;
        private string _ServiceRegistStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ServiceRegistStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ServiceRegistStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServiceRegistStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ServiceRegistStatusId
        {
            get { return _ServiceRegistStatusId; }
            set { _ServiceRegistStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceRegistStatusName
        {
            get { return _ServiceRegistStatusName; }
            set { _ServiceRegistStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceRegistStatusDesc
        {
            get { return _ServiceRegistStatusDesc; }
            set { _ServiceRegistStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<ServiceRegistStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServiceRegistStatus> l_ServiceRegistStatus = new List<ServiceRegistStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus(db.ConnectionString);
                    m_ServiceRegistStatus.ServiceRegistStatusId = smartReader.GetByte("ServiceRegistStatusId");
                    m_ServiceRegistStatus.ServiceRegistStatusName = smartReader.GetString("ServiceRegistStatusName");
                    m_ServiceRegistStatus.ServiceRegistStatusDesc = smartReader.GetString("ServiceRegistStatusDesc");

                    l_ServiceRegistStatus.Add(m_ServiceRegistStatus);
                }
                reader.Close();
                return l_ServiceRegistStatus;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRegistStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusName", this.ServiceRegistStatusName));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusDesc", this.ServiceRegistStatusDesc));
                cmd.Parameters.Add("@ServiceRegistStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServiceRegistStatusId = Convert.ToByte((cmd.Parameters["@ServiceRegistStatusId"].Value == null) ? 0 : (cmd.Parameters["@ServiceRegistStatusId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRegistStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusName", this.ServiceRegistStatusName));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusDesc", this.ServiceRegistStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusId", this.ServiceRegistStatusId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRegistStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusId", this.ServiceRegistStatusId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<ServiceRegistStatus> GetList()
        {
            List<ServiceRegistStatus> RetVal = new List<ServiceRegistStatus>();
            try
            {
                string sql = "SELECT * FROM V$ServiceRegistStatus";
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
        public static List<ServiceRegistStatus> Static_GetList(string ConStr)
        {
            List<ServiceRegistStatus> RetVal = new List<ServiceRegistStatus>();
            try
            {
                ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus(ConStr);
                RetVal = m_ServiceRegistStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServiceRegistStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ServiceRegistStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus(ConStr);
            List<ServiceRegistStatus> RetVal = m_ServiceRegistStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ServiceRegistStatus.ServiceRegistStatusName = TextOptionAll;
                m_ServiceRegistStatus.ServiceRegistStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_ServiceRegistStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServiceRegistStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ServiceRegistStatus> GetListOrderBy(string OrderBy)
        {
            List<ServiceRegistStatus> RetVal = new List<ServiceRegistStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServiceRegistStatus ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<ServiceRegistStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus(ConStr);
            return m_ServiceRegistStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceRegistStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceRegistStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ServiceRegistStatus> RetVal = new List<ServiceRegistStatus>();
            ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ServiceRegistStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ServiceRegistStatus.ServiceRegistStatusName = TextOptionAll;
                    m_ServiceRegistStatus.ServiceRegistStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_ServiceRegistStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServiceRegistStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<ServiceRegistStatus> GetListByServiceRegistStatusId(byte ServiceRegistStatusId)
        {
            List<ServiceRegistStatus> RetVal = new List<ServiceRegistStatus>();
            try
            {
                if (ServiceRegistStatusId > 0)
                {
                    string sql = "SELECT * FROM V$ServiceRegistStatus WHERE (ServiceRegistStatusId=" + ServiceRegistStatusId.ToString() + ")";
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
        public ServiceRegistStatus Get(byte ServiceRegistStatusId)
        {
            ServiceRegistStatus RetVal = new ServiceRegistStatus(db.ConnectionString);
            try
            {
                List<ServiceRegistStatus> list = GetListByServiceRegistStatusId(ServiceRegistStatusId);
                if (list.Count > 0)
                {
                    RetVal = (ServiceRegistStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServiceRegistStatus Get()
        {
            return Get(this.ServiceRegistStatusId);
        }
        //-------------------------------------------------------------- 
        public static ServiceRegistStatus Static_Get(byte ServiceRegistStatusId)
        {
            return Static_Get(ServiceRegistStatusId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte ServiceRegistStatusId)
        {
            string RetVal = "";
            ServiceRegistStatus m_ServiceRegistStatus = new ServiceRegistStatus();
            m_ServiceRegistStatus = m_ServiceRegistStatus.Get(ServiceRegistStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_ServiceRegistStatus.ServiceRegistStatusDesc;
            }
            else
            {
                RetVal = m_ServiceRegistStatus.ServiceRegistStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
