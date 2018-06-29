using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.AppCode.Attribute;
using LawsVN.Library;
using LawsVN.Library.Sercurity;
using LawsVN.Models;
using LawsVN.Models.Docs;

namespace LawsVN.Controllers
{
    public class DocsController : Controller
    {
        private byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdVbpq;
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdVbpq;
        private readonly short _displayTypeIdDocTypeId = Constants.DocTypeIdDisplayTypeIdVbpq;
        private readonly short _displayTypeIdOrganId = Constants.DocTypeIdDisplayTypeIdVbpq;
        private readonly short _displayTypeIdByField = Constants.DisplayTypeIdByField;
        /// <summary>
        /// Danh sách văn bản theo lĩnh vực
        /// </summary>
        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]// co box van ban theo nguoi dung, chua cache 
        public ActionResult ListByField(int page = 0, int pSize = 20, short fieldId = 0, byte docGroupId = 0, byte effectStatusId = 0, byte docTypeId = 0, short organId = 0, string effectStatusName = "", int id= 0, int cid = 0)
        {
            int rowCount = 0, rowCount2 = 0, customerId = 0;
            string orderBy = string.Empty,
                searchKeyword = string.Empty,
                docName = string.Empty,
                docIdentity = string.Empty,
                searchByDate = string.Empty,
                dateFrom = string.Empty,
                dateTo = string.Empty;
            byte getRowCount = 1,
                isSearchExact = 0, displayTypeId = 0;
            short signerId = 0;

            var docModel = new DocFieldModel
            {
                Page = page,
                HaveAmp = true,
                DisplayTypeId = _displayTypeId,
                DisplayTypeIdDocTypeId = _displayTypeIdDocTypeId,
                DisplayTypeIdOrganId = _displayTypeIdOrganId,
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pSize, page > 0 ? page - 1 : page, orderBy, _languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusName, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListDocsViewMostView = DocsViewHelpers.Docs_GetViewMostView(_languageId, docGroupId, fieldId, Constants.RowAmount5),
                ListDocDisplaysByField = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, orderBy, _languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, 0, fieldId, 0, 0, signerId, 0, _displayTypeIdByField, customerId, 0, 0, string.Empty, 0, 0, 0, searchByDate, dateFrom, dateTo, 0, 0, 0, 0, 0, 0, 0, ref rowCount2),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    UrlPaging = string.Empty,
                    LanguageId = _languageId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    DocGroupId = docGroupId,
                    DisplayTypeId = _displayTypeId,
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    DocTypeId = docTypeId,
                    OrganId = organId,
                    EffectStatusName = effectStatusName,
                    PostTimeRight = "page",
                    ClassTagItem = "tag-item item7",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.ListOnSuccess"
                    }
                }
            };
            if (docModel.ListDocsView.HasValue())
            {
                docModel.ListDocsView = (from a in docModel.ListDocsView
                    join b in docModel.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId
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
            docModel.mFieldDisplays = docModel.ListFieldDisplays.FirstOrDefault(f => f.FieldId == fieldId) ?? new FieldDisplays();
            if (!string.IsNullOrEmpty(docModel.mFieldDisplays.FieldName))
            {
                docModel.SeoReplace = docModel.mFieldDisplays.FieldName;
            }
            if (docModel.ListDocsView.Count < 10)
            {
                docModel.WebsiteCanonical = docModel.AbsoluteUri.Replace("-f" + docGroupId + ".html", "-f1.html");
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
            return Extensions.GetViewMode("ListByField", docModel, Request);
        }

        /// <summary>
        /// Danh sách văn bản mới cập nhật, tiếng Anh trang chủ
        /// </summary>
        [SEOAction]
        public ActionResult ListDocs(byte docGroupId = 1, byte languageId = 1, int page = 0, int pSize = 20)
        {
            int rowCount = 0;
            var model = new DocsModel
            {
                Page = page,
                HaveAmp = true,
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pSize, page > 0 ? page - 1 : page, string.Empty, languageId, docGroupId, string.Empty, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty, string.Empty, 1, 0, 0, 0, 0, 0, 0, ref rowCount), 
                ListDocsViewMostView = DocsViewHelpers.Docs_GetViewMostView(languageId, docGroupId, 0, Constants.RowAmount5),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    UsingDisplayTable = 1,
                    PaginationType = 2,
                    DisplayTypeId = _displayTypeId,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH,"van-ban-moi.html"),
                    LanguageId = languageId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    DocGroupId = docGroupId,
                    PostTimeRight = "page",
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
                model.ListDocsView = (from a in model.ListDocsView
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
                        EffectStatusName = languageId == LawsVnLanguages.AvailableLanguages[0].LanguageId ? b.EffectStatusDesc : b.EffectStatusName
                    }).ToList();
            }
            return Extensions.GetViewMode("ListDocs", model, Request);
        }

        /// <summary>
        /// Danh sách văn bản theo trạng thái hiệu lực trang chủ
        /// </summary>
        /// <param name="effectStatusName"></param>
        /// <returns></returns>
       [SEOAction]
        public ActionResult ListDocsEffects(string effectStatusName = "SapSuaDoi", int page = 0, int pSize = 20)
        {
            int rowCount = 0;
            var model = new DocsModel
            {
                Page = page,
                HaveAmp = true,
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pSize, page > 0 ? page - 1 : page, "", _languageId, 0, "", 0, 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, effectStatusName, 0, 0, 0, "", "", "", 1, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListDocsViewMostView = DocsViewHelpers.Docs_GetViewMostView(_languageId, Constants.DocGroupIdVbpq, 0, Constants.RowAmount5),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    UsingDisplayTable = 1,
                    PaginationType = 2,
                    DisplayTypeId = _displayTypeId,
                    UrlPaging = string.Concat(CmsConstants.ROOT_PATH, "van-ban-sap-co-hieu-luc.html"),
                    LanguageId = _languageId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",//Docs_GetViewEffect2
                    //DocGroupId =  Constants.DocGroupIdVbpq,
                    PostTimeRight = "page",
                    ClassTagItem = "tag-item item7",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                EffectStatus = effectStatusName
            };
            if (model.ListDocsView.HasValue())
            {
                model.ListDocsView = (from a in model.ListDocsView join b in model.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId select new DocsView
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
            switch (effectStatusName)
            {
                case "HetHieuLuc":
                    {
                        model.EffectStatusName = "Văn bản hết hiệu lực";
                        break;
                    }
                case "SapCoHieuLuc":
                    {
                        model.EffectStatusName = "Văn bản sắp có hiệu lực";
                        break;
                    }
                case "SapHetHieuLuc":
                    {
                        model.EffectStatusName = "Văn bản sắp hết hiệu lực";
                        break;
                    }
                default:
                    {
                        model.EffectStatusName = "Văn bản sắp sửa đổi";
                        break;
                    }
            }
            return Extensions.GetViewMode("ListDocsEffects", model, Request);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Summary(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0, string displayPosition = "")
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
                    DisplayPosition = displayPosition,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, _languageId)
                };
                //get Eng
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Get(docId, ICSoft.HelperLib.LanguageHelpers.English);
                model.DocContentEng = m_Docs.DocContent;
                //end Eng
                //TODO chưa có vb tiếng Anh
               
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
                    var fieldFirsts = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirsts != null) model.FieldId = fieldFirsts.FieldId;

                    // TODO: tab Nội dung
                    //model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);

                    //model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                    if (tab.Equals("noidung"))
                    {
                        model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        }
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Nội dung ");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Nội dung");
                        return Extensions.GetViewMode("Detail", model);
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

                    // TODO: tab So sánh
                    //if (tab.Equals("sosanh"))
                    //{
                    //    return View("~/Views/Docs/SoSanh.cshtml", mDocsViewDetailModel);
                    //}

                    // TODO: tab Hiệu lực
                    if (tab.Equals("hieuluc"))
                    {
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Hiệu lực");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Hiệu lực");
                        return Extensions.GetViewMode("Effect", model);
                    }

                    // TODO: tab Liên quan
                    if (tab.Equals("lienquan"))
                    {
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Liên quan");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Liên quan");
                        return Extensions.GetViewMode("Relate", model);
                    }

                    // TODO: tab Tiếng Anh
                    if (tab.Equals("tienganh"))
                    {
                        return Extensions.GetViewMode("English", model);
                    }

                    // TODO: tab Tải về
                    if (tab.Equals("taive"))
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Tải về");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Tải về");
                        return Extensions.GetViewMode("Download", model);
                    }
                    // TODO: tab tóm tắt
                    else
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        return Extensions.GetViewMode("Summary", model);
                    }
                    
                    
                } return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DetailFull(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0, string displayPosition = "", int id = 0, int cid = 0)
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
                    DisplayPosition = displayPosition,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, _languageId)
                };
                //get Eng
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Get(docId, ICSoft.HelperLib.LanguageHelpers.English);
                model.DocContentEng = m_Docs.DocContent;
                if(m_Docs.DocId == 0)
                {
                    model.NotDocEng = true;
                }
                //end Eng

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
                    } else if(model.mDocsViewDetail.lDocOthers != null)
                    {
                        model.mDocsViewDetail.lDocOthersNewest = model.mDocsViewDetail.lDocOthers;
                    }
                    // TODO: SEO MetaTitle -> H1Tag -> Số hiệu+CQBH+ngày ban hành
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
                        if(model.mDocsViewDetail.mDocsView.DocName.Length <= 75)
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
                    var fieldFirsts = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirsts != null) model.FieldId = fieldFirsts.FieldId;

                    // TODO: tab Nội dung: xóa thẻ style
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
                    return Extensions.GetViewMode("DetailFull", model, Request);
                }
                return RedirectToAction("Error404", "Error");
            }
            return RedirectToAction("Error404", "Error");
        }
    }
}
