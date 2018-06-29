using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Models.Account
{
    public class TaxInvoiceModel
    {
        private string _companyName;
        private string _companyAddress;
        private string _companyTaxCode;
        private string _internalContent;

        [Display(Name = "CompanyName", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string CompanyName
        {
            get { return !string.IsNullOrEmpty(_companyName) ? _companyName.StripTags().Sanitize() : string.Empty; }
            set { _companyName = value; }
        }

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string CompanyAddress
        {
            get { return !string.IsNullOrEmpty(_companyAddress) ? _companyAddress.StripTags().Sanitize() : string.Empty; }
            set { _companyAddress = value; }
        }

        [Display(Name = "TaxCode", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string CompanyTaxCode
        {
            get { return !string.IsNullOrEmpty(_companyTaxCode) ? _companyTaxCode.StripTags().Sanitize() : string.Empty; }
            set { _companyTaxCode = value; }
        }

        [Display(Name = "BillContent", ResourceType = typeof(Resource))]
        //[StringLength(1000, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 10)]
        public string InternalContent
        {
            get { return !string.IsNullOrEmpty(_internalContent) ? _internalContent.StripTags().Sanitize() : string.Empty; }
            set { _internalContent = value; }
        }
    }
}