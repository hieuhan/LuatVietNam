using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.Library;
using LawsVNEN.Models;
using System.Web.Mvc.Ajax;
using LawsVN.Library.Sercurity;
using ICSoft.CMSLib;
using LawsVNEN.Models.Account;

namespace LawsVNEN.Controllers
{
    public class DocsController : Controller
    {
        //
        // GET: /Docs/
        private byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdVbpq;
        private readonly short _displayTypeId = Constants.FieldsDisplayTypeIdVbpq;
        private readonly short _displayTypeIdDocTypeId = Constants.DocTypeIdDisplayTypeIdVbpq;
        private readonly short _displayTypeIdOrganId = Constants.DocTypeIdDisplayTypeIdVbpq;

        public ActionResult Index(short fieldId = 0, byte docGroupId = 0, int page = 0)
        {
            int rowCount = 0, customerId = 0;
            string orderBy = string.Empty,
                effectStatusType = string.Empty,
                searchKeyword = string.Empty,
                docName = string.Empty,
                docIdentity = string.Empty,
                searchByDate = string.Empty,
                dateFrom = string.Empty,
                dateTo = string.Empty;
            byte docTypeId = 0, effectStatusId = 0, getRowCount = 1,
                isSearchExact = 0, displayTypeId = 0;
            short signerId = 0, organId = 0;

            var docModel = new DocFieldModel
            {
                Page = page,
                FieldId = fieldId,
                DisplayTypeId = _displayTypeId,
                DisplayTypeIdDocTypeId = _displayTypeIdDocTypeId,
                DisplayTypeIdOrganId = _displayTypeIdOrganId,
                ListDocsView = DocsViewHelpers.Docs_GetViewSearchEN(0, Constants.RowAmount20, page > 0 ? page - 1 : page, orderBy, _languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    PageLoad = true,
                    UrlPaging = string.Empty,
                    LanguageId = _languageId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_ViewSearch",
                    DocGroupId = docGroupId,
                    DisplayTypeId = _displayTypeId,
                    FieldId = fieldId,
                    EffectStatusName = "",
                    PostTimeRight = "page",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace
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
                        EffectStatusName = _languageId == LawsVnLanguages.AvailableLanguages[1].LanguageId ? b.EffectStatusDesc : b.EffectStatusName
                    }).ToList();
            }
            docModel.mFieldDisplays = docModel.ListFieldDisplays.FirstOrDefault(f => f.FieldId == fieldId);
            return View(docModel);
        }

        //public ActionResult NewDocuments(short displayTypeId = 72)
        //{
        //    int rowCount = 0;
        //    byte fileTypeId = 0;
        //    int actUserId = 0, customerId = 0;
        //    byte isSearchExact = 0, DocTypeId = 0, EffectStatusId = 0;
        //    string orderBy = "DocId Desc",
        //    docName = string.Empty,
        //    docIdentity = string.Empty,
        //    searchKeyword = string.Empty,
        //    effectStatusType = string.Empty,
        //    searchByDate = string.Empty;
        //    string dateFrom = string.Empty;
        //    string dateTo = string.Empty;
        //    short FieldId = 0, OrganId = 0, SignerId = 0;

        //    string fieldName = App_GlobalResources.Resource.NewDocuments;
        //    if(displayTypeId == Constants.DisplayTypeIdCongBaoPDF)
        //    {
        //        fieldName = App_GlobalResources.Resource.OfficialGazette;
        //    }
        //    else if (displayTypeId == Constants.DisplayTypeIdCongBaoDOC)
        //    {
        //        fieldName = App_GlobalResources.Resource.Others;
        //    }
        //    if (displayTypeId == 73)
        //    {
        //        fileTypeId = 2;
        //    }else if(displayTypeId == 74)
        //    {
        //        fileTypeId = 3;
        //    }
        //    var listDocViewContent = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(actUserId, Constants.RowAmount20, 0,
        //        orderBy, _languageId, _docGroupId, searchKeyword, isSearchExact, 1, docName, docIdentity,
        //        DocTypeId, FieldId, 0, OrganId, SignerId, 0, displayTypeId,
        //        customerId, 0, EffectStatusId, effectStatusType, 0, 0, fileTypeId, 0, searchByDate, dateFrom, dateTo,
        //        1, 1, 1, 1, 1, 1, 1, ref rowCount).lDocsView;
        //    var docModel = new DocFieldModel
        //    {
        //        DisplayTypeId = displayTypeId,
        //        DisplayTypeIdDocTypeId = _displayTypeIdDocTypeId,
        //        DisplayTypeIdOrganId = _displayTypeIdOrganId,
        //        ListDocsView = listDocViewContent,
        //        mPartialPaginationAjax = new PartialPaginationAjax
        //        {
        //            TotalPage = rowCount,
        //            PageSize = Constants.RowAmount20,
        //            LinkLimit = Constants.RowAmount5,
        //            ShowNumberOfResults = Constants.RowAmount20,
        //            UrlPaging = string.Empty,
        //            LanguageId = _languageId,
        //            ControllerName = "Ajax",
        //            ActionName = "NewDocumentsAjax",
        //            DocGroupId = Constants.DocGroupIdVbpq,
        //            DisplayTypeId = displayTypeId,
        //            EffectStatusName = "",
        //            PostTimeRight = "page",
        //            LawsAjaxOptions = new AjaxOptions
        //            {
        //                UpdateTargetId = "ListByField",
        //                InsertionMode = InsertionMode.Replace,
        //                OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
        //            }
        //        },
        //        mFieldDisplays = new FieldDisplays
        //        {
        //            FieldName = fieldName,
        //        }
        //    };
        //    return View(docModel);
        //}

        public ActionResult NewDocuments(int DisplayTypeId = 72, int page = 0)
        {
            int rowCount = 0;
            byte fileTypeId = 0;
            int actUserId = 0, customerId = 0;
            byte isSearchExact = 0, DocTypeId = 0, EffectStatusId = 0;
            string orderBy = "DocId Desc",
            docName = string.Empty,
            docIdentity = string.Empty,
            searchKeyword = string.Empty,
            effectStatusType = string.Empty,
            searchByDate = string.Empty;
            string dateFrom = string.Empty;
            string dateTo = string.Empty;
            short displayTypeId = 0, FieldId = 0, OrganId = 0, SignerId = 0;

            string fieldName = App_GlobalResources.Resource.NewDocuments;
            if (DisplayTypeId == Constants.DisplayTypeIdVBMoi)
            {
                fieldName = App_GlobalResources.Resource.NewDocuments;
            }
            else if(DisplayTypeId == Constants.DisplayTypeIdCongBaoPDF)
            {
                fileTypeId = Constants.FileTypeIdVbChinhThucPDF; //pdf
                fieldName = App_GlobalResources.Resource.OfficialGazette;
            }
            else if (DisplayTypeId == Constants.DisplayTypeIdCongBaoDOC)
            {
                fileTypeId = Constants.FileTypeIdVbThamKhao; //word
                fieldName = App_GlobalResources.Resource.Others;
            }
            var listDocViewContent = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(actUserId, Constants.RowAmount20, page > 0 ? page - 1 : page,
                orderBy, _languageId, _docGroupId, searchKeyword, isSearchExact, 1, docName, docIdentity,
                DocTypeId, FieldId, 0, OrganId, SignerId, 0, displayTypeId,
                customerId, 0, EffectStatusId, effectStatusType, 0, 0, fileTypeId, 0, searchByDate, dateFrom, dateTo,
                1, 0, 0, 0, 0, 0, 0, ref rowCount).lDocsView;
            var docModel = new DocFieldModel
            {
                Page = page,
                DisplayTypeId = (short)DisplayTypeId,
                DisplayTypeIdDocTypeId = _displayTypeIdDocTypeId,
                DisplayTypeIdOrganId = _displayTypeIdOrganId,
                ListDocsView = listDocViewContent,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    PageLoad = true,
                    UrlPaging = string.Empty,
                    LanguageId = _languageId,
                    ControllerName = "Ajax",
                    ActionName = "NewDocumentsAjax",
                    DocGroupId = Constants.DocGroupIdVbpq,
                    DisplayTypeId = (short)DisplayTypeId,
                    EffectStatusName = "",
                    PostTimeRight = "page",
                    FileTypeId = fileTypeId,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    }
                },
                mFieldDisplays = new FieldDisplays
                {
                    FieldName = fieldName,
                }
            };
            return View(docModel);
        }

        public ActionResult Content(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0)
        {
            int actUserId = 0;
            byte replicated = 0;
            short sysMessageId = 0;
            if (docId > 0)
            {
                var model = new DocsViewDetailModel
                {
                    DocGroupId = docGroupId,
                    LanguageId = _languageId,
                    RelateTypeId = relateTypeId,
                    mDocsViewDetail = "vietnamese".Equals(tab) ? DocsViewHelpers.View_GetDocDetail(docId,1) : DocsViewHelpers.View_GetDocDetail_EN(docId, _languageId)
                };
                if (model.mDocsViewDetail.mDocsView.DocId <= 0 && _languageId == 2)
                {
                    model.NotDocEng = true;
                    model.mDocsViewDetail = DocsViewHelpers.View_GetDocDetail(docId, 1);
                }
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
                    model.WebsiteTitle =
                        model.mDocsViewDetail.mDocsView.MetaTitle.TrimmedOrDefault(model.mDocsViewDetail.mDocsView
                            .DocName);
                    model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc;
                    model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword;
                    var fieldFirsts = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirsts != null) model.FieldId = fieldFirsts.FieldId;
                    if (Extensions.IsAuthenticated)
                    {
                        if (!model.LawsUser.IsInRole("TA,TC,NC"))
                        {
                            model.mDocsViewDetail.mDocsView.EffectStatusName = App_GlobalResources.Resource.Known;
                        }
                    }
                    else
                    {
                        model.mDocsViewDetail.mDocsView.EffectStatusName = App_GlobalResources.Resource.Known;
                    }

                    // TODO: tab Tóm tắt
                    if (tab.Equals("summary"))
                    {
                        model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        }
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Summary");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Summary");

                        return View("~/Views/Docs/Summary.cshtml", model);
                    }

                    // TODO: tab Hiệu lực
                    if (tab.Equals("effect"))
                    {
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Effect");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Effect");
                        return View("~/Views/Docs/Effect.cshtml", model);
                    }

                    // TODO: tab Liên quan
                    if (tab.Equals("relate"))
                    {
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Relate");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Relate");

                        return View("~/Views/Docs/Relate.cshtml", model);
                    }

                    // TODO: tab Tiếng Việt
                    if (tab.Equals("vietnamese"))
                    {
                        model.mDocsViewDetail.lDocIndexes.RemoveAll(i => i.LanguageId == 2);
                        if (string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                        {
                            model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(model.mDocsViewDetail.mDocsView.DocId);
                        }
                        return View("~/Views/Docs/Vietnamese.cshtml", model);
                    }

                    // TODO: tab Tải về
                    if (tab.Equals("download"))
                    {
                        model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                        model.WebsiteTitle =
                            string.Concat(model.WebsiteTitle, " | Download");
                        model.WebsiteDescription =
                            string.Concat(model.WebsiteDescription, " | Download");

                        return View("~/Views/Docs/Download.cshtml", model);
                    }
                    // TODO: tab Tóm tắt
                    return View(model);
                } return RedirectToAction("Index", "Home");
            } return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailFull(int docId = 0, byte docGroupId = 0, string tab = "", short relateTypeId = 0)
        {
            if (docId > 0)
            {
                var model = new DocsViewDetailModel
                {
                    DocGroupId = docGroupId,
                    LanguageId = _languageId,
                    RelateTypeId = relateTypeId,
                    mDocsViewDetail = DocsViewHelpers.View_GetDocDetail_EN(docId, _languageId),
                    mDocsViewDetailVi = DocsViewHelpers.View_GetDocDetail(docId,LawsVnLanguages.AvailableLanguages[1].LanguageId)
                };

                if (model.mDocsViewDetail.mDocsView.DocId <= 0 && _languageId == LawsVnLanguages.AvailableLanguages[0].LanguageId)
                {
                    model.NotDocEng = true;
                    model.mDocsViewDetail = model.mDocsViewDetailVi;
                }

                if (model.mDocsViewDetail.mDocsView.DocId > 0)
                {
                    // TODO: SEO
                    if (!string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.MetaTitle))
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
                            titleFromProperties += " of " + model.mDocsViewDetail.mDocsView.OrgansText.Trim();
                            titleFromProperties += " date issued " + model.mDocsViewDetail.mDocsView.IssueDate.ToString("dd/MM/yyyy");
                        }
                        model.WebsiteTitle = titleFromProperties;
                        model.WebsiteDescription = model.mDocsViewDetail.mDocsView.MetaDesc.TrimmedOrDefault(model.mDocsViewDetail.mDocsView.DocName.Replace("\"", ""));
                        model.WebsiteKeywords = model.mDocsViewDetail.mDocsView.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                    }
                    var fieldFirsts = model.mDocsViewDetail.lFieldDisplays.FirstOrDefault();
                    if (fieldFirsts != null) model.FieldId = fieldFirsts.FieldId;

                    if (Extensions.IsAuthenticated)
                    {
                        if (!model.LawsUser.IsInRole(Constants.RolesFull))
                        {
                            model.mDocsViewDetail.mDocsView.EffectStatusName = App_GlobalResources.Resource.Known;
                        }
                    }
                    else
                    {
                        model.mDocsViewDetail.mDocsView.EffectStatusName = App_GlobalResources.Resource.Known;
                    }

                    // TODO: tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.mDocsViewDetail.mDocsView.DocContent))
                    {
                        string docContent = model.mDocsViewDetail.mDocsView.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.mDocsViewDetail.mDocsView.DocContent = docContent;
                    }
                    model.mDocsViewDetail.lDocFiles = new DocFiles().GetView(docId);
                    return View(model);
                }
                return RedirectToAction("Error404", "Error");
            }
            return RedirectToAction("Error404", "Error");
        }

    }
}
