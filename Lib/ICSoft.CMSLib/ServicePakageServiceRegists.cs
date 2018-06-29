
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
    public class ServicePakageServiceRegists
    {
        private int _ServicePakageServiceRegistId;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private byte _PakageStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ServiceRegistId;
        private short _ServicePakageId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ServicePakageServiceRegists()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ServicePakageServiceRegists(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServicePakageServiceRegists()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ServicePakageServiceRegistId
        {
            get { return _ServicePakageServiceRegistId; }
            set { _ServicePakageServiceRegistId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public byte PakageStatusId
        {
            get { return _PakageStatusId; }
            set { _PakageStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        public int ServiceRegistId
        {
            get { return _ServiceRegistId; }
            set { _ServiceRegistId = value; }
        }
        public short ServicePakageId
        {
            get { return _ServicePakageId; }
            set { _ServicePakageId = value; }
        }
        private List<ServicePakageServiceRegists> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServicePakageServiceRegists> l_ServicePakageServiceRegists = new List<ServicePakageServiceRegists>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServicePakageServiceRegists m_ServicePakageServiceRegists = new ServicePakageServiceRegists(db.ConnectionString);
                    m_ServicePakageServiceRegists.ServicePakageServiceRegistId = smartReader.GetInt32("ServicePakageServiceRegistId");
                    m_ServicePakageServiceRegists.ServiceRegistId = smartReader.GetInt32("ServiceRegistId");
                    m_ServicePakageServiceRegists.ServicePakageId = smartReader.GetInt16("ServicePakageId");
                    m_ServicePakageServiceRegists.StartTime = smartReader.GetDateTime("StartTime");
                    m_ServicePakageServiceRegists.EndTime = smartReader.GetDateTime("EndTime");
                    m_ServicePakageServiceRegists.PakageStatusId = smartReader.GetByte("PakageStatusId");
                    m_ServicePakageServiceRegists.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServicePakageServiceRegists.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ServicePakageServiceRegists.Add(m_ServicePakageServiceRegists);
                }
                reader.Close();
                return l_ServicePakageServiceRegists;
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
                SqlCommand cmd = new SqlCommand("ServicePakageServiceRegists_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", this.ServiceRegistId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", this.ServicePakageId));
                cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@PakageStatusId", this.PakageStatusId));
                cmd.Parameters.Add("@ServicePakageServiceRegistId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServicePakageServiceRegistId = Convert.ToInt32((cmd.Parameters["@ServicePakageServiceRegistId"].Value == null) ? 0 : (cmd.Parameters["@ServicePakageServiceRegistId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServicePakageServiceRegists_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", this.ServiceRegistId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", this.ServicePakageId));
                cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@PakageStatusId", this.PakageStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageServiceRegistId", this.ServicePakageServiceRegistId));
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
        //-----------------------------------------------------------
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePakageServiceRegists_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageServiceRegistId", this.ServicePakageServiceRegistId));
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
        public List<ServicePakageServiceRegists> GetList()
        {
            List<ServicePakageServiceRegists> RetVal = new List<ServicePakageServiceRegists>();
            try
            {
                string sql = "SELECT * FROM V$ServicePakageServiceRegists";
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
        public static List<ServicePakageServiceRegists> Static_GetList(string ConStr)
        {
            List<ServicePakageServiceRegists> RetVal = new List<ServicePakageServiceRegists>();
            try
            {
                ServicePakageServiceRegists m_ServicePakageServiceRegists = new ServicePakageServiceRegists(ConStr);
                RetVal = m_ServicePakageServiceRegists.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePakageServiceRegists> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<ServicePakageServiceRegists> GetListOrderBy(string OrderBy)
        {
            List<ServicePakageServiceRegists> RetVal = new List<ServicePakageServiceRegists>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServicePakageServiceRegists ";
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
        public static List<ServicePakageServiceRegists> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServicePakageServiceRegists m_ServicePakageServiceRegists = new ServicePakageServiceRegists(ConStr);
            return m_ServicePakageServiceRegists.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServicePakageServiceRegists> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<ServicePakageServiceRegists> GetListByServicePakageServiceRegistId(int ServicePakageServiceRegistId)
        {
            List<ServicePakageServiceRegists> RetVal = new List<ServicePakageServiceRegists>();
            try
            {
                if (ServicePakageServiceRegistId > 0)
                {
                    string sql = "SELECT * FROM V$ServicePakageServiceRegists WHERE (ServicePakageServiceRegistId=" + ServicePakageServiceRegistId.ToString() + ")";
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
        public ServicePakageServiceRegists Get(int ServicePakageServiceRegistId)
        {
            ServicePakageServiceRegists RetVal = new ServicePakageServiceRegists(db.ConnectionString);
            try
            {
                List<ServicePakageServiceRegists> list = GetListByServicePakageServiceRegistId(ServicePakageServiceRegistId);
                if (list.Count > 0)
                {
                    RetVal = (ServicePakageServiceRegists)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServicePakageServiceRegists Get()
        {
            return Get(this.ServicePakageServiceRegistId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakageServiceRegists Static_Get(int ServicePakageServiceRegistId)
        {
            return Static_Get(ServicePakageServiceRegistId);
        }
        //--------------------------------------------------------------     
        public List<ServicePakageServiceRegists> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int ServiceRegistId, short ServicePakageId, byte PakageStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ServicePakageServiceRegists> RetVal = new List<ServicePakageServiceRegists>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePakageServiceRegists_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", ServiceRegistId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", ServicePakageId));
                cmd.Parameters.Add(new SqlParameter("@PakageStatusId", PakageStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<ServicePakageServiceRegists> Search(int ActUserId, string OrderBy, int ServiceRegistId, short ServicePakageId, byte PakageStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServiceRegistId, ServicePakageId, PakageStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}