using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSoft.ViewLibV3;
using VanBanLuat.AppCode.Attribute;
using VanBanLuat.Librarys;
using VanBanLuatVN.Models;
namespace VanBanLuatVN.Controllers
{
    public class FeedBacksController : Controller
    {
        [SEOAction]
        [AllowAnonymous]
        public ActionResult Feedbacks()
        {
            var model = new FeedbacksViewModel();
            if (Extensions.IsAuthenticated)
            {
                model.FullName = Extensions.GetCurrentCustomerFullName() == null || Extensions.GetCurrentCustomerFullName() == "" ? Extensions.GetCurrentCustomerName() : Extensions.GetCurrentCustomerFullName();
                model.PhoneNumber = Extensions.GetCurrentCustomerMobile();
                model.Email = Extensions.GetCurrentCustomerMail();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult FeedBacks(FeedbacksViewModel model)
        {
            short sysMessageId = 0;
            int actUserId = 0;
            var mFeedBacks = new FeedBacks
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber.TrimmedOrDefault(string.Empty),
                Email = model.Email,
                Comment = model.Comment,
                SiteId = VanBanLuat.Librarys.Constants.SiteId,
                Title = string.Concat("Khách hàng ", model.FullName, " gửi liên hệ"),
                FeedBackGroupId =VanBanLuat.Librarys.Constants.FeedBackGroupId,
                FromIP = Request.UserHostAddress,
                ReviewStatusId=1,
                Address = string.Empty
            };
            mFeedBacks.InsertOrUpdate(0, actUserId, ref sysMessageId);
            if (mFeedBacks.FeedBackId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Cảm ơn quý khách đã gửi liên hệ. Chúng tôi sẽ liên lạc với quý khách trong thời gian sớm nhất.",
                    ReturnUrl = VanBanLuat.Librarys.Constants.ROOT_PATH,
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = false,
                Message = "Quý khách vui lòng thử lại sau",
                Completed = false
            };
        }

    }
}
