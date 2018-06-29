using ICSoft.CMSViewLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;

namespace LawsVN.Models
{
    public class ServiceModel : ViewModelBase
    {
        public List<ArticlesViewDetail> lArticlesViewPackage { get; set; }
        public ArticlesViewDetail mArticlesViewDetailCompare { get; set; }
        public DataTable ListCustomerServices { get; set; }
    }
}