
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
    public class NewsletterMembers
    {
        private int _NewsletterMemberId;
        private int _CustomerId;
        private string _Email;
        private short _NewsletterGroupId;
        private byte _NewsletterMemberStatusId;
        private int _CrUserId;
        private string _CustomerFullName;
        private string _CustomerName;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterMembers()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterMembers(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterMembers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterMemberId
        {
            get { return _NewsletterMemberId; }
            set { _NewsletterMemberId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public short NewsletterGroupId
        {
            get { return _NewsletterGroupId; }
            set { _NewsletterGroupId = value; }
        }
        //-----------------------------------------------------------------
        public byte NewsletterMemberStatusId
        {
            get { return _NewsletterMemberStatusId; }
            set { _NewsletterMemberStatusId = value; }
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

        private List<NewsletterMembers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterMembers> l_NewsletterMembers = new List<NewsletterMembers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterMembers m_NewsletterMembers = new NewsletterMembers(db.ConnectionString);
                    m_NewsletterMembers.NewsletterMemberId = smartReader.GetInt32("NewsletterMemberId");
                    m_NewsletterMembers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_NewsletterMembers.Email = smartReader.GetString("Email");
                    m_NewsletterMembers.NewsletterGroupId = smartReader.GetInt16("NewsletterGroupId");
                    m_NewsletterMembers.NewsletterMemberStatusId = smartReader.GetByte("NewsletterMemberStatusId");
                    m_NewsletterMembers.CrUserId = smartReader.GetInt32("CrUserId");
                    m_NewsletterMembers.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    m_NewsletterMembers.CustomerName = smartReader.GetString("CustomerName");
                    m_NewsletterMembers.CustomerFullName = smartReader.GetString("CustomerFullName");

                    l_NewsletterMembers.Add(m_NewsletterMembers);
                }
                reader.Close();
                return l_NewsletterMembers;
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
                SqlCommand cmd = new SqlCommand("NewsletterMembers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", this.NewsletterGroupId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusId", this.NewsletterMemberStatusId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberId", this.NewsletterMemberId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterMemberId = Convert.ToInt32((cmd.Parameters["@NewsletterMemberId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterMemberId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterMembers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberId", this.NewsletterMemberId));
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
        public List<NewsletterMembers> GetList()
        {
            List<NewsletterMembers> RetVal = new List<NewsletterMembers>();
            try
            {
                string sql = "SELECT * FROM NewsletterMembers";
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
        public static List<NewsletterMembers> Static_GetList(string ConStr)
        {
            List<NewsletterMembers> RetVal = new List<NewsletterMembers>();
            try
            {
                NewsletterMembers m_NewsletterMembers = new NewsletterMembers(ConStr);
                RetVal = m_NewsletterMembers.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<NewsletterMembers> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<NewsletterMembers> GetListByNewsletterMemberId(int NewsletterMemberId)
        {
            List<NewsletterMembers> RetVal = new List<NewsletterMembers>();
            try
            {
                if (NewsletterMemberId > 0)
                {
                    string sql = "SELECT A.NewsletterMemberId, A.CustomerId, A.Email, A.NewsletterGroupId, A.NewsletterMemberStatusId, A.CrUserId, A.CrDateTime, B.CustomerFullName,B.CustomerName FROM NewsletterMembers A LEFT JOIN Customers B ON A.CustomerId = B.CustomerId WHERE (NewsletterMemberId=" + NewsletterMemberId.ToString() + ")";
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
        public NewsletterMembers Get(int NewsletterMemberId)
        {
            NewsletterMembers RetVal = new NewsletterMembers(db.ConnectionString);
            try
            {
                List<NewsletterMembers> list = GetListByNewsletterMemberId(NewsletterMemberId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterMembers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterMembers Get()
        {
            return Get(this.NewsletterMemberId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterMembers Static_Get(int NewsletterMemberId)
        {
            return new NewsletterMembers().Get(NewsletterMemberId);
        }
        //--------------------------------------------------------------     
        public List<NewsletterMembers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId, string Email, short NewsletterGroupId, 
                                                string DateFrom, string DateTo, ref int RowCount)
        {
            List<NewsletterMembers> RetVal = new List<NewsletterMembers>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterMembers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", NewsletterGroupId));
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
        public List<NewsletterMembers> GetPageWithStatus(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int CustomerId,
            string Email, short NewsletterGroupId,int NewsletterMemberStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<NewsletterMembers> RetVal = new List<NewsletterMembers>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterMembers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", NewsletterGroupId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusId", NewsletterMemberStatusId));

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
        public List<NewsletterMembers> Search(int ActUserId, string OrderBy, int CustomerId, string Email, short NewsletterGroupId,string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, CustomerId, Email, NewsletterGroupId, DateFrom,  DateTo, ref RowCount);
        }
    }
}

