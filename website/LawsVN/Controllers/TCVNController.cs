using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LawsVN.Models;
using ICSoft.LawDocsLib;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using System.Web.Mvc.Ajax;
using LawsVN.AppCode.Attribute;
using LawsVN.Models.Account;
using LawsVN.Models.Docs;

namespace LawsVN.Controllers
{
    public class TCVNController : Controller
    {
        private byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdTcvn;
        private readonly short _displayTypeId = Constants.FieldDisplayTypeIdTcvn;

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Index(byte docTypeId = 61, int page = 0, int pSize = 20, short fieldId = 0, short organId = 0, byte effectStatusId = 0)
        {
            int rowCount = 0, rowCount1 = 0, customerId = 0;
            byte isSearchExact = 0, displayTypeId = 0, getRowCount = 1;
            string effectStatusType = string.Empty, searchByDate = string.Empty,
                dateFrom = string.Empty, dateTo = string.Empty;
            if (docTypeId < 61 || docTypeId > 64) docTypeId = 61;
            var model = new TCVNViewModel
            {
                Page = page,
                HaveAmp = true,
                mArticlesViewCate = ArticlesViewHelpers.View_GetArticleCate(Constants.CateIdHotNewsLaw,
                    Constants.RowAmount3,
                    0, Constants.RowAmount3, 0),
                ListDocsNewest = DocsViewHelpers.View_GetDocsViewNewest(_languageId, _docGroupId, Constants.RowAmount5, 0, 1, 0, ref rowCount1),
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pSize, page > 0 ? page - 1 : page, "", _languageId, _docGroupId, "", isSearchExact, 0, "", "", docTypeId, fieldId, 0, organId, 0, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount3,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    LanguageId = _languageId,
                    DocTypeId = docTypeId,
                    DocGroupId = _docGroupId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    EffectStatusName = "",
                    PostTimeRight = "page",
                    ClassTagItem = "tag-item item6",
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

            if (docTypeId == Constants.DocTypesTieuChuanNganh)
                model.TcvnTitle = "Tiêu chuẩn ngành";
            else if (docTypeId == Constants.DocTypesTieuChuanXdvn)
                model.TcvnTitle = "Tiêu chuẩn xây dựng Việt Nam";
            else if (docTypeId == Constants.DocTypesQuyChuanVn)
            {
                model.TcvnTitle = "Quy chuẩn Việt Nam";
            }
            else model.TcvnTitle = "Tiêu chuẩn Việt Nam";
            return Extensions.GetViewMode("Index", model, Request);
        }

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
                        model.mDocsViewDetail.lDocFiles = new DocFiles().DocFiles_GetList(0, string.Empty, docId);
                        return Extensions.GetViewMode("Download", model);
                    }
                    //ToDo tab Nội dung
                    if (tab.Equals("noidung"))
                    {
                        model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        }
                        return Extensions.GetViewMode("Detail", model);
                    }
                    //ToDo tab Lược đồ
                    if (tab.Equals("luocdo"))
                    {
                        return Extensions.GetViewMode("Diagram", model);
                    }
                    //ToDo tab Liên quan
                    if (tab.Equals("lienquan"))
                    {
                        return Extensions.GetViewMode("Relate", model);
                    }
                    //ToDo tab Thuộc tính
                    return Extensions.GetViewMode("Properties", model);

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
                            if (model.mDocsViewDetail.mDocsView.IssueDate > DateTime.MinValue)
                                titleFromProperties += " ban hành ngày " + model.mDocsViewDetail.mDocsView.IssueDate.ToString("dd/MM/yyyy");
                            else
                                titleFromProperties += " ban hành năm " + model.mDocsViewDetail.mDocsView.IssueYear.ToString();
                        }
                        model.WebsiteTitle = titleFromProperties;
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.DocName.Replace("\"", ""));
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                    }
                    FieldDisplays fieldFirst = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirst != null) model.FieldId = fieldFirst.FieldId;
                    model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                    // TODO: tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                    {
                        string docContent = model.mDocsViewDetail.mDocsView.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.mDocsViewDetail.mDocsView.DocContent = docContent;
                    }
                    model.ListDocFields =
                        model.mDocsViewDetail.mDocsView.FieldsText.GetListFieldsDocOthers(model.ListFields);

                    if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                    {
                        model.mDocsViewDetail.mDocsView.DocSummary = GoogleAmp.DocConvert(model.mDocsViewDetail.mDocsView.DocSummary);
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
