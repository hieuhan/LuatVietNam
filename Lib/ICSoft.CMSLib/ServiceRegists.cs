
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
    public class ServiceRegists
    {
        private int _ServiceRegistId;
        private byte _ServiceTypeId;
        private string _Title;
        private string _FullName;
        private string _Organization;
        private string _Phone;
        private string _Tel;
        private string _Email;
        private string _Fax;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _ServiceRegistStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ServiceRegists()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ServiceRegists(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServiceRegists()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ServiceRegistId
        {
            get { return _ServiceRegistId; }
            set { _ServiceRegistId = value; }
        }
        //-----------------------------------------------------------------
        public byte ServiceTypeId
        {
            get { return _ServiceTypeId; }
            set { _ServiceTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //-----------------------------------------------------------------
        public string Organization
        {
            get { return _Organization; }
            set { _Organization = value; }
        }
        //-----------------------------------------------------------------
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        //-----------------------------------------------------------------
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
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

        public byte ServiceRegistStatusId
        {
            get { return _ServiceRegistStatusId; }
            set { _ServiceRegistStatusId = value; }
        }
        private List<ServiceRegists> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServiceRegists> l_ServiceRegists = new List<ServiceRegists>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServiceRegists m_ServiceRegists = new ServiceRegists(db.ConnectionString);
                    m_ServiceRegists.ServiceRegistId = smartReader.GetInt32("ServiceRegistId");
                    m_ServiceRegists.ServiceTypeId = smartReader.GetByte("ServiceTypeId");
                    m_ServiceRegists.Title = smartReader.GetString("Title");
                    m_ServiceRegists.FullName = smartReader.GetString("FullName");
                    m_ServiceRegists.Organization = smartReader.GetString("Organization");
                    m_ServiceRegists.Phone = smartReader.GetString("Phone");
                    m_ServiceRegists.Tel = smartReader.GetString("Tel");
                    m_ServiceRegists.Email = smartReader.GetString("Email");
                    m_ServiceRegists.Fax = smartReader.GetString("Fax");
                    m_ServiceRegists.ServiceRegistStatusId = smartReader.GetByte("ServiceRegistStatusId");
                    m_ServiceRegists.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServiceRegists.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ServiceRegists.Add(m_ServiceRegists);
                }
                reader.Close();
                return l_ServiceRegists;
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRegists_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", this.ServiceTypeId));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@Organization", this.Organization));
                cmd.Parameters.Add(new SqlParameter("@Phone", this.Phone));
                cmd.Parameters.Add(new SqlParameter("@Tel", this.Tel));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Fax", this.Fax));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusId", this.ServiceRegistStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", this.ServiceRegistId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServiceRegistId = Convert.ToInt32((cmd.Parameters["@ServiceRegistId"].Value == null) ? 0 : (cmd.Parameters["@ServiceRegistId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServiceRegists_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", this.ServiceRegistId));
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
        public List<ServiceRegists> GetList()
        {
            List<ServiceRegists> RetVal = new List<ServiceRegists>();
            try
            {
                string sql = "SELECT * FROM V$ServiceRegists";
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
        public static List<ServiceRegists> Static_GetList(string ConStr)
        {
            List<ServiceRegists> RetVal = new List<ServiceRegists>();
            try
            {
                ServiceRegists m_ServiceRegists = new ServiceRegists(ConStr);
                RetVal = m_ServiceRegists.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServiceRegists> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<ServiceRegists> GetListOrderBy(string OrderBy)
        {
            List<ServiceRegists> RetVal = new List<ServiceRegists>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServiceRegists ";
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
        public static List<ServiceRegists> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServiceRegists m_ServiceRegists = new ServiceRegists(ConStr);
            return m_ServiceRegists.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceRegists> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<ServiceRegists> GetListByServiceRegistId(int ServiceRegistId)
        {
            List<ServiceRegists> RetVal = new List<ServiceRegists>();
            try
            {
                if (ServiceRegistId > 0)
                {
                    string sql = "SELECT * FROM V$ServiceRegists WHERE (ServiceRegistId=" + ServiceRegistId.ToString() + ")";
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
        public ServiceRegists Get(int ServiceRegistId)
        {
            ServiceRegists RetVal = new ServiceRegists(db.ConnectionString);
            try
            {
                List<ServiceRegists> list = GetListByServiceRegistId(ServiceRegistId);
                if (list.Count > 0)
                {
                    RetVal = (ServiceRegists)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServiceRegists Get()
        {
            return Get(this.ServiceRegistId);
        }
        //-------------------------------------------------------------- 
        public static ServiceRegists Static_Get(int ServiceRegistId)
        {
            return Static_Get(ServiceRegistId);
        }
        //--------------------------------------------------------------     
        public List<ServiceRegists> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int ServiceRegistId, byte ServiceTypeId, byte ServiceRegistStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ServiceRegists> RetVal = new List<ServiceRegists>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRegists_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistId", ServiceRegistId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", ServiceTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRegistStatusId", ServiceRegistStatusId));
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
        public List<ServiceRegists> Search(int ActUserId, string OrderBy, int ServiceRegistId, byte ServiceTypeId, byte ServiceRegistStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServiceRegistId, ServiceTypeId, ServiceRegistStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}