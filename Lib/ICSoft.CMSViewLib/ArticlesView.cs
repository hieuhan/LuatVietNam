
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;



namespace ICSoft.CMSViewLib
{
    public class ArticlesView
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
        private int _ProvinceId;
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
        private int _UpdUserId;
        private DateTime _UpdDateTime;
        private int _RevUserId;
        private DateTime _RevDateTime;
        private short _DisplayTypeId;
        private string _DisplayTypeName;
        private int _ViewCount;
        public int CommentCount;

        private List<ArticleCategoriesView> _lArticleCategoriesView;
        private ArticleViewCountView _mArticleViewCountView;
        private List<FeaturesView> _lFeaturesView;
        private List<TagsView> _lTagsView;
        private List<MediasView> _lMediasView;
        private List<ArticleCommentsView> _lArticleCommentsView;
        //-----------------------------------------------------------------
        public ArticlesView()
        {
            
        }
        //-----------------------------------------------------------------
        ~ArticlesView()
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
        public int ProvinceId
        {
            get { return _ProvinceId; }
            set {  _ProvinceId = value; }
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
            get
            {
                if (String.IsNullOrEmpty(_MetaTitle))
                    return _Title;
                else
                    return _MetaTitle;
            }
            set { _MetaTitle = value; }
        }
        //-----------------------------------------------------------------
        public string MetaDesc
        {
            get
            {
                if (String.IsNullOrEmpty(_MetaDesc))
                    return _Summary;
                else
                    return _MetaDesc;
            }
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
        public int UpdUserId
        {
            get { return _UpdUserId; }
            set { _UpdUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime UpdDateTime
        {
            get { return _UpdDateTime; }
            set { _UpdDateTime = value; }
        }
        //-----------------------------------------------------------------
        public int RevUserId
        {
            get { return _RevUserId; }
            set { _RevUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime RevDateTime
        {
            get { return _RevDateTime; }
            set { _RevDateTime = value; }
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
        public ArticleViewCountView mArticleViewCountView
        {
            get { return _mArticleViewCountView; }
            set { _mArticleViewCountView = value; }
        }
        //-----------------------------------------------------------------
        public List<FeaturesView> lFeaturesView
        {
            get { return _lFeaturesView; }
            set { _lFeaturesView = value; }
        }
        //-----------------------------------------------------------------
        public List<TagsView> lTagsView
        {
            get { return _lTagsView; }
            set { _lTagsView = value; }
        }
        //-----------------------------------------------------------------
        public List<MediasView> lMediasView
        {
            get { return _lMediasView; }
            set { _lMediasView = value; }
        }
        //-----------------------------------------------------------------
        public List<ArticleCommentsView> lArticleCommentsView
        {
            get { return _lArticleCommentsView; }
            set { _lArticleCommentsView = value; }
        }
        //-------------------------------------------------------------- 
        public List<ArticleCategoriesView> lArticleCategoriesView
        {
            get { return _lArticleCategoriesView; }
            set { _lArticleCategoriesView = value; }
        }
        //-----------------------------------------------------------------
        public string GetCategoryName()
        {
            string RetVal = "";
            if (_lArticleCategoriesView != null)
            {
                for (int i = 0; i < _lArticleCategoriesView.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lArticleCategoriesView[i].CategoryName;
                }
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string TruncateTitle(int mLengthRemain)
        {
            string RetVal = _Title;
            if (_Title.Length > mLengthRemain)
            {
                RetVal = _Title.Substring(0, mLengthRemain);
                string nextChar = _Title.Substring(mLengthRemain, 1);
                if (nextChar != " ") RetVal = RetVal.Substring(0, RetVal.LastIndexOf(" "));
                RetVal = RetVal + " ...";
            }
            return RetVal.Trim();
        }
        //-------------------------------------------------------------- 
        public string GetArticleUrl()
        {
            return string.IsNullOrEmpty(_ArticleUrl) ? string.Empty : UrlGetPrefix(CmsConstants.ROOT_PATH, _ArticleUrl);
            //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            //return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = _ImagePath;
            if (string.IsNullOrEmpty(_ImagePath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            else
            {
                if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
                {
                    RetVal = "";
                    string[] l_Path = _ImagePath.Split('/');
                    for(int index =0; index< l_Path.Length; index++)
                    {
                        
                        if(index < l_Path.Length-1)
                        {
                            RetVal += l_Path[index] + "/";
                        }
                        else
                        {
                            RetVal += "550x500/" + l_Path[index];
                        }
                    }
                    RetVal = UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                    //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                }
                else
                {
                    RetVal = UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                    //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                }
                
            }
            
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrlOriginal()
        {
            string RetVal = _ImagePath;
            RetVal = string.IsNullOrEmpty(_ImagePath) ? CmsConstants.NO_IMAGE_URL : UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);

            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageCoverUrl()
        {
            return string.IsNullOrEmpty(_SourceUrl) ? "http://funring.icsoft.vn/Images/860x360.png" : UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, _SourceUrl);
            //RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if(_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/97x75_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "c_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Icon/");
            }
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
        /// <summary>
        /// 97x75
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/97x75_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "m_" + l_Path[index];
                    }
                }

                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 150x100
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb2()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/150x100_crop/");
            }

            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "m_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 200x110_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb3()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/200x110_crop/");
            }

            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "m_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 100x100_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb4()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/100x100_crop/");
            }

            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "m_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 550x500
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Standard()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl();
            }

            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {
                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "l_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Standard/");
            }
        }
        //-------------------------------------------------------------- 

        /// <summary>
        /// 362x235
        /// </summary>
        /// <returns></returns>

        public string GetImageUrl_Mobile()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/362x235_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "l_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 300x200
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile2()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/300x200_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "l_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 465x325
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile3()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/465x325_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "l_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN, RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 360x200_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile4()
        {

            if (string.IsNullOrEmpty(_ImagePath))
            {
                return CmsConstants.NO_IMAGE_URL;
            }
            else if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/360x200_crop/");
            }
            else if (!_ImagePath.Contains("uploaded/Images/"))// old luat media
            {
                if (string.IsNullOrEmpty(_ImagePath))
                    return CmsConstants.NO_IMAGE_URL;

                string RetVal = "";
                string[] l_Path = _ImagePath.Split('/');
                for (int index = 0; index < l_Path.Length; index++)
                {

                    if (index < l_Path.Length - 1)
                    {
                        RetVal += l_Path[index] + "/";
                    }
                    else
                    {
                        RetVal += "l_" + l_Path[index];
                    }
                }
                return UrlGetPrefix(CmsConstants.WEBSITE_IMAGEDOMAIN , RetVal);
                //if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                //return RetVal;
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        public string GetCurrencyName()
        {
            if (_CurrencyId == 1) return CmsConstants.CURRENCY_VND;
            else if (_CurrencyId == 2) return CmsConstants.CURRENCY_USD;
            else return CmsConstants.CURRENCY;
        }
        //--------------------------------------------------------------  
        public string GetSalePrice()
        {
            string strPrice = CmsConstants.CONTACT_PRICE;
            if (_SalePrice > 0)
            {
                strPrice = _SalePrice.ToString("N0") + GetCurrencyName();
            }
            else if (!string.IsNullOrEmpty(_ContactPrice)) strPrice = _ContactPrice;

            return strPrice;
        }
        //--------------------------------------------------------------  
        public string GetOriginalPrice()
        {
            string strPrice = "";
            if (_OriginalPrice > 0)
            {
                strPrice = _OriginalPrice.ToString("N0") + GetCurrencyName();
            }
            return strPrice;
        }
        //--------------------------------------------------------------  
        public string GetSavingPrice()
        {
            string strPrice = "";
            if (_OriginalPrice > 0 && _SalePrice > 0)
            {
                strPrice = (_OriginalPrice - _SalePrice).ToString("N0") + GetCurrencyName();
            }

            return strPrice;
        }
        //--------------------------------------------------------------  
        public string GetSavingPricePercent()
        {
            string strPrice = "";
            if (_OriginalPrice > 0 && _SalePrice > 0)
            {
                strPrice = (((_OriginalPrice - _SalePrice)/_OriginalPrice) * 100).ToString("N0") + " %";
            }

            return strPrice;
        }
        //--------------------------------------------------------------  
        public string GetInventoryStatus()
        {
            string RetVal = "";
            if (_InventoryStatusId == 1) RetVal = "Còn hàng";
            else if (_InventoryStatusId == 2) RetVal = "Hết hàng";

            return RetVal;
        }
        //--------------------------------------------------------------  
        public static List<ArticlesView> Static_GetByCategoryId(short CategoryId, List<ArticlesView> list)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            RetVal = list.FindAll(i => i.CategoryId == CategoryId);
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static List<ArticlesView> Static_GetByCategoryIdAndDisplayTypeName(short CategoryId, string DisplayTypeName, List<ArticlesView> list)
        {
            List<ArticlesView> RetVal = new List<ArticlesView>();
            RetVal = list.FindAll(i => i.CategoryId == CategoryId && i.DisplayTypeName == DisplayTypeName);
            return RetVal;
        }
        //-----------------------------------------------------------------
        private static string UrlGetPrefix(string prefix , string retVal)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = CmsConstants.ROOT_PATH;
            }
            if (!prefix.EndsWith("/"))
            {
                prefix = string.Concat(prefix, "/");
            }
            string[] prefixes = {"http://", "https://", "/"};
            return !prefixes.Any(p => retVal.StartsWith(p, StringComparison.OrdinalIgnoreCase)) ? string.Concat(prefix, retVal) : retVal;
        }
    }
}

