
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
    public class Members
    {
        private int _MemberId;
        private string _UserName;
        private string _Password;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _ImagePath;
        private string _Mobile;
        private string _Tel;
        private string _Email;
        private string _Fax;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _MemberStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Members()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Members(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Members()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //-----------------------------------------------------------------
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        //-----------------------------------------------------------------
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        //-----------------------------------------------------------------
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        //-----------------------------------------------------------------
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        //-----------------------------------------------------------------
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        //-----------------------------------------------------------------
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
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

        public byte MemberStatusId
        {
            get { return _MemberStatusId; }
            set { _MemberStatusId = value; }
        }
        private List<Members> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Members> l_Members = new List<Members>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Members m_Members = new Members(db.ConnectionString);
                    m_Members.MemberId = smartReader.GetInt32("MemberId");
                    m_Members.UserName = smartReader.GetString("UserName");
                    m_Members.Password = smartReader.GetString("Password");
                    m_Members.FirstName = smartReader.GetString("FirstName");
                    m_Members.MiddleName = smartReader.GetString("MiddleName");
                    m_Members.LastName = smartReader.GetString("LastName");
                    m_Members.ImagePath = smartReader.GetString("ImagePath");
                    m_Members.Mobile = smartReader.GetString("Mobile");
                    m_Members.Tel = smartReader.GetString("Tel");
                    m_Members.Email = smartReader.GetString("Email");
                    m_Members.Fax = smartReader.GetString("Fax");
                    m_Members.MemberStatusId = smartReader.GetByte("MemberStatusId");
                    m_Members.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Members.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Members.Add(m_Members);
                }
                reader.Close();
                return l_Members;
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
                SqlCommand cmd = new SqlCommand("Members_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
                cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
                cmd.Parameters.Add(new SqlParameter("@FirstName", this.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", this.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LastName", this.LastName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Tel", this.Tel));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Fax", this.Fax));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusId", this.MemberStatusId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberId = Convert.ToInt32((cmd.Parameters["@MemberId"].Value == null) ? 0 : (cmd.Parameters["@MemberId"].Value));
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
        public byte UpdateMemberStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Members_UpdateMemberStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusId", this.MemberStatusId));
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
                SqlCommand cmd = new SqlCommand("Members_Delete");
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
        public List<Members> GetList()
        {
            List<Members> RetVal = new List<Members>();
            try
            {
                string sql = "SELECT * FROM V$Members";
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
        public static List<Members> Static_GetList(string ConStr)
        {
            List<Members> RetVal = new List<Members>();
            try
            {
                Members m_Members = new Members(ConStr);
                RetVal = m_Members.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Members> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<Members> GetListOrderBy(string OrderBy)
        {
            List<Members> RetVal = new List<Members>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Members ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<Members> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Members m_Members = new Members(ConStr);
            return m_Members.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Members> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<Members> GetListByMemberId(int MemberId)
        {
            List<Members> RetVal = new List<Members>();
            try
            {
                if (MemberId > 0)
                {
                    string sql = "SELECT * FROM V$Members WHERE (MemberId=" + MemberId.ToString() + ")";
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
        public Members Get(int MemberId)
        {
            Members RetVal = new Members(db.ConnectionString);
            try
            {
                List<Members> list = GetListByMemberId(MemberId);
                if (list.Count > 0)
                {
                    RetVal = (Members)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Members Get()
        {
            return Get(this.MemberId);
        }
        //-------------------------------------------------------------- 
        public static Members Static_Get(int MemberId)
        {
            return Static_Get(MemberId);
        }
        //-------------------------------------------------------------- 
        public Members GetByUserNameOrEmail()
        {
            Members RetVal = new Members(db.ConnectionString);
            try
            {                
                SqlCommand cmd = new SqlCommand("Members_GetByUserNameOrEmail");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
                List<Members> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Members)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Members> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte MemberStatusId, string UserName, string LastName, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Members> RetVal = new List<Members>();
            try
            {
                SqlCommand cmd = new SqlCommand("Members_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusId", MemberStatusId));
                cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                cmd.Parameters.Add(new SqlParameter("@LastName", LastName));

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
        public List<Members> GetPage(int RowAmount, int PageIndex, byte MemberStatusId, string LastName, ref int RowCount)
        {
            
            int ActUserId = 0;
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            string UserName = "";
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberStatusId, UserName, LastName, DateFrom, DateTo, ref RowCount);
        }
       
        //--------------------------------------------------------------     
        public List<Members> Search(int ActUserId, string OrderBy, byte MemberStatusId, string LastName, string DateFrom, string DateTo, ref int RowCount)
        {
            string UserName = "";
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberStatusId, UserName, LastName, DateFrom, DateTo, ref RowCount);
        }
    }
}