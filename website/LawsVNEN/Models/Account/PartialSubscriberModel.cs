using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.Library;

namespace LawsVNEN.Models.Account
{
    public class PartialSubscriberModel
    {
        public byte LanguageId { get; set; }
        private string _priceText;
        private string _priceVatText;
        private string _totalText;
        private string _concurrentLoginText;
        private string _expiryDate;

        public short ServicePackageParentId { get; set; }

        public int NumMonthUse { get; set; }

        public int NumDayUse { get; set; }

        public int ConcurrentLogin { get; set; }

        public int Price { get; set; }

        public int PriceVAT { get; set; }

        public int Total { get; set; }

        public string PriceText
        {
            get { return Price > 0 ? (LawsVnLanguages.GetCurrentLanguageId() == 1 ? string.Format("{0:#,###} VNĐ", Price) : string.Format("VND {0:#,###}", Price)) : string.Empty; }
            set { _priceText = value; }
        }

        public string PriceVatText
        {
            get { return PriceVAT > 0 ? (LawsVnLanguages.GetCurrentLanguageId() == 1 ? string.Format("{0:#,###} VNĐ", PriceVAT) : string.Format("VND {0:#,###}", PriceVAT)) : string.Empty; }
            set { _priceVatText = value; }
        }

        public string TotalText
        {
            get { return Total > 0 ? (LawsVnLanguages.GetCurrentLanguageId() == 1 ? string.Format("{0:#,###} VNĐ", Total) : string.Format("VND {0:#,###}", Total)) : string.Empty; }
            set { _totalText = value; }
        }

        public string ConcurrentLoginText
        {
            get { return string.Format(ConcurrentLogin > 1 ? Resource.PersonsUsingAtTheSameTime : Resource.PersonUsingAtTheSameTime, ConcurrentLogin); }
            set { _concurrentLoginText = value; }
        }

        public string ExpiryDate
        {
            get { return DateTime.Now.AddMonths(NumMonthUse).AddDays(NumDayUse).toString(); }
            set { _expiryDate = value; }
        }

        public ServicePackages ServicePackage { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServicePackageId
        /// </summary>
        public List<ServicePackages> ListServicePackages { get; set; }

        public Services Service { get; set; }
    }
}