
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
    public class Customers
    {
        private int _CustomerId;
        private string _CustomerName;
        private string _CustomerPass;
        private string _CustomerFullName;
        private string _CustomerNickName;
        private string _CustomerMail;
        private string _Website;
        private string _Facebook;
        private string _Avatar;        
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
        private short _OrganOccupationId;
        private string _Address;
        private string _AccountNumber;
        private string _OrganName;
        private string _OrganPhone;
        private string _OrganFax;
        private string _OrganAddress;
        private string _OrganTaxCode;
        private string _Note;
        private short _InfoChannelId;
        private short _ServiceId;
        private short _NewsletterGroupId;
        private byte _RegisterNewsletter;
        private DateTime _LastLoginTime;
        private DateTime _EndTime;
        private int _VBQT;
        private int _TCVN;
        private int _TTHC;
        private int _VBHN;
        private int _VBTA;
        private byte _MaxConcurrentLogin;
        private int _SumByStatus1;
        private int _SumByStatus2;
        private int _SumByTime1;
        private int _SumByTime2;
        private byte _LockPassword;
        private short _ServicePackageId;
        private string _ServicePackageDesc;
        private string _ServiceDesc;
        private int _SumByServicePackage;
        private int _TotalByServicePackage;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Customers()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Customers(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Customers()
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
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        //-----------------------------------------------------------------
        public string Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; }
        }    
        //-----------------------------------------------------------------
        public string Avatar
        {
            get { return _Avatar; }
            set { _Avatar = value; }
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

        public short OrganOccupationId
        {
            get { return _OrganOccupationId; }
            set { _OrganOccupationId = value; }
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
        public string OrganTaxCode
        {
            get { return _OrganTaxCode; }
            set { _OrganTaxCode = value; }
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
        public short ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
        public short NewsletterGroupId
        {
            get { return _NewsletterGroupId; }
            set { _NewsletterGroupId = value; }
        }
        //-----------------------------------------------------------------
        public byte RegisterNewsletter
        {
            get { return _RegisterNewsletter; }
            set { _RegisterNewsletter = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public int VBQT
        {
            get { return _VBQT; }
            set { _VBQT = value; }
        }
        //-----------------------------------------------------------------
        public int TCVN
        {
            get { return _TCVN; }
            set { _TCVN = value; }
        }
        //-----------------------------------------------------------------
        public int TTHC
        {
            get { return _TTHC; }
            set { _TTHC = value; }
        }
        //-----------------------------------------------------------------
        public int VBHN
        {
            get { return _VBHN; }
            set { _VBHN = value; }
        }
        //-----------------------------------------------------------------
        public int VBTA
        {
            get { return _VBTA; }
            set { _VBTA = value; }
        }
        //-----------------------------------------------------------------
        public byte MaxConcurrentLogin
        {
            get { return _MaxConcurrentLogin; }
            set { _MaxConcurrentLogin = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus1
        {
            get { return _SumByStatus1; }
            set { _SumByStatus1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus2
        {
            get { return _SumByStatus2; }
            set { _SumByStatus2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByTime1
        {
            get { return _SumByTime1; }
            set { _SumByTime1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByTime2
        {
            get { return _SumByTime2; }
            set { _SumByTime2 = value; }
        }
        //-----------------------------------------------------------------
        public short ServicePackageId
        {
            get { return _ServicePackageId; }
            set { _ServicePackageId = value; }
        }
        //-----------------------------------------------------------------
        public string ServicePackageDesc
        {
            get { return _ServicePackageDesc; }
            set { _ServicePackageDesc = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceDesc
        {
            get { return _ServiceDesc; }
            set { _ServiceDesc = value; }
        }
        //-----------------------------------------------------------------
        public int SumByServicePackage
        {
            get { return _SumByServicePackage; }
            set { _SumByServicePackage = value; }
        }
        //-----------------------------------------------------------------
        public int TotalByServicePackage
        {
            get { return _TotalByServicePackage; }
            set { _TotalByServicePackage = value; }
        }
        //-----------------------------------------------------------------
        public byte LockPassword
        {
            get { return _LockPassword; }
            set { _LockPassword = value; }
        }
        //-----------------------------------------------------------------
        private List<Customers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers(db.ConnectionString);
                    m_Customers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers.CustomerPass = smartReader.GetString("CustomerPass");
                    m_Customers.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_Customers.CustomerNickName = smartReader.GetString("CustomerNickName");
                    m_Customers.CustomerMail = smartReader.GetString("CustomerMail");
                    m_Customers.Website = smartReader.GetString("Website");
                    m_Customers.Facebook = smartReader.GetString("Facebook");
                    m_Customers.Avatar = smartReader.GetString("Avatar");
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
                    m_Customers.OrganOccupationId = smartReader.GetInt16("OrganOccupationId");
                    m_Customers.Address = smartReader.GetString("Address");
                    m_Customers.AccountNumber = smartReader.GetString("AccountNumber");
                    m_Customers.OrganName = smartReader.GetString("OrganName");
                    m_Customers.OrganPhone = smartReader.GetString("OrganPhone");
                    m_Customers.OrganFax = smartReader.GetString("OrganFax");
                    m_Customers.OrganAddress = smartReader.GetString("OrganAddress");
                    m_Customers.OrganTaxCode = smartReader.GetString("OrganTaxCode");
                    m_Customers.Note = smartReader.GetString("Note");
                    m_Customers.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                    m_Customers.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                    m_Customers.MaxConcurrentLogin = smartReader.GetByte("MaxConcurrentLogin");
                    m_Customers.LockPassword = smartReader.GetByte("LockPassword");

                    l_Customers.Add(m_Customers);
                }
                reader.Close();
                return l_Customers;
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
        //-----------------------------------------------------------------
        private List<Customers> InitName(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers(db.ConnectionString);
                    m_Customers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers.CustomerFullName = smartReader.GetString("CustomerFullName");

                    l_Customers.Add(m_Customers);
                }
                reader.Close();
                return l_Customers;
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
        public byte UpdateCustomerPass(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_UpdateCustomerPass");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", this.CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
        public byte UpdateAvatar(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_UpdateAvatar");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Avatar", this.Avatar));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
        public byte UpdateStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_UpdateStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
                SqlCommand cmd = new SqlCommand("Customers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", this.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", this.CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", this.CustomerFullName));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", this.CustomerNickName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", this.CustomerMail));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@Facebook", this.Facebook));
                if (!string.IsNullOrEmpty(this.Avatar))
                {
                    cmd.Parameters.Add(new SqlParameter("@Avatar", this.Avatar));
                }
                cmd.Parameters.Add(new SqlParameter("@CustomerBalance", this.CustomerBalance));
                cmd.Parameters.Add(new SqlParameter("@CustomerDayLeft", this.CustomerDayLeft));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", this.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@ClientId", this.ClientId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", this.GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                if (this.DateOfBirth == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", this.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Identifier", this.Identifier));
                cmd.Parameters.Add(new SqlParameter("@IMEI", this.IMEI));
                cmd.Parameters.Add(new SqlParameter("@GCMRegisterId", this.GCMRegisterId));
                cmd.Parameters.Add(new SqlParameter("@MSISDN", this.MSISDN));
                cmd.Parameters.Add(new SqlParameter("@EmailAuto", this.EmailAuto));
                cmd.Parameters.Add(new SqlParameter("@BusinessApplicationPlatformId", this.BusinessApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", this.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", this.PlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", this.BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@AppPlatformId", this.AppPlatformId));
                cmd.Parameters.Add(new SqlParameter("@MapCustomerId", this.MapCustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", this.OccupationId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId", this.OrganOccupationId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@OrganTaxCode", this.OrganTaxCode));
                cmd.Parameters.Add(new SqlParameter("@AccountNumber", this.AccountNumber));
                cmd.Parameters.Add(new SqlParameter("@OrganName", this.OrganName));
                cmd.Parameters.Add(new SqlParameter("@OrganPhone", this.OrganPhone));
                cmd.Parameters.Add(new SqlParameter("@OrganFax", this.OrganFax));
                cmd.Parameters.Add(new SqlParameter("@OrganAddress", this.OrganAddress));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", this.InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", this.RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@MaxConcurrentLogin", this.MaxConcurrentLogin));
                cmd.Parameters.Add(new SqlParameter("@LockPassword", this.LockPassword));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerId = Convert.ToInt32((cmd.Parameters["@CustomerId"].Value == null) ? 0 : (cmd.Parameters["@CustomerId"].Value));
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
        public string Customers_ForgotPassword(byte LanguageId, string CustomerName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ForgotPassword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@ActionMessage"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public string Customers_ResetPass(byte LanguageId, int CustomerId, string CustomerName, string ConfirmCode, string CustomerPass, string FromIP, string UserAgent)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ResetPass");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ConfirmCode", ConfirmCode));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@ActionMessage"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public Customers Customers_GetByEmail()
        {
            Customers RetVal = new Customers();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetByEmail");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", this.CustomerMail));
                List<Customers> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public Customers Customers_GetByMobile()
        {
            Customers RetVal = new Customers();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetByMobile");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", this.CustomerMobile));
                List<Customers> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public Customers Customers_GetById()
        {
            Customers RetVal = new Customers();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                List<Customers> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public Customers Customers_GetByName()
        {
            Customers RetVal = new Customers();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetByName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerName", this.CustomerName));
                List<Customers> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
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
                SqlCommand cmd = new SqlCommand("Customers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
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
        public List<Customers> GetListByCustomerId(int CustomerId)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                if (CustomerId > 0)
                {
                    string sql = "SELECT * FROM Customers WHERE (CustomerId=" + CustomerId.ToString() + ")";
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
        public List<Customers> GetListByListCustomerId(string ListCustomerId)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                string sql = "SELECT CustomerId,CustomerName,CustomerFullName FROM Customers WHERE (CustomerId IN (" + ListCustomerId + "))";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = InitName(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Customers Static_Get(int CustomerId, List<Customers> list)
        {
            Customers RetVal = new Customers();
            try
            {
                RetVal = list.Find(i => i.CustomerId == CustomerId);
                if (RetVal == null) RetVal = new Customers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public static List<Customers> Static_GetListByListCustomerId(string ListCustomerId)
        {
            Customers obj = new Customers();
            return obj.GetListByListCustomerId(ListCustomerId);
        }
        //-------------------------------------------------------------- 
        public Customers Get(int CustomerId)
        {
            Customers RetVal = new Customers(db.ConnectionString);
            try
            {
                List<Customers> list = GetListByCustomerId(CustomerId);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Customers Get()
        {
            return Get(this.CustomerId);
        }
        //-------------------------------------------------------------- 
        public static Customers Static_Get(int CustomerId)
        {
            Customers m_Customers = new Customers();
            m_Customers = m_Customers.Get(CustomerId);
            return m_Customers;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByCrDateTime(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId, 
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByCrDateTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByDailyLogin(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByDailyLogin");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByGroupId(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByGroupId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByInfoChannelId(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByInfoChannelId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByMonth(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByMonth");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByMonthlyLogin(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByMonthlyLogin");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByOccupationId(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByOccupationId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByProvinceId(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByProvinceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Customers_StatisticByServiceId(int ActUserId, short ServiceId, byte StatusId, byte GenderId, short ProvinceId, short OccupationId, short InfoChannelId,
                                                        byte RegisterNewsletter, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_StatisticByServiceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Customers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string CustomerName, string CustomerFullName, string CustomerNickName, 
                                        string CustomerMail, short ServiceId, byte StatusId, string CustomerMobile, byte GenderId, short ProvinceId, int BusinessApplicationPlatformId, 
                                        short ApplicationId, short PlatformId, short BusinessPartnerId, short AppPlatformId, int MapCustomerId, short CustomerGroupId, short OccupationId, short OrganOccupationId,
                                        short InfoChannelId, byte RegisterNewsletter,string OrganTaxCode, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", StringUtil.InjectionString(CustomerName)));                
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", StringUtil.InjectionString(CustomerFullName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", StringUtil.InjectionString(CustomerNickName)));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail",  StringUtil.InjectionString(CustomerMail)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", StringUtil.InjectionString(CustomerMobile)));
                cmd.Parameters.Add(new SqlParameter("@GenderId", GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@BusinessApplicationPlatformId", BusinessApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@AppPlatformId", AppPlatformId));
                cmd.Parameters.Add(new SqlParameter("@MapCustomerId", MapCustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId", OrganOccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@OrganTaxCode", StringUtil.InjectionString(OrganTaxCode))); 
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
        public List<Customers> GetPageWithCaredID(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string CustomerName, string CustomerFullName, string CustomerNickName,
                                        string CustomerMail, short ServiceId, byte StatusId, string CustomerMobile, byte GenderId, short ProvinceId, int BusinessApplicationPlatformId,
                                        short ApplicationId, short PlatformId, short BusinessPartnerId, short AppPlatformId, int MapCustomerId, short CustomerGroupId, short OccupationId, short OrganOccupationId,
                                        short InfoChannelId, byte RegisterNewsletter, string OrganTaxCode, string DateFrom, string DateTo, int CaredStatusID, ref int RowCount)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetPageCared");
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
                cmd.Parameters.Add(new SqlParameter("@BusinessApplicationPlatformId", BusinessApplicationPlatformId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformId", PlatformId));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerId", BusinessPartnerId));
                cmd.Parameters.Add(new SqlParameter("@AppPlatformId", AppPlatformId));
                cmd.Parameters.Add(new SqlParameter("@MapCustomerId", MapCustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", OccupationId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId", OrganOccupationId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@OrganTaxCode", StringUtil.InjectionString(OrganTaxCode)));
                cmd.Parameters.Add(new SqlParameter("@CaredStatusID", CaredStatusID));
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
        public List<Customers> Search(int ActUserId, string OrderBy, string CustomerName, string CustomerFullName, string CustomerNickName, string CustomerMail, short ServiceId, 
                                        byte StatusId, string CustomerMobile, byte GenderId, short ProvinceId, int BusinessApplicationPlatformId, short ApplicationId, 
                                        short PlatformId, short BusinessPartnerId, short AppPlatformId, int MapCustomerId, short CustomerGroupId, short OccupationId, short OrganOccupationId,
                                        short InfoChannelId, byte RegisterNewsletter, string OrganTaxCode,string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, CustomerName, CustomerFullName, CustomerNickName, CustomerMail, ServiceId, StatusId, CustomerMobile, GenderId,
                            ProvinceId, BusinessApplicationPlatformId, ApplicationId, PlatformId, BusinessPartnerId, AppPlatformId, MapCustomerId, CustomerGroupId, OccupationId,OrganOccupationId,
                            InfoChannelId, RegisterNewsletter, OrganTaxCode,DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------
        public List<Customers> GetRelatedList(int actUserId, byte languageId, byte applicationTypeId, int CustomerId, string orderBy)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetRelatedList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", actUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", applicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(orderBy)));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<Customers> Customers_ReportMember_GetPage( string DateFrom, string DateTo,string DateFromSame,string DateToSame,  string OrderBy, int PageIndex, int RowAmount, ref int CountCustomers, ref int CountRegisterNewsletter, ref int CountFreeService, ref int CountTrialService, ref int CountByStatusId1, ref int CountByStatusId2, ref int CountByNewsletterGroupId1, ref int CountByNewsletterGroupId2, ref int CountByNewsletterGroupId3, ref int CountByDocGroupId1, ref int CountByDocGroupId2, ref int CountByDocGroupId3, ref int RowCount)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportMember_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", actUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountCustomers", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountRegisterNewsletter", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountFreeService", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountTrialService", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByStatusId1", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByStatusId2", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByNewsletterGroupId1", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByNewsletterGroupId2", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByNewsletterGroupId3", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByDocGroupId1", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByDocGroupId2", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountByDocGroupId3", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Customers_ReportMember_Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                CountCustomers = int.Parse(cmd.Parameters["@CountCustomers"].Value.ToString());
                CountRegisterNewsletter = int.Parse(cmd.Parameters["@CountRegisterNewsletter"].Value.ToString());
                CountFreeService = int.Parse(cmd.Parameters["@CountFreeService"].Value.ToString());
                CountTrialService = int.Parse(cmd.Parameters["@CountTrialService"].Value.ToString());
                CountByStatusId1 = int.Parse(cmd.Parameters["@CountByStatusId1"].Value.ToString());
                CountByStatusId2 = int.Parse(cmd.Parameters["@CountByStatusId2"].Value.ToString());
                CountByNewsletterGroupId1 = int.Parse(cmd.Parameters["@CountByNewsletterGroupId1"].Value.ToString());
                CountByNewsletterGroupId2 = int.Parse(cmd.Parameters["@CountByNewsletterGroupId2"].Value.ToString());
                CountByNewsletterGroupId3 = int.Parse(cmd.Parameters["@CountByNewsletterGroupId3"].Value.ToString());
                CountByDocGroupId1 = int.Parse(cmd.Parameters["@CountByDocGroupId1"].Value.ToString());
                CountByDocGroupId2 = int.Parse(cmd.Parameters["@CountByDocGroupId2"].Value.ToString());
                CountByDocGroupId3 = int.Parse(cmd.Parameters["@CountByDocGroupId3"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------  
        private List<Customers> Customers_ReportMember_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers_ReportMember = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers_ReportMember = new Customers(db.ConnectionString);
                    m_Customers_ReportMember.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers_ReportMember.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers_ReportMember.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_Customers_ReportMember.CustomerMail = smartReader.GetString("CustomerMail");
                    m_Customers_ReportMember.CustomerMobile = smartReader.GetString("CustomerMobile");
                    m_Customers_ReportMember.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                    m_Customers_ReportMember.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Customers_ReportMember.StatusId = smartReader.GetByte("StatusId");
                    m_Customers_ReportMember.ServiceId = smartReader.GetInt16("ServiceId");
                    m_Customers_ReportMember.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_Customers_ReportMember.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_Customers_ReportMember.EndTime = smartReader.GetDateTime("EndTime");
                    m_Customers_ReportMember.NewsletterGroupId = smartReader.GetInt16("NewsletterGroupId");
                    m_Customers_ReportMember.ApplicationId = smartReader.GetInt16("ApplicationId");
                    m_Customers_ReportMember.LastLoginTime = smartReader.GetDateTime("LastLoginTime");
                    m_Customers_ReportMember.VBQT = smartReader.GetInt32("VBQT");
                    m_Customers_ReportMember.TCVN = smartReader.GetInt32("TCVN");
                    m_Customers_ReportMember.TTHC = smartReader.GetInt32("TTHC");
                    m_Customers_ReportMember.Address = smartReader.GetString("Address");

                    l_Customers_ReportMember.Add(m_Customers_ReportMember);
                }
                reader.Close();
                return l_Customers_ReportMember;
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

        //--------------------------------------------------------------
        /// <summary>
        /// Báo cáo tổng hợp thành viên theo CustomerGroup
        /// </summary>
        public List<Customers> Customers_ReportByCustomerGroup(string DateFrom, string DateTo, string DateFromSame, string DateToSame)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportByCustomerGroup");
                cmd.CommandType = CommandType.StoredProcedure;
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
                RetVal = Customers_ReportByCustomerGroup_Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------  
        private List<Customers> Customers_ReportByCustomerGroup_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers(db.ConnectionString);
                    m_Customers.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_Customers.CustomerName = smartReader.GetString("CustomerGroupName");
                    m_Customers.SumByStatus1 = smartReader.GetInt32("SumByStatus1");
                    m_Customers.SumByStatus2 = smartReader.GetInt32("SumByStatus2");
                    m_Customers.SumByTime1 = smartReader.GetInt32("SumByTime1");
                    m_Customers.SumByTime2 = smartReader.GetInt32("SumByTime2");
                    l_Customers.Add(m_Customers);
                }
                reader.Close();
                return l_Customers;
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

        //--------------------------------------------------------------
        /// <summary>
        /// Báo cáo tổng hợp khách hàng thuê bao
        /// </summary>
        public List<Customers> Customers_ReportByServicePackage(string DateFrom, string DateTo, string DateFromSame, string DateToSame, short ServiceId, string OrderBy)
        {
            List<Customers> RetVal = new List<Customers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportByServicePackage");
                cmd.CommandType = CommandType.StoredProcedure;
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                RetVal = Customers_ReportByServicePackage_Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Báo cáo tổng hợp khách hàng thuê bao
        /// </summary>
        public DataSet Customers_ReportByServicePackageDataSet(string DateFrom, string DateTo, string DateFromSame, string DateToSame, short ServiceId, string OrderBy)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportByServicePackage");
                cmd.CommandType = CommandType.StoredProcedure;
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        private List<Customers> Customers_ReportByServicePackage_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers(db.ConnectionString);
                    m_Customers.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_Customers.ServiceDesc = smartReader.GetString("ServiceDesc");
                    m_Customers.ServicePackageDesc = smartReader.GetString("ServicePackageDesc");
                    m_Customers.SumByServicePackage = smartReader.GetInt32("SumByServicePackage");
                    m_Customers.TotalByServicePackage = smartReader.GetInt32("TotalByServicePackage");
                    l_Customers.Add(m_Customers);
                }
                reader.Close();
                return l_Customers;
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

        //------------------------------------------------------------
        public string GetImageUrl()
        {
            string RetVal = _Avatar;
            if (string.IsNullOrEmpty(_Avatar))
            {
                RetVal = "";
            }
            else
            {
                if (!RetVal.StartsWith("http://")) RetVal = ICSoft.CMSLib.CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            }

            return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            return GetImageUrl().Replace("/Original/", "/Icon/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_IconWithHtmlTag(int width, int height)
        {
            string result = "";
            string imageUrl = GetImageUrl_Icon();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                result = "<a class=\"popup\" href=\"" + GetImageUrl() + "\"><img style=\"width:" + width.ToString() + "px;height:" + height.ToString() + "px\" src=\"" + imageUrl + "\" /></a>";
            }
            return result;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Thumb()
        {
            return GetImageUrl().Replace("/Original/", "/Thumb/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Standard()
        {
            return GetImageUrl().Replace("/Original/", "/Standard/");
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl_Mobile()
        {
            return GetImageUrl().Replace("/Original/", "/Mobile/");
        }
        private List<Customers> Customers_ReportVBQT_View_Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Customers> l_Customers_ReportVBQT_View = new List<Customers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Customers m_Customers_ReportVBQT_View = new Customers(db.ConnectionString);
                    m_Customers_ReportVBQT_View.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers_ReportVBQT_View.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers_ReportVBQT_View.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_Customers_ReportVBQT_View.VBQT = smartReader.GetInt32("VBQT");
                    m_Customers_ReportVBQT_View.TCVN = smartReader.GetInt32("TCVN");
                    m_Customers_ReportVBQT_View.TTHC = smartReader.GetInt32("TTHC");
                    m_Customers_ReportVBQT_View.VBTA = smartReader.GetInt32("VBTA");
                    m_Customers_ReportVBQT_View.VBHN = smartReader.GetInt32("VBHN");
                    l_Customers_ReportVBQT_View.Add(m_Customers_ReportVBQT_View);
                }
                reader.Close();
                return l_Customers_ReportVBQT_View;
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
        /// <summary>
        /// Báo cáo tổng hợp khách hàng thuê bao
        /// </summary>
        public Customers Customers_ReportVBQT_View(int actUserId, int RowAmount, int PageIndex, int CustomerId, short RegistTypeId , string orderBy, ref int RowCount)
        {
            Customers RetVal = new Customers();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportVBQT_View");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", actUserId));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(orderBy)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Customers> list = Customers_ReportVBQT_View_Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Customers)list[0];
                }
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