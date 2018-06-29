
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;

namespace ICSoft.CMSViewLib
{
    public class CategoriesView
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _CategoryId;
	    private string _CategoryName;
	    private string _CategoryDesc;
	    private string _CategoryUrl;
        private short _DataTypeId;
        private short _SiteId;
        private short _FeatureGroupId;
        private byte _ShowTop;
        private byte _ShowBottom;
        private byte _ShowWeb;
        private byte _ShowWap;
        private byte _ShowApp;
	    private string _MetaTitle;
	    private string _MetaDesc;
	    private string _MetaKeyword;
        private string _CanonicalTag;
        private string _H1Tag;
        private string _SeoFooter;
	    private int _ParentCategoryId;
	    private byte _CategoryLevel;
	    private string _ImagePath;
	    private int _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private string _JsonData;
        private short _DisplayTypeId;
        private string _DisplayTypeName;

        private List<ArticlesView> _lArticlesView;
        private List<CategoriesView> _lCategoriesViewToRoot;
        //-----------------------------------------------------------------
        public CategoriesView()
        {
		}
        //-----------------------------------------------------------------        
        ~CategoriesView()
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
	    public short CategoryId
        {
		    get { return _CategoryId; }
		    set { _CategoryId = value; }
	    }
        //-----------------------------------------------------------------
        public string CategoryName
		{
            get { return _CategoryName; }
		    set { _CategoryName = value; }
		}    
        //-----------------------------------------------------------------
        public string CategoryDesc
		{
            get { return _CategoryDesc; }
		    set { _CategoryDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string CategoryUrl
		{
            get { return _CategoryUrl; }
		    set { _CategoryUrl = value; }
		}
        //-----------------------------------------------------------------
        public short DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short FeatureGroupId
        {
            get { return _FeatureGroupId; }
            set { _FeatureGroupId = value; }
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
        public string CanonicalTag
        {
            get { return _CanonicalTag; }
            set { _CanonicalTag = value; }
        }
        //-----------------------------------------------------------------
        public string H1Tag
        {
            get { return _H1Tag; }
            set { _H1Tag = value; }
        }
        //-----------------------------------------------------------------
        public string SeoFooter
        {
            get { return _SeoFooter; }
            set { _SeoFooter = value; }
        }    
        //-----------------------------------------------------------------
        public int ParentCategoryId
		{
            get { return _ParentCategoryId; }
		    set { _ParentCategoryId = value; }
		}    
        //-----------------------------------------------------------------
        public byte CategoryLevel
		{
            get { return _CategoryLevel; }
		    set { _CategoryLevel = value; }
		}    
        //-----------------------------------------------------------------
        public string ImagePath
		{
            get { return _ImagePath; }
		    set { _ImagePath = value; }
		}    
        //-----------------------------------------------------------------
        public int DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
		}    
        //-----------------------------------------------------------------
        public byte ReviewStatusId
		{
            get { return _ReviewStatusId; }
		    set { _ReviewStatusId = value; }
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
        public string JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
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
        public List<ArticlesView> lArticlesView
        {
            get { return _lArticlesView; }
            set { _lArticlesView = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesViewToRoot
        {
            get { return _lCategoriesViewToRoot; }
            set { _lCategoriesViewToRoot = value; }
        }

        //-----------------------------------------------------------------
        public static List<CategoriesView> Init(SmartDataReader smartReader)
        {
            List<CategoriesView> l_Categories = new List<CategoriesView>();
            try
            {
                while (smartReader.Read())
                {
                    CategoriesView m_Categories = new CategoriesView();
                    m_Categories.LanguageId = smartReader.GetByte("LanguageId");
                    m_Categories.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Categories.CategoryName = smartReader.GetString("CategoryName");
                    m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                    m_Categories.SiteId = smartReader.GetInt16("SiteId");
                    m_Categories.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Categories.ShowTop = smartReader.GetByte("ShowTop");
                    m_Categories.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Categories.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Categories.ShowWap = smartReader.GetByte("ShowWap");
                    m_Categories.ShowApp = smartReader.GetByte("ShowApp");
                    m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                    if(String.IsNullOrEmpty(m_Categories.MetaTitle))
                        m_Categories.MetaTitle = m_Categories.CategoryName;
                    m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                    if (String.IsNullOrEmpty(m_Categories.MetaDesc))
                        m_Categories.MetaTitle = m_Categories.CategoryDesc;
                    m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Categories.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_Categories.H1Tag = smartReader.GetString("H1Tag");
                    m_Categories.SeoFooter = smartReader.GetString("SeoFooter");
                    m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                    m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                    m_Categories.ImagePath = smartReader.GetString("ImagePath");
                    m_Categories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Categories.JsonData = smartReader.GetString("JsonData");
                    m_Categories.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Categories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Categories.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Categories.Add(m_Categories);
                }
                return l_Categories;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public string GetCategoryUrl()
        {
            return string.IsNullOrEmpty(_CategoryUrl) ? string.Empty : UrlGetPrefix(CmsConstants.ROOT_PATH, _CategoryUrl);
            //string RetVal = _CategoryUrl;
            //if (!RetVal.StartsWith("http://") && RetVal.StartsWith(CmsConstants.ROOT_PATH)==false) RetVal = CmsConstants.ROOT_PATH + RetVal;
            //return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            return string.IsNullOrEmpty(_ImagePath) ? CmsConstants.NO_IMAGE_URL : UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, _ImagePath);
            //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            //return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            return GetImageUrl().Replace("/Original/", "/Icon/");
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
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByParentCategoryId(short ParentCategoryId, List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ParentCategoryId == ParentCategoryId);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByShowTop(List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ShowTop == 1);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByShowBottom(List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ShowBottom == 1);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByShowWeb(List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ShowWeb == 1);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByShowWap(List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ShowWap == 1);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByShowApp(List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.ShowApp == 1);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static CategoriesView Static_GetByCategoryId(short CategoryId, List<CategoriesView> list)
        {
            CategoriesView RetVal = new CategoriesView();
            RetVal = list.Find(i => i.CategoryId == CategoryId);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<CategoriesView> Static_GetByDisplayTypeName(string DisplayTypeName, List<CategoriesView> list)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            RetVal = list.FindAll(i => i.DisplayTypeName == DisplayTypeName);
            return RetVal;
        }
        //-----------------------------------------------------------------
        private static string UrlGetPrefix(string prefix, string retVal)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = CmsConstants.ROOT_PATH;
            }
            if (!prefix.EndsWith("/"))
            {
                prefix = string.Concat(prefix, "/");
            }
            string[] prefixes = { "http://", "https://", "/" };
            return !prefixes.Any(p => retVal.StartsWith(p, StringComparison.OrdinalIgnoreCase)) ? string.Concat(prefix, retVal) : retVal;
        }
    } 
}