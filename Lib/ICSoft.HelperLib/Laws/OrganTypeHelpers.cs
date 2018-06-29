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
    public class OrganTypeHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Department

        {
            get { return 1; }
            set { }
        }
        public static byte Province 
        {
            get { return 2; }
            set { }
        }
        public static byte Other
        {
            get { return 3; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
