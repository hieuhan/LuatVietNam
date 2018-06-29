using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using ICSoft.CMSLib;
using LawsVN.Library.Sercurity;
using LawsVN.Library.Sercurity;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Models
{
    public class ViewModelBase 
    {
        #region SEO

        private bool _isIndex = true;

        public bool IsIndex
        {
            get { return _isIndex; }
            set { _isIndex = value; }
        }

        public bool IsHomePage { get; set; }

        public string SeoHeader { get; set; }

        public string SeoFooter { get; set; }

        private string _websiteTitle;

        public string WebsiteTitle
        {
            get
            {
                return !string.IsNullOrEmpty(_websiteTitle) ? (Page > 1 ? string.Concat(_websiteTitle.Replace("\"", ""), " - " , Resource.Page , " ", Page) : _websiteTitle.Replace("\"", "")) : Constants.WebsiteTitle;
            }
            set { _websiteTitle = value; }
        }

        private string _metaTitle;

        public string MetaTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_metaTitle))
                    _metaTitle = string.Empty;
                return string.Concat("content=\"", _metaTitle, "\"");
            }
            set { _metaTitle = value; }
        }

        public string WebsiteCanonical
        {
            get
            {
                return _websiteCanonical;//.TrimmedOrDefault(AbsoluteUri.BuildQueryStringUrl(new NameValueCollection { { "page", string.Empty } }));
            }
            set { _websiteCanonical = value; }
        }

        private string _websiteDescription;

        public string WebsiteDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_websiteDescription))
                    _websiteDescription = string.Empty;
                return string.Concat("content=\"", _websiteDescription, "\"");
            }
            set
            {
                _websiteDescription = value;
            }
        }

        private string _websiteKeywords;

        public string WebsiteKeywords
        {
            get
            {
                if (string.IsNullOrEmpty(_websiteKeywords))
                    _websiteKeywords = string.Empty;
                return string.Concat("content=\"", _websiteKeywords, "\"");
            }
            set
            {
                _websiteKeywords = value;
            }
        }

        private string _websiteImage;
        private string _websiteCanonical;

        public string WebsiteImage
        {
            get
            {
                if (string.IsNullOrEmpty(_websiteImage))
                    _websiteImage = string.Empty;
                return _websiteImage;
            }
            set
            {
                _websiteImage = value;
            }
        }

        public string AbsoluteUri
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }


        #endregion

        #region Exception
        public Exception InnerException { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ErrorMessage { get; set; }
        public int HttpStatusCode { get; set; }
        #endregion

        public virtual LawsVnPrincipal LawsUser
        {
            get { return HttpContext.Current.User as LawsVnPrincipal; }
        }

       
        /// <summary>
        /// Vị trí quảng cáo
        /// </summary>
        public int[] AdvertPositionId { get; set; }

        public string ReturnUrl { get; set; }

        public bool NotAuthenticate { get; set; }

        public bool NotAuthorize { get; set; }

        public MessageTypes MessageType { get; set; }

        public string LoginMessages { get; set; }

        public string SessionId
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return !string.IsNullOrWhiteSpace(HttpContext.Current.Session["_LuatVietNamSessionId"].ToString())
                        ? HttpContext.Current.Session["_LuatVietNamSessionId"].ToString()
                        : HttpContext.Current.Session.SessionID;
                }
                return string.Empty;
            }
        }

        public int Page { get; set; }

        public enum MessageTypes
        {
            NotAuth, //Hiệu lực, liên quan, lược đồ
            NotAuthDownload //Tải về
        }
    }

}