
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
    public class ServicePakageStatus
    {
        private byte _ServicePakageStatusId;
        private string _ServicePakageStatusName;
        private string _ServicePakageStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ServicePakageStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ServicePakageStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServicePakageStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ServicePakageStatusId
        {
            get { return _ServicePakageStatusId; }
            set { _ServicePakageStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string ServicePakageStatusName
        {
            get { return _ServicePakageStatusName; }
            set { _ServicePakageStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string ServicePakageStatusDesc
        {
            get { return _ServicePakageStatusDesc; }
            set { _ServicePakageStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<ServicePakageStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServicePakageStatus> l_ServicePakageStatus = new List<ServicePakageStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus(db.ConnectionString);
                    m_ServicePakageStatus.ServicePakageStatusId = smartReader.GetByte("ServicePakageStatusId");
                    m_ServicePakageStatus.ServicePakageStatusName = smartReader.GetString("ServicePakageStatusName");
                    m_ServicePakageStatus.ServicePakageStatusDesc = smartReader.GetString("ServicePakageStatusDesc");

                    l_ServicePakageStatus.Add(m_ServicePakageStatus);
                }
                reader.Close();
                return l_ServicePakageStatus;
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
                SqlCommand cmd = new SqlCommand("ServicePakageStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusName", this.ServicePakageStatusName));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusDesc", this.ServicePakageStatusDesc));
                cmd.Parameters.Add("@ServicePakageStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServicePakageStatusId = Convert.ToByte((cmd.Parameters["@ServicePakageStatusId"].Value == null) ? 0 : (cmd.Parameters["@ServicePakageStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServicePakageStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusName", this.ServicePakageStatusName));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusDesc", this.ServicePakageStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusId", this.ServicePakageStatusId));
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
                SqlCommand cmd = new SqlCommand("ServicePakageStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusId", this.ServicePakageStatusId));
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
        public List<ServicePakageStatus> GetList()
        {
            List<ServicePakageStatus> RetVal = new List<ServicePakageStatus>();
            try
            {
                string sql = "SELECT * FROM V$ServicePakageStatus";
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
        public static List<ServicePakageStatus> Static_GetList(string ConStr)
        {
            List<ServicePakageStatus> RetVal = new List<ServicePakageStatus>();
            try
            {
                ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus(ConStr);
                RetVal = m_ServicePakageStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePakageStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ServicePakageStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus(ConStr);
            List<ServicePakageStatus> RetVal = m_ServicePakageStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ServicePakageStatus.ServicePakageStatusName = TextOptionAll;
                m_ServicePakageStatus.ServicePakageStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_ServicePakageStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServicePakageStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ServicePakageStatus> GetListOrderBy(string OrderBy)
        {
            List<ServicePakageStatus> RetVal = new List<ServicePakageStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServicePakageStatus ";
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
        public static List<ServicePakageStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus(ConStr);
            return m_ServicePakageStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServicePakageStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServicePakageStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ServicePakageStatus> RetVal = new List<ServicePakageStatus>();
            ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ServicePakageStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ServicePakageStatus.ServicePakageStatusName = TextOptionAll;
                    m_ServicePakageStatus.ServicePakageStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_ServicePakageStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServicePakageStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<ServicePakageStatus> GetListByServicePakageStatusId(byte ServicePakageStatusId)
        {
            List<ServicePakageStatus> RetVal = new List<ServicePakageStatus>();
            try
            {
                if (ServicePakageStatusId > 0)
                {
                    string sql = "SELECT * FROM V$ServicePakageStatus WHERE (ServicePakageStatusId=" + ServicePakageStatusId.ToString() + ")";
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
        public ServicePakageStatus Get(byte ServicePakageStatusId)
        {
            ServicePakageStatus RetVal = new ServicePakageStatus(db.ConnectionString);
            try
            {
                List<ServicePakageStatus> list = GetListByServicePakageStatusId(ServicePakageStatusId);
                if (list.Count > 0)
                {
                    RetVal = (ServicePakageStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServicePakageStatus Get()
        {
            return Get(this.ServicePakageStatusId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakageStatus Static_Get(byte ServicePakageStatusId)
        {
            return Static_Get(ServicePakageStatusId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte ServicePakageStatusId)
        {
            string RetVal = "";
            ServicePakageStatus m_ServicePakageStatus = new ServicePakageStatus();
            m_ServicePakageStatus = m_ServicePakageStatus.Get(ServicePakageStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_ServicePakageStatus.ServicePakageStatusDesc;
            }
            else
            {
                RetVal = m_ServicePakageStatus.ServicePakageStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
