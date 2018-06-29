using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class FeedbacksViewModel :ViewModelBase
    {
        static HttpSessionState AlawvnSession
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }

        [Display(Name = "Nội dung bình luận (*)")]
        [Required(ErrorMessage = "{0}")]
        public string Comment
        {
            get { return !string.IsNullOrEmpty(_comment) ? _comment.StripTags().Sanitize() : string.Empty; } 
            set { _comment = value; }
        }

        [Display(Name = "Địa chỉ Email (*)")]
        [Required(ErrorMessage = "{0} (*)")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không đúng !")]
        public string Email { get; set; }

        [Display(Name = "Họ và Tên (*)")]
        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : string.Empty; } 
            set { _fullName = value; }
        }

        [Display(Name = "Số điện thoại (*)")]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mã xác nhận")]
        public string Captcha
        {
            get
            {
                return AlawvnSession["Captcha_Feedbacks"] == null ? default(string) : AlawvnSession["Captcha_Feedbacks"].ToString();
            }
            set
            {
                AlawvnSession["Captcha_Feedbacks"] = value;
            }
        }

        [Display(Name = "Mã xác nhận")]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("Captcha", ErrorMessage = "{0} không đúng")]
        public string CaptchaCode { get; set; }

        private string _comment;
        private string _fullName;
    }
}