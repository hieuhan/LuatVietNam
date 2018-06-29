
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
    public class NewsletterEmails
    {
        private int _NewsletterEmailId;
        private short _SiteId;
        private int _CustomerId;
        private string _Email;
        private int _IsReceiveNews;
        private string _CustomerFullName;
        private string _CustomerName;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterEmails()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterEmails(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterEmails()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterEmailId
        {
            get { return _NewsletterEmailId; }
            set { _NewsletterEmailId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
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
        public int IsReceiveNews
        {
            get { return _IsReceiveNews; }
            set { _IsReceiveNews = value; }
        }
        //----------------------------------------------------------------
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
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<NewsletterEmails> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterEmails> l_NewsletterEmails = new List<NewsletterEmails>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterEmails m_NewsletterEmails = new NewsletterEmails(db.ConnectionString);
                    m_NewsletterEmails.NewsletterEmailId = smartReader.GetInt32("NewsletterEmailId");
                    m_NewsletterEmails.SiteId = smartReader.GetInt16("SiteId");
                    m_NewsletterEmails.CustomerId = smartReader.GetInt32("CustomerId");
                    m_NewsletterEmails.Email = smartReader.GetString("Email");
                    m_NewsletterEmails.IsReceiveNews = smartReader.GetInt32("IsReceiveNews");
                    m_NewsletterEmails.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    m_NewsletterEmails.CustomerName = smartReader.GetString("CustomerName");
                    m_NewsletterEmails.CustomerFullName = smartReader.GetString("CustomerFullName");

                    l_NewsletterEmails.Add(m_NewsletterEmails);
                }
                reader.Close();
                return l_NewsletterEmails;
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
                SqlCommand cmd = new SqlCommand("NewsletterEmails_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@IsReceiveNews", this.IsReceiveNews));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailId", this.NewsletterEmailId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterEmailId = Convert.ToInt32((cmd.Parameters["@NewsletterEmailId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterEmailId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterEmails_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailId", this.NewsletterEmailId));
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
        public List<NewsletterEmails> GetListByNewsletterEmailId(int NewsletterEmailId)
        {
            List<NewsletterEmails> RetVal = new List<NewsletterEmails>();
            try
            {
                if (NewsletterEmailId > 0)
                {
                    string sql = "SELECT A.NewsletterEmailId, A.CustomerId, A.Email, A.IsReceiveNews, A.CrDateTime, C.CustomerFullName,C.CustomerName FROM NewsletterEmails A LEFT JOIN Customers C ON A.CustomerId = C.CustomerId WHERE (NewsletterEmailId=" + NewsletterEmailId.ToString() + ")";
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
        public NewsletterEmails Get(int NewsletterEmailId)
        {
            NewsletterEmails RetVal = new NewsletterEmails(db.ConnectionString);
            try
            {
                List<NewsletterEmails> list = GetListByNewsletterEmailId(NewsletterEmailId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterEmails)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterEmails Get()
        {
            return Get(this.NewsletterEmailId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterEmails Static_Get(int NewsletterEmailId)
        {
            return new NewsletterEmails().Get(NewsletterEmailId);
        }
        //--------------------------------------------------------------     
        public List<NewsletterEmails> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short NewsletterGroupId, int CustomerId, string Email, int IsReceiveNews,  string DateFrom, string DateTo, ref int RowCount)
        {
            List<NewsletterEmails> RetVal = new List<NewsletterEmails>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterEmails_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", NewsletterGroupId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
                cmd.Parameters.Add(new SqlParameter("@IsReceiveNews", IsReceiveNews));
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
        public NewsletterEmails GetByEmail()
        {
            NewsletterEmails retVal = new NewsletterEmails();
            int actUserId = 0, rowCount = 0;
            string dateFrom = "";
            string dateTo = "";
            string orderBy = "";
            int pageSize = 1;
            int pageNumber = 0;
            short newsletterGroupId = 0;
            try
            {
                List<NewsletterEmails> list = GetPage(actUserId, pageSize, pageNumber, orderBy, newsletterGroupId, this.CustomerId, this.Email, this.IsReceiveNews, dateFrom, dateTo, ref rowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        //--------------------------------------------------------------            
        public void UpdateIsReceiveNews_ByEmail()
        {
            try
            {
                SqlCommand cmd =
                    new SqlCommand("NewsletterEmails_UpdateIsReceiveNews_ByEmail")
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@IsReceiveNews", this.IsReceiveNews));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        
    }
}