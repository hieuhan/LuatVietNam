using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class CustomerCare
    {
        private int _CustomerCareId;
        private string _Info;
        private int _CustomerId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerCare()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerCare(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerCare()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerCareId
        {
            get { return _CustomerCareId; }
            set { _CustomerCareId = value; }
        }
        //-----------------------------------------------------------------
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerCare> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerCare> l_CustomerCare = new List<CustomerCare>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerCare m_CustomerCare = new CustomerCare(db.ConnectionString);
                    m_CustomerCare.CustomerCareId = smartReader.GetInt32("CustomerCareId");
                    m_CustomerCare.Info = smartReader.GetString("Info");
                    m_CustomerCare.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerCare.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_CustomerCare.CrUserId = smartReader.GetInt32("CrUserId");
                    l_CustomerCare.Add(m_CustomerCare);
                }
                reader.Close();
                return l_CustomerCare;
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
        public int Insert(byte Replicated, int ActUserId, ref int CustomerCareId)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerCare_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Info", this.Info));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add("@CustomerCareId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerCareId = Convert.ToInt32((cmd.Parameters["@CustomerCareId"].Value == null) ? 0 : (cmd.Parameters["@CustomerCareId"].Value));
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToInt32((cmd.Parameters["@CustomerCareId"].Value == null) ? 0 : (cmd.Parameters["@CustomerCareId"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        public List<CustomerCare> GetListByCustomerId(int CustomerId = 0)
        {
            List<CustomerCare> RetVal = new List<CustomerCare>();
            try
            {
                string sql = "SELECT * FROM CustomerCare where CustomerId = "+ CustomerId +" Order by CrDateTime desc";
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
        public static List<CustomerCare> Static_GetListByCustomerId(string ConStr, int CustomerId = 0)
        {
            List<CustomerCare> RetVal = new List<CustomerCare>();
            try
            {
                CustomerCare m_CustomerCare = new CustomerCare(ConStr);
                RetVal = m_CustomerCare.GetListByCustomerId(CustomerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CustomerCare> Static_GetList(int CustomerId = 0)
        {
            return Static_GetListByCustomerId("", CustomerId);
        }
       
    }
}