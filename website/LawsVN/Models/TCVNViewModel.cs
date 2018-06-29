using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawsVN.Models.Docs;

namespace LawsVN.Models
{
    public class TCVNViewModel : ViewModelBase
    {
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdTcvn;

        public string TcvnTitle { get; set; }

        // TCVN mới cập nhật
        public List<DocsView> ListDocsNewest { get; set; }

        /// <summary>
        /// Tin pháp luật hot
        /// </summary>
        public ArticlesViewCate mArticlesViewCate { get; set; }

        // DS TCVN 
        public List<DocsView> ListDocsView { get; set; }

        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }

        private List<FieldDisplays> _listFieldDisplays;
        private List<Organs> _listOrganDisplays;
        private List<EffectStatus> _listEffectStatus;

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
        //public List<DocTypes> ListDocTypeDisplays
        //{
        //    get { return !_listDocTypeDisplays.HasValue() ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(_displayTypeId) : _listDocTypeDisplays; }
        //    set { _listDocTypeDisplays = value; }
        //}
        //private List<Fields> _listFields;
        //public List<Fields> ListFields
        //{
        //    set { _listFields = value; }
        //    get
        //    {
        //        return !_listFields.HasValue() ? DropDownListHelpers.GetAllFields() : _listFields;
        //    }
        //}

        /// <summary>
        /// Cơ quan ban hành theo loại hiển thị VBHN
        /// </summary>
        public List<Organs> ListOrganDisplays
        {
            get { return !_listOrganDisplays.HasValue() ? DropDownListHelpers.GetListOrgansByDisplayTypeId(_displayTypeId) : _listOrganDisplays; }
            set { _listOrganDisplays = value; }
        }
        
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

    }
}