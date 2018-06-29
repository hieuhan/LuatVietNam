using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VanBanLuat.Models;
using VanBanLuat.Librarys;

namespace VanBanLuatVN.Models
{
    public class FeedbacksViewModel : ViewModelBase
    {
        [Display(Name = "Nội dung bình luận (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string Comment
        {
            get { return !string.IsNullOrEmpty(_comment) ? _comment.Sanitize() : string.Empty; }
            set { _comment = value; }
        }

        [Display(Name = "Địa chỉ Email (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không đúng !")]
        public string Email { get; set; }

        [Display(Name = "Họ và Tên")]
        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.Sanitize() : string.Empty; }
            set { _fullName = value; }
        }

        [Display(Name = "Số điện thoại")]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string PhoneNumber { get; set; }
        private string _comment;
        private string _fullName;
    }
}