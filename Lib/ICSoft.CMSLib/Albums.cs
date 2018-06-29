
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
    public class Albums
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
        public int ViewCount { get; set; }
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Albums()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Albums(string constr)
        {
           db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);           
        }
        //-----------------------------------------------------------------
        ~Albums()
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
                    m_Articles.ViewCount = smartReader.GetInt32("ViewCount");

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
        //-------------------------------------------------------------- 
        public List<AlbumsView> GetPageView(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int SingerId, string DateFrom, string DateTo, byte GetCategory, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<AlbumsView> RetVal = new List<AlbumsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Albums_GetPage");
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
                cmd.Parameters.Add(new SqlParameter("@ArticleRelateId", SingerId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@GetCategory", GetCategory));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = AlbumsView.InitView(smartReader);
                //Get Singers
                reader.NextResult();
                List<AlbumSingersView> l_Singers = AlbumSingersView.Init(smartReader, true);

                //Get Category
                List<ArticleCategoriesView> l_Category = null;
                if (GetCategory==1)
                {
                    reader.NextResult();
                    l_Category = ArticleCategoriesView.Init(smartReader, true);
                }

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].lSingers = AlbumSingersView.Static_GetList(RetVal[i].ArticleId, l_Singers);
                    if (GetCategory == 1)
                    {
                        RetVal[i].lCategory = ArticleCategoriesView.Static_GetList(RetVal[i].ArticleId, l_Category);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public int CheckName(int AlbumId, byte DataTypeId, byte AlbumTypeId, string Title)
        {
            int RetVal = 0;
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Albums_CheckName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", AlbumId));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(Title)));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", AlbumTypeId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListByAlbumTypeId(byte AlbumTypeId, byte ShowTop, int RowAmount, string OrderBy)
        {
            int ActUserId=0;
            int PageIndex=0;
            int SingerId = 0;
            string DateFrom="";
            string DateTo="";
            int RowCount=0;
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = ShowTop;
            this.ArticleTypeId = AlbumTypeId;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListByAlbumTypeIdPaging(byte AlbumTypeId, byte ShowTop, string OrderBy, int RowAmount, int PageIndex, ref int RowCount)
        {
            int ActUserId = 0;
            int SingerId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = ShowTop;
            this.ArticleTypeId = AlbumTypeId;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListByCategoryId(short CategoryId, byte ShowTop, int RowAmount, string OrderBy)
        {
            int ActUserId = 0;
            int PageIndex = 0;
            int SingerId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = ShowTop;
            this.CategoryId = CategoryId;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListByCategoryIdPaging(short CategoryId, byte ShowTop, string OrderBy, int RowAmount, int PageIndex, ref int RowCount)
        {
            int ActUserId = 0;
            int SingerId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = ShowTop;
            this.CategoryId = CategoryId;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListBySingerIdPaging(int SingerId, byte ShowTop, string OrderBy, int RowAmount, int PageIndex, ref int RowCount)
        {
            int ActUserId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = ShowTop;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_Search(short CategoryId, byte AlbumTypeId, int SingerId, string Keyword, int RowAmount, int PageIndex, string OrderBy, ref int RowCount)
        {
            int ActUserId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.CategoryId = CategoryId;
            this.ArticleTypeId = AlbumTypeId;
            this.Title = Keyword;
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListRBT_Hot(int RowAmount, string OrderBy)
        {
            int ActUserId = 0;
            int PageIndex = 0;
            int SingerId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = 1;
            this.CategoryId = 0;
            this.DataTypeId = 19;//Album nhac cho
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_GetListRBT_HotPaging(string OrderBy, int RowAmount, int PageIndex, ref int RowCount)
        {
            int ActUserId = 0;
            int SingerId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.ShowTop = 1;
            this.CategoryId = 0;
            this.DataTypeId = 19;//Album nhac cho
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<AlbumsView> View_SearchRBT(short CategoryId, byte AlbumTypeId, int SingerId, string Keyword, int RowAmount, int PageIndex, string OrderBy, ref int RowCount)
        {
            int ActUserId = 0;
            string DateFrom = "";
            string DateTo = "";
            byte GetCategory = 0;
            this.ReviewStatusId = 2;
            this.CategoryId = CategoryId;
            this.ArticleTypeId = AlbumTypeId;
            this.Title = Keyword;
            this.DataTypeId = 19;//Album nhac cho
            return GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, SingerId, DateFrom, DateTo, GetCategory, ref RowCount);
        }
    }
}

