using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVN.Models.Docs
{
    public class TCVNDocDetailModel : ViewModelBase
    {
        public DocsViewDetail m_DocsViewDetail { get; set; }
        public List<DocRelates> l_DocRelatesEffect { get; set; }
        public List<DocRelates> l_DocRelatesContent { get; set; }
        public List<RelateTypes> l_RelateTypes { get; set; }
        public PartialPaginationAjax PartialPaginationAjax { get; set; }
        public string DoctypeName { get; set; }
        public int TabId { get; set; }
        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

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