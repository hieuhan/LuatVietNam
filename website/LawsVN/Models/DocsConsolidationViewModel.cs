using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class DocsConsolidationViewModel: ViewModelBase
    {
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdVbhn;

        public DocsViewSearch mDocsViewSearch { get; set; }

        /// <summary>
        /// Danh sách văn bản hợp nhất
        /// </summary>
        public List<DocsView> ListDocsView { get; set; }

        /// <summary>
        /// Lĩnh vực theo loại hiển thị VBHN
        /// </summary>
        public List<FieldDisplays> ListFieldDisplays
        {
            get { return !_listFieldDisplays.HasValue() ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(_displayTypeId) : _listFieldDisplays; }
            set { _listFieldDisplays = value; }
        }

        /// <summary>
        /// Loại văn bản theo loại hiển thị VBHN
        /// </summary>
        public List<DocTypes> ListDocTypeDisplays
        {
            get { return !_listDocTypeDisplays.HasValue() ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(_displayTypeId) : _listDocTypeDisplays; }
            set { _listDocTypeDisplays = value; }
        }

        /// <summary>
        /// Cơ quan ban hành theo loại hiển thị VBHN
        /// </summary>
        public List<Organs> ListOrganDisplays
        {
            get { return !_listOrganDisplays.HasValue() ? DropDownListHelpers.GetListOrgansByDisplayTypeId(_displayTypeId) : _listOrganDisplays; }
            set { _listOrganDisplays = value; }
        }

        /// <summary>
        /// Năm ban hành
        /// </summary>
        public IEnumerable<int> ListIssueYears
        {
            get { return DropDownListHelpers.GetIssueYears(2013); }
        }

        /// <summary>
        /// Tin pháp luật hot
        /// </summary>
        public ArticlesViewCate mArticlesViewCate { get; set; }

        /// <summary>
        /// List tình trạng hiệu lực
        /// </summary>
        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        private List<FieldDisplays> _listFieldDisplays;
        private List<DocTypes> _listDocTypeDisplays;
        private List<Organs> _listOrganDisplays;
    }
}