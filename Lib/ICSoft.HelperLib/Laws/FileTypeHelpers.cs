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
    public class FileTypeHelpers
	{
        //---------------------------------------------------------------------------------------
        
        public static byte Word
        {
            get { return 1; }
            set { }
        }
        public static byte Pdf
        {
            get { return 2; }
            set { }
        }
        public static byte Html
        {
            get { return 5; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
