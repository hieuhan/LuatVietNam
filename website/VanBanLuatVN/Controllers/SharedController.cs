using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using VanBanLuatVN.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuat.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        [ChildActionOnly]
        public ActionResult PartialHeader(byte docGroupId = 1,short menuItemId= 0, bool isHomePage = false)
        {
            var model = new SharedViewModel.HeaderModel
            {
                DocGroupId = docGroupId,
                IsHomePage = isHomePage,
                MenuItemId = menuItemId,
                ListMenuItems = ListHelpers.GetListByMenuId(Constants.MenuIdHeader),
                ListMenuItemsMobile = ListHelpers.GetListByMenuId(Constants.MenuIdHeaderMobile),
                ListFields = ListHelpers.FieldsGetListByDisplayType(Constants.FieldDisplaysDisplayTypeIdVbpq)
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialSEOPagination(int page = 0, int totalPage = 0, int pageSize = 20)
        {
            var model = new PaginationModel
            {
                Page = page,
                PageSize = pageSize,
                TotalPage = totalPage
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFooter()
        {
            var model = new SharedViewModel.FooterModel
            {
                ListMenuItems = ListHelpers.GetListByMenuId(Constants.MenuIdFooter)
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialAdvertise(int advertPositionId)
        {
            return PartialView(advertPositionId);
        }

        [ChildActionOnly]
        public ActionResult PartialTinXemNhieu()
        {
            var model = new NewViewModel
            {
                mCategoryArticles = ArticleHelpers.GetMostView(Constants.SiteId, 0, Constants.RowAmount6, "ArticleId,Title,ArticleUrl,ImagePath")
            };
            return PartialView(model);
        }

        //[ChildActionOnly]
        //public ActionResult PartialChinhSachMoi()
        //{
        //    var model = new NewViewModel
        //    {
        //        mCategoryArticles = ArticleHelpers.GetTopByCategoryId(562, Librarys.Constants.RowAmount5, "*", "*")
        //    };
        //    return PartialView(model);
        //}

        /// <summary>
        /// Bộ lọc tìm văn bản
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocFilter()
        {
            return PartialView(ListHelpers.GetDocListFilterOrderByDocCount());
        }

        /// <summary>
        /// Box tra cứu văn bản trang chủ
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFieldOfSearchHome(byte docGroupId = 1)
        {
            var model = new SharedViewModel.FieldOfSearchModel
            {
                DocGroupId = docGroupId,
                ListFields = ListHelpers.FieldsGetListByDisplayType(Constants.FieldDisplaysDisplayTypeIdVbpq),
                ListMenuItems = ListHelpers.GetListByMenuId(Constants.MenuIdLeft)
            };
            return PartialView(model);
        }

        /// <summary>
        /// Box tra cứu văn bản chi tiết văn bản
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFieldOfSearchDocDetail(byte docGroupId = 1)
        {
            var model = new SharedViewModel.FieldOfSearchModel
            {
                DocGroupId = docGroupId,
                ListFields = ListHelpers.FieldsGetListByDisplayType(Constants.FieldDisplaysDisplayTypeIdVbpq),
                ListMenuItems = ListHelpers.GetListByMenuId(Constants.MenuIdLeft)
            };
            return PartialView(model);
        }

        /// <summary>
        /// Box tra cứu header
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFieldOfSearchHeader(byte docGroupId = 1)
        {
            var model = new SharedViewModel.FieldOfSearchModel
            {
                DocGroupId = docGroupId,
                ListFields = ListHelpers.FieldsGetListByDisplayType(Constants.FieldDisplaysDisplayTypeIdVbpq),
                ListMenuItems = ListHelpers.GetListByMenuId(Constants.MenuIdLeft)
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialDocumentAttribute(SharedViewModel.DocumentAttribute model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialLogin()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult PartialRegister()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult PartialNewsLetterEmail()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult PartialPagination(PaginationModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialYeuCauVanBan()
        {
            return PartialView();
        }
        //[ChildActionOnly]
        //public ActionResult ChinhSachMoi(List<Articles> model)
        //{
        //    return PartialView("~/Views/Shared/ChinhSachMoi.cshtml", model);
        //}

        /// <summary>
        /// Danh sách tin theo chuyên mục và view
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialArticlesGetByCategoryId(short categoryId = 0, int rowAmount = 5, string viewName = "")
        {
            var model = ArticleHelpers.GetTopByCategoryId(categoryId, rowAmount, "PublishTime DESC,DisplayOrder DESC", "ArticleId,Title,ImagePath,ArticleUrl,PublishTime");
            return PartialView(viewName, model);
        }

        [ChildActionOnly]
        public ActionResult PartialArticlesBieuMauGetByCategoryId(short categoryId = 0, int rowAmount = 5, string viewName = "")
        {
            var model = ArticleHelpers.GetPage(new ArticleFilterParams
            {
                SiteId = Constants.SiteId,
                CategoryId = categoryId,
                ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                ArticleFieldSelect = "ArticleId,Title,ImagePath,ArticleUrl,PublishTime",
                RowAmount = rowAmount,
                OrderBy = "DisplayOrder DESC"
            });
            //var model = ArticleHelpers.GetTopByCategoryId(categoryId, rowAmount, "PublishTime DESC,DisplayOrder DESC", "ArticleId,Title,ImagePath,ArticleUrl,PublishTime");
            return PartialView(viewName, model);
        }

        /// <summary>
        /// Danh sách tin theo chuyên mục và view
        /// </summary>
        /// <param name="mCategoryArticles"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialShowViewByListArticle(CategoryArticles mCategoryArticles, string viewName = "", string categoryName="")
        {
            if (mCategoryArticles != null && !string.IsNullOrEmpty(categoryName))
                mCategoryArticles.mCategory.MetaTitle = categoryName;
            return PartialView(viewName, mCategoryArticles);
        }

        /// <summary>
        /// Danh sách chuyên mục con và view
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialGetListCateIdByParentId(short parentId = 0, short categoryId = 0, string viewName = "")
        {
            NewViewModel model = new NewViewModel
            {
                CategoryId = categoryId,
                lCategories = CategoryHelpers.GetByParentItemId(parentId, "CategoryId,CategoryDesc,CategoryUrl")
            };
            return PartialView(viewName, model);
        }

        /// <summary>
        /// Danh sách văn bản theo displayTypeId và view 
        /// </summary>
        /// <param name="displayTypeId"></param>
        /// <param name="boxTitle"></param>
        /// <param name="rowAmount"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocumentsByGetByDisplayTypeId(short displayTypeId = 0, string boxTitle="", int rowAmount = 5, string viewName = "")
        {
            var model = new SharedViewModel.DocumentsByGetByDisplayType
            {
                DocList = DocHelpers.GetByDisplayTypeId(displayTypeId,
                    "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId", rowAmount, 0, Constants.LanguageId),
                SeoHeader = boxTitle
            };
            return PartialView(viewName, model);
        }

        #region Documents

        /// <summary>
        /// Lược đồ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PartialDocDiagram(DocViewModel.DocDetailModel model)
        {
            model.AllDocRelateLists = DocHelpers.GetAllDocRelatesVN(model.Docs.DocId);
            model.ListRelateTypesByGroupId = RelateTypes.Static_GetListByDocGroupId(model.Docs.DocGroupId);
            return PartialView("~/Views/Shared/Document/PartialDocDiagram.cshtml", model);
        }

        /// <summary>
        /// Lược đồ Tiêu chuẩn Việt Nam
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PartialDocVietNamStandardsDiagram(DocViewModel.DocDetailModel model)
        {
            model.AllDocRelateLists = DocHelpers.GetAllDocRelatesVN(model.Docs.DocId);
            model.ListRelateTypesByGroupId = RelateTypes.Static_GetListByDocGroupId(model.Docs.DocGroupId);
            return PartialView("~/Views/Shared/Document/PartialDocVietNamStandardsDiagram.cshtml", model);
        }

        /// <summary>
        /// Lược đồ văn bản hợp nhất
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PartialDocConsolidationDiagram(DocViewModel.DocDetailModel model)
        {
            model.AllDocRelateLists = DocHelpers.GetAllDocRelatesVN(model.Docs.DocId);
            model.ListRelateTypesByGroupId = RelateTypes.Static_GetListByDocGroupId(model.Docs.DocGroupId);
            return PartialView("~/Views/Shared/Document/PartialDocConsolidationDiagram.cshtml", model);
        }

        /// <summary>
        /// Liên quan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PartialDocRelate(DocViewModel.DocDetailModel model)
        {
            //Todo ds văn bản liên quan
            model.DocRelateLists = DocHelpers.GetDocRelates(model.Docs.DocId, model.Pagination.RelateTypeId, model.Pagination.DisplayPosition, Constants.RowAmount20, model.Pagination.Page > 0 ? model.Pagination.Page - 1 : model.Pagination.Page, 1,
                Constants.LanguageId, 1);
            if (model.DocRelateLists.lCountByRelateType.HasValue())
            {
                model.CountByRelateTypeId = model.DocRelateLists.lCountByRelateType.Sum(x => x.DocCount);
            }
            return PartialView("~/Views/Shared/Document/PartialDocRelate.cshtml", model);
        }

        /// <summary>
        /// ds văn bản liên quan hiệu lực
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PartialDocEffect(DocViewModel.DocDetailModel model)
        {
            model.DocRelateListsEffect = DocHelpers.GetDocRelatesEffect(model.Docs.DocId, Constants.LanguageId, 1);
            return PartialView("~/Views/Shared/Document/PartialDocEffect.cshtml", model);
        }

        public ActionResult PartialRelateFieldDoc(DateTime issueDate, short fieldId=0)
        {
            var docRelateNewest = DocHelpers.GetPage(new DocFilterParams
            {
                FieldSelect = "DocId,DocName,DocUrl",
                FieldId = fieldId,
                RowAmount = Constants.RowAmount5,
                HighlightKeyword = 0,
                DateFrom = issueDate != DateTime.MinValue ? issueDate.AddDays(1) : DateTime.MinValue
            });
            var docRelate = issueDate != DateTime.MinValue ? DocHelpers.GetPage(new DocFilterParams
            {
                FieldSelect = "DocId,DocName,DocUrl",
                FieldId = fieldId,
                RowAmount = Constants.RowAmount5,
                HighlightKeyword = 0,
                DateTo = issueDate.AddDays(-1)
            }) : new DocList();
            var model = new DocViewModel.RelateFieldDoc
            {
                ListDocRelateNewest = docRelateNewest != null && docRelateNewest.lDocs.HasValue() ? docRelateNewest.lDocs : new List<Docs>(),
                ListDocRelate = docRelate != null && docRelate.lDocs.HasValue() ? docRelate.lDocs : new List<Docs>()
            };
            return PartialView("~/Views/Shared/Document/PartialRelateFieldDoc.cshtml", model);
        }

        #endregion
    }
}
