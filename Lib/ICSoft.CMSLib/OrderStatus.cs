
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class OrderStatus
    {
        private byte _OrderStatusId;
        private string _OrderStatusName;
        private string _OrderStatusDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public OrderStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OrderStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrderStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte OrderStatusId
        {
            get { return _OrderStatusId; }
            set { _OrderStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string OrderStatusName
        {
            get { return _OrderStatusName; }
            set { _OrderStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string OrderStatusDesc
        {
            get { return _OrderStatusDesc; }
            set { _OrderStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<OrderStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrderStatus> l_OrderStatus = new List<OrderStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrderStatus m_OrderStatus = new OrderStatus(db.ConnectionString);
                    m_OrderStatus.OrderStatusId = smartReader.GetByte("OrderStatusId");
                    m_OrderStatus.OrderStatusName = smartReader.GetString("OrderStatusName");
                    m_OrderStatus.OrderStatusDesc = smartReader.GetString("OrderStatusDesc");
                    m_OrderStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_OrderStatus.Add(m_OrderStatus);
                }
                reader.Close();
                return l_OrderStatus;
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
        public List<OrderStatus> GetList()
        {
            List<OrderStatus> RetVal = new List<OrderStatus>();
            try
            {
                string sql = "SELECT * FROM OrderStatus ORDER BY DisplayOrder, OrderStatusName";
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
        public List<OrderStatus> GetListByOrderStatusId(byte OrderStatusId)
        {
            List<OrderStatus> RetVal = new List<OrderStatus>();
            try
            {
                if (OrderStatusId > 0)
                {
                    string sql = "SELECT * FROM OrderStatus WHERE (OrderStatusId=" + OrderStatusId.ToString() + ")";
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
        public OrderStatus Get(byte OrderStatusId)
        {
            OrderStatus RetVal = new OrderStatus();
            try
            {
                List<OrderStatus> list = GetListByOrderStatusId(OrderStatusId);
                if (list.Count > 0)
                {
                    RetVal = (OrderStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static OrderStatus Static_Get(byte OrderStatusId, List<OrderStatus> list)
        {
            OrderStatus RetVal = new OrderStatus();
            try
            {
                RetVal = list.Find(i => i.OrderStatusId == OrderStatusId);
                if (RetVal == null) RetVal = new OrderStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OrderStatus Get()
        {
            return Get(this.OrderStatusId);
        }
        //-------------------------------------------------------------- 
        public static OrderStatus Static_Get(byte OrderStatusId)
        {
            return new OrderStatus().Get(OrderStatusId);
        }
        //--------------------------------------------------------------     
        public static List<OrderStatus> Static_GetList()
        {
            return new OrderStatus().GetList();
        }
        
    }
}

