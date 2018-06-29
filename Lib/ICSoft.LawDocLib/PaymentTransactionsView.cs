
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
    public class PaymentTransactionsView
    {
        private byte _AccountTypeId = 0;
        private string _PlusSub = "";
        private int _PaymentTransactionId;
        private short _SiteId;
        private int _Amount;
        private short _ServicePackageId;
        private int _OrderId;
        private int _DataId;
        private byte _DataTypeId;
        private byte _TransactionStatusId;
        private byte _TransactionTypeId;
        private string _TransactionDesc;
        private string _TransactionCode;
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
        private CustomersView _mCustomer;
        private ServicesView _mService;
        private ServicePackagesView _mServicePackage;
        //-----------------------------------------------------------------
        public PaymentTransactionsView()
        {
        }
        //-----------------------------------------------------------------        
        ~PaymentTransactionsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        public byte AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }
        public string PlusSub
        {
            get { return _PlusSub; }
            set { _PlusSub = value; }
        }
        //-----------------------------------------------------------------    
        public CustomersView mCustomer
        {
            get { return _mCustomer; }
            set { _mCustomer = value; }
        }
        //-----------------------------------------------------------------    
        public ServicesView mService
        {
            get { return _mService; }
            set { _mService = value; }
        }
        //-----------------------------------------------------------------    
        public ServicePackagesView mServicePackage
        {
            get { return _mServicePackage; }
            set { _mServicePackage = value; }
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
        public static List<PaymentTransactionsView> Init(SmartDataReader smartReader)
        {
            List<PaymentTransactionsView> l_PaymentTransactions = new List<PaymentTransactionsView>();
            try
            {
                while (smartReader.Read())
                {
                    PaymentTransactionsView m_PaymentTransactions = new PaymentTransactionsView();
                    m_PaymentTransactions.PaymentTransactionId = smartReader.GetInt32("PaymentTransactionId");
                    m_PaymentTransactions.SiteId = smartReader.GetInt16("SiteId");
                    m_PaymentTransactions.CustomerId = smartReader.GetInt32("CustomerId");
                    m_PaymentTransactions.AccountTypeId = smartReader.GetByte("AccountTypeId");
                    m_PaymentTransactions.PlusSub = smartReader.GetString("PlusSub");
                    m_PaymentTransactions.Amount = smartReader.GetInt32("Amount");
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
                    
                    l_PaymentTransactions.Add(m_PaymentTransactions);
                }
                return l_PaymentTransactions;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
    }
}