using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VanBanLuat.Librarys;
using VanBanLuatVN.Library.Sercurity;

namespace VanBanLuatVN.AppCode.Attributes
{
    /// <summary>
    /// Filter kiểm tra người dùng đăng nhập
    /// </summary>
    public class VbLuatAuthorize : AuthorizeAttribute
    {
        protected virtual VbLuatPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as VbLuatPrincipal; }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(Users))
            {
                if (!Users.Contains(CurrentUser.CustomerId.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                throw new InvalidOperationException("AuthorizeAttribute không áp dụng trong Cache ChildAction");
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                IsUserAuthorized(filterContext);
            }
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            //if (filterContext.Result == null)
            //    return;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Completed = false,
                        Message = "Rất tiếc. Quý khách không xem được nội dung này. Vui lòng đăng nhập tài khoản hoặc liên hệ với quản trị website để biết thêm chi tiết.",
                        ReturnUrl = filterContext.HttpContext.Request.UrlReferrer != null && filterContext.HttpContext.Request.UrlReferrer.PathAndQuery.IndexOf("user/dang-nhap.html", StringComparison.Ordinal) < 0 ? string.Format("{0}?ReturnUrl={1}",FormsAuthentication.LoginUrl, filterContext.HttpContext.Request.UrlReferrer.PathAndQuery)  : string.Concat(FormsAuthentication.LoginUrl,"?ReturnUrl=/")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            else base.HandleUnauthorizedRequest(filterContext);
        }
    }
}