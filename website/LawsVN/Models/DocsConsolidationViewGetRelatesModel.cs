using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using LawsVN.Models.Docs;

namespace LawsVN.Models
{
    public class DocsConsolidationViewGetRelatesModel : ViewModelBase
    {
        // danh sách văn bản theo relatetypeid
        public DocsViewDetail m_DocRelates { get; set; }
        // chi tiết văn bản
        public DocsViewDetail m_DocViewDetail { get; set; }

        public PartialFullSearchDetailModel mFullSearchDetailModel { get; set; }

        // phân trang
        public string TextSearch { get; set; }
        public string Keyword { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public byte DocTypeId { get; set; }
        public short OrganId { get; set; }
        public int EffectStatusId { get; set; }
        public short FieldId { get; set; }
        public string FieldName { get; set; }
        public short SignerId { get; set; }
        public byte LanguageId { get; set; }
        public int RowCount { get; set; }
        public int Page { get; set; }
        public int RowAmount { get; set; }
        public int DocId { get; set; }
        public short RelateTypeId { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
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