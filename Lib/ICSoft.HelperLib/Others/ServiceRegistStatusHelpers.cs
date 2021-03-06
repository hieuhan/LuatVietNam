using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct ServiceRegistStatusHelpers        
    {

        public static byte Pending
        {
            get { return 1; }
            set { }
        }
        public static byte Reviewed
        {
            get { return 2; }
            set { }
        }
        public static byte Completed
        {
            get { return 3; }
            set { }
        }
        public static byte Expired
        {
            get { return 4; }
            set { }
        }
    }
}
