
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
    public class MemberResetPasswords
    {
        private int _MemberResetPasswordId;
        private int _MemberId;
        private string _PasswordReset;
        private DateTime _RequestTime;
        private DateTime _ConfirmTime;
        
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberResetPasswords()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberResetPasswords(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberResetPasswords()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberResetPasswordId
        {
            get { return _MemberResetPasswordId; }
            set { _MemberResetPasswordId = value; }
        }
        //-----------------------------------------------------------------    
        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //-----------------------------------------------------------------
        public string PasswordReset
        {
            get { return _PasswordReset; }
            set { _PasswordReset = value; }
        }
        
        //-----------------------------------------------------------------
        public DateTime RequestTime
        {
            get { return _RequestTime; }
            set { _RequestTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ConfirmTime
        {
            get { return _ConfirmTime; }
            set { _ConfirmTime = value; }
        }
        private List<MemberResetPasswords> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberResetPasswords> l_MemberResetPasswords = new List<MemberResetPasswords>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberResetPasswords m_MemberResetPasswords = new MemberResetPasswords(db.ConnectionString);
                    m_MemberResetPasswords.MemberResetPasswordId = smartReader.GetInt32("MemberResetPasswordId");
                    m_MemberResetPasswords.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberResetPasswords.PasswordReset = smartReader.GetString("PasswordReset");
                    m_MemberResetPasswords.RequestTime = smartReader.GetDateTime("RequestTime");
                    m_MemberResetPasswords.ConfirmTime = smartReader.GetDateTime("ConfirmTime");

                    l_MemberResetPasswords.Add(m_MemberResetPasswords);
                }
                reader.Close();
                return l_MemberResetPasswords;
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
                SqlCommand cmd = new SqlCommand("MemberResetPasswords_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@PasswordReset", this.PasswordReset));
                cmd.Parameters.Add(new SqlParameter("@MemberResetPasswordId", this.MemberResetPasswordId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberResetPasswordId = Convert.ToInt32((cmd.Parameters["@MemberResetPasswordId"].Value == null) ? 0 : (cmd.Parameters["@MemberResetPasswordId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberResetPasswords_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("MemberResetPasswords_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
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
        public MemberResetPasswords GetByMemberId(int MemberId)
        {
            MemberResetPasswords RetVal = new MemberResetPasswords();
            int ActUserId = 0;
            int RowAmount = 1;
            int PageIndex = 0;
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            try
            {
                List<MemberResetPasswords> l_MemberResetPasswords;
                l_MemberResetPasswords = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, DateFrom, DateTo, ref RowCount);
                if (l_MemberResetPasswords.Count > 0)
                    RetVal = l_MemberResetPasswords[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<MemberResetPasswords> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MemberId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MemberResetPasswords> RetVal = new List<MemberResetPasswords>();
            try
            {
                SqlCommand cmd = new SqlCommand("MemberResetPasswords_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
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
       
    }
}