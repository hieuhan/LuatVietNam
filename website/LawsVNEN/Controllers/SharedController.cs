using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.Library;
using LawsVNEN.Models;
using LawsVNEN.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LawsVN.Library;

namespace LawsVNEN.Controllers
{
    public class SharedController : Controller
    {
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
        private readonly byte _siteId = Extensions.GetSiteId();

        [ChildActionOnly]
        public ActionResult _PartialHeader(string returnUrl = "")
        {
            var model = new HeaderModel
            {
                ListMenuItemsView =
                    DropDownListHelpers.GetMenuItemsByMenuId(Extensions.GetMenuIdTopId(_currentLanguageId)),
                ReturnUrl = returnUrl
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _PartialFooter()
        {
            List<MenuItemsView> listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Extensions.GetMenuIdBottomId(_currentLanguageId));
            return PartialView(listMenuItemsView);
        }

        [ChildActionOnly]
        public ActionResult PartialSEOPagination(int page = 0, int totalPage = 0, int pageSize = 20)
        {
            var model = new PartialPagination
            {
                PageIndex = page,
                PageSize = pageSize,
                TotalPage = totalPage
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _PartialContentLeft()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _PartialContentRight()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _PartialWidgetUser(string returnUrl = "")
        {
            int countMessages = 0, countCustomerDocs = 0, countThongBaoHieuLuc = 0, countPaymentTransactionSuccess = 0 , customerId = Extensions.GetCurrentCustomerId();
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            DateTime endTime = new DateTime();
            CustomerHelpers.WidgetUser_GetViewCountEN(_currentCustomerId, languageId, 0, 1, 0, 0, 1, ref countMessages, ref countCustomerDocs, ref countThongBaoHieuLuc, ref countPaymentTransactionSuccess, ref endTime);
            var model = new WidgetUserModel
            {
                CountMessages = countMessages,
                CountCustomerDocs = countCustomerDocs,
                CountThongBaoHieuLuc = countThongBaoHieuLuc,
                EndTime = endTime.toString(),
                ReturnUrl = returnUrl
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _PartialWidgetUserHeader()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _PartialNewsLetterEmail()
        {
            return PartialView();
        }
        
        [ChildActionOnly]
        public ActionResult _PartialMenuLeft( byte docGroupId = 1, bool isHome = false)
        {
            short displayTypeId = 68; 
            List<FieldDisplays> listFieldDisplays = DropDownListHelpers.GetAllFieldDisplays(displayTypeId);
            List<Fields> listFields = DropDownListHelpers.GetAllFieldsByLanguage();
            List<Fields> listFieldsOther = listFields.Where(f => listFieldDisplays.All(d => f.FieldId != d.FieldId)).ToList();
            List<MenuItemsView> listMenuItemsLeftBottom = DropDownListHelpers.GetMenuItemsByMenuId(Extensions.GetMenuIdLeftBottomId(_currentLanguageId));
            MenuLeftModel model = new MenuLeftModel
            {
                DocGroupId = docGroupId,
                LMenuItemsLeftTop = DropDownListHelpers.GetMenuItemsByMenuId(Extensions.GetMenuIdLeftTopId(_currentLanguageId)),
                ListFieldDisplays = listFieldDisplays,
                ListFieldsOther = listFieldsOther,
                LMenuItemsLeftBottom = listMenuItemsLeftBottom,
                LMenuItemsLeftBottomParent = listMenuItemsLeftBottom.Where(x => x.ParentItemId == 0).ToList(),
                IsHome = isHome
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _PartialNewsLetterLuatVN()
        {
            //int rowCount = 0;
            short categoryId = Constants.CateIdBanTinLuatVN;
            //short categoryId = Extensions.GetCateIdBanTinLuatVnId(LanguageId);
            CategoriesView model = CategoriesViewHelpers.View_GetByCategoryId(categoryId);
            if (_currentLanguageId == 1)
            {
                model.CategoryName = "Bản tin Tiếng Anh";
            }
            //model.lArticlesView = ArticlesViewHelpers.View_SearchWithContent(Constants.RowAmount4, 0, 1, 0, categoryId, _siteId, 0, "", ref rowCount);
            model.lArticlesView = ArticlesViewHelpers.View_GetArticleByCategoryId(categoryId, 0, Constants.RowAmount4, "DisplayOrder DESC");
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFullSearchHome(PartialDocSearchModel model)
        {
            ModelState.Clear();
            model.SearchOptions = 1;
            model.DisplayTypeId = 62;
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _PartialFullSearch(PartialDocSearchModel model)
        {
            ModelState.Clear();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFullSearchDetail(PartialFullSearchDetailModel model)
        {
            ModelState.Clear();
            return PartialView(model);
        }

         [ChildActionOnly]
         public ActionResult _PartialLinkDocumentAttribute(PartialLinkDocumentAttributeModel model)
         {
             return PartialView(model);
         }

         [ChildActionOnly]
         public ActionResult _PartialLinkDocumentAttributeRight(PartialLinkDocumentAttributeModel model)
         {
             return PartialView(model);
         }


         [ChildActionOnly]
         public ActionResult PartialPaginationAjaxPageNumber(PartialPaginationAjax model)
         {
             return PartialView(model);
         }

         [ChildActionOnly]
         [LawsVnAuthorize(Roles = "NVTA,NVTC,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "NotPermissionDocContent")] 
         public ActionResult PartialPermissionDocContent(DocsViewDetailModel docModel)
         {
             if (docModel == null) docModel = new DocsViewDetailModel();
             return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocContent.cshtml", docModel);
         }

        [ChildActionOnly]
        [LawsVnAuthorize(Roles = "NVTA,NVTC,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "NotPermissionDocRelate" )] 
         public ActionResult PartialPermissionDocRelate(DocsViewDetailModel docModel)
         {
            if (docModel != null)
            {
                int rowCount = 0;
                var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                    docModel.mDocsViewDetail.mDocsView.DocId, docModel.RelateTypeId, string.Empty,
                    Constants.RowAmount20, 0, 1,
                    ref rowCount);

                docModel.mDocsViewDetail.lDocRelates = (from r in mDocsViewDetail.lDocRelates
                                                        join e in docModel.ListEffectStatus on r.EffectStatusId equals e.EffectStatusId
                                                        select new DocRelates
                                                        {
                                                            DocName = r.DocName,
                                                            DocId = r.DocId,
                                                            DocUrl = r.DocUrl,
                                                            EffectDate = r.EffectDate,
                                                            IssueDate = r.IssueDate,
                                                            CrDateTime = r.CrDateTime,
                                                            EffectStatusName = _currentLanguageId == 1 ? e.EffectStatusDesc : e.EffectStatusName,
                                                            RelateTypeName = r.RelateTypeName
                                                        }).ToList();

                docModel.mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    LanguageId = docModel.LanguageId,
                    FieldId = docModel.FieldId,
                    ControllerName = "Ajax",
                    ActionName = "Docs_GetRelate",
                    EffectStatusName = "",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        HttpMethod = "GET",
                        UpdateTargetId = "ListDocRelate",
                        InsertionMode = InsertionMode.Replace
                    },
                    DocId = docModel.mDocsViewDetail.mDocsView.DocId
                };
            }
            else docModel = new DocsViewDetailModel();
            return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocsRelate.cshtml", docModel);
         }

        [ChildActionOnly]
        [LawsVnAuthorize(Roles = "NVTA,NVTC,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "NotPermissionDocSummary")] 
        public ActionResult PartialPermissionDocSummary(DocsViewDetailModel docModel)
        {
            if (docModel == null) docModel = new DocsViewDetailModel();
            return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocSummary.cshtml", docModel);
        }

        [ChildActionOnly]
        [LawsVnAuthorize(Roles = "NVTA,NVNC,TA,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "NotPermissionDocDownload", MessageTypes = ViewModelBase.MessageTypes.NotAuthDownload)] 
        public ActionResult PartialPermissionDocDownload(DocsViewDetailModel docModel)
        {
            if (docModel == null) docModel = new DocsViewDetailModel();
            return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocDownload.cshtml", docModel);
        }

        public ActionResult PartialBankPayment(BankPaymentModel model)
        {
            return PartialView(model);
        }

    }
}