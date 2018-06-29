
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class CustomerAccessLogs
    {
        private int _CustomerAccessLogId;
        private int _CustomerId;
        private byte _ActionTypeId;
        private string _FromIP;
        private string _UserAgent;
        private DateTime _CrDateTime;
        private string _CustomerName;
        public string Channel { get; set; }
        public int CrUserId { get; set; }
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerAccessLogs()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerAccessLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerAccessLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerAccessLogId
        {
            get { return _CustomerAccessLogId; }
            set { _CustomerAccessLogId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public byte ActionTypeId
        {
            get { return _ActionTypeId; }
            set { _ActionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerAccessLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerAccessLogs> l_CustomerAccessLogs = new List<CustomerAccessLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerAccessLogs m_CustomerAccessLogs = new CustomerAccessLogs(db.ConnectionString);
                    m_CustomerAccessLogs.CustomerAccessLogId = smartReader.GetInt32("CustomerAccessLogId");
                    m_CustomerAccessLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerAccessLogs.ActionTypeId = smartReader.GetByte("ActionTypeId");
                    m_CustomerAccessLogs.FromIP = smartReader.GetString("FromIP");
                    m_CustomerAccessLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_CustomerAccessLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_CustomerAccessLogs.Channel = smartReader.GetString("Channel");
                    m_CustomerAccessLogs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerAccessLogs.CustomerName = smartReader.GetString("CustomerName");
                    l_CustomerAccessLogs.Add(m_CustomerAccessLogs);
                }
                reader.Close();
                return l_CustomerAccessLogs;
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
                SqlCommand cmd = new SqlCommand("CustomerAccessLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add("@CustomerAccessLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerAccessLogId = Convert.ToInt32((cmd.Parameters["@CustomerAccessLogId"].Value == null) ? 0 : (cmd.Parameters["@CustomerAccessLogId"].Value));
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
        public List<CustomerAccessLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, byte ActionTypeId, string FromIP, string UserAgent, 
                                                 string DateFrom, string DateTo, ref int RowCount)
        {
            List<CustomerAccessLogs> RetVal = new List<CustomerAccessLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccessLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", StringUtil.InjectionString(FromIP)));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", StringUtil.InjectionString(UserAgent)));
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
        public List<CustomerAccessLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string CustomerName, byte ActionTypeId, string FromIP, string UserAgent,
                                                 string DateFrom, string DateTo, ref int RowCount)
        {
            List<CustomerAccessLogs> RetVal = new List<CustomerAccessLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccessLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", StringUtil.InjectionString(FromIP)));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", StringUtil.InjectionString(UserAgent)));
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
        public List<CustomerAccessLogs> Search(int ActUserId, string OrderBy, int CustomerId, byte ActionTypeId, string FromIP, string UserAgent, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, CustomerId, ActionTypeId, FromIP, UserAgent, DateFrom, DateTo, ref RowCount);
        }

        public CustomerAccessLogs GetCustomerAccessLogsByCustomerId(int CustomerId)
        {
            CustomerAccessLogs RetVal = new CustomerAccessLogs();
            List<CustomerAccessLogs> lCustomerAccessLogs = new List<CustomerAccessLogs>();
            try
            {
                if (CustomerId > 0)
                {
                    string sql = "SELECT TOP(1) * FROM CustomerAccessLogs WHERE (CustomerId=" + CustomerId.ToString() + ") ORDER BY CrDateTime DESC";
                    SqlCommand cmd = new SqlCommand(sql);
                    lCustomerAccessLogs = Init(cmd);
                    if (lCustomerAccessLogs.Count > 0)
                    {
                        RetVal = lCustomerAccessLogs[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}