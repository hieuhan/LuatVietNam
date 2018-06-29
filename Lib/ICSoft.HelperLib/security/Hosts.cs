using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ICSoft.CMSLib;
namespace ICSoft.HelperLib
{
    public class HostHelpers
    {
        public static string Get_PRIMARY_CONSTR()
        {
            return CmsConstants.CMS_CONSTR;
        }
        public static string Get_GATE_CONSTR()
        {
            return CmsConstants.CMS_CONSTR;
        }
        public static string Get_PRO_CONSTR()
        {
            return CmsConstants.CMS_CONSTR;
        }
    }
}
