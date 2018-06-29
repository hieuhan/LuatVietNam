
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
    public class PaymentTransactions
    {
        public byte AccountTypeId=0;
        public string PlusSub="";
        private int _PaymentTransactionId;
        private short _SiteId;
        private int _Amount;
        private short _ServicePackageId;
        private int _OrderId;
        private int _DataId;
        private byte _DataTypeId;
        private byte _TransactionStatusId;
        private byte _TransactionTypeId;
        private string _TransactionDesc="";
        private string _TransactionCode = "";
        private string _TransactionData;
        private DateTime _ResponseTime;
        private string _ResponseDesc;
        private DateTime _PaymentDate;
        private int _BalanceAdded;
        private string _FromIP;
        private int _ApplicationPlatformId;
        private short _BusinessPartnerId;
        private short _ApplicationId;
        private short _PlatformId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _CustomerId;
        private byte _PaymentTypeId;
        private DateTime _BeginTime;
        private DateTime _EndTime;
        private short _NumMonthUse;
        private DBAccess db;
        //-----------------------------------------------------------------
        public PaymentTransactions()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public PaymentTransactions(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PaymentTransactions()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int PaymentTransactionId
        {
            get { return _PaymentTransactionId; }
            set { _PaymentTransactionId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //-----------------------------------------------------------------
        public short ServicePackageId
        {
            get { return _ServicePackageId; }
            set { _ServicePackageId = value; }
        }
        //-----------------------------------------------------------------
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        //-----------------------------------------------------------------
        public int DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte TransactionStatusId
        {
            get { return _TransactionStatusId; }
            set { _TransactionStatusId = value; }
        }
        //-----------------------------------------------------------------
        public byte TransactionTypeId
        {
            get { return _TransactionTypeId; }
            set { _TransactionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionDesc
        {
            get { return _TransactionDesc; }
            set { _TransactionDesc = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionData
        {
            get { return _TransactionData; }
            set { _TransactionData = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ResponseTime
        {
            get { return _ResponseTime; }
            set { _ResponseTime = value; }
        }
        //-----------------------------------------------------------------
        public string ResponseDesc
        {
            get { return _ResponseDesc; }
            set { _ResponseDesc = value; }
        }
        //-----------------------------------------------------------------
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        //-----------------------------------------------------------------
        public int BalanceAdded
        {
            get { return _BalanceAdded; }
            set { _BalanceAdded = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public int ApplicationPlatformId
        {
            get { return _ApplicationPlatformId; }
            set { _ApplicationPlatformId = value; }
        }
        //-----------------------------------------------------------------
        public short BusinessPartnerId
        {
            get { return _BusinessPartnerId; }
            set { _BusinessPartnerId = value; }
        }
        //-----------------------------------------------------------------
        public short ApplicationId
        {
            get { return _ApplicationId; }
            set { _ApplicationId = value; }
        }
        //-----------------------------------------------------------------
        public short PlatformId
        {
            get { return _PlatformId; }
            set { _PlatformId = value; }
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
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public byte PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }
        //-----------------------------------------------------------------

        public DateTime BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public short NumMonthUse
        {
            get { return _NumMonthUse; }
            set { _NumMonthUse = value; }
        }
        //-----------------------------------------------------------------

        private List<PaymentTransactions> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PaymentTransactions> l_PaymentTransactions = new List<PaymentTransactions>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PaymentTransactions m_PaymentTransactions = new PaymentTransactions(db.ConnectionString);
                    m_PaymentTransactions.PaymentTransactionId = smartReader.GetInt32("PaymentTransactionId");
                    m_PaymentTransactions.SiteId = smartReader.GetInt16("SiteId");
                    m_PaymentTransactions.CustomerId = smartReader.GetInt32("CustomerId");
                    m_PaymentTransactions.Amount = smartReader.GetInt32("Amount");
                    m_PaymentTransactions.AccountTypeId = smartReader.GetByte("AccountTypeId");
                    m_PaymentTransactions.PlusSub = smartReader.GetString("PlusSub");
                    m_PaymentTransactions.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_PaymentTransactions.OrderId = smartReader.GetInt32("OrderId");
                    m_PaymentTransactions.DataId = smartReader.GetInt32("DataId");
                    m_PaymentTransactions.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_PaymentTransactions.TransactionStatusId = smartReader.GetByte("TransactionStatusId");
                    m_PaymentTransactions.TransactionTypeId = smartReader.GetByte("TransactionTypeId");
                    m_PaymentTransactions.TransactionDesc = smartReader.GetString("TransactionDesc");
                    m_PaymentTransactions.TransactionCode = smartReader.GetString("TransactionCode");
                    m_PaymentTransactions.TransactionData = smartReader.GetString("TransactionData");
                    m_PaymentTransactions.ResponseTime = smartReader.GetDateTime("ResponseTime");
                    m_PaymentTransactions.ResponseDesc = smartReader.GetString("ResponseDesc");
                    m_PaymentTransactions.PaymentTypeId = smartReader.GetByte("PaymentTypeId");
                    m_PaymentTransactions.PaymentDate = smartReader.GetDateTime("PaymentDate");
                    m_PaymentTransactions.BalanceAdded = smartReader.GetInt32("BalanceAdded");
                    m_PaymentTransactions.FromIP = smartReader.GetString("FromIP");
                    m_PaymentTransactions.ApplicationPlatformId = smartReader.GetInt32("ApplicationPlatformId");
                    m_PaymentTransactions.BusinessPartnerId = smartReader.GetInt16("BusinessPartnerId");
                    m_PaymentTransactions.ApplicationId = smartReader.GetInt16("ApplicationId");
                    m_PaymentTransactions.PlatformId = smartReader.GetInt16("PlatformId");
                    m_PaymentTransactions.CrUserId = smartReader.GetInt32("CrUserId");
                    m_PaymentTransactions.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_PaymentTransactions.BeginTime = smartReader.GetDateTime("BeginTime");
                    m_PaymentTransactions.EndTime = smartReader.GetDateTime("EndTime");
                    m_PaymentTransactions.NumMonthUse = smartReader.GetInt16("NumMonthUse");
                    
                    l_PaymentTransactions.Add(m_PaymentTransactions);
                }
                reader.Close();
                return l_PaymentTransactions;
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
        //--------------------------------------------------------------
        public byte UpdateTransactionStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_UpdateTransactionStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", this.PaymentTransactionId));
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@Amount", this.Amount));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@DataId", this.DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", this.TransactionDesc));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", this.TransactionCode));
                cmd.Parameters.Add(new SqlParameter("@TransactionData", this.TransactionData));
                if (this.ResponseTime != DateTime.MinValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@ResponseTime", this.ResponseTime));
                }
                cmd.Parameters.Add(new SqlParameter("@ResponseDesc", this.ResponseDesc));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                if (this.PaymentDate != DateTime.MinValue)
                {
                    cmd.Parameters.Add(new SqlParameter("@PaymentDate", this.PaymentDate));
                }
                cmd.Parameters.Add(new SqlParameter("@BalanceAdded", this.BalanceAdded));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", this.ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", this.BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", this.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", this.PlatformId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", this.PaymentTransactionId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PaymentTransactionId = Convert.ToInt32((cmd.Parameters["@PaymentTransactionId"].Value == null) ? 0 : (cmd.Parameters["@PaymentTransactionId"].Value));
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
        public byte PaymentTransactions_Add(byte Replicated, int ActUserId, int CustomerId, DateTime BeginTimeService, short ServicePackageId, byte TransactionStatusId, 
                                            byte PaymentTypeId, ref int PaymentTransactionId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_Add");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                if (BeginTimeService == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@BeginTimeService", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@BeginTimeService", BeginTimeService));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", PaymentTransactionId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                PaymentTransactionId = Convert.ToInt32((cmd.Parameters["@PaymentTransactionId"].Value == null) ? 0 : (cmd.Parameters["@PaymentTransactionId"].Value));
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
                SqlCommand cmd = new SqlCommand("PaymentTransactions_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", this.PaymentTransactionId));
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
        public List<PaymentTransactions> GetListByPaymentTransactionId(int PaymentTransactionId)
        {
            List<PaymentTransactions> RetVal = new List<PaymentTransactions>();
            try
            {
                if (PaymentTransactionId > 0)
                {
                    string sql = "SELECT * FROM PaymentTransactions WHERE (PaymentTransactionId=" + PaymentTransactionId.ToString() + ")";
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
        public PaymentTransactions Get(int PaymentTransactionId)
        {
            PaymentTransactions RetVal = new PaymentTransactions(db.ConnectionString);
            try
            {
                List<PaymentTransactions> list = GetListByPaymentTransactionId(PaymentTransactionId);
                if (list.Count > 0)
                {
                    RetVal = (PaymentTransactions)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PaymentTransactions Get()
        {
            return Get(this.PaymentTransactionId);
        }
        //-------------------------------------------------------------- 
        public static PaymentTransactions Static_Get(int PaymentTransactionId)
        {
            return Static_Get(PaymentTransactionId);
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByCrDateTime(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId, 
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom, 
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByCrDateTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByMonth(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByMonth");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByPaymentTypeId(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByPaymentTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByTransactionTypeId(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByTransactionTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByDataTypeId(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByDataTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByServiceId(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByServiceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet PaymentTransactions_StatisticByServicePackageId(int ActUserId, short ServiceId, short ServicePackageId, byte TransactionStatusId, byte PaymentTypeId,
                                                                 int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, string DateFrom,
                                                                 string DateTo, ref int TotalCount, ref Int64 TotalMoney)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_StatisticByServicePackageId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32((cmd.Parameters["@TotalCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalCount"].Value));
                TotalMoney = Convert.ToInt64((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<PaymentTransactionsView> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, string CustomerName, short ServiceId,  
                                                  short ServicePackageId, byte TransactionStatusId, string TransactionDesc, string TransactionCode, byte PaymentTypeId, 
                                                  int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId,  int CrUserId,  string DateFrom,
                                                  string DateTo, ref int RowCount, ref int TotalMoney)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Int).Direction = 0;//ParameterDirection.Output
              
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = PaymentTransactionsView.Init(smartReader);

                    //Get customer
                    reader.NextResult();
                    List<CustomersView> l_Customers = CustomersView.Init(smartReader);

                    //Get Service
                    reader.NextResult();
                    List<ServicesView> l_Services = ServicesView.Init(smartReader);

                    //Get Service pagekage
                    reader.NextResult();
                    List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);
                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                        RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                        RetVal[i].mService = ServicesView.Static_Get(RetVal[i].mServicePackage.ServiceId, l_Services);
                    }
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
                finally
                {
                    db.closeConnection(con);
                }

                RowCount = Convert.ToInt32((cmd.Parameters["@RowCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@RowCount"].Value));
                TotalMoney = Convert.ToInt32((cmd.Parameters["@TotalMoney"].Value)==DBNull.Value ? 0: (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<PaymentTransactionsView> GetPageV2(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, string CustomerName, short ServiceId,
                                                  short ServicePackageId, byte TransactionStatusId, string TransactionDesc, string TransactionCode, byte PaymentTypeId,
                                                  int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, int CrUserId, int FromAmount, int ToAmount, string DateFrom,
                                                  string DateTo, ref int RowCount, ref decimal TotalMoney, ref int TotalCustomer)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@FromAmount", FromAmount));
                cmd.Parameters.Add(new SqlParameter("@ToAmount", ToAmount));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = PaymentTransactionsView.Init(smartReader);

                    //Get customer
                    reader.NextResult();
                    List<CustomersView> l_Customers = CustomersView.Init(smartReader);
                    TotalCustomer = l_Customers.Count;
                    //Get Service
                    reader.NextResult();
                    List<ServicesView> l_Services = ServicesView.Init(smartReader);

                    //Get Service pagekage
                    reader.NextResult();
                    List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);
                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                        RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                        RetVal[i].mService = ServicesView.Static_Get(RetVal[i].mServicePackage.ServiceId, l_Services);
                    }
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
                finally
                {
                    db.closeConnection(con);
                }

                RowCount = Convert.ToInt32((cmd.Parameters["@RowCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@RowCount"].Value));
                TotalMoney = Convert.ToInt32((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  

        public List<PaymentTransactionsView> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, string CustomerName, short ServiceId,
                                                  short ServicePackageId, byte TransactionStatusId, string TransactionDesc, string TransactionCode, byte PaymentTypeId,
                                                  int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, int CrUserId, string DateFrom,
                                                  string DateTo, ref int RowCount, ref decimal TotalMoney, ref int TotalCustomer)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = PaymentTransactionsView.Init(smartReader);

                    //Get customer
                    reader.NextResult();
                    List<CustomersView> l_Customers = CustomersView.Init(smartReader);
                    TotalCustomer = l_Customers.Count;
                    //Get Service
                    reader.NextResult();
                    List<ServicesView> l_Services = ServicesView.Init(smartReader);

                    //Get Service pagekage
                    reader.NextResult();
                    List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);
                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                        RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                        RetVal[i].mService = ServicesView.Static_Get(RetVal[i].mServicePackage.ServiceId, l_Services);
                    }
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
                finally
                {
                    db.closeConnection(con);
                }

                RowCount = Convert.ToInt32((cmd.Parameters["@RowCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@RowCount"].Value));
                TotalMoney = Convert.ToInt32((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<PaymentTransactionsView> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short ServiceId, string DateFrom, string DateTo, ref int RowCount, ref int TotalMoney)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", this.PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(this.TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(this.TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", this.ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", this.BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", this.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", this.PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = PaymentTransactionsView.Init(smartReader);

                    //Get customer
                    reader.NextResult();
                    List<CustomersView> l_Customers = CustomersView.Init(smartReader);

                    //Get Service
                    reader.NextResult();
                    List<ServicesView> l_Services = ServicesView.Init(smartReader);

                    //Get Service pagekage
                    reader.NextResult();
                    List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);

                    reader.Close();
                    RowCount = Convert.ToInt32((cmd.Parameters["@RowCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@RowCount"].Value));
                    TotalMoney = Convert.ToInt32((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                        RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                        RetVal[i].mService = ServicesView.Static_Get(RetVal[i].mServicePackage.ServiceId, l_Services);
                    }
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
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public List<PaymentTransactionsView> GetPageView(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short ServiceId, string DateFrom, string DateTo, ref int RowCount, ref int TotalMoney)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPageView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", this.AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", this.PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(this.TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(this.TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", this.ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", this.BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", this.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", this.PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = PaymentTransactionsView.Init(smartReader);

                    //Get customer
                    reader.NextResult();
                    List<CustomersView> l_Customers = CustomersView.Init(smartReader);

                    //Get Service
                    reader.NextResult();
                    List<ServicesView> l_Services = ServicesView.Init(smartReader);

                    //Get Service pagekage
                    reader.NextResult();
                    List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);

                    reader.Close();
                    RowCount = Convert.ToInt32((cmd.Parameters["@RowCount"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@RowCount"].Value));
                    TotalMoney = Convert.ToInt32((cmd.Parameters["@TotalMoney"].Value) == DBNull.Value ? 0 : (cmd.Parameters["@TotalMoney"].Value));
                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                        RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                        RetVal[i].mService = ServicesView.Static_Get(RetVal[i].mServicePackage.ServiceId, l_Services);
                    }
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
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<PaymentTransactionsView> Search(int ActUserId, string OrderBy, int CustomerId, string CustomerName, short ServiceId, short ServicePackageId, byte TransactionStatusId, string TransactionDesc,
                                                 string TransactionCode, byte PaymentTypeId, int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId,
                                                 short PlatformId, int CrUserId, string DateFrom, string DateTo, ref int RowCount, ref int TotalMoney)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, CustomerId, CustomerName, ServiceId, ServicePackageId, TransactionStatusId, TransactionDesc,TransactionCode, PaymentTypeId,
                            ApplicationPlatformId, BusinessPartnerId, ApplicationId, PlatformId, CrUserId, DateFrom, DateTo, ref RowCount, ref TotalMoney);
        }
        public List<PaymentTransactionsView> GetPage_ForParter(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, string CustomerName, short ServiceId,
                                                  short ServicePackageId, byte TransactionStatusId, string TransactionDesc, string TransactionCode, byte PaymentTypeId,
                                                  int ApplicationPlatformId, short BusinessPartnerId, short ApplicationId, short PlatformId, int CrUserId, short ProvinceId, string DateFrom,
                                                  string DateTo, ref int RowCount, ref decimal TotalMoney)
        {
            List<PaymentTransactionsView> RetVal = new List<PaymentTransactionsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_GetPage_ForParter");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", AccountTypeId));
                cmd.Parameters.Add(new SqlParameter("@PlusSub", PlusSub));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@TransactionTypeId", this.TransactionTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TransactionDesc", StringUtil.InjectionString(TransactionDesc)));
                cmd.Parameters.Add(new SqlParameter("@TransactionCode", StringUtil.InjectionString(TransactionCode)));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationPlatformId", ApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = PaymentTransactionsView.Init(smartReader);

                //Get customer
                reader.NextResult();
                List<CustomersView> l_Customers = CustomersView.Init(smartReader);

                reader.NextResult();
                List<ServicePackagesView> l_ServicePackages = ServicePackagesView.Init(smartReader);
                reader.Close();
                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].mCustomer = CustomersView.Static_Get(RetVal[i].CustomerId, l_Customers);
                    RetVal[i].mServicePackage = ServicePackagesView.Static_Get(RetVal[i].ServicePackageId, l_ServicePackages);
                }
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
                TotalMoney = Convert.ToInt64(cmd.Parameters["@TotalMoney"].Value);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public bool HasRoleEditAmount(int userId)
        {
            byte hasRole = 0;
            try
            {
                SqlCommand sqlCommand =
                    new SqlCommand("PaymentTransactions_RoleEdit_Amount") {CommandType = CommandType.StoredProcedure};
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", userId));
                sqlCommand.Parameters.Add("@HasRole", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                this.db.ExecuteSQL(sqlCommand);
                hasRole = (byte)sqlCommand.Parameters["@HasRole"].Value;
            }
            catch
            {
                hasRole = 0;
            }
            return hasRole == 1;
        }
        //-----------------------------------------------------------
        public byte Update_Amount(int actUserId, int paymentTransactionId, int amount)
        {
            byte retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTransactions_Update_Amount");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", actUserId));
                cmd.Parameters.Add(new SqlParameter("@Amount", amount));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", paymentTransactionId));
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                retVal = Convert.ToByte(cmd.Parameters["@SysMessageTypeId"].Value ?? "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------   
        public DataSet TotalAmountByServicePackage(DateTime DateFrom, DateTime DateTo)
        {
            DataSet retVal = new DataSet();
            try
            {
                SqlCommand cmd =
                    new SqlCommand("PaymentTransaction_TotalAmount") {CommandType = CommandType.StoredProcedure};                
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                retVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------     
    }
}