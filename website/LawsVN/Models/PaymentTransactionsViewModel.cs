using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using ICSoft.CMSLib;

namespace LawsVN.Models
{
    public class PaymentTransactionsViewModel : ViewModelBase
    { 
        public List<PaymentTransactionsView> lPaymentTransactions { get; set; }
        public List<CustomerCount> lCustomer { set; get; }
        //public CustomerPartialPagingAjax CustomerPartialPagingAjax { get; set; }
    }
}