using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;

namespace VanBanLuat.Models
{
    public class SharedViewModel
    {
        public class HeaderModel:ViewModelBase
        {
            public byte DocGroupId { get; set; }
            public List<MenuItems> ListMenuItems { get; set; }

            public List<MenuItems> ListMenuItemsMobile { get; set; }
            public List<Fields> ListFields { get; set; }
        }

        public class FieldOfSearchModel
        {
            public List<MenuItems> ListMenuItems { get; set; }

            public List<Fields> ListFields { get; set; }

            public byte DocGroupId { get; set; }
        }

        public class FooterModel
        {
            public List<MenuItems> ListMenuItems { get; set; }
        }

        public class DocumentAttribute
        {
            private bool _enableSaveDoc = true;
            public DocRelates DocRelates { get; set; }
            public Docs Docs { get; set; }

            /// <summary>
            /// Hiển thị link lưu văn bản quan tâm
            /// </summary>
            public bool EnableSaveDoc
            {
                get { return _enableSaveDoc; }
                set { _enableSaveDoc = value; }
            }
        }

        public class DocumentsByGetByDisplayType:ViewModelBase
        {
            public DocList DocList { get; set; }
        }

        public class NewsletterEmailsModel
        {
            [Display(Name = "Email")]
            [Required(ErrorMessage = "Vui lòng nhập {0} *")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            public string NewsletterEmail { get; set; }
        }
    }
}