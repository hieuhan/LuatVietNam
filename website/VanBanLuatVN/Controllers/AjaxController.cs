using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using VanBanLuatVN.AppCode.Attributes;
using VanBanLuatVN.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuat.Controllers
{
    public class AjaxController : Controller
    {
        private readonly int _customerId = Extensions.GetCurrentCustomerId();

        //
        // GET: /Ajax/

        /// <summary>
        /// Danh sách văn bản
        /// </summary>
        /// <param name="searchParams">DocFilterParams</param>
        /// <param name="url">link xem thêm</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult DocsGetPage(DocFilterParams searchParams, string url = "")
        {
            searchParams.FieldSelect = "DocId,DocName,DocUrl,EffectDate,IssueDate";
            searchParams.GetEffectStatusName = 1;
            var model = new DocViewModel.DocsGetPageModel
            {
                Url = url,
                DocList = DocHelpers.GetPage(searchParams)
            };
            return PartialView("~/Views/AjaxTemplates/DocsGetPage.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult DocsRelateGetPage(PaginationModel model)
        {
            model.DocRelateList = DocHelpers.GetDocRelates(model.DocId, model.RelateTypeId, model.DisplayPosition, model.PageSize,
                model.Page > 0 ? model.Page - 1 : model.Page, 0, Constants.LanguageId, 1);
            return PartialView("~/Views/AjaxTemplates/DocsRelateGetPage.cshtml", model);
        }

        /// <summary>
        /// Danh sách văn bản mới
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListDocsNewest(int page = 0)
        {
            return ListDocsGetPage(page, 0,0,string.Empty, "/Ajax/ListDocsNewest");
        }

        /// <summary>
        /// Danh sách công văn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListDocCongVan(int page = 0)
        {
            return ListDocsGetPage(page, Constants.DocGroupIdCongVan, 0 ,string.Empty, "/Ajax/ListDocCongVan");
        }

        /// <summary>
        /// Danh sách vb UBND
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListDocUbnd(int page = 0)
        {
            return ListDocsGetPage(page, Constants.DocGroupIdUbnd, 0, string.Empty, "/Ajax/ListDocUbnd");
        }

        /// <summary>
        /// Danh sách vb hợp nhất
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListDocVbhn(int page = 0)
        {
            return ListDocsGetPage(page, Constants.DocGroupIdVbhn, 0, string.Empty, "/Ajax/ListDocVbhn");
        }

        /// <summary>
        /// Danh sách vb TCVN
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListDocTcvn(int page = 0)
        {
            return ListDocsGetPage(page, Constants.DocGroupIdTcvn, 0, string.Empty, "/Ajax/ListDocTcvn");
        }

        [HttpGet]
        public ActionResult ListDocSapCoHieuLuc(int page = 0)
        {
            return ListDocsGetPage(page, 0, 0, "SapCoHieuLuc", "/Ajax/ListDocSapCoHieuLuc");
        }

        [HttpGet]
        public ActionResult ListDocHetHieuLuc(int page = 0)
        {
            return ListDocsGetPage(page, 0, 0, "HetHieuLuc", "/Ajax/ListDocHetHieuLuc");
        }

        [HttpGet]
        public ActionResult ListDocSapHetHieuLuc(int page = 0)
        {
            return ListDocsGetPage(page, 0, 0, "SapHetHieuLuc", "/Ajax/ListDocSapHetHieuLuc");
        }

        [HttpGet]
        public ActionResult ListDocSapSuaDoi(int page = 0)
        {
            return ListDocsGetPage(page, 0, 0, "SapSuaDoi", "/Ajax/ListDocSapSuaDoi");
        }

        #region ListDocByField

        [HttpGet]
        public ActionResult ListDocsByField(int page = 0, short fieldId = 0, byte docGroupId = 0)
        {
            return ListDocsGetPage(page, docGroupId, fieldId, string.Empty, "/Ajax/ListDocsByField");
        }
        #endregion

        [HttpGet]
        public ActionResult ListDocsGetPage(int page = 0, byte docGroupId = 0, short fieldId = 0, string effectStatusType = "", string ajaxUrl = "/Ajax/ListDocsGetPage", NameValueCollection nameValueCollection = null)
        {
            var docFilterParams = new DocFilterParams
            {
                PageIndex = page > 0 ? page - 1 : page,
                DocGroupId = docGroupId,
                FieldId = fieldId,
                EffectStatusType = effectStatusType,
                FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                GetRowCount = 1,
                GetEffectStatusName = 1,
                OrderBy = "IssueDate DESC",
                RowAmount = Constants.RowAmount20
            };
            PaginationModel model = new PaginationModel
            {
                Page = page,
                FieldId = fieldId,
                DocGroupId = docGroupId,
                EffectStatusType = effectStatusType,
                DocList = DocHelpers.GetPage(docFilterParams),
                AjaxUrl = ajaxUrl,
                NameValueCollection = nameValueCollection ?? new NameValueCollection
                {
                    {"page",(page+1).ToString() }
                }
            };
            return PartialView("~/Views/AjaxTemplates/ListDocsGetPage.cshtml", model);
        }

        [HttpGet]
        [VbLuatAuthorize]
        public ActionResult CustomerDocsGetPage(int page = 1, byte docGroupId = 0, short fieldId = 0, byte effectStatusId = 0, short organId = 0, byte docTypeId = 0)
        {
            var docFilterParams = new DocFilterParams
            {
                FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                CustomerId = Extensions.GetCurrentCustomerId(),
                RegistTypeId = Constants.RegistTypeIdVbqt,
                DocGroupId = docGroupId,
                FieldId = fieldId,
                EffectStatusId = effectStatusId,
                OrganId = organId,
                DocTypeId = docTypeId,
                LanguageId = Constants.LanguageId,
                RowAmount = Constants.RowAmount20,
                PageIndex = page > 0 ? page - 1 : page,
                GetRowCount = 1
            };
            var model = new PaginationModel
            {
                Page = page,
                DocGroupId = docGroupId,
                FieldId = fieldId,
                EffectStatusId = effectStatusId,
                OrganId = organId,
                DocTypeId = docTypeId,
                DocList = DocHelpers.GetPage(docFilterParams)
            };
            return PartialView("~/Views/AjaxTemplates/CustomerDocsGetPage.cshtml", model);
        }

        #region Account

        #region CustomerDocs

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult SaveMyDoc(int docId=0)
        {
            DocHelpers.SaveMyDoc(_customerId, docId, Constants.LanguageId);
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true,
                Message = "Lưu văn bản quan tâm thành công."
            };
        }

        #endregion

        #region NewsLetterEmail
        [HttpPost]
        public AjaxResult NewsLetterEmail(SharedViewModel.NewsletterEmailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Completed = false,
                    Message = "Quý khách vui lòng thử lại sau."
                };
            }

            short sysMessageId = 0;
            byte replicated = 0;
            int actUserId = 0;
            new NewsletterEmails
            {
                SiteId = Constants.SiteId,
                Email = model.NewsletterEmail,
                IsReceiveNews = 1,
                CustomerId = Extensions.GetCurrentCustomerId()
            }.InsertOrUpdate(replicated, actUserId, ref sysMessageId);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true,
                Message = "Đăng ký nhận bản tin thành công."
            };
        }
        #endregion

        #region DocDetail
        /// <summary>
        /// Gửi link văn bản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VbLuatAuthorize]
        public PartialViewResult DocSendMail()
        {
            return PartialView("~/Views/AjaxTemplates/Account/DocSendMail.cshtml");
        }

        /// <summary>
        /// Gửi góp ý cho văn bản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [VbLuatAuthorize]
        public PartialViewResult DocFeedback()
        {
            return PartialView("~/Views/AjaxTemplates/Account/DocFeedback.cshtml");
        }

        [HttpGet]
        [VbLuatAuthorize]
        public PartialViewResult GuiYeuCauVB()
        {
            return PartialView("~/Views/AjaxTemplates/Account/PartialGuiYeuCauVB.cshtml");
        }
        #endregion

        [HttpGet]
        [VbLuatAuthorize]
        public AjaxResult DeleteDocument(byte docGroupId, int docId, byte type = 1)
        {
            string Message = "";
            if (docId > 0)
            {
                try
                {
                    DocHelpers.DeleteMyDoc(Extensions.GetCurrentCustomerId(), docId, Constants.LanguageId);
                    Message = "Xóa văn bản quan tâm thành công.";
                }
                catch (Exception ex)
                {
                    Message = "Đã xảy ra lỗi. Xin quý khách vui lòng thử lại.";
                }
            }
            else
            {
                Message = "Đã xảy ra lỗi. Xin quý khách vui lòng thử lại.";
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true,
                Message = Message
            };
        }

        [HttpGet]
        [VbLuatAuthorize]
        public PartialViewResult MyDocumentFilter(short FieldId = 0, byte EffectStatusId = 0, short OrganId = 0, byte DocTypesId = 0)
        {
            int customerID= Extensions.GetCurrentCustomerId();
            DocByFieldViewModel model = new DocByFieldViewModel();
            if (customerID > 0)
            {
                DocFilterParams mDocFilterParams = new DocFilterParams
                {
                    FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                    CustomerId = customerID,
                    RegistTypeId = Constants.RegistTypeIdVbqt,
                    FieldId = FieldId,
                    EffectStatusId = EffectStatusId,
                    OrganId = OrganId,
                    DocTypeId = DocTypesId,
                    DocGroupId = 1,
                    LanguageId = Constants.LanguageId,
                    RowAmount = Constants.RowAmount20,
                    PageIndex = 0,
                    GetRowCount = 1,
                    GetEffectStatusName = 1,
                    GetCountByEffectStatus = 1
                };
                model.DocList = DocHelpers.GetPage(mDocFilterParams);
            }
            return PartialView("~/Views/AjaxTemplates/Account/MyDocumentGetPage.cshtml", model);
        }
        #endregion

        #region DocSearch

        [HttpPost]
        public AjaxResult AutocompleteSignerByName(string signerName)
        {
            var model = Signers.Static_GetList(signerName.Sanitize().StripTags(), Constants.RowAmount20, Constants.LanguageId);
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Completed = true,
                Data = model
            };
        }

        [HttpGet]
        public PartialViewResult DocSearchGetPage(PaginationModel model)
        {
            var docFilterParams = new DocFilterParams
            {
                PageIndex = model.Page > 0 ? model.Page - 1 : model.Page,
                IsSearchExact = model.IsSearchExact,
                DocGroupId = model.DocGroupId,
                DocTypeId = model.DocTypeId,
                EffectStatusId = model.EffectStatusId,
                FieldId = model.FieldId,
                OrganId = model.OrganId,
                SignerId = model.SignerId,
                SearchByDate = model.SearchByDate,
                FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                GetRowCount = 1,
                GetEffectStatusName = 1,
                RowAmount = model.PageSize = Constants.RowAmount20
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
            model.TotalPage = model.DocList.RowCount;
            // TODO logs tìm kiếm
            if (!string.IsNullOrEmpty(model.Keywords))
            {
                DocHelpers.InsertSearchLog(model.Keywords, docFilterParams.DateFrom, docFilterParams.DateTo,
                    docFilterParams.DocTypeId, docFilterParams.OrganId, docFilterParams.SignerId,
                    docFilterParams.FieldId, Constants.LanguageId, Extensions.GetCurrentCustomerId());
            }
            return PartialView("~/Views/AjaxTemplates/DocSearchGetPage.cshtml", model);
        }
        #endregion

        #region Article
        public ActionResult ListArticlesGetPage(short categoryId=0, int page = 0)
        {
            return ArticlesGetPage(page, categoryId, "/Ajax/ListArticlesGetPage");
        }
        [HttpGet]
        public ActionResult ArticlesGetPage(int page = 1, short categoryId = 0, string ajaxUrl = "/Ajax/ListArticlesGetPage", NameValueCollection nameValueCollection = null)
        {
            string FieldArticleSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime", FieldCategorySelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl";
            int PageIndex = page > 0 ? page - 1 : page;
            PaginationModel model = new PaginationModel
            {
                Page = page,
                CategoryArticles = ArticleHelpers.GetByCategoryId(categoryId, Constants.RowAmount5, PageIndex, 1, 1, 0, "DisplayOrder Desc", FieldArticleSelect, FieldCategorySelect),
                AjaxUrl = ajaxUrl,
                CategoryId=categoryId
            };
            return PartialView("~/Views/AjaxTemplates/ArticlesGetPage.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult ArticleTagGetPage(int tagid, int page)
        {
            int page1 = page > 0 ? page - 1 : page;
            NewViewModel mNewViewModel = new NewViewModel
            {
                mTagArticles = ArticleHelpers.GetByTagId(Constants.SiteId, tagid, Constants.RowAmount5, page1, 1,
                    "ArticleId,Title,ImagePath,ArticleUrl,Summary,PublishTime",
                    "ArticleId,Title,ImagePath,ArticleUrl,Summary,PublishTime"),
                Page = page
            };
            return PartialView("~/Views/AjaxTemplates/ArticlesTagGetPage .cshtml", mNewViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult ArticleSendLinkForm()
        {
            return PartialView("~/Views/AjaxTemplates/Account/ArticleSendLink.cshtml");
        }

        [HttpGet]
        [VbLuatAuthorize]
        public PartialViewResult ArticleFeedBackForm()
        {
            return PartialView("~/Views/AjaxTemplates/Account/ArticleFeedback.cshtml");
        }

        [HttpPost]
        public AjaxResult ArticleSendLink(ArticlesModel.ArticleSendLinkModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            var messageTemplate = new MessageTemplates().Get(Constants.TemplateArticleSendLink);
            if (messageTemplate.MessageTemplateId > 0)
            {
                var messageSends = new MessageSends
                {
                    SiteId = Constants.SiteId,
                    MessageTemplateId = messageTemplate.MessageTemplateId,
                    SendFrom = messageTemplate.SendFrom,
                    SendTo = model.SendMail,
                    Title = "Vanbanluat.vn gửi link bài viết",
                    MsgContent = messageTemplate.MsgContent.Replace("#Email#", model.SendMail).Replace("#ArticleUrl#", string.Concat(Constants.WEBSITE_DOMAIN, model.ArticleUrl)),
                    SendMethodId = 1, //email
                    SendStatusId = 1, //chờ gửi
                    SendTime = DateTime.Now
                };
                messageSends.Insert();
                if (messageSends.MessageSendId > 0)
                {
                    return new AjaxResult
                    {
                        StatusCode = 200,
                        AllowGet = true,
                        Message = "Gửi link bài viết thành công.",
                        Completed = true
                    };
                }
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Quý khách vui lòng thử lại sau.",
                Completed = false
            };
        }
        #endregion

        #region GuiYeuCauVB 
        
        [HttpPost]
        [VbLuatAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult GuiYeuCauVB(AccountModel.GuiYeuCauVB model)
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
                FeedBackGroupId = Constants.FeedBackGroupIdGuiYeuCauVB,
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
                Message = "Gửi nội dung yêu cầu bị lỗi, bạn vui lòng quay lại sau.",
                Completed = false
            };
        }

        #endregion 

        #region Log
        [HttpGet]
        public AjaxResult ArticleLogs(int articleId = 0, short categoryId = 0)
        {
            if (articleId <= 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Vui lòng thử lại sau",
                    Completed = false
                };
            }
               string UserAgent = Request.UserAgent;
               string FromIP = Request.UserHostAddress;
               string RefererFrom = HttpContext.Request.UrlReferrer != null? HttpContext.Request.UrlReferrer.ToString(): "N/A";
               int CustomerId = Extensions.GetCurrentCustomerId();
            ArticleHelpers.InsertViewLog(articleId, VanBanLuat.Librarys.Constants.SiteId, 0, categoryId, 0, 1, CustomerId, RefererFrom, UserAgent, FromIP);
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true
                };
        }
        #endregion

        #region Biểu mẫu
        [HttpGet]
        public PartialViewResult BieuMauGetPage(short caterogyId = 0)
        {
            string ArticleFieldSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime";
            NewViewModel mNewViewModel = new NewViewModel();
            ArticleFilterParams mArticleFilterParams = new ArticleFilterParams
            {
                CategoryId = caterogyId,
                ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                ArticleFieldSelect = ArticleFieldSelect,
                RowAmount = Constants.RowAmount20,
                PageIndex = 0,
                OrderBy = "DisplayOrder DESC"
            };
            mNewViewModel.mCategoryArticles = ArticleHelpers.GetPage(mArticleFilterParams);
            return PartialView("~/Views/AjaxTemplates/BieuMauGetPage.cshtml", mNewViewModel);
        }

        [HttpPost]
        public PartialViewResult BieuMauGetByCategoryId(short categoryId = 0)
        {
            CategoryArticles model = ArticleHelpers.GetPage(new ArticleFilterParams
            {
                SiteId = Constants.SiteId,
                CategoryId = categoryId > 0 ? categoryId : Constants.CateId_BieuMauCanBiet,
                ArticleTypeId = Constants.ArticleTypeIdBieuMau,
                ArticleFieldSelect = "ArticleId, Title, Summary, ImagePath, ArticleUrl, PublishTime",
                RowAmount = Constants.RowAmount20,
                PageIndex = 0,
                OrderBy = "DisplayOrder DESC"
            });
            return PartialView("~/Views/AjaxTemplates/BieuMauGetByCategoryId.cshtml", model);
        }
        #endregion
    }
}
