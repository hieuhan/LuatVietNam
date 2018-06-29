using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.LawDocsLib;
using LawsVN.AppCode.Attribute;
using LawsVN.App_GlobalResources;
using LawsVN.Library;
using Languages = ICSoft.CMSLib.Languages;

namespace LawsVN.Models.Docs
{
    public class PartialDocSearchModel//:ViewModelBase
    {
        private string _keywords;

        [Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        public string Keywords
        {
            get { return !string.IsNullOrEmpty(_keywords) ? _keywords.StripTags().SanitizeWithoutSplash() : string.Empty; }
            set { _keywords = value; }
        }

        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateFrom { get; set; }

        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
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

        public int Page { get; set; }

        public short SignerId { get; set; }

        public string SignerName { get; set; }

        public short OrganId { get; set; }

        public byte DocGroupId { get; set; }

        public byte DocTypeId { get; set; }

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
                    default: _displayTypeId = 0;break;
                }
                return _displayTypeId;
            }

            set
            {
                _displayTypeId = value;
            }
        }

        public short FieldDisplayTypeId { get; set; }
        public short DocTypeDisplayTypeId { get; set; }
        private int _pSize;
        public int pSize
        {
            get { return _pSize == 0 ? Constants.RowAmount20 : _pSize; }
            set { _pSize = value; }
        }

        /// <summary>
        /// Tìm chính xác cụm từ
        /// </summary>
        public byte SearchExact { get; set; }

        /// <summary>
        /// Extract tìm kiếm nâng cao khi chọn điều kiện lọc
        /// </summary>
        public bool ShowAdvancedSearchPannel
        {
            get { return !string.IsNullOrEmpty(DateFrom) || !string.IsNullOrEmpty(DateTo) || DocTypeId > 0 || OrganId > 0 || EffectStatusId > 0 || FieldId > 0 || LanguageId > 0 || SignerId > 0; } 
            set { _showAdvancedSearchPannel = value; }
        }

        /// <summary>
        /// Tùy chọn tìm kiếm: Tất cả - Tiêu đề - Số hiệu văn bản
        /// </summary>
        public byte? SearchOptions { get; set; }

        #region ds du lieu tu dien

        private List<Fields> _listFields;
        public List<Fields> ListFields
        {
            set { _listFields = value; }
            get
            {
                return !_listFields.HasValue() ? DisplayTypeId == 0 ? DropDownListHelpers.GetAllFields() : Fields.Static_GetListbyDisplayTypeId(DisplayTypeId) : _listFields;
            }
        }

        private List<DocTypes> _listDocTypes;
        public List<DocTypes> ListDocTypes
        {
            set { _listDocTypes = value; }
            get
            {
                return DisplayTypeId > 0
                    ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(DisplayTypeId)
                    : DropDownListHelpers.DocTypes_GetList(DisplayTypeId);
            }

        }

        private List<Organs> _listOrgans;

        public List<Organs> ListOrgans
        {
            set { _listOrgans = value; }
            get
            {
                return DisplayTypeId > 0
                    ? DropDownListHelpers.GetListOrgansByDisplayTypeId(DisplayTypeId)
                    : DropDownListHelpers.GetAllOrgans();
            }

        }

        private List<Languages> _listLanguages;

        public List<ICSoft.CMSLib.Languages> ListLanguages
        {
            get { return !_listLanguages.HasValue() ? DropDownListHelpers.GetAllLanguages() : _listLanguages; }
            set { _listLanguages = value; }
        }

        private List<EffectStatus> _listEffectStatus;

        public List<EffectStatus> ListEffectStatus
        {
            get
            {
               return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus;
            }
            set { _listEffectStatus = value; }
        }


        private List<DocGroups> _listDocGroups;
        private bool _showAdvancedSearchPannel;

        public List<DocGroups> ListDocGroups
        {
            get { return !_listDocGroups.HasValue() ? DropDownListHelpers.GetAllDocGroups() : _listDocGroups; }
            set { _listDocGroups = value; }
        }

        #endregion
    }
}