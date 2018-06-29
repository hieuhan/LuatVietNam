using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuatVN.AppCode.Attributes;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuat.Models
{
    public class AccountModel
    {
        public class LoginModel : ViewModelBase
        {
            private string _customerName;
            private string _customerPass;

            [Display(Name = "Tên đăng nhập")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage =
                "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái và số.")]
            public string CustomerName
            {
                get { return !string.IsNullOrEmpty(_customerName) ? _customerName.Sanitize() : string.Empty; }
                set { _customerName = value; }
            }

            [Display(Name = "Mật khẩu", Description = "Vui lòng nhập mật khẩu")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string CustomerPass
            {
                get { return !string.IsNullOrEmpty(_customerPass) ? _customerPass : string.Empty; ; }
                set { _customerPass = value; }
            }

            public string ReturnUrl { get; set; }

            [Display(Name = "Lưu mật khẩu")]
            public bool RememberMe { get; set; }
        }
        public class GoogleAccountMode
        {
            public string GoogleId { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string GivenName { get; set; }
            public string FamilyName { get; set; }
            public string Picture { get; set; }

            public string ReturnUrl { get; set; }
        }

        public class FacebookAccountMode
        {
            public string FacebookId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string ReturnUrl { get; set; }
        }

        public class RegisterHeaderModel : ViewModelBase
        {
            private string _customerName;
            private string _password;
            private string _confirmPassword;

            [Display(Name = "Tên truy cập")]
            [Required(ErrorMessage = "Vui lòng nhập {0} *")]
            [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage =
                "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái và số.")]
            //[Remote("IsCustomerNameExist", "Ajax")]
            public string CustomerName
            {
                get { return !string.IsNullOrEmpty(_customerName) ? _customerName.Sanitize() : string.Empty; }
                set { _customerName = value; }
            }

            [Display(Name = "Mật khẩu")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string Password
            {
                get { return !string.IsNullOrEmpty(_password) ? _password.Sanitize() : string.Empty; }
                set { _password = value; }
            }

            [Display(Name = "Mật khẩu xác nhận")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "{0} không đúng.")]
            public string ConfirmPassword
            {
                get { return !string.IsNullOrEmpty(_confirmPassword) ? _confirmPassword.Sanitize() : string.Empty; }
                set { _confirmPassword = value; }
            }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Vui lòng nhập {0} nhận văn bản (*)")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            //[Remote("IsCustomerMailExist", "Ajax")]
            public string EmailRegister { get; set; }

            [Display(Name = "Quy ước sử dụng của VanBanLuat")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
            public bool TermsAndConditions { get; set; }

        }

        public class RegisterModel : ViewModelBase
        {
            private string _customerName;
            private string _password;
            private string _confirmPassword;
            private string _organPhone;
            private string _fullName;
            private string _address;
            private string _organName;
            private List<Fields> _listFields;
            private short[] _fieldId;

            [Display(Name = "Tên truy cập")]
            [Required(ErrorMessage = "Vui lòng nhập {0} *")]
            [RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage =
                "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái và số.")]
            //[Remote("IsCustomerNameExist", "Ajax")]
            public string CustomerName
            {
                get { return !string.IsNullOrEmpty(_customerName) ? _customerName.Sanitize() : string.Empty; }
                set { _customerName = value; }
            }

            [Display(Name = "Mật khẩu")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string Password
            {
                get { return !string.IsNullOrEmpty(_password) ? _password.Sanitize() : string.Empty; }
                set { _password = value; }
            }

            [Display(Name = "Mật khẩu xác nhận")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "{0} không đúng.")]
            public string ConfirmPassword
            {
                get { return !string.IsNullOrEmpty(_confirmPassword) ? _confirmPassword.Sanitize() : string.Empty; }
                set { _confirmPassword = value; }
            }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            //[Remote("IsCustomerMailExist", "Ajax")]
            public string EmailRegister { get; set; }

            [Display(Name = "Số điện thoại")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
            public string Phone { get; set; }

            public string OrganPhone
            {
                get { return !string.IsNullOrEmpty(_organPhone) ? _organPhone.StripTags().Sanitize() : string.Empty; }
                set { _organPhone = value; }
            }

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

            public string OrganName
            {
                get { return !string.IsNullOrEmpty(_organName) ? _organName.StripTags().Sanitize() : string.Empty; }
                set { _organName = value; }
            }

            /// <summary>
            /// Dịch vụ quan tâm
            /// </summary>
            public short[] ServiceOfInterest { get; set; }
            public int Day { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }

            public byte GenderId { get; set; }

            [Required(ErrorMessage = "* Đăng ký để nhận được thông tin văn bản mới nhất về lĩnh vực bạn quan tâm.")]
            [MultiCheckBoxRequired(ErrorMessage = "Quý khách vui lòng chọn lĩnh vực văn bản quan tâm")]
            public short[] FieldId
            {
                get {
                    if (_fieldId != null && _fieldId.Length > 0)
                    {
                        _fieldId = _fieldId.Where(val => val != 0).ToArray();
                    }
                    return _fieldId;
                    ;
                }
                set { _fieldId = value; }
            }

            public List<Fields> ListFields
            {
                get
                {
                    return !_listFields.HasValue()
                        ? ListHelpers.FieldsGetListByDisplayType(Constants.FieldDisplayTypeIdVbpq)
                        : _listFields;}
                set
                {
                    _listFields = value;
                }
            }

            private List<Occupations> _listOccupations;
            private List<Categories> _listCategories;

            public List<Occupations> ListOccupations
            {
                get { return !_listOccupations.HasValue() ? ListHelpers.GetAllOccupations() : _listOccupations; }
            }

            public List<Categories> ListCategories
            {
                get { return !_listCategories.HasValue() ? ListHelpers.GetCategoryById(Constants.CateId_LoaiHinhDoanhNghiep) : _listCategories; }
            }
        }

        public class ChangePasswordModel : ViewModelBase
        {
            [Display(Name = "Mật khẩu cũ")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Display(Name = "Mật khẩu mới")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Mật khẩu xác nhận")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không đúng.")]
            public string ConfirmPassword { get; set; }
        }

        public class ForgotPasswordModel : ViewModelBase
        {
            [Display(Name = "Email")]
            [Required(ErrorMessage = "Vui lòng nhập {0} *")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            public string ForgotPasswordEmail { get; set; }

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

        public class ConfirmResetPasswordModel : ViewModelBase
        {
            private string _confirmCode;

            [Display(Name = "Mật khẩu")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Mật khẩu xác nhận")]
            [StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "{0} không đúng.")]
            public string ConfirmPassword { get; set; }

            public string ConfirmCode
            {
                get { return !string.IsNullOrEmpty(_confirmCode) ? _confirmCode.StripTags().Sanitize() : string.Empty; }
                set { _confirmCode = value; }
            }

            public int CustomerId { get; set; }
        }

        public class GuiYeuCauVB : ViewModelBase
        {
            private string _fullName;
            private string _questions;
            private string _title;
            public string Email { get; set; }
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
        public class GopYModel : ViewModelBase
        {
            private string _fullName;
            private string _questions;
            private string _title;
            public string Email { get; set; }

            public string FullName
            {
                get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : _fullName; }
                set { _fullName = value; }
            }
            public string CustommerName{get;set;}
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
        public class AccountProfileModel : ViewModelBase
        {
            private List<Categories> _listCategories;
            private List<Fields> _listFields;

            public List<CustomerFields> lCustomerFields { get; set; }
            public string CustomerName { get; set; }
            [Display(Name = "Họ và tên")]
            [Required(ErrorMessage = "{0} (*)")]
            public string FullName { get; set; }
            [Display(Name = "Ngày sinh")]
            [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
            public string DateOfBirth { get; set; }

            public byte GenderId { get; set; }
            [DataType(DataType.PhoneNumber)]
            [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không đúng !")]
            public string CustomerMobile { get; set; }
            
            [Display(Name = "Email")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Email không hợp lệ")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            // thong tin doanh nghiep
            public string OrganName { get; set; }

            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            public string OrganFax { get; set; }//Email doanh nghiep
            public string OrganAddress { get; set; }
            [DataType(DataType.PhoneNumber)]
            [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không đúng !")]
            public string OrganPhone { get; set; }
            public string Password { get; set; }
            public short OccupationId { get; set; }
            public short ApplicationId { get; set; }
            public short[] FieldId { get; set; }
            public short[] ServiceOfInterest { get; set; } // dich vu tra cuu quan tam

            //danh sach linh vuc doanh nghiep
            //public List<Occupations> ListOccupations
            //{
            //    get { return !_listOccupations.HasValue() ? ListHelpers.GetAllOccupations() : _listOccupations; }
            //}

            public List<Categories> ListCategories
            {
                get { return !_listCategories.HasValue() ? ListHelpers.GetCategoryById(Constants.CateId_LoaiHinhDoanhNghiep) : _listCategories; }
            }

            public List<Fields> ListFields
            {
                get { return !_listFields.HasValue() ? ListHelpers.FieldsGetListByDisplayTypeV2(Constants.FieldDisplayTypeIdVbpq) : _listFields; }
            }
            /// <summary>
            /// Dịch vụ quan tâm
            /// </summary>

            public int Day { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }

        }
    }
}