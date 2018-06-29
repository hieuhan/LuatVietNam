using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanBanLuat.Models;
using ICSoft.ViewLibV3;
using System.Collections;
using VanBanLuat;
using VanBanLuat.AppCode.Attribute;
using VanBanLuat.Librarys;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuat.Controllers
{
    public class NewController : Controller
    {
        //
        // GET: /New/
        #region Tin tức
        [SEOAction]
        [OutputCache(CacheProfile = "Cache3Minutes")]
        public ActionResult Index()
        {
            Categories mCategories = CategoryHelpers.GetById(Constants.CateId_TinTuc);
            if (mCategories.CategoryId > 0)
            {
                var model = new NewViewModel
                {
                    MenuItemId = Constants.MenuItemIdTinTucPhapLuat,
                    MetaTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteDescription = mCategories.MetaDesc.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteKeywords = mCategories.MetaKeyword.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteImage = mCategories.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = mCategories.CanonicalTag.TrimmedOrDefault(Constants.ROOT_PATH + mCategories.CategoryUrl),
                    SeoFooter = mCategories.SeoFooter,
                    SeoHeader = mCategories.H1Tag.TrimmedOrDefault(mCategories.CategoryName)
                };
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "ArticlesByCateCache3Minutes")]
        public ActionResult ArticleByCate(short categoryId = 559, int page = 1)
        {
            if (categoryId > 0)
            {
                string FieldArticleSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime", FieldCategorySelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,H1Tag,MetaDesc,MetaKeyword,SeoFooter,MetaTitle,ImagePath";
                CategoryArticles mCategoryArticles = ArticleHelpers.GetByCategoryId(categoryId, Constants.RowAmount5, page > 0 ? page - 1 : page, 1, 0, 0, "DisplayOrder Desc", FieldArticleSelect, FieldCategorySelect);
                if (!Request.RawUrl.Contains(mCategoryArticles.mCategory.CategoryUrl))
                {
                    Response.RedirectPermanent(mCategoryArticles.mCategory.CategoryUrl);
                }
                if (mCategoryArticles.mCategory.CategoryId > 0)
                {
                    Categories mCategories = mCategoryArticles.mCategory;
                    var model = new NewViewModel
                    {
                        Page = page,
                        mCategoryArticles = mCategoryArticles,
                        MetaTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                        WebsiteTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                        WebsiteDescription = mCategories.MetaDesc.TrimmedOrDefault(mCategories.CategoryDesc),
                        WebsiteKeywords = mCategories.MetaKeyword.TrimmedOrDefault(mCategories.CategoryDesc),
                        WebsiteImage = mCategories.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                        WebsiteCanonical = mCategories.CanonicalTag.TrimmedOrDefault(mCategories.CategoryUrl.StartsWith("http") ? mCategories.CategoryUrl : (mCategories.CategoryUrl.StartsWith(Constants.ROOT_PATH) ? mCategories.CategoryUrl : string.Concat(Constants.WEBSITE_DOMAIN, mCategories.CategoryUrl))),
                        SeoFooter = mCategories.SeoFooter,
                        SeoHeader = mCategories.H1Tag.TrimmedOrDefault(mCategories.CategoryName)
                    };
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "ArticlesTagsCache3Minutes")]
        public ActionResult ArticleTags(int tagId = 0)
        {
            if (tagId > 0)
            {
                NewViewModel model = new NewViewModel
                {
                    mTagArticles = ArticleHelpers.GetByTagId(Constants.SiteId, tagId,
                        Constants.RowAmount20, 0, 1,
                        "ArticleId,Title,ImagePath,ArticleUrl,Summary,PublishTime",
                        "ArticleId,Title,ImagePath,ArticleUrl,Summary,PublishTime")
                };
                return View(model);

            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "ArticleDetailCache3Minutes")]
        public ActionResult ArticleDetail(short categoryId = 0, int articleId = 0)
        {
            int rowAmount1 = 13;
            int rowAmount = Constants.RowAmount5;
            if (articleId > 0)
            {
                var mArticleDetail = ArticleHelpers.GetById(categoryId, articleId, rowAmount1, 1, 1, 1, "ArticleId,Title,ImagePath,ArticleUrl,Summary,ArticleContent,PublishTime", "ArticleId,Title,ImagePath,ArticleUrl,PublishTime");
                if (mArticleDetail.mArticle.ArticleId <= 0)
                {
                    return RedirectToAction("Index", "Error");
                }
                var model = new ArticleViewDetailModel
                {
                    mArticleDetail = mArticleDetail,
                    ListTieuDiem = ArticleHelpers.GetTopByCategoryId(Constants.CateId_TieuDiem, rowAmount, "DisplayOrder DESC", "ArticleId,Title,ArticleUrl"),
                    WebsiteTitle = mArticleDetail.mArticle.MetaTitle.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    WebsiteDescription = mArticleDetail.mArticle.MetaDesc.StripTags().TrimmedOrDefault(mArticleDetail.mArticle.Summary.StripTags()),
                    WebsiteKeywords = mArticleDetail.mArticle.MetaKeyword.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    SeoFooter = mArticleDetail.mArticle.SeoFooter,
                    SeoHeader = mArticleDetail.mArticle.MetaTitle.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    WebsiteImage = mArticleDetail.mArticle.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl_Article),
                    WebsiteCanonical = Constants.WebsiteCanonical + mArticleDetail.mArticle.ArticleUrl
                };
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }
        #endregion Tin tức

        #region Pháp lý doanh nghiệp

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3Minutes")]
        public ActionResult PhapLyDoanhNghiep()
        {
            string FieldArticleSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime";
            string FieldCategorySelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,H1Tag,MetaDesc,MetaKeyword,SeoFooter,MetaTitle,ImagePath";
            List<CategoryArticles> lCategoryArticles = new List<CategoryArticles>();
            Categories mCategories = CategoryHelpers.GetById(Constants.CateId_PhapLyDoanhNghiep);
            if (mCategories.CategoryId > 0)
            {
                string[] lcateId = Constants.CateId_DanhMucPhapLy_ListCate.Split(';');
                foreach (string cateId in lcateId)
                {
                    var mCategoryArticles = ArticleHelpers.GetTopByCategoryId(short.Parse(cateId.Replace(" ", "")), Constants.RowAmount3, "DisplayOrder Desc", FieldArticleSelect, FieldCategorySelect);
                    lCategoryArticles.Add(mCategoryArticles);
                }
                var model = new NewViewModel
                {
                    MenuItemId = Constants.MenuItemIdPhapLyDoanhNghiep,
                    lCategoryArticles = lCategoryArticles,
                    MetaTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteDescription = mCategories.MetaDesc.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteKeywords = mCategories.MetaKeyword.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteImage = mCategories.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = mCategories.CanonicalTag.TrimmedOrDefault(Constants.ROOT_PATH + mCategories.CategoryUrl),
                    SeoFooter = mCategories.SeoFooter,
                    SeoHeader = mCategories.H1Tag.TrimmedOrDefault(mCategories.CategoryName)
                };
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult LoaiHinhDoanhNghiep(short categoryId = 0)
        {
            List<CategoryArticles> lCategoryArticles = new List<CategoryArticles>();
            if (categoryId > 0)
            {
                Categories mCategories = CategoryHelpers.GetById(categoryId);
                if (mCategories.CategoryId <= 0)
                { return RedirectToAction("Index", "Error"); }
                //ToDo danh sách biểu mẫu
                CategoryArticles mCategoryArticlesBieuMau = ArticleHelpers.GetPage(new ArticleFilterParams
                {
                    ArticleFieldSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime",
                    CategoryFieldSelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,H1Tag,MetaDesc,MetaKeyword,SeoFooter,MetaTitle,ImagePath",
                    CategoryId = categoryId,
                    SiteId = Constants.SiteId,
                    DataTypeId = 1,
                    ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                    RowAmount = Constants.RowAmount10,
                    GetListSubCate = 1
                });
                //ToDo danh sách tin theo chuyên mục con
                if (mCategoryArticlesBieuMau.lSubCategory.HasValue())
                {
                    foreach (var item in mCategoryArticlesBieuMau.lSubCategory)
                    {
                        CategoryArticles mCategoryArticles = ArticleHelpers.GetPage(new ArticleFilterParams
                        {
                            ArticleFieldSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime",
                            CategoryFieldSelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,H1Tag,MetaDesc,MetaKeyword,SeoFooter,MetaTitle,ImagePath",
                            CategoryId = item.CategoryId,
                            SiteId = Constants.SiteId,
                            DataTypeId = 1,
                            ArticleTypeId = 1,
                            RowAmount = Constants.RowAmount3
                        });
                        if (mCategoryArticles.lArticle.HasValue())
                            lCategoryArticles.Add(mCategoryArticles);
                    }
                }
                else
                {
                    CategoryArticles mCategoryArticles = ArticleHelpers.GetPage(new ArticleFilterParams
                    {
                        ArticleFieldSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime",
                        CategoryFieldSelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,H1Tag,MetaDesc,MetaKeyword,SeoFooter,MetaTitle,ImagePath",
                        CategoryId = categoryId,
                        SiteId = Constants.SiteId,
                        DataTypeId = 1,
                        ArticleTypeId = 1,
                        RowAmount = Constants.RowAmount3
                    });
                    lCategoryArticles.Add(mCategoryArticles);
                }
                var model = new NewViewModel
                {
                    mCategories = mCategories,
                    lCategoryArticles = lCategoryArticles,
                    categoryArticlesBieuMau = mCategoryArticlesBieuMau,
                    MetaTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                    WebsiteDescription = mCategories.MetaDesc.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteKeywords = mCategories.MetaKeyword.TrimmedOrDefault(mCategories.CategoryDesc),
                    WebsiteImage = mCategories.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                    WebsiteCanonical = mCategories.CanonicalTag.TrimmedOrDefault(Constants.ROOT_PATH + mCategories.CategoryUrl),
                    SeoFooter = mCategories.SeoFooter,
                    SeoHeader = mCategories.H1Tag.TrimmedOrDefault(mCategories.CategoryName)
                };
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult BieuMau()
        {
            Categories mCategories = CategoryHelpers.GetById(Constants.CateId_BieuMau);
            if (mCategories.CategoryId <= 0)
            { return RedirectToAction("Index", "Error"); }
            //ToDo ds chuyên mục con của Loại hình doanh nghiệp
            List<Categories> lCategories = CategoryHelpers.GetByParentItemId(Constants.CateId_LoaiHinhDoanhNghiep,
                "CategoryId, CategoryDesc, CategoryUrl");
            var mCategoryArticlesBieuMau = ArticleHelpers.GetPage(new ArticleFilterParams
            {
                SiteId = Constants.SiteId,
                CategoryId = Constants.CateId_BieuMau,
                ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                ArticleFieldSelect = "ArticleId, Title, ImagePath, ArticleUrl",
                RowAmount = Constants.RowAmount10,
                OrderBy = "DisplayOrder DESC"
            });

            var mCategoryArticles = 
                ArticleHelpers.GetPage(new ArticleFilterParams
                {
                    SiteId = Constants.SiteId,
                    CategoryId = Constants.CateId_BieuMauCanBiet,
                    ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                    ArticleFieldSelect = "ArticleId, Title, ImagePath, ArticleUrl",
                    RowAmount = Constants.RowAmount20,
                    OrderBy = "DisplayOrder DESC"
                });
            var model = new NewViewModel
            {
                mCategories = mCategories,
                lCategories = lCategories,
                categoryArticlesBieuMau = mCategoryArticlesBieuMau,
                mCategoryArticles = mCategoryArticles,
                MetaTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                WebsiteTitle = mCategories.MetaTitle.TrimmedOrDefault(mCategories.CategoryName),
                WebsiteDescription = mCategories.MetaDesc.TrimmedOrDefault(mCategories.CategoryDesc),
                WebsiteKeywords = mCategories.MetaKeyword.TrimmedOrDefault(mCategories.CategoryDesc),
                WebsiteImage = mCategories.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl),
                WebsiteCanonical = mCategories.CanonicalTag.TrimmedOrDefault(Constants.ROOT_PATH + mCategories.CategoryUrl),
                SeoFooter = mCategories.SeoFooter,
                SeoHeader = mCategories.H1Tag.TrimmedOrDefault(mCategories.CategoryName)
            };
            return View(model);
        }

        [OutputCache(CacheProfile = "ArticleDetailCache3Minutes")]
        public ActionResult BieuMauDetail(short categoryId = 0, int articleId = 15616)
        {
            if (articleId > 0)
            {
                var mArticleDetail = ArticleHelpers.GetById(categoryId, articleId, Constants.RowAmount6, 0, 0, 0, 1, 1, "ArticleId,Title,ImagePath,ArticleUrl,Summary,ArticleContent,PublishTime", "ArticleId,Title,ImagePath,ArticleUrl");
                if (mArticleDetail.mArticle.ArticleId <= 0)
                {
                    return RedirectToAction("Index", "Error");
                }
                var model = new ArticleViewDetailModel
                {
                    mArticleDetail = mArticleDetail,
                    ListTieuDiem = ArticleHelpers.GetTopByCategoryId(Constants.CateId_BieuMau, Constants.RowAmount6, "DisplayOrder DESC", "ArticleId,Title,ArticleUrl", ""),
                    WebsiteTitle = mArticleDetail.mArticle.MetaTitle.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    WebsiteDescription = mArticleDetail.mArticle.MetaDesc.StripTags().TrimmedOrDefault(mArticleDetail.mArticle.Summary.StripTags()),
                    WebsiteKeywords = mArticleDetail.mArticle.MetaKeyword.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    SeoFooter = mArticleDetail.mArticle.SeoFooter,
                    SeoHeader = mArticleDetail.mArticle.MetaTitle.TrimmedOrDefault(mArticleDetail.mArticle.Title),
                    WebsiteImage = mArticleDetail.mArticle.GetImageUrl().TrimmedOrDefault(Constants.NoImageUrl_Article),
                    WebsiteCanonical = Constants.WebsiteCanonical + mArticleDetail.mArticle.ArticleUrl
                };
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }

        #endregion Pháp lý doanh nghiệp
    }
}
