using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using LawsVN.Models;
using LawsVN.Models.Docs;
using ICSoft.CMSLib;
using LawsVN.AppCode.Attribute;
using LawsVN.Models.Account;

namespace LawsVN.Controllers
{
    public class DocsCongVanController : Controller
    {
        private byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdCongVan;
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdCongVan;
        private readonly byte _docTypeId = Constants.DocTypesIdCongVan;
        //
        // GET: /DocsView/

        /// <summary>
        /// View công văn thuế - hải quan
        /// </summary>
        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Index(short fieldId = 4, int page = 0, int pSize = 20, byte effectStatusId = 0)
        {
            int rowCount = 0, customerId = 0;
            string orderBy = string.Empty, effectStatusType = string.Empty,
                searchKeyword = string.Empty, docName = string.Empty,
                docIdentity = string.Empty, searchByDate = string.Empty,
                dateFrom = string.Empty, dateTo = string.Empty;
            short signerId = 0, organId = 0;
            byte isSearchExact = 0, displayTypeId = 0, getRowCount = 1;
            var model = new CongVanModel
            {
                Page = page,
                HaveAmp = true,
                ListArticlesView = ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdTinVbMoi, 1, Constants.RowAmount5, string.Empty), // điểm tin văn bản mới
                ListArticlesViewCapNhatHangTuan = ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdCapNhatHangTuan, 1, Constants.RowAmount20, string.Empty),
                ListCongVanThueMoi = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, "", 0,
                    Constants.DocGroupIdCongVan, "", 0, 0, "", "", Constants.DocTypesIdCongVan, 4, 0, 0, 0, 0, 0, 0, 0, 0,
                    "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListCongVanHaiQuanMoi = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, "", 0,
                    Constants.DocGroupIdCongVan, "", 0, 0, "", "", Constants.DocTypesIdCongVan, 21, 0, 0, 0, 0, 0, 0, 0,
                    0, "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pSize, page > 0 ? page - 1 : page, orderBy, _languageId,
                    _docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, _docTypeId, fieldId, 0, organId,
                    signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate,
                    dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListDocsViewMostView = DocsViewHelpers.Docs_GetViewMostView(_languageId, _docGroupId, fieldId, Constants.RowAmount5),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    LanguageId = _languageId,
                    DocGroupId = _docGroupId,
                    FieldId = fieldId,
                    DocTypeId = _docTypeId,
                    PostTimeRight = "page",
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    ClassTagItem = "tag-item item7",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.ListOnSuccess"
                    }
                }
            };
            if (model.ListDocsView.HasValue())
            {
                model.ListDocsView = model.ListDocsView.Join(model.ListEffectStatus, a => a.EffectStatusId,
                    b => b.EffectStatusId, (a, b) => new DocsView
                    {
                        DocId = a.DocId,
                        DocName = a.DocName,
                        DocGroupId = a.DocGroupId,
                        DocUrl = a.DocUrl,
                        DocSummary = a.DocSummary,
                        IssueYear = a.IssueYear,
                        IssueDate = a.IssueDate,
                        EffectDate = a.EffectDate,
                        EffectStatusId = b.EffectStatusId,
                        EffectStatusName = b.EffectStatusDesc
                    }).ToList();
            }
            return Extensions.GetViewMode("Index", model, Request);
        }

        /// Danh sách văn bản theo lĩnh vực
        /// 
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListByField(short fieldId = 0)
        {
            int seoid = 13;
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
                    LinkLimit = Extensions.IsMobile() == true ? Constants.RowAmount3 : Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    LanguageId = _languageId,
                    FieldId = fieldId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    DocGroupId = _docGroupId,
                    DisplayTypeId = _displayTypeId,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            model.mFieldDisplays = model.ListFieldDisplays.FirstOrDefault(f => f.FieldId == fieldId);
            if (model.mFieldDisplays != null && !string.IsNullOrEmpty(model.mFieldDisplays.FieldName))
            {
                var seos = new Seos().Get(seoid);
                if (seos.SeoId > 0)
                {

                    model.WebsiteTitle = seos.MetaTitle.Replace("[FieldName]", model.mFieldDisplays.FieldName);
                    model.WebsiteDescription =
                        seos.MetaDesc.Replace("[FieldName]", model.mFieldDisplays.FieldName);
                    model.WebsiteKeywords =
                        seos.MetaKeyword.Replace("[FieldName]", model.mFieldDisplays.FieldName);
                    model.SeoHeader = seos.H1Tag.Replace("[FieldName]", model.mFieldDisplays.FieldName);
                }
            }
            return Extensions.GetViewMode("ListByField", model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Properties(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0)
        {
            int actUserId = 0;
            byte replicated = 0;
            short sysMessageId = 0;
            _languageId = (byte)("tienganh".Equals(tab) ? 2 : _languageId);
            if (docId > 0)
            {
                var model = new DocsViewDetailModel
                {
                    DocGroupId = docGroupId,
                    LanguageId = _languageId,
                    RelateTypeId = relateTypeId,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, _languageId),
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
                    FieldDisplays fieldDisplayFirst = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldDisplayFirst != null) model.FieldId = fieldDisplayFirst.FieldId;
                    // ToDo tab Nội dung
                    if (tab.Equals("noidung"))
                    {
                        model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        }
                        return Extensions.GetViewMode("Detail", model);
                    }
                    // ToDo tab Hiệu lực
                    if (tab.Equals("hieuluc"))
                    {
                        return Extensions.GetViewMode("Effect", model);
                    }
                    // ToDo tab Liên quan
                    if (tab.Equals("lienquan"))
                    {
                        return Extensions.GetViewMode("Relate", model);
                    }
                    // ToDo tab Tải về
                    if (tab.Equals("taive"))
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        return Extensions.GetViewMode("Download", model);
                    }

                    // TODO: tab Lược đồ
                    if (tab.Equals("luocdo"))
                    {
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Lược đồ");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Lược đồ");
                        return Extensions.GetViewMode("Diagram", model);
                    }

                    // ToDo tab Thuộc tính
                    else
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        return Extensions.GetViewMode("Properties", model);
                    }

                } return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DetailFull(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0, int id = 0, int cid = 0)
        {
            _languageId = ICSoft.HelperLib.LanguageHelpers.Vietnamese;
            var query = Extensions.GetUrlParameter(Request, "layout");
            if (docId > 0)
            {
                var model = new DocsViewDetailModel
                {
                    HaveAmp = true,
                    DocGroupId = docGroupId,
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
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.MetaTitle);
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.MetaTitle);
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
                    FieldDisplays fieldDisplayFirst = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldDisplayFirst != null) model.FieldId = fieldDisplayFirst.FieldId;
                    // ToDo tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                    {
                        string docContent = model.mDocsViewDetail.mDocsView.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.mDocsViewDetail.mDocsView.DocContent = docContent;
                    }
                    model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                    model.ListDocFields =
                        model.mDocsViewDetail.mDocsView.FieldsText.GetListFieldsDocOthers(model.ListFields);

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
                    return Extensions.GetViewMode("DetailFull", model,Request);
                }
                return RedirectToAction("Error404", "Error");
            }
            return RedirectToAction("Error404", "Error");
        }
    }
}
