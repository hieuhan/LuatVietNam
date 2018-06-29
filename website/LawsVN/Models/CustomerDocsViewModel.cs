using System.Collections.Generic;
using ICSoft.CMSLibEstate;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using System.Data;

namespace LawsVN.Models
{
    public class CustomerDocsViewModel : ViewModelBase
    {
        public List<DocsView> lMyDocs { set; get; }
        /// <summary>
        /// Danh sách văn bản văn bản pháp luật, tiêu chuẩn, hợp nhất
        /// </summary>
        public List<DocsView> ListDocsViewFirst { get; set; }
        public List<DocsView> ListDocsViewrelated { get; set; }

        /// <summary>
        /// Partial phân trang pháp luật, tiêu chuẩn, hợp nhất
        /// </summary>
        public PartialPaginationAjax PartialPaginationAjaxFirst { get; set; }
        public PartialPaginationAjax PartialPaginationAjaxSecond { get; set; }
        public List<ICSoft.CMSViewLib.Messages> lMessages { set; get; }
        public List<ICSoft.CMSViewLib.Messages> lMessagesSave { set; get; }
        public int RowCount { set; get; }
        public int Page { set; get; }

        public List<DocTypes> ListDocTypes {get ;set; }

        // lĩnh vực theo lĩnh vực khách hàng quan tâm

        public List<CustomerFields> lCustomerFields { set; get; }
        public DataTable DataTableField { set; get; }
        //tinh thanh khach hang dang ky
        public List<CustomerProvinces> lCustomerProvinces { set; get; }

        // Cơ quan ban hành
        public List<Organs> ListOrgans
        {
            get { return DropDownListHelpers.GetAllOrgans(); }
        }
        // tình trạng hiệu lực

        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }

        //Loại văn bản
        public List<DocTypeDisplays> ListDocTypeDisplays
        {
            get { return DropDownListHelpers.GetAllDocTypeDisplays("DocTypeDisplays", 0); }
        }
        //Linh vuc
        public List<Fields> ListFields { set; get; }
        //tin tuc
        public ArticlesViewMaster mArticlesViewMaster { get; set; }
        //van ban UBND
        public List<DocsView> lDocsView_UBND { get; set; }
        //van ban TCVN
        public List<DocsView> lDocsView_TCVN { get; set; }

        public List<ArticlesView> ListArticlesByCustomerId { get; set; }

    }
}