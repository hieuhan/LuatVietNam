using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class DocSearchModel:ViewModelBase
    {
        public string Keywords { get; set; }

        public string DateFrom { get; set; }
       
        public string DateTo { get; set; }

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

        public int Year { get; set; }

        /// <summary>
        /// Tìm chính xác cụm từ
        /// </summary>
        public byte SearchExact { get; set; }

        /// <summary>
        /// Tùy chọn tìm kiếm: Tất cả - Tiêu đề - Số hiệu văn bản
        /// </summary>
        public byte? SearchOptions { get; set; }

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
                switch (DocGroupId)
                {
                    case 1: _displayTypeId = Constants.FieldDisplayTypeIdVbpq;
                        break;
                    case 2: _displayTypeId = Constants.FieldDisplayTypeIdUbnd;
                        break;
                    case 3: _displayTypeId = Constants.OrganTieuChuanVn;
                        break;
                    case 5: _displayTypeId = Constants.FieldDisplayTypeIdVbhn;
                        break;
                    case 6: _displayTypeId = Constants.FieldDisplayTypeIdCongVan;
                        break;
                    default: _displayTypeId = 0; break;
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
                return !_listFields.HasValue() ? DisplayTypeId == 0 ? DropDownListHelpers.GetAllFields() : Fields.Static_GetListbyDisplayTypeId(DisplayTypeId) : _listFields;
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
            {
                return !_listDocTypes.HasValue() ? DisplayTypeId > 0
                    ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(DisplayTypeId)
                    : DropDownListHelpers.DocTypes_GetList(DisplayTypeId) : _listDocTypes;
            }

        }

    }
}