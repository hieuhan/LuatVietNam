
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
    public class CustomerServices
    {
        private int _CustomerServiceId;
        private DateTime _BeginTime;
        private DateTime _EndTime;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DateTime _RenewDateTime;
        private int _CustomerId;
        private short _ServiceId;
        private short _ServicePackageId;        
        private byte _StatusId;
        private ServicePackages _ServicePackages;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerServices()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerServices(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerServices()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerServiceId
        {
            get { return _CustomerServiceId; }
            set { _CustomerServiceId = value; }
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
        public short ServiceId 
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
        //-----------------------------------------------------------------
        public short ServicePackageId
        {
            get { return _ServicePackageId; }
            set { _ServicePackageId = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
        }
        //-----------------------------------------------------------------
        public ServicePackages ServicePackages
        {
            get { return _ServicePackages; }
            set { _ServicePackages = value; }
        }

        public DateTime RenewDateTime
        {
            get
            {
                return _RenewDateTime;
            }

            set
            {
                _RenewDateTime = value;
            }
        }

        //-----------------------------------------------------------------
        private List<CustomerServices> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerServices> l_CustomerServices = new List<CustomerServices>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                
                bool hasMyColumn = (reader.GetSchemaTable().Select("ColumnName = 'ServicePackageName'").Length == 1);
                while (smartReader.Read())
                {
                    CustomerServices m_CustomerServices = new CustomerServices(db.ConnectionString);
                    m_CustomerServices.CustomerServiceId = smartReader.GetInt32("CustomerServiceId");
                    m_CustomerServices.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerServices.ServiceId = smartReader.GetInt16("ServiceId");
                    m_CustomerServices.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_CustomerServices.BeginTime = smartReader.GetDateTime("BeginTime");
                    m_CustomerServices.EndTime = smartReader.GetDateTime("EndTime");
                    m_CustomerServices.RenewDateTime = smartReader.GetDateTime("RenewDateTime");
                    m_CustomerServices.StatusId = smartReader.GetByte("StatusId");
                    m_CustomerServices.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerServices.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_CustomerServices.ServicePackages = new ServicePackages();
                    if (hasMyColumn)
                    {
                        m_CustomerServices.ServicePackages.ServiceId = smartReader.GetInt16("ServiceId");
                        m_CustomerServices.ServicePackages.ServicePackageName = smartReader.GetString("ServicePackageName");
                        m_CustomerServices.ServicePackages.ServicePackageDesc = smartReader.GetString("ServicePackageDesc");
                        m_CustomerServices.ServicePackages.NumMonthUse = smartReader.GetInt16("NumMonthUse");
                        m_CustomerServices.ServicePackages.NumDayUse = smartReader.GetInt16("NumDayUse");
                        m_CustomerServices.ServicePackages.NumDownload = smartReader.GetInt16("NumDownload");
                        m_CustomerServices.ServicePackages.ConcurrentLogin = smartReader.GetByte("ConcurrentLogin");
                        m_CustomerServices.ServicePackages.Price = smartReader.GetInt32("Price");
                        m_CustomerServices.ServicePackages.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                        m_CustomerServices.ServicePackages.CrUserId = smartReader.GetInt32("CrUserId");
                        m_CustomerServices.ServicePackages.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    }

                    l_CustomerServices.Add(m_CustomerServices);
                }
                reader.Close();
                return l_CustomerServices;
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
                SqlCommand cmd = new SqlCommand("CustomerServices_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
                cmd.Parameters.Add(new SqlParameter("@BeginTime", this.BeginTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerServiceId", this.CustomerServiceId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerServiceId = Convert.ToInt32((cmd.Parameters["@CustomerServiceId"].Value == null) ? 0 : (cmd.Parameters["@CustomerServiceId"].Value));
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
                SqlCommand cmd = new SqlCommand("CustomerServices_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerServiceId", this.CustomerServiceId));
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
        public byte DeleteByCustomerIdAndServiceId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_DeleteBy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
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
        public List<CustomerServices> GetListByCustomerServiceId(int CustomerServiceId)
        {
            List<CustomerServices> RetVal = new List<CustomerServices>();
            try
            {
                if (CustomerServiceId > 0)
                {
                    string sql = "SELECT * FROM CustomerServices WHERE (CustomerServiceId=" + CustomerServiceId.ToString() + ")";
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
        public void UpdateStatusId(byte StatusId)
        {
            List<CustomerServices> RetVal = new List<CustomerServices>();
            try
            {
                if (CustomerServiceId > 0)
                {
                    string sql = "UPDATE CustomerServices SET StatusId = " + StatusId.ToString() + " WHERE (CustomerServiceId=" + CustomerServiceId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    db.ExecuteSQL(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }
        //-------------------------------------------------------------- 
        public CustomerServices Get(int CustomerServiceId)
        {
            CustomerServices RetVal = new CustomerServices(db.ConnectionString);
            try
            {
                List<CustomerServices> list = GetListByCustomerServiceId(CustomerServiceId);
                if (list.Count > 0)
                {
                    RetVal = (CustomerServices)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CustomerServices Get()
        {
            return Get(this.CustomerServiceId);
        }
        //-------------------------------------------------------------- 
        public static CustomerServices Static_Get(int CustomerServiceId)
        {
            return Static_Get(CustomerServiceId);
        }
        //--------------------------------------------------------------     
        public List<CustomerServices> CustomerServices_GetList(int ActUserId, string OrderBy, int CustomerId, short ServiceId)
        {
            List<CustomerServices> RetVal = new List<CustomerServices>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<CustomerServices> CustomerServices_GetListByCustomerId(int CustomerId)
        {
            List<CustomerServices> RetVal = new List<CustomerServices>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_GetListByCustomerID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public DataTable CustomerServices_LVN_GetList(int CustomerId, string CustomerName)
        {
            DataTable RetVal = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                RetVal = db.getDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public DataTable CustomerServices_LVN_GetList(int CustomerId, string CustomerName, byte GetOnlyServiceRegisted, byte LanguageId = 1)
        {
            DataTable RetVal = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@GetOnlyServiceRegisted", GetOnlyServiceRegisted));
                RetVal = db.getDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_KiemtraDvDangKy(int CustomerId, string CustomerName, short ServiceId, ref byte IsRegistService, ref short ServiceId_Process, ref string FeeType, ref string ActionButton, ref string Msg, byte LanguageId = 1)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_KiemtraDvDangKy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@IsRegistService", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ServiceId_Process", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@FeeType", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionButton", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                IsRegistService = Convert.ToByte((cmd.Parameters["@IsRegistService"].Value == null) ? "0" : cmd.Parameters["@IsRegistService"].Value);
                ServiceId_Process = Convert.ToInt16((cmd.Parameters["@ServiceId_Process"].Value == null) ? "0" : cmd.Parameters["@ServiceId_Process"].Value);
                FeeType = cmd.Parameters["@FeeType"].Value == null ? "" : cmd.Parameters["@FeeType"].Value.ToString();
                ActionButton = cmd.Parameters["@ActionButton"].Value == null ? "" : cmd.Parameters["@ActionButton"].Value.ToString();
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_DangKyMienphi(int CustomerId, string CustomerName, string FromIP, ref string Msg, byte LanguageId = 1)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_DangKyMienphi");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_DangKyDungThu(int CustomerId, string CustomerName, string FromIP, ref string Msg)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_DangKyDungThu");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_DangKyThuebao(int CustomerId, string CustomerName, short ServiceId, short ServicePackageId, string PromotionCode, int PaymentTransactionId_Src, string FromIP, ref string Msg, byte LanguageId = 1)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_DangKyThuebao");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", PromotionCode));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId_Src", PaymentTransactionId_Src));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_DangKySudungMaThe(int CustomerId, string CustomerName, short ServiceId, short ServicePackageId, string PromotionCode, string FromIP, ref string Msg)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_DangKySudungMaThe");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", PromotionCode));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public void CustomerServices_LVN_DangKyCMS(int ActUserId, int CustomerId, string CustomerName, short ServiceId, short ServicePackageId, DateTime BeginTime, DateTime EndTime, int MoneyAmount, string FromIP, ref string Msg)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerServices_LVN_DangKyCMS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@BeginTime", BeginTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime", EndTime));
                cmd.Parameters.Add(new SqlParameter("@MoneyAmount", MoneyAmount));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db.ExecuteSQL(cmd);
                Msg = cmd.Parameters["@Msg"].Value == null ? "" : cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}