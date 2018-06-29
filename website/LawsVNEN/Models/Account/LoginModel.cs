using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models.Account
{
    public class LoginModel : ViewModelBase
    {
        [Display(Name = "Tên đăng nhập", Description = "Tên đăng nhập")]
        [Required(ErrorMessage = "{0} (*)")]
        //[RegularExpression("^[a-z0-9]*$", ErrorMessage = "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string CustomerName { get; set; }

        [Display(Name = "Mật khẩu", Description = "Mật khẩu")]
        //[StringLength(100, ErrorMessage = "{0} bao gồm từ {2} ký tự trở lên.", MinimumLength = 6)]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.Password)]
        public string CustomerPass { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Lưu mật khẩu")]
        public bool RememberMe { get; set; }
    }
}