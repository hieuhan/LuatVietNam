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
    public class FieldHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Tax
        {
            get { return 4; }
            set { }
        }
        public static byte Customs
        {
            get { return 21; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
