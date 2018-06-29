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
    public class DocTypeHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Law
        {
            get { return 10; }
            set { }
        }
        public static byte Dispatch
        {
            get { return 3; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
