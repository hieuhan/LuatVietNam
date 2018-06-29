using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.WebPages;
using VanBanLuat.Controllers;
using VanBanLuat.Librarys;
using VanBanLuatVN.Library.Sercurity;

namespace LawsVN
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Context.Response.Clear();
            Context.ClearError();
            var httpException = exception as HttpException ?? new HttpException((Int32)HttpStatusCode.InternalServerError, "Internal Server Error", exception);
            var httpStatusCode = httpException.GetHttpCode();
            ICSoft.ViewLibV3.LogUtil.WriteLog(exception.ToString(), "Application_Error");
            if (exception != null)
            {
                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "Index";
                routeData.Values.Add("Exception", exception);
                routeData.Values.Add("ErrorMessage", httpException.ToString());
                routeData.Values.Add("HttpStatusCode", httpStatusCode);

                Server.ClearError();
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (authTicket != null)
                {
                    AccountSerializerModel serializeModel = serializer.Deserialize<AccountSerializerModel>(authTicket.UserData);
                    VbLuatPrincipal vbLuatUser = new VbLuatPrincipal(authTicket.Name)
                    {
                        CustomerId = serializeModel.CustomerId,
                        CustomerName = serializeModel.CustomerName,
                        CustomerFullName = serializeModel.CustomerFullName,
                        CustomerMail = serializeModel.CustomerMail,
                        CustomerMobile = serializeModel.CustomerMobile,
                        OpenId = serializeModel.OpenId
                    };
                    HttpContext.Current.User = vbLuatUser;
                }
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            bool isMobile = context.Request.Browser.IsMobileDevice,
                isAuth = Extensions.IsAuthenticated;
            string currentCustomerId = Extensions.GetCurrentCustomerId().ToString(),
                varyByCustom = string.Concat(isMobile,"-", isAuth, "-", currentCustomerId, "-", context.Request.FilePath);
            return varyByCustom;
        }

    }
}