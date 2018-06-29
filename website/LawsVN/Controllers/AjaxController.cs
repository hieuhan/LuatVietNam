using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;
using LawsVN.Models;
using LawsVN.Models.Templates;
using ICSoft.CMSLib;
using LawsVN.FileUploadService;
using LawsVN.Models.Account;
using sms.utils;
using ICSoft.PartnerLib;
using System.Data;
using LawsVN.Models.Docs;

namespace LawsVN.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        [HttpGet]
        public PartialViewResult GetDocsNewest(byte docGroupId = 0, byte languageId = 0, int page = 1, byte usingDisplayTable = 0, byte paginationType = 1, string urlPaging = "", string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewest", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.View_GetDocsViewNewest(languageId,
                    docGroupId, pageSize, page > 0 ? page - 1 : page, usingDisplayTable, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    PaginationType = paginationType,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId,
                    UsingDisplayTable = usingDisplayTable
                }
            };
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsNewestHomeMobile.cshtml" : "~/Views/AjaxTemplates/DocsNewest.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult GetDocsNewestMobile(byte docGroupId = 0, byte languageId = 0, int page = 1, byte usingDisplayTable = 0, byte paginationType = 1, string urlPaging = "", string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewestMobile", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20, string Title = "", byte GroupId = 0, string Title_1 = "")
        {
            int rowCount = 0;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.View_GetDocsViewNewest(languageId,
                    docGroupId, showNumberOfResults, page > 0 ? page - 1 : page, usingDisplayTable, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ShowNumberOfResults = showNumberOfResults,
                    PaginationType = paginationType,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    TerName = Title,
                    LawTerminGroupId= GroupId,
                    SignerName= Title_1,

                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId,
                    UsingDisplayTable = usingDisplayTable
                }
            };
            return PartialView("~/Views/AjaxTemplates/Mobile/DocsNewestMobile.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult GetDocsNewest2(byte docGroupId = 0, byte languageId = 0, bool isMobile = false, int page = 1, byte usingDisplayTable = 0, byte paginationType = 2, string postTimeRight = "", string urlPaging = "", string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewest", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.View_GetDocsViewNewest(languageId,
                    docGroupId, showNumberOfResults, pageIndex, usingDisplayTable, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ShowNumberOfResults = showNumberOfResults,
                    PaginationType = paginationType,
                    PostTimeRight = postTimeRight,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    IsMobile = isMobile,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId,
                    UsingDisplayTable = usingDisplayTable
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", isMobile ? "Mobile/DocsNewest.Mobile" : "DocsNewest2"), docsNewest);
        }

        [HttpGet]
        public PartialViewResult Docs_GetViewEffect(string effectStatusName = "SapSuaDoi", byte languageId = 0, int page = 1, byte usingDisplayTable = 0, byte paginationType = 1, string urlPaging = "", string updateTargetId = "SecondBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "SecondBoxLoading", string controllerName = "Ajax", string actionName = "Docs_GetViewEffect", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewEffect(languageId, effectStatusName, pageSize, page > 0 ? page - 1 : page, usingDisplayTable, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    PaginationType = paginationType,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId
                    },
                    EffectStatusName = effectStatusName,
                    UsingDisplayTable = usingDisplayTable,
                    LanguageId = languageId
                }
            };
            if (Request.UserAgent.Contains("Mobi") == true)
            {
                return PartialView("~/Views/AjaxTemplates/Mobile/DocsNewestHomeMobile.cshtml", docsNewest);
            }
            else
            {
                return PartialView("~/Views/AjaxTemplates/DocsNewest.cshtml", docsNewest);
            }
        }

        [HttpGet]
        public PartialViewResult Docs_GetViewEffect2(string effectStatusName = "SapSuaDoi", byte languageId = 0, bool isMobile = false, int page = 1, byte usingDisplayTable = 0, byte paginationType = 1, string urlPaging = "", string postTimeRight = "", string updateTargetId = "SecondBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "SecondBoxLoading", string controllerName = "Ajax", string actionName = "Docs_GetViewEffect", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0;
            int pageIndex = page > 0 ? page - 1 : page;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewEffect(languageId, effectStatusName, showNumberOfResults, pageIndex, usingDisplayTable, 1, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    ShowNumberOfResults = showNumberOfResults,
                    PaginationType = paginationType,
                    PostTimeRight = postTimeRight,
                    UrlPaging = urlPaging,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    IsMobile= isMobile,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId
                    },
                    EffectStatusName = effectStatusName,
                    UsingDisplayTable = usingDisplayTable,
                    LanguageId = languageId
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", isMobile==true?"Mobile/DocsNewest.Mobile":"DocsNewest2"), docsNewest);
        }

        /// <summary>
        /// Docs_ViewSearch
        /// </summary>
        [HttpGet]
        public PartialViewResult Docs_ViewSearch(byte docGroupId = 0, bool isMobile = false, byte languageId = 0, int page = 1, short fieldId = 0,
            string effectStatusName = "", string orderBy = "", string classTagItem = "tag-item", byte effectStatusId = 0, short organId = 0, string postTimeRight = "",
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
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, showNumberOfResults, pageIndex, orderBy, languageId
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
                    IsMobile = isMobile,
                    UsingDisplayTable = usingDisplayTable,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        OnSuccess = "lawsVn.ajaxEvents.ListOnSuccess"
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
                    PostTimeRight = postTimeRight,
                    ClassTagItem = classTagItem
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
            string viewName = Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsListMobile.cshtml" : "~/Views/AjaxTemplates/DocsList.cshtml";
            return PartialView(viewName, model);
        }

        [HttpGet]
        public PartialViewResult CauHoiThuongGap_GetView(short cateId = 521, byte languageId = 0, int page = 1, string keyword = "", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0; string updateTargetId = "FirstBox"; InsertionMode insertionMode = InsertionMode.Replace; string loadingElementId = "FirstBoxLoading";
            int pageIndex = page > 0 ? page - 1 : page;
            var mNewsViewModel = new NewsViewModel
            {
                ListArticlesView = ArticlesViewHelpers.View_SearchWithContent(pageSize, pageIndex, languageId, 0, cateId, Constants.SiteId, 1, keyword, ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ControllerName = "Ajax",
                    ActionName = "CauHoiThuongGap_GetView",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                    },
                    CategoryId = cateId,
                    LanguageId = languageId
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Request.Browser.IsMobileDevice == true ? "Mobile/CauHoiThuongGapMobile" : "CauHoiThuongGap"), mNewsViewModel);
        }

        [HttpGet]
        public JsonResult AutocompleteSignerByName(string signerName)
        {
            string jsonRetval = string.Empty;
            new Signers().Signers_GetIdAndNameByJson(signerName.Sanitize().StripTags(), Constants.RowAmount20, ref jsonRetval);
            //Signers_GetIdAndNameByJson(signerName.Sanitize().StripTags(), Constants.RowAmount15, ref jsonRetval);
            return Json(new { jsonRetval }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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
            //if (mNewsletterEmail.NewsletterEmailId > 0)
            //{
                javaScript = Resource.YouSignupForASuccessfulNewsletter;
            //}
            return JavaScript(string.Format("lawsVn.successfulNewsletter('{0}')", javaScript));
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult CustomerRegisterNewsLetterEmail()
        {
            short sysMessageId = 0;
            byte replicated = 0;
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            var mNewsletterEmail = new NewsletterEmails
            {
                SiteId = Constants.SiteId,
                Email = Extensions.GetCurrentCustomerMail(),
                IsReceiveNews = 1,
                CustomerId = currentCustomerId
            };

            mNewsletterEmail.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
            if (mNewsletterEmail.NewsletterEmailId > 0)
            {
                return new AjaxResult
                {
                    AllowGet = true,
                    Message = Resource.YouSignupForASuccessfulNewsletter,
                    Completed = true
                };
            }
            return new AjaxResult
            {
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        #region Upload File

        [HttpPost]
        public AjaxResult UploadFile(string imageData, string imageName)
        {
            if (!string.IsNullOrEmpty(imageData) && !string.IsNullOrEmpty(imageName))
            {
                if (imageName.IsImage())
                {
                    string appKey = string.Concat(Constants.AppKey, DateTime.Now.ToString("ddMMyyyHH"));
                    byte[] data = Convert.FromBase64String(imageData);
                    string fileUploaded = new FileUploaderSoapClient().UploadFile(data, imageName, appKey, true);
                    if (!string.IsNullOrEmpty(fileUploaded) && fileUploaded.StartsWith("http") &&
                        !fileUploaded.Equals("unauthorized"))
                    {
                        return new AjaxResult
                        {
                            Completed = true,
                            Message = "Tải ảnh lên thành công.",
                            Data = fileUploaded
                        };
                    }
                }
                else
                {
                    return new AjaxResult
                    {
                        Completed = false,
                        Message = "Quý khách lòng chọn file ảnh dạng: .jpg, .jpeg, .png, .bmp, .gif"
                    };
                }
            }

            return new AjaxResult
            {
                Completed = false,
                Message = Resource.PleaseTryAgainLater
            };
        }

        [HttpPost]
        public AjaxResult LawerUploadFile(List<HttpPostedFileBase> files, int lawsId = 0)
        {
            if (files.HasValue() && lawsId > 0)
            {
                List<string> listFileUploaded = new List<string>();
                string appKey = string.Concat(Constants.AppKey, DateTime.Now.ToString("ddMMyyyHH"));
                int imageMaximumBytes = 2 * 1024 * 1024; //2MB
                foreach (var file in files)
                {
                    if (0 < file.ContentLength && file.ContentLength <= imageMaximumBytes)
                    {
                        using (BinaryReader binaryReader = new BinaryReader(file.InputStream))
                        {
                            byte[] data = binaryReader.ReadBytes((int)file.ContentLength);
                            string fileUploaded = new FileUploaderSoapClient().LawyersUploadFile(data, file.FileName, lawsId.ToString(), Constants.MediaLawyersPath, appKey, true);
                            if (!string.IsNullOrEmpty(fileUploaded) && fileUploaded.StartsWith("http") && !fileUploaded.Equals("unauthorized"))
                            {
                                listFileUploaded.Add(fileUploaded);
                            }
                        }
                    }
                    else
                    {
                        return new AjaxResult
                        {
                            Completed = false,
                            Message = "File tải lên không hợp lệ. Luật Việt Nam hỗ trợ dung lượng file 2MB."
                        };
                    }
                }
                if (listFileUploaded.Count > 0)
                {
                    return new AjaxResult
                    {
                        Completed = true,
                        Message = "Tải ảnh lên thành công.",
                        Data = listFileUploaded
                    };
                }
            }
            return new AjaxResult
            {
                Completed = false,
                Message = Resource.PleaseTryAgainLater
            };
        }

        #endregion

        /// <summary>
        /// Don't use
        /// </summary>
        [HttpGet]
        public PartialViewResult Docs_GetViewSearch(byte docGroupId = 1, short fieldId = 0, string effectStatusName = "", byte effectStatusId = 0, int page = 0, short organId = 0, byte docTypeId = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Docs_GetViewSearch", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, customerId = 0;
            string orderBy = string.Empty,
                searchKeyword = string.Empty,
                docName = string.Empty,
                docIdentity = string.Empty,
                searchByDate = string.Empty,
                dateFrom = string.Empty,
                dateTo = string.Empty;
            byte isSearchExact = 0, displayTypeId = 0, getRowCount = 1, languageId = LawsVnLanguages.GetCurrentLanguageId();
            short signerId = 0;
            var docNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, showNumberOfResults, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusName, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
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
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsList.cshtml", docNewest);
        }

        [HttpPost]
        [ValidateInput(false)]
        public PartialViewResult Docs_GetViewSearchWithKeyword(string keywords = "", string orderBy = "DocId DESC", short fieldId = 0, byte docGroupId = 1, byte languageId = 1, byte searchOptions = 1, byte isSearchExact = 1, short signerId = 0, short organId = 0, byte effectStatusId = 0, int page = 0, int year = 0, byte docTypeId = 0, string dateFrom = "", string dateTo = "", string updateTargetId = "ListDocsViews", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Docs_GetViewSearchWithKeyword", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, customerId = 0;
            keywords = keywords.StripTags().SanitizeWithoutSplash();
            string searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty;
            byte highlightKeyword = 1, displayTypeId = 0, getCountByGroup = 0;
            string effectStatusType = string.Empty, searchByDate = string.Empty;
            if (year > 0)
            { 
                dateFrom = string.Concat("1/1/", year);
                dateTo = string.Concat("31/12/", year);
            }
            else
            {
                DateTime dtFrom, dtTo;
                DateTime.TryParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dtFrom);
                dateFrom = dtFrom != DateTime.MinValue ? dtFrom.ToString("dd/MM/yyyy") : string.Empty;
                DateTime.TryParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dtTo);
                dateTo = dtTo != DateTime.MinValue ? dtTo.ToString("dd/MM/yyyy") : string.Empty;
            }
            if (docGroupId == 0)
            {
                getCountByGroup = 1;
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
                        searchKeyword = keywords;
                        break;
                    }
            }

            var model = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearchWithKeyword(0, showNumberOfResults, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, isSearchExact, highlightKeyword, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, 1, 0, 0, 0, 0, 0, 0, ref rowCount, getCountByGroup).lDocsView,
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
                        HttpMethod = "Post",
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        OnSuccess = "lawsVn.ajaxEvents.SearchOnSuccess"
                    },
                    DocGroupId = docGroupId,
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    Keywords = keywords,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    SearchOptions = searchOptions,
                    IsSearchExact = isSearchExact,
                    LanguageId = languageId,
                    SignerId = signerId,
                    Year = year
                }
            };
            if (model.ListDocsView.HasValue())
            {
                model.ListDocsView = model.ListDocsView.Join(model.ListEffectStatus, a => a.EffectStatusId,
                    b => b.EffectStatusId, (a, b) => new DocsView
                    {
                        DocId = a.DocId,
                        DocGroupId = a.DocGroupId,
                        DocUrl = a.GetDocUrl(),
                        DocName = a.DocName,
                        DocSummary = a.DocSummary,
                        IssueDate = a.IssueDate,
                        IssueYear = a.IssueYear,
                        EffectDate = a.EffectDate,
                        EffectStatusName = b.EffectStatusDesc
                    }).ToList();
            }
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsListSearchMobile.cshtml" : "~/Views/AjaxTemplates/SearchDocsList.cshtml", model);
        }

        //Don't use
        [HttpGet]
        public PartialViewResult TCVN_GetViewSearch(short fieldId = 0, string effectStatusName = "", byte effectStatusId = 0, byte languageId = 1, int page = 0, short organId = 0, byte docTypeId = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "TCVN_GetViewSearch", int pageSize = 20, int linkLimit = 5, int showNumberOfResults = 20, byte docGroupId = 3)
        {
            int rowCount = 0;
            string orderBy = string.Empty, searchKeyword = string.Empty, docName = string.Empty,
                searchByDate = string.Empty, dateFrom = string.Empty, dateTo = string.Empty,
                effectStatusType = string.Empty, docIdentity = string.Empty;
            short signerId = 0; int customerId = 0;
            byte getRowCount = 1, displayTypeId = 0, isSearchExact = 0;
            var model = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, showNumberOfResults, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
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
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/TCVNNewest.cshtml", model);
        }

        /// <summary>
        /// Don't use
        /// </summary>
        [HttpGet]
        public PartialViewResult DocsConsolidation_GetViewSearch(byte docGroupId = 0, short fieldId = 0, int page = 0, short organId = 0, int year = 0, string dateFrom = "", string dateTo = "", byte docTypeId = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "DocsConsolidation_GetViewSearch", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            string OrderBy = ""; byte LanguageId = 1; string SearchKeyword = ""; byte IsSearchExact = 0;
            string DocName = ""; string DocIdentity = "";
            short SignerId = 0; int CustomerId = 0;
            string EffectStatusType = ""; byte effectStatusId = 0;
            string SearchByDate = ""; byte GetRowCount = 1;

            //DocsViewHelpers.Docs_GetViewSearch(RowAmount, PageIndex, OrderBy, LanguageId, DocGroups.VBHN, SearchKeyword, IsSearchExact, DocName, DocIdentity, docTypeId, fieldId, organId, SignerId, CustomerId, effectStatusId, EffectStatusType, SearchByDate, DateFrom, DateTo, GetRowCount, ref RowCount);
            var docNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, showNumberOfResults, pageIndex, OrderBy, LanguageId
                    , docGroupId, SearchKeyword, IsSearchExact, 0, DocName, DocIdentity, docTypeId, fieldId, 0, organId, SignerId, 0, 0, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, dateFrom, dateTo, GetRowCount
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
                    DateFrom = dateFrom,
                    DateTo = dateTo
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsConsolidation.cshtml", docNewest);
        }

        [HttpGet]
        public ActionResult Customers_Docs(byte docGroupId = 0, byte languageId = 0, int page = 1, byte usingDisplayTable = 0, string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetDocsNewest", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int customerId = Extensions.GetCurrentCustomerId();
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount20, page > 0 ? page - 1 : page, "", 0, docGroupId, "", 0, 0, "", "", 0, 0, 0, 0, 0, 0, 0, customerId, 0, 0, "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId
                }
            };
            return PartialView("~/Views/AjaxTemplates/Customers_Docs.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult Customers_Interface(string effectStatusName = "", byte languageId = 0, int page = 1, byte usingDisplayTable = 0, string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "SecondBoxLoading", string controllerName = "Ajax", string actionName = "Customers_Interface", int pageSize = 5, int linkLimit = 3,string urlPaging = "")
        {
            int CustomerId = Extensions.GetCurrentCustomerId();
            string orderBy = string.Empty, searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty, searchByDate = string.Empty;
            byte docGroupId = 1;
            short signerId = 0; byte IsSearchExact = 0; byte DisplayTypeId = 0;
            string EffectStatusType = effectStatusName;
            string SearchByDate = "", DateFrom = "", DateTo = ""; byte GetRowCount = 1;
            short fieldId = 0; byte effectStatusId = 0; short organId = 0; byte docTypeId = 0;
            int rowCount = 0;
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.CustomerDocs_CustomerFields(0, pageSize, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                    },
                    EffectStatusName = effectStatusName,
                    LanguageId = languageId,
                    UrlPaging = urlPaging,
                }
            };
            if(docsNewest.ListDocsView.HasValue())
            {
                for(int i=0; i< docsNewest.ListDocsView.Count; i++)
                {
                    if (string.IsNullOrEmpty(docsNewest.ListDocsView[i].EffectStatusName))
                    {
                        docsNewest.ListDocsView[i].EffectStatusName = docsNewest.ListEffectStatus.GetEffectStatusNameById(docsNewest.ListDocsView[i].EffectStatusId).TrimmedOrDefault(string.Empty);
                    }
                }
            }
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsNewestHomeMobile.cshtml" : "~/Views/AjaxTemplates/Customers_Interface.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult Customers_InterfaceByField(byte docGroupId = 1, short ProvinceId = 0, short fieldId = 0, byte effectStatusId = 0, short organId = 0, byte docTypeId = 0, string effectStatusName = "", byte languageId = 0, int page = 1, byte usingDisplayTable = 0, string updateTargetId = "ContentByField", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "SecondBoxLoading", string controllerName = "Ajax", string actionName = "Customers_InterfaceByField", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            string orderBy = string.Empty, effectStatusType = string.Empty, searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty, searchByDate = string.Empty;
            short signerId = 0; byte IsSearchExact = 0; byte DisplayTypeId = 0;
            string EffectStatusType = effectStatusName;
            string SearchByDate = "", DateFrom = "", DateTo = ""; byte GetRowCount = 1;
            List<DocsView> lDocsViewsByCustomerFields = DocsViewHelpers.CustomerDocs_CustomerFields(0, pageSize, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, currentCustomerId, 0, effectStatusId, EffectStatusType, ProvinceId, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);
            CustomerFields mCustomerFields = new CustomerFields();
            List<CustomerFields> lCustomerFields = mCustomerFields.GetListFieldsByCustomerId(currentCustomerId);
            CustomerProvinces mCustomerProvinces = new CustomerProvinces();
            List<CustomerProvinces> lCustomerProvinces = mCustomerProvinces.GetListByCustomerId(currentCustomerId);
            var docsNewest = new CustomerDocsViewModel
            {
                lCustomerFields = lCustomerFields,
                ListDocsViewFirst = lDocsViewsByCustomerFields,
                lCustomerProvinces = lCustomerProvinces,
                ListDocTypes = LawsVN.Library.DropDownListHelpers.DocTypes_GetList(0),
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocGroupId = docGroupId,
                    TotalPage = rowCount,
                    DocTypeId = docTypeId,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                    },
                    EffectStatusName = effectStatusName,
                    LanguageId = languageId,
                    ProvinceId = ProvinceId,
                }
            };
            if (Extensions.IsMobile())
            {
                var docsNewest1 = new DocsNewest
                {
                    ListDocsView = lDocsViewsByCustomerFields,
                    PartialPaginationAjax = new PartialPaginationAjax
                    {
                        TotalPage = rowCount,
                        PageIndex = page,
                        LinkLimit = linkLimit,
                        PageSize = pageSize,
                        ControllerName = controllerName,
                        ActionName = actionName,
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = updateTargetId,
                            InsertionMode = insertionMode,
                            LoadingElementId = loadingElementId,
                        },
                        EffectStatusName = effectStatusName,
                        LanguageId = languageId
                    }
                };
                if (docsNewest1.ListDocsView.HasValue())
                {
                    for (int i = 0; i < docsNewest1.ListDocsView.Count; i++)
                    {
                        if (string.IsNullOrEmpty(docsNewest1.ListDocsView[i].EffectStatusName))
                        {
                            docsNewest1.ListDocsView[i].EffectStatusName = docsNewest.ListEffectStatus.GetEffectStatusNameById(docsNewest1.ListDocsView[i].EffectStatusId).TrimmedOrDefault(string.Empty);
                        }
                    }
                }
                return PartialView("~/Views/AjaxTemplates/Mobile/DocsNewestHomeMobile.cshtml", docsNewest1);
            }
            return PartialView("~/Views/AjaxTemplates/Customers_InterfaceByField.cshtml", docsNewest);
         }

        [HttpGet]
        public PartialViewResult Customers_InterfaceLocation(byte docGroupId = 2, short fieldId = 0, byte effectStatusId = 0, short organId = 0, byte provinceId = 0, byte docTypeId = 0, int year = 0, string effectStatusName = "", byte languageId = 0, int page = 1, byte usingDisplayTable = 0, string updateTargetId = "FirstBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "SecondBoxLoading", string controllerName = "Ajax", string actionName = "Customers_InterfaceLocation", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 5)
        {
            int rowCount = 0;
            if(showNumberOfResults>=5)
            {
                pageSize = showNumberOfResults;
            }
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            string orderBy = string.Empty, effectStatusType = string.Empty, searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty, searchByDate = string.Empty;
            short signerId = 0; byte IsSearchExact = 0; byte DisplayTypeId = 0; byte GetRowCount = 1;
            string dateFrom, dateTo = DateTime.Now.toString();
            switch (year)
            {
                case 1:
                {
                    dateFrom = DateTime.Now.AddDays(-7).toString();
                    break;
                }
                case 2:
                {
                    dateFrom = DateTime.Now.AddMonths(-1).toString();
                    break;
                }
                case 3:
                {
                    dateFrom = DateTime.Now.AddYears(-1).toString();
                    break;
                }
                default:
                    dateFrom = string.Empty;
                    break;
            }
            List<DocsView> lDocsViewsByCustomerFields = DocsViewHelpers.CustomerDocs_CustomerFields(0, pageSize, page > 0 ? page - 1 : page, orderBy, languageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, currentCustomerId, 0, effectStatusId, effectStatusType, provinceId, 0, 0, searchByDate, dateFrom, dateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);

            var docsNewest = new DocsNewest
            {
                ListDocsView = lDocsViewsByCustomerFields,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    DocGroupId = docGroupId,
                    ProvinceId = provinceId,
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    TotalPage = rowCount,
                    DocTypeId = docTypeId,
                    Year = year,
                    PageIndex = (Extensions.IsMobile() ? page - 1 : page),
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ShowNumberOfResults = showNumberOfResults,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode,
                        LoadingElementId = loadingElementId,
                    },
                    EffectStatusName = effectStatusName,
                    LanguageId = languageId
                }
            };
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsListMobile.cshtml" : "~/Views/AjaxTemplates/Customers_Interface.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult Customers_InterfaceTCVN(short fieldId = 0, string effectStatusName = "", byte effectStatusId = 0, int page = 0, short organId = 0, byte docTypeId = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Customers_InterfaceTCVN", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 10, byte docGroupId = 3)
        {
            if (page > 0)
            {
                page = page - 1;
            }
            int rowCount = 0;
            string orderBy = ""; byte languageId = 1; string searchKeyword = ""; byte isSearchExact = 0;
            string docName = ""; string docIdentity = "";
            short signerId = 0; int customerId = Extensions.GetCurrentCustomerId();
            string effectStatusType = "";
            string searchByDate = "", dateFrom = "", dateTo = ""; byte getRowCount = 1;
            byte DisplayTypeId = 0;
            List<DocsView> listDocsView = DocsViewHelpers.CustomerDocs_CustomerFields(0, showNumberOfResults, page, orderBy, languageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);
            if (listDocsView.HasValue())
            {
                listDocsView = listDocsView.Join(DropDownListHelpers.GetAllEffectStatus(), a => a.EffectStatusId,
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
            var docNewest = new DocsNewest
            {
                ListDocsView = listDocsView,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
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
                    FieldId = fieldId,
                    EffectStatusId = effectStatusId,
                    OrganId = organId,
                    DocTypeId = docTypeId,
                    DocGroupId = docGroupId,
                }
            };
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/DocsListMobile.cshtml" : "~/Views/AjaxTemplates/Customers_Interface.cshtml", docNewest);
        }

        [HttpGet]
        public ActionResult Customer_ReportEffectStatus(byte docGroupId = 0, byte languageId = 0, int page = 1, byte usingDisplayTable = 0, int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int CustomerId = 650310;// Extensions.GetCurrentCustomerId()
            var docsNewest = new DocsNewest
            {
                ListDocsView = DocsViewHelpers.Docs_GetViewSearch(0, pageSize, page > 0 ? page - 1 : 0, "", LawsVnLanguages.GetCurrentLanguageId()
                , 0, "", 0, 0, "", "", 0, 0, 0, 0, 0, 0, 0, CustomerId, 2, 0, "SapHetHieuLuc", 0, 0, 0, "ExpireDate", "", "", 1
                , 0, 0, 0, 0, 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    LinkLimit = linkLimit,
                    PageSize = pageSize,
                    ControllerName = "Ajax",
                    ActionName = "Customer_ReportEffectStatus",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "FirstBox",
                        InsertionMode = InsertionMode.Replace,
                        LoadingElementId = "FirstBoxLoading"
                    },
                    DocGroupId = docGroupId,
                    LanguageId = languageId
                }
            };
            return PartialView("~/Views/AjaxTemplates/Customers_Docs.cshtml", docsNewest);
        }

        [HttpGet]
        public PartialViewResult DocsConsolidation_GetDocRelateType(short RelateTypeId = 0, int DocId = 0, int page = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "DocsConsolidation_GetDocRelateType", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 10)
        {
            int RowCount = 0;
            int RowAmount = showNumberOfResults, PageIndex = page >= 1 ? page - 1 : page;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            DocsViewDetail mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(languageId, DocId, RelateTypeId, "", RowAmount, PageIndex, 1, ref RowCount);
            var docNewest = new DocsNewest
            {
                mDocsViewDetail = mDocsViewDetail,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = RowCount,
                    PageIndex = PageIndex,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = linkLimit,
                    PageSize = RowAmount,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    },
                    DocId = DocId,
                    RelateTypeId = RelateTypeId
                }
            };
            return PartialView("~/Views/AjaxTemplates/DocsConsolidation_GetDocRelateType.cshtml", docNewest);
        }

        [HttpGet]
        public PartialViewResult Docs_GetRelate(short relateTypeId = 0, int docId = 0, string displayPosition = "", int page = 0, string updateTargetId = "ListDocRelate", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "Docs_GetRelate", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0;
            int pageIndex = page > 0  ? page - 1 : page;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            DocsViewDetail mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(languageId, docId, relateTypeId, displayPosition, showNumberOfResults, pageIndex, 1, ref rowCount, 1);
            var docNewest = new DocsNewest
            {
                mDocsViewDetail = mDocsViewDetail,
                mRelateTypes = mDocsViewDetail.lRelateTypes.FirstOrDefault(m=>m.RelateTypeId == relateTypeId),
                CountByRelateTypeId = mDocsViewDetail.lRelateTypes.Sum(x => x.RowCount),
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
                    RelateTypeId = relateTypeId,
                    DisplayPosition = displayPosition
                }
            };
            if (docNewest.mDocsViewDetail.lDocRelates.HasValue())
            {
                docNewest.mDocsViewDetail.lDocRelates = (from r in mDocsViewDetail.lDocRelates
                                                         join e in docNewest.ListEffectStatus on r.EffectStatusId equals e.EffectStatusId
                                                        select new DocRelates
                                                        {
                                                            DocName = r.DocName,
                                                            DocId = r.DocId,
                                                            DocUrl = r.DocUrl,
                                                            EffectStatusName = e.EffectStatusDesc,
                                                            RelateTypeName = r.RelateTypeName,
                                                            CrDateTime = r.CrDateTime,
                                                            EffectDate = r.EffectDate,
                                                            IssueDate = r.IssueDate,
                                                            IssueYear = r.IssueYear
                                                        }).ToList();
            }
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() == true ? "Mobile/Docs_GetRelateMobile" : "Docs_GetRelate"), docNewest);
        }

        [HttpGet]
        public PartialViewResult TNPL_GetViewSearch(byte lawTerminGroupId = 0, string terName = "", int page = 0, string updateTargetId = "ListLawTerminsTab", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "TNPL_GetViewSearch", int pageSize = 20, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, pageIndex = page > 0 ? page -1 : page;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            var lLawTermins = new LawTermins().GetPage(0, showNumberOfResults, pageIndex, "TermName", languageId, 0, terName.StripTags().Sanitize(), 0, lawTerminGroupId, "", "", ref rowCount);
            var docNewest = new DocsNewest
            {
                l_LawTermins = lLawTermins,
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
                        InsertionMode = insertionMode,
                        OnSuccess = "lawsVn.ajaxEvents.LawTerminsOnSuccess"
                    },
                    TerName = terName,
                    LawTerminGroupId = lawTerminGroupId
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() ? "TNPL_GetViewSearchMobile" : "TNPL_GetViewSearch"), docNewest);
        }

        [HttpGet]
        public JsonResult AutocompleteTNPLByName(string lawterminName)
        {
            string jsonRetval = string.Empty;
            new LawTermins().LawTermins_GetIdAndNameByJson(lawterminName.Sanitize().StripTags(), Constants.RowAmount15, ref jsonRetval);
            return Json(new { jsonRetval }, JsonRequestBehavior.AllowGet);
        }

        #region Account

        [HttpPost]
        [LawsVnAuthorize]
        public PartialViewResult AccountProfileSwitchMode(string mode = "edit")
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
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml", mode.Equals("edit") ? "PartialAccountProfileSwitchEditMode" : "PartialAccountProfileSwitchViewMode"), model);
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
                OrganTaxCode = mCustomer.OrganTaxCode,
                AccountNumber = mCustomer.AccountNumber
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml", mode.Equals("edit") ? "BusinessInformationSwitchEditMode" : "BusinessInformationSwitchViewMode"), model);
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
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml", mode.Equals("edit") ? "PartialRenewalOfServiceAccountProfileSwitchEditMode" : "PartialRenewalOfServiceAccountProfileSwitchViewMode"), model);
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
            byte replicated = 0;
            var mCustomers = new Customers().Get(currentCustomerId);
            mCustomers.CustomerFullName = model.FullName;
            mCustomers.CustomerMobile = model.CustomerMobile;
            mCustomers.InsertOrUpdate(replicated, actUserId, ref sysMessageIdShort);

            Extensions.UpdateUserData();
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.UpdateSuccessfulAccountInformation,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/nang-cap-goi-dich-vu.html"),
                Completed = true
            };
        }

        /// <summary>
        /// Danh sách lĩnh vực văn bản quan tâm của người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult EditCustomerFields(byte docGroupId=1)
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new LawsCustomerFields
            {
                ListCustomerFields = new CustomerFields().GetListFieldsById(currentCustomerId, docGroupId)
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialEditCustomerFields.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult EditCustomerFieldsTCVN()
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            List<CustomerFields> lCustomerFields = new CustomerFields().GetListFieldsById(currentCustomerId, Constants.DocGroupIdTcvn);
            var model = new LawsCustomerFields
            {
                ListCustomerFields = lCustomerFields,
                ListFields = DropDownListHelpers.GetAllFieldDisplays_OrderBy(Constants.FieldDisplayTypeIdTcvn)
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialEditCustomerFieldsTCVN.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult EditCustomerFieldsV2()
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new LawsCustomerFieldsV2
            {
                ListCustomerFields = new CustomerFields().GetListFieldsByCustomerId2(currentCustomerId, Constants.ReviewStatusIdDaDuyet)
            };
            return PartialView(
                string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml",
                    Extensions.IsMobile()
                        ? "PartialEditCustomerFieldsV2.Mobile"
                        : "PartialEditCustomerFieldsV2"), model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult EditCustomerProvices()
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomerProvinces = new LawsCustomerProvinces
            {
                ListCustomerProvinces = new CustomerProvinces().GetListByCustomerId(currentCustomerId)
            };
            return PartialView(
                string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml",
                    Extensions.IsMobile()
                        ? "PartialEditCustomerProvinces.Mobile"
                        : "PartialEditCustomerProvinces"), mCustomerProvinces);
        }
        
        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult DeleteOneFieldById(int customerFieldId = 0)
        {
            byte replicated = 0, sysMessageTypeId = 0;
            int actUserId = 0, sysMessageId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            string message = Resource.PleaseTryAgainLater;
            if (customerFieldId > 0)
            {
                List<CustomerFields> listCustomerFields = new CustomerFields().GetListFieldsByCustomerId(currentCustomerId);
                if (listCustomerFields.Count > 0)
                {
                    var mCustomerFields = new CustomerFields
                    {
                        CustomerFieldId = customerFieldId
                    };
                    sysMessageTypeId = mCustomerFields.Delete(replicated, actUserId, ref sysMessageId);
                    sysMessageTypeId = 1;//them vi sysMessageTypeId tra ra bang 0
                    if (sysMessageTypeId == Constants.SysMesssageTypeIdDeleteSuccessful)
                    {
                        return new AjaxResult
                        {
                            StatusCode = 200,
                            AllowGet = true,
                            Message = Resource.DeleteTheFieldOfSuccess,
                            ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#linhvucvanbanquantam"),
                            Completed = true
                        };
                    }
                }
                //message = Resource.CanNotDeleteSingleFieldOfInterest;
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Completed = false
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult MyDocuments_GetPage(byte registTypeId = 1, byte docGroupId = 0, short fieldId = 0, short organId = 0, byte effectStatusId = 0, byte docTypeId = 0, int showNumberOfResults = 20, byte languageId = 1, int page = 1, string updateTargetId = "MyDocumentsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "MyDocuments_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new MyDocuments
            {
                mDocsViewSearch = DocsViewHelpers.MyDocuments_ViewGetPage(currentCustomerId, docGroupId, registTypeId, languageId, fieldId, organId, effectStatusId, docTypeId, "", "", "", showNumberOfResults, page > 0 ? page - 1 : page, 0, 0, ref rowCount),
                RowCount = rowCount,
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
                    LanguageId = languageId,
                    ControllerName = "Ajax",
                    ActionName = "MyDocuments_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyDocumentsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            model.ListMyDocuments = model.mDocsViewSearch.lDocsView.Join(model.ListEffectStatus, a => a.EffectStatusId,
                b => b.EffectStatusId, (a, b) => new MyDocumentsModel
                {
                    EffectStatusName = b.EffectStatusDesc,
                    DocsView = a
                });
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() == true ? "Mobile/MyDocumentsMobile" : "Account/Profile/PartialMyDocuments"), model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult NoticeOfValidity_GetPage(byte registTypeId = 2, short fieldId = 0, int showNumberOfResults = 20, int year = 0, byte languageId = 1, int page = 1, string updateTargetId = "NoticeOfValidityBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "NoticeOfValidity_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            string dateFrom, dateTo = DateTime.Now.toString();
            switch (year)
            {
                case 1:
                {
                    dateFrom = DateTime.Now.AddYears(-1).toString();
                    break;
                }
                case 2:
                {
                    dateFrom = DateTime.Now.AddMonths(-1).toString();
                    break;
                }
                case 3:
                {
                    dateFrom = DateTime.Now.AddDays(-7).toString();
                    break;
                }
                default:
                    dateFrom = string.Empty;
                    break;
            }
            var model = new NoticeOfValidityModel()
            {
                mDocsViewSearch = DocsViewHelpers.MyDocuments_ViewGetPage(currentCustomerId, 0, registTypeId, languageId, fieldId, 0, 0, 0, dateFrom,dateTo, "", showNumberOfResults, page > 0 ? page - 1 : page, 1, 1, ref rowCount),
                RowCount=rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = linkLimit,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = showNumberOfResults,
                    RegistTypeId = registTypeId,
                    FieldId = fieldId,
                    ControllerName = "Ajax",
                    ActionName = "NoticeOfValidity_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "NoticeOfValidityBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            model.ListMyDocuments = model.mDocsViewSearch.lDocsView.Join(model.ListEffectStatus, a => a.EffectStatusId,
                b => b.EffectStatusId, (a, b) => new MyDocumentsModel
                {
                    EffectStatusName = b.EffectStatusDesc,
                    DocsView = a
                });
            
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() == true ? "Mobile/PartialNoticeOfValidityMobile" : "Account/Profile/PartialNoticeOfValidity"), model);
            //return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialNoticeOfValidity.cshtml", model);
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
                    LanguageId = LawsVnLanguages.GetCurrentLanguageId()
                };
                sysMessageTypeId = mCustomerDocs.Insert(replicated, actUserId, ref sysMessageId);
                //ToDo: sysMessageTypeId = 3 : Đã thêm rồi -> ko tăng count

                //if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful || sysMessageTypeId == 3)
                //{
                    message = Resource.AddDocumentOfInterestToSuccess;
                //}
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = new CustomerDocs { DocId = docId, RegistTypeId = sysMessageTypeId },
                Message = message,
                Completed = true //sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful || sysMessageTypeId == 3
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult SubscriptionNoticeOfValidity(int docId)
        {
            byte replicated = 0;
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
                mCustomerDocs.Insert(replicated, actUserId, ref sysMessageId);
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
                Completed = true//sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult MyMessages_GetPage(byte messageTypeId = 1, int showNumberOfResults = 20, byte languageId = 1, int page = 1, string updateTargetId = "MyDocumentsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "MyDocuments_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new MyMessagesModel
            {
                ListMessages = new Messages(Constants.EXT_CONSTR) { UserId = currentCustomerId, MessageTypeId = messageTypeId, HasRead = Constants.HasReadGetAllMessages }.GetPage(showNumberOfResults, page > 0 ? page - 1 : page, "", "", "", ref rowCount),
                RowCount = rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    MessageTypeId = messageTypeId,
                    ShowNumberOfResults = showNumberOfResults,
                    IsMyMessage = true,
                    ControllerName = "Ajax",
                    ActionName = "MyMessages_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyMessagesBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() == true ? "Mobile/MyMessageMobile" : "Account/Profile/PartialMyMessages"), model);
            //return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialMyMessages.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult SavedMessages_GetPage(byte messageTypeId = 1, int showNumberOfResults = 20, byte languageId = 1, int page = 1, string updateTargetId = "MyDocumentsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "MyDocuments_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new MyMessagesModel
            {
                ListMessages = new Messages(Constants.EXT_CONSTR) { UserId = currentCustomerId, MessageTypeId = messageTypeId, HasSave = 1, HasRead = Constants.HasReadGetAllMessages}.GetPage(showNumberOfResults, page > 0 ? page - 1 : page, "", "", "", ref rowCount),
                RowCount = rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page > 0 ? page - 1 : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    MessageTypeId = Constants.MessageTypeIdInbox,
                    ShowNumberOfResults = showNumberOfResults,
                    IsMyMessage = true,
                    ControllerName = "Ajax",
                    ActionName = "SavedMessages_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "SaveMessagesBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/{0}.cshtml", Extensions.IsMobile() == true ? "Mobile/MyMessageMobile" : "Account/Profile/PartialSaveMessages"), model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult DeleteDocument(byte docGroupId, int docId, byte type = 1)
        {
            if (docId > 0)
            {
                byte replicated = 0, sysMessageTypeId = 0, languageId = LawsVnLanguages.GetCurrentLanguageId();
                int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId(); short sysMessageId = 0;
                var mCustomerDocs = new CustomerDocs
                {
                    CustomerId = currentCustomerId,
                    DocId = docId,
                    LanguageId = languageId
                };
                sysMessageTypeId = mCustomerDocs.Delete(replicated, actUserId, ref sysMessageId);
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    //Message = "Xóa văn bản quan tâm thành công.",
                    ReturnUrl = string.Format("{0}{1}", CmsConstants.ROOT_PATH, type == 1 ? string.Concat("user/van-ban-cua-toi.html?docGroupId=",docGroupId) : "user/thong-bao-hieu-luc.html"),
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
        public AjaxResult MessageDelete(int[] messageId, byte actionTypeId)
        {
            if (messageId != null && messageId.Length > 0)
            {
                int currentCustomerId = Extensions.GetCurrentCustomerId();
                foreach (var item in messageId)
                {
                    var mMessages = new Messages(Constants.EXT_CONSTR)
                    {
                        MessageId = item
                    };
                    mMessages.Delete(currentCustomerId);
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = string.Format("Xóa {0} tin nhắn thành công.", messageId.Length),
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, actionTypeId == 1 ? "user/tin-nhan.html" : "user/tin-nhan-da-luu.html"),
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
        public AjaxResult MessageSave(int[] messageId)
        {
            if (messageId != null && messageId.Length > 0)
            {
                foreach (var item in messageId)
                {
                    var mMessages = new Messages(Constants.EXT_CONSTR)
                    {
                        MessageId = item,
                        HasSave = 1
                    };
                    mMessages.UpdateHasSave();
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = string.Format("Lưu {0} tin nhắn thành công.", messageId.Length),
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/tin-nhan.html"),
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
        public AjaxResult MessageUnSave(int[] messageId, byte actionTypeId)
        {
            if (messageId != null && messageId.Length > 0)
            {
                foreach (var item in messageId)
                {
                    var mMessages = new Messages(Constants.EXT_CONSTR)
                    {
                        MessageId = item,
                        HasSave = 0
                    };
                    mMessages.UpdateHasSave();
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = string.Format("Bỏ lưu {0} tin nhắn thành công.", messageId.Length),
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, actionTypeId == 2 ? "user/tin-nhan-da-luu.html" : "user/tin-nhan.html"),
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
        public PartialViewResult MessageRead(int messageId)
        {
            var mMessages = new Messages(Constants.EXT_CONSTR).Get(messageId);
            mMessages.UpdateHasRead();
            var model = new MyMessagesModel
            {
                mMessages = mMessages
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialMessageContent.cshtml", model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult MarkAllRead()
        {
            int userId = Extensions.GetCurrentCustomerId();
            new Messages(Constants.EXT_CONSTR) { UserId = userId }.UpdateHasRead_AllByUserId();
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Đánh dấu tất cả tin nhắn đã đọc thành công.",
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult MessagesSetStar(int messageId, byte actionType)
        {
            new Messages(Constants.EXT_CONSTR) { MessageId = messageId, HasStar = 1 }.UpdateHasStar();
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.SetstarTheMessageForSuccess,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, actionType == 1 ? "user/tin-nhan.html" : "user/tin-nhan-da-luu.html"),
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult MessagesUnStar(int messageId, byte actionType)
        {
            new Messages(Constants.EXT_CONSTR) { MessageId = messageId, HasStar = 0 }.UpdateHasStar();
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.UnstarTheMessageForSuccess,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, actionType == 1 ? "user/tin-nhan.html" : "user/tin-nhan-da-luu.html"),
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult HistoryTransactions_GetPage(int showNumberOfResults = 20, byte languageId = 1, byte transactionStatusId = 1, int year = 0, int page = 0, string updateTargetId = "HistoryTransactionsBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "HistoryTransactions_GetPage", int pageSize = 5, int linkLimit = 5)
        {
            int rowCount = 0, totalMoney = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            string dateFrom, dateTo = DateTime.Now.toString();
            switch (year)
            {
                case 1:
                    {
                        dateFrom = DateTime.Now.AddYears(-1).toString();
                        break;
                    }
                case 2:
                    {
                        dateFrom = DateTime.Now.AddMonths(-1).toString();
                        break;
                    }
                case 3:
                    {
                        dateFrom = DateTime.Now.AddDays(-7).toString();
                        break;
                    }
                default:
                    dateFrom = string.Empty;
                    break;
            }
            var list = new PaymentTransactions
            {
                CustomerId = currentCustomerId,
                SiteId = Constants.SiteId,
                TransactionDesc = string.Empty,
                TransactionCode = string.Empty,
                TransactionStatusId = Constants.TransactionStatusIdSuccess
            }.GetPageView(0, showNumberOfResults, page > 0 ? page - 1 : page, "", 0, dateFrom, "", ref rowCount, ref totalMoney);
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
                                ? string.Format("{0:#,###} {1}", paymentTransaction.Amount, CmsConstants.CURRENCY_VND)
                                : "0")
                            : "0",
                    PaymentTypeId = paymentTransaction.PaymentTypeId,
                    ServiceId = (short) (l1 != null ? l1.ServiceId : 0),
                    ServicePackageId = paymentTransaction.ServicePackageId,
                    ServicePackageDesc = l1 == null ? string.Empty : l1.ServicePackageDesc.TrimmedOrDefault(string.Empty)
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
                    ServiceDesc = l2 == null ? string.Empty : l2.ServiceDesc.TrimmedOrDefault(string.Empty),
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
                    PaymentTypeDesc = l3 == null ? string.Empty : l3.PaymentTypeDesc.TrimmedOrDefault(string.Empty)
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
                    TransactionTypesDesc = l4 == null ? string.Empty : l4.TransactionTypeDesc.TrimmedOrDefault(string.Empty)
                };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/PartialPaymentTransactions_GetPage{0}.cshtml", Request.Browser.IsMobileDevice == true ? ".Mobile" : ""), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult PartialLogin()
        {
            //if (Request.IsAuthenticated)
            //{
            //    Extensions.Logout(Session,Response);
            //}
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialLogin.cshtml");
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult PartialForgotPassword()
        {
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Account/Profile/PartialForgotPasswordMobile.cshtml" : "~/Views/AjaxTemplates/Account/Profile/PartialForgotPassword.cshtml");
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialSendQuestions()
        {
            var model = new SendQuestionsModel
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                Email = Extensions.GetCurrentCustomerMail(),
                Mobile = Extensions.GetCurrentCustomerMobile()
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/{0}.cshtml", Request.Browser.IsMobileDevice == true ? "PartialSendQuestionsMobile" : "PartialSendQuestions"), model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialGopY()
        {
            var model = new GopYModel
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                Email = Extensions.GetCurrentCustomerMail(),
                Mobile = Extensions.GetCurrentCustomerMobile()
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialGopY.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialDocSendMail()
        {
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialDocSendMail.cshtml");
        }

        [HttpGet]
        public PartialViewResult PartialRegisterMail()
        {
            return PartialView("~/Views/AjaxTemplates/PartialRegisterMail.cshtml");
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialLawerQuestion(int lawerid)
        {
            var model = new LawerQuestionModel();
            {
                model.LawerId = lawerid;
            }
            return PartialView("~/Views/AjaxTemplates/Lawers/PartialLawerQuestion.cshtml", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult FaqViewDetail(int faqId)
        {
            var model = new LawerQuestionModel
            {
                mFaqs = new Faqs().Get(faqId)
            };
            return PartialView("~/Views/AjaxTemplates/Lawers/FaqViewDetail.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialChangePassword()
        {
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialChangePassword.cshtml");
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = string.Join("<br/>", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)),
                    Completed = false
                };
            }
            var changePasswordResult = ICSoft.CMSViewLib.CustomerHelpers.ChangePass(model.LawsUser.CustomerName,
                model.Password, model.CurrentPassword, Request.UserHostAddress, Request.UserAgent, LawsVnLanguages.GetCurrentLanguageId());

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = changePasswordResult.ActionMessage,
                Completed = "OK".Equals(changePasswordResult.ActionStatus)
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult CustomerDocs_GetViewNewest()
        {
            List<DocsView> model =
                DocsViewHelpers.CustomerDocs_GetViewNewest(LawsVnLanguages.GetCurrentLanguageId(), Extensions.GetCurrentCustomerId(), 1,
                    Constants.RowAmount5);
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialCustomerDocs.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult CustomerDocs_GetViewEffect(string effectStatus = "")
        {
            int rowCount = 0;
            List<DocsView> model = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount5, 0, string.Empty,
                1, 0, string.Empty, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, Extensions.GetCurrentCustomerId(), 0, 0, effectStatus,
                0, 0, 0, string.Empty, string.Empty, String.Empty, 0, 0, 0, 0, 0, 0, 0, ref rowCount);
                //DocsViewHelpers.CustomerDocs_GetViewEffect(LawsVnLanguages.GetCurrentLanguageId(), Extensions.GetCurrentCustomerId(), effectStatus,
                //    Constants.RowAmount5);
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialCustomerDocs.cshtml", model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult ReadNotifyMessages(int messageId = 0, string targetUrl = "")
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            new ICSoft.CMSLibEstate.NotifyMessages(Constants.EXT_CONSTR)
            {
                MessageId = messageId,
                HasRead = 1
            }.UpdateHasRead(currentCustomerId);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                ReturnUrl = targetUrl,
                Completed = true
            };

        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult ReadAllNotifyMessages()
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            new ICSoft.CMSLibEstate.NotifyMessages(Constants.EXT_CONSTR).UpdateHasRead_AllByUserId(currentCustomerId);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Tất cả thông báo đã được đánh dấu đã đọc.",
                ReturnUrl = CmsConstants.ROOT_PATH,
                Completed = true
            };
        }

        #region Services

        /// <summary>
        /// Đăng ký dịch vụ miễn phí
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult RegisterFreeService(short serviceId = 0)
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            string message = Resource.PleaseTryAgainLater;

            if (serviceId > 0)
            {
                new CustomerServices().CustomerServices_LVN_DangKyMienphi(currentCustomerId, string.Empty, Request.UserHostAddress, ref message);
                //ToDo Thông báo về trang user/thong-bao-dich-vu.html
                TempData.Set("ServicePackagesMessages", message);
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = message,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-bao-dang-ky-dich-vu-mien-phi.html"),
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Completed = false
            };
        }

        /// <summary>
        /// Đăng ký dịch vụ dùng thử 15 ngày
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [LawsVnAuthorize]
        public AjaxResult RegisterTrialService(short serviceId = 0)
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            string message = Resource.PleaseTryAgainLater;

            if (serviceId > 0)
            {
                new CustomerServices().CustomerServices_LVN_DangKyDungThu(currentCustomerId, string.Empty, Request.UserHostAddress, ref message);
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = message,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html"),
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Completed = false
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public AjaxResult PromotionCodeCheck(PromotionCodeCheckModel model)
        {
            string message = "Quý khách chưa nhập mã khuyến mãi cần kiểm tra.";
            float amount = 0, percentDecrease = 0;
            if (!string.IsNullOrEmpty(model.PromotionCode))
            {
                message = new PromotionCodes().PromotionCodes_Check(model.PromotionCode, ref amount,
                    ref percentDecrease);
                if (message.Equals("OK"))
                {
                    var listPromotions =
                        new Promotions(DocConstants.DOC_CONSTR).Promotions_GetByPromotionCode(model.PromotionCode);
                    if (listPromotions.HasValue())
                    {
                        var promotion = listPromotions[0];
                        promotion.PromotionDesc = model.PromotionCode; // gán mã khuyến mại
                        return new AjaxResult
                        {
                            StatusCode = 200,
                            AllowGet = true,
                            Data = promotion,
                            Completed = true
                        };
                    }
                }
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = message,
                Completed = false
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateInput(false)]
        public AjaxResult CustomerNameCheck(string customerName = "")
        {
            string messages = "Quý khách vui lòng nhập tên truy cập cần kiểm tra.";
            if (!string.IsNullOrEmpty(customerName))
            {
                var customer = new Customers { CustomerName = customerName.StripTags().Sanitize() }.Customers_GetByName();
                messages = customer.CustomerId > 0
                    ? string.Format("Tài khoản {0} hợp lệ.", customer.CustomerName)
                    : string.Format("Tên tài khoản {0} không tồn tại.", customerName);
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                Completed = true
            };
        }

        /// <summary>
        /// form nhập thông tin hóa đơn GTGT
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialTaxInvoice()
        {
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialTaxInvoice.cshtml");
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

        //không dùng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        /// <param name="servicePackageParentId">Goi dich vu cha</param>
        /// <param name="servicePackageId">Goi dich vu con</param>
        /// <param name="typeChange">0:ServicePackageParentId thay đổi, 1:ServicePackageId thay đổi, 2: ServiceId thay đổi </param>
        /// <returns></returns>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialRegisterServicePackages(short serviceId = 15, short servicePackageParentId = 0, short servicePackageId = 0, byte typeChange = 0)
        {
            short[] servicesArray = { Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao };
            if (!Array.Exists(servicesArray, s => s == serviceId))
            {
                serviceId = Constants.ServiceIdTraCuuTieuChuan;
            }

            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId,2);

            var listServicePackagesParent = listServicePackagesByServiceId
                .Where(item => item.ServicePackageParentId == 0)
                .OrderBy(item => item.ConcurrentLogin).ToList();

            List<ServicePackages> listServicePackagesChild = null;
            ServicePackages servicePackages = null;
            int total = 0, priceVat = 0;
            if (listServicePackagesParent.HasValue())
            {
                servicePackages = typeChange == 2 ? listServicePackagesParent[0] : listServicePackagesByServiceId.FirstOrDefault(s => s.ServicePackageId == servicePackageParentId);
                if (servicePackages != null)
                {
                    listServicePackagesChild = listServicePackagesByServiceId
                        .Where(item =>
                            item.ServicePackageParentId == servicePackages.ServicePackageId &&
                            item.ServicePackageParentId != 0).OrderBy(item => item.NumMonthUse).ToList();

                    if (listServicePackagesChild.HasValue())
                    {
                        if (typeChange == 1)
                        {
                            if (servicePackageId > 0)
                            {
                                servicePackages =
                                    listServicePackagesChild.FirstOrDefault(s =>
                                        s.ServicePackageId == servicePackageId);
                            }
                        }
                        else
                        {
                            servicePackages = listServicePackagesChild[0];
                        }
                        if (servicePackages != null)
                        {
                            priceVat = servicePackages.Price * 10 / 100;
                            total = servicePackages.Price + priceVat;
                        }
                    }
                }
            }
            var model = new RegisterServicePackagesModel
            {
                ListServicePackagesParent = listServicePackagesParent,
                ListServicePackages = listServicePackagesChild,
                mServices = Services.Static_Get(serviceId),
                ServiceId = serviceId,
                mServicePackages = servicePackages,
                PriceVAT = priceVat,
                Total = total,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    ControllerName = "Ajax",
                    ActionName = "PartialRegisterServicePackages",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "RegisterServicePackage",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialRegisterServicePackages.cshtml", model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult ServicePackagesByServiceId(short serviceId = 0)
        {
            short[] servicesArray = { Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao };
            if (!Array.Exists(servicesArray, s => s == serviceId))
            {
                serviceId = Constants.ServiceIdTraCuuTieuChuan;
            }

            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            var listServicePackagesParent = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn số người dùng tại một thời điểm"}
            };
            var listServicePackagesChild = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
            };
            listServicePackagesParent.AddRange(listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                .OrderBy(item => item.ConcurrentLogin));

            var model = new LawServices(10)
            {
                ListServicePackagesParent = listServicePackagesParent,
                ListServicePackages = listServicePackagesChild,
                Services = Services.Static_Get(serviceId)
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
        public AjaxResult ServicePackagesByParentId(short serviceId = 0, short servicePackageParentId = 0)
        {
            short[] servicesArray = { Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao };
            if (!Array.Exists(servicesArray, s => s == serviceId))
            {
                serviceId = Constants.ServiceIdTraCuuTieuChuan;
            }
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            ServicePackages servicePackage = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageParentId && m.ServicePackageParentId == 0);
            var listServicePackagesChild = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
            };
            if (servicePackage != null)
            {
                listServicePackagesChild.AddRange(listServicePackagesByServiceId
                    .Where(item =>
                        item.ServicePackageParentId == servicePackageParentId &&
                        item.ServicePackageParentId != 0).OrderBy(item => item.NumMonthUse));
            }

            var model = new LawServices(10)
            {
                ListServicePackages = listServicePackagesChild
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
        public AjaxResult ServicePackagesById(short serviceId = 0,  short servicePackageParentId = 0, short servicePackageId = 0)
        {
            short[] servicesArray = { Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao };
            if (!Array.Exists(servicesArray, s => s == serviceId))
            {
                serviceId = Constants.ServiceIdTraCuuTieuChuan;
            }
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            ServicePackages servicePackage = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageParentId && m.ServicePackageParentId == 0);
            var listServicePackagesChild = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
            };
            if (servicePackage != null)
            {
                listServicePackagesChild.AddRange(listServicePackagesByServiceId
                    .Where(item =>
                        item.ServicePackageParentId == servicePackage.ServicePackageId &&
                        item.ServicePackageParentId != 0).OrderBy(item => item.NumMonthUse));
                if (listServicePackagesChild.Count > 1)
                {
                    servicePackage = listServicePackagesChild.FirstOrDefault(m=>m.ServicePackageId == servicePackageId && m.ServicePackageParentId > 0);
                }
            }

            var model = new LawServices(10, servicePackage)
            {
                ListServicePackages = listServicePackagesChild
            };

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = model,
                Completed = true
            };
        }

        //không dùng
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialRenewalOfService(short serviceId = 15, string serviceName = "", short servicePackageParentId = 0, short servicePackageId = 16)
        {
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId,2);
            ServicePackages servicePackages = null;
            int total = 0, priceVat = 0;
            if (listServicePackagesByServiceId.HasValue())
            {
                servicePackages = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageId);
                if (servicePackages != null)
                {
                    priceVat = servicePackages.Price * 10 / 100;
                    total = servicePackages.Price + priceVat;
                }
            }
            var model = new RegisterServicePackagesModel
            {
                ListServicePackages = listServicePackagesByServiceId.Where(m => m.ServicePackageParentId != 0 && m.ServicePackageParentId == servicePackageParentId).OrderBy(m => m.NumMonthUse).ToList(),
                mServicePackagesParent = listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == servicePackageParentId),
                mServicePackages = servicePackages,
                ServiceId = serviceId,
                ServiceName = serviceName,
                PriceVAT = priceVat,
                Total = total,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    ControllerName = "Ajax",
                    ActionName = "PartialRenewalOfService",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "RenewalOfService",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialRenewalOfService.cshtml", model);
        }

        //không dùng
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialServiceConversion(short serviceId = 15, short serviceIdUse = 0, short servicePackageParentId = 0, short servicePackageId = 0, byte typeChange = 0)
        {
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId,2);
            var listServicePackagesParent = listServicePackagesByServiceId
                .Where(item => item.ServicePackageParentId == 0)
                .OrderBy(item => item.ConcurrentLogin).ToList();
            List<ServicePackages> listServicePackagesChild = null;

            ServicePackages servicePackages = null;
            int total = 0, priceVat = 0;
            if (listServicePackagesParent.HasValue())
            {
                servicePackages = typeChange == 2 ? listServicePackagesParent[0] : listServicePackagesByServiceId.FirstOrDefault(s => s.ServicePackageId == servicePackageParentId);
                if (servicePackages != null)
                {
                    listServicePackagesChild = listServicePackagesByServiceId
                        .Where(item =>
                            item.ServicePackageParentId == servicePackages.ServicePackageId &&
                            item.ServicePackageParentId != 0).OrderBy(item => item.NumMonthUse).ToList();

                    if (listServicePackagesChild.HasValue())
                    {
                        if (typeChange == 1)
                        {
                            if (servicePackageId > 0)
                            {
                                servicePackages =
                                    listServicePackagesChild.FirstOrDefault(s =>
                                        s.ServicePackageId == servicePackageId);
                            }
                        }
                        else
                        {
                            servicePackages = listServicePackagesChild[0];
                        }
                        if (servicePackages != null)
                        {
                            priceVat = servicePackages.Price * 10 / 100;
                            total = servicePackages.Price + priceVat;
                        }
                    }
                }
            }
            var model = new RegisterServicePackagesModel
            {
                ListServicePackagesParent = listServicePackagesParent,
                ListServicePackages = listServicePackagesChild,
                mServices = Services.Static_Get(serviceId),
                ServiceId = serviceId,
                ServiceIdUse = serviceIdUse,
                mServicePackages = servicePackages,
                PriceVAT = priceVat,
                Total = total,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    ControllerName = "Ajax",
                    ActionName = "PartialServiceConversion",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ServiceConversion",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialServiceConversion.cshtml", model);
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialPaymentMethodsScratchCard()
        {
            var model = new PaymentServiceUsingScratchCardModel
            {
                ServiceId = Constants.ServiceIdTraCuuNangCao,
                ServiceName = "Tra cứu nâng cao",
                ServicePackageId = Constants.ServicePackageIdTheCaoLuat,
                ServicePackageName = "Miễn phí 3 tháng sử dụng mã thẻ Luật",
                Total = 0,
                Price = 0,
                TransactionTypeId = 0
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/Account/Profile/PartialPaymentMethodsScratchCard{0}.cshtml", Extensions.IsMobile() ? ".Mobile" : string.Empty), model);
        }

        /// <summary>
        /// Form đăng ký dịch vụ sử dụng thẻ cào
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult PaymentServiceUsingScratchCard(PaymentServiceUsingScratchCardModel model)
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            float amount = 0, percentDecrease = 0;
            if (!model.PaymentServiceUsingScratchCardCaptcha.Equals(model.PaymentServiceUsingScratchCardCode))
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = Resource.TheSecurityCodeIsIncorrect,
                    Completed = false
                };
            }

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
            var messages = new PromotionCodes().PromotionCodes_Check(model.ScratchCardCode,ref amount, ref percentDecrease);
            if (messages.Equals("OK"))
            {
                new CustomerServices().CustomerServices_LVN_DangKySudungMaThe(currentCustomerId, string.Empty, model.ServiceId, model.ServicePackageId, model.ScratchCardCode, Request.UserHostAddress, ref messages);
                Extensions.UpdateUserData();
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = messages,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html"),
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                Completed = false
            };
        }

        //không dùng
        /// <summary>
        /// Văn phòng LuậtVietNam
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialPaymentMethodsOnline(short serviceId = 0, string serviceName = "", short servicePackageId = 0, string servicePackageName = "", byte transactionTypeId = 0, int total = 0, int price = 0)
        {
            var model = new PaymentMethodsOnlineModel
            {
                ServiceId = serviceId,
                ServiceName = serviceName,
                ServicePackageId = servicePackageId,
                ServicePackageName = servicePackageName,
                Total = total,
                Price = price,
                TransactionTypeId = transactionTypeId
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialPaymentMethodsOnline.cshtml", model);
        }

        /// <summary>
        /// Đăng ký dịch vụ sử dụng tại văn phòng luật
        /// </summary>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult PartialPaymentMethodsOnline(PaymentMethodsOnlineModel model)
        {
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId(), messageSendId = 0;
            byte replicated = 0;
            short sysMessageId = 0;

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

            if (model.ServiceId > 0 && model.ServicePackageId > 0)
            {
                //TODO tạo đơn hàng
                var mOrders = new Orders
                {
                    OrderName = string.Format("{0}, gói dịch vụ: {1}",
                        model.ServiceName, model.ServicePackageName),
                    FullName = Extensions.GetCurrentCustomerFullName(),
                    Email = Extensions.GetCurrentCustomerMail(),
                    Address = string.Empty,
                    Mobile = string.Empty,
                    SiteId = Constants.SiteId,
                    OrderValue = model.Total,
                    OrderCode = string.Empty,
                    CouponCode = model.PromotionCodeBankAccount,
                    CouponValue = 0,
                    CrDateTime = DateTime.Now,
                    CompanyName = string.Empty,
                    CompanyAddress = string.Empty,
                    CompanyTaxCode = string.Empty,
                    OrderDesc = string.Empty,
                    CountryId = 1,
                    CrUserId = 0,
                    CustomerId = currentCustomerId,
                    DeliveryFee = 0,
                    DeliveryNote = string.Empty,
                    DeliveryStatusId = 1, //Chờ giao hàng
                    FromIP = Request.UserHostAddress,
                    OrderStatusId = 1, //Chờ thanh toán
                    PartnerId = 0,
                    PaymentTypeId = 5, //Thanh toán trực tiếp
                    RequireInvoice = 0
                };
                mOrders.Insert(replicated, actUserId, ref sysMessageId);
                if (mOrders.OrderId > 0)
                {
                    messageSendId = new MessageSends().MessageSends_AddOrder(currentCustomerId, mOrders.OrderId,
                        model.ServicePackageId);

                    if (messageSendId > 0)
                    {
                        //ToDo Thông báo về trang user/thong-bao-dang-ky-dich-vu.html
                        TempData.Set("ServicePackagesMessages", "Cảm ơn quý khách đã sử dụng dịch vụ. Quý khách vui lòng kiểm tra Email để xem đơn hàng chi tiết.");
                        return new AjaxResult
                        {
                            StatusCode = 200,
                            AllowGet = true,
                            //Message = "Cảm ơn quý khách đã sử dụng dịch vụ. Quý khách vui lòng kiểm tra Email để xem đơn hàng chi tiết.",
                            ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-bao-dang-ky-dich-vu.html?serviceId=", model.ServiceId),
                            Completed = true
                        };
                    }
                }
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialPaymentMethodsBankAccount(short serviceId = 0, string serviceName = "", short servicePackageId = 0, string servicePackageName = "", byte transactionTypeId = 0, int total = 0, int price = 0)
        {
            var model = new PaymentServiceUsingBankAccountModel
            {
                CustomerName = Extensions.GetCurrentCustomerName(),
                ServiceId = serviceId,
                ServiceName = serviceName,
                ServicePackageId = servicePackageId,
                ServicePackageName = servicePackageName,
                Total = total,
                Price = price,
                TransactionTypeId = transactionTypeId
            };
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialPaymentMethodsBankAccount.cshtml", model);
        }

        /// <summary>
        /// Form đăng ký dịch vụ sử dụng tk ngân hàng
        /// </summary>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult PaymentServiceUsingBankAccount(PaymentServiceUsingBankAccountModel model)
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
            //khuyen mai thoi gian dau
            //string PromotionCodeBankAccount = "FEE4A5";
            //model.PromotionCodeBankAccount = PromotionCodeBankAccount;
            double PromotionValue = 0;
            //end khuyen mai
            if (model.ServiceId > 0 && model.ServicePackageId > 0)
            {
                //TODO kiểm tra mã khuyến mại
                if (!string.IsNullOrEmpty(model.PromotionCodeBankAccount))
                {
                    var messages = new PromotionCodes().PromotionCodes_Check(model.PromotionCodeBankAccount, ref amount, ref percentDecrease);
                    PromotionValue = amount > 0 ? amount : (percentDecrease > 0 ? model.Total * percentDecrease / 100 : 0);
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
                    OrderValue = model.Total - PromotionValue,
                    OrderCode = string.Empty,
                    CouponCode = model.PromotionCodeBankAccount,
                    CouponValue = PromotionValue,
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
                    PaymentTypeId = 3,// online qua gan hang
                    RequireInvoice = (byte)(model.TaxInvoice ? 1 : 0) //hóa đơn GTGT
                };
                mOrders.Insert(replicated, actUserId, ref sysMessageId);
                if (mOrders.OrderId > 0)
                {
                    model.OrderId = mOrders.OrderId;
                    if(model.TransactionTypeId == Constants.TransactionTypeIdDangKy) 
                    {
                        transactionDesc = string.Format("Đăng ký Thuê bao {0}", transactionDesc);
                    }else if (model.TransactionTypeId == Constants.TransactionTypeIdGiaHan) 
                    {
                        transactionDesc = string.Format("Gia hạn Thuê bao {0}", transactionDesc);
                    }else if (model.TransactionTypeId == Constants.TransactionTypeIdChuyenDoi)
                    {
                        transactionDesc = string.Format("Chuyển đổi Thuê bao {0}", transactionDesc);
                    }
                    //TODO tạo giao dịch thanh toán
                    var paymentTransactions = new PaymentTransactions
                    {
                        SiteId = Constants.SiteId,
                        CustomerId = currentCustomerId,
                        Amount = Convert.ToInt32(model.Total - PromotionValue), //Convert.ToInt32(model.Price),
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
                    _123Pay m_Pay = new _123Pay();
                    m_Pay.addInfo = ""; 
                    m_Pay.bankCode = model.BankCode;
                    m_Pay.cancelURL = "";
                    m_Pay.checksum = "";
                    m_Pay.clientIP = Request.UserHostAddress;
                    m_Pay.custAddress = StringUtil.RemoveSignature( m_Customers.Address);
                    m_Pay.custDOB = m_Customers.DateOfBirth == DateTime.MinValue ? "" : m_Customers.DateOfBirth.ToString("dd/MM/yyyy");
                    m_Pay.custGender = m_Customers.GenderId == 1 ? "M" : m_Customers.GenderId == 0 ? "U" : "F";
                    m_Pay.custMail = m_Customers.CustomerMail;
                    m_Pay.custName = StringUtil.RemoveSignature(m_Customers.CustomerFullName);
                    m_Pay.custPhone = m_Customers.CustomerMobile;
                    m_Pay.description = "";
                    m_Pay.errorURL = "";
                    m_Pay.mTransactionID = paymentTransactions.PaymentTransactionId.ToString();
                    m_Pay.redirectURL = Constants.PAY_RESULT_URL;

                    m_Pay.totalAmount = (model.Total - PromotionValue).ToString();

                    string[] responseObj = m_Pay.createOrder(currentCustomerId, mOrders.OrderId);
                    if (responseObj.Length > 2)// success
                    {
                        string returnCode = responseObj[0];
                        string Partner123PayTransactionId = responseObj[1];
                        string redirectURL = responseObj[2];
                        model.PayGateUrl = redirectURL;
                    }
                    else
                    {
                        string returnCode = responseObj[0];
                        string returnDesc = responseObj[1];
                        Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
                        m_Partner123PayResponseCodes.PartnerCode = returnCode;
                        m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                        m_AjaxResult.Message = m_Partner123PayResponseCodes.PartnerCodeDesc;
                    }
                }
            }

            return m_AjaxResult;
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
                //khuyen mai thoi gian dau 10%
                string PromotionCodeBankAccount = "FEE4A5";
                model.PromotionCodeBankAccount = PromotionCodeBankAccount;
                double PromotionValue = 0;
                //end khuyen mai
                //TODO kiểm tra mã khuyến mại
                if (!string.IsNullOrEmpty(model.PromotionCodeBankAccount))
                {
                    var messages = new PromotionCodes().PromotionCodes_Check(model.PromotionCodeBankAccount, ref amount, ref percentDecrease);
                    PromotionValue = amount > 0 ? amount : (percentDecrease > 0 ? model.Total * percentDecrease / 100 : 0);
                    if (!messages.Equals("OK"))
                    {
                        model.PromotionCodeBankAccount = string.Empty;
                    }
                    else transactionDesc = string.Format(". Sử dụng mã khuyến mại: {0}", model.PromotionCodeBankAccount);
                }
                //TODO tạo đơn hàng
                if (String.IsNullOrEmpty(model.ServiceName))
                {
                    model.ServiceName = Services.Static_Get(model.ServiceId).ServiceDesc;
                }
                var mOrders = new Orders
                {
                    OrderName = string.Format("{0}, gói dịch vụ: {1} {2}",
                        model.ServiceName, model.ServicePackageName, !string.IsNullOrEmpty(model.PromotionCodeBankAccount) ? string.Concat(", sử dụng mã khuyến mại: ", model.PromotionCodeBankAccount) : string.Empty),
                    FullName = Extensions.GetCurrentCustomerFullName(),
                    Email = Extensions.GetCurrentCustomerMail(),
                    Address = string.Empty,
                    Mobile = string.Empty,
                    SiteId = Constants.SiteId,
                    OrderValue = model.Total - PromotionValue,
                    OrderCode = string.Empty,
                    CouponCode = model.PromotionCodeBankAccount,
                    CouponValue = PromotionValue,
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
                        Amount = Convert.ToInt32(model.Total - PromotionValue), //Convert.ToInt32(model.Price),
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
                    m_Pay.amount = int.Parse((model.Total - PromotionValue).ToString("0"));
                    m_Pay.apptransid = DateTime.Now.ToString("yyMMdd") + paymentTransactions.PaymentTransactionId.ToString();
                    string[] responseObj = m_Pay.createOrder(currentCustomerId, mOrders.OrderId);
                    if (responseObj.Length > 1)// success
                    {
                        string returnCode = responseObj[0];
                        string returnDesc = responseObj[1];
                        if(returnCode == "1")
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
        /// Form xác nhận đăng ký dịch vụ qua tài khoản ngân hàng thành công hay lỗi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult ConfirmPaymentServiceUsingBankAccount(ConfirmPaymentServiceUsingBankAccountModel model)
        {
            int actUserId = 0, currentCustomerId = Extensions.GetCurrentCustomerId();
            byte replicated = 0;
            short sysMessageId = 0;
            float amount = 0, percentDecrease = 0;
            string messages = Resource.PleaseTryAgainLater;

            if (model.OrderId > 0 && model.PaymentTransactionId > 0)
            {
                //Giao dịch thành công
                if (model.ButtonType.Equals("PaymentSuccess"))
                {
                    if (!string.IsNullOrEmpty(model.PromotionCodeBankAccount))
                    {
                        messages = new PromotionCodes().PromotionCodes_Check(model.PromotionCodeBankAccount, ref amount, ref percentDecrease);
                        if (!messages.Equals("OK"))
                        {
                            model.PromotionCodeBankAccount = string.Empty;
                        }
                    }
                    //Đăng ký dịch vụ
                    new CustomerServices().CustomerServices_LVN_DangKyThuebao(currentCustomerId, string.Empty,
                        model.ServiceId, model.ServicePackageId, model.PromotionCodeBankAccount,
                        model.PaymentTransactionId, Request.UserHostAddress, ref messages);

                    if (messages.Equals("Đăng ký gói dịch vụ thành công."))
                    {
                        if (model.TransactionTypeId == Constants.TransactionTypeIdGiaHan)
                        {
                            messages = "Gia hạn gói dịch vụ thành công";
                        }
                        else if (model.TransactionTypeId == Constants.TransactionTypeIdChuyenDoi)
                        {
                            messages = "Chuyển đổi gói dịch vụ thành công";
                        }
                        Extensions.UpdateUserData();
                        return new AjaxResult
                        {
                            StatusCode = 200,
                            AllowGet = true,
                            Message = messages,
                            ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html"),
                            Completed = true
                        };
                    }
                }
                //Giao dịch lỗi
                else if (model.ButtonType.Equals("PaymentError"))
                {
                    messages = string.Empty;
                    var mOrder = new Orders().Get(model.OrderId);
                    if (mOrder.OrderId > 0)
                    {
                        mOrder.OrderStatusId = 3; //Hủy 
                        var sysMessageTypeId = mOrder.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
                        if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
                        {
                            messages = string.Concat(messages, "Hủy đơn hàng ");
                        }
                    }
                    var paymentTransactions = new PaymentTransactions().Get(model.PaymentTransactionId);
                    if (paymentTransactions.PaymentTransactionId > 0)
                    {
                        paymentTransactions.TransactionStatusId = 2; //Lỗi
                        var sysMessageTypeId =  paymentTransactions.InsertOrUpdate(replicated, actUserId, ref sysMessageId);
                        if (sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
                        {
                            messages = string.Concat(messages, " và giao dịch");
                        }
                    }
                    if (!string.IsNullOrEmpty(messages))
                    {
                        messages = string.Concat(messages, " thành công.");
                    }
                }
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html"),
                Completed = true
            };
        }

        [HttpGet]
        [LawsVnAuthorize]
        public AjaxResult KiemTraDvDangKy(short serviceId = 0)
        {
            byte isRegistService = 0;
            short serviceIdProcess = 0;
            string feeType = string.Empty,
                actionButton = string.Empty,
                msg = string.Empty;
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            if (serviceId > 0)
            {
                new CustomerServices(CmsConstants.CMS_CONSTR).CustomerServices_LVN_KiemtraDvDangKy(currentCustomerId, string.Empty,
                    serviceId, ref isRegistService, ref serviceIdProcess, ref feeType, ref actionButton, ref msg);

                //var listServicesId = new List<short>
                //{
                //    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                //};
                ////TODO danh sách dịch vụ hỗ trợ chuyển đổi
                //int index = listServicesId.IndexOf(serviceIdProcess);
                //List<short> listServicesIdUpgradeAllowed = null;
                //if (index > -1)
                //{
                //    listServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s < serviceIdProcess).ToList();
                //}
                var model = new DtCustomerServices
                {
                    Messages = msg,
                    ServiceId = serviceIdProcess,
                    IsRegistService = isRegistService,
                    FeeType = feeType,
                    ActionButton = actionButton
                    //IsUpgradeAllowed = listServicesIdUpgradeAllowed.HasValue() //&& listServicesIdUpgradeAllowed.Exists(m=>m == serviceId)
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

        /// <summary>
        /// Form giả lập xác nhận thanh toán qua tk ngân hàng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LawsVnAuthorize]
        public PartialViewResult PartialConfirmPaymentServiceUsingBankAccount()
        {
            var model = new PaymentServiceUsingBankAccountModel();
            return PartialView("~/Views/AjaxTemplates/Account/Profile/PartialConfirmPaymentServiceUsingBankAccount.cshtml",model);
        }

        #endregion  

        #endregion

        #region Tin tức
        [HttpGet]
        public PartialViewResult GetViewByCateId_Paging(short categoryId = 0, bool isMobile = false, int showNumberOfResults = 10, byte languageId = 1, int page = 1, string updateTargetId = "ArticlesByCateBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetViewByCateId_Paging", int pageSize = 10, int linkLimit = 3)
        {
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            var mArticlesViewCate = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, showNumberOfResults, 0, 0, 0, 1, 0, pageIndex,
                ref rowCount);
            var model = new NewsViewModel
            {
                mArticlesViewCate = mArticlesViewCate,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = isMobile ? pageIndex : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = linkLimit,
                    ShowNumberOfResults = showNumberOfResults,
                    UrlPaging = string.Empty,
                    CategoryId = categoryId,
                    ControllerName = "Ajax",
                    ActionName = "GetViewByCateId_Paging",
                    IsMobile = isMobile,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView( string.Format("~/Views/AjaxTemplates/News/PartialListArticlesView{0}.cshtml", isMobile ? ".Mobile" : string.Empty), model);
        }

        [HttpGet]
        public PartialViewResult GetViewByCateId_PagingV2(short categoryId = 0, bool isMobile = false, int showNumberOfResults = 10, byte languageId = 1, int page = 1, string updateTargetId = "ArticlesByCateBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetViewByCateId_Paging", int pageSize = 10, int linkLimit = 3)
        {
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            var mArticlesViewCate = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, showNumberOfResults, 0, 0, 0, 1, 0, pageIndex,
                ref rowCount);
            var model = new NewsViewModel
            {
                mArticlesViewCate = mArticlesViewCate,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = isMobile ? pageIndex : page,
                    PageSize = showNumberOfResults,
                    LinkLimit = linkLimit,
                    ShowNumberOfResults = showNumberOfResults,
                    UrlPaging = string.Empty,
                    CategoryId = categoryId,
                    ControllerName = "Ajax",
                    ActionName = "GetViewByCateId_Paging",
                    IsMobile = isMobile,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView(string.Format("~/Views/AjaxTemplates/News/PartialListArticlesView{0}V2.cshtml", Extensions.IsMobile() ? "Mobile" : string.Empty), model);
        }

        [HttpGet]
        public PartialViewResult BanTinLuatVN_Search(string keyword = "", short categoryId = 0, int tagId = 0, int showNumberOfResults = 20, byte languageId = 1, int page = 0, string updateTargetId = "ArticlesByCateBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "BanTinLuatVN_Search", int pageSize = 5, int linkLimit = 5)
        {
            byte applicationTypeId = 0, dataTypeId = 1;
            short provinceId = 0, districtId = 0;
            keyword = keyword.StripTags().Sanitize();
            int rowCount = 0, pageIndex = page > 0 ? page -1  :  page;
            var model = new NewsViewModel
            {
                ListArticlesView = ArticlesViewHelpers.View_Search_01(showNumberOfResults, pageIndex,
                    languageId, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                    keyword, provinceId, districtId, 0, 0, 0, "", ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = showNumberOfResults,
                    Keywords = keyword,
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
            if (Extensions.IsMobile())
            {
                return PartialView(string.Format("~/Views/AjaxTemplates/Mobile/PartialListBanTinLuatVNMobile.cshtml"), model);
            }
            else
            {
                return PartialView("~/Views/AjaxTemplates/News/PartialListBanTinLuatVN.cshtml", model);
            }
           
            
        }

        [HttpGet]
        public PartialViewResult BanTinLuatVN_SearchV2(int month = 0, int year = 0, short categoryId = 0, int tagId = 0, byte languageId = 1, int page = 1, int pageSize = 24)
        {
            byte applicationTypeId = 0, dataTypeId = 1;
            short provinceId = 0, districtId = 0;
            DateTime dt = DateTime.Now;
            month = month > 0 ? month : dt.Month;
            year = year > 0 ? year : dt.Year;
            DateTime? datFrom = categoryId == Constants.CateIdBanTinLuat && !Extensions.IsMobile() ? new DateTime(year, month, 1) : (DateTime?)null, dateTo = !Extensions.IsMobile() ? new DateTime(year, month, DateTime.DaysInMonth(year, month)) : (DateTime?)null;
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            var model = new NewsViewModel
            {
                mCategoriesView = CategoriesViewHelpers.View_GetByCategoryId(categoryId),
                ListArticlesView = ArticlesViewHelpers.View_Search_01(pageSize, pageIndex,
                    languageId, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                    "", provinceId, districtId, 0, 0, 0, "PublishTime DESC", ref rowCount, null, dateTo),
                ListCategoriesViewByParentId = CategoriesViewHelpers.View_GetListByParentId(Constants.CateIdBanTinLuat),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = page,
                    PageSize = pageSize,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    Month = month,
                    Year = year,
                    CategoryId = categoryId,
                    ControllerName = "Ajax",
                    ActionName = "BanTinLuatVN_SearchV2",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return PartialView(Extensions.IsMobile() ? "~/Views/AjaxTemplates/Mobile/PartialListBanTinLuatVNMobile.cshtml" : "~/Views/AjaxTemplates/News/PartialListBanTinLuatVN_V2.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult ArticlesView_Search(string keywords = "", bool isMobile = false, short provinceId = 0, int tagId = 0, int showNumberOfResults = 20, byte languageId = 1, int page = 0, string updateTargetId = "ArticlesByCateBox", InsertionMode insertionMode = InsertionMode.Replace, string loadingElementId = "FirstBoxLoading", string controllerName = "Ajax", string actionName = "GetViewByCateId_Paging", int pageSize = 5, int linkLimit = 5)
        {
            byte applicationTypeId = 0, dataTypeId = 1;
            short categoryId = 0, districtId = 0;
            keywords = keywords.StripTags().Sanitize();
            int rowCount = 0, pageIndex = page > 0 ? page -1 : page;
            var model = new NewsViewModel
            {
                ListArticlesView = ArticlesViewHelpers.View_Search_01(showNumberOfResults, pageIndex,
                    languageId, applicationTypeId, categoryId, Constants.SiteId, dataTypeId, tagId,
                    keywords, provinceId, districtId, 0, 0, 0, "", ref rowCount),
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    PageSize = showNumberOfResults,
                    LinkLimit = Constants.RowAmount3,
                    UrlPaging = string.Empty,
                    ShowNumberOfResults = showNumberOfResults,
                    Keywords = keywords,
                    ProvinceId = provinceId,
                    ControllerName = "Ajax",
                    ActionName = "ArticlesView_Search",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ArticlesByCateBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            if (tagId > 0)
            {
                return PartialView(string.Format("~/Views/AjaxTemplates/News/{0}.cshtml", Extensions.IsMobile()==true ? "PartialListArticlesViewByTag.Mobile" : "PartialListArticlesViewByTag"), model);
            }
            return PartialView(string.Format("~/Views/AjaxTemplates/News/{0}.cshtml", Extensions.IsMobile() == true ? "PartialListArticlesViewSearch.Mobile" : "PartialListArticlesViewSearch"), model);
        }
        #endregion

        #region Luật sư
        [HttpGet]
        public PartialViewResult Lawer_GetViewSearch(string Keyword = "", string StartName = "", short FieldId = 0, int ProvinceId = 0, int DistrictId = 0, int WardId = 0, int page = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "TNPL_GetViewSearch", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int RowCount = 0;
            int RowAmount = showNumberOfResults, pageIndex = page > 0 ? page - 1 : page;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId(), LawerGroupId = 0;
            LawersView mLawersView = new LawersView();

            var mLawerViewModel = new LawerViewModel
            {
                mLawersView = mLawersView.GetPageView(0, Keyword, StartName, LawerGroupId, FieldId, ProvinceId, DistrictId, WardId, RowAmount, pageIndex, "", "", "", 0, 0, ref RowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = RowCount,
                    PageIndex = pageIndex,
                    Keywords = Keyword,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = linkLimit,
                    PageSize = RowAmount,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Lawer_GetViewSearch.cshtml", mLawerViewModel);
        }

        [HttpGet]
        public PartialViewResult Lawer_GetView(string lawerKeywords = "", string startName = "", byte languageId = 1, short fieldId = 0, int provinceId = 0, int districtId = 0, int wardId = 0, int page = 0, string updateTargetId = "ListByField", InsertionMode insertionMode = InsertionMode.Replace, string controllerName = "Ajax", string actionName = "TNPL_GetView", int pageSize = 5, int linkLimit = 5, int showNumberOfResults = 20)
        {
            int rowCount = 0, pageIndex = page > 0 ? page - 1 : page;
            byte lawerGroupId = 0;
            lawerKeywords = lawerKeywords.StripTags().Sanitize();
            var mLawerViewModel = new LawerViewModel
            {
                mLawersView = new LawersView().GetPageView(0, lawerKeywords, startName, lawerGroupId, fieldId, provinceId, districtId, wardId, showNumberOfResults, pageIndex, "", "", "", 0, 0, ref rowCount),
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = pageIndex,
                    Keywords = lawerKeywords,
                    ShowNumberOfResults = showNumberOfResults,
                    LinkLimit = linkLimit,
                    PageSize = showNumberOfResults,
                    FieldId = fieldId,
                    ProvinceId = (short)provinceId,
                    DistrictId = districtId,
                    WardId = wardId,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = updateTargetId,
                        InsertionMode = insertionMode
                    }
                }
            };
            return PartialView("~/Views/AjaxTemplates/Lawer_GetView.cshtml", mLawerViewModel);
        }
        #endregion

        [HttpGet]
        public int DeleteCustomerProvinces(int CustomerProvinceId)
        {
            int value = 0;
            try
            {
                byte Replicated = 0;
                int ActUserId = 0;
                CustomerProvinces mCustomerProvinces = new CustomerProvinces();
                mCustomerProvinces.CustomerProvinceId = CustomerProvinceId;
                mCustomerProvinces.Delete(Replicated, ActUserId, ref value);
                value = 2;
            }
            catch (Exception ex) { sms.utils.LogFiles.WriteLog(ex.ToString()); }
            return value;
        }
        public int AddCustomerProvinces(int CustomerId, short ProvinceId)
        {
            int value = 0;
            try
            {
                byte Replicated = 0;
                int ActUserId = 0;
                CustomerProvinces mCustomerProvinces = new CustomerProvinces();
                mCustomerProvinces.CustomerId = CustomerId;
                mCustomerProvinces.ProvinceId = ProvinceId;
                mCustomerProvinces.InsertOrUpdate(Replicated, ActUserId, ref value);
                value = mCustomerProvinces.CustomerProvinceId;
            }
            catch (Exception ex) { sms.utils.LogFiles.WriteLog(ex.ToString()); }
            return value;
        }

        [HttpGet]
        public PartialViewResult GetDistrictsByProvinceId(short provinceId = 0)
        {
            var model = Districts.Static_GetList(provinceId);
            return PartialView("~/Views/AjaxTemplates/PartialGetDistrictsByProvinceId.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult GetWardsByDistrictId(short districtId = 0)
        {
            var model = Wards.Static_GetList(districtId);
            return PartialView("~/Views/AjaxTemplates/PartialGetWardsByDistrictId.cshtml", model);
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
            var articleViewLogs = new ArticleViewLogs
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
            articleViewLogs.Insert(replicated, actUserId, ref sysMessageId, ref articleViewLogByDayId);
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = articleViewLogs.ArticleViewLogId > 0
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

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult SendQuestions(SendQuestionsModel model)
        {
            short sysMessageId = 0;
            int actUserId = 0;
            //if (!model.SendQuestionsCaptcha.Equals(model.SendQuestionsCaptchaCode))
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

            var mFeedBacks = new FeedBacks
            {
                FullName = model.FullName,
                PhoneNumber = model.Mobile,
                Email = model.Email,
                Comment = model.Questions,
                SiteId = Constants.SiteId,
                Title = string.Concat("Khách hàng ", model.FullName, " gửi câu hỏi"),
                FeedBackGroupId = Constants.FeedBackGroupIdSendQuestion,
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
                    Message = "Cảm ơn quý khách đã gửi câu hỏi.",
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH,"huong-dan/tai-khoan.html"),
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
        [ValidateAntiForgeryToken]
        public AjaxResult GopY(GopYModel model)
        {
            short sysMessageId = 0;
            int actUserId = 0;
            //if (!model.GopYCaptcha.Equals(model.GopYCaptchaCode))
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

            var mFeedBacks = new FeedBacks
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                PhoneNumber = Extensions.GetCurrentCustomerMobile(),
                Email = Extensions.GetCurrentCustomerMail(),
                Comment = model.Content,
                SiteId = Constants.SiteId,
                Title = model.Title,
                FeedBackGroupId = Constants.FeedBackGroupIdGopY,
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
                    Message = "Cảm ơn quý khách đã gửi góp ý.",
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
                Title = "LuatVietNam gửi link văn bản",
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
                    Message = "Gửi link văn bản thành công.",
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
        [OutputCache(CacheProfile = "DocProperties3Minutes")]
        public PartialViewResult GetDocProperties(int docId, byte languageId)
        {
            if (languageId == 0)
            {
                languageId = LawsVnLanguages.AvailableLanguages[0].LanguageId;
            }
            DataSet ds = new Docs().Docs_GetProperties(0, languageId, docId);
            DataTable dt = ds.Tables[0];
            DocPropertiesModel model = null;
            if (dt.DataTableExists())
            {
                model = new DocPropertiesModel
                {
                    DocGroupId = Extensions.GetColumnValue<byte>(dt.Rows[0],
                        "DocGroupId"),
                    DocName = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "DocName"),
                    OrganName = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "OrganName"),
                    FieldName = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "FieldName"),
                    IssueDate = Extensions.GetColumnValue<DateTime>(dt.Rows[0],
                        "IssueDate"),
                    Fee = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "Fee"),
                    EffectDate = Extensions.GetColumnValue<DateTime>(dt.Rows[0],
                        "EffectDate"),
                    ExpireDate = Extensions.GetColumnValue<DateTime>(dt.Rows[0],
                        "ExpireDate"),
                    DocTypeName = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "DocTypeName"),
                    DocIdentity = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "DocIdentity"),
                    SignerName = Extensions.GetColumnValue<string>(dt.Rows[0],
                        "SignerName"),
                    LanguageId = languageId
                };
            }
            return PartialView("~/Views/AjaxTemplates/DocPropertiesTooltip.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult PartialTermsConditionsView()
        {
            var model = ArticlesViewHelpers.View_GetArticleDetail(Constants.QuyUocBaoMatArticleId, 0, 0, 0);
            return PartialView("~/Views/AjaxTemplates/PartialTermsConditionsView.cshtml", model);
        }

        [HttpGet]  
        [AllowAnonymous]
        public JsonResult IsCustomerNameExist(string customerName)
        {
            var retVal = new Customers { CustomerName = customerName }.Customers_GetByName();
            bool isExist = retVal.CustomerId > 0;
            if (!isExist)
                return Json(true, JsonRequestBehavior.AllowGet);

            string messages = String.Format(CultureInfo.InvariantCulture,
                "Tài khoản: {0} đã được sử dụng.", retVal.CustomerName);

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

            string messages = String.Format(CultureInfo.InvariantCulture,
                "Email: {0} đã được sử dụng.", retVal.CustomerMail);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }  

    }
}
