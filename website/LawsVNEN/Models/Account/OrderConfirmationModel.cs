using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models.Account
{
    public class OrderConfirmationModel:ViewModelBase
    {
        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public short NumMonthUse { get; set; }

        public short NumDayUse { get; set; }

        public byte ConcurrentLogin { get; set; }

        public int Price { get; set; }

        public int PriceVAT { get; set; }

        public int Total { get; set; }
    }
}