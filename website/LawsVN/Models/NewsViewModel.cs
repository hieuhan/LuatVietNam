using ICSoft.CMSViewLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class NewsViewModel:ViewModelBase
    {
        // Chuyên mục
        public CategoriesView mCategoriesView { get; set; }
        public short CategoryId { get; set; }
        public int ProvinceId { get; set; }

        public ArticlesViewDetail m_ArticlesViewDetail { get; set; }

        public ArticlesViewHome mArticlesViewHome { get; set; }

        public ArticlesViewMaster mArticlesViewMaster { get; set; }

        // DS tin theo chuyên mục
        public ArticlesViewCate mArticlesViewCate { get; set; }

        // Chuyên mục và danh sách tin xem nhiều
        public ArticlesViewCate mArticlesViewCateMostView { get; set; }

        // DS Slide
        //public List<ArticlesView> l_ArticlesView_Slide { get; set; }
        // Tin vắn  
        //public List<ArticlesView> l_ArticlesView_Brief { get; set; }

        // Tin xem nhiều
        public List<ArticlesView> ListArticlesMostView { get; set; }

        // Tiêu điểm
        public List<ArticlesView> ListArticlesTopView { get; set; }

        
        //thong tin tag
        public Tags mTags { get; set; }
        public List<ArticlesView> ListArticlesView { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
        public IEnumerable<Tuple<DateTime, DateTime>> ListDateRange
        {
            get
            {
                DateTime dt = DateTime.Now;
                int year = mPartialPaginationAjax.Year > 0 ? mPartialPaginationAjax.Year : dt.Year,
                    month = mPartialPaginationAjax.Month > 0 ? mPartialPaginationAjax.Month : dt.Month,
                    daysInMonth = DateTime.DaysInMonth(year,month);
                DateTime dateFrom = new DateTime(year,month,1),
                    dateTo = new DateTime(year,month,daysInMonth);
                return dateFrom.SplitDateRange(dateTo);
            }
        }

        public bool IsDiemTin
        {
            get
            {
                return mPartialPaginationAjax.CategoryId == Constants.CateIdBanTinLuat ||
                       mPartialPaginationAjax.CategoryId == Constants.CateIdDiemTinVb ||
                       mPartialPaginationAjax.CategoryId == Constants.CateIdDiemTinChinhSachMoi;
            }
        }

        //public readonly Dictionary<short, string> SeoDictionary = new Dictionary<short, string>
        //{
        //    { Constants.CateIdBanTinHieuLuc, "Bản tin Hiệu lực Văn bản - Những thay đổi về hiệu lực của văn bản, Văn bản hết hạn và những văn bản thay thế, Văn bản bị sửa đổi bổ sung"},
        //    { Constants.CateIdCapNhatHangTuan, "Văn bản cập nhật hàng tuần - Điểm tin Văn bản nổi bật hàng tuần, bản tin Văn bản, chính sách, luật, nghị quyết tiêu biểu, hiệu lực văn bản được cập nhật hàng tuần"},
        //    { Constants.CateIdDiemTinVb, "Điểm tin Văn bản mới nhất - Điểm lại một số Văn bản pháp luật nổi bật nhất mới ban hành, vừa được sửa đổi bổ sung, Văn bản chính sách mới của các bộ luật"},
        //    { Constants.CateIdDiemTinChinhSachMoi, ""}
        //};

        /// <summary>
        /// Danh sách chuyên mục theo parentId
        /// </summary>
        public List<CategoriesView> ListCategoriesViewByParentId { get; set; }
        public List<Provinces> ListProvinces { get; set; }
    }
}