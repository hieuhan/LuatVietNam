using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVN.Models
{
    public class ServicesViewModel:ViewModelBase
    {
        // Danh sách gói dịch vụ
        public List<ServicePackages> l_ServicePackages { get; set; }
        // Bài viết so sánh quyền lợi
        public ArticlesViewDetail m_ArticlesViewDetail { get; set; }
    }
}