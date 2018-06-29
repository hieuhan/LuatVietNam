using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using sms.common;
using sms.utils;
using sms.database;

namespace ICSoft.CMSLib
{
    public class InternalLinks
    {
        private int _InternalLinkId;
        private short _InternalLinkGroupId;
        private string _InternalLinkName;
        private string _InternalLinkDesc;
        private string _InternalLinkEnglishName;
        private string _InternalLinkUrl;
        private DateTime _CrDateTime;
        private int _LinkCounter;
        private byte _StatusId;
        private short _SiteId;
        private DBAccess db;
        public SysMessages m_SysMessages;
        //-----------------------------------------------------------
        public InternalLinks()
        {
            db = new DBAccess(CmsConstants.CONNECTION_STRING);
            m_SysMessages = new SysMessages(db.ConnectionString);
        }
        //-----------------------------------------------------------
        public InternalLinks(string connString)
        {
            db = new DBAccess(connString);
            m_SysMessages = new SysMessages(connString);
        }
        //-----------------------------------------------------------
        ~InternalLinks() { }
        //-----------------------------------------------------------
        public int InternalLinkId
        {
            get
            {
                return _InternalLinkId;
            }
            set
            {
                _InternalLinkId = value;
            }
        }
        //-----------------------------------------------------------
        public short InternalLinkGroupId
        {
            get
            {
                return _InternalLinkGroupId;
            }
            set
            {
                _InternalLinkGroupId = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkName
        {
            get
            {
                return _InternalLinkName;
            }
            set
            {
                _InternalLinkName = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkDesc
        {
            get
            {
                return _InternalLinkDesc;
            }
            set
            {
                _InternalLinkDesc = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkEnglishName
        {
            get
            {
                return _InternalLinkEnglishName;
            }
            set
            {
                _InternalLinkEnglishName = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkUrl
        {
            get
            {
                return _InternalLinkUrl;
            }
            set
            {
                _InternalLinkUrl = value;
            }
        }
        //-----------------------------------------------------------
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }
        //-----------------------------------------------------------
        public int LinkCounter
        {
            get
            {
                return _LinkCounter;
            }
            set
            {
                _LinkCounter = value;
            }
        }
        //-----------------------------------------------------------
        public byte StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                _StatusId = value;
            }
        }
        //-----------------------------------------------------------
        public short SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                _SiteId = value;
            }
        }
        //-----------------------------------------------------------
        //-----------------------------------------------------------
        private IList Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            IList l_InternalLinks = new ArrayList();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InternalLinks m_InternalLinks = new InternalLinks(db.ConnectionString);
                    m_InternalLinks.InternalLinkId = smartReader.GetInt32("InternalLinkId");
                    m_InternalLinks.InternalLinkGroupId = smartReader.GetInt16("InternalLinkGroupId");
                    m_InternalLinks.InternalLinkName = smartReader.GetString("InternalLinkName");
                    m_InternalLinks.InternalLinkDesc = smartReader.GetString("InternalLinkDesc");
                    m_InternalLinks.InternalLinkEnglishName = smartReader.GetString("InternalLinkEnglishName");
                    m_InternalLinks.InternalLinkUrl = smartReader.GetString("InternalLinkUrl");
                    m_InternalLinks.LinkCounter = smartReader.GetInt32("LinkCounter");
                    m_InternalLinks.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_InternalLinks.StatusId = smartReader.GetByte("StatusId");
                    m_InternalLinks.SiteId = smartReader.GetInt16("SiteId");
                    l_InternalLinks.Add(m_InternalLinks);
                }
                reader.Close();
            }
            catch (SqlException err)
            {
                Log.writeLog(err.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            finally
            {
                db.closeConnection(con);
            }
            return l_InternalLinks;
        }
        //-----------------------------------------------------------
        public short Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinks_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupId", this.InternalLinkGroupId));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkName", this.InternalLinkName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkDesc", this.InternalLinkDesc));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkEnglishName", this.InternalLinkEnglishName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkUrl", this.InternalLinkUrl));
                cmd.Parameters.Add(new SqlParameter("@LinkCounter", this.LinkCounter));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add("@InternalLinkId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.InternalLinkId = Int16.Parse(cmd.Parameters["@InternalLinkId"].Value.ToString());
                m_SysMessages.SysMessageId = cmd.Parameters["@SysMessageId"].Value == DBNull.Value ? (short)0 : short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                m_SysMessages.SysMessageTypeId = 1;// cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value ? (short)0 : short.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinks_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkId", this.InternalLinkId));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupId", this.InternalLinkGroupId));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkName", this.InternalLinkName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkDesc", this.InternalLinkDesc));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkEnglishName", this.InternalLinkEnglishName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkUrl", this.InternalLinkUrl));
                cmd.Parameters.Add(new SqlParameter("@LinkCounter", this.LinkCounter));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = 1;// Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short Delete()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinks_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkId", this.InternalLinkId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = 1;// Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short UpdateCounter(string idList, byte incrementFlag)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinks_UpdateCounter");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdList", idList));
                cmd.Parameters.Add(new SqlParameter("@IncrementFlag", incrementFlag));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public IList GetList()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinks";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListToInsert(string orderBy, byte duplicateLinkFlag)
        {
            SqlCommand cmd;
            IList list = new ArrayList();
            try
            {
                cmd = new SqlCommand("InternalLinks_GetListToInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrderBy", orderBy));
                cmd.Parameters.Add(new SqlParameter("@DuplicateLinkFlag", duplicateLinkFlag));
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListToInsert(short siteId, string orderBy, byte duplicateLinkFlag)
        {
            SqlCommand cmd;
            IList list = new ArrayList();
            try
            {
                cmd = new SqlCommand("InternalLinks_GetListToInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", siteId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", orderBy));
                cmd.Parameters.Add(new SqlParameter("@DuplicateLinkFlag", duplicateLinkFlag));
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListNA()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinks";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListAll()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinks";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListAllNA()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinks";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public InternalLinks Get()
        {
            InternalLinks m_InternalLinks = new InternalLinks();
            try
            {
                string sql = "SELECT * FROM InternalLinks WHERE (InternalLinkId = " + this.InternalLinkId + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                IList list = Init(cmd);
                if (list.Count == 1)
                {
                    m_InternalLinks = (InternalLinks)list[0];
                }
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_InternalLinks;
        }
        //-----------------------------------------------------------
        public IList Search(short SiteId, short InternalLinkGroupId, string Keywords, string FromDate, string ToDate, string Orderby, string OrderType, ref int RowCount, int PageSize, int PageNumber)
        {
            SqlCommand cmd;
            IList list = new ArrayList();
            try
            {
                cmd = new SqlCommand("InternalLinks_PagingSearch");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupId", InternalLinkGroupId));
                cmd.Parameters.Add(new SqlParameter("@Keywords", Keywords));
                if (!string.IsNullOrEmpty(FromDate))
                {
                    cmd.Parameters.Add(new SqlParameter("@FromDate", DateTime.Parse(FromDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    cmd.Parameters.Add(new SqlParameter("@ToDate", DateTime.Parse(ToDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                }
                cmd.Parameters.Add(new SqlParameter("@Orderby", Orderby));
                cmd.Parameters.Add(new SqlParameter("@OrderType", OrderType));
                cmd.Parameters.Add(new SqlParameter("@RowsAmount", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageNumber));
                cmd.Parameters.Add("@RowsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                list = Init(cmd);
                RowCount = Int32.Parse(cmd.Parameters["@RowsCount"].Value.ToString());
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
                throw;
            }
            return list;
        }
    }
}
