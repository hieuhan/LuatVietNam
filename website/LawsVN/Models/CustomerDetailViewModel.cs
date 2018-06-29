using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using ICSoft.CMSLib;

namespace LawsVN.Models
{
    public class CustomerDetailViewModel:ViewModelBase
    {
        public Customers mCustomers { get; set; }
        public Customers lCustomerReportVBQT { set; get; }
        public List<CustomerFields> lCustomerFields { set; get; }
        public List<CustomerCount> lCustomerLogs { get; set; }
    }
}