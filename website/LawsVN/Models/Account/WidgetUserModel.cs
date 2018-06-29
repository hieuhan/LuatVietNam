using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVN.Models.Account
{
    public class WidgetUserModel
    {
        public int CountMessages { get; set; }
        public int CountCustomerDocs { get; set; }
        public int CountThongBaoHieuLuc { get; set; }
        public int CountPaymentTransactionSuccess { get; set; }
        public bool IsUserVip {get;set;}
        public byte IsRegistService { get; set; }
        public string CustomerNameSubstring { get; set; }
    }
}