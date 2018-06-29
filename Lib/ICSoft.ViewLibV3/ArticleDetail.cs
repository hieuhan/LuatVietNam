using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class ArticleDetail
    {
        public Articles mArticle { get; set; }
        public Categories mCategory { get; set; }
        public List<Categories> lSubCategory { get; set; }
        public List<Categories> lParentCategory { get; set; }
        public List<Articles> lOtherArticle { get; set; }
        public List<Articles> lArticleRelate { get; set; }
        public List<Tags> lTag { get; set; }
        public List<Medias> lMedia { get; set; }
    }
}
