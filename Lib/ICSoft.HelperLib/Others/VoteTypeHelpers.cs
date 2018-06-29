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
    public class VoteTypeHelpers
	{
        //---------------------------------------------------------------------------------------
        
        public static byte OneSelect
        {
            get { return 1; }
            set { }
        }
        public static byte MultiSelect
        {
            get { return 2; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
