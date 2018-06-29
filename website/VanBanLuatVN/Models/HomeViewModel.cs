using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;

namespace VanBanLuat.Models
{
    public class HomeViewModel : ViewModelBase
    {
        public DocList DocList { get; set; }
        public DocFilterParams SearchParams { get; set; }
    }
}