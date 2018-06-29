using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;

namespace VanBanLuat.Models
{
    public class NewViewModel : ViewModelBase
    {
        public List<CategoryArticles> lCategoryArticles { get; set; }
        public CategoryArticles mCategoryArticles { get; set; }

        public CategoryArticles categoryArticlesBieuMau { get; set; }
        public List<Categories> lCategories { get; set; }
        public Categories mCategories { get; set; }
        //thong tin tag
        public TagArticles mTagArticles { get; set; }
        public int CategoryId { get; set; }

    }
}