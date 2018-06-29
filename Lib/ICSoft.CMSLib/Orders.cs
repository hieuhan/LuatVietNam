
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;

namespace ICSoft.CMSLib
{
    public class Orders
    {
        private int _OrderId;
        private string _OrderName;
        private string _OrderDesc;
        private string _OrderCode;
        private int _CustomerId;
        private short _SiteId;
        private string _FullName;
        private short _CountryId;
        private short _ProvinceId;
        private short _DistrictId;
        private string _Address;
        private string _Mobile;
        private string _Email;
        private string _CouponCode;
        private double _CouponValue;
        private byte _RequireInvoice;
        private string _CompanyName;
        private string _CompanyAddress;
        private string _CompanyTaxCode;
        private double _OrderValue;
        private double _DeliveryFee;
        private string _DeliveryNote;
        private byte _PaymentTypeId;
        private byte _OrderStatusId;
        private byte _DeliveryStatusId;
        private short _PartnerId;
        private string _FromIP;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Orders()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Orders(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Orders()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        //-----------------------------------------------------------------
        public string OrderName
        {
            get { return _OrderName; }
            set { _OrderName = value; }
        }
        //-----------------------------------------------------------------
        public string OrderDesc
        {
            get { return _OrderDesc; }
            set { _OrderDesc = value; }
        }
        //-----------------------------------------------------------------
        public string OrderCode
        {
            get { return _OrderCode; }
            set { _OrderCode = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //-----------------------------------------------------------------
        public short CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        //-----------------------------------------------------------------
        public short ProvinceId
        {
            get { return _ProvinceId; }
            set { _ProvinceId = value; }
        }
        //-----------------------------------------------------------------
        public short DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }
        //-----------------------------------------------------------------
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //-----------------------------------------------------------------
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //-----------------------------------------------------------------
        public double CouponValue
        {
            get { return _CouponValue; }
            set { _CouponValue = value; }
        }
        //-----------------------------------------------------------------
        public byte RequireInvoice
        {
            get { return _RequireInvoice; }
            set { _RequireInvoice = value; }
        }
        //-----------------------------------------------------------------
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        //-----------------------------------------------------------------
        public string CompanyAddress
        {
            get { return _CompanyAddress; }
            set { _CompanyAddress = value; }
        }
        //-----------------------------------------------------------------
        public string CompanyTaxCode
        {
            get { return _CompanyTaxCode; }
            set { _CompanyTaxCode = value; }
        }
        //-----------------------------------------------------------------
        public double OrderValue
        {
            get { return _OrderValue; }
            set { _OrderValue = value; }
        }
        //-----------------------------------------------------------------
        public double DeliveryFee
        {
            get { return _DeliveryFee; }
            set { _DeliveryFee = value; }
        }
        //-----------------------------------------------------------------
        public string DeliveryNote
        {
            get { return _DeliveryNote; }
            set { _DeliveryNote = value; }
        }
        //-----------------------------------------------------------------
        public byte PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte OrderStatusId
        {
            get { return _OrderStatusId; }
            set { _OrderStatusId = value; }
        }
        //-----------------------------------------------------------------
        public byte DeliveryStatusId
        {
            get { return _DeliveryStatusId; }
            set { _DeliveryStatusId = value; }
        }
        //-----------------------------------------------------------------
        public short PartnerId
        {
            get { return _PartnerId; }
            set { _PartnerId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
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

        private List<Orders> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Orders> l_Orders = new List<Orders>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Orders m_Orders = new Orders(db.ConnectionString);
                    m_Orders.OrderId = smartReader.GetInt32("OrderId");
                    m_Orders.OrderName = smartReader.GetString("OrderName");
                    m_Orders.OrderDesc = smartReader.GetString("OrderDesc");
                    m_Orders.OrderCode = smartReader.GetString("OrderCode");
                    m_Orders.SiteId = smartReader.GetInt16("SiteId");
                    m_Orders.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Orders.FullName = smartReader.GetString("FullName");
                    m_Orders.CountryId = smartReader.GetInt16("CountryId");
                    m_Orders.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_Orders.DistrictId = smartReader.GetInt16("DistrictId");
                    m_Orders.Address = smartReader.GetString("Address");
                    m_Orders.Mobile = smartReader.GetString("Mobile");
                    m_Orders.Email = smartReader.GetString("Email");
                    m_Orders.CouponCode = smartReader.GetString("CouponCode");
                    m_Orders.CouponValue = smartReader.GetDouble("CouponValue");
                    m_Orders.RequireInvoice = smartReader.GetByte("RequireInvoice");
                    m_Orders.CompanyName = smartReader.GetString("CompanyName");
                    m_Orders.CompanyAddress = smartReader.GetString("CompanyAddress");
                    m_Orders.CompanyTaxCode = smartReader.GetString("CompanyTaxCode");
                    m_Orders.OrderValue = smartReader.GetDouble("OrderValue");
                    m_Orders.DeliveryFee = smartReader.GetDouble("DeliveryFee");
                    m_Orders.DeliveryNote = smartReader.GetString("DeliveryNote");
                    m_Orders.PaymentTypeId = smartReader.GetByte("PaymentTypeId");
                    m_Orders.OrderStatusId = smartReader.GetByte("OrderStatusId");
                    m_Orders.DeliveryStatusId = smartReader.GetByte("DeliveryStatusId");
                    m_Orders.PartnerId = smartReader.GetInt16("PartnerId");
                    m_Orders.FromIP = smartReader.GetString("FromIP");
                    m_Orders.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Orders.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Orders.Add(m_Orders);
                }
                reader.Close();
                return l_Orders;
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
                SqlCommand cmd = new SqlCommand("Orders_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderName", this.OrderName));
                cmd.Parameters.Add(new SqlParameter("@OrderDesc", this.OrderDesc));
                cmd.Parameters.Add(new SqlParameter("@OrderCode", this.OrderCode));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@CouponCode", this.CouponCode));
                cmd.Parameters.Add(new SqlParameter("@CouponValue", this.CouponValue));
                cmd.Parameters.Add(new SqlParameter("@RequireInvoice", this.RequireInvoice));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", this.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@CompanyAddress", this.CompanyAddress));
                cmd.Parameters.Add(new SqlParameter("@CompanyTaxCode", this.CompanyTaxCode));
                cmd.Parameters.Add(new SqlParameter("@OrderValue", this.OrderValue));
                cmd.Parameters.Add(new SqlParameter("@DeliveryFee", this.DeliveryFee));
                cmd.Parameters.Add(new SqlParameter("@DeliveryNote", this.DeliveryNote));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatusId", this.OrderStatusId));
                cmd.Parameters.Add(new SqlParameter("@DeliveryStatusId", this.DeliveryStatusId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@OrderId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrderId = (cmd.Parameters["@OrderId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@OrderId"].Value);
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
                SqlCommand cmd = new SqlCommand("Orders_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderName", this.OrderName));
                cmd.Parameters.Add(new SqlParameter("@OrderDesc", this.OrderDesc));
                cmd.Parameters.Add(new SqlParameter("@OrderCode", this.OrderCode));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@CouponCode", this.CouponCode));
                cmd.Parameters.Add(new SqlParameter("@CouponValue", this.CouponValue));
                cmd.Parameters.Add(new SqlParameter("@RequireInvoice", this.RequireInvoice));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", this.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@CompanyAddress", this.CompanyAddress));
                cmd.Parameters.Add(new SqlParameter("@CompanyTaxCode", this.CompanyTaxCode));
                cmd.Parameters.Add(new SqlParameter("@OrderValue", this.OrderValue));
                cmd.Parameters.Add(new SqlParameter("@DeliveryFee", this.DeliveryFee));
                cmd.Parameters.Add(new SqlParameter("@DeliveryNote", this.DeliveryNote));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatusId", this.OrderStatusId));
                cmd.Parameters.Add(new SqlParameter("@DeliveryStatusId", this.DeliveryStatusId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
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
                SqlCommand cmd = new SqlCommand("Orders_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderName", this.OrderName));
                cmd.Parameters.Add(new SqlParameter("@OrderDesc", this.OrderDesc));
                cmd.Parameters.Add(new SqlParameter("@OrderCode", this.OrderCode));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@CouponCode", this.CouponCode));
                cmd.Parameters.Add(new SqlParameter("@CouponValue", this.CouponValue));
                cmd.Parameters.Add(new SqlParameter("@RequireInvoice", this.RequireInvoice));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", this.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@CompanyAddress", this.CompanyAddress));
                cmd.Parameters.Add(new SqlParameter("@CompanyTaxCode", this.CompanyTaxCode));
                cmd.Parameters.Add(new SqlParameter("@OrderValue", this.OrderValue));
                cmd.Parameters.Add(new SqlParameter("@DeliveryFee", this.DeliveryFee));
                cmd.Parameters.Add(new SqlParameter("@DeliveryNote", this.DeliveryNote));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatusId", this.OrderStatusId));
                cmd.Parameters.Add(new SqlParameter("@DeliveryStatusId", this.DeliveryStatusId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrderId = (cmd.Parameters["@OrderId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@OrderId"].Value);
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
                SqlCommand cmd = new SqlCommand("Orders_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
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
        public List<Orders> GetListByOrderId(int OrderId)
        {
            List<Orders> RetVal = new List<Orders>();
            try
            {
                if (OrderId > 0)
                {
                    string sql = "SELECT * FROM Orders WHERE (OrderId=" + OrderId.ToString() + ")";
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
        public Orders Get(int OrderId)
        {
            Orders RetVal = new Orders();
            try
            {
                List<Orders> list = GetListByOrderId(OrderId);
                if (list.Count > 0)
                {
                    RetVal = (Orders)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Orders Get()
        {
            return Get(this.OrderId);
        }
        public static Orders Static_Get(int OrderId)
        {
            return new Orders().Get(OrderId);
        }
        //--------------------------------------------------------------     
        public List<Orders> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Orders> RetVal = new List<Orders>();
            try
            {
                SqlCommand cmd = new SqlCommand("Orders_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@OrderName", this.OrderName));
                cmd.Parameters.Add(new SqlParameter("@OrderCode", this.OrderCode));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@CouponCode", this.CouponCode));
                cmd.Parameters.Add(new SqlParameter("@RequireInvoice", this.RequireInvoice));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", this.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@CompanyTaxCode", this.CompanyTaxCode));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatusId", this.OrderStatusId));
                cmd.Parameters.Add(new SqlParameter("@DeliveryStatusId", this.DeliveryStatusId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
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

