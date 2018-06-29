using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class LawerQuestionModel : ViewModelBase
    {
        private string _question;
        private string _fullName;

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

        [Display(Name = "Nội dung câu hỏi", Description = "Nội dung câu hỏi")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Question
        {
            get { return !string.IsNullOrEmpty(_question) ? _question.StripTags().Sanitize() : string.Empty; }
            set { _question = value; }
        }

        [Display(Name = "Mã xác nhận")]
        public string Captcha
        {
            get
            {
                return LawsVnSession["Captcha_LawerQuestions"] == null ? default(string) : LawsVnSession["Captcha_LawerQuestions"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_LawerQuestions"] = value;
            }
        }

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : string.Empty; }
            set { _fullName = value; }
        }

        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không đúng !")]
        public string Mobile { get; set; }

        [Display(Name = "Mã xác nhận")]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("Captcha", ErrorMessage = "{0} không đúng")]
        public string CaptchaCode { get; set; }

        public int LawerId { get; set; }

        public Faqs mFaqs { get; set; }
    }
}