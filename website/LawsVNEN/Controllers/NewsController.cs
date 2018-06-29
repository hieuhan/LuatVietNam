using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawsVNEN.Models;
using ICSoft.CMSViewLib;
using LawsVNEN.Library;
using LawsVNEN.AppCode;
using DevTrends.MvcDonutCaching;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;

namespace LawsVNEN.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult ArticleDetail(short CategoryId = 0, int ArticleId = 0)
        {
            ArticleViewModel m_ArticleViewModel = new ArticleViewModel();
            if (ArticleId > 0)
            {
                m_ArticleViewModel.m_ArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(ArticleId, CategoryId, 9, 0);
                m_ArticleViewModel.WebsiteTitle = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaTitle) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaTitle : m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.Title;
                m_ArticleViewModel.WebsiteDescription = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaDesc.StripTags()) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaDesc.StripTags() : Constants.WebsiteDescription;
                m_ArticleViewModel.WebsiteKeywords = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaKeyword) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaKeyword : Constants.WebsiteDescription;
                m_ArticleViewModel.SeoFooter = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mCategoriesView.SeoFooter) ? m_ArticleViewModel.m_ArticlesViewDetail.mCategoriesView.SeoFooter : "";
            }
            else
                return RedirectToAction("Index", "Home");
            return View(m_ArticleViewModel);
        }

        public ActionResult UserGuide()
        {
            ArticleViewModel m_ArticleViewModel = new ArticleViewModel();
            m_ArticleViewModel.m_ArticlesViewMaster = Extensions.GetArticlesViewMaster();
            if (m_ArticleViewModel.m_ArticlesViewMaster.lCategoriesMain1.Count > 0)
            {
                m_ArticleViewModel.LinkDownLoad = (LawsVnLanguages.GetCurrentLanguageId() == 1) ? Constants.Link_DownloadVN :Constants.Link_DownloadEN ;
                m_ArticleViewModel.l_CategoriesView = m_ArticleViewModel.m_ArticlesViewMaster.lCategoriesMain1;
                m_ArticleViewModel.WebsiteTitle = !string.IsNullOrEmpty(m_ArticleViewModel.l_CategoriesView[0].MetaTitle) ? m_ArticleViewModel.l_CategoriesView[0].MetaTitle : m_ArticleViewModel.l_CategoriesView[0].CategoryName;
                m_ArticleViewModel.WebsiteDescription = !string.IsNullOrEmpty(m_ArticleViewModel.l_CategoriesView[0].MetaDesc.StripTags()) ? m_ArticleViewModel.l_CategoriesView[0].MetaDesc.StripTags() : m_ArticleViewModel.l_CategoriesView[0].CategoryDesc;
                m_ArticleViewModel.WebsiteKeywords = !string.IsNullOrEmpty(m_ArticleViewModel.l_CategoriesView[0].MetaKeyword) ? m_ArticleViewModel.l_CategoriesView[0].MetaKeyword : Constants.WebsiteDescription;
                m_ArticleViewModel.SeoFooter = !string.IsNullOrEmpty(m_ArticleViewModel.l_CategoriesView[0].SeoFooter) ? m_ArticleViewModel.l_CategoriesView[0].SeoFooter : "";
            }
            else
                return View("~/Views/Error/PageNotFound.cshtml");
            return View(m_ArticleViewModel);
        }

        public ActionResult BanTinLuatVN(short CategoryId = 553, string keyword = "", int page = 0)
        {
            byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
            int rowCount = 0; short tagId = 0;
            var model = new ArticleViewModel();
            byte siteID = Extensions.GetSiteId();
            var mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(CategoryId);
            if (CategoryId == Constants.CateIdBanTinLuatVN)
            {
                siteID = Constants.SiteId;
                if (_languageId == 1)
                {
                    mCategoriesView.CategoryName = "Bản tin Tiếng Anh";
                }
            }
            Articles m_Articles = new Articles();
            m_Articles.ReviewStatusId = 2;//Reviewed
            m_Articles.ShowTop = 0;
            m_Articles.SiteId = siteID;
            m_Articles.CategoryId = CategoryId;
            m_Articles.Title = keyword;
            //m_Articles.LanguageId = 2;//English
            List<ArticlesView> ListArticlesView = m_Articles.GetPageView(0, Constants.RowAmount20, page > 0 ? page - 1 : page, "DisplayOrder DESC", tagId, 0, 0, "", "", ref rowCount);
            if (mCategoriesView.CategoryId > 0) { 
            model = new ArticleViewModel
            {
                Page = page,
                m_CategoriesView = mCategoriesView,
                ListArticlesView = m_Articles.GetPageView(0, Constants.RowAmount20, page > 0 ? page - 1 : page, "DisplayOrder DESC", tagId, 0, 0, "", "", ref rowCount),
                //ListArticlesView = ArticlesViewHelpers.View_Search_01(Constants.RowAmount20, page > 0 ? page - 1 : page,
                   // 0, applicationTypeId, CategoryId, siteID, dataTypeId, tagId,
                   // keyword, 0, districtId, 0, 0, 0, "DisplayOrder DESC", ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = Constants.RowAmount20,
                    PageLoad = true,
                    CategoryId = CategoryId,
                    KeyWord = keyword,
                    ControllerName = "Ajax",
                    ActionName = "BanTinLuatVN_Search",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                MetaTitle = string.IsNullOrEmpty(mCategoriesView.MetaTitle) ? mCategoriesView.CategoryName : mCategoriesView.MetaTitle,
                WebsiteTitle = string.IsNullOrEmpty(mCategoriesView.MetaTitle) ? mCategoriesView.CategoryName : mCategoriesView.MetaTitle,
                WebsiteDescription = string.IsNullOrEmpty(mCategoriesView.MetaDesc) ? mCategoriesView.CategoryDesc : mCategoriesView.MetaDesc,
                WebsiteKeywords = mCategoriesView.MetaKeyword,
                WebsiteImage = mCategoriesView.GetImageUrl(),
                WebsiteCanonical = string.IsNullOrEmpty(mCategoriesView.CanonicalTag) ? mCategoriesView.GetCategoryUrl() : mCategoriesView.CanonicalTag,
                SeoFooter = mCategoriesView.SeoFooter
            };
            }
            else RedirectToAction("Index", "Home");
            return View(model);
        }

        public ActionResult BanTin(short CategoryId = 0, int ArticleId = 0)
        {
            ArticleViewModel m_ArticleViewModel = new ArticleViewModel();
            if (ArticleId > 0)
            {
                m_ArticleViewModel.m_ArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(ArticleId, CategoryId, 0, 0);
                m_ArticleViewModel.WebsiteTitle = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaTitle) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaTitle : m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.Title;
                m_ArticleViewModel.WebsiteDescription = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaDesc.StripTags()) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaDesc.StripTags() : Constants.WebsiteDescription;
                m_ArticleViewModel.WebsiteKeywords = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaKeyword) ? m_ArticleViewModel.m_ArticlesViewDetail.mArticlesView.MetaKeyword : Constants.WebsiteDescription;
                m_ArticleViewModel.SeoFooter = !string.IsNullOrEmpty(m_ArticleViewModel.m_ArticlesViewDetail.mCategoriesView.SeoFooter) ? m_ArticleViewModel.m_ArticlesViewDetail.mCategoriesView.SeoFooter : "";
            }
            else
                return RedirectToAction("Index", "Home");
            return View(m_ArticleViewModel);
        }
    }
}
