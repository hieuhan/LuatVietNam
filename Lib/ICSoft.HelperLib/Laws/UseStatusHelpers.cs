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
    public class UseStatusHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte NotUsing
        {
            get { return 1; }
            set { }
        }
        public static byte Using
        {
            get { return 2; }
            set { }
        }
        public static byte UseForTimeEffect
        {
            get { return 3; }
            set { }
        }
        public static byte UseForSMS
        {
            get { return 4; }
            set { }
        }
        
        
	}//end ApplicationType
    
}//end namespace service
