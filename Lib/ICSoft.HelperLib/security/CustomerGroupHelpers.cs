using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;

namespace ICSoft.HelperLib
{
    public class CustomerGroupHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Web
        {
            get { return 1; }
            set { }
        }
        public static byte Wap
        {
            get { return 2; }
            set { }
        }
        public static byte SMS
        {
            get { return 3; }
            set { }
        }
        public static byte Business
        {
            get { return 4; }
            set { }
        }
        public static byte Promotion
        {
            get { return 5; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
