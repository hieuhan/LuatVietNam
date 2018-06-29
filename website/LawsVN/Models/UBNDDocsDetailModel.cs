using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class UBNDDocsDetailModel : ViewModelBase
    {
        public DocsViewDetail m_DocsViewDetail { get; set; }
        public PartialPaginationAjax PartialPaginationAjax { get; set; }
        public string DoctypeName { get; set; }
        public int TabId { get; set; }

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