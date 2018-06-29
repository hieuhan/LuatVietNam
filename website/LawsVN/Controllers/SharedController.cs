using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;
using ICSoft.CMSLibEstate;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using LawsVN.Library.Sercurity;
using LawsVN.Models;
using LawsVN.Models.Account;
using LawsVN.Models.Docs;
using LawsVN.Models.Shared;
using System.Data;

namespace LawsVN.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();

        [ChildActionOnly]
        public ActionResult PartialCustomerNotifyMessages()
        {
            int rowCountNotifyMessages = 0;
            var mHeaderModel = new HeaderModel
            {
                ListMessages = new ICSoft.CMSLibEstate.Messages(Constants.EXT_CONSTR) { UserId = _currentCustomerId, HasRead = 0, MessageTypeId = Constants.MessageTypeIdInbox }.GetPage(Constants.RowAmount20, 0, "", "", "", ref rowCountNotifyMessages),
                RowCountNotifyMessages = rowCountNotifyMessages
            };
            return PartialView(mHeaderModel);
        }

        [ChildActionOnly]
        public ActionResult PartialWidgetUserHeader(byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0)
        {
           
            int countMessages = 0, countCustomerDocs = 0, countThongBaoHieuLuc = 0, countPaymentTransactionSuccess = 0;
            string customerName = Extensions.GetCurrentCustomerName();
            byte isRegistService = 0;
            short serviceId = 0, serviceIdProcess = 0; string feeType = string.Empty, actionButton = string.Empty, msg =
                string.Empty;
            DateTime endTime = new DateTime();
            new CustomerServices().CustomerServices_LVN_KiemtraDvDangKy(_currentCustomerId, string.Empty,
                serviceId, ref isRegistService, ref serviceIdProcess, ref feeType, ref actionButton, ref msg);
            CustomerHelpers.WidgetUser_GetViewCount(_currentCustomerId, _currentLanguageId, 1, 1, getCountThongBaoHieuLuc, getCountPaymentTransactionSuccess, 0, ref countMessages, ref countCustomerDocs, ref countThongBaoHieuLuc, ref countPaymentTransactionSuccess, ref endTime);
            var model = new WidgetUserModel
            {
                CountMessages = countMessages,
                CountCustomerDocs = countCustomerDocs,
                CountThongBaoHieuLuc = countThongBaoHieuLuc,
                CountPaymentTransactionSuccess = countPaymentTransactionSuccess,
                IsUserVip = serviceIdProcess == Constants.ServiceIdTraCuuNangCao,
                CustomerNameSubstring =  customerName.ExtSubstring(0,serviceIdProcess == Constants.ServiceIdTraCuuNangCao ? 11 : 15),
                IsRegistService = isRegistService
            };
            return PartialView(model);
        }
        
        [ChildActionOnly]
        public ActionResult PartialWidgetUserHeaderMobile(byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0)
        {
            int countMessages = 0, countCustomerDocs = 0, countThongBaoHieuLuc = 0, countPaymentTransactionSuccess = 0;
            byte isRegistService = 0;
            short serviceId = 0, serviceIdProcess = 0; string feeType = string.Empty, actionButton = string.Empty, msg =
                string.Empty;
            DateTime endTime = new DateTime();
            new CustomerServices().CustomerServices_LVN_KiemtraDvDangKy(_currentCustomerId, string.Empty,
                serviceId, ref isRegistService, ref serviceIdProcess, ref feeType, ref actionButton, ref msg);
            CustomerHelpers.WidgetUser_GetViewCount(_currentCustomerId, _currentLanguageId, 1, 1, getCountThongBaoHieuLuc, getCountPaymentTransactionSuccess, 0, ref countMessages, ref countCustomerDocs, ref countThongBaoHieuLuc, ref countPaymentTransactionSuccess, ref endTime);
            var model = new WidgetUserModel
            {
                CountMessages = countMessages,
                CountCustomerDocs = countCustomerDocs,
                CountThongBaoHieuLuc = countThongBaoHieuLuc,
                CountPaymentTransactionSuccess = countPaymentTransactionSuccess,
                IsUserVip = serviceIdProcess == Constants.ServiceIdTraCuuNangCao,
                IsRegistService = isRegistService
            };
            return PartialView("~/Views/Shared/Mobile/PartialWidgetUserHeaderMobile.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialWidgetUser()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult PartialCustomerDocs()
        {
            int rowCount = 0;
            List<DocsView> model = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount5, 0, string.Empty,
                1, 0, string.Empty, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, _currentCustomerId, 0, 0, "",
                0, 0, 0, string.Empty, string.Empty, String.Empty, 0, 0, 0, 0, 0, 0, 0, ref rowCount);
                //DocsViewHelpers.CustomerDocs_GetViewNewest(_currentLanguageId, _currentCustomerId, 1,
                    //Constants.RowAmount5);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialHeader(int menuItemId = 0, byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0, string title = "")
        {
            var model = new HeaderModel
            {
                MenuItemId = menuItemId,
                GetCountPaymentTransactionSuccess = getCountPaymentTransactionSuccess,
                GetCountThongBaoHieuLuc = getCountThongBaoHieuLuc,
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdTop),
                Title = title
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialHeaderV2(int menuItemId = 0, byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0, string title = "")
        {
            var model = new HeaderModel
            {
                MenuItemId = menuItemId,
                GetCountPaymentTransactionSuccess = getCountPaymentTransactionSuccess,
                GetCountThongBaoHieuLuc = getCountThongBaoHieuLuc,
                ListMenuItemsHeaderView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdHeader),
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdTop),
                Title = title
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialHeaderMobile(int menuItemId = 0, byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0,string title = "")
        {
            short displayTypeId = Constants.FieldsDisplayTypeIdVbpq;
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdMobile);
            List<Fields> listFields = DropDownListHelpers.GetAllFieldDisplayCache(displayTypeId);
            
            var model = new HeaderMobileModel
            {
                DocGroupId = Constants.DocGroupIdVbpq,
                MenuItemId = menuItemId,
                GetCountPaymentTransactionSuccess = getCountPaymentTransactionSuccess,
                GetCountThongBaoHieuLuc = getCountThongBaoHieuLuc,
                ListMenuItemsView = listMenuItemsView,
                ListMenuItemsViewParent = listMenuItemsView.Where(x => x.ParentItemId == 0).ToList(),
                ListField = listFields,
                WebsiteTitle = title
            };
            return PartialView("~/Views/Shared/Mobile/PartialHeaderMobile.cshtml", model);
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
        public ActionResult PartialHeaderMobileMyLuat(int menuItemId = 0, byte getCountPaymentTransactionSuccess = 0, byte getCountThongBaoHieuLuc = 0, string title = "")
        {
            short displayTypeId = Constants.FieldsDisplayTypeIdVbpq;
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdMyLuatVn);
            List<Fields> listFields = DropDownListHelpers.GetAllFieldDisplayCache(displayTypeId);
            var model = new HeaderMobileModel
            {
                DocGroupId = Constants.DocGroupIdVbpq,
                MenuItemId = menuItemId,
                GetCountPaymentTransactionSuccess = getCountPaymentTransactionSuccess,
                GetCountThongBaoHieuLuc = getCountThongBaoHieuLuc,
                ListMenuItemsView = listMenuItemsView,
                ListField = listFields,
                WebsiteTitle = title
            };
            return PartialView("~/Views/Shared/Mobile/PartialHeaderMobileMyLuat.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialHeaderAMP()
        {
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdMobile);
            var model = new HeaderMobileModel
            {
                ListMenuItemsView = listMenuItemsView,
                ListMenuItemsViewParent = listMenuItemsView.Where(x => x.ParentItemId == 0).ToList()
            };
            return PartialView("~/Views/Shared/AMP/PartialHeader.cshtml",model);
        }

        [ChildActionOnly]
        public ActionResult PartialMessageSearch(MessageSearch model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialHeaderMyLuatVN(string title)
        {
            var model = new HeaderModel
            {
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdMyLuatVn),
                Title = title
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialTaisaochonLuatVietNam()
        {
            List<MenuItemsView> listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdTaisaochonLuatvietnam);
            return PartialView(listMenuItemsView);
        }

        /// Heder tinh theo customer My luat
        [ChildActionOnly]
        public ActionResult PartialCustomerProvince()
        {
            CustomerProvinces mCustomerProvinces = new CustomerProvinces();
            List<CustomerProvinces> lCustomerProvinces = new List<CustomerProvinces>();
            try
            {
                Customers mCustomers = Customers.Static_Get(_currentCustomerId);
                lCustomerProvinces = mCustomerProvinces.GetListByCustomerId(_currentCustomerId);
                if (mCustomers.ProvinceId > 0)
                {
                    foreach (CustomerProvinces my_CustomerProvinces in lCustomerProvinces)
                    {
                        if (my_CustomerProvinces.ProvinceId == mCustomers.ProvinceId)
                        {
                            lCustomerProvinces = new List<CustomerProvinces>();
                            lCustomerProvinces.Add(my_CustomerProvinces);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex){ sms.utils.LogFiles.WriteLog(ex.ToString()); }
            return PartialView(lCustomerProvinces);
        }

        /// <summary>
        /// Menu trái, dưới box Lĩnh vực tra cứu - Trang chủ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialMenuLeft(List<MenuItemsView> model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFooter()
        {
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottom);
            FooterModel mFooterModel = new FooterModel
            {
                ListMenuItemsView = listMenuItemsView,
                ListMenuItemsViewParent = listMenuItemsView.Where(x => x.ParentItemId == 0).ToList(),
                //ListMenuItemsBottomRightView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottomRight)
            };
            return PartialView(mFooterModel);
        }

        [ChildActionOnly]
        public ActionResult PartialFooterMobile()
        {
            return PartialView("~/Views/Shared/Mobile/PartialFooterMobile.cshtml");
        }

        [ChildActionOnly]
        public ActionResult PartialMenuFooterMobile()
        {
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottomMobile);
            FooterModel mFooterModel = new FooterModel
            {
                ListMenuItemsView = listMenuItemsView,
                ListMenuItemsViewParent = listMenuItemsView.Where(x => x.ParentItemId == 0).ToList(),
                //ListMenuItemsBottomRightView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottomRight)
            };
            return PartialView("~/Views/Shared/Mobile/PartialMenuFooterMobile.cshtml", mFooterModel);
        }

        [ChildActionOnly]
        public ActionResult PartialMenuFooterAMP()
        {
            var listMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottomMobile);
            FooterModel mFooterModel = new FooterModel
            {
                ListMenuItemsView = listMenuItemsView,
                ListMenuItemsViewParent = listMenuItemsView.Where(x => x.ParentItemId == 0).ToList(),
            };
            return PartialView("~/Views/Shared/AMP/PartialMenuFooter.cshtml", mFooterModel);
        }

        /// <summary>
        /// Partial phân trang Ajax
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialPaginationAjax(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax2(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax3(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax4(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax5(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax6(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationAjax7(PartialPaginationAjax model)
        {
            return PartialView(model);
        }

        /// <summary>
        /// Partial tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFullSearch(PartialDocSearchModel model)
        {
            ModelState.Clear();
            return PartialView(model);
        }

        /// <summary>
        /// Partial tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFullSearchDetail(PartialFullSearchDetailModel model)
        {
            ModelState.Clear();
            model.FieldId = 0; //reset
            return PartialView(model);
        }

        /// <summary>
        /// Partial tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFullSearchHome(PartialDocSearchModel model)
        {
            ModelState.Clear();
            model.SearchOptions = 1;
            model.DisplayTypeId = 62;
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFullSearchHomeV2(PartialDocSearchModel model)
        {
            ModelState.Clear();
            model.SearchOptions = 1;
            model.DisplayTypeId = 62;
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFullSearchHomeMobile(PartialDocSearchModel model)
        {
            ModelState.Clear();
            model.SearchOptions = 1;
            model.DisplayTypeId = 62;
            return PartialView("~/Views/Shared/Mobile/PartialFullSearchHomeMobile.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialFullSearchMenuMobile(PartialDocSearchModel model)
        {
            ModelState.Clear();
            model.SearchOptions = 1;
            model.DisplayTypeId = 62;
            return PartialView("~/Views/Shared/Mobile/PartialFullSearchMenuMobile.cshtml", model);
        }

        /// <summary>
        /// Partial Lĩnh vực tra cứu
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFieldOfSearchHome(short displayTypeId = 68, byte docGroupId = 1, bool isHome = false)
        {
            displayTypeId = 68; //fix
            List<FieldDisplays> listFieldDisplays = DropDownListHelpers.GetAllFieldDisplays(displayTypeId);
            List<Fields> listFields = DropDownListHelpers.GetAllFieldByDisplayTypeId(Constants.DocTypeIdDisplayTypeIdVbpq);
            List<Fields> listFieldsOther = listFields.Where(f => listFieldDisplays.All(d => f.FieldId != d.FieldId)).ToList();

            var model = new PartialFieldOfSearchModel
            {
                DocGroupId = docGroupId,
                ListFieldDisplays = listFieldDisplays,
                ListFieldsOther = listFieldsOther,
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdLeft),
                IsHome = isHome
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialFieldOfSearch(short displayTypeId = 68, byte docGroupId = 1, bool isHome = false)
        {
            displayTypeId = 68; //fix
            List<FieldDisplays> listFieldDisplays =
                DropDownListHelpers.GetAllFieldDisplays(displayTypeId);
            List<Fields> listFields = DropDownListHelpers.GetAllFields();
            List<Fields> listFieldsOther = listFields.Where(f => listFieldDisplays.All(d => f.FieldId != d.FieldId)).ToList();

            var model = new PartialFieldOfSearchModel
            {
                DocGroupId = docGroupId,
                ListFieldDisplays = listFieldDisplays,
                ListFieldsOther = listFieldsOther,
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdLeft),
                IsHome = isHome
            };
            return PartialView(model);
        }

        /// <summary>
        /// Partial Lĩnh vực tra cứu của vbhn
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialFieldOfSearchVBHN(short displayTypeId = 0)
        {
            //var model = new PartialFieldOfSearchModel
            //{
            //    ListFieldDisplays = new FieldDisplays().FieldDisplays_GetList(0, "DisplayOrder ASC", displayTypeId, LawsVnLanguages.GetCurrentLanguageId())
            //};
            //if (model.ListFieldDisplays.HasValue() &&
            //    model.ListFieldDisplays.Count > 14)
            //{
            //    model.ListFieldVbhn = model.ListFieldDisplays.GetRange(15, model.ListFieldDisplays.Count - 15);
            //}
            displayTypeId = 68; //fix
            List<FieldDisplays> listFieldDisplays =
                DropDownListHelpers.GetAllFieldDisplays(displayTypeId);
            List<Fields> listFields = DropDownListHelpers.GetAllFields();
            List<Fields> listFieldsOther = listFields.Where(f => listFieldDisplays.All(d => f.FieldId != d.FieldId )).ToList();

            var model = new PartialFieldOfSearchModel
            {
                ListFieldDisplays = listFieldDisplays,
                ListFieldsOther = listFieldsOther
            };
            return PartialView(model);
        }

        /// <summary>
        /// Box điểm tin văn bản mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDiemTinVanBanMoi(CategoriesView model)
        {
            return PartialView("~/Views/Shared/Home/PartialDiemTinVanBanMoi.cshtml", model);
        }

        /// <summary>
        /// Partial tin tức pháp luật
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialTinTucPhapLuat(CategoriesView model)
        {
            return PartialView("~/Views/Shared/Home/PartialTinTucPhapLuat.cshtml", model);
        }

        //noi dung da xem
        [ChildActionOnly]
        public ActionResult PartialContentViewed(CategoriesView model)
        {
            return PartialView("~/Views/Shared/PartialContentViewed.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialContentViewedMobile(CategoriesView model)
        {
            return PartialView("~/Views/Shared/Mobile/PartialContentViewedMobile.cshtml", model);
        }

        //doi tac va phan hoi
        [ChildActionOnly]
        public ActionResult PartialFeedBackAndPartner(CategoriesView model)
        {
            return PartialView("~/Views/Shared/PartialFeedBackAndPartner.cshtml", model);
        }

        /// <summary>
        /// Partial tin vắn chính sách mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialTinVan(CategoriesView model)
        {
            if (model == null || model.CategoryId <= 0)
            {
                var mArticlesViewMaster = Extensions.GetArticlesViewMaster();
                model = mArticlesViewMaster.lCategoriesRight1.HasValue()
                    ? mArticlesViewMaster.lCategoriesRight1[0]
                    : new CategoriesView();
            }
            return PartialView("~/Views/Shared/PartialTinVan.cshtml", model);
        }

        /// <summary>
        /// Chi tiết tin: box ds tin theo chuyên mục Tin vb mới hoặc Tin pháp luật
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialBoxTieuDiem(short categoryId = 0)
        {
            int rowCount = 0;
            short[] cateArr = { Constants.CateIdTinVbMoi, Constants.CateIdTinPl };
            categoryId = Array.Exists(cateArr, s => s == categoryId) ? Array.Find(cateArr, m => m != categoryId) : Constants.CateIdTinVbMoi;
            var model = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, Constants.RowAmount3, 0, 0, 0, 0, 0, 0,
                ref rowCount);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialBoxTieuDiemMobile(short categoryId = 0)
        {
            int rowCount = 0;
            short[] cateArr = { Constants.CateIdTinVbMoi, Constants.CateIdTinPl };
            categoryId = Array.Exists(cateArr, s => s == categoryId) ? Array.Find(cateArr, m => m != categoryId) : Constants.CateIdTinVbMoi;
            var model = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, Constants.RowAmount3, 0, 0, 0, 0, 0, 0,
                ref rowCount);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialBoxTieuDiemAMP(short categoryId = 0)
        {
            int rowCount = 0;
            short[] cateArr = { Constants.CateIdTinVbMoi, Constants.CateIdTinPl };
            categoryId = Array.Exists(cateArr, s => s == categoryId) ? Array.Find(cateArr, m => m != categoryId) : Constants.CateIdTinVbMoi;
            var model = ArticlesViewHelpers.GetViewByCateId_Paging(categoryId, Constants.RowAmount3, 0, 0, 0, 0, 0, 0,
                ref rowCount);
            return PartialView(model);
        }

        /// <summary>
        /// Chi tiết tin: box ds văn bản mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialBoxNewDocuments()
        {
            int rowCount = 0;
            byte docGroupId = 1;
            List<DocsView> model = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, string.Empty,
                _currentLanguageId, docGroupId, string.Empty, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                string.Empty, 0, 0, 0, string.Empty, string.Empty, string.Empty, 0 , 0, 0, 0, 0, 0, 0, ref rowCount);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialBoxNewDocumentsMobile()
        {
            int rowCount = 0;
            byte docGroupId = 1;
            List<DocsView> model = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, string.Empty,
                _currentLanguageId, docGroupId, string.Empty, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                string.Empty, 0, 0, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, ref rowCount);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialBanTinLuatVietNam(CategoriesView model)
        {
            return PartialView("~/Views/Shared/Home/PartialBanTinLuatVietNam.cshtml", model);
        }

        /// <summary>
        /// Partial văn bản xem nhiều
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocsMostView()
        {
            List<DocsView> listDocsView = DocsViewHelpers.Docs_GetViewMostView(LawsVnLanguages.GetCurrentLanguageId(),
                Constants.DocGroupIdVbpq, 0, Constants.RowAmount10);
            return PartialView("~/Views/Shared/Home/PartialDocsMostView.cshtml", listDocsView);
        }
        /// Partial 5 văn bản xem nhiều
        [ChildActionOnly]
        public ActionResult PartialDocsMostView_5(PartialFullSearchDetailModel model)
        {
            int docGroupId = Constants.DocGroupIdVbpq;
            short fieldId = 0;
            if(model!= null)
            {
                if (model.DocGroupId > 0) docGroupId = model.DocGroupId;
                if (model.FieldId > 0) fieldId = model.FieldId;
            }
            List<DocsView> listDocsView = DocsViewHelpers.Docs_GetViewMostView(LawsVnLanguages.GetCurrentLanguageId(),docGroupId, fieldId, Constants.RowAmount5);
            return PartialView("~/Views/Shared/Home/PartialDocsMostView.cshtml", listDocsView);
        }

        [ChildActionOnly]
        public ActionResult PartialDocsNewestByField(short fieldId = 0, byte docGroupId = 0, byte languageId = 0)
        {
            int rowCount = 0;
            List<DocsView> listDocsView = DocsViewHelpers.Docs_GetViewSearch(Constants.RowAmount5, 0, string.Empty, languageId, docGroupId,string.Empty,0,string.Empty,string.Empty,0,fieldId,0,0,0,0,string.Empty,string.Empty,string.Empty,string.Empty,0,ref rowCount);
            return PartialView(listDocsView);
        }
        [ChildActionOnly]
        public ActionResult PartialDocsHotViewByDisplayType(short displayTypeId = 0, short fieldId = 0, byte docGroupId = 0, byte languageId = 0)
        {
            int rowCount = 0;
            List<DocsView> listDocsView = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, "DisplayOrder", languageId, docGroupId, string.Empty, 0, 0, string.Empty, string.Empty, 0, fieldId, 0, 0, 0, 0, displayTypeId, 0, 0, 0, string.Empty, 0, 0, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, ref rowCount);
            return PartialView(listDocsView);
        }

        /// Partial TCVN moi nhat
        [ChildActionOnly]
        public ActionResult PartialDocsNewestTCVN()
        {
            int rowCount = 0;
            List<DocsView> listDocsView = DocsViewHelpers.View_GetDocsViewNewest(LawsVnLanguages.GetCurrentLanguageId(), Constants.DocGroupIdTcvn, Constants.RowAmount5, 0, 1, 0, ref rowCount);
            return PartialView("~/Views/Shared/PartialDocsNewestTCVN.cshtml", listDocsView);
        }
        /// Partial văn bản UBND theo tinh
        [ChildActionOnly]
        public ActionResult PartialDocsMostView_UBND(int docId)
        {
            List<DocsView> lDocsView_UBND = new List<DocsView>();
            int DocGroupId = Constants.DocGroupIdVbpq;
            DocProvinces m_DocProvinces = new DocProvinces();
            string DateFrom = "", DateTo = "", OrderBy = "";
            int RowCount = 0, PageSize = 100, PageNum = 0;
            m_DocProvinces.DocId = docId;
            List<DocProvinces> l_DocProvinces = m_DocProvinces.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNum, ref RowCount);
            if (l_DocProvinces.HasValue())
            {
                int rowCount = 0, customerId = 0;
                string orderBy = string.Empty,
                    searchKeyword = string.Empty,
                    docName = string.Empty,
                    docIdentity = string.Empty,
                    searchByDate = string.Empty,
                    dateFrom = string.Empty,
                    dateTo = string.Empty;
                byte getRowCount = 1, isSearchExact = 0, displayTypeId = 0;
                short signerId = 0, fieldId = 0;
                byte effectStatusId = 0, docTypeId = 0;
                short organId = 0; string effectStatusName = "";
                m_DocProvinces = l_DocProvinces[0];
                short ProvinceId = m_DocProvinces.ProvinceId;
                lDocsView_UBND = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, orderBy, LawsVnLanguages.GetCurrentLanguageId(), Constants.DocGroupIdUbnd, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusName, ProvinceId, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);
            }
            if(lDocsView_UBND.Count==0)
            {
                lDocsView_UBND = DocsViewHelpers.Docs_GetViewMostView(LawsVnLanguages.GetCurrentLanguageId(), Constants.DocGroupIdUbnd, 0, Constants.RowAmount5);
            }
            return PartialView("~/Views/Shared/Home/PartialDocsMostView.cshtml", lDocsView_UBND);
        }
        /// Partial cong van thue va hai quan
        [ChildActionOnly]
        public ActionResult PartialCongVanHaiQuanThue()
        {
            int rowCount = 0;
            var model = new CongVanModel
            {
                ListCongVanThueMoi = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, "", 0,
                    Constants.DocGroupIdCongVan, "", 0, 0, "", "", Constants.DocTypesIdCongVan, 4, 0, 0, 0, 0, 0, 0, 0, 0,
                    "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, ref rowCount),
                ListCongVanHaiQuanMoi = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount5, 0, "", 0,
                Constants.DocGroupIdCongVan, "", 0, 0, "", "", Constants.DocTypesIdCongVan, 21, 0, 0, 0, 0, 0, 0, 0,
                0, "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, ref rowCount),
            };
            return PartialView("~/Views/Shared/PartialCongVanHaiQuanThue.cshtml", model);
        }
        /// Partial VB hop nhat moi cap nhat
        [ChildActionOnly]
        public ActionResult PartialDocsConsolidationNewest()
        {
            byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
            byte _docGroupId = Constants.DocGroupIdVbhn;
            short _displayTypeId = Constants.FieldDisplayTypeIdVbhn;
            byte _docTypeId = Constants.DocTypesIdVbhn;
            int rowCount = 0;
            string orderBy = string.Empty, searchKeyword = string.Empty,
                dateFrom = string.Empty, dateTo = string.Empty;
            short fieldId = 0, organId = 0;
            DocsViewSearch mDocsViewSearch = DocsViewHelpers.Docs_GetViewSearchWithCount(0, Constants.RowAmount5, 0, orderBy,
                    _languageId, _docGroupId, searchKeyword, 0, 0, "", "", _docTypeId, fieldId, 0, organId, 0, 0, 0, 0, 0, 0, "", 0, 0, 0,
                    "", dateFrom, dateTo, 0, 0, 1, 0, 0, 0, 0, ref rowCount);
            return PartialView("~/Views/Shared/PartialDocsConsolidationNewest.cshtml", mDocsViewSearch);
        }
        /// Partial Tin Tức xem nhiều
        [ChildActionOnly]
        public ActionResult PartialNewsMostView()
        {
            int rowCount = 0;
            short categoryId = 230; //tin phap luat
            ArticlesViewCate listDocsView =  ArticlesViewHelpers.GetViewMostViewByCate_Paging(categoryId, Constants.RowAmount5, 0, 0, 0, ref rowCount);
            return PartialView("~/Views/Shared/Home/PartialNewsMostView.cshtml", listDocsView);
        }

        [ChildActionOnly]
        public ActionResult PartialMenuDocDetail()
        {
            var model = new MenuDocDetail
            {
                Menu = Menus.Static_Get(Constants.MenuIdDocDetail),
                ListMenuItemsView = DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdDocDetail)
            };
            return PartialView(model);
        }

        /// <summary>
        /// Partial văn bản cập nhật hàng tuần
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialVanBanCapNhatHangTuan(CategoriesView model)
        {
            return PartialView("~/Views/Shared/Home/PartialVanBanCapNhatHangTuan.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialSharedCorner(CategoriesView model)
        {
            if (model == null || model.CategoryId <= 0)
            {
                var mArticlesViewMaster = Extensions.GetArticlesViewMaster();
                model = mArticlesViewMaster.lCategoriesBottom1.HasValue()
                    ? mArticlesViewMaster.lCategoriesBottom1[0]
                    : new CategoriesView();
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialNewsLetterEmail()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult PartialNewsLetterEmailMobile()
        {
            return PartialView("~/Views/Shared/Mobile/PartialNewsLetterEmailMobile.cshtml");
        }

        [ChildActionOnly]
        public ActionResult PartialPaginationArticlesAjax(PartialPaginationAjax model)
        {
            return PartialView(model);
        }
        /// <summary>
        /// Partial tiêu điểm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialTieuDiem(CategoriesView model)
        {
            return PartialView(model);
        }

        /// <summary>
        /// Partial mục lục văn bản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocIndexes(List<DocIndexes> model)
        {
            return PartialView(model);
        }

        /// <summary>
        /// Partial văn bản cùng lĩnh vực mới nhất
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocOthersNewset(DocOthersModel model)
        {
            model.ListFields = DropDownListHelpers.GetAllFields();
            return PartialView(model);
        }

        /// <summary>
        /// Partial văn bản cùng lĩnh vực cũ hơn văn bản hiện tại
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PartialDocOthers(DocOthersModel model)
        {
            model.ListFields = DropDownListHelpers.GetAllFields();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialDocOthersNewsetMobile(DocOthersModel model)
        {
            model.ListFields = DropDownListHelpers.GetAllFields();
            return PartialView("~/Views/Shared/Mobile/PartialDocOthersNewsetMobile.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialDocOthersMobile(DocOthersModel model)
        {
            model.ListFields = DropDownListHelpers.GetAllFields();
            return PartialView("~/Views/Shared/Mobile/PartialDocOthersMobile.cshtml",model);
        }

        [ChildActionOnly]
        public ActionResult PartialAdvertise(int advertPositionId)
        {
            return PartialView(advertPositionId);
        }

        [ChildActionOnly]
        public ActionResult PartialLinkDocumentAttribute(PartialLinkDocumentAttributeModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialLinkDocumentAttributeMobile(PartialLinkDocumentAttributeModel model)
        {
            return PartialView("~/Views/Shared/Mobile/PartialLinkDocumentAttributeMobile.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialLinkDocumentAttribute2(PartialLinkDocumentAttributeModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialLinkDocumentAttribute2Mobile(PartialLinkDocumentAttributeModel model)
        {
            return PartialView("~/Views/Shared/Mobile/PartialLinkDocumentAttribute2Mobile.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult PartialLinkDocumentAttributeAMP(PartialLinkDocumentAttributeModel model)
        {
            return PartialView("~/Views/Shared/AMP/PartialLinkDocumentAttributeAMP.cshtml", model);
        }

        [LawsVnAuthorize]
        [ChildActionOnly]
        public ActionResult PartialCustomerInterFaceFields()
        {
            DataSet ds = new CustomerFields().CustomerFields_GetCount2(_currentCustomerId, _currentLanguageId, Constants.DocGroupIdVbpq, 1, 0);
            DataTable dt1 = CustomerFields.Static_GetCount_GetListField(ds);
            DataSet ds2 = new CustomerFields().CustomerFields_GetCount2(_currentCustomerId, _currentLanguageId, Constants.DocGroupIdTcvn, 1, 0);
            DataTable dt2 = CustomerFields.Static_GetCount_GetListField(ds2);
            CustomerInterFaceFieldsModel model = new CustomerInterFaceFieldsModel
            {
                ListFieldsVbpq = dt1.DataTableExists() ? (from DataRow dr in dt1.Rows
                    select new Fields
                    {
                        FieldId = Extensions.GetColumnValue<short>(dr, "FieldId"),
                        FieldName = Extensions.GetColumnValue<string>(dr, "FieldName"),
                        FieldDesc = Extensions.GetColumnValue<string>(dr, "FieldDesc"),
                        SoLuong = Extensions.GetColumnValue<int>(dr, "SoLuong")
                    }).ToList() : new List<Fields>(),
                ListFieldsTcvb = dt2.DataTableExists() ? (from DataRow dr in dt2.Rows
                    select new Fields
                    {
                        FieldId = Extensions.GetColumnValue<short>(dr, "FieldId"),
                        FieldName = Extensions.GetColumnValue<string>(dr, "FieldName"),
                        FieldDesc = Extensions.GetColumnValue<string>(dr, "FieldDesc"),
                        SoLuong = Extensions.GetColumnValue<int>(dr, "SoLuong")
                    }).ToList() : new List<Fields>()
            };
            return PartialView(model);
        }

        #region Services
        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialPaymentServiceUsingBankAccount(PaymentServiceUsingBankAccountModel model)
        {
            model.CustomerName = Extensions.GetCurrentCustomerName();
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialBankPayment(BankPaymentModel model)
        {
            return PartialView(model);
        }
        
        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialBankPaymentZalo(BankPaymentModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialBankPaymentMobile(BankPaymentModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialBankPaymentZaloMobile(BankPaymentModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialOnlinePayment(PaymentMethodsOnlineModel model)
        {
            return PartialView(model);
        }       
        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialOnlinePaymentMobile(PaymentMethodsOnlineModel model)
        {
            model.PaymentRegulations = DropDownListHelpers.GetArticlesViewDetail(Constants.QuyUocBaoMatArticleId);
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialPaymentServiceUsingScratchCard(PaymentServiceUsingScratchCardModel model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialPromotionCodeCheck(PromotionCodeCheckModel model)
        {
            ModelState.Clear();
            if (model == null)
            {
                model = new PromotionCodeCheckModel
                {
                    ServicePackageId = 0,
                    ServicePackageParentId = 0
                };
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialPromotionCodeCheckMobile(PromotionCodeCheckModel model)
        {
            ModelState.Clear();
            if (model == null)
            {
                model = new PromotionCodeCheckModel
                {
                    ServicePackageId = 0,
                    ServicePackageParentId = 0
                };
            }
            return PartialView(model);
        }

        [ChildActionOnly]
        [LawsVnAuthorize]
        public ActionResult PartialCustomerNameCheck()
        {
            var model = new CustomerNameCheckModel
            {
                CustomerName = Extensions.GetCurrentCustomerName()
            };
            return PartialView(model);
        }
        #endregion

        #region Permission
            #region DocPermission

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionDocDiagram(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0, rowCountEn = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount5000, 0, 1,
                        ref rowCount);

                    var mRelateTypes = new RelateTypes
                    {
                        DocGroupId = docModel.DocGroupId
                    };
                    docModel.ListRelateTypesByGroupId = mRelateTypes.GetByGroupId();
                    docModel.ListRelateTypesLeft =
                        docModel.ListRelateTypesByGroupId.FindAll(i =>
                            i.DisplayPosition.Equals("Left"));
                    docModel.ListRelateTypesBottom =
                        docModel.ListRelateTypesByGroupId.FindAll(i =>
                            i.DisplayPosition.Equals("Bottom"));
                    docModel.ListRelateTypesRight =
                        docModel.ListRelateTypesByGroupId.FindAll(
                            i => i.DisplayPosition.Equals("Right"));
                    //docModel.ListRelateTypesOtherLanguages =
                    //    docModel.ListRelateTypesByGroupId.FindAll(i =>
                    //        i.LanguageId == (docModel.LanguageId == 1 ? 2 : 1));
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.ListDocsOtherLanguages = new Docs().GetPage(0, Constants.RowAmount300, 0, string.Empty, 2, string.Empty, 0, 0,
                        docModel.mDocsViewDetail.mDocsView.DocId, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, ref rowCountEn);
                    docModel.RowCountDocEnglish = rowCountEn;
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocDiagram.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionDocDiagramMobile(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0, rowCountEn = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, string.Empty, Constants.RowAmount5000, 0, 1,
                        ref rowCount);
                   
                    var mRelateTypes = new RelateTypes
                    {
                        DocGroupId = docModel.DocGroupId
                    };
                    docModel.ListRelateTypesByGroupId = mRelateTypes.GetByGroupId();
                    if (mDocsViewDetail.lRelateTypes.HasValue())
                    {
                        docModel.ListRelateTypesByGroupId.RemoveAll(m =>
                            mDocsViewDetail.lRelateTypes.Select(l => l.RelateTypeId).Contains(m.RelateTypeId) && mDocsViewDetail.lRelateTypes.Select(l => l.DisplayPosition).Contains(m.DisplayPosition));
                    }
                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.ListDocsOtherLanguages = new Docs().GetPage(0, Constants.RowAmount300, 0, string.Empty, 2, string.Empty, 0, 0,
                        docModel.mDocsViewDetail.mDocsView.DocId, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, ref rowCountEn);
                    docModel.RowCountDocEnglish = rowCountEn;
                }
                else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocDiagramMobile.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Hiệu lực, biết tình trạng hiệu lực của văn bản đến thời điểm hiện tại.")]
            public ActionResult PartialPermissionDocEffect(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelatesEffect(docModel.LanguageId, docModel.mDocsViewDetail.mDocsView.DocId);
                    docModel.ListDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.ListRelateTypesRight = mDocsViewDetail.lRelateTypes;
                }
                else docModel = new DocsViewDetailModel();
                return PartialView(string.Format("~/Views/Shared/{0}.cshtml", Request.Browser.IsMobileDevice ? "Mobile/PartialPermissionDocEffectMobile" : "Permission/Docs/PartialPermissionDocEffect"), docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Liên quan, hiểu các mối quan hệ, các văn bản liên quan đến văn bản đang xem.")]
            public ActionResult PartialPermissionDocRelate(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, docModel.RelateTypeId, "",
                        Constants.RowAmount20, 0, 1,
                        ref rowCount,1);

                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                    docModel.RelateTypes =
                        docModel.mDocsViewDetail.lRelateTypes.FirstOrDefault(
                            x => x.RelateTypeId == docModel.RelateTypeId);
                    docModel.CountByRelateTypeId = mDocsViewDetail.lRelateTypes.Sum(x => x.RowCount);

                    docModel.mDocsViewDetail.lDocRelates = (from r in mDocsViewDetail.lDocRelates
                        join e in docModel.ListEffectStatus on r.EffectStatusId equals e.EffectStatusId
                        select new DocRelates
                        {
                            DocName = r.DocName,
                            DocId = r.DocId,
                            DocGroupId = r.DocGroupId,
                            DocUrl = r.DocUrl,
                            EffectStatusName = e.EffectStatusDesc,
                            RelateTypeName = r.RelateTypeName,
                            CrDateTime = r.CrDateTime,
                            EffectDate = r.EffectDate,
                            IssueDate = r.IssueDate,
                            IssueYear = r.IssueYear
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
                        RelateTypeId = docModel.RelateTypeId,
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
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocRelate.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Liên quan, hiểu các mối quan hệ, các văn bản liên quan đến văn bản đang xem.")]
            public ActionResult PartialPermissionDocRelateMobile(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, docModel.RelateTypeId, "",
                        Constants.RowAmount20, 0, 1,
                        ref rowCount);

                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                    docModel.RelateTypes =
                        docModel.mDocsViewDetail.lRelateTypes.FirstOrDefault(
                            x => x.RelateTypeId == docModel.RelateTypeId);
                    docModel.CountByRelateTypeId = mDocsViewDetail.lRelateTypes.Sum(x => x.RowCount);

                    docModel.mDocsViewDetail.lDocRelates = (from r in mDocsViewDetail.lDocRelates
                                                            join e in docModel.ListEffectStatus on r.EffectStatusId equals e.EffectStatusId
                                                            select new DocRelates
                                                            {
                                                                DocName = r.DocName,
                                                                DocId = r.DocId,
                                                                DocGroupId = r.DocGroupId,
                                                                DocUrl = r.DocUrl,
                                                                EffectStatusName = e.EffectStatusDesc,
                                                                RelateTypeName = r.RelateTypeName,
                                                                CrDateTime = r.CrDateTime,
                                                                EffectDate = r.EffectDate,
                                                                IssueDate = r.IssueDate,
                                                                IssueYear = r.IssueYear
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
                        RelateTypeId = docModel.RelateTypeId,
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
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocRelateMobile.cshtml", docModel);
            }

            //[ChildActionOnly]
            //[LawsVnAuthorize(Roles = "TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem tóm tắt văn bản")]
            //public ActionResult PartialPermissionDocSummary(DocsViewDetailModel docModel)
            //{
            //    if (docModel == null) docModel = new DocsViewDetailModel();
            //    return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocSummary.cshtml", docModel);
            //}

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,TC,NVTA,NVNC,TA,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml")]
            public ActionResult PartialPermissionDocEnglish(DocsViewDetailModel docModel, byte languageId)
            {
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocEnglish.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,TC,NVTA,NVNC,TA,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml")]
            public ActionResult PartialPermissionDocEnglishMobile(DocsViewDetailModel docModel, byte languageId)
            {                
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocEnglishMobile.cshtml", docModel);
            }
            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTA,NVNC,TA,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", MessageTypes = ViewModelBase.MessageTypes.NotAuthDownload)]
            public ActionResult PartialPermissionDocEngDownload(DocsViewDetailModel docModel)
            {
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocEngDownload.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionVietNamStandardDiagram(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount5000, 0, 1,
                        ref rowCount);
                    var mRelateTypes = new RelateTypes
                    {
                        DocGroupId = docModel.mDocsViewDetail.mDocsView.DocGroupId
                    };
                    docModel.ListRelateTypesByGroupId = mRelateTypes.GetByGroupId();
                    docModel.ListRelateTypesTop1 = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Top1"));
                    docModel.ListRelateTypesTop2 = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Top2"));
                    docModel.ListRelateTypesLeft = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Left"));
                    docModel.ListRelateTypesBottom = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Bottom"));
                    docModel.ListRelateTypesRight = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Right"));
                    docModel.ListRelateTypesOtherLanguages = docModel.ListRelateTypesByGroupId.FindAll(i => i.LanguageId == (docModel.LanguageId == 1 ? 2 : 1));
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionVietNamStandardDiagram.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionVietNamStandardDiagramMobile(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount5000, 0, 1,
                        ref rowCount);
                    var mRelateTypes = new RelateTypes
                    {
                        DocGroupId = docModel.mDocsViewDetail.mDocsView.DocGroupId
                    };
                    docModel.ListRelateTypesByGroupId = mRelateTypes.GetByGroupId();
                    docModel.ListRelateTypesTop1 = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Top1"));
                    docModel.ListRelateTypesTop2 = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Top2"));
                    docModel.ListRelateTypesLeft = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Left"));
                    docModel.ListRelateTypesBottom = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Bottom"));
                    docModel.ListRelateTypesRight = docModel.ListRelateTypesByGroupId.FindAll(i => i.DisplayPosition.Equals("Right"));
                    docModel.ListRelateTypesOtherLanguages = docModel.ListRelateTypesByGroupId.FindAll(i => i.LanguageId == (docModel.LanguageId == 1 ? 2 : 1));
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                }
                else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionVietNamStandardDiagramMobile.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Liên quan, hiểu các mối quan hệ, các văn bản liên quan đến văn bản đang xem.")]
            public ActionResult PartialPermissionVietNamStandardRelate(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount20, 0, 1,
                           ref rowCount);
                    var mRelateTypes = new RelateTypes
                    {
                        DocGroupId = docModel.mDocsViewDetail.mDocsView.DocGroupId
                    };
                    docModel.ListRelateTypesByGroupId = mRelateTypes.GetByGroupId();

                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;

                    //ds lien quan hieu luc
                    docModel.ListDocRelatesEffect = from a in docModel.mDocsViewDetail.lDocRelates
                        join b in docModel.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId
                        where (from x in docModel.ListRelateTypesByGroupId
                            where x.RelateTypeGroupId == Constants.RelateTypeGroupHieuLuc
                            select x.RelateTypeId).Contains(a.RelateTypeId)
                        select new DocRelates
                        {
                            DocId = a.DocId,
                            DocName = a.DocName,
                            DocUrl = a.DocUrl,
                            DocGroupId = a.DocGroupId,
                            IssueDate = a.IssueDate,
                            EffectDate = a.EffectDate,
                            IssueYear = a.IssueYear,
                            EffectStatusId = a.EffectStatusId,
                            EffectStatusName = b.EffectStatusDesc,
                            RelateTypeId = a.RelateTypeId,
                            RelateTypeName = a.RelateTypeName,
                            LanguageId = a.LanguageId
                        };
                    //ds lien quan noi dung
                    docModel.ListDocRelatesContent = from a in docModel.mDocsViewDetail.lDocRelates
                        join b in docModel.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId
                        where (from x in docModel.ListRelateTypesByGroupId
                            where x.RelateTypeGroupId == Constants.RelateTypeGroupNoiDung
                            select x.RelateTypeId).Contains(a.RelateTypeId)
                        select new DocRelates
                        {
                            DocId = a.DocId,
                            DocName = a.DocName,
                            DocUrl = a.DocUrl,
                            DocGroupId = a.DocGroupId,
                            IssueDate = a.IssueDate,
                            EffectDate = a.EffectDate,
                            IssueYear = a.IssueYear,
                            EffectStatusId = a.EffectStatusId,
                            EffectStatusName = b.EffectStatusDesc,
                            RelateTypeId = a.RelateTypeId,
                            RelateTypeName = a.RelateTypeName,
                            LanguageId = a.LanguageId
                        };

                    docModel.mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        TotalPage = rowCount,
                        PageSize = Constants.RowAmount20,
                        LinkLimit = Constants.RowAmount5,
                        ShowNumberOfResults = Constants.RowAmount20,
                        LanguageId = docModel.LanguageId,
                        DocGroupId = docModel.mDocsViewDetail.mDocsView.DocGroupId,
                        FieldId = docModel.FieldId,
                        ControllerName = "Ajax",
                        ActionName = "DocsConsolidation_GetDocRelateType",
                        DocId = docModel.mDocsViewDetail.mDocsView.DocId,
                        RelateTypeId = docModel.RelateTypeId,
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ListByField",
                            InsertionMode = InsertionMode.Replace
                        }
                    };
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionVietNamStandardRelate.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionDocsConsolidationDiagram(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount5000, 0, 1,
                        ref rowCount);
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.ListDocRelates = docModel.mDocsViewDetail.lDocRelates.FindAll(m => m.DisplayPosition.Equals("Left"));
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocsConsolidationDiagram.cshtml", docModel);
            }
            
            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Lược đồ, biết vị trí văn bản trong hệ thống Luật Việt Nam")]
            public ActionResult PartialPermissionDocsConsolidationDiagramMobile(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, "", Constants.RowAmount5000, 0, 1,
                        ref rowCount);
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.ListDocRelates = docModel.mDocsViewDetail.lDocRelates.FindAll(m => m.DisplayPosition.Equals("Left"));
                }
                else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocsConsolidationDiagramMobile.cshtml", docModel);
            }

            [ChildActionOnly]
            [LawsVnAuthorize(Roles = "NVTC,NVTA,NVNC,TA,TC,NC", ShowAuthView = true, ViewNameUnAuth = "~/Views/Error/NotAuth.cshtml", ErrorMessage = "Để xem mục Liên quan, hiểu các mối quan hệ, các văn bản liên quan đến văn bản đang xem.")]
            public ActionResult PartialPermissionDocsConsolidationRelate(DocsViewDetailModel docModel, byte languageId)
            {
                if (docModel != null)
                {
                    int rowCount = 0;
                    var mDocsViewDetail = DocsViewHelpers.Docs_GetViewRelates(docModel.LanguageId,
                        docModel.mDocsViewDetail.mDocsView.DocId, 0, string.Empty, Constants.RowAmount20, 0, 1,
                        ref rowCount);
                    docModel.mDocsViewDetail.lDocRelates = mDocsViewDetail.lDocRelates;
                    docModel.mDocsViewDetail.lRelateTypes = mDocsViewDetail.lRelateTypes;
                    docModel.mPartialPaginationAjax = new PartialPaginationAjax
                    {
                        TotalPage = rowCount,
                        PageSize = Constants.RowAmount20,
                        LinkLimit = Constants.RowAmount5,
                        ShowNumberOfResults = Constants.RowAmount20,
                        LanguageId = docModel.LanguageId,
                        DocGroupId = docModel.mDocsViewDetail.mDocsView.DocGroupId,
                        FieldId = docModel.FieldId,
                        ControllerName = "Ajax",
                        ActionName = "DocsConsolidation_GetDocRelateType",
                        DocId = docModel.mDocsViewDetail.mDocsView.DocId,
                        RelateTypeId = docModel.RelateTypeId,
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ListByField",
                            InsertionMode = InsertionMode.Replace
                        }
                    };
                }else docModel = new DocsViewDetailModel();
                return PartialView("~/Views/Shared/Permission/Docs/PartialPermissionDocsConsolidationRelate.cshtml", docModel);
            }

            #endregion
        #endregion

        #region Mobile

        [ChildActionOnly]
        public ActionResult PartialLogin()
        {
            return PartialView("~/Views/Shared/Mobile/PartialLogin.cshtml");
        }

        [ChildActionOnly]
        public ActionResult PartialUpdateCustomerField()
        {
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var model = new LawsCustomerFields
            {
                ListCustomerFields = new CustomerFields().GetListFieldsByCustomerId(currentCustomerId)
            };
            return PartialView("~/Views/Shared/Mobile/PartialUpdateCustomerField.cshtml", model);
        }
        #endregion

    }
}
