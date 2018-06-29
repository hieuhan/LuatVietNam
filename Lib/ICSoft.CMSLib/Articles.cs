
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSViewLib;



namespace ICSoft.CMSLib
{
    public class Articles
    {
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private int _ArticleId;
        private string _Title;
        private string _Summary;
        private string _ArticleContent;
        private string _ArticleCode;
        private string _ImagePath;
        private string _SourceUrl;
        private string _ArticleUrl;
        private short _DataSourceId;
        private short _CategoryId;
        private short _SiteId;
        private byte _DataTypeId;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private double _OriginalPrice;
        private double _SalePrice;
        private string _ContactPrice;
        private byte _CurrencyId;
        private byte _InventoryStatusId;
        private byte _IsVerify;
        private byte _ShowTop;
        private byte _ShowBottom;
        private byte _ShowWeb;
        private byte _ShowWap;
        private byte _ShowApp;
        private string _JsonData;
        private DateTime _PublishTime;
        private DateTime _DisplayStartTime = DateTime.MinValue;
        private DateTime _DisplayEndTime = DateTime.MinValue;
        private byte _IconStatusId;
        private byte _ArticleTypeId;
        private byte _ReviewStatusId;
        private int _DisplayOrder;
        private int _CrUserId;
        private int _ViewCount;
        private int _SumByStatus1;
        private int _SumByStatus2;
        private int _SumByStatus3;
        private int _SumBySource1;
        private int _SumBySource2;
        private int _SumByDownload1;
        private int _SumByDownload2;
        private int _SumByView1;
        private int _SumByView2;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Articles()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Articles(string constr)
        {
           db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);           
        }
        //-----------------------------------------------------------------
        ~Articles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

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
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleContent
        {
            get { return _ArticleContent; }
            set { _ArticleContent = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleCode
        {
            get { return _ArticleCode; }
            set { _ArticleCode = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string SourceUrl
        {
            get { return _SourceUrl; }
            set { _SourceUrl = value; }
        }
        //-----------------------------------------------------------------
        public string ArticleUrl
        {
            get { return _ArticleUrl; }
            set { _ArticleUrl = value; }
        } 
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
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
        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }
        //-----------------------------------------------------------------
        public string MetaDesc
        {
            get { return _MetaDesc; }
            set { _MetaDesc = value; }
        }
        //-----------------------------------------------------------------
        public string MetaKeyword
        {
            get { return _MetaKeyword; }
            set { _MetaKeyword = value; }
        }
        //-----------------------------------------------------------------
        public double OriginalPrice
        {
            get { return _OriginalPrice; }
            set { _OriginalPrice = value; }
        }
        //-----------------------------------------------------------------
        public double SalePrice
        {
            get { return _SalePrice; }
            set { _SalePrice = value; }
        }
        //-----------------------------------------------------------------
        public string ContactPrice
        {
            get { return _ContactPrice; }
            set { _ContactPrice = value; }
        }
        //-----------------------------------------------------------------
        public byte CurrencyId
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }
        //-----------------------------------------------------------------
        public byte InventoryStatusId
        {
            get { return _InventoryStatusId; }
            set { _InventoryStatusId = value; }
        }
        //-----------------------------------------------------------------
        public byte IsVerify
        {
            get { return _IsVerify; }
            set { _IsVerify = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowTop
        {
            get { return _ShowTop; }
            set { _ShowTop = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowBottom
        {
            get { return _ShowBottom; }
            set { _ShowBottom = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWeb
        {
            get { return _ShowWeb; }
            set { _ShowWeb = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWap
        {
            get { return _ShowWap; }
            set { _ShowWap = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowApp
        {
            get { return _ShowApp; }
            set { _ShowApp = value; }
        }   
        //-----------------------------------------------------------------
        public string JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
        }
        //-----------------------------------------------------------------
        public DateTime PublishTime
        {
            get { return _PublishTime; }
            set { _PublishTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DisplayStartTime
        {
            get { return _DisplayStartTime; }
            set { _DisplayStartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DisplayEndTime
        {
            get { return _DisplayEndTime; }
            set { _DisplayEndTime = value; }
        }
        //-----------------------------------------------------------------
        public byte IconStatusId
        {
            get { return _IconStatusId; }
            set { _IconStatusId = value; }
        }
        //-----------------------------------------------------------------
        public byte ArticleTypeId
        {
            get { return _ArticleTypeId; }
            set { _ArticleTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus1
        {
            get { return _SumByStatus1; }
            set { _SumByStatus1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus2
        {
            get { return _SumByStatus2; }
            set { _SumByStatus2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus3
        {
            get { return _SumByStatus3; }
            set { _SumByStatus3 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource1
        {
            get { return _SumBySource1; }
            set { _SumBySource1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource2
        {
            get { return _SumBySource2; }
            set { _SumBySource2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload1
        {
            get { return _SumByDownload1; }
            set { _SumByDownload1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload2
        {
            get { return _SumByDownload2; }
            set { _SumByDownload2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView1
        {
            get { return _SumByView1; }
            set { _SumByView1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView2
        {
            get { return _SumByView2; }
            set { _SumByView2 = value; }
        }
        //-----------------------------------------------------------------

        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<Articles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Articles> l_Articles = new List<Articles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Articles m_Articles = new Articles(db.ConnectionString);
                    m_Articles.LanguageId = smartReader.GetByte("LanguageId");
                    m_Articles.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Articles.ArticleId = smartReader.GetInt32("ArticleId");
                    m_Articles.Title = smartReader.GetString("Title");
                    m_Articles.Summary = smartReader.GetString("Summary");
                    m_Articles.ArticleContent = smartReader.GetString("ArticleContent");
                    m_Articles.ArticleCode = smartReader.GetString("ArticleCode");
                    m_Articles.ImagePath = smartReader.GetString("ImagePath");
                    m_Articles.SourceUrl = smartReader.GetString("SourceUrl");
                    m_Articles.ArticleUrl = smartReader.GetString("ArticleUrl");
                    m_Articles.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Articles.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Articles.SiteId = smartReader.GetInt16("SiteId");
                    m_Articles.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_Articles.ArticleTypeId = smartReader.GetByte("ArticleTypeId");
                    m_Articles.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Articles.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Articles.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Articles.OriginalPrice = smartReader.GetFloat("OriginalPrice");
                    m_Articles.SalePrice = smartReader.GetFloat("SalePrice");
                    m_Articles.ContactPrice = smartReader.GetString("ContactPrice");
                    m_Articles.CurrencyId = smartReader.GetByte("CurrencyId");
                    m_Articles.InventoryStatusId = smartReader.GetByte("InventoryStatusId");
                    m_Articles.IsVerify = smartReader.GetByte("IsVerify");
                    m_Articles.ShowTop = smartReader.GetByte("ShowTop");
                    m_Articles.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Articles.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Articles.ShowWap = smartReader.GetByte("ShowWap");
                    m_Articles.ShowApp = smartReader.GetByte("ShowApp");
                    m_Articles.JsonData = smartReader.GetString("JsonData");
                    m_Articles.PublishTime = smartReader.GetDateTime("PublishTime");
                    m_Articles.DisplayStartTime = smartReader.GetDateTime("DisplayStartTime");
                    m_Articles.DisplayEndTime = smartReader.GetDateTime("DisplayEndTime");
                    m_Articles.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Articles.IconStatusId = smartReader.GetByte("IconStatusId");
                    m_Articles.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Articles.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Articles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Articles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Articles.Add(m_Articles);
                }
                reader.Close();              
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return l_Articles;
        }
        //-----------------------------------------------------------
        private List<ArticlesView> InitView(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticlesView> l_Articles = new List<ArticlesView>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticlesView m_Articles = new ArticlesView();
                    m_Articles.LanguageId = smartReader.GetByte("LanguageId");
                    m_Articles.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Articles.ArticleId = smartReader.GetInt32("ArticleId");
                    m_Articles.Title = smartReader.GetString("Title");
                    m_Articles.Summary = smartReader.GetString("Summary");
                    m_Articles.ArticleContent = smartReader.GetString("ArticleContent");
                    m_Articles.ArticleCode = smartReader.GetString("ArticleCode");
                    m_Articles.ImagePath = smartReader.GetString("ImagePath");
                    m_Articles.SourceUrl = smartReader.GetString("SourceUrl");
                    m_Articles.ArticleUrl = smartReader.GetString("ArticleUrl");
                    m_Articles.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Articles.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Articles.SiteId = smartReader.GetInt16("SiteId");
                    m_Articles.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_Articles.ArticleTypeId = smartReader.GetByte("ArticleTypeId");
                    m_Articles.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Articles.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Articles.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Articles.OriginalPrice = smartReader.GetFloat("OriginalPrice");
                    m_Articles.SalePrice = smartReader.GetFloat("SalePrice");
                    m_Articles.ContactPrice = smartReader.GetString("ContactPrice");
                    m_Articles.CurrencyId = smartReader.GetByte("CurrencyId");
                    m_Articles.InventoryStatusId = smartReader.GetByte("InventoryStatusId");
                    m_Articles.IsVerify = smartReader.GetByte("IsVerify");
                    m_Articles.ShowTop = smartReader.GetByte("ShowTop");
                    m_Articles.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Articles.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Articles.ShowWap = smartReader.GetByte("ShowWap");
                    m_Articles.ShowApp = smartReader.GetByte("ShowApp");
                    m_Articles.JsonData = smartReader.GetString("JsonData");
                    m_Articles.PublishTime = smartReader.GetDateTime("PublishTime");
                    m_Articles.DisplayStartTime = smartReader.GetDateTime("DisplayStartTime");
                    m_Articles.DisplayEndTime = smartReader.GetDateTime("DisplayEndTime");
                    m_Articles.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Articles.IconStatusId = smartReader.GetByte("IconStatusId");
                    m_Articles.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Articles.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Articles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Articles.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Articles.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_Articles.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_Articles.RevUserId = smartReader.GetInt32("RevUserId");
                    m_Articles.RevDateTime = smartReader.GetDateTime("RevDateTime");

                    l_Articles.Add(m_Articles);
                }
                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return l_Articles;
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
        //--------------------------------------------------------------
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
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
        public byte UpdatePublicTime(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_UpdatePublicTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@PublishTime", this.PublishTime));
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
        public byte UpdateArticleId(int NewArticleId, byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLanguages_UpdateArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@NewArticleId", NewArticleId));
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
        public byte UpdateDisplayOrder(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_UpdateDisplayOrder_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                //RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateShowTop(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_UpdateShowTop_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                //RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Articles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@Summary", this.Summary));
                cmd.Parameters.Add(new SqlParameter("@ArticleContent", this.ArticleContent));
                cmd.Parameters.Add(new SqlParameter("@ArticleCode", this.ArticleCode));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ArticleUrl", this.ArticleUrl));
                cmd.Parameters.Add(new SqlParameter("@SourceUrl", this.SourceUrl));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@OriginalPrice", this.OriginalPrice));
                cmd.Parameters.Add(new SqlParameter("@SalePrice", this.SalePrice));
                cmd.Parameters.Add(new SqlParameter("@ContactPrice", this.ContactPrice));
                cmd.Parameters.Add(new SqlParameter("@CurrencyId", this.CurrencyId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                if (this.DisplayStartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DisplayStartTime", this.DisplayStartTime));
                if (this.DisplayEndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DisplayEndTime", this.DisplayEndTime));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@IconStatusId", this.IconStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleId = Convert.ToInt32((cmd.Parameters["@ArticleId"].Value == null) ? 0 : (cmd.Parameters["@ArticleId"].Value));
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
                SqlCommand cmd = new SqlCommand("Articles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
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
        // Added by: Vu.The
        // Date: June 12, 2015
        //--------------------------------------------------------------  
        public List<Articles> GetTopFocusNewsByCateId(short CategoryId, byte LanguageId, byte ApplicationTypeId, int RowAmount)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                int ArticleId = 0;
                int ActUserId = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string Title = "";
                short DataSourceId = 0;
                byte ReviewStatusId = 0;
                short TagId = 0;
                short EventStreamId = 0;
                short DisplayTypeId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                byte ShowTop = 1;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId,
                                DisplayTypeId, DateFrom, DateTo, ShowTop, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        // Added by: Vu.The
        // Date: June 12, 2015
        //--------------------------------------------------------------  
        public List<Articles> GetByEventStreamId(short EventStreamId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                short CategoryId = 0;
                int ArticleId = 0;
                int ActUserId = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string Title = "";
                short DataSourceId = 0;
                byte ReviewStatusId = 0;
                short TagId = 0;
                short DisplayTypeId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                byte ShowTop = 1;
                int RowAmount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId,
                                DisplayTypeId, DateFrom, DateTo, ShowTop, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Articles> GetListByArticleId(int ArticleId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string Title = "";
                short DataSourceId = 0;
                byte ReviewStatusId = 0;
                short TagId = 0;
                short EventStreamId = 0;
                short DisplayTypeId = 0;                
                string DateFrom = "";
                string DateTo = "";
                int RowCount=0;               
                RetVal= GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, TagId, EventStreamId, 
                                DisplayTypeId, DateFrom, DateTo, ref RowCount); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Articles Get(int ArticleId, byte LanguageId, byte ApplicationTypeId)
        {
            Articles RetVal = new Articles(db.ConnectionString);
            try
            {
                List<Articles> list = GetListByArticleId(ArticleId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (Articles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Articles Get()
        {
            return Get(this.ArticleId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public Articles GetByArticleCode(string ArticleCode, byte LanguageId, byte ApplicationTypeId)
        {
            Articles RetVal = new Articles();
            try
            {
                string sql = "SELECT * FROM Articles WHERE ArticleCode='" + ArticleCode + "'";
                SqlCommand cmd = new SqlCommand(sql);
                List<Articles> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Articles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Articles GetByArticleCode()
        {
            Articles RetVal = new Articles();
            try
            {
                string sql = "SELECT * FROM Articles WHERE ArticleCode='" + this.ArticleCode + "'";
                SqlCommand cmd = new SqlCommand(sql);
                List<Articles> list = Init(cmd);
                if (list.Count > 0)
                {
                    RetVal = (Articles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Articles Static_Get(string Constr, int ArticleId, byte LanguageId, byte ApplicationTypeId)
        {
            Articles m_Articles = new Articles(Constr);
            return m_Articles.Get(ArticleId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Articles Static_Get(int ArticleId, byte LanguageId, byte ApplicationTypeId)
        {
           return Static_Get("",ArticleId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy,byte LanguageId, byte ApplicationTypeId, int ArticleId, string Title, 
                                        short DataSourceId, byte ReviewStatusId, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            short CategoryId = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, 
                            EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetListTopByCategoryId(int RowAmount, byte LanguageId, byte ApplicationTypeId, byte ReviewStatusId, short CategoryId)
        {
            int ActUserId = 0;
            int PageIndex = 0;
            string OrderBy = "";
            int ArticleId = 0;
            string Title = "";
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, 
                            EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPageByCategoryId(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, byte ReviewStatusId, short CategoryId, ref int RowCount)
        {
            int ActUserId = 0;
            string OrderBy = "";
            int ArticleId = 0;
            string Title = "";
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, 
                            EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, byte ReviewStatusId, string Title)
        {
            int ActUserId = 0;
            string OrderBy = "";
            int ArticleId = 0;
            short CategoryId = 0;
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, 
                            EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, byte ReviewStatusId, string Title, ref int RowCount)
        {
            int ActUserId = 0;
            string OrderBy = "";
            int ArticleId = 0;
            short CategoryId = 0;
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, 
                            EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int ArticleId, string Title, 
                                       short DataSourceId, byte ReviewStatusId, short CategoryId, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, 
                                       string DateTo, ref int RowCount)
        {
            this.LanguageId = LanguageId;
            this.ApplicationTypeId = ApplicationTypeId;
            this.ArticleId = ArticleId;
            this.Title = Title;
            this.DataSourceId = DataSourceId;
            this.ReviewStatusId = ReviewStatusId;
            this.CategoryId = CategoryId;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(this.Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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

        public List<ArticlesView> GetPageView(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(this.Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@IconStatusId", this.IconStatusId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitView(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<ArticlesView> GetPageWithCateId(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPageWithCateId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(this.Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@IconStatusId", this.IconStatusId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitView(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<ArticlesView> GetMostView(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPageMostView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(this.Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                if (this.IconStatusId > 0)
                    cmd.Parameters.Add(new SqlParameter("@IconStatusId", this.IconStatusId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitView(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        // Added by: Vu.The
        // Date: June 12, 2015
        //-------------------------------------------------------------- 
        public List<Articles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int ArticleId, string Title, 
                                       short DataSourceId, byte ReviewStatusId, short CategoryId, short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, 
                                       string DateTo, byte ShowTop,  ref int RowCount)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", this.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@InventoryStatusId", this.InventoryStatusId));
                cmd.Parameters.Add(new SqlParameter("@IconStatusId", this.IconStatusId));
                cmd.Parameters.Add(new SqlParameter("@IsVerify", this.IsVerify));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
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
        public List<Articles> GetPageNext( int RowAmount, byte LanguageId, byte ApplicationTypeId, int ArticleId,  short CategoryId, byte ReviewStatusId)
        {
            int ActUserId = 0;
            int PageIndex = 0;
            string OrderBy = "";
            string Title = "";
            short TagId = 0;            
            short DataSourceId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            List<Articles> RetVal = new List<Articles>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetPageNext");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(Title)));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
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
        // Added by: Vu.The June 12, 2015
        //--------------------------------------------------------------
        public List<Articles> GetRelatedList(int actUserId, byte languageId, byte applicationTypeId, int articleId, string orderBy)
        {
            List<Articles> RetVal = new List<Articles>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetRelatedList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", actUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", applicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", articleId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(orderBy)));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Articles> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, int ArticleId, string Title, short DataSourceId, byte ReviewStatusId, 
                                      short TagId, short EventStreamId, short DisplayTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, TagId, EventStreamId, 
                            DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------     
        public DataSet Articles_StatisticByCategoryId(int ActUserId, byte LanguageId, byte ApplicationTypeId, short SiteId, byte DataTypeId, short CategoryId, short DataSourceId, byte ReviewStatusId, 
                                                       int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_StatisticByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));   
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Articles_StatisticByCrDateTime(int ActUserId, byte LanguageId, byte ApplicationTypeId, short SiteId, byte DataTypeId, short CategoryId, short DataSourceId, byte ReviewStatusId, 
                                                       int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_StatisticByCrDateTime");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Articles_StatisticByCrUserId(int ActUserId, byte LanguageId, byte ApplicationTypeId, short SiteId, byte DataTypeId, short CategoryId, short DataSourceId, byte ReviewStatusId,
                                                       int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_StatisticByCrUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Articles_StatisticByDataSourceId(int ActUserId, byte LanguageId, byte ApplicationTypeId, short SiteId, byte DataTypeId, short CategoryId, short DataSourceId, byte ReviewStatusId,
                                                       int CrUserId, string DateFrom, string DateTo, ref int TotalCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_StatisticByDataSourceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                TotalCount = Convert.ToInt32(cmd.Parameters["@TotalCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet ArticleViewCount_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, short SiteId, byte DataTypeId, int ArticleId,
                                                        short CategoryId, string Title, short DataSourceId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleViewCount_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@Title", Title));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                RetVal = db.getDataSet(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// Báo cáo tổng hợp tin tức
        /// </summary>
        public List<Articles> Articles_ReportData(string DateFrom, string DateTo, string DateFromSame, string DateToSame)
        {
            List<Articles> RetVal = new List<Articles>();
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_ReportData");
                cmd.CommandType = CommandType.StoredProcedure;
                string  Datefrom = StringUtil.ConvertToDateTime(DateFrom).ToString("yyyy-MM-dd");
                string Dateto = StringUtil.ConvertToDateTime(DateTo).ToString("yyyy-MM-dd");
                string DateFromSame1 = StringUtil.ConvertToDateTime(DateFromSame).ToString("yyyy-MM-dd");
                string DateToSame1 = StringUtil.ConvertToDateTime(DateToSame).ToString("yyyy-MM-dd");
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", Datefrom));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", Dateto));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", DateFromSame1));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", DateToSame1));
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Articles_ReportData_Init(smartReader);

                reader.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------     

        private List<Articles> Articles_ReportData_Init(SmartDataReader smartReader)
        {
            List<Articles> l_Articles_ReportData = new List<Articles>();
            while (smartReader.Read())
            {
                Articles m_Articles = new Articles();
                m_Articles.CategoryId = smartReader.GetByte("CategoryId");
                m_Articles.Title = smartReader.GetString("CategoryName");
                m_Articles.SumByStatus1 = smartReader.GetInt32("SumByStatus1");
                m_Articles.SumByStatus2 = smartReader.GetInt32("SumByStatus2");
                m_Articles.SumByStatus3 = smartReader.GetInt32("SumByStatus3");
                m_Articles.SumBySource1 = smartReader.GetInt32("SumBySource1");
                m_Articles.SumBySource2 = smartReader.GetInt32("SumBySource2");
                //m_Articles.SumByDownload1 = smartReader.GetInt32("SumByDownload1");
                //m_Articles.SumByDownload2 = smartReader.GetInt32("SumByDownload2");
                m_Articles.SumByView1 = smartReader.GetInt32("SumByView1");
                m_Articles.SumByView2 = smartReader.GetInt32("SumByView2");
                l_Articles_ReportData.Add(m_Articles);
            }
            return l_Articles_ReportData;
        }
        //========================================================
        public byte Move(int ArticleId, short CategoryId, string ActionOrder)
        {
            byte retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_Move_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ActionOrder", ActionOrder));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                retVal = 0;
                throw ex;
            }
            return retVal;
        }

        public List<ArticlesView> GetByCustomerId(int CustomerID, int ArticleId, int ProvinceId, string OrderBy, string DateFrom, string DateTo, int PageSize, int PageNumber, ref int RowCount)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetByCustomer");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitView(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //---------------------------------------------------------------
        public int GetSiteMapPage(short siteId, int rowAmount = 5000)
        {
            int retVal = 0;
            try
            {
                string sql = "SELECT COUNT(1) FROM Articles Where SiteId = " + siteId + " AND CategoryId > 0 AND ReviewStatusId = 2 ";
                SqlCommand cmd = new SqlCommand(sql) { CommandType = CommandType.Text };
                retVal = db.ExecuteScalar(cmd);
                if (retVal % rowAmount > 0)
                    retVal = retVal / rowAmount + 1;
                else
                    retVal = retVal / rowAmount;
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return retVal;
        }
    }
}

