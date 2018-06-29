using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class ArticleViewDetailModel : ViewModelBase
    {
        public ArticlesViewDetail m_ArticlesViewDetail { get; set; }
        public ArticlesViewCate m_ArticlesViewCate { get; set; }
        public List<ArticlesView> l_ArticlesTinVanChinhSachMoi { get; set; }
        public ArticlesViewCate mArticlesMostView { set; get; }
        public List<ArticlesView> ListTieuDiem { get; set; }
        public List<ArticlesView> l_Tags { get; set; }
        //public ArticlesViewHome mArticlesViewHome { get; set; }
        public ArticlesViewMaster mArticlesViewMaster { get; set; }
        public CategoriesView mSharedCorner
        {
            get
            {
                if (_mSharedCorner != null)
                    return _mSharedCorner;
                else
                {
                    ArticlesViewHome m_ArticlesViewHome = ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, 1, 1, 0, Constants.RowAmount3, 0);
                    if (m_ArticlesViewHome.lCategoriesMain3.Count > 1)
                        return m_ArticlesViewHome.lCategoriesMain3[1];
                    else
                        return new CategoriesView();
                }
            }

            set
            {
                _mSharedCorner = value;
            }
        }

        private CategoriesView _mSharedCorner;
    }
}