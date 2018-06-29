using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models.Account
{
    public class AccountProfileModel : ViewModelBase
    {

        private List<Provinces> _listProvinces;
        public CustomersViewDetail mCustomersViewDetail { get; set; }

        public int CountVbpl { get; set; }
        public int CountVbhn { get; set; }
        public int CountTcvn { get; set; }

        public List<Provinces> ListProvinces
        {
            get { return !_listProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _listProvinces; } 
            set { _listProvinces = value; }
        }

        public string CustomerName { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateOfBirth { get; set; }

        public byte GenderId { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string CustomerMobile { get; set; }

        //[Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsCustomerMailExist", "Ajax")]
        public string Email { get; set; }

        public string Avatar { get; set; }

        public short ProvinceId { get; set; }

        public string ProvinceDesc { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// Số tháng sử dụng dịch vụ còn lại
        /// </summary>
        public double MonthRemain { get; set; }

        /// <summary>
        /// Số ngày sử dụng dịch vụ còn lại
        /// </summary>
        public double DayRemain { get; set; }


        /// <summary>
        /// Xem || Sửa
        /// </summary>
        public string Mode { get; set; }

        public string GetAvatar()
        {
            var retVal = this.Avatar;
            return !string.IsNullOrEmpty(retVal)
                ? (!this.Avatar.StartsWith("http://")
                    ? string.Concat(CmsConstants.WEBSITE_IMAGEDOMAIN, retVal)
                    : retVal)
                : Constants.NoAvatar;
        }

        public string GetAvatarMobile()
        {
            return GetAvatar().Replace("/Original/", "/Mobile/");
        }
    }

    /// <summary>
    /// Thông tin doanh nghiệp
    /// </summary>
    public class BusinessInformation : ViewModelBase
    {
        public string Mode { get; set; }
        public string OrganName { get; set; }
        public string OrganAddress { get; set; }
        public string OrganPhone { get; set; }
        public string OrganTaxCode { get; set; }
        public string AccountNumber { get; set; }
    }

    public class LawsCustomerFields : ViewModelBase
    {
        [MinLength(2, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseSelectAtLeastOneField")]
        public short[] FieldId { get; set; }

        public List<CustomerFields> ListCustomerFields { get; set; }

        private List<FieldDisplays> _listFields;

        public List<FieldDisplays> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdVbpq) : _listFields; }
            set { _listFields = value; }
        }

        //public List<Fields> ListFields {get { return DropDownListHelpers.GetAllFields(); }}
    }

    public class LawsCustomerFieldsV2 : ViewModelBase
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        [MultiCheckBoxRequired(ErrorMessage = "Quý khách vui lòng chọn lĩnh vực văn bản Pháp quy")]
        public short[] FieldIdVb { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        [MultiCheckBoxRequired(ErrorMessage = "Quý khách vui lòng chọn lĩnh vực văn bản Tiêu chuẩn Việt Nam")]
        public short[] FieldIdTcvn { get; set; }
        public List<CustomerFields> ListCustomerFields { get; set; }

        private List<FieldDisplays> _listFieldDisplaysVb;
        private List<FieldDisplays> _listFieldDisplaysTcvn;

        /// <summary>
        /// Lĩnh vực văn bản quan tâm
        /// </summary>
        public List<FieldDisplays> ListFieldDisplaysVb
        {
            get { return !_listFieldDisplaysVb.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdVbpq) : _listFieldDisplaysVb; }
            set { _listFieldDisplaysVb = value; }
        }

        /// <summary>
        /// Lĩnh vực tiêu chuẩn việt nam quan tâm
        /// </summary>
        public List<FieldDisplays> ListFieldDisplaysTcvn
        {
            get { return !_listFieldDisplaysTcvn.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdTcvn) : _listFieldDisplaysTcvn; }
            set { _listFieldDisplaysTcvn = value; }
        }
    }

    public class LawsCustomerProvinces : ViewModelBase
    {
        [MinLength(2, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseSelectAtLeastOneProvices")]
        public short[] provinceId { get; set; }

        public List<CustomerProvinces> ListCustomerProvinces { get; set; }

        private List<Provinces> _listProvinces;

        public List<Provinces> ListProvinces
        {
            get { return !_listProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _listProvinces; }
            set { _listProvinces = value; }
        }
    }

    public class ChangePasswordModel : ViewModelBase
    {
        [Display(Name = "CurrentPassword", ResourceType = typeof(Resource))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(Resource))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthConfirmPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordNotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class CustomerNameCheckModel : ViewModelBase
    {
        private string _customerName;

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "UserNameRequired")]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string CustomerName
        {
            get { return !string.IsNullOrEmpty(_customerName) ? _customerName.StripTags().Sanitize() : string.Empty; }
            set { _customerName = value; }
        }
    }

    /// /// <summary>
    /// Thanh toán đơn hàng qua ngân hàng
    /// </summary>
    public class ConfirmPaymentServiceUsingBankAccountModel : ViewModelBase
    {
        public short ServiceId { get; set; }

        public string ServiceName
        {
            get { return !string.IsNullOrEmpty(_serviceName) ? _serviceName : string.Empty; }
            set { _serviceName = value; }
        }

        public short ServicePackageId { get; set; }

        public int PaymentTransactionId { get; set; }

        public int OrderId { get; set; }

        private string _promotionCodeBankAccount;
        private string _serviceName;

        public string PromotionCodeBankAccount
        {
            get { return !string.IsNullOrEmpty(_promotionCodeBankAccount) ? _promotionCodeBankAccount : string.Empty; }
            set { _promotionCodeBankAccount = value; }
        }

        public string ButtonType { get; set; }

        public byte TransactionTypeId { get; set; }
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

        public string ServiceName
        {
            get { return !string.IsNullOrEmpty(_serviceName)? _serviceName.StripTags().Sanitize() : string.Empty; }
            set { _serviceName = value; }
        }

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

    public class BankPaymentModel
    {
        [Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        public bool TaxInvoice { get; set; }

        public short ServiceId { get; set; }

        /// <summary>
        /// ServiceId dùng khi bấm chọn lại 
        /// </summary>
        public short ServiceIdFixed { get; set; }

        public string ServiceName
        {
            get { return !string.IsNullOrEmpty(_serviceName) ? _serviceName.StripTags().Sanitize() : string.Empty; }
            set { _serviceName = value; }
        }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

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

        [Display(Name = "Ngân hàng")]
        [Required(ErrorMessage = "Quý khách vui lòng chọn {0} sử dụng để thanh toán")]
        public string BankCode { get; set; }

        public string PayGateUrl { get; set; }
    }

    public class PaymentServiceUsingScratchCardModel : ViewModelBase
    {
        public static HttpSessionState LawsVnSession
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

        public double Total { get; set; }

        public double Price { get; set; }

        [Display(Name = "Mã thẻ cào")]
        [Required(ErrorMessage = "{0} (*)")]
        public string ScratchCardCode
        {
            get { return !string.IsNullOrEmpty(_scratchCardCode) ? _scratchCardCode.StripTags().Sanitize() : string.Empty; }
            set { _scratchCardCode = value; }
        }

        [Display(Name = "Mã xác nhận")]
        public string PaymentServiceUsingScratchCardCaptcha
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
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("PaymentServiceUsingScratchCardCaptcha", ErrorMessage = "{0} không đúng.")]
        public string PaymentServiceUsingScratchCardCode { get; set; }

        [Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditionsScratchCard { get; set; }

        public bool TaxInvoiceScratchCard { get; set; }

        public short ServiceId { get; set; }

        public string ServiceName
        {
            get { return !string.IsNullOrEmpty(_serviceName) ? _serviceName :string.Empty; }
            set {_serviceName = value; }
        }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        [Display(Name = "Mã giảm giá")]
        public string PromotionCodeScratchCard
        {
            get { return !string.IsNullOrEmpty(_promotionCodeScratchCard) ? _promotionCodeScratchCard.StripTags().Sanitize() : string.Empty; }
            set { _promotionCodeScratchCard = value; }
        }

        private string _companyName;
        private string _companyAddress;
        private string _companyTaxCode;
        private string _internalContent;
        private string _scratchCardCode;
        private string _serviceName;
        private string _promotionCodeScratchCard;

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
    }

    public class PaymentMethodsOnlineModel : ViewModelBase
    {
        //[Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        //public bool TermsAndConditions { get; set; }

        //public bool TaxInvoice { get; set; }

        public short ServiceId { get; set; }

        public string ServiceName
        {
            get { return !string.IsNullOrEmpty(_serviceName) ? _serviceName.StripTags().Sanitize() : string.Empty; }
            set { _serviceName = value; }
        }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

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

        public ArticlesViewDetail PaymentRegulations { get; set; }
    }

    public class PromotionCodeCheckModel : ViewModelBase
    {
        private string _promotionCode;

        [Display(Name = "Mã giảm giá")]
        [Required(ErrorMessage = "Quý khách vui lòng nhập {0}")]
        public string PromotionCode
        {
            get { return !string.IsNullOrEmpty(_promotionCode) ? _promotionCode.Sanitize().StripTags() : string.Empty; }
            set { _promotionCode = value; }
        }

        [Display(Name = "Gói dịch vụ")]
        [Range(1,Int16.MaxValue,ErrorMessage = "Quý khách vui lòng chọn {0}")]
        public short ServicePackageParentId { get; set; }

        [Display(Name = "Thời hạn thuê bao")]
        [Range(1, Int16.MaxValue, ErrorMessage = "Quý khách vui lòng chọn {0}")]
        public short ServicePackageId { get; set; }

        /// <summary>
        /// Giá gói dịch vụ
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Thuế VAT
        /// </summary>
        public int PriceVAT { get; set; }
    }

    public class ForgotPasswordModel:ViewModelBase
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

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "UserNameRequired")]
        //[Remote("IsCustomerNameExist", "Ajax")]
        //[RegularExpression("^[a-z0-9]*$", ErrorMessage =
        //    "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string CustomerName { get; set; }

        //[Required(ErrorMessage = "{0} (*)")]
        //[Display(Name = "Email")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Mã xác nhận")]
        public string ForgotPasswordCaptcha
        {
            get
            {
                return LawsVnSession["Captcha_ForgotPassword"] == null ? default(string) : LawsVnSession["Captcha_ForgotPassword"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_ForgotPassword"] = value;
            }
        }

        [Display(Name = "Mã xác nhận")]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("ForgotPasswordCaptcha", ErrorMessage = "{0} không đúng.")]
        public string ForgotPasswordCaptchaCode { get; set; }

    }

    public class ConfirmResetPasswordModel : ViewModelBase
    {
        private string _confirmCode;

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthConfirmPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordNotMatch")]
        public string ConfirmPassword { get; set; }

        public string ConfirmCode
        {
            get { return !string.IsNullOrEmpty(_confirmCode) ? _confirmCode.StripTags().Sanitize() : string.Empty; } 
            set { _confirmCode = value; }
        }

        public int CustomerId { get; set; }
    }

    public class SendQuestionsModel : ViewModelBase
    {
        private string _fullName;
        private string _questions;
        private string _mobile;

        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : string.Empty; }
            set { _fullName = value; }
        }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile
        {
            get { return !string.IsNullOrEmpty(_mobile) ? _mobile.StripTags().Sanitize() : string.Empty; } 
            set { _mobile = value; }
        }

        [Display(Name = "Nội dung câu hỏi")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Questions
        {
            get { return !string.IsNullOrEmpty(_questions) ? _questions.StripTags().Sanitize() : string.Empty; }
            set { _questions = value; }
        }
    }

    public class GopYModel : ViewModelBase
    {
        private string _fullName;
        private string _questions;
        private string _title;

        //private static HttpSessionState LawsVnSession
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //        {
        //            throw new ApplicationException(Resource.NoHttpContext);
        //        }
        //        return HttpContext.Current.Session;
        //    }
        //}

        //[Required(ErrorMessage = "{0} (*)")]
        //[Display(Name = "Email")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Display(Name = "Mã xác nhận")]
        //public string GopYCaptcha
        //{
        //    get
        //    {
        //        return LawsVnSession["Captcha_GopY"] == null ? default(string) : LawsVnSession["Captcha_GopY"].ToString();
        //    }
        //    set
        //    {
        //        LawsVnSession["Captcha_GopY"] = value;
        //    }
        //}

        //[Display(Name = "Mã xác nhận")]
        //[Required(ErrorMessage = "{0} (*)")]
        //[Compare("GopYCaptcha", ErrorMessage = "{0} không đúng.")]
        //public string GopYCaptchaCode { get; set; }

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : _fullName; }
            set { _fullName = value; }
        }

        //[Display(Name = "Số điện thoại")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không đúng !")]
        public string Mobile { get; set; }

        [Display(Name = "Tiều đề góp ý")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Title
        {
            get { return !string.IsNullOrEmpty(_title) ? _title.StripTags().SanitizeWithoutSplash() : _title; }
            set { _title = value; }
        }

        [Display(Name = "Nội dung góp ý")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Content
        {
            get { return !string.IsNullOrEmpty(_questions) ? _questions.StripTags().Sanitize() : _questions; }
            set { _questions = value; }
        }
    }

    public class DocSendMailModel : ViewModelBase
    {
        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Quý khách vui lòng nhập địa chỉ Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string DocUrl { get; set; }
    }

    public class LawServices
    {
        private readonly int _vatTax;
        private string _priceText;
        private int _total;
        private int _priceVat;
        private DateTime _expiryDate;
        private string _priceVatText;
        private string _totalText;
        private string _expiryDateText;
        private string _promotionCode;
        private string _numberOfUsers;
        private int _price;
        private short _numMonthUse;
        private short _numDayUse;
        private short _servicePackageId;
        private string _servicePackageDesc;

        public LawServices(int vatTax)
        {
            _vatTax = vatTax;

        }

        public LawServices(int vatTax, ServicePackages servicePackage)
        {
            _vatTax = vatTax;
            ServicePackages = servicePackage;
        }

        public short ServicePackageId
        {
            get { return ServicePackages != null ? ServicePackages.ServicePackageId :  _servicePackageId; } 
            set { _servicePackageId = value; }
        }

        public string ServicePackageDesc
        {
            get { return ServicePackages != null && !string.IsNullOrEmpty(ServicePackages.ServicePackageDesc) ? ServicePackages.ServicePackageDesc : string.Empty; } 
            set { _servicePackageDesc = value; }
        }

        public int Price
        {
            get { return ServicePackages != null ? ServicePackages.Price : _price; } 
            set { _price = value; }
        }


        public string PriceText
        {
            get { return Price > 0 ? string.Format("{0:#,###} {1}", Price, CmsConstants.CURRENCY_VND) : string.Empty; }
            set { _priceText = value; }
        }

        public int PriceVat
        {
            get
            {
                return Price > 0 && _vatTax > 0 ? Price*_vatTax/100 : 0;
            }
            set { _priceVat = value; }
        }

        public string PriceVatText
        {
            get { return PriceVat > 0 ? string.Format("{0:#,###} {1}", PriceVat, CmsConstants.CURRENCY_VND) : string.Empty; }   
            set { _priceVatText = value; }
        }

        public int Total
        {
            get { return Price + PriceVat; }
            set { _total = value; }
        }

        public string TotalText
        {
            get { return Total > 0 ? string.Format("{0:#,###} {1}", Total, CmsConstants.CURRENCY_VND) : string.Empty; } 
            set { _totalText = value; }
        }

        public short NumMonthUse
        {
            get { return ServicePackages != null ? ServicePackages.NumMonthUse :  _numMonthUse; } 
            set { _numMonthUse = value; }
        }

        public short NumDayUse
        {
            get { return ServicePackages != null ? ServicePackages.NumDayUse : _numDayUse; } 
            set { _numDayUse = value; }
        }

        public string NumberOfUsers
        {
            get { return ServicePackages != null && ServicePackages.ConcurrentLogin > 0 ? (ServicePackages.ServiceId != Constants.ServiceIdTraCuuTiengAnh && ServicePackages.ConcurrentLogin <= 3 ? string.Format("1 - {0} người",ServicePackages.ConcurrentLogin) : string.Format("{0} người",ServicePackages.ConcurrentLogin) ) : _numberOfUsers; } 
            set { _numberOfUsers = value; }
        }

        public DateTime ExpiryDate
        {
            get { return NumMonthUse > 0 || NumDayUse > 0 ? DateTime.Now.AddMonths(NumMonthUse).AddDays(NumDayUse) : DateTime.MinValue; }
            set { _expiryDate = value; }
        }

        public string ExpiryDateText
        {
            get { return ExpiryDate.toString(); } 
            set { _expiryDateText = value; }
        }

        /// <summary>
        /// Mã khuyến mại
        /// </summary>
        public string PromotionCode
        {
            get { return !string.IsNullOrEmpty(_promotionCode) ? _promotionCode : string.Empty; } 
            set { _promotionCode = value; }
        }

        public List<ServicePackages> ListServicePackagesParent { get; set; }
        public List<ServicePackages> ListServicePackages { get; set; }
        public ServicePackages ServicePackages { get; set; }
        public Services Services { get; set; }
    }

    public class UnsubscribeModel:ViewModelBase
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

        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessage = "Vui lòng nhập hộp thư của bạn")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Mã an toàn")]
        public string UnsubcribeCaptcha
        {
            get
            {
                return LawsVnSession["Captcha_Unsubcribe"] == null ? string.Empty : LawsVnSession["Captcha_Unsubcribe"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_Unsubcribe"] = value;
            }
        }

        [Display(Name = "Mã an toàn")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [Compare("UnsubcribeCaptcha", ErrorMessage = "{0} không đúng.")]
        public string UnsubcribeCaptchaCode { get; set; }
    }

}