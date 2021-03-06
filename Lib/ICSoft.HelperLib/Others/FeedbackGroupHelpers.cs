using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct FeedbackGroupHelpers        
    {

        public static byte Default
        {
            get { return 1; }
            set { }
        }
        public static byte ErrorReport
        {
            get { return 2; }
            set { }
        }
        public static byte RequestService
        {
            get { return 3; }
            set { }
        }
        public static byte CustomerFeedback
        {
            get { return 20; }
            set { }
        }
        public static byte RegistBook
        {
            get { return 21; }
            set { }
        }
        public static byte RegistBook2011
        {
            get { return 22; }
            set { }
        }
        public static byte RegistBook2012
        {
            get { return 23; }
            set { }
        }
    }
}
