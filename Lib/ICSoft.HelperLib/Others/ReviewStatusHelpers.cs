using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct ReviewStatusHelpers        
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
        public static byte Unreview
        {
            get { return 3; }
            set { }
        }
        public static byte Reject
        {
            get { return 4; }
            set { }
        }
    }
}
