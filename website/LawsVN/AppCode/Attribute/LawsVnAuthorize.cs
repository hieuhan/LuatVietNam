using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LawsVN.App_GlobalResources;
using LawsVN.Library.Sercurity;
using LawsVN.Models;
using LawsVN.Models.Docs;

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
            object languageId;
            filterContext.RouteData.Values.TryGetValue("languageId", out languageId);
            //TODO Gói tra cứu Tiếng Anh : Không xem được thông tin VB tiếng Việt
            if (languageId != null && languageId.ToString().Equals("0"))
            {
                if (Extensions.IsAuthenticated && !CurrentUser.Roles.Any("NVNC,NC".Contains) && CurrentUser.Roles.Any("NVTA,TA".Contains))
                {
                    IsUserAuthorized(filterContext);
                }
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
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Completed = false,
                        //gọi hàm kiểm tra dịch vụ : khi chưa login -> show thông báo text
                        Message = HttpContext.Current.Request.Url.AbsolutePath.IndexOf("KiemTraDvDangKy", StringComparison.Ordinal) > 0 ? "dichvu" : Resource.Unauthorized,
                        ReturnUrl = FormsAuthentication.LoginUrl
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //TODO Đã login, chưa có quyền
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var model = new ViewModelBase
                    {
                        NotAuthorize = true,
                        ErrorMessage = ErrorMessage,
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
                else if (ShowAuthView) //TODO chưa login : trả về view thông báo
                {
                    var model = new ViewModelBase
                    {
                        NotAuthenticate = true,
                        MessageType = MessageTypes,
                        ErrorMessage = ErrorMessage,
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