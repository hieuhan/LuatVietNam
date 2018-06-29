
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;


namespace ICSoft.CMSViewLib
{
    public class AlbumsView
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
        private DateTime _PublishTime = DateTime.MinValue;
        private DateTime _DisplayStartTime = DateTime.MinValue;
        private DateTime _DisplayEndTime = DateTime.MinValue;
        private byte _IconStatusId;
        private byte _ArticleTypeId;
        private byte _ReviewStatusId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private short _DisplayTypeId;
        private string _DisplayTypeName;
        private int _ViewCount;
        private byte _ViewType;
        //-----------------------------------------------------------------
        ~AlbumsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ViewType
        {
            get { return _ViewType; }
            set { _ViewType = value; }
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
        public short DisplayTypeId
        {
            get { return _DisplayTypeId; }
            set { _DisplayTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string DisplayTypeName
        {
            get { return _DisplayTypeName; }
            set { _DisplayTypeName = value; }
        }
        //-----------------------------------------------------------------
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------
        private List<AlbumSingersView> _lSingers = new List<AlbumSingersView>();
        private List<ArticleCategoriesView> _lCategory;
        private List<ArticleCategoriesView> _lMusicType;

        //-----------------------------------------------------------------
        public AlbumsView()
        {
        }
        
        //-----------------------------------------------------------------    
        public List<AlbumSingersView> lSingers
        {
            get { return _lSingers; }
            set { _lSingers = value; }
        }
        //-----------------------------------------------------------
        public List<ArticleCategoriesView> lCategory
        {
            get { return _lCategory; }
            set { _lCategory = value; }
        }
        //-----------------------------------------------------------
        public List<ArticleCategoriesView> lMusicType
        {
            get { return _lMusicType; }
            set { _lMusicType = value; }
        }

        //-----------------------------------------------------------
        public static List<AlbumsView> InitView(SmartDataReader smartReader)
        {
            return InitView(smartReader, false);
        }
        //-----------------------------------------------------------
        public static List<AlbumsView> InitView(SmartDataReader smartReader,bool hasViewType)
        {
            List<AlbumsView> l_Articles = new List<AlbumsView>();
            try
            {
                while (smartReader.Read())
                {
                    AlbumsView m_Articles = new AlbumsView();
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
                    if (String.IsNullOrEmpty(m_Articles.MetaTitle))
                        m_Articles.MetaTitle = m_Articles.Title;
                    m_Articles.MetaDesc = smartReader.GetString("MetaDesc");
                    if (String.IsNullOrEmpty(m_Articles.MetaDesc))
                        m_Articles.MetaDesc = m_Articles.Summary;
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
                    if (hasViewType) m_Articles.ViewType = smartReader.GetByte("ViewType");

                    l_Articles.Add(m_Articles);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return l_Articles;
        }
        //-----------------------------------------------------------
        public static AlbumsView Static_GetByViewType(byte ViewType, List<AlbumsView> list)
        {
            AlbumsView RetVal = new AlbumsView();
            try
            {
                RetVal = list.Find(i => i.ViewType == ViewType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static List<AlbumsView> Static_GetListByViewType(byte ViewType, List<AlbumsView> list)
        {
            List<AlbumsView> RetVal = new List<AlbumsView>();
            try
            {
                RetVal = list.FindAll(i => i.ViewType == ViewType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetCategoryName()
        {
            string RetVal = "";
            if (_lCategory != null)
            {
                for (int i = 0; i < _lCategory.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lCategory[i].CategoryName;
                }
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = _ArticleUrl;
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = _ImagePath;
            if (string.IsNullOrEmpty(_ImagePath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            return GetImageUrl().Replace("/Original/", "/Icon/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_IconWithHtmlTag(int width, int height)
        {
            string result = "";
            string imageUrl = GetImageUrl_Icon();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                result = "<a class=\"popup\" href=\"" + GetImageUrl() + "\"><img style=\"width:" + width.ToString() + "px;height:" + height.ToString() + "px\" src=\"" + imageUrl + "\" /></a>";
            }
            return result;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Thumb()
        {
            return GetImageUrl().Replace("/Original/", "/Thumb/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Standard()
        {
            return GetImageUrl().Replace("/Original/", "/Standard/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Mobile()
        {
            return GetImageUrl().Replace("/Original/", "/Mobile/");
        }
        //-----------------------------------------------------------------
        public string GetSingerName()
        {
            string RetVal = "";
            if (_lSingers != null)
            {
                for (int i = 0; i < _lSingers.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lSingers[i].SingerName;
                }
            }
            return RetVal;
        }
    }
}