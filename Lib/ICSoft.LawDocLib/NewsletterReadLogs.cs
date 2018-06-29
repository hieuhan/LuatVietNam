
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
    public class NewsletterReadLogs
    {
        private int _NewsletterReadLogId;
        private int _NewsletterSendId;
        private string _Email;
        private DateTime _ReadTime;
        private DateTime _SendTime;
        private DateTime _CrDateTime;
        private string _SendFrom;
        private string _Title;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterReadLogs()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterReadLogs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterReadLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterReadLogId
        {
            get { return _NewsletterReadLogId; }
            set { _NewsletterReadLogId = value; }
        }
        //-----------------------------------------------------------------
        public int NewsletterSendId
        {
            get { return _NewsletterSendId; }
            set { _NewsletterSendId = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ReadTime
        {
            get { return _ReadTime; }
            set { _ReadTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string SendFrom
        {
            get { return _SendFrom; }
            set { _SendFrom = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------

        private List<NewsletterReadLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterReadLogs> l_NewsletterReadLogs = new List<NewsletterReadLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterReadLogs m_NewsletterReadLogs = new NewsletterReadLogs(db.ConnectionString);
                    m_NewsletterReadLogs.NewsletterReadLogId = smartReader.GetInt32("NewsletterReadLogId");
                    m_NewsletterReadLogs.NewsletterSendId = smartReader.GetInt32("NewsletterSendId");
                    m_NewsletterReadLogs.Email = smartReader.GetString("Email");
                    m_NewsletterReadLogs.ReadTime = smartReader.GetDateTime("ReadTime");
                    m_NewsletterReadLogs.SendTime = smartReader.GetDateTime("SendTime");
                    m_NewsletterReadLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_NewsletterReadLogs.SendFrom = smartReader.GetString("SendFrom");
                    m_NewsletterReadLogs.Title = smartReader.GetString("Title");
                    l_NewsletterReadLogs.Add(m_NewsletterReadLogs);
                }
                reader.Close();
                return l_NewsletterReadLogs;
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
                SqlCommand cmd = new SqlCommand("NewsletterReadLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterSendId", this.NewsletterSendId));
                cmd.Parameters.Add("@NewsletterReadLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterReadLogId = Convert.ToInt32((cmd.Parameters["@NewsletterReadLogId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterReadLogId"].Value));
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
        public List<NewsletterReadLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, int NewsletterId, string Email, string DateFrom, string DateTo, 
                                                ref int RowCount)
        {
            List<NewsletterReadLogs> RetVal = new List<NewsletterReadLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterReadLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", NewsletterId));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
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
        public List<NewsletterReadLogs> Search(int ActUserId, string OrderBy, short SiteId, int NewsletterId, string Email, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SiteId, NewsletterId, Email, DateFrom, DateTo, ref RowCount);
        }
    }
}

