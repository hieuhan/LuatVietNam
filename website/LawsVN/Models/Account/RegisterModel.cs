using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.AppCode.Attribute;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models.Account
{
    public class RegisterModel : ViewModelBase
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
        [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái và số.")]
        [Remote("IsCustomerNameExist", "Ajax")]
        public string CustomerName
        {
            get { return !string.IsNullOrEmpty(_customerName) ? _customerName.Sanitize() : string.Empty; } 
            set { _customerName = value; }
        }

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "StringLengthPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return !string.IsNullOrEmpty(_password) ? _password.Sanitize() : string.Empty; } 
            set { _password = value; }
        }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "StringLengthConfirmPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "ConfirmPasswordNotMatch")]
        public string ConfirmPassword
        {
            get { return !string.IsNullOrEmpty(_confirmPassword) ? _confirmPassword.Sanitize() : string.Empty; } 
            set { _confirmPassword = value; }
        }

        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsCustomerMailExist", "Ajax")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Phone { get; set; }

        public byte RegisterNewsletter { get; set; }

        public short NewsletterGroupId { get; set; }

        /// <summary>
        /// Dịch vụ quan tâm
        /// </summary>
        public short ServiceOfInterest { get; set; }

        //[MaxFileSize(2 * 1024 * 1024, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "MaxFileSizeUpload")]
        //[FileType("jpg,jpeg,png")]
        //public HttpPostedFileBase Avatar { get; set; }
        public string Avatar
        {
            get { return !string.IsNullOrEmpty(_avatar) ? _avatar : string.Empty; } 
            set { _avatar = value; }
        }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        [MultiCheckBoxRequired(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        public short[] FieldId { get; set; }

        public short ServiceId { get; set; }

        public short ServicePackageId { get; set; }

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : string.Empty; }
            set { _fullName = value; }
        }

        public string Address
        {
            get { return !string.IsNullOrEmpty(_address) ? _address.StripTags().Sanitize() : string.Empty; } 
            set { _address = value; }
        }

        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateOfBirth { get; set; }

        public byte GenderId { get; set; }

        public short ProvinceId { get; set; }

        public short OccupationId { get; set; }
        public short OrganOccupationId { get; set; }

        public string OrganName
        {
            get { return !string.IsNullOrEmpty(_organName) ? _organName.StripTags().Sanitize() : string.Empty; } 
            set { _organName = value; }
        }

        public string OrganPhone
        {
            get { return !string.IsNullOrEmpty(_organPhone) ? _organPhone.StripTags().Sanitize() : string.Empty; } 
            set { _organPhone = value; }
        }

        public string OrganAddress
        {
            get { return !string.IsNullOrEmpty(_organAddress) ? _organAddress.StripTags().Sanitize() : string.Empty; } 
            set { _organAddress = value; }
        }

        public string AccountNumber
        {
            get { return !string.IsNullOrEmpty(_accountNumber) ? _accountNumber.StripTags().Sanitize() : string.Empty; } 
            set { _accountNumber = value; }
        }

        public string OrganTaxCode
        {
            get { return !string.IsNullOrEmpty(_organTaxCode) ? _organTaxCode.StripTags().Sanitize() : string.Empty; } 
            set { _organTaxCode = value; }
        }

        [Display(Name = "Quy ước sử dụng của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        [Display(Name = "Mã an toàn")]
        public string RegisterCaptcha
        {
            get
            {
                return LawsVnSession["Captcha_RegisterAccount"] == null ? default(string) : LawsVnSession["Captcha_RegisterAccount"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_RegisterAccount"] = value;
            }
        }

        [Display(Name = "Mã an toàn")]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("RegisterCaptcha", ErrorMessage = "{0} không đúng.")]
        public string RegisterCaptchaCode { get; set; }

        #region ds dữ liệu từ điển
        public List<Occupations> ListOccupations
        {
            get { return DropDownListHelpers.GetAllOccupations(); }
        }
        private List<OrganOccupations> _listOrganOccupations;//Lĩnh vực hoạt động
        private List<FieldDisplays> _listFields;
        private string _organName;
        private string _organPhone;
        private string _organAddress;
        private string _accountNumber;
        private string _address;
        private string _fullName;
        private string _customerName;
        private string _password;
        private string _confirmPassword;
        private string _organTaxCode;
        private string _avatar;

        public List<OrganOccupations> ListOrganOccupations
        {
            get { return !_listOrganOccupations.HasValue() ? DropDownListHelpers.GetAllOrganOccupations() : _listOrganOccupations; }
            set { _listOrganOccupations = value; }
        }
        public List<FieldDisplays> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdVbpq) : _listFields; }
            set { _listFields = value; }
        }
        public List<Provinces> ListProvinces
        {
            get { return DropDownListHelpers.GetAllProvinces(); }
        }
        #endregion

        /// <summary>
        /// Nội dung quy ước bảo mật từ bài viết chi tiết
        /// </summary>
        public ArticlesViewDetail QuyUocBaoMat
        {
            get { return ArticlesViewHelpers.View_GetArticleDetail(Constants.QuyUocBaoMatArticleId, 0, 0, 0); }
        }

        public bool IsMobile { get; set; }
    }

    public class RegisterPersonalInterface : ViewModelBase
    {
        private List<FieldDisplays> _listFieldDisplaysVb;
        private List<FieldDisplays> _listFieldDisplaysTcvn;
        //private List<FieldDisplays> _listFieldDisplaysTthc;
        public string CustomerName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateOfBirth { get; set; }

        public byte GenderId { get; set; }

        [Display(Name = "Tỉnh/Thành phố")]
        [Range(1, short.MaxValue, ErrorMessage = "{0} *")]
        public short ProvinceId { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại (*)")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Phone { get; set; }

        public string Address { get; set; }

        public short OccupationId { get; set; }
        public short OrganOccupationId { get; set; }

        /// <summary>
        /// Dịch vụ quan tâm
        /// </summary>
        public short ServiceOfInterest { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        [MultiCheckBoxRequired(ErrorMessage = "Quý khách vui lòng chọn lĩnh vực văn bản Pháp quy")]
        public short[] FieldIdVb { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        [MultiCheckBoxRequired(ErrorMessage = "Quý khách vui lòng chọn lĩnh vực văn bản Tiêu chuẩn Việt Nam")]
        public short[] FieldIdTcvn { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        //[MultiCheckBoxRequired(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FieldRequired")]
        //public short[] FieldIdTthc { get; set; }

        public List<CustomerFields> ListCustomerFields { get; set; }

        #region ds dữ liệu từ điển
        public List<Occupations> ListOccupations
        {
            get { return DropDownListHelpers.GetAllOccupations(); }
        }

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

        /// <summary>
        /// Lĩnh vực thủ tục hành chính quan tâm
        /// </summary>
        //public List<FieldDisplays> ListFieldDisplaysTthc
        //{
        //    get { return !_listFieldDisplaysTthc.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdTthc) : _listFieldDisplaysTthc; }
        //    set { _listFieldDisplaysTthc = value; }
        //}

        public List<Provinces> ListProvinces
        {
            get { return DropDownListHelpers.GetAllProvinces(); }
        }

        public CategoriesView mCategoriesView
        {
            get
            {
                var mArticlesViewHome = Extensions.GetArticlesViewHome();
                return mArticlesViewHome.lCategoriesMain1.HasValue()
                     ? mArticlesViewHome.lCategoriesMain1[0]
                     : new CategoriesView();
            }
        }

        #endregion
    }

    public class RegisterFreeServiceModel : ViewModelBase
    {
        /// <summary>
        /// Chi tiết dịch vụ
        /// </summary>
        public Services mServices { get; set; }
    }

    public class RegisterServicePackagesModel : ViewModelBase
    {
        //private ArticlesViewDetail _articlePaymentRegulations;
        public short ServiceId { get; set; }

        public short CurrentServiceId { get; set; }
        
        /// <summary>
        /// Dich vu dang su dung
        /// </summary>
        public short ServiceIdUse { get; set; }

        public string ServiceName { get; set; }


        public short ServicePackageParentId { get; set; }

        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Bạn chưa chọn thời hạn thuê bao.")]
        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public string ServicePackageTime { get; set; }

        public string ServiceTimeRemain { get; set; }

        public short CurrentLogin { get; set; }

        public int Price { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public string FeeType { get; set; }

        public string ActionButton { get; set; }

        public int PriceVAT { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// Thông báo chuyển đổi số ngày khi gia hạn, nâng cấp gói dịch vụ
        /// </summary>
        public string MsgChangeService { get; set; }

        public bool TaxInvoice { get; set; }

        [Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        /// <summary>
        /// Chi tiết dịch vụ
        /// </summary>
        public Services mServices { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServiceId
        /// </summary>
        public List<ServicePackages> ListServicePackagesParent { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServicePackageId
        /// </summary>
        public List<ServicePackages> ListServicePackages { get; set; }

        /// <summary>
        /// Chi tiết gói dịch vụ cha
        /// </summary>
        public ServicePackages mServicePackagesParent { get; set; }

        /// <summary>
        /// Chi tiết gói dịch vụ
        /// </summary>
        public ServicePackages mServicePackages { get; set; }

        /// <summary>
        /// Mã khuyến mại
        /// </summary>
        public string PromotionCode { get; set; }

        /// <summary>
        /// Gói dịch vụ đang sử dụng
        /// </summary>
        public DataTable DtCustomerService { get; set; }

        public PartialPaginationAjax PartialPaginationAjax { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ hỗ trợ nâng cấp
        /// </summary>
        public List<short> ListServicesIdUpgradeAllowed { get; set; }

        /// <summary>
        /// Hỗ trợ nâng cấp dịch vụ ko
        /// </summary>
        public bool IsUpgradeAllowed { get; set; }

        //public ArticlesViewDetail ArticlePaymentRegulations
        //{
        //    get { return _articlePaymentRegulations ?? DropDownListHelpers.GetArticlesViewDetail(Constants.QuyUocBaoMatArticleId); }
        //    set { _articlePaymentRegulations = value; }
        //}
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

        public string MsgChangeService { get; set; }

        /// <summary>
        /// Hỗ trợ nâng cấp dịch vụ hay không
        /// </summary>
        public bool IsUpgradeAllowed { get; set; }
    }

    public class CustomerServicesNotice:ViewModelBase
    {
        public short ServiceId { get; set; }

        public byte IsRegistService { get; set; }

        public string ServiceName { get; set; }

        public string FeeType { get; set; }

        public string ActionButton { get; set; }

        public string Messages { get; set; }
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

    public class RegisterSuccessfulModel:ViewModelBase
    {
        public string RegisterMessages { get; set; }
    }
}