using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.ViewLibV3;
using Newtonsoft.Json;
using VanBanLuat.AppCode.Attribute;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using VanBanLuatVN.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuat.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3Minutes")]
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                IsHomePage = true,
                MenuItemId = Constants.MenuItemIdHomePage,
                DocList = DocHelpers.GetByDocGroupId(Constants.DocGroupIdVbpq,"DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId","IssueDate DESC",Constants.RowAmount20, 0,Constants.LanguageId, 1),
                SearchParams = new DocFilterParams { DocGroupId = Constants.DocGroupIdVbpq,PageIndex = 0,RowAmount = Constants.RowAmount20}
            };
            return View(model);
        }

        [HttpGet]
        [SEOAction]
        [ValidateInput(false)]
        public ActionResult DocSearch(DocViewModel.DocSearchModel model)
        {
            var docFilterParams = new DocFilterParams
            {
                PageIndex = model.Page > 0 ? model.Page-1: model.Page,
                IsSearchExact = model.SearchExact,
                DocGroupId = model.DocGroupId,
                DocTypeId = model.DocTypeId,
                EffectStatusId = model.EffectStatusId,
                FieldId = model.FieldId,
                OrganId = model.OrganId,
                SignerId = model.SignerId,
                SearchByDate = model.SearchByDate,
                FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                GetRowCount = 1,
                GetCountByGroup = 1,
                GetCountByEffectStatus = 1,
                GetCountByField = 1,
                GetCountByOrgan = 1,
                GetCountByYear = 1,
                GetCountByDocType = 1,
                GetEffectStatusName = 1,
                RowAmount = Constants.RowAmount20
            };
            DateTime dtFrom, dtTo;
            DateTime.TryParseExact(model.DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom);
            docFilterParams.DateFrom = dtFrom;
            DateTime.TryParseExact(model.DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo);
            docFilterParams.DateTo = dtTo;

            //TODO SearchOptions: Tất cả - Tiêu đề - Số hiệu văn bản
            switch (model.SearchOptions)
            {
                case 2:
                    docFilterParams.DocName = model.Keywords;
                    break;
                case 3:
                    docFilterParams.DocIdentity = model.Keywords;
                    break;
                default:
                    docFilterParams.SearchKeyword = model.Keywords;
                    break;
            }
            model.DocList = DocHelpers.GetPage(docFilterParams);
            model.PageSize = Constants.RowAmount20;
            // TODO logs tìm kiếm
            if (!string.IsNullOrEmpty(model.Keywords))
            {
                DocHelpers.InsertSearchLog(model.Keywords, docFilterParams.DateFrom, docFilterParams.DateTo,
                    docFilterParams.DocTypeId, docFilterParams.OrganId, docFilterParams.SignerId,
                    docFilterParams.FieldId, Constants.LanguageId, Extensions.GetCurrentCustomerId());
            }
            return View(model);
        }

    }
}
