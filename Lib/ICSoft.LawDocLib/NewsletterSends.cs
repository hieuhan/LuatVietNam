
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
    public class NewsletterSends
    {
        private int _NewsletterSendId;
        private int _NewsletterMemberId;
        private string _Email;
        private int _NewsletterId;
        private byte _SendStatusId;
        private DateTime _SendTime;
        private DateTime _CrDateTime;
        private string _SendFrom;
        private string _Title;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterSends()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterSends(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterSends()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterSendId
        {
            get { return _NewsletterSendId; }
            set { _NewsletterSendId = value; }
        }
        //-----------------------------------------------------------------
        public int NewsletterMemberId
        {
            get { return _NewsletterMemberId; }
            set { _NewsletterMemberId = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public int NewsletterId
        {
            get { return _NewsletterId; }
            set { _NewsletterId = value; }
        }
        //-----------------------------------------------------------------
        public byte SendStatusId
        {
            get { return _SendStatusId; }
            set { _SendStatusId = value; }
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

        private List<NewsletterSends> Init(SqlCommand cmd, bool getTitle)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterSends> l_NewsletterSends = new List<NewsletterSends>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterSends m_NewsletterSends = new NewsletterSends(db.ConnectionString);
                    m_NewsletterSends.NewsletterSendId = smartReader.GetInt32("NewsletterSendId");
                    m_NewsletterSends.NewsletterMemberId = smartReader.GetInt32("NewsletterMemberId");
                    m_NewsletterSends.Email = smartReader.GetString("Email");
                    m_NewsletterSends.NewsletterId = smartReader.GetInt32("NewsletterId");
                    m_NewsletterSends.SendStatusId = smartReader.GetByte("SendStatusId");
                    m_NewsletterSends.SendTime = smartReader.GetDateTime("SendTime");
                    m_NewsletterSends.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    if (getTitle)
                    {
                        m_NewsletterSends.SendFrom = smartReader.GetString("SendFrom");
                        m_NewsletterSends.Title = smartReader.GetString("Title");
                    }
                    l_NewsletterSends.Add(m_NewsletterSends);
                }
                reader.Close();
                return l_NewsletterSends;
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
        public byte UpdateSendStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterSends_UpdateSendStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterSendId", this.NewsletterSendId));
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
        public List<NewsletterSends> GetListToGateway(int RowAmount)
        {
            List<NewsletterSends> RetVal = new List<NewsletterSends>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterSends_GetListToGateway");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = Init(cmd, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<NewsletterSends> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string Email,short SiteId, int NewsletterId, byte SendStatusId,
                                                            string DateFrom, string DateTo, ref int RowCount)
        {
            List<NewsletterSends> RetVal = new List<NewsletterSends>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterSends_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", NewsletterId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", SendStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd, true);
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
