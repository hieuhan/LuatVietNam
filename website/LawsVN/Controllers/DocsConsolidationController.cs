using System;
using System.Linq;
using System.Web.Mvc;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using LawsVN.Models.Docs;
using LawsVN.Models;
using System.Web.Mvc.Ajax;
using ICSoft.LawDocsLib;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class DocsConsolidationController : Controller
    {
        private byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdVbhn;
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdVbhn;
        private readonly byte _docTypeId = Constants.DocTypesIdVbhn;

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Index(int page = 0, int pSize = 20, int year = 0, short organId = 0, short fieldId = 0)
        {
            int rowCount = 0;
            string orderBy = string.Empty, searchKeyword = string.Empty, 
                dateFrom = year > 0 ? string.Concat("01/01/", year) : string.Empty, dateTo = year > 0 ? string.Concat("31/12/", year) : string.Empty;
            var model = new DocsConsolidationViewModel
            {
                Page = page,
                HaveAmp = true,
                mArticlesViewCate = ArticlesViewHelpers.View_GetArticleCate(Constants.CateIdHotNewsLaw,
                    Constants.RowAmount3,
                    0, Constants.RowAmount3, 0),
                mDocsViewSearch = DocsViewHelpers.Docs_GetViewSearchWithCount(0, pSize, page > 0 ? page - 1 : page, orderBy,
                    _languageId
                    , _docGroupId, searchKeyword, 0, 0, "", "", _docTypeId, fieldId, 0, organId, 0, 0, 0, 0, 0, 0, "", 0, 0, 0,
                    "", dateFrom, dateTo, 1
                    , 0, 1, 1, 0, 0, 1, ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Extensions.IsMobile() == true ? Constants.RowAmount3 : Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    LanguageId = _languageId,
                    DocGroupId = _docGroupId,
                    DocTypeId = _docTypeId,
                    DisplayTypeId = _displayTypeId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    ClassTagItem = "tag-item item",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.ListOnSuccess"
                    }
                }
            };
            if (model.mDocsViewSearch.lDocsView.HasValue())
            {
                model.mDocsViewSearch.lDocsView = (from a in model.mDocsViewSearch.lDocsView
                    join b in model.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId
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
                        EffectStatusName = _languageId == LawsVnLanguages.AvailableLanguages[0].LanguageId ? b.EffectStatusDesc : b.EffectStatusName
                    }).ToList();
            }
            return Extensions.GetViewMode("Index", model, Request);
        }

        /// Danh sách văn bản theo lĩnh vực
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListByField(short fieldId = 0)
        {
            int rowCount = 0, customerId = 0;
            string orderBy = string.Empty, effectStatusType = string.Empty,
                searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty,
                searchByDate = string.Empty, dateFrom = string.Empty, dateTo = string.Empty;
            byte effectStatusId = 0, getRowCount = 1,
                displayTypeId = 0, isSearchExact = 0, docTypeId = 0;
            short signerId = 0, organId = 0;
            var model = new DocFieldModel
            {
                DisplayTypeIdDocTypeId = _displayTypeId,
                DisplayTypeIdOrganId = _displayTypeId,
                DisplayTypeId = _displayTypeId,
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount20, 0, orderBy, _languageId, _docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListDocsViewMostView = DocsViewHelpers.Docs_GetViewMostView(_languageId, _docGroupId, fieldId, Constants.RowAmount5),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    LanguageId = _languageId,
                    FieldId = fieldId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    DocGroupId = _docGroupId,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.ListOnSuccess"
                    }
                }
            };
            model.mFieldDisplays = model.ListFieldDisplays.FirstOrDefault(f => f.FieldId == fieldId);
            return View("~/Views/Docs/ListByField.cshtml",model);
        }

        /// <summary>
        /// Chi tiết văn bản
        /// </summary>
        /// 
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Properties(int docId = 0, string tab = "", short relateTypeId = 0)
        {
            int actUserId = 0;
            byte replicated = 0;
            short sysMessageId = 0;
            _languageId = (byte)("tienganh".Equals(tab) ? 2 : _languageId);
            if (docId > 0)
            {
                var model = new DocsViewDetailModel
                {
                    LanguageId = _languageId,
                    RelateTypeId = relateTypeId,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, _languageId)
                };

                if (model.mDocsViewDetail.mDocsView.DocId > 0)
                {
                    // TODO: logs xem văn bản
                    new DocViewLogs
                    {
                        DocId = model.mDocsViewDetail.mDocsView.DocId,
                        DocGroupId = model.mDocsViewDetail.mDocsView.DocGroupId,
                        DocFileId = 0,
                        LanguageId = _languageId,
                        ApplicationTypeId = 1,
                        UserAgent = Request.UserAgent,
                        FromIP = Request.UserHostAddress,
                        RefererFrom = HttpContext.Request.UrlReferrer != null
                            ? HttpContext.Request.UrlReferrer.ToString()
                            : "N/A",
                        CustomerId = Extensions.GetCurrentCustomerId(),
                        ActionTypeId = tab.ActionTypeIdByTab()
                    }.Insert(replicated, actUserId, ref sysMessageId);

                    // TODO: SEO
                    model.WebsiteTitle = model.mDocsViewDetail.mDocsView.MetaTitle;
                    model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc;
                    model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword;
                    FieldDisplays fieldFirsts = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirsts != null) model.FieldId = fieldFirsts.FieldId;
                    //ToDo tab Tải về
                    if (tab.Equals("taive"))
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().DocFiles_GetList(0,string.Empty, docId);
                        return Extensions.GetViewMode("Download", model);
                        //return View("~/Views/DocsConsolidation/Download.cshtml", model);
                    }
                    //ToDo tab Nội dung
                    if (tab.Equals("noidung"))
                    {
                        
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        }
                        return Extensions.GetViewMode("Detail", model);
                        //return View("~/Views/DocsConsolidation/Detail.cshtml", model);
                    }
                    //ToDo tab Lược đồ
                    if (tab.Equals("luocdo"))
                    {
                        return Extensions.GetViewMode("Diagram", model);
                        //return View("~/Views/DocsConsolidation/Diagram.cshtml", model);
                    }
                    //ToDo tab Liên quan
                    if (tab.Equals("lienquan"))
                    {
                        return Extensions.GetViewMode("Relate", model);
                        //return View("~/Views/DocsConsolidation/Relate.cshtml", model);
                    }
                    //ToDo tab Tóm tắt
                    else
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        return Extensions.GetViewMode("Properties", model);
                    }

                    //return View(model);

                } return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DetailFull(int docId = 0, string tab = "", short relateTypeId = 0, int id = 0, int cid = 0)
        {
            if (docId > 0)
            {
                var query = Extensions.GetUrlParameter(Request, "layout");
                var model = new DocsViewDetailModel
                {
                    HaveAmp = true,
                    LanguageId = _languageId,
                    RelateTypeId = relateTypeId,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, _languageId),
                };
                
                if (model.mDocsViewDetail.mDocsView.DocId > 0)
                {
                    if (!Request.RawUrl.Contains(model.mDocsViewDetail.mDocsView.DocUrl))
                    {
                        Response.RedirectPermanent(model.mDocsViewDetail.mDocsView.DocUrl);
                    }
                    if (model.mDocsViewDetail.lDocOthersNewest != null)
                    {
                        model.mDocsViewDetail.lDocOthersNewest.RemoveAll(m =>
                            m.DocId == model.mDocsViewDetail.mDocsView.DocId);

                        if (model.mDocsViewDetail.lDocOthers != null)
                        {
                            model.mDocsViewDetail.lDocOthersNewest.AddRange(model.mDocsViewDetail.lDocOthers);
                        }
                    }
                    else if (model.mDocsViewDetail.lDocOthers != null)
                    {
                        model.mDocsViewDetail.lDocOthersNewest = model.mDocsViewDetail.lDocOthers;
                    }
                    // TODO: SEO
                    if (!String.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.MetaTitle))
                    {
                        model.WebsiteTitle = model.mDocsViewDetail.mDocsView.MetaTitle;
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc;
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword;
                    }
                    else if (!String.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.H1Tag))
                    {
                        model.WebsiteTitle = model.mDocsViewDetail.mDocsView.H1Tag;
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.H1Tag);
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.H1Tag);
                    }
                    else
                    {
                        string titleFromProperties = model.mDocsViewDetail.mDocsView.DocTypesText.Trim();
                        if (model.mDocsViewDetail.mDocsView.DocName.Length <= 75)
                        {
                            titleFromProperties = model.mDocsViewDetail.mDocsView.DocName.Replace("\"", "");
                        }
                        else
                        {
                            titleFromProperties += " " + model.mDocsViewDetail.mDocsView.DocIdentity.Trim();
                            titleFromProperties += " của " + model.mDocsViewDetail.mDocsView.OrgansText.Trim();
                            titleFromProperties += " ban hành ngày " + model.mDocsViewDetail.mDocsView.IssueDate.ToString("dd/MM/yyyy");
                        }
                        model.WebsiteTitle = titleFromProperties;
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.DocName.Replace("\"", ""));
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                    }
                    FieldDisplays fieldFirst = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirst != null) model.FieldId = fieldFirst.FieldId;
                    model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);

                    if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                    {
                        model.mDocsViewDetail.mDocsView.DocSummary = GoogleAmp.DocConvert(model.mDocsViewDetail.mDocsView.DocSummary);
                        model.mDocsViewDetail.mDocsView.DocContent =
                            GoogleAmp.DocConvert(model.mDocsViewDetail.mDocsView.DocContent);
                    }
                    //ToDo log email click
                    if (id > 0 && cid > 0)
                    {
                        new MessageSends
                        {
                            MessageSendId = id,
                            CampaignId = cid,
                            SendTime = DateTime.Now
                        }.MessageSends_UpdateClickLinkTime_Quick();
                    }
                    return Extensions.GetViewMode("DetailFull", model, Request);
                }
                return RedirectToAction("Error404", "Error");
            }
            return RedirectToAction("Error404", "Error");
        }
    }
}
