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
    public class DataTypeHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Article
        {
            get { return 1; }
            set { }
        }
        public static byte Doc
        {
            get { return 2; }
            set { }
        }
        public static byte DocFile
        {
            get { return 3; }
            set { }
        }
        public static byte Consulting
        {
            get { return 4; }
            set { }
        }
        public static byte NewsEffect
        {
            get { return 5; }
            set { }
        }
        public static byte ArticleNewsEffect
        {
            get { return 6; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
