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
    public class DisplayTypeHelpers
	{
        //---------------------------------------------------------------------------------------
        public static byte Committees
        {
            get { return 5; }
            set { }
        }
        
        public static byte DocNew
        {
            get { return 11; }
            set { }
        }
        public static byte DocNewEn
        {
            get { return 7; }
            set { }
        }
        public static byte DispatchNew
        {
            get { return 12; }
            set { }
        }
        public static byte DispatchTax
        {
            get { return 13; }
            set { }
        }
        public static byte DocView
        {
            get { return 14; }
            set { }
        }
        
        public static byte DocExpired
        {
            get { return 15; }
            set { }
        }
        public static byte ConsultingLaw
        {
            get { return 21; }
            set { }
        }
        
	}//end ApplicationType
    
}//end namespace service
