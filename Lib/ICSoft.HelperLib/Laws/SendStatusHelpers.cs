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
    public class SendStatusHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Waiting
        {
            get { return 1; }
            set { }
        }
        public static byte Success
        {
            get { return 2; }
            set { }
        }
        public static byte Error
        {
            get { return 3; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
