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
    public class CustomerLogReport
    {
        private int _CustomerId;
        private string _CustomerName;
        private string _CustomerFullName;
        private int _ServicePackageId;
        private string _ServiceDesc;
        private int _PaymentTypeId;
        private int _MaxConcurrentLogin;
        private DateTime _BeginTime;
        private DateTime _EndTime;
        private int _StatusId;
        private double _TotalPaymentMoney;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerLogReport()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerLogReport(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerLogReport()
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
        public string CustomerFullName
        {
            get { return _CustomerFullName; }
            set { _CustomerFullName = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceDesc
        {
            get { return _ServiceDesc; }
            set { _ServiceDesc = value; }
        }
        //-----------------------------------------------------------------
        public int ServicePackageId
        {
            get { return _ServicePackageId; }
            set { _ServicePackageId = value; }
        }
        //-----------------------------------------------------------------
        public int PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }
        
        //-----------------------------------------------------------------
        public int MaxConcurrentLogin
        {
            get { return _MaxConcurrentLogin; }
            set { _MaxConcurrentLogin = value; }
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
        public int StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
        }
        //-----------------------------------------------------------------
        public double TotalPaymentMoney
        {
            get { return _TotalPaymentMoney; }
            set { _TotalPaymentMoney = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerLogReport> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerLogReport> l_CustomerLogReport = new List<CustomerLogReport>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerLogReport m_CustomerLogReport = new CustomerLogReport(db.ConnectionString);
                    m_CustomerLogReport.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerLogReport.CustomerName = smartReader.GetString("CustomerName");
                    m_CustomerLogReport.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_CustomerLogReport.ServiceDesc = smartReader.GetString("ServiceDesc");
                    m_CustomerLogReport.ServicePackageId = smartReader.GetInt32("ServicePackageId");
                    m_CustomerLogReport.PaymentTypeId = smartReader.GetInt32("PaymentTypeId");
                    m_CustomerLogReport.MaxConcurrentLogin = smartReader.GetInt32("MaxConcurrentLogin");
                    m_CustomerLogReport.BeginTime = smartReader.GetDateTime("BeginTime");
                    m_CustomerLogReport.EndTime = smartReader.GetDateTime("EndTime");
                    m_CustomerLogReport.StatusId = smartReader.GetInt32("StatusId");
                    m_CustomerLogReport.TotalPaymentMoney = smartReader.GetInt32("TotalPaymentMoney");
                    l_CustomerLogReport.Add(m_CustomerLogReport);
                }
                reader.Close();
                return l_CustomerLogReport;
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
        public List<CustomerLogReport> GetPage(int ActUserId, int RowAmount, int PageIndex, short ServiceId, short ServicePackageId, byte PaymentTypeId, byte StatusId, string OrderBy, string DateFrom, string DateTo, string DateFromSame, string DateToSame, ref int RowCount)
        {
            List<CustomerLogReport> RetVal = new List<CustomerLogReport>();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportsService_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
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
