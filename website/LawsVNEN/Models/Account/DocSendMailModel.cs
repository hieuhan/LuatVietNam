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
    public class DocSendMailModel : ViewModelBase
    {
        //private static HttpSessionState LawsVnSession
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //        {
        //            throw new ApplicationException(Resource.NoHttpContext);
        //        }
        //        return HttpContext.Current.Session;
        //    }
        //}

        [Required(ErrorMessage = "{0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Display(Name = "Mã xác nhận")]
        //public string DocSendMailCaptcha
        //{
        //    get
        //    {
        //        return LawsVnSession["Captcha_DocSendMail"] == null ? default(string) : LawsVnSession["Captcha_DocSendMail"].ToString();
        //    }
        //    set
        //    {
        //        LawsVnSession["Captcha_DocSendMail"] = value;
        //    }
        //}

        //[Display(Name = "Mã xác nhận")]
        //[Required(ErrorMessage = "{0} (*)")]
        //[Compare("DocSendMailCaptcha", ErrorMessage = "{0} không đúng.")]
        //public string DocSendMailCaptchaCode { get; set; }

        public string DocUrl { get; set; }
    }
}