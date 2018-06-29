using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.Library;

namespace LawsVNEN.Models.Account
{
    public class AccountProfileModel : ViewModelBase
    {
        private string _fullName;
        private string _address;
        private string _customerMobile;
        private string _email;

        public CustomersViewDetail mCustomersViewDetail { get; set; }
        // tinh thanh pho
        public List<Provinces> ListProvinces
        {
            get
            {
                return DropDownListHelpers.GetAllProvinces();
            }
        }
        //cong viec
        public List<Occupations> ListOccupations
        {
            get { return DropDownListHelpers.GetAllOccupations(); }
        }

        public string CustomerName { get; set; }

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.Sanitize().StripTags() : string.Empty; } 
            set { _fullName = value; }
        }

        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateOfBirth { get; set; }

        public byte GenderId { get; set; }

        public string Address
        {
            get { return !string.IsNullOrEmpty(_address) ? _address.Sanitize().StripTags() : string.Empty; } 
            set { _address = value; }
        }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không đúng.")]
        public string CustomerMobile
        {
            get { return !string.IsNullOrEmpty(_customerMobile) ? _customerMobile.Sanitize().StripTags() : string.Empty; } 
            set { _customerMobile = value; }
        }

        public string Avatar { get; set; }

        public string Email
        {
            get { return !string.IsNullOrEmpty(_email) ? _email.Sanitize().StripTags() : string.Empty; } 
            set { _email = value; }
        }

        public short ProvinceId { get; set; }
        public short OccupationId { get; set; }
        public string Mode { get; set; }

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
        public string OrganFax { get; set; }
        public string AccountNumber { get; set; }
    }
    // đăng kí nhận bản tin
    public class SubscribeToNewsletters: ViewModelBase
    {
        public string Mode { get; set; }
        public byte RegisterNewsletter { get; set; }
        public int CustomerId { get; set; }
        public byte NewsletterGroupId { get; set; }
        public string Email { get; set; }
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
            get { return !string.IsNullOrEmpty(_confirmCode) ? _confirmCode.StripTags().Sanitize() : _confirmCode; }
            set { _confirmCode = value; }
        }

        public int CustomerId { get; set; }
    }
}