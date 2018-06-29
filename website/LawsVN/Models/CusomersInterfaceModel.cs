using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using System.Data;


namespace LawsVN.Models
{
    public class CusomersInterfaceModel : ViewModelBase
    {
        private List<Provinces> _lProvinces;
        private List<EffectStatus> _listEffectStatus;
        private List<DocTypes> _listDocTypesUbnd;
        private List<DocTypes> _listDocTypes;
        private List<Organs> _listOrgans ;
        private List<Fields> _listFields;

        private const short DisplayTypeId = 5;

        // Tin vắn
        //public CategoriesView m_CategoriesView_Brief { get; set; }
        /// <summary>
        /// Lĩnh vực theo vị trí hiển thị
        /// </summary>
        public List<Fields> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFields() : _listFields; }
            set { _listFields = value; }
        }

        public List<Fields> ListFieldsQuanTamTcvn { get; set; }

        public List<Fields> ListFieldsQuanTam { get; set; }

        /// <summary>
        /// Danh sách hiệu lực
        /// </summary>
        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus;  }
            set { _listEffectStatus = value; }
        }


        public DataTable DataTableField { set; get; }

        // danh sách Loại văn bản
        public List<DocTypes> ListDocTypes
        {
            get { return !_listDocTypes.HasValue() ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(Constants.DocTypeIdDisplayTypeIdVbpq) : _listDocTypes;  }
            set { _listDocTypes = value; }
        }

        // danh sách Loại văn bản UBND
        public List<DocTypes> ListDocTypesUbnd
        {
            get { return !_listDocTypesUbnd.HasValue() ? DropDownListHelpers.GetListDocTypesbyDisplayTypeId(Constants.FieldDisplayTypeIdUbnd) : _listDocTypesUbnd;  }
            set { _listDocTypesUbnd = value; }
        } 

        public List<Provinces> lProvinces
        {
            get { return !_lProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _lProvinces;  }
            set { _lProvinces = value; }
        }

        public List<Provinces> ListProvinces { get; set; }

        public List<Organs> ListOrgans
        {
            get { return! _listOrgans.HasValue() ? DropDownListHelpers.GetAllOrgans() : _listOrgans ; }
            set { _listOrgans  = value; }
        }

        //public List<ArticlesView> l_Docs_Newest { get; set; }

        public List<DocsView> lDocsView_UBND { get; set; }// danh sách VBUBND
        public List<DocsView> lDocsView_TCVN { get; set; }
        //public DocsViewSearch l_VBUBNDLeft { get; set; }
        public List<DocsView> lDocsView_TrungUongLienQuan { get; set; }
        //public List<DocsView> l_VBUBNDXemNhieu { get; set; }
        //public List<DocsView> l_VBProvince { get; set; }
        // danh sach tinh thanh
        public List<DocsView> lDocsView_Newest { get; set; }
        // linh vuc duoc quan tam cua khach hang
        public List<CustomerFields> lCustomerFields { set; get; }

        public List<CustomerProvinces> lCustomerProvinces { set; get; }// dia phuong quan tam
        //public List<OrderByClauses> LOrderByClauses { set; get; }
        /// <summary>
        /// Năm ban hành
        /// </summary>
        //public IEnumerable<int> listIssueYears
        //{
        //    get { return DropDownListHelpers.GetIssueYears(); }
        //}
        //public int CountProvinces { set; get; }
        //public int CountYear { set; get; }
        //public int CountCustomerFields { set; get; }
        //public string TextSearch { get; set; }
        //public string Keyword { get; set; }
        //public string DateFrom { get; set; }
        //public string DateTo { get; set; }
        //public byte DocTypeId { get; set; }
        public byte DocGroupId { get; set; }
        public short OrganId { get; set; }
        //public int EffectStatusId { get; set; }
        public short FieldId { get; set; }
        //public string FieldName { get; set; }
        //public short SignerId { get; set; }
        public short ProvinceId { get; set; }
        //public byte LanguageId { get; set; }
        //public int RowCount { get; set; }
        //public int Page { get; set; }
        //public int RowAmount { get; set; }
        public int CustomerId { get; set; }
        public PartialPaginationAjax PartialPaginationAjaxFirst { get; set; }
        public PartialPaginationAjax PartialPaginationAjaxSecond { get; set; }
    }
}