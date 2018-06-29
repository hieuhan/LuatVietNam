
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;


namespace ICSoft.CMSViewLib
{
    public class CustomerOnline
    {

        private static DBAccess db;
        private int _CustomerOnlineId;
        private int _CustomerId;
        private string _SessionId;
        private string _FromIP;
        private string _CustomerName;
        private DateTime _LastTime;
        //-----------------------------------------------------------------
        ~CustomerOnline()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------
        public int CustomerOnlineId
        {
            get { return _CustomerOnlineId; }
            set { _CustomerOnlineId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastTime
        {
            get { return _LastTime; }
            set { _LastTime = value; }
        }

        //-----------------------------------------------------------------
        public CustomerOnline()
        {
        }
        
        //-----------------------------------------------------------
        public static List<CustomerOnline> InitView(SmartDataReader smartReader)
        {
            return InitView(smartReader, false);
        }
        //-----------------------------------------------------------
        public static List<CustomerOnline> InitView(SmartDataReader smartReader, bool hasViewType)
        {
            List<CustomerOnline> l_CustomerOnline = new List<CustomerOnline>();
            try
            {
                while (smartReader.Read())
                {
                    CustomerOnline m_CustomerOnline = new CustomerOnline();
                    m_CustomerOnline.CustomerOnlineId = smartReader.GetInt32("CustomerOnlineId");
                    m_CustomerOnline.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerOnline.CustomerName = smartReader.GetString("CustomerName");
                    m_CustomerOnline.FromIP = smartReader.GetString("FromIP");
                    m_CustomerOnline.SessionId = smartReader.GetString("SessionId");
                    m_CustomerOnline.LastTime = smartReader.GetDateTime("LastTime");

                    l_CustomerOnline.Add(m_CustomerOnline);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return l_CustomerOnline;
        }
        public static List<CustomerOnline> CustomerOnline_GetAll(string SessionId = "", string CustomerName = "", string FromIP = "")
        {
            List<CustomerOnline> RetVal = new List<CustomerOnline>();
            try
            {
                
                SqlCommand cmd = new SqlCommand("CustomerOnline_GetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SessionId", SessionId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<CustomerOnline> lCustomers = InitView(smartReader);
                RetVal = lCustomers;
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }

    }
}