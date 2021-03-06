using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class DocConstants 
    /// </summary>
    public class DocConstants        
    {
        public static string CUSTOMER_CONSTR = (ConfigurationManager.AppSettings["CUSTOMER_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["CUSTOMER_CONSTR"];
        public static string DOC_CONSTR = (ConfigurationManager.AppSettings["DOC_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["DOC_CONSTR"];
        public static string CMS_CONSTR = (ConfigurationManager.AppSettings["CMS_CONSTR"] == null) ? "" : ConfigurationManager.AppSettings["CMS_CONSTR"];
    }
}
