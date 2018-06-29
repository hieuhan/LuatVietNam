
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSLib
{
    public class CustomerAccounts
    {
        private string _CustomerName;
        private int _CustomerAccountId;
        private int _CustomerId;
        private byte _AccountTypeId;
        private int _AccountBalance;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private byte _CheckTimeExpire;
        private DateTime _LastChangeBalanceTime;
        private byte _StatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerAccounts()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerAccounts(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~CustomerAccounts()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerAccountId
        {
            get { return _CustomerAccountId; }
            set { _CustomerAccountId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        //-----------------------------------------------------------------
        public byte AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }
        //-----------------------------------------------------------------
        public int AccountBalance
        {
            get { return _AccountBalance; }
            set { _AccountBalance = value; }
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
        public byte CheckTimeExpire
        {
            get { return _CheckTimeExpire; }
            set { _CheckTimeExpire = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastChangeBalanceTime
        {
            get { return _LastChangeBalanceTime; }
            set { _LastChangeBalanceTime = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
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

        private List<CustomerAccounts> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerAccounts> l_CustomerAccounts = new List<CustomerAccounts>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerAccounts m_CustomerAccounts = new CustomerAccounts(db.ConnectionString);
                    m_CustomerAccounts.CustomerName = smartReader.GetString("CustomerName");
                    m_CustomerAccounts.CustomerAccountId = smartReader.GetInt32("CustomerAccountId");
                    m_CustomerAccounts.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerAccounts.AccountTypeId = smartReader.GetByte("AccountTypeId");
                    m_CustomerAccounts.AccountBalance = smartReader.GetInt32("AccountBalance");
                    m_CustomerAccounts.StartTime = smartReader.GetDateTime("StartTime");
                    m_CustomerAccounts.EndTime = smartReader.GetDateTime("EndTime");
                    m_CustomerAccounts.CheckTimeExpire = smartReader.GetByte("CheckTimeExpire");
                    m_CustomerAccounts.LastChangeBalanceTime = smartReader.GetDateTime("LastChangeBalanceTime");
                    m_CustomerAccounts.StatusId = smartReader.GetByte("StatusId");
                    m_CustomerAccounts.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerAccounts.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CustomerAccounts.Add(m_CustomerAccounts);
                }
                reader.Close();
                return l_CustomerAccounts;
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
        public ActionResults AddMoney(int ActUserId, PaymentTransactions mPaymentTransactions)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_AddMoney");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", mPaymentTransactions.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", mPaymentTransactions.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", mPaymentTransactions.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@Amount", mPaymentTransactions.Amount));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", mPaymentTransactions.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", mPaymentTransactions.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", mPaymentTransactions.TransactionDesc));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", mPaymentTransactions.TransactionCode));
                cmd.Parameters.Add(new SqlParameter("@TransactionData", mPaymentTransactions.TransactionData));
                cmd.Parameters.Add(new SqlParameter("@ResponseDesc", mPaymentTransactions.ResponseDesc));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", mPaymentTransactions.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", mPaymentTransactions.FromIP));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal.ActionStatus = cmd.Parameters["@ActionStatus"].Value.ToString();
                RetVal.ActionMessage = cmd.Parameters["@ActionMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public ActionResults SubMoney(int ActUserId, PaymentTransactions mPaymentTransactions)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_SubMoney");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", mPaymentTransactions.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", mPaymentTransactions.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", mPaymentTransactions.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@Amount", mPaymentTransactions.Amount));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", mPaymentTransactions.ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", mPaymentTransactions.OrderId));
                cmd.Parameters.Add(new SqlParameter("@DataId", mPaymentTransactions.DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", mPaymentTransactions.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", mPaymentTransactions.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", mPaymentTransactions.TransactionDesc));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", mPaymentTransactions.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", mPaymentTransactions.FromIP));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal.ActionStatus = cmd.Parameters["@ActionStatus"].Value.ToString();
                RetVal.ActionMessage = cmd.Parameters["@ActionMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                if (this.StartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                if (this.EndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@CheckTimeExpire", this.CheckTimeExpire));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add("@CustomerAccountId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerAccountId = (cmd.Parameters["@CustomerAccountId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@CustomerAccountId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CustomerAccounts_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                if (this.StartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                if (this.EndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@CheckTimeExpire", this.CheckTimeExpire));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerAccountId", this.CustomerAccountId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CustomerAccounts_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                if (this.StartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                if (this.EndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@CheckTimeExpire", this.CheckTimeExpire));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerAccountId", this.CustomerAccountId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerAccountId = (cmd.Parameters["@CustomerAccountId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@CustomerAccountId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CustomerAccounts_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@CustomerAccountId", this.CustomerAccountId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<CustomerAccounts> GetListByCustomerId(int CustomerId)
        {
            List<CustomerAccounts> RetVal = new List<CustomerAccounts>();
            try
            {
                string sql = "SELECT * FROM CustomerAccounts WHERE CustomerId=" + CustomerId.ToString();
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
        public List<CustomerAccounts> GetListByCustomerAccountId(int CustomerAccountId)
        {
            List<CustomerAccounts> RetVal = new List<CustomerAccounts>();
            try
            {
                if (CustomerAccountId > 0)
                {
                    string sql = "SELECT * FROM CustomerAccounts WHERE (CustomerAccountId=" + CustomerAccountId.ToString() + ")";
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
        public CustomerAccounts GetByCustomerIdAndAccountTypeId(int CustomerId, byte AccountTypeId)
        {
            List<CustomerAccounts> RetVal = new List<CustomerAccounts>();
            try
            {
                string sql = "SELECT TOP(1) * FROM CustomerAccounts WHERE CustomerId=" + CustomerId.ToString() + " AND AccountTypeId=" + AccountTypeId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
                return (RetVal.Count == 0) ? new CustomerAccounts() : RetVal[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public CustomerAccounts Get(int CustomerAccountId)
        {
            CustomerAccounts RetVal = new CustomerAccounts();
            try
            {
                List<CustomerAccounts> list = GetListByCustomerAccountId(CustomerAccountId);
                if (list.Count > 0)
                {
                    RetVal = (CustomerAccounts)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static CustomerAccounts Static_GetByAccountTypeId(byte AccountTypeId, List<CustomerAccounts> list)
        {
            CustomerAccounts RetVal = new CustomerAccounts();
            try
            {
                RetVal = list.Find(i => i.AccountTypeId == AccountTypeId);
                if (RetVal == null) RetVal = new CustomerAccounts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CustomerAccounts Get()
        {
            return Get(this.CustomerAccountId);
        }
        //-------------------------------------------------------------- 
        public static CustomerAccounts Static_Get(int CustomerAccountId)
        {
            return Static_Get(CustomerAccountId);
        }
        //--------------------------------------------------------------     
        public static List<CustomerAccounts> Static_GetListByCustomerId(int CustomerId)
        {
            List<CustomerAccounts> RetVal = new List<CustomerAccounts>();
            try
            {
                CustomerAccounts m_CustomerAccounts = new CustomerAccounts();
                RetVal = m_CustomerAccounts.GetListByCustomerId(CustomerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<CustomerAccounts> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<CustomerAccounts> RetVal = new List<CustomerAccounts>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", this.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
    }
}

