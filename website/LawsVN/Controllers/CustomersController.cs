using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawsVN.Models;
using ICSoft.CMSViewLib;
using System.Web.Script.Serialization;
using LawsVN.Library;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using System.Web.Mvc.Ajax;
using System.Data;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/
        private int _rowCount = 0,_rowCount1 = 0;
        private byte currentLanguageId = 1;

        #region Giao diện cá nhân

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult CustomerInterFace()
        {
            Customers mCustomers = new Customers();
            CustomerFields mCustomerFields = new CustomerFields();
            int CustomerId = Extensions.GetCurrentCustomerId();
            int rowCount = 0, RowCount_ = 0;
            mCustomers = Customers.Static_Get(CustomerId);
            string orderBy = string.Empty, effectStatusType = string.Empty, searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty, searchByDate = string.Empty;
            byte docGroupId = 1;
            short signerId = 0; byte IsSearchExact = 0; byte DisplayTypeId = 0;
            string EffectStatusType = Constants.EffectStatusMoiBanHanh;
            string SearchByDate = "", DateFrom = "", DateTo = ""; byte GetRowCount = 1;
            int pageIndex = 0; short fieldId = 0, fieldFirst = 0; byte effectStatusId = 0; short organId = 0; byte docTypeId = 0;
            short provinceid = mCustomers.ProvinceId;
            CustomerProvinces mCustomerProvinces = new CustomerProvinces();
            List<CustomerProvinces> lCustomerProvinces = mCustomerProvinces.GetListByCustomerId(CustomerId);
            List<CustomerFields> lCustomerFields = mCustomerFields.GetListFieldsByCustomerId(CustomerId);
            //DataSet dt = mCustomerFields.CustomerFields_GetCount(CustomerId, currentLanguageId, docGroupId, 1, 0);
            //DataTable datafield = CustomerFields.Static_GetCount_GetListField(dt);
            
            //lay van ban moi ban hanh theo customerID
            List<DocsView> lDocsViews_VBMoiBanHanh = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount3, pageIndex, orderBy, currentLanguageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);
            //lay van ban lien quan khac
            DocKeywords mDocKeywords = new DocKeywords();
            if(lDocsViews_VBMoiBanHanh.HasValue())
            {
                foreach(DocsView myDocsView in lDocsViews_VBMoiBanHanh)
                {
                    List<DocKeywords> lDocKeywords = mDocKeywords.GetListByDocId(myDocsView.DocId);
                    if (lDocKeywords.HasValue())
                    {
                        mDocKeywords = lDocKeywords[0];
                        break;
                    }
                }

            }
            byte GetListCateOfShowArticle = 0;
            int RowAmount = 2, PageIndexDocsViewrelated = 0, RowCountDocsViewrelated = 0;
            List<DocsView> ListDocsViewrelated = new List<DocsView>();
            ListDocsViewrelated = DocsViewHelpers.GetViewByKeyWordId_Paging(mDocKeywords.KeywordId, RowAmount, GetListCateOfShowArticle, GetRowCount, PageIndexDocsViewrelated, ref RowCountDocsViewrelated);
            if (ListDocsViewrelated.Count == 0 && lDocsViews_VBMoiBanHanh.HasValue())
            {
                byte isSearchExact = 0, displayTypeId = 0;
                List<DocFields> DocFields = new DocFields().DocFields_GetList(0, lDocsViews_VBMoiBanHanh[0].DocId);
                if (DocFields.HasValue())
                {
                    fieldId = DocFields[0].FieldId;
                }
                //fieldId = mDocsViewDetail.mDocsView.
                ListDocsViewrelated = DocsViewHelpers.Docs_GetViewSearch(0, Constants.RowAmount3, 0, orderBy, currentLanguageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, 0, 0, effectStatusId, "", 0, 0, 0, searchByDate, "", "", 0, 0, 0, 0, 0, 0, 0, ref _rowCount);
            }
            //lay van ban theo linh vuc
            if (lCustomerFields.HasValue())
            {
                fieldId = lCustomerFields[0].FieldId;
                fieldFirst = fieldId;
            }
            List<DocsView> lDocsViewsByCustomerFields = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount10, pageIndex, orderBy, currentLanguageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref _rowCount);

            ArticlesViewMaster mArticlesViewMaster = Extensions.GetArticlesViewMaster();
           //lay tin tuc theo customerId
            //int rowcout_Article = 0;
            //Articles m_Articles = new Articles();
            //List<ArticlesView> ListArticlesByCustomerId = m_Articles.GetByCustomerId(CustomerId, 0, 0, "", "", "", 10, 0, ref rowcout_Article);
            
            //lay danh sach van ban uy ban nhan dan
            fieldId = 0;
            docGroupId = DocGroups.VBUBND;
            List<DocsView> lDocsView_UBND = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount3, pageIndex, orderBy, currentLanguageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref RowCount_);
            //lay danh sach van ban TCVN
            docGroupId = DocGroups.TCVN;
            List<DocsView> lDocsView_TCVN = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount3, pageIndex, orderBy, currentLanguageId, docGroupId, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, DisplayTypeId, CustomerId, 0, effectStatusId, EffectStatusType, 0, 0, 0, SearchByDate, DateFrom, DateTo, GetRowCount, 0, 0, 0, 0, 0, 0, ref RowCount_);
            
            var mMyDocsViewModel = new CustomerDocsViewModel
            {
                //ListArticlesByCustomerId = ListArticlesByCustomerId,
                mArticlesViewMaster = mArticlesViewMaster,
                lCustomerFields = lCustomerFields,// Lĩnh vực văn bản quan tâm
                //DataTableField  = datafield,
                lCustomerProvinces = lCustomerProvinces, //tinh thanh khac hang dang ky
                ListDocsViewFirst = lDocsViews_VBMoiBanHanh,
                lMyDocs = lDocsViewsByCustomerFields,
                ListDocsViewrelated = ListDocsViewrelated,
                //ListFields =  DropDownListHelpers.GetAllFields(),
                lDocsView_UBND = lDocsView_UBND,
                lDocsView_TCVN = lDocsView_TCVN,
                ListDocTypes = DropDownListHelpers.DocTypes_GetList(0),
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    TotalPage = _rowCount,
                    PageIndex = 0,
                    PageSize = Constants.RowAmount3,
                    LinkLimit = Constants.RowAmount3,
                    UrlPaging = string.Empty,
                    DocGroupId = docGroupId,
                    LanguageId = currentLanguageId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Customers_Interface",
                    EffectStatusName = Constants.EffectStatusMoiBanHanh,
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "FirstBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                PartialPaginationAjaxSecond = new PartialPaginationAjax
                {
                    FieldId = fieldFirst,
                    TotalPage = _rowCount,
                    PageIndex = 1,
                    PageSize = Constants.RowAmount10,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    DocGroupId =  DocGroups.VBPQ,
                    LanguageId = currentLanguageId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Customers_InterfaceByField",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ContentByField",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return View(mMyDocsViewModel);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult CustomerInterFaceLocation()
        {
            int customerId = Extensions.GetCurrentCustomerId(), rowCount = 0;
            string orderBy = string.Empty, effectStatusType = string.Empty, searchKeyword = string.Empty, docName = string.Empty, docIdentity = string.Empty, searchByDate = string.Empty;
            short signerId = 0, fieldId = 0, organId = 0;
            byte IsSearchExact = 0;
            byte displayTypeId = 0, getRowCount = 1, effectStatusId = 0, docTypeId = 0;
            string dateFrom = string.Empty, dateTo = string.Empty; 

            //DataSet dt = new CustomerFields().CustomerFields_GetCount(customerId, currentLanguageId, DocGroups.VBPQ, 1, 0);
            //DataTable datafield = CustomerFields.Static_GetCount_GetListField(dt);

            var model = new CusomersInterfaceModel
            {
                lCustomerProvinces = new CustomerProvinces().GetListByCustomerId(customerId),
                lDocsView_UBND = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount5, 0, orderBy, currentLanguageId, DocGroups.VBUBND, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                lDocsView_TrungUongLienQuan = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount10, 0, orderBy, currentLanguageId, DocGroups.VBPQ, searchKeyword, IsSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref _rowCount),
                //DataTableField = datafield,
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageIndex = (Extensions.IsMobile()?0:1),
                    PageSize = Constants.RowAmount5,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    DocGroupId = DocGroups.VBUBND,
                    LanguageId = currentLanguageId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Customers_InterfaceLocation",
                    ClassTagItem = "tag-item item7",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "FirstBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                PartialPaginationAjaxSecond = new PartialPaginationAjax
                {
                    TotalPage = _rowCount,
                    PageIndex = 1,
                    PageSize = Constants.RowAmount10,
                    LinkLimit = Constants.RowAmount5,
                    UrlPaging = string.Empty,
                    DocGroupId = DocGroups.VBPQ,
                    LanguageId = currentLanguageId,
                    UsingDisplayTable = 0,
                    ControllerName = "Ajax",
                    ActionName = "Customers_InterfaceLocation",
                    ClassTagItem = "tag-item item7",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "SecondBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };

            //ToDo: Lấy danh sách tỉnh thành quan tâm
            if (model.lCustomerProvinces.HasValue())
            {
                model.ListProvinces = (from p in model.lProvinces
                                       join cp in model.lCustomerProvinces on p.ProvinceId equals cp.ProvinceId
                                       select new Provinces
                                       {
                                           CustomerProvinceId = cp.CustomerProvinceId,
                                           ProvinceId = p.ProvinceId,
                                           ProvinceDesc = p.ProvinceDesc
                                       }).ToList();
            }

            //ToDo: Lấy danh sách lĩnh vực vb quan tâm
            if (model.DataTableField.DataTableExists())
            {
                model.ListFieldsQuanTam = (from DataRow dr in model.DataTableField.Rows
                    select new Fields
                    {
                        FieldId = Extensions.GetColumnValue<short>(dr,"FieldId"),
                        FieldName = Extensions.GetColumnValue<string>(dr,"FieldName"),
                        FieldDesc = Extensions.GetColumnValue<string>(dr, "FieldDesc"),
                        SoLuong = Extensions.GetColumnValue<int>(dr, "SoLuong")
                    }).ToList();
            }

            if (model.lDocsView_UBND.HasValue())
            {
                model.lDocsView_UBND = (from a in model.lDocsView_UBND
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
                        EffectStatusName = b.EffectStatusDesc
                    }).ToList();
            }

            if (model.lDocsView_TrungUongLienQuan.HasValue())
            {
                model.lDocsView_TrungUongLienQuan = (from a in model.lDocsView_TrungUongLienQuan
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
                        EffectStatusName = b.EffectStatusDesc
                    }).ToList();
            }
            return View(model);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult CustomerInterFaceTCVN()
        {
            int customerId = Extensions.GetCurrentCustomerId(), rowCount = 0, rowAmount = Constants.RowAmount10, pageIndex = 0;
            byte docGroupId = Constants.DocGroupIdTcvn, docTypeId = 61, isSearchExact = 0, effectStatusId = 0, displayTypeId = 0, getRowCount = 1;
            short organId = 0, fieldId = 0, signerId = 0;
            string orderBy = "", searchKeyword = "",docName = "",docIdentity = "",effectStatusType = "",searchByDate = "", dateFrom = "", dateTo = "";

            //DataSet dt = new CustomerFields().CustomerFields_GetCount(customerId, currentLanguageId, Constants.DocGroupIdTcvn, 1, 0);
            //DataTable datafield = CustomerFields.Static_GetCount_GetListField(dt);

            var model = new CusomersInterfaceModel
            {
                lDocsView_TCVN = DocsViewHelpers.CustomerDocs_CustomerFields(0, rowAmount, pageIndex, orderBy, currentLanguageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount),
                lDocsView_Newest = DocsViewHelpers.View_GetDocsViewNewest(LawsVnLanguages.GetCurrentLanguageId(), Constants.DocGroupIdTcvn, Constants.RowAmount5, 0, 0, 1, ref _rowCount1),
                //DataTableField = datafield,
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    FieldId = fieldId,
                    TotalPage = rowCount,
                    ShowNumberOfResults = rowAmount,
                    PageIndex = 0,
                    PageSize = rowAmount,
                    LinkLimit = rowAmount,
                    UrlPaging = string.Empty,
                    DocGroupId = DocGroups.TCVN,
                    LanguageId = currentLanguageId,
                    UsingDisplayTable = 0,
                    ClassTagItem = "tag-item item7",
                    ControllerName = "Ajax",
                    ActionName = "Customers_InterfaceTCVN",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "Content",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };

            //ToDo: Danh sách lĩnh vực quan tâm Tiêu chuẩn VN
            List<CustomerFields> lCustomerFieldsAll = new CustomerFields().GetListFieldsByCustomerId(customerId);
            if (lCustomerFieldsAll.HasValue())
            {
                model.ListFieldsQuanTamTcvn = (from cp in lCustomerFieldsAll
                    join p in model.ListFields on cp.FieldId equals p.FieldId where cp.DocGroupId == Constants.DocGroupIdTcvn
                    select new Fields
                    {
                        FieldId = p.FieldId,
                        FieldName = p.FieldName,
                        FieldDesc = p.FieldDesc
                    }).ToList();
            }

            if (model.lDocsView_TCVN.HasValue())
            {
                model.lDocsView_TCVN = model.lDocsView_TCVN.Join(model.ListEffectStatus, a => a.EffectStatusId,
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

            //ToDo: Lấy danh sách count lĩnh vực vb quan tâm
            if (model.DataTableField.DataTableExists())
            {
                model.ListFieldsQuanTam = (from DataRow dr in model.DataTableField.Rows
                    select new Fields
                    {
                        FieldId = Extensions.GetColumnValue<short>(dr, "FieldId"),
                        FieldName = Extensions.GetColumnValue<string>(dr, "FieldName"),
                        FieldDesc = Extensions.GetColumnValue<string>(dr, "FieldDesc"),
                        SoLuong = Extensions.GetColumnValue<int>(dr, "SoLuong")
                    }).ToList();
            }
            return View(model);
        }

        [SEOAction(13)]
        public ActionResult CustomerListDocsByField(short fieldId = 0, byte docGroupId = 0)
        {
            Customers mCustomers = new Customers();
            CustomerFields mCustomerFields = new CustomerFields();
            int CustomerId = Extensions.GetCurrentCustomerId();
            byte _languageId = LawsVnLanguages.GetCurrentLanguageId();
            byte _docGroupId = Constants.DocGroupIdVbpq;
            short _displayTypeId = Constants.FieldsDisplayTypeIdVbpq;
            int rowCount = 0, customerId = Extensions.GetCurrentCustomerId();
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
            List<CustomerFields> lCustomerFields = mCustomerFields.GetListFieldsByCustomerId(CustomerId);
            List<DocsView> lMyDocs = DocsViewHelpers.CustomerDocs_CustomerFields(0, Constants.RowAmount20, 0, orderBy, currentLanguageId, docGroupId, searchKeyword, isSearchExact, 0, docName, docIdentity, docTypeId, fieldId, 0, organId, signerId, 0, displayTypeId, customerId, 0, effectStatusId, effectStatusType, 0, 0, 0, searchByDate, dateFrom, dateTo, getRowCount, 0, 0, 0, 0, 0, 0, ref rowCount);
            //DataSet dt = mCustomerFields.CustomerFields_GetCount(CustomerId, currentLanguageId, docGroupId, 1, 0);
            //DataTable datafield = CustomerFields.Static_GetCount_GetListField(dt);
            List<Fields> lFieldDisplays = new List<Fields>();
            lFieldDisplays.Add(DropDownListHelpers.GetAllFields().FirstOrDefault(f => f.FieldId == fieldId));
            var docModel = new CustomerDocsViewModel
            {
                mArticlesViewMaster  = Extensions.GetArticlesViewMaster(),
                ListDocTypes = DropDownListHelpers.GetListDocTypesbyDisplayTypeId(Constants.DocTypeIdDisplayTypeIdVbpq),
                lMyDocs = lMyDocs,
                lCustomerFields = lCustomerFields,// Lĩnh vực văn bản quan tâm
                //DataTableField = datafield,
                ListFields = lFieldDisplays,
                PartialPaginationAjaxFirst = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    LanguageId = _languageId,
                    ControllerName = "Ajax",
                    ActionName = "Customers_InterfaceByField",
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
            if (docModel.ListFields.HasValue())
            {
                if (!string.IsNullOrEmpty(docModel.ListFields[0].FieldName))
                {
                    docModel.SeoReplace = docModel.ListFields[0].FieldName;
                }
            }
            return View(docModel);
        }
        #endregion giao dien ca nhan
    }
}
