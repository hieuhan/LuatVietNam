using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
namespace ICSoft.HelperLib
{
    public class StatusHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Active
        {
            get { return 1; }
            set { }
        }
        public static byte Suspense
        {
            get { return 2; }
            set { }
        }
        public static byte Finished
        {
            get { return 3; }
            set { }
        }
        public static byte Deleted
        {
            get { return 4; }
            set { }
        }
	}//end ApplicationType
    
}//end namespace service
