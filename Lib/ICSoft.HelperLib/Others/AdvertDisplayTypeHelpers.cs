using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct AdvertDisplayTypeHelpers        
    {
       
        public static byte ListAll
        {
            get { return 1; }
            set { }
        }
        public static byte ListRandom
        {
            get { return 2; }
            set { }
        }
        public static byte ListSlide
        {
            get { return 3; }
            set { }
        }
    }
}
