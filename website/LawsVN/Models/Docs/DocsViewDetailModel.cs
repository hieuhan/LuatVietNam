using ICSoft.CMSViewLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class DocsViewDetailModel : ViewModelBase
    {
        public byte DocGroupId { get; set; }

        public short FieldId { get; set; }

        public byte LanguageId { get; set; }

        public short RelateTypeId { get; set; }

        public string DisplayPosition { get; set; }

        public int CountByRelateTypeId { get; set; }

        public RelateTypes RelateTypes { get; set; }

        /// <summary>
        /// Văn bản chưa có nội dung tiếng Anh
        /// </summary>
        public bool NotDocEng { get; set; }

        public int RowCountDocEnglish { get; set; }

        public DocsViewDetail mDocsViewDetail { get; set; }

        public string DocContentEng { get; set; }

        /// <summary>
        /// Danh sách văn bản liên quan theo nhóm liên kết Hiệu lực
        /// </summary>
        public IEnumerable<DocRelates> ListDocRelatesEffect { get; set; }

        /// <summary>
        /// Danh sách văn bản liên quan theo nhóm liên kết Nội dung
        /// </summary>
        public IEnumerable<DocRelates> ListDocRelatesContent { get; set; }

        public List<DocRelates> ListDocRelates { get; set; }

        public List<ICSoft.LawDocsLib.Docs> ListDocsOtherLanguages { get; set; }

        /// <summary>
        /// Tất cả ds loại liên quan theo nhóm VB
        /// </summary>
        public List<RelateTypes> ListRelateTypesByGroupId { get; set; }

        public List<RelateTypes> ListRelateTypesTop1 { get; set; }

        public List<RelateTypes> ListRelateTypesTop2 { get; set; }

        public List<RelateTypes> ListRelateTypesLeft { get; set; }

        public List<RelateTypes> ListRelateTypesRight { get; set; }

        public List<RelateTypes> ListRelateTypesBottom { get; set; }

        public List<RelateTypes> ListRelateTypesOtherLanguages { get; set; }

        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        public IEnumerable<Fields> ListDocFields { get; set; }

        private List<EffectStatus> _listEffectStatus;
        private List<Fields> _listFields;

        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }

        public List<Fields> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFields() : _listFields; }
            set { _listFields = value; }
        }
    }
}