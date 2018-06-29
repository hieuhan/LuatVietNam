using sms.database;
using sms.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.LawDocsLib
{
    public class CustomerLogReportHeader
    {
        private int _CountCustomerServices;
        private int _CountPayOnline;
        private int _CountPayOffline;
        private int _CountStatusStillExpired;
        private int _CountStatusExpiringSoon;
        private int _CountServicePackageTC;
        private int _CountServicePackageNC;
        private int _CountServicePackageTA;
        private int _CountServicePackageNV;
        private double _TotalMoney;
        private double _TotalMoneyTC;
        private double _TotalMoneyNC;
        private double _TotalMoneyTA;
        private double _TotalMoneyNV;
        private int _CountPaymentTypeBank;
        private int _CountPaymentTypeIncomOffice;
        private int _CountPaymentTypeLuatVNCard;
        private int _CountNumberPayFirst;
        private int _CountNumberPaySecond;
        private int _CountNumberPay3rd;
        private int _CountNumberPay4rd;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerLogReportHeader()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerLogReportHeader(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerLogReportHeader()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------
        public int CountCustomerServices
        {
            get { return _CountCustomerServices; }
            set { _CountCustomerServices = value; }
        }
        //-----------------------------------------------------------------
        public int CountPayOnline
        {
            get { return _CountPayOnline; }
            set { _CountPayOnline = value; }
        }
        //-----------------------------------------------------------------
        public int CountPayOffline
        {
            get { return _CountPayOffline; }
            set { _CountPayOffline = value; }
        }
        //-----------------------------------------------------------------
        public int CountStatusStillExpired
        {
            get { return _CountStatusStillExpired; }
            set { _CountStatusStillExpired = value; }
        }
        
        //-----------------------------------------------------------------
        public int CountStatusExpiringSoon
        {
            get { return _CountStatusExpiringSoon; }
            set { _CountStatusExpiringSoon = value; }
        }
        //-----------------------------------------------------------------
        public int CountServicePackageTC
        {
            get { return _CountServicePackageTC; }
            set { _CountServicePackageTC = value; }
        }
        //-----------------------------------------------------------------
        public int CountServicePackageNC
        {
            get { return _CountServicePackageNC; }
            set { _CountServicePackageNC = value; }
        }
        //-----------------------------------------------------------------
        public int CountServicePackageTA
        {
            get { return _CountServicePackageTA; }
            set { _CountServicePackageTA = value; }
        }
        //-----------------------------------------------------------------
        public int CountServicePackageNV
        {
            get { return _CountServicePackageNV; }
            set { _CountServicePackageNV = value; }
        }
        //-----------------------------------------------------------------
        public double TotalMoney
        {
            get { return _TotalMoney; }
            set { _TotalMoney = value; }
        }
        //-----------------------------------------------------------------
        public double TotalMoneyTC
        {
            get { return _TotalMoneyTC; }
            set { _TotalMoneyTC = value; }
        }
        //-----------------------------------------------------------------
        public double TotalMoneyNC
        {
            get { return _TotalMoneyNC; }
            set { _TotalMoneyNC = value; }
        }
        //-----------------------------------------------------------------
        public double TotalMoneyTA
        {
            get { return _TotalMoneyTA; }
            set { _TotalMoneyTA = value; }
        }
        //-----------------------------------------------------------------
        public double TotalMoneyNV
        {
            get { return _TotalMoneyNV; }
            set { _TotalMoneyNV = value; }
        }
        //-----------------------------------------------------------------
        public int CountPaymentTypeBank
        {
            get { return _CountPaymentTypeBank; }
            set { _CountPaymentTypeBank = value; }
        }
        //-----------------------------------------------------------------
        public int CountPaymentTypeIncomOffice
        {
            get { return _CountPaymentTypeIncomOffice; }
            set { _CountPaymentTypeIncomOffice = value; }
        }
        //-----------------------------------------------------------------
        public int CountPaymentTypeLuatVNCard
        {
            get { return _CountPaymentTypeLuatVNCard; }
            set { _CountPaymentTypeLuatVNCard = value; }
        }
        //-----------------------------------------------------------------
        public int CountNumberPayFirst
        {
            get { return _CountNumberPayFirst; }
            set { _CountNumberPayFirst = value; }
        }
        //-----------------------------------------------------------------
        public int CountNumberPaySecond
        {
            get { return _CountNumberPaySecond; }
            set { _CountNumberPaySecond = value; }
        }
        //-----------------------------------------------------------------
        public int CountNumberPay3rd
        {
            get { return _CountNumberPay3rd; }
            set { _CountNumberPay3rd = value; }
        }
        //-----------------------------------------------------------------
        public int CountNumberPay4rd
        {
            get { return _CountNumberPay4rd; }
            set { _CountNumberPay4rd = value; }
        }

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
        public CustomerLogReportHeader Get()
        {
            CustomerLogReportHeader RetVal = new CustomerLogReportHeader();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ReportsService_Header");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CountCustomerServices", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPayOnline", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPayOffline", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountStatusStillExpired", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountStatusExpiringSoon", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountServicePackageTC", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountServicePackageNC", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountServicePackageTA", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountServicePackageNV", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoney", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoneyTC", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoneyNC", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoneyTA", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalMoneyNV", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPaymentTypeBank", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPaymentTypeIncomOffice", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPaymentTypeLuatVNCard", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountNumberPayFirst", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountNumberPaySecond", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountNumberPay3rd", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountNumberPay4rd", SqlDbType.Int).Direction = ParameterDirection.Output;
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                }
                reader.Close();
                db.closeConnection(con);
                RetVal.CountCustomerServices = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountCustomerServices"].Value.ToString()) ? "0" : cmd.Parameters["@CountCustomerServices"].Value));
                RetVal.CountPayOnline = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountPayOnline"].Value.ToString()) ? "0" : cmd.Parameters["@CountPayOnline"].Value));
                RetVal.CountPayOffline = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountPayOffline"].Value.ToString()) ? "0" : cmd.Parameters["@CountPayOffline"].Value));
                RetVal.CountStatusStillExpired = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountStatusStillExpired"].Value.ToString()) ? "0" : cmd.Parameters["@CountStatusStillExpired"].Value));
                RetVal.CountStatusExpiringSoon = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountStatusExpiringSoon"].Value.ToString()) ? "0" : cmd.Parameters["@CountStatusExpiringSoon"].Value));
                RetVal.CountServicePackageTC = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountServicePackageTC"].Value.ToString()) ? "0" : cmd.Parameters["@CountServicePackageTC"].Value));
                RetVal.CountServicePackageNC = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountServicePackageNC"].Value.ToString()) ? "0" : cmd.Parameters["@CountServicePackageNC"].Value));
                RetVal.CountServicePackageTA = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountServicePackageTA"].Value.ToString()) ? "0" : cmd.Parameters["@CountServicePackageTA"].Value));
                RetVal.CountServicePackageNV = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountServicePackageNV"].Value.ToString()) ? "0" : cmd.Parameters["@CountServicePackageNV"].Value));
                RetVal.TotalMoney = Convert.ToInt64((string.IsNullOrEmpty(cmd.Parameters["@TotalMoney"].Value.ToString()) ? "0" : cmd.Parameters["@TotalMoney"].Value));
                RetVal.TotalMoneyTC = Convert.ToInt64((string.IsNullOrEmpty(cmd.Parameters["@TotalMoneyTC"].Value.ToString()) ? "0" : cmd.Parameters["@TotalMoneyTC"].Value));
                RetVal.TotalMoneyNC = Convert.ToInt64((string.IsNullOrEmpty(cmd.Parameters["@TotalMoneyNC"].Value.ToString()) ? "0" : cmd.Parameters["@TotalMoneyNC"].Value));
                RetVal.TotalMoneyTA = Convert.ToInt64((string.IsNullOrEmpty(cmd.Parameters["@TotalMoneyTA"].Value.ToString()) ? "0" : cmd.Parameters["@TotalMoneyTA"].Value));
                RetVal.TotalMoneyNV = Convert.ToInt64((string.IsNullOrEmpty(cmd.Parameters["@TotalMoneyNV"].Value.ToString()) ? "0" : cmd.Parameters["@TotalMoneyNV"].Value));
                RetVal.CountPaymentTypeBank = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountPaymentTypeBank"].Value.ToString()) ? "0" : cmd.Parameters["@CountPaymentTypeBank"].Value));
                RetVal.CountPaymentTypeIncomOffice = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountPaymentTypeIncomOffice"].Value.ToString()) ? "0" : cmd.Parameters["@CountPaymentTypeIncomOffice"].Value));
                RetVal.CountPaymentTypeLuatVNCard = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountPaymentTypeLuatVNCard"].Value.ToString()) ? "0" : cmd.Parameters["@CountPaymentTypeLuatVNCard"].Value));
                RetVal.CountNumberPayFirst = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountNumberPayFirst"].Value.ToString()) ? "0" : cmd.Parameters["@CountNumberPayFirst"].Value));
                RetVal.CountNumberPaySecond = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountNumberPaySecond"].Value.ToString()) ? "0" : cmd.Parameters["@CountNumberPaySecond"].Value));
                RetVal.CountNumberPay3rd = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountNumberPay3rd"].Value.ToString()) ? "0" : cmd.Parameters["@CountNumberPay3rd"].Value));
                RetVal.CountNumberPay4rd = Convert.ToInt32((string.IsNullOrEmpty(cmd.Parameters["@CountNumberPay4rd"].Value.ToString()) ? "0" : cmd.Parameters["@CountNumberPay4rd"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
    }
}

