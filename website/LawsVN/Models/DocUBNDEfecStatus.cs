using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class DocUBNDEfecStatus : ViewModelBase
    {
        public DocsViewDetail mydoc { get; set; }
        public List<DocsView> l_DocUBNDEfecStatus { get; set; }

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