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
    public class ActionTypeHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Insert
        {
            get { return 1; }
            set { }
        }
        public static byte Update
        {
            get { return 2; }
            set { }
        }
        public static byte Delete
        {
            get { return 3; }
            set { }
        }
        public static byte Login
        {
            get { return 4; }
            set { }
        }
        public static byte Logout
        {
            get { return 5; }
            set { }
        }
        public static byte ViewContent
        {
            get { return 6; }
            set { }
        }
        public static byte Download
        {
            get { return 7; }
            set { }
        }
	}//end ApplicationType
    
}//end namespace service
