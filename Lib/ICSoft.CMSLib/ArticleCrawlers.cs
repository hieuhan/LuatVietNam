
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
    public class ArticleCrawlers
    {
        private int _ArticleId;
        private short _DataSourceId;
        private byte _DataTypeId;
        private string _Category;
        private string _ArticleTitle;
        private string _ArticleSummary;
        private string _ImagePath;
        private string _ArticleContent;
        private DateTime _PublishDateTime;
        private string _Tags;
        private string _CrawlUrl;
        private int _HashValue;
        private DateTime _CrDateTime;
        private byte _isSync;
        private int _NewArticleId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleCrawlers()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleCrawlers(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleCrawlers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleTitle
        {
            get { return _ArticleTitle; }
            set { _ArticleTitle = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleSummary
        {
            get { return _ArticleSummary; }
            set { _ArticleSummary = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleContent
        {
            get { return _ArticleContent; }
            set { _ArticleContent = value; }
        }
        //-----------------------------------------------------------------
        public DateTime PublishDateTime
        {
            get { return _PublishDateTime; }
            set { _PublishDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string Tags
        {
            get { return _Tags; }
            set { _Tags = value; }
        }
        //-----------------------------------------------------------------
        public string CrawlUrl
        {
            get { return _CrawlUrl; }
            set { _CrawlUrl = value; }
        }
        //-----------------------------------------------------------------
        public int HashValue
        {
            get { return _HashValue; }
            set { _HashValue = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public byte isSync
        {
            get { return _isSync; }
            set { _isSync = value; }
        }
        //-----------------------------------------------------------------
        public int NewArticleId
        {
            get { return _NewArticleId; }
            set { _NewArticleId = value; }
        }
        //-----------------------------------------------------------------

        private List<ArticleCrawlers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleCrawlers> l_ArticleCrawlers = new List<ArticleCrawlers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleCrawlers m_ArticleCrawlers = new ArticleCrawlers(db.ConnectionString);
                    m_ArticleCrawlers.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleCrawlers.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_ArticleCrawlers.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_ArticleCrawlers.Category = smartReader.GetString("Category");
                    m_ArticleCrawlers.ArticleTitle = smartReader.GetString("ArticleTitle");
                    m_ArticleCrawlers.ArticleSummary = smartReader.GetString("ArticleSummary");
                    m_ArticleCrawlers.ImagePath = smartReader.GetString("ImagePath");
                    m_ArticleCrawlers.ArticleContent = smartReader.GetString("ArticleContent");
                    m_ArticleCrawlers.PublishDateTime = smartReader.GetDateTime("PublishDateTime");
                    m_ArticleCrawlers.Tags = smartReader.GetString("Tags");
                    m_ArticleCrawlers.CrawlUrl = smartReader.GetString("CrawlUrl");
                    m_ArticleCrawlers.HashValue = smartReader.GetInt32("HashValue");
                    m_ArticleCrawlers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ArticleCrawlers.isSync = smartReader.GetByte("isSync");
                    m_ArticleCrawlers.NewArticleId = smartReader.GetInt32("NewArticleId");

                    l_ArticleCrawlers.Add(m_ArticleCrawlers);
                }
                reader.Close();
                return l_ArticleCrawlers;
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
                SqlCommand cmd = new SqlCommand("ArticleCrawlers_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@Category", this.Category));
                cmd.Parameters.Add(new SqlParameter("@ArticleTitle", this.ArticleTitle));
                cmd.Parameters.Add(new SqlParameter("@ArticleSummary", this.ArticleSummary));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ArticleContent", this.ArticleContent));
                cmd.Parameters.Add(new SqlParameter("@PublishDateTime", this.PublishDateTime));
                cmd.Parameters.Add(new SqlParameter("@Tags", this.Tags));
                cmd.Parameters.Add(new SqlParameter("@CrawlUrl", this.CrawlUrl));
                cmd.Parameters.Add(new SqlParameter("@HashValue", this.HashValue));
                cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleId = (cmd.Parameters["@ArticleId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ArticleId"].Value);
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
                SqlCommand cmd = new SqlCommand("ArticleCrawlers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
        public byte Sync(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleCrawlers_Sync");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<ArticleCrawlers> GetListByArticleId(int ArticleId)
        {
            List<ArticleCrawlers> RetVal = new List<ArticleCrawlers>();
            try
            {
                if (ArticleId > 0)
                {
                    string sql = "SELECT * FROM ArticleCrawlers WHERE (ArticleId=" + ArticleId.ToString() + ")";
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
        public void InsertLog(string Url, int HashCode, string ProcessStatus)
        {
            try
            {
                string sql = "INSERT INTO ArticleCrawlerLogs (HashCode,Url,ProcessStatus) VALUES (" + HashCode.ToString() + ",'" + Url + "','" + ProcessStatus + "')";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public bool Exists(int HashValue)
        {
            bool RetVal = false;
            try
            {
                //string sql = "SELECT COUNT(1) FROM ArticleCrawlers WHERE (HashValue=" + HashValue.ToString() + ")";
                string sql = "SELECT COUNT(1) FROM ArticleCrawlerLogs WHERE (HashCode=" + HashValue.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = db.ExecuteScalar(cmd) == 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleCrawlers Get(int ArticleId)
        {
            ArticleCrawlers RetVal = new ArticleCrawlers();
            try
            {
                List<ArticleCrawlers> list = GetListByArticleId(ArticleId);
                if (list.Count > 0)
                {
                    RetVal = (ArticleCrawlers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleCrawlers Get()
        {
            return Get(this.ArticleId);
        }
        //--------------------------------------------------------------     
        public List<ArticleCrawlers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticleCrawlers> RetVal = new List<ArticleCrawlers>();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleCrawlers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@Category", this.Category));
                cmd.Parameters.Add(new SqlParameter("@ArticleTitle", this.ArticleTitle));
                cmd.Parameters.Add(new SqlParameter("@CrawlUrl", this.CrawlUrl));
                cmd.Parameters.Add(new SqlParameter("@HashValue", this.HashValue));
                cmd.Parameters.Add(new SqlParameter("@isSync", this.isSync));
                cmd.Parameters.Add(new SqlParameter("@NewArticleId", this.NewArticleId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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

