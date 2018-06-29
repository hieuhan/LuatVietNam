using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.AppCode;
using LawsVNEN.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models
{
    public class DocSearchModel : ViewModelBase
    {
        //private string _keywords;

        //[Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        public string Keywords { get; set; }

        public DateTime? DateFrom { get; set; }

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

        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

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
        public List<YearView> ListYearView { get; set; }

        public PartialDocSearchModel mPartialDocSearchModel { get; set; }

        private short _displayTypeId;
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

        private List<Fields> _listFields;
        public List<Fields> ListFields
        {
            set { _listFields = value; }
            get
            {
                return !_listFields.HasValue() ? _displayTypeId == 0 ? DropDownListHelpers.GetAllFieldsByLanguage() : Fields.Static_GetListbyDisplayTypeId(_displayTypeId) : _listFields;
            }
        }

        private List<EffectStatus> _listEffectStatus;
        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }

        private List<DocTypes> _listDocTypes;
        public List<DocTypes> ListDocTypes
        {
            set { _listDocTypes = value; }
            get
            { return !_listDocTypes.HasValue() ? DropDownListHelpers.DocTypes_GetList(_displayTypeId, LawsVnLanguages.GetCurrentLanguageId()) : _listDocTypes; }

        }

    }
}