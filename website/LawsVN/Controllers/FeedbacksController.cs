using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawsVN.Models;
using ICSoft.CMSLib;
using LawsVN.Library;
using ICSoft.CMSViewLib;
using LawsVN.AppCode.Attribute;
using LawsVN.App_GlobalResources;

namespace LawsVN.Controllers
{
    public class FeedbacksController : Controller
    {
        [AllowAnonymous]
        [SEOAction]
        public ActionResult Feedbacks()
        {
            var model = new FeedbacksViewModel
            {
                MenuItemId = 690
            };
            if (Extensions.IsAuthenticated)
            {
                model.FullName = Extensions.GetCurrentCustomerFullName();
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
            var mFeedBacks = new FeedBacks
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber.TrimmedOrDefault(string.Empty),
                Email = model.Email,
                Comment = model.Comment,
                SiteId = Constants.SiteId,
                Title = string.Concat("Khách hàng " , model.FullName , " gửi liên hệ"),
                FeedBackGroupId = Constants.FeedBackGroupId,
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
                    Message = "Cảm ơn quý khách đã gửi liên hệ. Chúng tôi sẽ liên lạc với quý khách trong thời gian sớm nhất.",
                    ReturnUrl = CmsConstants.ROOT_PATH,
                    Completed = true
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = false,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }
        
        public ActionResult CaptchaImage()
        {
            string prefix = "lawvn";
            //image stream 
            FileContentResult image = null;
            var mMemoryStream = Extensions.ImageStreamCaptcha(prefix);
            
            image = this.File(mMemoryStream.GetBuffer(), "image/Jpeg");
            return image;
        }

    }
}
