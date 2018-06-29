using ICSoft.CMSViewLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models
{
    public class HomeViewModel : ViewModelBase
    {
        public List<DocsView> ListDocsView { get; set; }
        public PartialPaginationAjax PartialPaginationAjax{ get; set; }
    }
}
