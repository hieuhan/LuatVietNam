using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawsVN.Library;

namespace LawsVN.Models.Account
{
    public class LoginModel: ViewModelBase
    {
        private string _customerName;
        private string _customerPass;

        [Display(Name = "Tên đăng nhập", Description = "Tên đăng nhập")]
        [Required(ErrorMessage = "{0} (*)")]
        //[RegularExpression("^[a-zA-Z0-9_-]+$", ErrorMessage =
        //    "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái và số.")]
        public string CustomerName
        {
            get { return !string.IsNullOrEmpty(_customerName) ? _customerName.Sanitize() : string.Empty; } 
            set { _customerName = value; }
        }

        [Display(Name = "Mật khẩu", Description = "Mật khẩu")]
        //[StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 4)]
        [Required(ErrorMessage = "{0} (*)")]
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
}