using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Articles
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleCode { get; set; }
        public string ImagePath { get; set; }
        public string ArticleUrl { get; set; }
        public short CategoryId { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime CrDateTime { get; set; }
        public byte IconStatusId { get; set; }
        public byte ReviewStatusId { get; set; }
        public int ViewCount { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string CanonicalTag { get; set; }
        public string H1Tag { get; set; }
        public string SeoFooter { get; set; }

        //-------------------------------------------------------------- 
        public string TruncateTitle(int mLengthRemain)
        {
            string RetVal = Title;
            if (Title.Length > mLengthRemain)
            {
                RetVal = Title.Substring(0, mLengthRemain);
                string nextChar = Title.Substring(mLengthRemain, 1);
                if (nextChar != " ") RetVal = RetVal.Substring(0, RetVal.LastIndexOf(" "));
                RetVal = RetVal + " ...";
            }
            return RetVal.Trim();
        }
        //-------------------------------------------------------------- 
        public string TruncateSummary(int mLengthRemain)
        {
            string RetVal = Summary;
            if (Summary.Length > mLengthRemain)
            {
                RetVal = Summary.Substring(0, mLengthRemain);
                string nextChar = Summary.Substring(mLengthRemain, 1);
                if (nextChar != " ") RetVal = RetVal.Substring(0, RetVal.LastIndexOf(" "));
                RetVal = RetVal + " ...";
            }
            return RetVal.Trim();
        }
        //-------------------------------------------------------------- 
        public string GetArticleUrl()
        {
            string RetVal = ArticleUrl;
            if (!RetVal.StartsWith("http")) RetVal = Constants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = ImagePath;
            if (string.IsNullOrEmpty(ImagePath))
            {
                RetVal = Constants.NO_IMAGE_URL;
            }
            else
            {
                if (!RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;
            }

            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrlOriginal()
        {
            string RetVal = ImagePath;
            if (string.IsNullOrEmpty(ImagePath))
            {
                RetVal = Constants.NO_IMAGE_URL;
            }
            else
            {
                if (!RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;

            }

            return RetVal;
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
    }
}
