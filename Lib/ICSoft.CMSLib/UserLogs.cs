
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class UserLogs
    {
        private int _UserLogId;
        private string _UserName;
        private string _UserPass;
        private string _IPAddress;
        private byte _StatusId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public UserLogs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public UserLogs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~UserLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int UserLogId
        {
            get { return _UserLogId; }
            set { _UserLogId = value; }
        }
        //-----------------------------------------------------------------
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        //-----------------------------------------------------------------
        public string UserPass
        {
            get { return _UserPass; }
            set { _UserPass = value; }
        }
        //-----------------------------------------------------------------
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
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

        private List<UserLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<UserLogs> l_UserLogs = new List<UserLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    UserLogs m_UserLogs = new UserLogs(db.ConnectionString);
                    m_UserLogs.UserLogId = smartReader.GetInt32("UserLogId");
                    m_UserLogs.UserName = smartReader.GetString("UserName");
                    m_UserLogs.UserPass = smartReader.GetString("UserPass");
                    m_UserLogs.IPAddress = smartReader.GetString("IPAddress");
                    m_UserLogs.StatusId = smartReader.GetByte("StatusId");
                    m_UserLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_UserLogs.Add(m_UserLogs);
                }
                reader.Close();
                return l_UserLogs;
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
        public List<UserLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string UserName, string IPAddress, byte StatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<UserLogs> RetVal = new List<UserLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("UserLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@UserName", StringUtil.InjectionString(UserName)));
                cmd.Parameters.Add(new SqlParameter("@IPAddress", StringUtil.InjectionString(IPAddress)));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));
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
    } 
}

