using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.Library;
using LawsVNEN.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LawsVNEN.AppCode.Attribute;

namespace LawsVNEN.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();

        public ActionResult Index()
        {
            int rowCount = 0;
            int displayTypeId = Constants.DisplayTypeIdVBMoi;
            var homeViewModel = new HomeViewModel
            {
                ListDocsView = DocsViewHelpers.View_GetDocsViewNewestEN(_currentLanguageId, Constants.DocGroupIdVbpq,displayTypeId, Constants.RowAmount20, 0, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    ShowNumberOfResults = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH, "new-documents.html"),
                    DocGroupId = Constants.DocGroupIdVbpq,
                    LanguageId = _currentLanguageId,
                    DisplayTypeId = (short)displayTypeId,
                    ControllerName = "Ajax",
                    ActionName = "GetDocsNewest",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "boxcontent",
                        InsertionMode = InsertionMode.Replace
                    }
                },
            };
            return View(homeViewModel);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult DocSearch(PartialDocSearchModel model)
        {
            int rowCount = 0, actUserId = 0, customerId = 0;
            byte replicated = 0, isSearchExact = model.SearchExact,
                languageId = model.LanguageId > 0 ? model.LanguageId : LawsVnLanguages.AvailableLanguages[0].LanguageId;
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

            //TODO Điều kiện sắp xếp theo Table Docs
            var listOrderByClauses = OrderByClauses.Static_GetList("Docs");
            if (listOrderByClauses.Count>0)
            {
                orderBy = listOrderByClauses[0].OrderBy;
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
            
            //if (!string.IsNullOrEmpty(model.SignerName))
            //{
            //    Signers mSigners = new Signers();
            //    string signerNameResult = "";
            //    DataSet data = mSigners.Signers_GetIdAndNameByJson(model.SignerName, 10, ref signerNameResult);
            //    if (!string.IsNullOrEmpty(signerNameResult))
            //    {
            //        List<Signers> stuff = JsonConvert.DeserializeObject<List<Signers>>(signerNameResult);
            //        if (stuff.HasValue())
            //        {
            //            model.SignerId = stuff[0].SignerId;
            //        }
            //    }
            //}
            
            var docsViewSearch = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(actUserId, Constants.RowAmount20, model.Page > 0 ? model.Page - 1 : model.Page,
                orderBy, languageId, model.DocGroupId, searchKeyword, isSearchExact, 1, docName, docIdentity,
                model.DocTypeId, model.FieldId, 0, model.OrganId, model.SignerId, 0, displayTypeId,
                customerId, 0, model.EffectStatusId, effectStatusType, 0,0, model.FileTypeId,0, searchByDate, dateFrom, dateTo,
                1, 0, 0, 0, 0, 0, 0, ref rowCount);

            var docsSearchModel = new DocSearchModel
            {
                Page = model.Page,
                ListOrderByClauses = listOrderByClauses,
                DocsViewSearch = docsViewSearch,
                mPartialDocSearchModel = model,
                 mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = model.Page,
                    KeyWord = model.Keywords,
                    SearchOptions = model.SearchOptions,
                    SearchExact = model.SearchExact,
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    PageLoad = true,
                    UrlPaging = string.Empty,
                    LanguageId = _currentLanguageId,
                    FieldId = model.FieldId,
                    EffectStatusId = model.EffectStatusId,
                    OrganId = model.OrganId,
                    DocTypeId = model.DocTypeId,
                    DocGroupId = model.DocGroupId,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    SignerName = model.SignerName,
                    SignerId = model.SignerId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Search_GetViewSearch",
                    EffectStatusName = "",
                    FileTypeId = model.FileTypeId,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListDocsViews",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    }
                }
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

            return View(docsSearchModel);
        }
        public ActionResult CaptchaImage(string prefix = "LawsVNEN")
        {
            //image stream 
            FileContentResult image = null;
            var mMemoryStream = Extensions.ImageStreamCaptcha(prefix);
            image = this.File(mMemoryStream.GetBuffer(), "image/Jpeg");
            return image;
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LawsVnLanguages().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }
    }
}
