using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct AdvertContentTypeHelpers        
    {
       
        public static byte Image
        {
            get { return 1; }
            set { }
        }
        public static byte Script
        {
            get { return 2; }
            set { }
        }
        public static byte Flash
        {
            get { return 3; }
            set { }
        }
    }
}
