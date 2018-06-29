using System;
using System.Collections.Generic;
using System.Linq;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using LawsVN.Models;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News Index/
        private readonly byte _languageId = LawsVnLanguages.AvailableLanguages[0].LanguageId;

        //[SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Index(short categoryId = 230)
        {
            int rowCount = 0;
            NewsViewModel model;
            var categoriesView = CategoriesViewHelpers.View_GetByCategoryId(categoryId);
            if (categoriesView.CategoryId > 0)
            {
                model = new NewsViewModel
                {
                    HaveAmp = true,
                    mArticlesViewMaster = ArticlesViewHelpers.View_GetArticleMaster(Constants.SiteId, 1, _languageId, 0, 1),
                    mCategoriesView = categoriesView,
                    mArticlesViewCateMostView = ArticlesViewHelpers.GetViewMostViewByCate_Paging(categoryId,
                        Constants.RowAmount5, 0, 0, 0, ref rowCount),
                    CategoryId = categoryId,
                    MetaTitle = categoriesView.MetaTitle.TrimmedOrDefault(categoriesView.CategoryName),
                    WebsiteTitle = categoriesView.MetaTitle.TrimmedOrDefault(categoriesView.CategoryName),
                    WebsiteDescription = categoriesView.MetaDesc.TrimmedOrDefault(categoriesView.CategoryDesc),
                    WebsiteKeywords = categoriesView.MetaKeyword.TrimmedOrDefault(categoriesView.CategoryDesc),
                    WebsiteImage = categoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = categoriesView.CanonicalTag.TrimmedOrDefault(CmsConstants.WEBSITE_DOMAIN + categoriesView.CategoryUrl),
                    SeoFooter = categoriesView.SeoFooter,
                    SeoHeader = categoriesView.H1Tag.TrimmedOrDefault(categoriesView.CategoryName),
                    MenuItemId = 688,
                };
            }
            else return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("Index", model,Request);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ArticlesCategory(short categoryId = 230, int page = 1, int pSize = 10)
        {
            NewsViewModel model;
            int rowCount = 0, rowCountArticleMostView = 0;
            var mArticlesViewCate = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, pSize, 1, 1, 1, 1, 0, page > 0 ? page - 1 : page,
                ref rowCount);
            var mCategoriesView = mArticlesViewCate.mCategoriesView;
            if (!Request.RawUrl.Contains(mCategoriesView.CategoryUrl))
            {
                Response.RedirectPermanent(mCategoriesView.CategoryUrl);
            }
            if (mCategoriesView.CategoryId > 0)
            {
                model = new NewsViewModel
                {
                    Page = page,
                    HaveAmp = true,
                    mArticlesViewHome = Extensions.GetArticlesViewHome(),
                    mArticlesViewCate = mArticlesViewCate,
                    mCategoriesView = mCategoriesView,
                    mArticlesViewCateMostView = ArticlesViewHelpers.GetViewMostViewByCate_Paging(categoryId,
                        Constants.RowAmount5, 0, 0, 0, ref rowCountArticleMostView),
                    mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        PageIndex = Extensions.IsMobile() && page == 1 ? 0 : page,
                        TotalPage = rowCount,
                        PageSize = pSize,
                        LinkLimit = Constants.RowAmount3,
                        UrlPaging = string.Empty,
                        ShowNumberOfResults = pSize,
                        PageLoad = true,
                        CategoryId = categoryId,
                        ControllerName = "Ajax",
                        ActionName = "GetViewByCateId_Paging",
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ArticlesByCateBox",
                            InsertionMode = InsertionMode.Replace
                        }
                    },
                    MetaTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteDescription = mCategoriesView.MetaDesc.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteKeywords = mCategoriesView.MetaKeyword.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteImage = mCategoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = mCategoriesView.CanonicalTag.TrimmedOrDefault(CmsConstants.WEBSITE_DOMAIN + mCategoriesView.CategoryUrl),
                    SeoFooter = mCategoriesView.SeoFooter,
                    SeoHeader = mCategoriesView.H1Tag.TrimmedOrDefault(mCategoriesView.CategoryName)
                };
            }
            else return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("ArticlesCategory", model,Request);
        }

        [HttpGet]
        [ValidateInput(false)]
        [SEOAction]
        public ActionResult Search(string keyword = "", short provinceId = 0, int page = 0, int pSize = 10)
        {
            byte applicationTypeId = 0, dataTypeId = 1;
            short categoryId = 0, districtId = 0;
            keyword = keyword.StripTags().SanitizeWithoutSplash();
            //string provinceDesc = provinceId > 0 ? string.Concat(" tại ", Provinces.Static_Get((short)provinceId).ProvinceDesc) : string.Empty;
            int rowCount = 0, tagId = 0;
            var model = new NewsViewModel
            {
                HaveAmp = true,
                Page = page,
                mArticlesViewHome = Extensions.GetArticlesViewHome(),
                ListArticlesTopView = ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdTieuDiem, 0,
                    Constants.RowAmount5, "DisplayOrder Desc"),
                ListArticlesMostView = ArticlesViewHelpers.View_GetArticleMostView(0, Constants.RowAmount5),
                ListArticlesView = ArticlesViewHelpers.View_Search_01(pSize, page > 0 ? page - 1 : page,
                    0, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                    keyword, provinceId, districtId, 0, 0, 0, "", ref rowCount),
                ListProvinces = DropDownListHelpers.GetAllProvinces("Cả nước"),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount3,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    Keywords = keyword,
                    ProvinceId = provinceId,
                    ControllerName = "Ajax",
                    ActionName = "ArticlesView_Search",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
                //MetaTitle = (keyword.Trim() != "" ? keyword + ", " : "") + "Tin tức pháp luật mới nhất" +
                //            (keyword.Trim() != "" ? " về " + keyword : "") + provinceDesc,
                //WebsiteTitle = (keyword.Trim() != "" ? keyword + ", " : "") + "Tin tức pháp luật mới nhất" +
                //               (keyword.Trim() != "" ? " về " + keyword : "") + provinceDesc,
                //WebsiteDescription = (keyword.Trim() != "" ? keyword + ", " : "") + "Tin tức pháp luật mới nhất" +
                //                     (keyword.Trim() != "" ? " về " + keyword : "") + provinceDesc +
                //                     " được cập nhật liên tục tại luatvietnam.vn",
                //WebsiteKeywords = (keyword.Trim() != "" ? keyword + ", " : "") + "Tin phap luat, Tin phap luat " +
                //                  (keyword.Trim() != "" ? " ve " + keyword : "") + provinceDesc,
                //WebsiteCanonical = string.Concat(CmsConstants.ROOT_PATH, "tim-kiem-tin-tuc.html"),
                //SeoHeader = "Tìm kiếm"
            };
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ArticleDetail(short categoryId = 0, int articleId = 0)
        {
            int rowCount = 0;
            var query = Extensions.GetUrlParameter(Request, "layout");
            if (articleId > 0)
            {
                var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount6, 0);
                if (mArticlesViewDetail.mArticlesView.ArticleId <= 0)
                {
                    return RedirectToAction("Error404", "Error");
                }
                var model = new ArticleViewDetailModel
                {
                    HaveAmp = true,
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    ListTieuDiem =
                        ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdTieuDiem, 0,
                            Constants.RowAmount5, "DisplayOrder DESC,PublishTime DESC"),
                    mArticlesMostView = ArticlesViewHelpers.GetViewMostView_Paging(Constants.SiteId, 1,
                        Constants.RowAmount5, 1, 0, 0, 0, ref rowCount),
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteImage = mArticlesViewDetail.mArticlesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl_Article),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl

                };
                if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                {
                    mArticlesViewDetail.mArticlesView.ArticleContent =
                        GoogleAmp.Convert(mArticlesViewDetail.mArticlesView.ArticleContent);
                }
                return Extensions.GetViewMode("ArticleDetail", model, Request);
            }
            return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ArticleGoiDichVu(short categoryId = 0, int articleId = 0)
        {
            
            ArticleViewDetailModel model;
            if (articleId > 0)
            {
                var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount4, 0);
                model = new ArticleViewDetailModel
                {
                    HaveAmp = true,
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteImage = mArticlesViewDetail.mArticlesView.GetImageUrl(),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl
                };
                if (model.mArticlesViewMaster.lCategoriesBottom1.HasValue())
                    model.mSharedCorner = model.mArticlesViewMaster.lCategoriesBottom1[0];
            }
            else return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("ArticleGoiDichVu", model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ServiceDetail(short categoryId = 0, int articleId = 0)
        {
            ArticleViewDetailModel model;
            if (articleId > 0)
            {
                var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount4, 0);
                model = new ArticleViewDetailModel
                {
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl
                };
                if (model.mArticlesViewMaster.lCategoriesBottom1.HasValue())
                    model.mSharedCorner = model.mArticlesViewMaster.lCategoriesBottom1[0];
                if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
                {
                    return Extensions.GetViewMode("ServiceDetail", model);
                }
                else return RedirectToAction("Error404", "Error");
            }
            else return RedirectToAction("Index", "Home");
        }


        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        [SEOAction]
        public ActionResult GioiThieuDetail(short categoryId = 518)
        {
            var query = Extensions.GetUrlParameter(Request, "layout");
            NewsViewModel model = new NewsViewModel();
            if (categoryId > 0)
            {
                model.HaveAmp = true;
                model.mArticlesViewHome = ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, 1,
                    LawsVnLanguages.GetCurrentLanguageId(), 0, Constants.RowAmount3, 0);
                model.m_ArticlesViewDetail =
                    ArticlesViewHelpers.View_GetArticleDetail(Constants.ArticleIdGioiThieu, 518, 0, 0);
                model.ListArticlesView =
                    ArticlesViewHelpers.View_GetArticleByCategoryId(categoryId, 0, Constants.RowAmount100, "");
                model.MenuItemId = 12;
                if (model.m_ArticlesViewDetail.mArticlesView.ArticleId > 0)
                {
                    if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                    {
                        model.m_ArticlesViewDetail.mArticlesView.ArticleContent =
                            GoogleAmp.Convert(model.m_ArticlesViewDetail.mArticlesView.ArticleContent);
                    }
                    return Extensions.GetViewMode("GioiThieuDetail", model, Request);
                }
                else return RedirectToAction("Error404", "Error");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult BanTinLuatVn(short categoryId = 531, int page = 1, int pSize = 24)
        {
            byte applicationTypeId = 0, dataTypeId = 1; 
            int districtId = 0;
            int rowCount = 0, tagId = 0;
            DateTime dt = DateTime.Now;
            DateTime? datFrom =
                    categoryId == Constants.CateIdBanTinLuat && !Extensions.IsMobile()
                        ? new DateTime(dt.Year, dt.Month, 1)
                        : (DateTime?) null,
                dateTo = !Extensions.IsMobile()
                    ? new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month))
                    : (DateTime?) null;
            pSize = Extensions.IsMobile() ? Constants.RowAmount20 : pSize;
            var mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(categoryId);
            if (mCategoriesView.CategoryId > 0)
            {
                if (!Request.RawUrl.Contains(mCategoriesView.CategoryUrl))
                {
                    Response.RedirectPermanent(mCategoriesView.CategoryUrl);
                }
                var model = new NewsViewModel
                {
                    Page = Extensions.IsMobile() ? page > 0 ? page - 1 : page : page,
                    HaveAmp = true,
                    mCategoriesView = mCategoriesView,
                    //mArticlesViewHome = Extensions.GetArticlesViewHome(),
                    //ListArticlesTopView = ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdTieuDiem, 0,
                        //Constants.RowAmount5, "DisplayOrder Desc"),
                    //ListArticlesMostView = ArticlesViewHelpers.View_GetArticleMostView(0, Constants.RowAmount5),
                    ListArticlesView = ArticlesViewHelpers.View_Search_01(pSize, page > 0 ? page - 1 : page,
                        0, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                        "", 0, districtId, 0, 0, 0, "PublishTime DESC", ref rowCount, datFrom, dateTo),
                    ListCategoriesViewByParentId = CategoriesViewHelpers.View_GetListByParentId(Constants.CateIdBanTinLuat),
                    mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        PageIndex = Extensions.IsMobile() ? page > 0 ? page - 1 : page : page,
                        TotalPage = rowCount,
                        PageSize = pSize,
                        ShowNumberOfResults = pSize,
                        LinkLimit = Constants.RowAmount5,
                        UrlPaging = string.Empty,
                        CategoryId = categoryId,
                        Month = dt.Month,
                        Year = dt.Year,
                        ControllerName = "Ajax",
                        ActionName = Extensions.IsMobile() ? "BanTinLuatVN_Search" : "BanTinLuatVN_SearchV2",
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ArticlesByCateBox",
                            InsertionMode = InsertionMode.Replace
                        }
                    },
                    MetaTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteDescription = mCategoriesView.MetaDesc.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteKeywords = mCategoriesView.MetaKeyword.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteImage = mCategoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = mCategoriesView.CanonicalTag.TrimmedOrDefault(CmsConstants.WEBSITE_DOMAIN + mCategoriesView.CategoryUrl),
                    SeoFooter = mCategoriesView.SeoFooter,
                    SeoHeader = mCategoriesView.H1Tag.TrimmedOrDefault(mCategoriesView.CategoryName),
                };
                return Extensions.GetViewMode("BanTinLuatVN", model, Request);
            }
            return RedirectToAction("Error404", "Error");
        }

        [HttpPost]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult BanTinLuatVn(short categoryId = 531, int month = 0, int year = 0, int page = 1, int pSize = 24)
        {
            byte applicationTypeId = 0, dataTypeId = 1;
            int districtId = 0;
            int rowCount = 0, tagId = 0;
            DateTime dt = DateTime.Now;
            month = month > 0 ? month : dt.Month;
            year = year > 0 ? year : dt.Year;
            DateTime? datFrom =
                    categoryId == Constants.CateIdBanTinLuat && !Extensions.IsMobile()
                        ? new DateTime(year, month, 1)
                        : (DateTime?) null,
                dateTo = !Extensions.IsMobile()
                    ? new DateTime(year, month, DateTime.DaysInMonth(year, month))
                    : (DateTime?) null;
            string keywords = "";
            var mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(categoryId);
            if (mCategoriesView.CategoryId > 0)
            {
                var model = new NewsViewModel
                {
                    Page = Extensions.IsMobile() ? page > 0 ? page - 1 : page : page,
                    HaveAmp = true,
                    mCategoriesView = mCategoriesView,
                    //mArticlesViewHome = Extensions.GetArticlesViewHome(),
                    //ListArticlesTopView = ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdTieuDiem, 0,
                        //Constants.RowAmount5, "DisplayOrder Desc"),
                    //ListArticlesMostView = ArticlesViewHelpers.View_GetArticleMostView(0, Constants.RowAmount5),
                    ListArticlesView = ArticlesViewHelpers.View_Search_01(pSize, page > 0 ? page - 1 : page,
                        0, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                        keywords, 0, districtId, 0, 0, 0, "PublishTime DESC", ref rowCount,datFrom, dateTo),
                    ListCategoriesViewByParentId = CategoriesViewHelpers.View_GetListByParentId(Constants.CateIdBanTinLuat),
                    mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        PageIndex = Extensions.IsMobile() ? page > 0 ? page - 1 : page : page,
                        TotalPage = rowCount,
                        PageSize = pSize,
                        ShowNumberOfResults = pSize,
                        LinkLimit = Constants.RowAmount5,
                        UrlPaging = string.Empty,
                        CategoryId = categoryId,
                        Month = month,
                        Year = year,
                        ControllerName = "Ajax",
                        ActionName = Extensions.IsMobile() ? "BanTinLuatVN_Search" : "BanTinLuatVN_SearchV2",
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ArticlesByCateBox",
                            InsertionMode = InsertionMode.Replace
                        }
                    },
                    MetaTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteTitle = mCategoriesView.MetaTitle.TrimmedOrDefault(mCategoriesView.CategoryName),
                    WebsiteDescription = mCategoriesView.MetaDesc.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteKeywords = mCategoriesView.MetaKeyword.TrimmedOrDefault(mCategoriesView.CategoryDesc),
                    WebsiteImage = mCategoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical =
                        mCategoriesView.CanonicalTag.TrimmedOrDefault(
                            CmsConstants.WEBSITE_DOMAIN + mCategoriesView.CategoryUrl),
                    SeoFooter = mCategoriesView.SeoFooter,
                    SeoHeader = mCategoriesView.H1Tag.TrimmedOrDefault(mCategoriesView.CategoryName),
                };
                return Extensions.GetViewMode("BanTinLuatVN", model, Request);
            }
            return RedirectToAction("Error404", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult TinVBMoi(short categoryId = 0, int articleId = 0)
        {
            var query = Extensions.GetUrlParameter(Request, "layout");
            ArticleViewDetailModel model;
            if (articleId > 0)
            {
                var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount4, 0);
                model = new ArticleViewDetailModel
                {
                    HaveAmp = true,
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl
                };
                if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
                {
                    if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                    {
                        model.m_ArticlesViewDetail.mArticlesView.ArticleContent =
                            GoogleAmp.Convert(model.m_ArticlesViewDetail.mArticlesView.ArticleContent);
                    }
                    return Extensions.GetViewMode("TinVBMoi", model, Request);
                }
                else return RedirectToAction("Error404", "Error");
            }
            else return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult BanTinHL(short categoryId = 0, int articleId = 0)
        {
            int rowCount = 0; 
            var query = Extensions.GetUrlParameter(Request, "layout");
            ArticleViewDetailModel model;
            var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount4, 0);
            if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
            {
                categoryId = mArticlesViewDetail.mArticlesView.CategoryId;
                model = new ArticleViewDetailModel
                {
                    HaveAmp = true,
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    mArticlesMostView = ArticlesViewHelpers.GetViewMostViewByCate_Paging(categoryId, Constants.RowAmount10, 0, 0, 0, ref rowCount),
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl,
                };
                if (model.mArticlesViewMaster.lCategoriesBottom1.HasValue())
                    model.mSharedCorner = model.mArticlesViewMaster.lCategoriesBottom1[0];

                if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                {
                    mArticlesViewDetail.mArticlesView.ArticleContent =
                        GoogleAmp.Convert(mArticlesViewDetail.mArticlesView.ArticleContent.Replace("ymailto=\"mailto:lawdata@luatvietnam.vn\"",""));
                }
                if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
                {
                    return Extensions.GetViewMode("BanTinHL", model, Request);
                }
                else return RedirectToAction("Error404", "Error");
            }
            else return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult VB_CNHT(short categoryId = 0, int articleId = 0)
        {
            
            ArticleViewDetailModel model;
            var mArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(articleId, categoryId, Constants.RowAmount10, 0);
            if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
            {
                categoryId = mArticlesViewDetail.mArticlesView.CategoryId;
                model = new ArticleViewDetailModel
                {
                    HaveAmp = true,
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    m_ArticlesViewDetail = mArticlesViewDetail,
                    WebsiteTitle = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteDescription = mArticlesViewDetail.mArticlesView.MetaDesc.StripTags().TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Summary.StripTags()),
                    WebsiteKeywords = mArticlesViewDetail.mArticlesView.MetaKeyword.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    SeoFooter = mArticlesViewDetail.mCategoriesView.SeoFooter,
                    SeoHeader = mArticlesViewDetail.mArticlesView.MetaTitle.TrimmedOrDefault(mArticlesViewDetail.mArticlesView.Title),
                    WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + mArticlesViewDetail.mArticlesView.ArticleUrl,
                };
                if (model.mArticlesViewMaster.lCategoriesBottom1.HasValue())
                    model.mSharedCorner = model.mArticlesViewMaster.lCategoriesBottom1[0];
                var query = Extensions.GetUrlParameter(Request, "layout");
                if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                {
                    mArticlesViewDetail.mArticlesView.ArticleContent =
                        GoogleAmp.Convert(mArticlesViewDetail.mArticlesView.ArticleContent);
                }
                if (mArticlesViewDetail.mArticlesView.ArticleId > 0)
                {
                    return Extensions.GetViewMode("VB_CNHT", model, Request);
                }
                else return RedirectToAction("Error404", "Error");
            }
            else return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        //[SEOAction]
        public ActionResult HuongDan()
        {

            ArticlesViewCate m_ArticlesViewCate = new ArticlesViewCate();
            var model = new NewsViewModel
            {
                HaveAmp = true,
                mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(Constants.CateIdHuongDan),
                mArticlesViewMaster =  Extensions.GetArticlesViewMaster()
                //mArticlesViewHome = ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, 1, LawsVnLanguages.GetCurrentLanguageId(), 0, Constants.RowAmount3, 0)
            };
            model.MenuItemId = 689;
            if (model.mArticlesViewMaster.lCategoriesMain4.HasValue())
            {
                for (int i = 0; i < 4 && i < model.mArticlesViewMaster.lCategoriesMain4.Count; i++)
                {
                    CategoriesView m_CategoriesView = model.mArticlesViewMaster.lCategoriesMain4[i];
                    if (Request.Url.AbsoluteUri.Contains(model.mArticlesViewMaster.lCategoriesMain4[i].CategoryUrl))
                    {

                        model.MetaTitle = m_CategoriesView.MetaTitle.TrimmedOrDefault(m_CategoriesView.CategoryName);
                        model.WebsiteTitle = m_CategoriesView.MetaTitle.TrimmedOrDefault(m_CategoriesView.CategoryName);
                        model.WebsiteDescription = m_CategoriesView.MetaDesc.TrimmedOrDefault(m_CategoriesView.CategoryDesc);
                        model.WebsiteKeywords = m_CategoriesView.MetaKeyword.TrimmedOrDefault(m_CategoriesView.CategoryDesc);
                        model.WebsiteImage = m_CategoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl);
                        model.WebsiteCanonical = CmsConstants.WEBSITE_DOMAIN + m_CategoriesView.CategoryUrl;
                        model.SeoFooter = m_CategoriesView.SeoFooter;
                        model.SeoHeader = m_CategoriesView.H1Tag.TrimmedOrDefault(m_CategoriesView.CategoryName);
                       
                        break;
                    }                    
                }
            }
            else
                return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("HuongDan", model, Request);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult CauHoiThuongGap(short categoryId = 521)
        {
            int rowCount = 0;
            var model = new NewsViewModel
            {
                mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(categoryId),
                mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                ListArticlesView = ArticlesViewHelpers.View_SearchWithContent(Constants.RowAmount20, 0, LawsVnLanguages.GetCurrentLanguageId(), 0, categoryId, Constants.SiteId, 1, "", ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    HaveAmp = true,
                    TotalPage = rowCount,
                    CategoryId = categoryId,
                    PageIndex = 0,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                    ControllerName = "Ajax",
                    ActionName = "CauHoiThuongGap_GetView",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace
                    }

                }
            };
            if (model.mCategoriesView.CategoryId > 0)
            {
                model.MetaTitle = model.mCategoriesView.MetaTitle.TrimmedOrDefault(model.mCategoriesView.CategoryName);
                model.WebsiteTitle = model.mCategoriesView.MetaTitle.TrimmedOrDefault(model.mCategoriesView.CategoryName);
                model.WebsiteDescription = model.mCategoriesView.MetaDesc.TrimmedOrDefault(model.mCategoriesView.CategoryDesc);
                model.WebsiteKeywords = model.mCategoriesView.MetaKeyword.TrimmedOrDefault(model.mCategoriesView.CategoryDesc);
                model.WebsiteImage = model.mCategoriesView.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl);
                model.WebsiteCanonical = model.mCategoriesView.GetCategoryUrl();
                model.SeoFooter = model.mCategoriesView.SeoFooter;
                model.SeoHeader = model.mCategoriesView.H1Tag.TrimmedOrDefault(model.mCategoriesView.CategoryName);
                model.MenuItemId = 689;
            }
            else
                return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("CauHoiThuongGap", model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        [SEOAction]
        public ActionResult ArticleTags(int tagId = 0, int page = 0, int pSize = 10)
        {
            NewsViewModel model;
            bool isMobile = HttpContext.Request.Browser.IsMobileDevice;
            if (tagId > 0)
            {
                int rowCount = 0, rowCountArticleMostView = 0;
                model = new NewsViewModel
                {
                    Page = page,
                    mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                    mArticlesViewCateMostView = isMobile ? new ArticlesViewCate() : ArticlesViewHelpers.GetViewMostView_Paging(Constants.SiteId, 1,
                        Constants.RowAmount5, 0, 0, 0, 0, ref rowCountArticleMostView),
                    ListArticlesView = ArticlesViewHelpers.View_Search_01(pSize, page > 0 ? page - 1 : page, 0, 0, 0, 0, 0,
                        tagId, "", 0, 0, 0, 0, 0, "", ref rowCount),
                    mTags = Tags.Static_Get(tagId, 0),
                    IsIndex = false,
                    mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        HaveAmp = true,
                        PageIndex = page,
                        TotalPage = rowCount,
                        PageSize = pSize,
                        LinkLimit = Constants.RowAmount3,
                        UrlPaging = string.Empty,
                        ShowNumberOfResults = pSize,
                        PageLoad = true,
                        TagId = tagId,
                        ControllerName = "Ajax",
                        ActionName = "ArticlesView_Search",
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ArticlesByCateBox",
                            InsertionMode = InsertionMode.Replace
                        }
                    }
                };
            }
            else return RedirectToAction("Index", "Home");
            return Extensions.GetViewMode("ArticleTags", model);
        }

    }
}
