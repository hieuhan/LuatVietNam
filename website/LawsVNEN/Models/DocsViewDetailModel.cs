using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.Library;

namespace LawsVNEN.Models
{
    public class DocsViewDetailModel:ViewModelBase
    {
        public byte DocGroupId { get; set; }

        public short FieldId { get; set; }

        public byte LanguageId { get; set; }

        public short RelateTypeId { get; set; }

        public int CountByRelateTypeId { get; set; }

        public RelateTypes RelateTypes { get; set; }

        /// <summary>
        /// Văn bản chưa có nội dung tiếng Anh
        /// </summary>
        public bool NotDocEng { get; set; }

        public DocsViewDetail mDocsViewDetail { get; set; }

        public DocsViewDetail mDocsViewDetailVi { get; set; }

        /// <summary>
        /// Danh sách văn bản liên quan theo nhóm liên kết Hiệu lực
        /// </summary>
        public IEnumerable<DocRelates> ListDocRelatesEffect { get; set; }

        /// <summary>
        /// Danh sách văn bản liên quan theo nhóm liên kết Nội dung
        /// </summary>
        public IEnumerable<DocRelates> ListDocRelatesContent { get; set; }

        public List<DocRelates> ListDocRelates { get; set; }

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

        private List<EffectStatus> _listEffectStatus;

        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }
    }
}