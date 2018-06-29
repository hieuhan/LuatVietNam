
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSViewLib
{
    public class CustomersView
    {
        private int _CustomerId;
        private string _CustomerName;
        private string _CustomerPass;
        private string _CustomerFullName;
        private string _CustomerNickName;
        private string _CustomerMail;
        private int _CustomerBalance;
        private short _CustomerDayLeft;
        private short _SiteId;
        private byte _StatusId;
        private DateTime _CrDateTime;
        private string _CustomerMobile;
        private string _ClientId;
        private byte _GenderId;
        private short _ProvinceId;
        private DateTime _DateOfBirth;
        private string _Identifier;
        private string _IMEI;
        private string _GCMRegisterId;
        private string _MSISDN;
        private string _EmailAuto;
        private int _BusinessApplicationPlatformId;
        private short _ApplicationId;
        private short _PlatformId;
        private short _BusinessPartnerId;
        private short _AppPlatformId;
        private int _MapCustomerId;
        private short _CustomerGroupId;
        private short _OccupationId;
        private string _Address;
        private string _AccountNumber;
        private string _OrganName;
        private string _OrganPhone;
        private string _OrganFax;
        private string _OrganAddress;
        private string _Note;
        private short _InfoChannelId;
        private byte _RegisterNewsletter;
        private byte _MaxConcurrentLogin;
        private byte _LockPassword;
        //-----------------------------------------------------------------
        public CustomersView()
        {
        }
        //-----------------------------------------------------------------        
        ~CustomersView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

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
        public string CustomerPass
        {
            get { return _CustomerPass; }
            set { _CustomerPass = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerFullName
        {
            get { return _CustomerFullName; }
            set { _CustomerFullName = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerNickName
        {
            get { return _CustomerNickName; }
            set { _CustomerNickName = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerMail
        {
            get { return _CustomerMail; }
            set { _CustomerMail = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerBalance
        {
            get { return _CustomerBalance; }
            set { _CustomerBalance = value; }
        }
        //-----------------------------------------------------------------
        public short CustomerDayLeft
        {
            get { return _CustomerDayLeft; }
            set { _CustomerDayLeft = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerMobile
        {
            get { return _CustomerMobile; }
            set { _CustomerMobile = value; }
        }
        //-----------------------------------------------------------------
        public string ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        //-----------------------------------------------------------------
        public byte GenderId
        {
            get { return _GenderId; }
            set { _GenderId = value; }
        }
        //-----------------------------------------------------------------
        public short ProvinceId
        {
            get { return _ProvinceId; }
            set { _ProvinceId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        //-----------------------------------------------------------------
        public string Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; }
        }
        //-----------------------------------------------------------------
        public string IMEI
        {
            get { return _IMEI; }
            set { _IMEI = value; }
        }
        //-----------------------------------------------------------------
        public string GCMRegisterId
        {
            get { return _GCMRegisterId; }
            set { _GCMRegisterId = value; }
        }
        //-----------------------------------------------------------------
        public string MSISDN
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }
        //-----------------------------------------------------------------
        public string EmailAuto
        {
            get { return _EmailAuto; }
            set { _EmailAuto = value; }
        }
        //-----------------------------------------------------------------
        public int BusinessApplicationPlatformId
        {
            get { return _BusinessApplicationPlatformId; }
            set { _BusinessApplicationPlatformId = value; }
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
        public short BusinessPartnerId
        {
            get { return _BusinessPartnerId; }
            set { _BusinessPartnerId = value; }
        }
        //-----------------------------------------------------------------
        public short AppPlatformId
        {
            get { return _AppPlatformId; }
            set { _AppPlatformId = value; }
        }
        //-----------------------------------------------------------------
        public int MapCustomerId
        {
            get { return _MapCustomerId; }
            set { _MapCustomerId = value; }
        }
        //-----------------------------------------------------------------
        public short CustomerGroupId
        {
            get { return _CustomerGroupId; }
            set { _CustomerGroupId = value; }
        }
        //-----------------------------------------------------------------
        public short OccupationId
        {
            get { return _OccupationId; }
            set { _OccupationId = value; }
        }
        //-----------------------------------------------------------------
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //-----------------------------------------------------------------
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }
        //-----------------------------------------------------------------
        public string OrganName
        {
            get { return _OrganName; }
            set { _OrganName = value; }
        }
        //-----------------------------------------------------------------
        public string OrganPhone
        {
            get { return _OrganPhone; }
            set { _OrganPhone = value; }
        }
        //-----------------------------------------------------------------
        public string OrganFax
        {
            get { return _OrganFax; }
            set { _OrganFax = value; }
        }
        //-----------------------------------------------------------------
        public string OrganAddress
        {
            get { return _OrganAddress; }
            set { _OrganAddress = value; }
        }
        //-----------------------------------------------------------------
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        //-----------------------------------------------------------------
        public short InfoChannelId
        {
            get { return _InfoChannelId; }
            set { _InfoChannelId = value; }
        }
        //-----------------------------------------------------------------
        public byte RegisterNewsletter
        {
            get { return _RegisterNewsletter; }
            set { _RegisterNewsletter = value; }
        }
        //-----------------------------------------------------------------
        public byte MaxConcurrentLogin
        {
            get { return _MaxConcurrentLogin; }
            set { _MaxConcurrentLogin = value; }
        }
        //-----------------------------------------------------------------
        public byte LockPassword
        {
            get { return _LockPassword; }
            set { _LockPassword = value; }
        }
        //-----------------------------------------------------------------
        public static List<CustomersView> Init(SmartDataReader smartReader)
        {
            List<CustomersView> l_Customers = new List<CustomersView>();
            try
            {
                while (smartReader.Read())
                {
                    CustomersView m_Customers = new CustomersView();
                    m_Customers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers.CustomerPass = smartReader.GetString("CustomerPass");
                    m_Customers.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_Customers.CustomerNickName = smartReader.GetString("CustomerNickName");
                    m_Customers.CustomerMail = smartReader.GetString("CustomerMail");
                    m_Customers.CustomerBalance = smartReader.GetInt32("CustomerBalance");
                    m_Customers.CustomerDayLeft = smartReader.GetInt16("CustomerDayLeft");
                    m_Customers.SiteId = smartReader.GetInt16("SiteId");
                    m_Customers.StatusId = smartReader.GetByte("StatusId");
                    m_Customers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Customers.CustomerMobile = smartReader.GetString("CustomerMobile");
                    m_Customers.ClientId = smartReader.GetString("ClientId");
                    m_Customers.GenderId = smartReader.GetByte("GenderId");
                    m_Customers.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_Customers.DateOfBirth = smartReader.GetDateTime("DateOfBirth");
                    m_Customers.Identifier = smartReader.GetString("Identifier");
                    m_Customers.IMEI = smartReader.GetString("IMEI");
                    m_Customers.GCMRegisterId = smartReader.GetString("GCMRegisterId");
                    m_Customers.MSISDN = smartReader.GetString("MSISDN");
                    m_Customers.EmailAuto = smartReader.GetString("EmailAuto");
                    m_Customers.BusinessApplicationPlatformId = smartReader.GetInt32("BusinessApplicationPlatformId");
                    m_Customers.ApplicationId = smartReader.GetInt16("ApplicationId");
                    m_Customers.PlatformId = smartReader.GetInt16("PlatformId");
                    m_Customers.BusinessPartnerId = smartReader.GetInt16("BusinessPartnerId");
                    m_Customers.AppPlatformId = smartReader.GetInt16("AppPlatformId");
                    m_Customers.MapCustomerId = smartReader.GetInt32("MapCustomerId");
                    m_Customers.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_Customers.OccupationId = smartReader.GetInt16("OccupationId");
                    m_Customers.Address = smartReader.GetString("Address");
                    m_Customers.AccountNumber = smartReader.GetString("AccountNumber");
                    m_Customers.OrganName = smartReader.GetString("OrganName");
                    m_Customers.OrganPhone = smartReader.GetString("OrganPhone");
                    m_Customers.OrganFax = smartReader.GetString("OrganFax");
                    m_Customers.OrganAddress = smartReader.GetString("OrganAddress");
                    m_Customers.Note = smartReader.GetString("Note");
                    m_Customers.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                    m_Customers.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                    m_Customers.MaxConcurrentLogin = smartReader.GetByte("MaxConcurrentLogin");
                    m_Customers.LockPassword = smartReader.GetByte("LockPassword");

                    l_Customers.Add(m_Customers);
                }
                return l_Customers;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        public static CustomersView InitOne(SmartDataReader smartReader)
        {
            List<CustomersView> l_Customers = Init(smartReader);
            return l_Customers.Count > 0 ? l_Customers[0] : new CustomersView();
        }
        //-----------------------------------------------------------------
    }
}

