using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.Library;
using LawsVNEN.AppCode;

namespace LawsVNEN.Models.Account
{
    public class RegisterModel : ViewModelBase
    {
        private string _organName;
        private string _organPhone;
        private string _organFax;
        private string _organAddress;
        private string _accountNumber;
        private string _fullName;
        private string _address;

        public static HttpSessionState LawsVNENSession
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
        //CREAT AN ACCOUNT
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "UserNameRequired")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        [Remote("IsCustomerNameExist", "Ajax")]
        public string CustomerName { get; set; }

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

        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsCustomerMailExist", "Ajax")]
        public string Email { get; set; }

        public byte RegisterNewsletter { get; set; }

        public short ServiceOfInterest { get; set; }

        //YOUR INFORMATIONS
        [Display(Name = "FullName", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName : string.Empty; } 
            set { _fullName = value; }
        }

        [Display(Name = "Sex", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public byte GenderId { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Invalid selected date.")]
        public string DateOfBirth { get; set; }

        public short OccupationId { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string Address
        {
            get { return !string.IsNullOrEmpty(_address) ? _address : string.Empty; } 
            set { _address = value; }
        }

        [Display(Name = "ProvinceCity", ResourceType = typeof(Resource))]
        //[Range(1, short.MaxValue, ErrorMessage = "{0} *")]
        public short ProvinceId { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$",ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string Phone { get; set; }

        //YOUR COMPANY INFORMATIONS
        public string OrganName
        {
            get { return !string.IsNullOrEmpty(_organName) ? _organName : string.Empty; } 
            set { _organName = value; }
        }

        public string OrganPhone
        {
            get { return !string.IsNullOrEmpty(_organPhone) ? _organPhone : string.Empty; } 
            set { _organPhone = value; }
        }

        public string OrganFax
        {
            get { return !string.IsNullOrEmpty(_organFax) ? _organFax : string.Empty; } 
            set { _organFax = value; }
        }

        public string OrganAddress
        {
            get { return !string.IsNullOrEmpty(_organAddress) ? _organAddress : string.Empty; } 
            set { _organAddress = value; }
        }

        public string AccountNumber
        {
            get { return !string.IsNullOrEmpty(_accountNumber) ? _accountNumber : string.Empty; } 
            set { _accountNumber = value; }
        }

        public short NewsletterGroupId { get; set; }

        [Display(Name = "Use convention of Vietnamese law")]
        [Range(typeof(bool), "true", "true",ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ClickIAgree")]
        public bool TermsAndConditions { get; set; }
        [Display(Name = "Safety code")]
        public string RegisterCaptcha
        {
            get
            {
                return LawsVNENSession["Captcha_RegisterAccount"] == null ? default(string) : LawsVNENSession["Captcha_RegisterAccount"].ToString();
            }
            set
            {
                LawsVNENSession["Captcha_RegisterAccount"] = value;
            }
        }

        [Display(Name = "Safetycode", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("RegisterCaptcha", ErrorMessageResourceName = "Incorrect", ErrorMessageResourceType = typeof(Resource))]
        public string RegisterCaptchaCode { get; set; }

        /// <summary>
        /// Nội dung quy ước bảo mật từ bài viết chi tiết
        /// </summary>
        public ArticlesViewDetail QuyUocBaoMat
        {
            get { return ArticlesViewHelpers.View_GetArticleDetail(LawsVnLanguages.GetCurrentLanguageId() == 1 ? Constants.QuyUocBaoMatArticleIdVNI : Constants.QuyUocBaoMatArticleIdEN, 0, 0, 0); }
        }
        // list danh sach
        public List<Occupations> ListOccupations
        {
            get { return DropDownListHelpers.GetAllOccupations(); }
        }
        public List<Provinces> ListProvinces
        {
            get { return DropDownListHelpers.GetAllProvinces(); }
        }
    }
    /// <summary>
    /// Class chứa thông tin dịch vụ khách hàng đang sử dụng
    /// </summary>
    public class DtCustomerServices
    {
        public short ServiceId { get; set; }

        public byte IsRegistService { get; set; }

        public string ServiceName { get; set; }

        public string ServiceStatus { get; set; }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public string ServicePackageTime { get; set; }

        public short CurrentLogin { get; set; }

        public int Price { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime RenewTime { get; set; }

        public string FeeType { get; set; }

        public string ActionButton { get; set; }

        public string Messages { get; set; }

        public string ServiceTimeRemain { get; set; }

        /// <summary>
        /// Hỗ trợ nâng cấp dịch vụ hay không
        /// </summary>
        public bool IsUpgradeAllowed { get; set; }

        public List<short> ListServicesIdUpgradeAllowed { get; set; }
    }

    public class NewUserActiveModel : ViewModelBase
    {
        private string _confirmCode;

        public string ConfirmCode
        {
            get { return !string.IsNullOrEmpty(_confirmCode) ? _confirmCode.StripTags().Sanitize() : _confirmCode; }
            set { _confirmCode = value; }
        }

        public int CustomerId { get; set; }

        public bool ActiveStatus { get; set; }

        public string ActiveMessage { get; set; }
    }
}