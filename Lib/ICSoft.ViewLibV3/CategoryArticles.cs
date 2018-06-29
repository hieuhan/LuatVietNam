using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class CategoryArticles
    {
        public Categories mCategory { get; set; }
        public List<Categories> lSubCategory { get; set; }
        public List<Categories> lParentCategory { get; set; }
        public List<Articles> lArticle { get; set; }
        public int RowCount { get; set; }
    }
}
