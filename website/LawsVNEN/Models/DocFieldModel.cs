using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.Library;
using LawsVNEN.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models
{
    public class DocFieldModel : ViewModelBase
    {
        public short FieldId { get; set; }

        public byte EffectStatusId { get; set; }

        public short OrganId { get; set; }

        public byte DocTypeId { get; set; }

        public short DisplayTypeId { get; set; }

        public short DisplayTypeIdDocTypeId { get; set; }

        public short DisplayTypeIdOrganId { get; set; }

        public FieldDisplays mFieldDisplays { get; set; }
        /// <summary>
        /// Danh sách văn bản theo lĩnh vực
        /// </summary>
        public List<DocsView> ListDocsView { get; set; }
        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

        /// <summary>
        /// partial phân trang
        /// </summary>
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        private List<FieldDisplays> _listFieldDisplays;
        public List<FieldDisplays> ListFieldDisplays
        {
            get
            {
                return !_listFieldDisplays.HasValue()
                    ? DropDownListHelpers.GetAllFieldDisplays(DisplayTypeId)
                    : _listFieldDisplays;
            }
            set
            {
                _listFieldDisplays = value;
            }
        }
    }
}