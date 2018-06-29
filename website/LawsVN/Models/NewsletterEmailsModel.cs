using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawsVN.App_GlobalResources;

namespace LawsVN.Models
{
    public class NewsletterEmailsModel
    {
        [Display(Name = "EnterYourEmail", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRequired")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRegularExpression")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessage = "Địa chỉ Email không đúng !")]
        public string Email { get; set; }
    }
}