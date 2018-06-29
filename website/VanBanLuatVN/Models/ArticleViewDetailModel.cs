using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;

namespace VanBanLuat.Models
{
    public class ArticleViewDetailModel: ViewModelBase
    {
        public CategoryArticles ListTieuDiem { get; set; }
        public ArticleDetail mArticleDetail { get; set; }
    }
}