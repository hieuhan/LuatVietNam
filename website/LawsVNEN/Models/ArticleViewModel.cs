using ICSoft.CMSViewLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models
{
    public class ArticleViewModel : ViewModelBase
    {
        public ArticlesViewDetail m_ArticlesViewDetail { get; set; }
        public CategoriesView m_CategoriesView { get; set; }
        public ArticlesViewMaster m_ArticlesViewMaster { get; set; }
        public List<CategoriesView> l_CategoriesView { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
        public List<ArticlesView> ListArticlesView { get; set; }
        public string LinkDownLoad { get; set; }
    }
}