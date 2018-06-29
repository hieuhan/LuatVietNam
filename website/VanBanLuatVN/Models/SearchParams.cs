using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VanBanLuat.Models
{
    public class SearchParams
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class ArticleSearchParams : SearchParams
    {
        public byte ArticleId { get; set; }
    }

}