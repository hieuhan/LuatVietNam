using System;
using System.Linq;
using System.Web.Mvc;
using LawsVN.Models;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using System.Web.Mvc.Ajax;
using LawsVN.App_GlobalResources;

namespace LawsVN.Controllers
{
    public class LawersController : Controller
    {
        //
        // GET: /Lawers/

        public ActionResult Index(short provinceId = 0)
        {
            int rowCount = 0;
            byte lawerGroupId = 2, isGetMostView = 1, isCountProvince = 1;
            short fieldId = 0;
            var lawersView = new LawersView().GetPageView(0, string.Empty, string.Empty, lawerGroupId, fieldId, provinceId, 0, 0, Constants.RowAmount20, 0, string.Empty, string.Empty, string.Empty, isCountProvince, isGetMostView, ref rowCount);

            var model = new LawerViewModel
            {
                mLawersView = lawersView,
                ListProvinces = lawersView.lProvinces,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    ProvinceId = provinceId,
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                    ControllerName = "Ajax",
                    ActionName = "Lawer_GetView",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListByField",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            model.ListProvinceCount = lawersView.lProvinces.Join(model.ListAllProvinces, a => a.ProvinceId,
                b => b.ProvinceId, (a, b) => new ProvinceCountModel
                {
                    Count = a.SoLuong,
                    ProvinceId = a.ProvinceId,
                    ProvinceName = b.ProvinceDesc
                });
            return View(model);
        }

        public ActionResult LawersDetail(int lawerId = 11)
        {
            int rowCount = 0;
            short fieldId = 0;
            LawerViewDetailModel model = null;
            if (lawerId > 0)
            {
                var lawerViewDetail = new LawersViewDetail().GetViewDetail(lawerId, ref rowCount);
                model = new LawerViewDetailModel
                {
                    mLawersViewDetail = lawerViewDetail,
                    mLawersView = new LawersView().GetPageView(0, "", "", 2, fieldId, 0, 0, 0, 0, 0, "",
                    "", "", 1, 1, ref rowCount),
                    lFaqs =
                        new Faqs().Search(0, "", "", 0, "", 0, 3, 0, lawerId, 0, 2, 0, "", "", ref rowCount),
                    PartialPaginationAjax = new PartialPaginationAjax
                    {
                        ProvinceId = (short)lawerViewDetail.mLawersDetail.ProviceId,
                        FieldId = lawerViewDetail.mLawersDetail.FieldId,
                        TotalPage = rowCount,
                        PageSize = Constants.RowAmount4,
                        LinkLimit = Constants.RowAmount4,
                        ShowNumberOfResults = Constants.RowAmount4,
                        UrlPaging = string.Empty,
                        LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                        ControllerName = "Ajax",
                        ActionName = "Lawer_GetView",
                        LawsAjaxOptions = new AjaxOptions
                        {
                            UpdateTargetId = "ListByField",
                            InsertionMode = InsertionMode.Replace
                        }
                    }
                };
                model.LawProvinces =
                    model.ListAllProvinces.FirstOrDefault(m => m.ProvinceId == model.mLawersViewDetail.mLawersDetail.ProviceId);
            }
            else return RedirectToAction("Index", "Home");
            return View(model);
        }

        public ActionResult Search(string Keyword = "", string StartName = "", short FieldId = 0, short ProvinceId = 0, int DistrictId = 0, int WardId = 0, int RowAmount = 10, int Page = 0)
        {
            int RowCount = 0;
            byte LawerGroupId = 2, IsGetMostView = 1, IsCountProvince = 1;

            LawersView mLawersView = new LawersView();
            LawerViewModel mLawerViewModel = new LawerViewModel();
            mLawerViewModel.mLawersView = mLawersView.GetPageView(0, Keyword, StartName, LawerGroupId, FieldId, ProvinceId, DistrictId, WardId, RowAmount, Page > 0 ? Page - 1 : Page, "", "", "", IsCountProvince, IsGetMostView, ref RowCount);
            PartialPaginationAjax mPartialPaginationAjax = new PartialPaginationAjax
            {
                Keywords = Keyword,
                FieldId = FieldId,
                ProvinceId = ProvinceId,
                DistrictId = DistrictId,
                WardId = WardId,
                TotalPage = RowCount,
                PageIndex = 0,
                PageSize = RowAmount,
                LinkLimit = RowAmount,
                ShowNumberOfResults = RowAmount,
                UrlPaging = string.Empty,
                LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                ControllerName = "Ajax",
                ActionName = "Lawer_GetViewSearch",
                LawsAjaxOptions = new AjaxOptions
                {
                    UpdateTargetId = "ListByField",
                    InsertionMode = InsertionMode.Replace
                }

            };
            mLawerViewModel.PartialPaginationAjax = mPartialPaginationAjax;
            return View(mLawerViewModel);
        }

        [LawsVnAuthorize]
        public ActionResult Insert()
        {
            var model = new LawerInsertModel
            {
                WebsiteTitle = "Thêm luật sư"
            };
            return View(model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult Insert(LawerInsertModel model)
        {
            if (!model.LawerInsertCaptcha.Equals(model.LawerInsertCaptchaCode))
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
            byte replicated = 0; int actUserId = 0;
            short sysMessageId = 0;
            var lawer = new Lawers
            {
                FullName = model.FullName,
                ImagePath = model.Avatar.TrimmedOrDefault(string.Empty),
                Address = model.Address.TrimmedOrDefault(string.Empty),
                ProviceId = model.ProvinceId,
                DistricId = 0,
                WardId = 0,
                PhoneNumber = model.PhoneNumber,
                Mobile = model.Mobile,
                Email = model.Email,
                Website = string.Empty,
                LawOfficeName = model.LawOfficeName,
                Experience = model.Experience,
                Education = model.Education,
                ReviewStatusId = 1,
                LawerGroupId = 0,
                Content = model.Content,
                CrUserId = 0
            };
            lawer.Insert(replicated,actUserId,ref sysMessageId);
            if (lawer.LawerID > 0)
            {
                //Lĩnh vực văn bản quan tâm
                if (model.FieldId != null && model.FieldId.Length > 0)
                {
                    new LawerFields
                    {
                        LawerId = lawer.LawerID
                    }.InsertByListId(String.Join(",", model.FieldId));
                }

                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Thêm mới luật sư thành công.",
                    Completed = true,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "danh-ba-luat-su.html")
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
        public AjaxResult LawerQuestion(LawerQuestionModel model)
        {
            short sysMessageId = 0;
            int actUserId = 0;
            if (model.Captcha != model.CaptchaCode)
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

            if (model.LawerId > 0)
            {
                var mFaq = new Faqs
                {
                    Question = model.Question,
                    LawerId = model.LawerId,
                    Answer = string.Empty,
                    FaqCode = string.Empty,
                    FaqGroupId = 3,
                    FaqTypeId = 0,
                    FieldId = 0,
                    LanguageId = 1,
                    ReviewStatusId = 1
                };
                mFaq.InsertOrUpdate(0, actUserId, ref sysMessageId);
                if (mFaq.FaqId > 0)
                {
                    return new AjaxResult
                    {
                        StatusCode = 200,
                        AllowGet = true,
                        Message = "Cảm ơn quý khách đã câu hỏi cho luật sư.Chúng tôi sẽ liên lạc và có câu trả lời sớm nhất!",
                        Completed = true
                    };
                }
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = false,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

    }
}
