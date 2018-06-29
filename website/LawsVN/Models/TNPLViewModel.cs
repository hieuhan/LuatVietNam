using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class TNPLViewModel : ViewModelBase
    {
        // Tin xem nhiều
        public ArticlesViewCate l_ArticlesMostView { get; set; }
        // văn bản mới
        public List<ArticlesView> l_ArticlesNewView { get; set; }
        //danh sach tu dien
        public List<LawTermins> l_LawTermins { get; set; }
        public PartialPaginationAjax PartialPaginationAjax { get; set; }
        public List<LawTerminGroups> ListLawTerminGroups
        {
            get { return DropDownListHelpers.GetAllLawTerminGroups(); }
        }
        public byte LawTerminGroupId { get; set; }

        public string Tername { get; set; }
    }
}