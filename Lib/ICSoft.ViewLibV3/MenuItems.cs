using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class MenuItems
    {
        public int MenuItemId { get; set; }
        public int MenuId { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string IconPath { get; set; }
        public string Url { get; set; }
        public int ParentItemId { get; set; }
        public byte ItemLevel { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string CanonicalTag { get; set; }
        public string H1Tag { get; set; }
        public string SeoFooter { get; set; }

        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = IconPath;
            if (string.IsNullOrEmpty(IconPath))
            {
                RetVal = Constants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
    }
}
