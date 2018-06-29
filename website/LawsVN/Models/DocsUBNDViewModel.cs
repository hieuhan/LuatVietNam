using System;
using System.Collections.Generic;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class DocsUbndViewModel : ViewModelBase
    {
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdUbnd;

        ///danh sách tin văn bản mới
        public List<ArticlesView> ListArticlesViewNewest { get; set; }

        public DocsViewSearch mDocsViewSearch { get; set; }

        public List<DocsView> ListVbUbndXemNhieu { get; set; }

        public List<DocsView> ListDocDisplaysVbTwLienQuan { get; set; }

        // linh vuc duoc quan tam cua khach hang
        //public List<CustomerFields> ListCustomerFields { set; get; }

        ///// <summary>
        ///// Năm ban hành
        ///// </summary>
        //public IEnumerable<int> ListIssueYears
        //{
        //    get { return DropDownListHelpers.GetIssueYears(); }
        //}

        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        public List<EffectStatus> ListEffectStatus
        {
            get
            {
               return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus;
            }
            set { _listEffectStatus = value; }
        }

        private List<DocTypes> _listDocTypes;
        public List<DocTypes> ListDocTypes
        {
            get { return !_listDocTypes.HasValue() ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(_displayTypeId) : _listDocTypes; }
            set { _listDocTypes = value; }
        }

        private List<FieldDisplays> _listFieldDisplays;

        public List<FieldDisplays> ListFieldDisplays
        {
            get
            {
                return !_listFieldDisplays.HasValue()
                    ? DropDownListHelpers.GetAllFieldDisplays_OrderBy(_displayTypeId)
                    : _listFieldDisplays;
            }
            set
            {
                _listFieldDisplays = value;
            }
        }

        private List<Provinces> _listProvinces;
        private List<EffectStatus> _listEffectStatus;

        public List<Provinces> ListProvinces
        {
            get { return !_listProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _listProvinces; }
            set { _listProvinces = value; }
        }
    }
}