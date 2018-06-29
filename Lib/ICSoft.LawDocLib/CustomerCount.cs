
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
    public class CustomerCount
    {
        private int _CustomerCountId;
        private int _CustomerId;
        private string _CustomerName;
        private string _CustomerFullName;
        private string _CustomerNickName;
        private string _CustomerMail;
        private byte _StatusId;
        private short _SiteId;
        private string _CustomerMobile;
        private int _LoginCount;
        private DateTime _LastLoginTime;
        private int _ChangePassCount;
        private DateTime _LastChangePassTime;
        private int _PaymentCount;
        private DateTime _FirstPaymentTime;
        private DateTime _LastPaymentTime;
        private int _TotalPaymentMoney;
        private int _LastPaymentMoney;
        private DateTime _ServiceBeginTime;
        private DateTime _ServiceEndTime;
        private short _CustomerGroupId;
        private short _OccupationId;
        private string _Address;
        private string _AccountNumber;
        private string _OrganName;
        private string _OrganPhone;
        private string _OrganFax;
        private string _OrganAddress;
        private short _InfoChannelId;
        private byte _RegisterNewsletter;
        private byte _MaxConcurrentLogin;
        private string _ServiceDetail;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerCount()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerCount(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerCount()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerCountId
        {
            get { return _CustomerCountId; }
            set { _CustomerCountId = value; }
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
        public string CustomerMobile
        {
            get { return _CustomerMobile; }
            set { _CustomerMobile = value; }
        }
        //-----------------------------------------------------------------
        public int LoginCount
        {
            get { return _LoginCount; }
            set { _LoginCount = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; }
        }
        //-----------------------------------------------------------------
        public int ChangePassCount
        {
            get { return _ChangePassCount; }
            set { _ChangePassCount = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastChangePassTime
        {
            get { return _LastChangePassTime; }
            set { _LastChangePassTime = value; }
        }
        //-----------------------------------------------------------------
        public int PaymentCount
        {
            get { return _PaymentCount; }
            set { _PaymentCount = value; }
        }
        //-----------------------------------------------------------------
        public DateTime FirstPaymentTime
        {
            get { return _FirstPaymentTime; }
            set { _FirstPaymentTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastPaymentTime
        {
            get { return _LastPaymentTime; }
            set { _LastPaymentTime = value; }
        }
        //-----------------------------------------------------------------
        public int TotalPaymentMoney
        {
            get { return _TotalPaymentMoney; }
            set { _TotalPaymentMoney = value; }
        }
        //-----------------------------------------------------------------
        public int LastPaymentMoney
        {
            get { return _LastPaymentMoney; }
            set { _LastPaymentMoney = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ServiceBeginTime
        {
            get { return _ServiceBeginTime; }
            set { _ServiceBeginTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ServiceEndTime
        {
            get { return _ServiceEndTime; }
            set { _ServiceEndTime = value; }
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
        public string ServiceDetail
        {
            get { return _ServiceDetail; }
            set { _ServiceDetail = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerCount> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerCount> l_CustomerCount = new List<CustomerCount>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerCount m_CustomerCount = new CustomerCount(db.ConnectionString);
                    m_CustomerCount.CustomerCountId = smartReader.GetInt32("CustomerCountId");
                    m_CustomerCount.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerCount.CustomerName = smartReader.GetString("CustomerName");
                    m_CustomerCount.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_CustomerCount.CustomerNickName = smartReader.GetString("CustomerNickName");
                    m_CustomerCount.CustomerMail = smartReader.GetString("CustomerMail");
                    m_CustomerCount.SiteId = smartReader.GetInt16("SiteId");
                    m_CustomerCount.StatusId = smartReader.GetByte("StatusId");
                    m_CustomerCount.CustomerMobile = smartReader.GetString("CustomerMobile");
                    m_CustomerCount.LoginCount = smartReader.GetInt32("LoginCount");
                    m_CustomerCount.LastLoginTime = smartReader.GetDateTime("LastLoginTime");
                    m_CustomerCount.ChangePassCount = smartReader.GetInt32("ChangePassCount");
                    m_CustomerCount.LastChangePassTime = smartReader.GetDateTime("LastChangePassTime");
                    m_CustomerCount.PaymentCount = smartReader.GetInt32("PaymentCount");
                    m_CustomerCount.FirstPaymentTime = smartReader.GetDateTime("FirstPaymentTime");
                    m_CustomerCount.LastPaymentTime = smartReader.GetDateTime("LastPaymentTime");
                    m_CustomerCount.TotalPaymentMoney = smartReader.GetInt32("TotalPaymentMoney");
                    m_CustomerCount.LastPaymentMoney = smartReader.GetInt32("LastPaymentMoney");
                    m_CustomerCount.ServiceBeginTime = smartReader.GetDateTime("ServiceBeginTime");
                    m_CustomerCount.ServiceEndTime = smartReader.GetDateTime("ServiceEndTime");
                    m_CustomerCount.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_CustomerCount.OccupationId = smartReader.GetInt16("OccupationId");
                    m_CustomerCount.Address = smartReader.GetString("Address");
                    m_CustomerCount.AccountNumber = smartReader.GetString("AccountNumber");
                    m_CustomerCount.OrganName = smartReader.GetString("OrganName");
                    m_CustomerCount.OrganPhone = smartReader.GetString("OrganPhone");
                    m_CustomerCount.OrganFax = smartReader.GetString("OrganFax");
                    m_CustomerCount.OrganAddress = smartReader.GetString("OrganAddress");
                    m_CustomerCount.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                    m_CustomerCount.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                    m_CustomerCount.MaxConcurrentLogin = smartReader.GetByte("MaxConcurrentLogin");
                    m_CustomerCount.ServiceDetail = smartReader.GetString("ServiceDetail");
                    m_CustomerCount.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CustomerCount.Add(m_CustomerCount);
                }
                reader.Close();
                return l_CustomerCount;
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
        public List<CustomerCount> CustomerCount_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string CustomerName, string CustomerFullName,
                                                     string CustomerNickName, string CustomerMail, short ServiceId, byte StatusId, string CustomerMobile, byte GenderId,
                                                     short ProvinceId, short CustomerGroupId, short OccupationId, short InfoChannelId, byte RegisterNewsletter,
                                                     string SearchByDate, string DateFrom, string DateTo, string SearchByValue, int FromValue, int ToValue, ref int RowCount, string GroupCustomer = "")
        {
            List<CustomerCount> RetVal = new List<CustomerCount>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerCount_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", StringUtil.InjectionString(CustomerFullName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", StringUtil.InjectionString(CustomerNickName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", StringUtil.InjectionString(CustomerMail)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", StringUtil.InjectionString(CustomerMobile)));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@GroupCustomer", StringUtil.InjectionString(GroupCustomer)));
                cmd.Parameters.Add(new SqlParameter("@SearchByValue", StringUtil.InjectionString(SearchByValue)));
                cmd.Parameters.Add(new SqlParameter("@FromValue", FromValue));
                cmd.Parameters.Add(new SqlParameter("@ToValue", ToValue));
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

        public List<CustomerCount> CustomerCount_GetPage_Keyword(int ActUserId, int RowAmount, int PageIndex, string OrderBy,string Keyword, string CustomerName, string CustomerFullName,
                                                     string CustomerNickName, string CustomerMail, short ServiceId, byte StatusId, string CustomerMobile, byte GenderId,
                                                     short ProvinceId, short CustomerGroupId, short OccupationId, short InfoChannelId, byte RegisterNewsletter,
                                                     string SearchByDate, string DateFrom, string DateTo, string SearchByValue, int FromValue, int ToValue, ref int RowCount, string GroupCustomer = "")
        {
            List<CustomerCount> RetVal = new List<CustomerCount>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerCount_GetPage_Keyword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@Keyword", StringUtil.InjectionString(Keyword)));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", StringUtil.InjectionString(CustomerFullName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", StringUtil.InjectionString(CustomerNickName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", StringUtil.InjectionString(CustomerMail)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", StringUtil.InjectionString(CustomerMobile)));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@GroupCustomer", StringUtil.InjectionString(GroupCustomer)));
                cmd.Parameters.Add(new SqlParameter("@SearchByValue", StringUtil.InjectionString(SearchByValue)));
                cmd.Parameters.Add(new SqlParameter("@FromValue", FromValue));
                cmd.Parameters.Add(new SqlParameter("@ToValue", ToValue));
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
        
    }
}