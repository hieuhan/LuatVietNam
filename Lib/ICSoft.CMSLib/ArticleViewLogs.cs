
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
    public class ArticleViewLogs
    {
        private int _ArticleViewLogId;
        private int _ArticleId;
        private short _SiteId;
        private byte _DataTypeId;
        private short _CategoryId;
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private string _RefererFrom;
        private string _UserAgent;
        private int _CustomerId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private string _Title;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleViewLogs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleViewLogs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleViewLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleViewLogId
        {
            get { return _ArticleViewLogId; }
            set { _ArticleViewLogId = value; }
        }
        //-----------------------------------------------------------------
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RefererFrom
        {
            get { return _RefererFrom; }
            set { _RefererFrom = value; }
        }
        //-----------------------------------------------------------------
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------

        private List<ArticleViewLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleViewLogs> l_ArticleViewLogs = new List<ArticleViewLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleViewLogs m_ArticleViewLogs = new ArticleViewLogs(db.ConnectionString);
                    m_ArticleViewLogs.ArticleViewLogId = smartReader.GetInt32("ArticleViewLogId");
                    m_ArticleViewLogs.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleViewLogs.SiteId = smartReader.GetInt16("SiteId");
                    m_ArticleViewLogs.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_ArticleViewLogs.CategoryId = smartReader.GetInt16("CategoryId");
                    m_ArticleViewLogs.LanguageId = smartReader.GetByte("LanguageId");
                    m_ArticleViewLogs.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ArticleViewLogs.RefererFrom = smartReader.GetString("RefererFrom");
                    m_ArticleViewLogs.UserAgent = smartReader.GetString("UserAgent");
                    m_ArticleViewLogs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_ArticleViewLogs.FromIP = smartReader.GetString("FromIP");
                    m_ArticleViewLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ArticleViewLogs.Title = smartReader.GetString("Title");

                    l_ArticleViewLogs.Add(m_ArticleViewLogs);
                }
                reader.Close();
                return l_ArticleViewLogs;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId, ref int ArticleViewLogByDayId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", this.RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@ArticleViewLogByDayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ArticleViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleViewLogId = Convert.ToInt32((cmd.Parameters["@ArticleViewLogId"].Value == DBNull.Value) ? 0 : (cmd.Parameters["@ArticleViewLogId"].Value));
                ArticleViewLogByDayId = Convert.ToInt32((cmd.Parameters["@ArticleViewLogByDayId"].Value == DBNull.Value) ? 0 : (cmd.Parameters["@ArticleViewLogByDayId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public List<ArticleViewLogs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int ArticleId, short CategoryId, byte LanguageId, byte ApplicationTypeId,
                                             string RefererFrom, string UserAgent, int CustomerId, string FromIP, string DateFrom, string DateTo, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleViewLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", StringUtil.InjectionString(RefererFrom)));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", StringUtil.InjectionString(UserAgent)));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", StringUtil.InjectionString(FromIP)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<ArticleViewLogs> list = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------           
    }
}

