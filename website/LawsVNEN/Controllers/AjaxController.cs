using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.AppCode;
using LawsVNEN.Library;
using LawsVNEN.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;
using ICSoft.PartnerLib;
using LawsVN.Library;
using LawsVNEN.Library;
using LawsVNEN.Models.Account;
using sms.utils;

namespace LawsVNEN.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _docGroupId = Constants.DocGroupIdVbpq;
        [HttpGet]
        public PartialViewResult GetDocsNewest(byte docGroupId = 0, byte languageId = 2, int page = 1, int displayTypeId = 72, byte paginationType = 1, string urlPaging = "", string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewest", int pageSize = 20, int showNumberOfResults = 20, int linkLimit = 5)
        {
            int rowCount = 0;
            int pageIndex = page > 0 ? page - 1 : page; 
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.View_GetDocsViewNewestEN(languageId,Constants.DocGroupIdVbpq,displayTypeId, showNumberOfResults, pageIndex, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ShowNumberOfResults = showNumberOfResults,
                    PaginationType = paginationType,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId,
                    DisplayTypeId = (short)displayTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsNewest.cshtml", docsNewest);
        }
        [HttpGet]
        public PartialViewResult NewDocumentsAjax(byte docGroupId = 0, byte languageId = 2, int page = 1, short displayTypeId = 72, byte paginationType = 1, string urlPaging = "", string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewest", int pageSize = 20, int showNumberOfResults = 20, int linkLimit = 5)
        {
            //int rowCount = 0;
            //string docName = string.Empty,
            //docIdentity = string.Empty,
            //searchKeyword = string.Empty,
            //effectStatusType = string.Empty,
            //searchByDate = string.Empty;
            //string dateFrom = string.Empty;
            //string dateTo = string.Empty;
            //List<DocsView> ListDocViewContent = DocsViewHelpers.View_GetDocsViewNewestEN(languageId, Constants.DocGroupIdVbpq, displayTypeId, showNumberOfResults, pageIndex, 1, ref rowCount);


            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
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
            short FieldId = 0, OrganId = 0, SignerId = 0;

            if (displayTypeId == Constants.DisplayTypeIdCongBaoPDF)
            {
                fileTypeId = Constants.FileTypeIdVbChinhThucPDF; //pdf
            }
            else if (displayTypeId == Constants.DisplayTypeIdCongBaoDOC)
            {
                fileTypeId = Constants.FileTypeIdVbThamKhao; //word
            }
            var listDocViewContent = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(actUserId, Constants.RowAmount20, 0,
                orderBy, _currentLanguageId, _docGroupId, searchKeyword, isSearchExact, 1, docName, docIdentity,
                DocTypeId, FieldId, 0, OrganId, SignerId, 0, 0,
                customerId, 0, EffectStatusId, effectStatusType, 0, 0, fileTypeId, 0, searchByDate, dateFrom, dateTo,
                1, 0, 0, 0, 0, 0, 0, ref rowCount).lDocsView;
            var docsNewest = new DocsNewest
            {
                ListDocsView = listDocViewContent,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ShowNumberOfResults = showNumberOfResults,
                    PaginationType = paginationType,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId,
                    DisplayTypeId = displayTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsNewest.cshtml", docsNewest);
        }
        
        [HttpGet]
        public PartialViewResult Docs_GetRelate(short relateTypeId = 0, int docId = 0, int page = 0, string updateTargetId = "ListDocRelate", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Docs_GetRelate", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0;
            int pageIndex = page >= 1 ? page - 1 : page;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            DocsViewDetail mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(languageId, docId, relateTypeId, "", showNumberOfResults, pageIndex, 1, ref rowCount);
            var docNewest = new DocsNewest
            {
                mDocsViewDetail = mDocsViewDetail,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    },
                    DocId = docId,
                    RelateTypeId = relateTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/Docs_GetRelate.cshtml", docNewest);
        }

        [HttpGet]
        public PartialViewResult Docs_ViewSearch(byte docGroupId = 0, byte languageId = 2, int page = 0, short fieldId = 0,
            string effectStatusName = "", string orderBy = "", byte effectStatusId = 0, short organId = 0, string postTimeRight = "",
            short signerId = 0, byte docTypeId = 0, byte displayTypeId = 0, int year = 0, short provinceId = 0,
            byte usingDisplayTable = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Docs_ViewSearch", int pageSize = 20, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, customerId = 0, pageIndex = page > 0 ? page - 1 : page;
            string searchKeyword = string.Empty, docName = string.Empty, searchByDate = string.Empty,
                dateFrom = year > 0 ? string.Concat("01/01/", year) : string.Empty, dateTo = year > 0 ? string.Concat("31/12/", year) : string.Empty;
            string docIdentity = string.Empty;
            byte isSearchExact = 0, getRowCount = 1;

            var model = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearchEN(0, showNumberOfResults, pageIndex, orderBy, languageId
                    , docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, 0, customerId, 0, effectStatusId, effectStatusName, provinceId, 0, 0, searchByDate, dateFrom, dateTo, getRowCount
                    , 0, 1, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LanguageId = languageId,
                    UsingDisplayTable = usingDisplayTable,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    },
                    DocGroupId = docGroupId,
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    EffectStatusName = effectStatusName,
                    DisplayTypeId = displayTypeId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    SignerId = signerId,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    Year = year,
                    ProvinceId = provinceId,
                    PostTimeRight = postTimeRight
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsList.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult HistoryTransactions_GetPage(int showNumberOfResults = 20, byte languageId = 1, byte transactionStatusId = 1, int year = 0, int page = 0, string updateTargetId = "HistoryTransactionsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "HistoryTransactions_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0, totalMoney = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            string dateFrom, dateTo = DateTime.Now.ToString("dd/MM/yyyy");
            switch (year)
            {
                case 1:
                    {
                        dateFrom = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
                        break;
                    }
                case 2:
                    {
                        dateFrom = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                        break;
                    }
                case 3:
                    {
                        dateFrom = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                        break;
                    }
                default:
                    dateFrom = string.Empty;
                    break;
            }
            var list = new PaymentTransactions
            {
                CustomerId = _currentCustomerId,
                SiteId = Constants.SiteId_TiengViet,
                TransactionDesc = string.Empty,
                TransactionCode = string.Empty,
                TransactionStatusId = Constants.TransactionStatusIdSuccess
            }.GetPageView(0, showNumberOfResults, page > 0 ? page - 1 : page, "", 0, dateFrom, dateTo, ref rowCount, ref totalMoney);
            var model = new HistoryTransactionsModel
            {
                RowCount = rowCount,
                TotalMoney = totalMoney,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = showNumberOfResults,
                    TransactionStatusId = transactionStatusId,
                    UrlPaging = string.Empty,
                    ControllerName = "Ajax",
                    ActionName = "HistoryTransactions_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "HistoryTransactionsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };

            model.ListPaymentTransactions = from paymentTransaction in list
                                            join servicePackage in model.ListServicePackages on paymentTransaction.ServicePackageId equals
                                                servicePackage.ServicePackageId into list1
                                            from l1 in list1.DefaultIfEmpty()
                                            select new PaymentTransactionsModel
                                            {
                                                PaymentTransactionId = paymentTransaction.PaymentTransactionId,
                                                TransactionDesc = paymentTransaction.TransactionDesc,
                                                TransactionStatusId = paymentTransaction.TransactionStatusId,
                                                TransactionStatusDesc = paymentTransaction.TransactionStatusId.TransactionStatusDescGetById(),
                                                PaymentDate = paymentTransaction.PaymentDate.toString(),
                                                Amount =
                                                    paymentTransaction.TransactionStatusId != Constants.TransactionStatusIdPending ||
                                                    l1 != null && !Constants.ServiceId_NghiepVu.Any(x => x.Equals(l1.ServiceId))
                                                        ? (paymentTransaction.Amount > 0
                                                            ? (_currentLanguageId == 1 ? string.Format("{0:#,###} {1}", paymentTransaction.Amount, CmsConstants.CURRENCY_VND) : string.Format("{0} {1:#,###} ", CmsConstants.CURRENCY, paymentTransaction.Amount))
                                                            : "0")
                                                        : "0",
                                                PaymentTypeId = paymentTransaction.PaymentTypeId,
                                                ServiceId = (short)(l1 == null ? 0 : l1.ServiceId),
                                                ServicePackageId = paymentTransaction.ServicePackageId,
                                                ServicePackageDesc = l1 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l1.ServicePackageDesc.TrimmedOrDefault(string.Empty) : l1.ServicePackageName.TrimmedOrDefault(string.Empty)
                                            }
                                                into list2

                                                join c in model.ListServices on list2.ServiceId equals c.ServiceId into list3
                                                from l2 in list3.DefaultIfEmpty()
                                                select new PaymentTransactionsModel
                                                {
                                                    PaymentTransactionId = list2.PaymentTransactionId,
                                                    TransactionDesc = list2.TransactionDesc,
                                                    TransactionStatusId = list2.TransactionStatusId,
                                                    TransactionStatusDesc = list2.TransactionStatusDesc,
                                                    Amount = list2.Amount,
                                                    PaymentTypeId = list2.PaymentTypeId,
                                                    PaymentDate = list2.PaymentDate,
                                                    ServiceId = list2.ServiceId,
                                                    ServiceDesc = l2 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l2.ServiceDesc.TrimmedOrDefault(string.Empty) : l2.ServiceName.TrimmedOrDefault(string.Empty),
                                                    ServicePackageId = list2.ServicePackageId,
                                                    ServicePackageDesc = list2.ServicePackageDesc
                                                }
                                                    into list4

                                                    join d in model.ListPaymentTypes on list4.PaymentTypeId equals d.PaymentTypeId into list5
                                                    from l3 in list5.DefaultIfEmpty()
                                                    select new PaymentTransactionsModel
                                                    {
                                                        PaymentTransactionId = list4.PaymentTransactionId,
                                                        TransactionDesc = list4.TransactionDesc,
                                                        TransactionStatusId = list4.TransactionStatusId,
                                                        TransactionStatusDesc = list4.TransactionStatusDesc,
                                                        Amount = list4.Amount,
                                                        PaymentTypeId = list4.PaymentTypeId,
                                                        PaymentDate = list4.PaymentDate,
                                                        ServiceId = list4.ServiceId,
                                                        ServiceDesc = list4.ServiceDesc,
                                                        ServicePackageId = list4.ServicePackageId,
                                                        ServicePackageDesc = list4.ServicePackageDesc,
                                                        PaymentTypeDesc = l3 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l3.PaymentTypeDesc.TrimmedOrDefault(string.Empty) : l3.PaymentTypeName.TrimmedOrDefault(string.Empty)
                                                    }
                                                        into list6

                                                        join e in model.ListTransactionTypes on list6.TransactionTypeId equals e.TransactionTypeId into list7
                                                        from l4 in list7.DefaultIfEmpty()
                                                        select new PaymentTransactionsModel
                                                        {
                                                            PaymentTransactionId = list6.PaymentTransactionId,
                                                            TransactionDesc = list6.TransactionDesc,
                                                            TransactionStatusId = list6.TransactionStatusId,
                                                            TransactionStatusDesc = list6.TransactionStatusDesc,
                                                            Amount = list6.Amount,
                                                            PaymentTypeId = list6.PaymentTypeId,
                                                            PaymentDate = list6.PaymentDate,
                                                            ServiceId = list6.ServiceId,
                                                            ServiceDesc = list6.ServiceDesc,
                                                            ServicePackageId = list6.ServicePackageId,
                                                            ServicePackageDesc = list6.ServicePackageDesc,
                                                            PaymentTypeDesc = list6.PaymentTypeDesc,
                                                            TransactionTypesDesc = l4 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l4.TransactionTypeDesc.TrimmedOrDefault(string.Empty) : l4.TransactionTypeName.TrimmedOrDefault(string.Empty)
                                                        };

            return PartialView("~/Views/AjaxTemplates/Account/PartialPaymentTransactions_GetPage.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JavaScriptResult NewsLetterEmail(NewsletterEmailsModel model)
        {
            short sysMessageId = 0;
            string javaScript = Resource.PleaseTryAgainLater;
            byte replicated = 0;
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            var mNewsletterEmail = new NewsletterEmails
            {
                SiteId = Constants.SiteId,
                Email = model.Email,
                IsReceiveNews = 1,
                CustomerId = currentCustomerId
            };

            mNewsletterEmail.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
            if (mNewsletterEmail.NewsletterEmailId > 0)
            {
                javaScript = Resource.YouSignupForASuccessfulNewsletter;
            }
            return JavaScript(string.Format("lawsVn.successfulNewsletter('{0}')", javaScript));
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult SaveDocument(int docId)
        {
            byte replicated = 0, sysMessageTypeId = 0;
            int actUserId = 0; short sysMessageId = 0;
            string message = Resource.PleaseTryAgainLater;
            if (docId > 0)
            {
                var mCustomerDocs = new CustomerDocs
                {
                    CustomerId = Extensions.GetCurrentCustomerId(),
                    DocId = docId,
                    RegistTypeId = 1,
                    LanguageId = LawsVnLanguages.AvailableLanguages[0].LanguageId
                };
                //ToDo: sysMessageTypeId = 3 : Đã thêm rồi -> ko tăng count
                sysMessageTypeId = mCustomerDocs.Insert(replicated, actUserId, ref sysMessageId);
                //if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
                //{
                //    message = Resource.AddDocumentOfInterestToSuccess;
                //}
                message = Resource.AddDocumentOfInterestToSuccess;
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Data = new CustomerDocs { DocId = docId, RegistTypeId = sysMessageTypeId },
                Completed = true//sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful
            };
        }

        public PartialViewResult Search_GetViewSearch(string keyword = "", short fieldId = 0, string effectStatusName = "", byte searchOptions = 1, byte searchExact = 0, byte effectStatusId = 0, int page = 0, short organId = 0, byte docTypeId = 0, byte fileTypeId=0, string updateTargetId = "ListDocsViews", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Search_GetViewSearch", int pageSize = 20, int linkLimit = 5, int showNumberOfResults = 20, string dateFrom = "", string dateTo = "", string signerName = "", string orderBy = "", short signerId = 0)
        {
            if (page > 0)
            {
                page = page - 1;
            }
            int rowCount = 0;
            string effectStatusType = string.Empty,
                searchKeyword = string.Empty,
                docName = string.Empty,
                docIdentity = string.Empty;
            byte docGroupId = 0, languageId = LawsVnLanguages.GetCurrentLanguageId();
            byte displayTypeId = 0;
            string searchByDate = ""; byte getRowCount = 1;

            DateTime dtFrom, dtTo;
            DateTime.TryParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom);
            string dateF = dtFrom != DateTime.MinValue ? dtFrom.ToString("dd/MM/yyyy") : string.Empty;
            DateTime.TryParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo);
            string dateT = dtTo != DateTime.MinValue ? dtTo.ToString("dd/MM/yyyy") : string.Empty;
            switch (searchOptions)
            {
                case 2:
                {
                    docName = keyword;
                    break;
                }
                case 3:
                {
                    docIdentity = keyword;
                    break;
                }
                default:
                {
                    searchKeyword = keyword;
                    break;
                }
            }

            var docNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(0, showNumberOfResults, page, orderBy, languageId, docGroupId, searchKeyword, searchExact, 0, docName, docIdentity, docTypeId, fieldId,0, organId, signerId, 0, displayTypeId, 0, 0, effectStatusId, effectStatusType, 0, 0, fileTypeId, 0, searchByDate, dateF, dateT, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount).lDocsView,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = showNumberOfResults,
                    PageSize = showNumberOfResults,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    },
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    KeyWord = keyword,
                    SearchOptions = searchOptions,
                    SearchExact = searchExact,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    SignerName = signerName,
                    SignerId = signerId,
                    FileTypeId = fileTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/SearchDocsList.cshtml", docNewest);
        }

        [HttpPost]
        public PartialViewResult Docs_GetViewSearchWithKeyword(string keywords = "", string orderBy = "DocId DESC", byte isSearchExact = 0, short fieldId = 0, byte docGroupId = 1, byte languageId = 2, byte searchOptions = 1, short signerId = 0,string signerName="", short organId = 0, byte effectStatusId = 0, int page = 0, int year = 0, byte docTypeId = 0, string dateFrom = "", string dateTo = "", string updateTargetId = "ListDocsViews", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Search_GetViewSearch", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, customerId = 0;
            string searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty;
            byte displayTypeId = 0;
            string effectStatusType = string.Empty, searchByDate = string.Empty;
            if(languageId ==0)
            {
                languageId = LawsVnLanguages.GetCurrentLanguageId();
            }
            if (year > 0)
            {
                dateFrom = string.Concat("1/1/", year);
                dateTo = string.Concat("31/12/", year);
            }
            else
            {

                DateTime dtFrom, dtTo;
                DateTime.TryParseExact(dateFrom, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dtFrom);
                dateFrom = dtFrom != DateTime.MinValue ? dtFrom.ToString("dd/MM/yyyy") : string.Empty;
                DateTime.TryParseExact(dateTo, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dtTo);
                dateTo = dtTo != DateTime.MinValue ? dtTo.ToString("dd/MM/yyyy") : string.Empty;
            }
            switch (searchOptions)
            {
                case 2:
                    {
                        docName = keywords;
                        break;
                    }
                case 3:
                    {
                        docIdentity = keywords;
                        break;
                    }
                default:
                    {
                        searchKeyword = keywords.StripTags().SanitizeWithoutSplash();
                        break;
                    }
            }

            var docNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearchWithKeywordEN(0, showNumberOfResults, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, isSearchExact, 1, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, 1, 0, 0, 0, 0, 0, 0, ref rowCount).lDocsView,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    PageSize = showNumberOfResults,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    },
                    DocGroupId = docGroupId,
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    KeyWord = searchKeyword,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    SearchOptions = searchOptions,
                    LanguageId = languageId,
                    SignerId = signerId,
                    SignerName  = signerName,
                    Year = year
                }
            };
            return PartialView("~/Views/AjaxTemplates/SearchDocsList.cshtml", docNewest);
        }

        [HttpGet]
        public PartialViewResult BanTinLuatVN_Search(string keyword = "", short categoryId = 0, short tagId = 0, int showNumberOfResults = 20, byte languageId = 1, int page = 0, string updateTargetId = "ArticlesByCateBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "BanTinLuatVN_Search", int pageSize = 5, int linkLimit = 5)
        {
            keyword = keyword.StripTags().Sanitize();
            byte siteID = Constants.SiteId;
            int rowCount = 0;
            Articles m_Articles = new Articles();
            m_Articles.ReviewStatusId = 2;//Reviewed
            m_Articles.ShowTop = 0;
            m_Articles.CategoryId = categoryId;
            m_Articles.SiteId = siteID;
            m_Articles.Title = keyword;
            //m_Articles.LanguageId = 2;//English
            var model = new ArticleViewModel
            {
                ListArticlesView = m_Articles.GetPageView(0, showNumberOfResults, page > 0 ? page - 1 : page, "DisplayOrder DESC", tagId, 0, 0, "", "", ref rowCount),
                //ListArticlesView = ArticlesViewHelpers.View_Search_01(showNumberOfResults, page > 0 ? page - 1 : page,
                //    0, 0, categoryId, siteID, 1, tagId,
                //    keyword, 0, 0, 0, 0, 0, "DisplayOrder DESC", ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = showNumberOfResults,
                    KeyWord = keyword,
                    CategoryId = categoryId,
                    ControllerName = "Ajax",
                    ActionName = "BanTinLuatVN_Search",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/PartialListBanTinLuatVN.cshtml", model);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult PartialLogin()
        {
            return PartialView("~/Views/AjaxTemplates/PartialLogin.cshtml");
        }

        #region Account
        [HttpPost]
        [LawsVnAuthorize]
        public PartialViewResult AccountProfileSwitchMode(string mode = "edit")
        {
            int countVbpl = 0, countVbhn = 0, countTcvn = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new AccountProfileModel
            {
                Mode = mode,
                CustomerName = mCustomer.CustomerName,
                FullName = mCustomer.CustomerFullName,
                GenderId = mCustomer.GenderId,
                CustomerMobile = mCustomer.CustomerMobile,
                Address = mCustomer.Address,
                Email = mCustomer.CustomerMail,
                ProvinceId = mCustomer.ProvinceId,
                OccupationId = mCustomer.OccupationId,
                DateOfBirth = mCustomer.DateOfBirth.ToString("dd/MM/yyyy"),
                mCustomersViewDetail = CustomerHelpers.LawsCustomer_GetViewDetail(currentCustomerId, Constants.DocGroupIdVbpq, 1, _currentLanguageId, ref countVbpl, ref countVbhn, ref countTcvn)
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/{0}.cshtml", mode.Equals("edit") ? "PartialAccountProfileSwitchEditMode" : "PartialAccountProfileSwitchViewMode"), model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult DeleteDocument(byte docGroupId = 1, string listdocId = "", short docsId = 0)
        {
            if (!string.IsNullOrEmpty(listdocId))
            {
                int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId(); short sysMessageId = 0;
                byte languageId = LawsVnLanguages.AvailableLanguages[0].LanguageId;
                new CustomerDocs().DeleteList(actUserId,currentCustomerId,listdocId,languageId, ref sysMessageId);
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = docsId > 1 ? Resource.DeleteInterestDocumentsSuccessful : Resource.DeleteInterestDocumentSuccessful,
                    ReturnUrl = string.Format("/interested-documents.html?docGroupId={0}", docGroupId),
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult MyDocuments_GetPage(byte registTypeId = 1, byte docGroupId = 0, short fieldId = 0, short organId = 0, byte effectStatusId = 0, byte docTypeId = 0, int showNumberOfResults = 20, byte languageId = 2, int page = 1,string orderBy = "", string updateTargetId = "MyDocumentsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "MyDocuments_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            languageId = _currentLanguageId;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var myDocuments = new MyDocuments
            {
                mDocsViewSearch = DocsViewHelpers.MyDocumentsEN_ViewGetPage(currentCustomerId, docGroupId, Constants.RegistTypeIdVbqt, languageId, fieldId, organId, effectStatusId, docTypeId, "", "", orderBy, showNumberOfResults, page > 0 ? page - 1 : page, 1, 1, ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = linkLimit,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = showNumberOfResults,
                    RegistTypeId = registTypeId,
                    DocGroupId = docGroupId,
                    FieldId = fieldId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    EffectStatusId = effectStatusId,
                    ControllerName = "Ajax",
                    ActionName = "MyDocuments_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyDocumentsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Account/PartialMyDocuments.cshtml", myDocuments);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public PartialViewResult BusinessInformationSwitchMode(string mode = "edit")
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new BusinessInformation
            {
                Mode = mode,
                OrganName = mCustomer.OrganName,
                OrganAddress = mCustomer.OrganAddress,
                OrganPhone = mCustomer.OrganPhone,
                OrganFax = mCustomer.OrganFax,
                AccountNumber = mCustomer.AccountNumber
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/{0}.cshtml", mode.Equals("edit") ? "BusinessInformationSwitchEditMode" : "BusinessInformationSwitchViewMode"), model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public PartialViewResult SubscribeToNewsletters(string mode = "edit")
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new SubscribeToNewsletters
            {
                Mode = mode,
                CustomerId = currentCustomerId,
                RegisterNewsletter = mCustomer.RegisterNewsletter
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/{0}.cshtml", mode.Equals("edit") ? "SubscribeToNewslettersEditMode" : "SubscribeToNewslettersViewMode"), model);
        }
        #endregion

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult KiemTraDvDangKy(short serviceId = 0)
        {
            byte isRegistService = 0, languageId = LawsVnLanguages.GetCurrentLanguageId();
            short serviceIdProcess = 0; string feeType = string.Empty, actionButton = string.Empty, msg =
                string.Empty;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            if (serviceId > 0)
            {
                new CustomerServices().CustomerServices_LVN_KiemtraDvDangKy(currentCustomerId, string.Empty,
                    serviceId, ref isRegistService, ref serviceIdProcess, ref feeType, ref actionButton, ref msg, languageId);

                var listServicesId = new List<short>
                {
                    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                };
                //TODO danh sách dịch vụ hỗ trợ chuyển đổi
                int index = listServicesId.IndexOf(serviceIdProcess);
                List<short> listServicesIdUpgradeAllowed = null;
                if (index > -1)
                {
                    listServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s <= serviceIdProcess).ToList();
                }
                var model = new DtCustomerServices
                {
                    Messages = msg,
                    ServiceId = serviceIdProcess,
                    IsRegistService = isRegistService,
                    FeeType = feeType,
                    ActionButton = actionButton,
                    IsUpgradeAllowed = listServicesIdUpgradeAllowed.HasValue() && listServicesIdUpgradeAllowed.Exists(m => m == serviceIdProcess || m == serviceId || m > serviceIdProcess),
                    ListServicesIdUpgradeAllowed = listServicesIdUpgradeAllowed
                };
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Data = model,
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult SubscriberByServicePackageParentId(short serviceId = 17,short servicePackageParentId = 0)
        {
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            ServicePackages servicePackage = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageParentId && m.ServicePackageParentId == 0);
            List<ServicePackages> listServicePackagesChild = new List<ServicePackages> { new ServicePackages { ServicePackageId = 0, ServicePackageName = Resource.PleaseSelectTheSubscriptionPeriod, ServicePackageDesc =  Resource.PleaseSelectTheSubscriptionPeriod} };
            int total = 0, price = 0, priceVat = 0, numMonthUse = 0, numDayUse = 0, concurrentLogin = 0;
            if (servicePackage != null)
            {
                listServicePackagesChild.AddRange(listServicePackagesByServiceId
                    .Where(item =>
                        item.ServicePackageParentId == servicePackageParentId &&
                        item.ServicePackageParentId != 0).OrderBy(item => item.NumMonthUse));
                if (listServicePackagesChild.HasValue())
                {
                    servicePackage = listServicePackagesChild[0];
                    price = servicePackage.Price;
                    priceVat = servicePackage.Price * 10 / 100;
                    total = servicePackage.Price + priceVat;
                    numMonthUse = servicePackage.NumMonthUse;
                    numDayUse = servicePackage.NumDayUse;
                    concurrentLogin = servicePackage.ConcurrentLogin;
                }
            }
            var model = new PartialSubscriberModel
            {
                ServicePackage = servicePackage,
                ListServicePackages = listServicePackagesChild,
                Price = price,
                PriceVAT = priceVat,
                Total = total,
                NumMonthUse = numMonthUse,
                NumDayUse = numDayUse,
                ConcurrentLogin = concurrentLogin,
                LanguageId = languageId
            };
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = model,
                Completed = true
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult SubscriberByServicePackageId(short serviceId = 17, short servicePackageId = 0)
        {
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            ServicePackages servicePackage = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageId && m.ServicePackageParentId != 0);
            int total = 0, price = 0, priceVat = 0, numMonthUse = 0, numDayUse = 0, concurrentLogin = 0;
            if (servicePackage != null)
            {
                price = servicePackage.Price;
                priceVat = servicePackage.Price * 10 / 100;
                total = servicePackage.Price + priceVat;
                numMonthUse = servicePackage.NumMonthUse;
                numDayUse = servicePackage.NumDayUse;
                concurrentLogin = servicePackage.ConcurrentLogin;
            }
            var model = new PartialSubscriberModel
            {
                ServicePackage = servicePackage,
                Price = price,
                PriceVAT = priceVat,
                Total = total,
                NumMonthUse = numMonthUse,
                NumDayUse = numDayUse,
                ConcurrentLogin = concurrentLogin,
                LanguageId = languageId
            };
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = model,
                Completed = true
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult BankPayment(BankPaymentModel model)
        {
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            byte replicated = 0;
            short sysMessageId = 0;
            string transactionDesc = string.Empty;

            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            if (model.ServicePackageId > 0)
            {
                //TODO tạo đơn hàng
                var mOrders = new Orders
                {
                    OrderName = string.Format("{0}, gói dịch vụ: {1}",
                        "Tra cứu tiếng Anh", model.ServicePackageName),
                    FullName = Extensions.GetCurrentCustomerFullName(),
                    Email = Extensions.GetCurrentCustomerMail(),
                    Address = string.Empty,
                    Mobile = string.Empty,
                    SiteId = Constants.SiteId_TiengViet,
                    OrderValue = model.Total,
                    OrderCode = string.Empty,
                    CouponCode = string.Empty,
                    CouponValue = 0,
                    CrDateTime = DateTime.Now,
                    CompanyName = model.TaxInvoice ? model.CompanyName : string.Empty,
                    CompanyAddress = model.TaxInvoice ? model.CompanyAddress : string.Empty,
                    CompanyTaxCode = model.TaxInvoice ? model.CompanyTaxCode : string.Empty,
                    OrderDesc = model.TaxInvoice ? model.InternalContent : string.Empty,
                    CountryId = 1,
                    CrUserId = 0,
                    CustomerId = currentCustomerId,
                    DeliveryFee = 0,
                    DeliveryNote = string.Empty,
                    DeliveryStatusId = 1, //Chờ giao hàng
                    FromIP = Request.UserHostAddress,
                    OrderStatusId = 1, //Chờ thanh toán
                    PartnerId = 0,
                    PaymentTypeId = 3,// online qua ngan hang
                    RequireInvoice = (byte)(model.TaxInvoice ? 1 : 0) //hóa đơn GTGT
                };
                mOrders.Insert(replicated, actUserId, ref sysMessageId);
                if (mOrders.OrderId > 0)
                {
                    if (model.TransactionTypeId != Constants.TransactionTypeIdDangKy)
                    {
                        if (model.TransactionTypeId == Constants.TransactionTypeIdGiaHan)
                        {
                            transactionDesc = string.Format("Gia hạn Thuê bao {0}", transactionDesc);
                        }
                    }
                    else
                    {
                        transactionDesc = string.Format("Đăng ký Thuê bao {0}", transactionDesc);
                    }

                    //TODO tạo giao dịch thanh toán
                    var paymentTransactions = new PaymentTransactions
                    {
                        SiteId = Constants.SiteId_TiengViet,
                        CustomerId = currentCustomerId,
                        Amount = Convert.ToInt32(model.Total), 
                        ServicePackageId = model.ServicePackageId,
                        OrderId = mOrders.OrderId,
                        DataId = 0,
                        DataTypeId = 0,
                        TransactionStatusId = 3, //Chờ xử lý
                        TransactionTypeId = model.TransactionTypeId,
                        TransactionDesc = transactionDesc,
                        TransactionCode = model.BankCode,
                        PaymentTypeId = 3,//online qua ngan hang
                        PaymentDate = DateTime.Now,
                        BalanceAdded = 0,
                        FromIP = Request.UserHostAddress
                    };
                    paymentTransactions.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
                    if (paymentTransactions.PaymentTransactionId > 0)
                    {
                        var customer = new Customers().Get(currentCustomerId);
                        _123Pay m_Pay = new _123Pay
                        {
                            addInfo = "",
                            bankCode = model.BankCode,
                            cancelURL = "",
                            checksum = "",
                            clientIP = Request.UserHostAddress,
                            custAddress = StringUtil.RemoveSignature(customer.Address),
                            custDOB = customer.DateOfBirth == DateTime.MinValue ? "" : customer.DateOfBirth.ToString("dd/MM/yyyy"),
                            custGender = customer.GenderId == 1 ? "M" : customer.GenderId == 0 ? "U" : "F",
                            custMail = customer.CustomerMail,
                            custName = StringUtil.RemoveSignature(customer.CustomerFullName),
                            custPhone = customer.CustomerMobile,
                            description = "",
                            errorURL = "",
                            mTransactionID = paymentTransactions.PaymentTransactionId.ToString(),
                            redirectURL = Constants.PAY_RESULT_URL,
                            totalAmount = model.Total.ToString()
                        };


                        string[] responseObj = m_Pay.createOrder(currentCustomerId, mOrders.OrderId);
                        if (responseObj.Length > 2)// success
                        {
                            string returnCode = responseObj[0];
                            string partner123PayTransactionId = responseObj[1];
                            string redirectUrl = responseObj[2];
                            //model.PayGateUrl = redirectUrl;
                            return new AjaxResult
                            {
                                StatusCode = 200,
                                AllowGet = true,
                                Data = model,
                                ReturnUrl = redirectUrl,
                                Completed = true
                            };
                        }
                        else
                        {
                            string returnCode = responseObj[0];
                            string returnDesc = responseObj[1];
                            Partner123PayResponseCodes mPartner123PayResponseCodes =
                                new Partner123PayResponseCodes { PartnerCode = returnCode };
                            mPartner123PayResponseCodes = mPartner123PayResponseCodes.Get();
                            return new AjaxResult
                            {
                                StatusCode = 200,
                                AllowGet = true,
                                Message = mPartner123PayResponseCodes.PartnerCodeName,
                                Data = model,
                                Completed = true
                            };
                        }
                    }
                }
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }


        /// <summary>
        /// Form đăng ký dịch vụ sử dụng tk ngân hàng
        /// </summary>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult PaymentServiceUsingBankAccountZalo(PaymentServiceUsingBankAccountModel model)
        {
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            byte replicated = 0;
            short sysMessageId = 0;
            float amount = 0, percentDecrease = 0;
            string transactionDesc = string.Empty;

            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            AjaxResult m_AjaxResult = new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "",
                Data = model,
                Completed = true
            };
            if (model.ServiceId > 0 && model.ServicePackageId > 0)
            {
                //TODO kiểm tra mã khuyến mại
                if (!string.IsNullOrEmpty(model.PromotionCodeBankAccount))
                {
                    var messages = new PromotionCodes().PromotionCodes_Check(model.PromotionCodeBankAccount, ref amount, ref percentDecrease);
                    if (!messages.Equals("OK"))
                    {
                        model.PromotionCodeBankAccount = string.Empty;
                    }
                    else transactionDesc = string.Format(". Sử dụng mã khuyến mại: {0}", model.PromotionCodeBankAccount);
                }
                //TODO tạo đơn hàng
                var mOrders = new Orders
                {
                    OrderName = string.Format("{0}, gói dịch vụ: {1} {2}",
                        model.ServiceName, model.ServicePackageName, !string.IsNullOrEmpty(model.PromotionCodeBankAccount) ? string.Concat(", sử dụng mã khuyến mại: ", model.PromotionCodeBankAccount) : string.Empty),
                    FullName = Extensions.GetCurrentCustomerFullName(),
                    Email = Extensions.GetCurrentCustomerMail(),
                    Address = string.Empty,
                    Mobile = string.Empty,
                    SiteId = Constants.SiteId,
                    OrderValue = model.Total,
                    OrderCode = string.Empty,
                    CouponCode = model.PromotionCodeBankAccount,
                    CouponValue = amount > 0 ? amount : (percentDecrease > 0 ? model.Price * percentDecrease / 100 : 0),
                    CrDateTime = DateTime.Now,
                    CompanyName = model.TaxInvoice ? model.CompanyName : string.Empty,
                    CompanyAddress = model.TaxInvoice ? model.CompanyAddress : string.Empty,
                    CompanyTaxCode = model.TaxInvoice ? model.CompanyTaxCode : string.Empty,
                    OrderDesc = model.TaxInvoice ? model.InternalContent : string.Empty,
                    CountryId = 1,
                    CrUserId = 0,
                    CustomerId = currentCustomerId,
                    DeliveryFee = 0,
                    DeliveryNote = string.Empty,
                    DeliveryStatusId = 1, //Chờ giao hàng
                    FromIP = Request.UserHostAddress,
                    OrderStatusId = 1, //Chờ thanh toán
                    PartnerId = 0,
                    PaymentTypeId = 3,// online qua ngan hang
                    RequireInvoice = (byte)(model.TaxInvoice ? 1 : 0) //hóa đơn GTGT
                };
                mOrders.Insert(replicated, actUserId, ref sysMessageId);
                if (mOrders.OrderId > 0)
                {
                    model.OrderId = mOrders.OrderId;
                    if (model.TransactionTypeId == Constants.TransactionTypeIdDangKy)
                    {
                        transactionDesc = string.Format("Đăng ký Thuê bao {0}", transactionDesc);
                    }
                    else if (model.TransactionTypeId == Constants.TransactionTypeIdGiaHan)
                    {
                        transactionDesc = string.Format("Gia hạn Thuê bao {0}", transactionDesc);
                    }
                    else if (model.TransactionTypeId == Constants.TransactionTypeIdChuyenDoi)
                    {
                        transactionDesc = string.Format("Chuyển đổi Thuê bao {0}", transactionDesc);
                    }
                    //TODO tạo giao dịch thanh toán
                    var paymentTransactions = new PaymentTransactions
                    {
                        SiteId = Constants.SiteId,
                        CustomerId = currentCustomerId,
                        Amount = Convert.ToInt32(model.Total), //Convert.ToInt32(model.Price),
                        ServicePackageId = model.ServicePackageId,
                        OrderId = mOrders.OrderId,
                        DataId = 0,
                        DataTypeId = 0,
                        TransactionStatusId = 3, //Chờ xử lý
                        TransactionTypeId = model.TransactionTypeId,
                        TransactionDesc = transactionDesc,
                        TransactionCode = model.BankCode,
                        PaymentTypeId = 3,//online qua ngan hang
                        PaymentDate = DateTime.Now,
                        BalanceAdded = 0,
                        FromIP = Request.UserHostAddress
                    };
                    paymentTransactions.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
                    if (paymentTransactions.PaymentTransactionId > 0)
                    {
                        model.PaymentTransactionId = paymentTransactions.PaymentTransactionId;
                    }
                    //LogFiles.WriteLog("start 123pay");
                    //123pay process
                    Customers m_Customers = new Customers();
                    m_Customers = m_Customers.Get(currentCustomerId);
                    ZaloPay m_Pay = new ZaloPay();
                    m_Pay.amount = int.Parse(model.Total.ToString("0"));
                    m_Pay.apptransid = DateTime.Now.ToString("yyMMdd") + paymentTransactions.PaymentTransactionId.ToString();
                    string[] responseObj = m_Pay.createOrder(currentCustomerId, mOrders.OrderId);
                    if (responseObj.Length > 1)// success
                    {
                        string returnCode = responseObj[0];
                        string returnDesc = responseObj[1];
                        if (returnCode == "1")
                        {
                            model.PayGateUrl = returnDesc;
                        }
                        else
                        {
                            m_AjaxResult.Message = returnDesc;
                        }
                    }
                    else
                    {
                        m_AjaxResult.Message = "Xửa lý giao dịch không thành công, mời bạn thử lại sau hoặc liên hệ Chăm sóc khách hàng để được trợ giúp.";
                    }
                }
            }

            return m_AjaxResult;
        }
        /// <summary>
        /// form nhập thông tin hóa đơn GTGT
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialTaxInvoice()
        {
            return PartialView("~/Views/AjaxTemplates/Account/PartialTaxInvoice.cshtml");
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult TaxInvoice(TaxInvoiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = model,
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialGopY()
        {
            return PartialView("~/Views/AjaxTemplates/Account/PartialGopY.cshtml");
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialDocSendMail()
        {
            return PartialView("~/Views/AjaxTemplates/Account/PartialDocSendMail.cshtml");
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult GopY(GopyModel model)
        {
            short sysMessageId = 0;
            int actUserId = 0;
            
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            var mFeedBacks = new FeedBacks
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                PhoneNumber = Extensions.GetCurrentCustomerMobile(),
                Email = Extensions.GetCurrentCustomerMail(),
                Comment = model.Content,
                SiteId = Constants.SiteId,
                Title = model.Title,
                FromIP = Request.UserHostAddress,
                Address = string.Empty
            };
            mFeedBacks.InsertOrUpdate(0, actUserId, ref sysMessageId);
            if (mFeedBacks.FeedBackId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = Resource.ThankYouForYourFeedback,
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult DocSendMail(DocSendMailModel model)
        {
            byte replicated = 0;
            int actUserId = 0;
            short sysMessageId = 0;
            //if (!model.DocSendMailCaptcha.Equals(model.DocSendMailCaptchaCode))
            //{
            //    return new AjaxResult
            //    {
            //        StatusCode = 200,
            //        AllowGet = false,
            //        Message = Resource.TheSecurityCodeIsIncorrect,
            //        Completed = false
            //    };
            //}

            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            var mMessageTemplates = new MessageTemplates().Get(Constants.MessageTemplateIdDocSendMail);
            var mMessageSends = new MessageSends
            {
                SiteId = Constants.SiteId,
                MessageTemplateId = Constants.MessageTemplateIdDocSendMail,
                SendFrom = mMessageTemplates.SendFrom,
                SendTo = model.Email,
                Title = Resource.LuatVietNamSendDocumentLink,
                MsgContent = mMessageTemplates.MsgContent.Replace("#Email#", Extensions.GetCurrentCustomerMail()).Replace("#DocUrl#", string.Concat(CmsConstants.WEBSITE_DOMAIN, model.DocUrl)),
                SendMethodId = 1, //email
                SendStatusId = 1, //chờ gửi
                SendTime = DateTime.Now
            };
            mMessageSends.Insert(replicated, actUserId, ref sysMessageId);

            if (mMessageSends.MessageSendId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.SendDocumentLinkSuccessful,
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialTermsConditionsView()
        {
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            int articleId = languageId == 1 ? Constants.QuyUocBaoMatArticleIdVNI : Constants.QuyUocBaoMatArticleIdEN;
            var model = ArticlesViewHelpers.View_GetArticleDetail(articleId, 0, 0, 0);
            return PartialView("~/Views/Shared/_PartialTermsConditionsView.cshtml", model);
        }

        [HttpGet]
        public JsonResult AutocompleteSignerByName(string signerName)
        {
            string jsonRetval = string.Empty;
            new Signers().Signers_GetIdAndNameByJson(signerName.Sanitize().StripTags(), Constants.RowAmount15, ref jsonRetval);
            //Signers_GetIdAndNameByJson(signerName.Sanitize().StripTags(), Constants.RowAmount15, ref jsonRetval);
            return Json(new { jsonRetval }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public PartialViewResult RenewalOfServiceAccountProfileSwitchMode(string mode = "edit")
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new AccountProfileModel
            {
                Mode = mode,
                CustomerName = mCustomer.CustomerName,
                FullName = mCustomer.CustomerFullName,
                GenderId = mCustomer.GenderId,
                CustomerMobile = mCustomer.CustomerMobile,
                Address = mCustomer.Address,
                Email = mCustomer.CustomerMail,
                Avatar = mCustomer.Avatar,
                ProvinceId = mCustomer.ProvinceId,
                DateOfBirth = mCustomer.DateOfBirth.toString()
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/{0}.cshtml", mode.Equals("edit") ? "PartialRenewalOfServiceAccountProfileSwitchEditMode" : "PartialRenewalOfServiceAccountProfileSwitchViewMode"), model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult EditExtensionServicePackagesAccountProfile(AccountProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            short sysMessageIdShort = 0;
            byte replicated = 0, sysMessageTypeId = 0;
            var mCustomers = new Customers().Get(currentCustomerId);
            mCustomers.CustomerFullName = model.FullName;
            mCustomers.CustomerMobile = model.CustomerMobile;
            sysMessageTypeId = mCustomers.InsertOrUpdate(replicated, actUserId, ref sysMessageIdShort);
            //if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
                Extensions.UpdateUserData();
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.UpdateSuccessfulAccountInformation,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/service-conversion.html"),
                    Completed = true
                };
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult SubscriptionNoticeOfValidity(int docId)
        {
            byte replicated = 0, sysMessageTypeId = 0;
            int actUserId = 0; short sysMessageId = 0;
            string message = Resource.PleaseTryAgainLater;
            if (docId > 0)
            {
                var mCustomerDocs = new CustomerDocs
                {
                    CustomerId = Extensions.GetCurrentCustomerId(),
                    DocId = docId,
                    LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                    RegistTypeId = 2
                };
                sysMessageTypeId = mCustomerDocs.Insert(replicated, actUserId, ref sysMessageId);
                //if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
                //{
                    message = Resource.SubscriptionNoticeOfValiditySuccessful;
                //}
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Completed = true //sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful
            };
        }

        #region Logs
        [HttpGet]
        public AjaxResult ArticleLogs(int articleId = 0, short categoryId = 0)
        {
            short sysMessageId = 0;
            byte replicated = 0, languageId = LawsVnLanguages.GetCurrentLanguageId();
            int actUserId = 0, articleViewLogByDayId = 0;
            if (articleId <= 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.PleaseTryAgainLater,
                    Completed = false
                };
            }
            var mArticleViewLogs = new ArticleViewLogs
            {
                ArticleId = articleId,
                SiteId = Constants.SiteId,
                LanguageId = languageId,
                DataTypeId = 1,
                CategoryId = categoryId,
                ApplicationTypeId = 1,
                UserAgent = string.Format("{0}||{1}", Request.UserAgent, Extensions.GetCurrentSessionId()),
                FromIP = Request.UserHostAddress,
                RefererFrom = HttpContext.Request.UrlReferrer != null
                    ? HttpContext.Request.UrlReferrer.ToString()
                    : "N/A",
                CustomerId = Extensions.GetCurrentCustomerId(),
                CrDateTime = DateTime.Now
            };

            mArticleViewLogs.Insert(replicated, actUserId, ref sysMessageId, ref articleViewLogByDayId);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = mArticleViewLogs.ArticleViewLogId > 0
            };
        }

        [HttpGet]
        public AjaxResult DocViewLogs(int docId = 0, byte docGroupId = 0, string actionTypeId = "")
        {
            byte languageId = LawsVnLanguages.GetCurrentLanguageId(), replicated = 0;
            int customerId = Extensions.GetCurrentCustomerId(), actUserId = 0;
            short sysMessageId = 0;

            if (docId < 0 || docGroupId < 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Tham số đầu vào không hợp lệ.",
                    Completed = false
                };
            }

            var docviewLogs = new DocViewLogs
            {
                DocId = docId,
                DocGroupId = docGroupId,
                DocFileId = 0,
                LanguageId = languageId,
                ApplicationTypeId = 1,
                UserAgent = string.Format("{0}||{1}", Request.UserAgent, Extensions.GetCurrentSessionId()),
                FromIP = Request.UserHostAddress,
                RefererFrom = HttpContext.Request.UrlReferrer != null
                    ? HttpContext.Request.UrlReferrer.ToString()
                    : "N/A",
                CustomerId = customerId,
                ActionTypeId = actionTypeId.ActionTypeIdByTab()
            };
            docviewLogs.Insert(replicated, actUserId, ref sysMessageId);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = docviewLogs.DocViewLogId > 0
            };
        }

        [HttpGet]
        [ValidateInput(false)]
        public AjaxResult DocSearchLogs(string keywords = "", string dateFrom = "", string dateTo = "",
            byte docTypeId = 0, short organId = 0, short signerId = 0, short fieldId = 0)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                byte languageId = LawsVnLanguages.GetCurrentLanguageId(), replicated = 0;
                int customerId = Extensions.GetCurrentCustomerId(), actUserId = 0;
                short sysMessageId = 0;
                DateTime dtFrom, dtTo;
                DateTime.TryParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom);
                DateTime.TryParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTo);
                new DocSearchLogs
                {
                    SearchKeyword = keywords,
                    DateFrom = dtFrom,
                    DateTo = dtTo,
                    DocTypeId = docTypeId,
                    OrganId = organId,
                    SignerId = signerId,
                    FieldId = fieldId,
                    LanguageId = languageId,
                    CustomerId = customerId
                }.Insert(replicated, actUserId, ref sysMessageId);
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true
            };
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public JsonResult IsCustomerNameExist(string customerName)
        {
            var retVal = new Customers { CustomerName = customerName }.Customers_GetByName();
            bool isExist = retVal.CustomerId > 0;
            if (!isExist)
                return Json(true, JsonRequestBehavior.AllowGet);

            string messages = String.Format(Resource.UserNameAlreadynUse, retVal.CustomerName);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult IsCustomerMailExist(string email)
        {
            var retVal = new Customers { CustomerMail = email }.Customers_GetByEmail();
            bool isExist = retVal.CustomerId > 0;

            if (!isExist)
                return Json(true, JsonRequestBehavior.AllowGet);

            string messages = String.Format(Resource.EmailAlreadynUse, retVal.CustomerMail);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }  


    }
}
