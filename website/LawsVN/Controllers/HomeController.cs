using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using LawsVN.Models;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using LawsVN.Models.Docs;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class HomeController : Controller
    {
        private int _rowCount = 0, _rowCount2 = 0, _rowCount3 = 0;
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3Minutes")]
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                HaveAmp = true,
                mArticlesViewMaster = Extensions.GetArticlesViewMaster(),
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdLeft),
                ListDocsViewFirst = DocsViewHelpers.View_GetDocsViewNewest(_currentLanguageId,
                    Constants.DocGroupIdVbpq, Constants.RowAmount10, 0, 1, 1, ref _rowCount),
                ListDocsViewSecond = DocsViewHelpers.Docs_GetViewEffect(_currentLanguageId,
                    Constants.EffectStatusSapCoHieuLuc, Constants.RowAmount5, 0, 0, 1, ref _rowCount2),
                ListDocsViewThird = DocsViewHelpers.View_GetDocsViewNewest(_currentLanguageId, Constants.DocGroupIdTcvn,
                    Constants.RowAmount5, 0, 1, 1, ref _rowCount3),
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    TotalPage = _rowCount,
                    PageIndex = 1,
                    PageSize = Constants.RowAmount10,
                    ShowNumberOfResults = Constants.RowAmount5,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH, "van-ban-moi.html"),
                    DocGroupId = Constants.DocGroupIdVbpq,
                    LanguageId = _currentLanguageId,
                    UsingDisplayTable = 1,
                    ControllerName = "Ajax",
                    ActionName = "GetDocsNewest",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "FirstBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                PartialPaginationAjaxSecond = new PartialPaginationAjax
                {
                    TotalPage = _rowCount2,
                    PageIndex = 1,
                    PageSize = Constants.RowAmount5,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH, "van-ban-sap-co-hieu-luc.html"),
                    LanguageId = _currentLanguageId,
                    UsingDisplayTable = 1,
                    ControllerName = "Ajax",
                    ActionName = "Docs_GetViewEffect",
                    EffectStatusName = "SapCoHieuLuc",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "SecondBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                PartialPaginationAjaxThird = new PartialPaginationAjax
                {
                    TotalPage = _rowCount3,
                    PageIndex = 1,
                    PageSize = Constants.RowAmount5,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH, "tieu-chuan-viet-nam.html"),
                    DocGroupId = Constants.DocGroupIdTcvn,
                    LanguageId = _currentLanguageId,
                    UsingDisplayTable = 1,
                    ControllerName = "Ajax",
                    ActionName = "GetDocsNewest",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ThirdBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                WebsiteTitle = Constants.WebsiteTitle,
                WebsiteDescription = Constants.WebsiteDescription,
                WebsiteKeywords = Constants.WebsiteKeywords,
                WebsiteCanonical = Constants.WebsiteCanonical,
                SeoHeader = Constants.SeoHeader,
                MenuItemId = 10,
                IsHomePage = true
            };
            
            return Extensions.GetViewMode("Index", homeViewModel,Request);
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LawsVnLanguages().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CaptchaImage(string prefix = "LawsVN")
        {
            //image stream 
            FileContentResult image = null;
            var mMemoryStream = Extensions.ImageStreamCaptcha(prefix);
            image = this.File(mMemoryStream.GetBuffer(), "image/Jpeg");
            return image;
        }

        [HttpGet]
        [ValidateInput(false)]
        [SEOAction]
        public ActionResult DocSearch(PartialDocSearchModel model)
        {
            int rowCount = 0,  actUserId = 0, customerId = 0;
            byte replicated = 0, isSearchExact = model.SearchExact, highlightKeyword = 1, getRowCount = 1, getCountBy = 1, getCountByDocGroupId = 0;
            string orderBy = string.Empty,
            docName = string.Empty,
            docIdentity = string.Empty,
            searchKeyword = string.Empty,
            effectStatusType = string.Empty,
            searchByDate = string.Empty;
            DateTime dtFrom, dtTo;
            DateTime.TryParseExact(model.DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom);
            string dateFrom = dtFrom != DateTime.MinValue ? dtFrom.ToString("dd/MM/yyyy") : string.Empty;
            DateTime.TryParseExact(model.DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo);
            string dateTo = dtTo != DateTime.MinValue ? dtTo.ToString("dd/MM/yyyy") : string.Empty;
            short sysMessageId = 0, displayTypeId = 0;

            if (HttpContext.Request.Browser.IsMobileDevice)
            {
                getCountBy = 0;
            }
            if (model.DocGroupId == 0)
            {
                getCountByDocGroupId = 1;
            }
            //TODO SearchOptions: Tất cả - Tiêu đề - Số hiệu văn bản
            switch (model.SearchOptions)
            {
                case 2:
                    {
                        docName = model.Keywords;
                        break;
                    }
                case 3:
                    {
                        docIdentity = model.Keywords;
                        break;
                    }
                default:
                    {
                        searchKeyword = model.Keywords;
                        break;
                    }
            }

            var docsViewSearch = DocsViewHelpers.Docs_GetViewSearchWithKeyword(actUserId, model.pSize, model.Page > 0 ? model.Page - 1 : model.Page,
                orderBy, model.LanguageId, model.DocGroupId, searchKeyword, isSearchExact, highlightKeyword, docName, docIdentity,
                model.DocTypeId, model.FieldId, 0, model.OrganId, model.SignerId, 0, displayTypeId,
                customerId, 0, model.EffectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo,
                getRowCount, getCountBy, getCountBy, getCountBy, getCountBy, getCountBy, getCountBy, ref rowCount, getCountByDocGroupId);
            
            var docsSearchModel = new DocSearchModel
            {
                Page = model.Page,
                DocsViewSearch = docsViewSearch,
                mPartialDocSearchModel = model,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageLoad = true,
                    PageIndex = model.Page,
                    Keywords = model.Keywords,
                    TotalPage = rowCount,
                    PageSize = model.pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = model.pSize,
                    LanguageId = model.LanguageId,
                    FieldId = model.FieldId,
                    EffectStatusId = model.EffectStatusId,
                    OrganId = model.OrganId,
                    DocTypeId = model.DocTypeId,
                    DocGroupId = model.DocGroupId,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    SearchOptions = model.SearchOptions ?? 0,
                    IsSearchExact = model.SearchExact,
                    SignerId = model.SignerId,
                    SignerName = model.SignerName,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Docs_GetViewSearchWithKeyword",
                    ClassTagItem = "tag-item page",
                    EffectStatusName = string.Empty,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        HttpMethod = "Post",
                        UpdateTargetId = "ListDocsViews",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    }
                },
                Keywords = model.Keywords,
                SearchOptions = model.SearchOptions,
                SearchExact = model.SearchExact,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                DocTypeId = model.DocTypeId,
                OrganId = model.OrganId,
                EffectStatusId = model.EffectStatusId,
                FieldId = model.FieldId,
                LanguageId = model.LanguageId,
                SignerId = model.SignerId,
                SignerName = model.SignerName,
                DocGroupId = model.DocGroupId
            };
            
            if (docsSearchModel.DocsViewSearch.lDocsView.HasValue())
            {
                docsSearchModel.DocsViewSearch.lDocsView = (from a in docsSearchModel.DocsViewSearch.lDocsView
                    join b in docsSearchModel.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId
                    select new DocsView
                    {
                        DocId = a.DocId,
                        DocGroupId = a.DocGroupId,
                        DocUrl = a.GetDocUrl(),
                        DocName = a.DocName,
                        LanguageId = a.LanguageId,
                        IssueYear = a.IssueYear,
                        IssueDate = a.IssueDate,
                        EffectDate = a.EffectDate,
                        EffectStatusId = b.EffectStatusId,
                        EffectStatusName = _currentLanguageId == LawsVnLanguages.AvailableLanguages[0].LanguageId ? b.EffectStatusDesc : b.EffectStatusName
                    }).ToList();
            }
            return Extensions.GetViewMode("DocSearch", docsSearchModel);
        }

        [SEOAction]
        public ActionResult Service()
        {
            short articleId = 12097;
            var model = new ServiceModel
            {
                ListCustomerServices = _currentCustomerId > 0 ? new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty) : null,
                mArticlesViewDetailCompare = ArticlesViewHelpers.View_GetArticleDetail(articleId, 0, 0, 0),
                MenuItemId = 686
            };
            return Extensions.GetViewMode("Service", model);
        }
    }
}
