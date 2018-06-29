
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
    public class OrderDetails
    {
        private int _OrderDetailId;
        private int _OrderId;
        private int _ProductId;
        private string _ProductName;
        private double _Price;
        private int _Quantity;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public OrderDetails()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OrderDetails(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrderDetails()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int OrderDetailId
        {
            get { return _OrderDetailId; }
            set { _OrderDetailId = value; }
        }
        //-----------------------------------------------------------------
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        //-----------------------------------------------------------------
        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }
        //-----------------------------------------------------------------
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        //-----------------------------------------------------------------
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        //-----------------------------------------------------------------
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<OrderDetails> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrderDetails> l_OrderDetails = new List<OrderDetails>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrderDetails m_OrderDetails = new OrderDetails(db.ConnectionString);
                    m_OrderDetails.OrderDetailId = smartReader.GetInt32("OrderDetailId");
                    m_OrderDetails.OrderId = smartReader.GetInt32("OrderId");
                    m_OrderDetails.ProductId = smartReader.GetInt32("ProductId");
                    m_OrderDetails.ProductName = smartReader.GetString("ProductName");
                    m_OrderDetails.Price = smartReader.GetDouble("Price");
                    m_OrderDetails.Quantity = smartReader.GetInt32("Quantity");
                    m_OrderDetails.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_OrderDetails.Add(m_OrderDetails);
                }
                reader.Close();
                return l_OrderDetails;
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
                SqlCommand cmd = new SqlCommand("OrderDetails_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
                cmd.Parameters.Add("@OrderDetailId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrderDetailId = (cmd.Parameters["@OrderDetailId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@OrderDetailId"].Value);
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
                SqlCommand cmd = new SqlCommand("OrderDetails_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
                cmd.Parameters.Add(new SqlParameter("@OrderDetailId", this.OrderDetailId));
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
                SqlCommand cmd = new SqlCommand("OrderDetails_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
                cmd.Parameters.Add(new SqlParameter("@OrderDetailId", this.OrderDetailId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrderDetailId = (cmd.Parameters["@OrderDetailId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@OrderDetailId"].Value);
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
                SqlCommand cmd = new SqlCommand("OrderDetails_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderDetailId", this.OrderDetailId));
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
        public List<OrderDetails> GetList(int OrderId)
        {
            List<OrderDetails> RetVal = new List<OrderDetails>();
            try
            {
                string sql = "SELECT * FROM OrderDetails WHERE OrderId=" + OrderId.ToString();
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
        public List<OrderDetails> GetListByOrderDetailId(int OrderDetailId)
        {
            List<OrderDetails> RetVal = new List<OrderDetails>();
            try
            {
                if (OrderDetailId > 0)
                {
                    string sql = "SELECT * FROM OrderDetails WHERE (OrderDetailId=" + OrderDetailId.ToString() + ")";
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
        public OrderDetails Get(int OrderDetailId)
        {
            OrderDetails RetVal = new OrderDetails();
            try
            {
                List<OrderDetails> list = GetListByOrderDetailId(OrderDetailId);
                if (list.Count > 0)
                {
                    RetVal = (OrderDetails)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OrderDetails Get()
        {
            return Get(this.OrderDetailId);
        }
        //-------------------------------------------------------------- 
        public static OrderDetails Static_Get(int OrderDetailId)
        {
            return new OrderDetails().Get(OrderDetailId);
        }
        //--------------------------------------------------------------     
        public static List<OrderDetails> Static_GetList(int OrderId)
        {
            return new OrderDetails().GetList(OrderId);
        }
        //--------------------------------------------------------------     
        public List<OrderDetails> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, ref int RowCount)
        {
            List<OrderDetails> RetVal = new List<OrderDetails>();
            try
            {
                SqlCommand cmd = new SqlCommand("OrderDetails_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", this.ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductName", this.ProductName));
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

