using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using ICSoft.CMSLib;

namespace LawsVN.Models
{
    public class LawerViewDetailModel : ViewModelBase
    {
        public LawersViewDetail mLawersViewDetail { get; set; }
        public LawersView mLawersView { get; set; }
        public PartialPaginationAjax PartialPaginationAjax { get; set; }
        public List<Faqs> lFaqs { get; set; }

        public string LawerKeywords { get; set; }

        public Provinces LawProvinces { get; set; }

        private List<Fields> _listFieldsLawer;
        private List<Provinces> _listAllProvinces;

        // danh sách lọc
        public List<Fields> ListFieldsLawer
        {
            get { return !_listFieldsLawer.HasValue() ? DropDownListHelpers.GetAllFields() : _listFieldsLawer; }
            set { _listFieldsLawer = value; }
        }

        //tinh thanh pho
        public List<Provinces> ListAllProvinces
        {
            get { return !_listAllProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _listAllProvinces; }
            set { _listAllProvinces = value; }
        }
    }
}