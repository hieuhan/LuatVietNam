using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct DeviceHelpers        
    {
       
        public static byte Android
        {
            get { return 1; }
            set { }
        }
        public static byte IOS
        {
            get { return 2; }
            set { }
        }
        public static byte WindowPhone
        {
            get { return 3; }
            set { }
        }
       
    }
}
