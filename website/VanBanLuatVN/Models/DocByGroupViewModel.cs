using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;
using VanBanLuatVN.Models;

namespace VanBanLuat.Models
{
    public class DocByGroupViewModel : ViewModelBase
    {
        public DocList DocList { get; set; }
        public DocGroups DocGroups { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}