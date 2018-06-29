using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class ArticleFilterParams
    {
        public ArticleFilterParams()
        {
            CategoryId = 0;
            SiteId = 0;
            Keyword = "";
            ArticleFieldSelect = "ArticleId,Title,ImagePath,ArticleUrl";
            DataTypeId = 0;
            ArticleTypeId = 0;
            CategoryFieldSelect = "CategoryId,CategoryDesc,CategoryUrl";
            RowAmount = 10;
            PageIndex = 0;
            OrderBy = "DisplayOrder DESC";
            DataSourceId = 0;
            ProvinceId = 0;
            TagId = 0;
            GetRowCount = 0;
            GetListSubCate = 0;
            GetListParentCate = 0;
        }
        public short CategoryId { get; set; }
        public short SiteId { get; set; }
        public string Keyword { get; set; }
        public string ArticleFieldSelect { get; set; }
        public byte DataTypeId { get; set; }
        public byte ArticleTypeId { get; set; }
        public string CategoryFieldSelect { get; set; }
        public int RowAmount { get; set; }
        public int PageIndex { get; set; }
        public string OrderBy { get; set; }
        public short DataSourceId { get; set; }
        public short ProvinceId { get; set; }
        public int TagId { get; set; }
        public byte GetRowCount { get; set; }
        public byte GetListSubCate { get; set; }
        public byte GetListParentCate { get; set; }
    }
}
