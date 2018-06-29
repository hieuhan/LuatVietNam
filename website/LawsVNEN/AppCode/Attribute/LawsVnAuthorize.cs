using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LawsVN.Library.Sercurity;
using LawsVNEN.Models;
using LawsVNEN.App_GlobalResources;

namespace LawsVN.Library
{
    public class LawsVnAuthorize: AuthorizeAttribute
    {
        protected virtual LawsVnPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as LawsVnPrincipal; }
        }

        private const string DefaultViewNameUnAuth = "~/Views/Error/Error403.cshtml";
        private string _viewNameUnAuth;

        /// <summary>
        /// view thông báo khi ko có quyền
        /// </summary>
        public string ViewNameUnAuth
        {
            get
            {
                return string.IsNullOrWhiteSpace(_viewNameUnAuth)
                    ? DefaultViewNameUnAuth
                    : _viewNameUnAuth;
            }
            set { _viewNameUnAuth = value; }
        }

        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Hiện thông báo khi chưa login
        /// </summary>
        public bool ShowAuthView { get; set; }

        public ViewModelBase.MessageTypes MessageTypes { get; set; }

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

            if (!string.IsNullOrEmpty(Roles))
            {
                if (!CurrentUser.IsInRole(Roles))
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
                throw new InvalidOperationException(Resource.AuthorizeAttributeCannotUseWithinChildActionCache);
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
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Completed = false,
                        Message = HttpContext.Current.Request.Url.AbsolutePath.IndexOf("SubscriberByServicePackageParentId", StringComparison.Ordinal) > 0 ? "dichvu" : Resource.Unauthorized,
                        ReturnUrl = FormsAuthentication.LoginUrl
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var model = new ViewModelBase
                    {
                        NotAuthorize = true,
                        ErrorMessage = Resource.ResourceManager.GetString(ErrorMessage),
                        MessageType = MessageTypes,
                        HttpStatusCode = (Int32)HttpStatusCode.Unauthorized
                    };
                    filterContext.Result = new ViewResult
                    {
                        ViewName = ViewNameUnAuth,
                        ViewData = new ViewDataDictionary(model),
                        TempData = filterContext.Controller.TempData
                    };
                }
                else if (ShowAuthView)
                {
                    var model = new ViewModelBase
                    {
                        NotAuthenticate = true,
                        MessageType = MessageTypes,
                        ErrorMessage = Resource.ResourceManager.GetString(ErrorMessage),
                        HttpStatusCode = (Int32)HttpStatusCode.Unauthorized
                    };
                    filterContext.Result = new ViewResult
                    {
                        ViewName = ViewNameUnAuth,
                        ViewData = new ViewDataDictionary(model),
                        TempData = filterContext.Controller.TempData
                    };
                }
                else
                    base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}