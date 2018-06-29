using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;
using LawsVN.Models.Docs;

namespace LawsVN.Models
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

        /// <summary>
        /// Danh sách văn bản theo lĩnh vực
        /// </summary>
        public List<DocsView> ListDocsView { get; set; }

        /// Danh sách văn bản xem nhiều
        public List<DocsView> ListDocsViewMostView { get; set; }

        /// <summary>
        /// Danh sách văn bản tiêu biểu theo lĩnh vực
        /// </summary>
        public List<DocsView> ListDocDisplaysByField { get;set; }

        public FieldDisplays mFieldDisplays { get; set; }

        /// <summary>
        /// partial phân trang
        /// </summary>
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        public List<EffectStatus> ListEffectStatus
        {
            get
            {
                return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus;
            }
            set { _listEffectStatus = value; }
        }

        public List<DocTypes> ListDocTypes
        {
            get { return !_listDocTypes.HasValue() ? DropDownListHelpers.DocTypes_GetList(DisplayTypeId) : _listDocTypes; }
            set { _listDocTypes = value; }
        }

        public List<Organs> ListOrgans
        {
            get { return !_listOrgans.HasValue() ? DropDownListHelpers.GetListOrgansByDisplayTypeId(DisplayTypeIdOrganId) : _listOrgans; }
            set { _listOrgans = value; }
        }

        private List<FieldDisplays> _listFieldDisplays;

        public List<FieldDisplays> ListFieldDisplays
        {
            get
            {
                return !_listFieldDisplays.HasValue()
                    ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(DisplayTypeId)
                    : _listFieldDisplays;
            }
            set
            {
                _listFieldDisplays = value;
            }
        }

        private List<DocTypes> _listDocTypes;
        private List<Organs> _listOrgans;
        private List<EffectStatus> _listEffectStatus;
    }
}