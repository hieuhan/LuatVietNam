using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSoft.ViewLibV3
{
    public class Categories
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string CategoryUrl { get; set; }
        public int ParentCategoryId { get; set; }
        public byte CategoryLevel { get; set; }
        public string ImagePath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string CanonicalTag { get; set; }
        public string H1Tag { get; set; }
        public string SeoFooter { get; set; }

        //-------------------------------------------------------------- 
        public string GetCategoryUrl()
        {
            string RetVal = CategoryUrl;
            if (!RetVal.StartsWith("http") && RetVal.StartsWith(Constants.ROOT_PATH) == false) RetVal = Constants.ROOT_PATH + RetVal;
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
            if (!RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;
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
