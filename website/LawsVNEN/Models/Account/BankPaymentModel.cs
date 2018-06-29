using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;
using ICSoft.CMSViewLib;
using System.Web.SessionState;

namespace LawsVNEN.Models.Account
{
    public class BankPaymentModel
    {
        public short ServiceId { get; set; }
        public string ServiceName { get; set; }
        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        //public short NumMonthUse { get; set; }

        //public short NumDayUse { get; set; }

        //public byte ConcurrentLogin { get; set; }

        public int Price { get; set; }

        //public int PriceVAT { get; set; }

        public int Total { get; set; }

        public byte TransactionTypeId { get; set; }

        [Display(Name = "PaymentRegulationsOfVietnameseLaw", ResourceType = typeof(Resource))]
        [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseAgreeToThePaymentRegulationsOfVietnameseLaws")]
        public bool TermsAndConditions { get; set; }

        public bool TaxInvoice { get; set; }

        private string _companyName;
        private string _companyAddress;
        private string _companyTaxCode;
        private string _internalContent;

        [Display(Name = "CompanyName", ResourceType = typeof(Resource))]
        public string CompanyName
        {
            get { return !string.IsNullOrEmpty(_companyName) ? _companyName.StripTags().Sanitize() : string.Empty; }
            set { _companyName = value; }
        }

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string CompanyAddress
        {
            get { return !string.IsNullOrEmpty(_companyAddress) ? _companyAddress.StripTags().Sanitize() : string.Empty; }
            set { _companyAddress = value; }
        }

        [Display(Name = "TaxCode", ResourceType = typeof(Resource))]
        public string CompanyTaxCode
        {
            get { return !string.IsNullOrEmpty(_companyTaxCode) ? _companyTaxCode.StripTags().Sanitize() : string.Empty; }
            set { _companyTaxCode = value; }
        }

        [Display(Name = "BillContent", ResourceType = typeof(Resource))]
        public string InternalContent
        {
            get { return !string.IsNullOrEmpty(_internalContent) ? _internalContent.StripTags().Sanitize() : string.Empty; }
            set { _internalContent = value; }
        }

        //public int PaymentTransactionId { get; set; }

        //public int OrderId { get; set; }

        [Display(Name = "Bank", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseSelectBankPayment")]
        public string BankCode { get; set; }

        //public string PayGateUrl { get; set; }
    }

    /// <summary>
    /// Valid form đăng ký dịch vụ sử dụng tk ngân hàng
    /// </summary>
    public class PaymentServiceUsingBankAccountModel
    {
        private static HttpSessionState LawsVnSession
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new ApplicationException(Resource.NoHttpContext);
                }
                return HttpContext.Current.Session;
            }
        }

        private string _customerName;

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string CustomerName
        {
            get { return !string.IsNullOrEmpty(_customerName) ? _customerName.StripTags().Sanitize() : string.Empty; }
            set { _customerName = value; }
        }

        [Display(Name = "Mã xác nhận")]
        public string CheckCustomerNameCaptcha
        {
            get
            {
                return LawsVnSession["Captcha_PaymentService"] == null ? default(string) : LawsVnSession["Captcha_PaymentService"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_PaymentService"] = value;
            }
        }

        [Display(Name = "Mã xác nhận")]
        public string CheckCustomerNameCaptchaCode { get; set; }

        [Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        public bool TaxInvoice { get; set; }

        public short ServiceId { get; set; }

        public string ServiceName { get; set; }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public int PaymentTransactionIdSrc { get; set; }

        public double Total { get; set; }

        public double Price { get; set; }

        /// <summary>
        /// % khuyến mại
        /// </summary>
        public float PercentDecrease { get; set; }

        /// <summary>
        /// Số tiền khuyến mại
        /// </summary>
        public float Amount { get; set; }

        [Display(Name = "Mã giảm giá")]
        public string PromotionCodeBankAccount
        {
            get { return !string.IsNullOrEmpty(_promotionCodeBankAccount) ? _promotionCodeBankAccount : string.Empty; }
            set { _promotionCodeBankAccount = value; }
        }

        private string _companyName;
        private string _companyAddress;
        private string _companyTaxCode;
        private string _internalContent;
        private string _promotionCodeBankAccount;
        private string _serviceName;

        [Display(Name = "Tên công ty")]
        public string CompanyName
        {
            get { return !string.IsNullOrEmpty(_companyName) ? _companyName.StripTags().Sanitize() : string.Empty; }
            set { _companyName = value; }
        }

        [Display(Name = "Địa chỉ")]
        public string CompanyAddress
        {
            get { return !string.IsNullOrEmpty(_companyAddress) ? _companyAddress.StripTags().Sanitize() : string.Empty; }
            set { _companyAddress = value; }
        }

        [Display(Name = "Mã số thuế")]
        public string CompanyTaxCode
        {
            get { return !string.IsNullOrEmpty(_companyTaxCode) ? _companyTaxCode.StripTags().Sanitize() : string.Empty; }
            set { _companyTaxCode = value; }
        }

        [Display(Name = "Nội dung hóa đơn")]
        public string InternalContent
        {
            get { return !string.IsNullOrEmpty(_internalContent) ? _internalContent.StripTags().Sanitize() : string.Empty; }
            set { _internalContent = value; }
        }

        /// <summary>
        /// Kiểu giao dịch
        /// </summary>
        public byte TransactionTypeId { get; set; }

        public int PaymentTransactionId { get; set; }

        public int OrderId { get; set; }

        public string BankCode { get; set; }
        public string PayGateUrl { get; set; }
    }
}