using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.HelperLib
{
    public struct ServiceTypeHelpers        
    {

        public static byte Trainings
        {
            get { return 1; }
            set { }
        }
        public static byte Consultings
        {
            get { return 2; }
            set { }
        }
        public static byte Sponsorship
        {
            get { return 3; }
            set { }
        }
    }
}
