using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Models.Account
{
    // login- forgotpass

    public class WidgetUserModel
    {
        public int CountMessages { get; set; }
        public int CountCustomerDocs { get; set; }
        public int CountThongBaoHieuLuc { get; set; }
        public bool IsUserVip { get; set; }
        public byte IsRegistService { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        //[RegularExpression("^[a-z0-9]*$", ErrorMessage = "{0} not whitespace, only include letters and numbers.")]
        public string CustomerName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        //[StringLength(100, ErrorMessage = "{0} includes from {2} characters or more.", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string CustomerPass { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "SavePassword", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
        public string EndTime { get; set; }
        
    }

    public class ForgotPasswordModel : ViewModelBase
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "UserNameRequired")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string CustomerName { get; set; }

        //[Required(ErrorMessage = "{0} (*)")]
        //[Display(Name = "Email")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
    }
    public class ChangePasswordModel : ViewModelBase
    {
        [Display(Name = "CurrentPassword", ResourceType = typeof(Resource))]
        //[StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "StringLengthPassword", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(Resource))]
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
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}