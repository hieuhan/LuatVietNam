using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Models.Account
{
    public class GopyModel:ViewModelBase
    {
        private string _fullName;
        private string _title;
        private string _content;

        public string FullName
        {
            get { return !string.IsNullOrEmpty(_fullName) ? _fullName.StripTags().Sanitize() : string.Empty; }
            set { _fullName = value; }
        }

        [Display(Name = "Title", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string Title
        {
            get { return !string.IsNullOrEmpty(_title) ? _title.StripTags().SanitizeWithoutSplash() : string.Empty; }
            set { _title = value; }
        }

        [Display(Name = "SuggestedContent", ResourceType = typeof(Resource))]
        [Required(ErrorMessage = "{0} (*)")]
        public string Content
        {
            get { return !string.IsNullOrEmpty(_content) ? _content.StripTags().SanitizeWithoutSplash() : string.Empty; } 
            set { _content = value; }
        }
    }
}