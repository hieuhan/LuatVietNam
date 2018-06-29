using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;
using ICSoft.CMSViewLib;

namespace LawsVN.Models
{
    public class DocsSearchModel
    {
        private string _keywords;

        [Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        public string Keywords
        {
            get {return _keywords.StripTags().Sanitize();}
            set { _keywords = value; }
        }

        public byte DocType { get; set; }

        [Display(Name = "DateFrom")]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "DateTo")]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Trạng thái hiệu lực
        /// </summary>
        public byte EffectStatusId { get; set; }

        /// <summary>
        /// Lĩnh vực
        /// </summary>
        public short FieldId { get; set; }

        public byte LanguageId { get; set; }

        public short SignerId { get; set; }

        public string SignerName { get; set; }

        public short OrganId { get; set; }

        public byte DocGroupId { get; set; }

        public byte DocTypeId { get; set; }

        //private byte _displayTypeId = 62;

        public short DisplayTypeId
        {
            get
            {
                if (DocGroupId == Constants.DocGroupIdVbpq)
                {
                    _displayTypeId = Constants.FieldDisplayTypeIdVbpq;
                }
                else if (DocGroupId == Constants.DocGroupIdTcvn)
                {
                    _displayTypeId = Constants.TcvnDisplaytypeId;
                }
                return _displayTypeId;
            }

            set
            {
                _displayTypeId = value;
            }
        }

        /// <summary>
        /// Tìm chính xác cụm từ
        /// </summary>
        public bool FindTheExactPhrase { get; set; }

        /// <summary>
        /// Tùy chọn tìm kiếm: Tất cả - Tiêu đề - Số hiệu văn bản
        /// </summary>
        public byte? SearchOptions { get; set; }

        public string ControllerSearchName { get; set; }

        public string ActionSearchName { get; set; }

        public List<Fields> ListFieldsSearch { get; set; }

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
        public List<OrderByClauses> ListOrderByClauses{ get;set; }

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

        public PartialPaginationAjax  mPartialPaginationAjax { get; set; }

        #region ds du lieu tu dien
        private List<Fields> _listFields;
        public List<Fields> ListFields
        {
            set { _listFields = value; }
            get
            {
                return _displayTypeId == 0 ? DropDownListHelpers.GetAllFields() : Fields.Static_GetListbyDisplayTypeId(_displayTypeId);
            }
        }
        private List<DocTypes> _listDocTypes;
        public List<DocTypes> ListDocTypes
        {
            set { _listDocTypes = value; }
            get
            { return !_listDocTypes.HasValue() ? DropDownListHelpers.DocTypes_GetList(_displayTypeId) : _listDocTypes; }
            
        }
        private List<Organs> _listOrgans;
        public List<Organs> ListOrgans
        {
            set { _listOrgans = value; }
            get { return !_listOrgans.HasValue() ? Organs.Static_GetListbyDisplayTypeId(DisplayTypeId) : _listOrgans; }
            
        }

        public List<ICSoft.CMSLib.Languages> ListLanguages
        {
            get { return DropDownListHelpers.GetAllLanguages(); }
        }

        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

        public List<DocGroups> ListDocGroups
        {
            get { return DropDownListHelpers.GetAllDocGroups(); }
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
        private short _displayTypeId;

        #endregion

    }
}