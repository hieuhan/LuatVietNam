using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawsVN.App_GlobalResources;

namespace LawsVN.Models
{
    public class SearchModel : ViewModelBase
    {
        public DocsViewSearch mDocsViewSearch { get; set; }
        public List<DocsView> lDocsViewSuggest { get; set; }
        // box search
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
        public int RowCountSuggest { get; set; }
        public int Page { get; set; }
        public int RowAmount { get; set; }
        public int SearchOptions { get; set; }
        public int DocGroupId { get; set; }
        public string SignerName { get; set; }
        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

        public List<Fields> ListFields { get; set; }

        public List<DocTypes> ListDocTypes { get; set; }

        public List<Organs> ListOrgans { 
            get { return DropDownListHelpers.GetAllOrgans(); }
        }

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



        private string _keywords;

        [Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        public string Keywords
        {
            get { return _keywords.StripTags().Sanitize(); }
            set { _keywords = value; }
        }

        /// <summary>
        /// Danh sách văn bản gợi ý tìm kiếm
        /// </summary>
        public List<DocsView> ListDocsViewSuggest { get; set; }

        /// <summary>
        /// Kết quả tìm kiếm
        /// </summary>
        public DocsViewSearch DocsViewSearch { get; set; }

        /// <summary>
        /// Danh sách sắp xếp tìm kiếm theo Table
        /// </summary>
        public List<OrderByClauses> ListOrderByClauses { get; set; }

        /// <summary>
        /// Lĩnh vực theo fieldId đầu vào
        /// </summary>
        public Fields mFields { get; set; }

        /// <summary>
        /// Trạng thái hiệu lực theo EffectStatusId đầu vào
        /// </summary>
        public EffectStatus mEffectStatus { get; set; }

        /// <summary>
        /// Loại văn bản theo DocTypeId đầu vào
        /// </summary>
        public DocTypes mDocTypes { get; set; }

        /// <summary>
        /// Cơ quan ban hành theo OrganId đầu vào
        /// </summary>
        public Organs mOrgans { get; set; }

        /// <summary>
        /// Năm
        /// </summary>
        public YearView mYearView { get; set; }
    }
}