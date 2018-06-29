using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;

namespace VanBanLuatVN.Models
{
    public class ArticlesModel
    {
        public class CategoryArticlesModel
        {
            public CategoryArticles CategoryArticles { get; set; }
            public string ClassFix { get; set; }
        }

        public class ArticleSendLinkModel
        {
            [Display(Name = "Email")]
            [Required(ErrorMessage = "Quý khách vui lòng nhập {0} (*)")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            public string SendMail { get; set; }

            public string ArticleUrl { get; set; }
        }

        /// <summary>
        /// Gửi góp ý cho tin bài
        /// </summary>
        public class ArticleFeedback
        {
            private string _feedbackContent;

            [Display(Name = "Tên bài viết")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            public string Title { get; set; }

            [Display(Name = "Nội dung góp ý")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            public string FeedbackContent
            {
                get { return !string.IsNullOrEmpty(_feedbackContent) ? _feedbackContent.StripTags().Sanitize() : string.Empty; }
                set { _feedbackContent = value; }
            }
        }
    }
}