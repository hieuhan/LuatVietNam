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
using LawsVN.Library;
using System.Web.WebPages;
using ICSoft.CMSLib;
using LawsVN.Library.Sercurity;
using LawsVN.Models.Account;
using NLog;

namespace LawsVN
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException ?? new HttpException((Int32)HttpStatusCode.InternalServerError, "Internal Server Error", exception);
            var httpStatusCode = httpException.GetHttpCode();
            sms.utils.LogFiles.WriteLog(exception.ToString() + "\nApplication_Error: " + Request.RawUrl + (Request.UrlReferrer != null ? "\nUrlReferrer: " + Request.UrlReferrer.AbsoluteUri : ""));
            Response.Clear();
            if (exception != null)
            {
                var routeData = new RouteData();

                routeData.Values.Add("Controller", "Error");
                routeData.Values.Add("Exception", exception);
                routeData.Values.Add("ErrorMessage", httpException.ToString());
                routeData.Values.Add("HttpStatusCode", httpStatusCode);

                switch ((HttpStatusCode)httpStatusCode)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                        routeData.Values.Add("action", string.Format("Error{0}", httpStatusCode));
                        break;

                    case HttpStatusCode.Continue:
                        break;
                    case HttpStatusCode.SwitchingProtocols:
                        break;
                    case HttpStatusCode.OK:
                        break;
                    case HttpStatusCode.Created:
                        break;
                    case HttpStatusCode.Accepted:
                        break;
                    case HttpStatusCode.NonAuthoritativeInformation:
                        break;
                    case HttpStatusCode.NoContent:
                        break;
                    case HttpStatusCode.ResetContent:
                        break;
                    case HttpStatusCode.PartialContent:
                        break;
                    case HttpStatusCode.MultipleChoices:
                        break;
                    case HttpStatusCode.MovedPermanently:
                        break;
                    case HttpStatusCode.Found:
                        break;
                    case HttpStatusCode.SeeOther:
                        break;
                    case HttpStatusCode.NotModified:
                        break;
                    case HttpStatusCode.UseProxy:
                        break;
                    case HttpStatusCode.Unused:
                        break;
                    case HttpStatusCode.TemporaryRedirect:
                        break;
                    case HttpStatusCode.BadRequest:
                        break;
                    case HttpStatusCode.Unauthorized:
                        break;
                    case HttpStatusCode.PaymentRequired:
                        break;
                    case HttpStatusCode.MethodNotAllowed:
                        break;
                    case HttpStatusCode.NotAcceptable:
                        break;
                    case HttpStatusCode.ProxyAuthenticationRequired:
                        break;
                    case HttpStatusCode.RequestTimeout:
                        break;
                    case HttpStatusCode.Conflict:
                        break;
                    case HttpStatusCode.Gone:
                        break;
                    case HttpStatusCode.LengthRequired:
                        break;
                    case HttpStatusCode.PreconditionFailed:
                        break;
                    case HttpStatusCode.RequestEntityTooLarge:
                        break;
                    case HttpStatusCode.RequestUriTooLong:
                        break;
                    case HttpStatusCode.UnsupportedMediaType:
                        break;
                    case HttpStatusCode.RequestedRangeNotSatisfiable:
                        break;
                    case HttpStatusCode.ExpectationFailed:
                        break;
                    case HttpStatusCode.NotImplemented:
                        break;
                    case HttpStatusCode.BadGateway:
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        break;
                    case HttpStatusCode.GatewayTimeout:
                        break;
                    case HttpStatusCode.HttpVersionNotSupported:
                        break;
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }

                Server.ClearError();

                IController controller = new Controllers.ErrorController();

                // TODO: Ghi Log
                _logger.Log(LogLevel.Error, exception.Message);

                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // chi tiet van ban
            if (Request.RawUrl.EndsWith("default.aspx"))// not to route
            {
                string OldFullUrl = Request.RawUrl;
                if (OldFullUrl.Contains("/VL/"))
                {
                    OldFullUrl = OldFullUrl.Substring(OldFullUrl.IndexOf("/VL/"));
                }
                string[] l_OldParam = OldFullUrl.Split('/');
                if (l_OldParam.Length > 4)
                {
                    if (l_OldParam[1] == "VL")
                    {
                        if ("659,661,662,667,669".Contains(l_OldParam[2]))
                        {
                            ICSoft.LawDocsLib.Docs m_Docs = new ICSoft.LawDocsLib.Docs();
                            List<ICSoft.LawDocsLib.Docs> l_Docs = m_Docs.GetListByDocGUId(l_OldParam[4], 1);
                            if (l_Docs.Count > 0)
                            {
                                m_Docs = l_Docs[0];
                            }
                            if (m_Docs.DocId > 0)
                            {
                                switch (l_OldParam[2])//tabid
                                {
                                    case "659"://lien quan
                                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "lienquan"));
                                        break;
                                    case "661"://hieu luc
                                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "hieuluc"));
                                        break;
                                    case "662"://noi dung
                                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "noidung"));
                                        break;
                                    case "667"://tai ve
                                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "taive"));
                                        break;
                                    case "669"://tom tat
                                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "tomtat"));
                                        break;
                                }
                            }
                            else
                            {
                                Response.Redirect("/Error/Error404");
                            }
                        }

                    }
                }
            }
            else if (Request.RawUrl.EndsWith("docsproperties.aspx"))// not to route
            {
                string[] l_OldParam = Request.RawUrl.Split('/');
                int ParamLength = l_OldParam.Length;
                if (ParamLength > 1)
                {
                    ICSoft.LawDocsLib.Docs m_Docs = new ICSoft.LawDocsLib.Docs();
                    List<ICSoft.LawDocsLib.Docs> l_Docs = m_Docs.GetListByDocGUId(l_OldParam[ParamLength - 2], 1);
                    if (l_Docs.Count > 0)
                    {
                        m_Docs = l_Docs[0];
                    }
                    if (m_Docs.DocId > 0)
                    {
                        Response.RedirectPermanent(ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl, "tomtat"));
                    }
                    else
                    {
                        Response.Redirect("/Error/Error404");
                    }
                }
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
                    LawsVnPrincipal lawsVnUser = new LawsVnPrincipal(authTicket.Name)
                    {
                        CustomerId = serializeModel.CustomerId,
                        CustomerName = serializeModel.CustomerName,
                        CustomerFullName = serializeModel.CustomerFullName,
                        CustomerMail = serializeModel.CustomerMail,
                        CustomerMobile = serializeModel.CustomerMobile,
                        Avatar = serializeModel.Avatar,
                        GenderId = serializeModel.GenderId,
                        Roles = serializeModel.Roles
                    };
                    HttpContext.Current.User = lawsVnUser;
                }
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            
            bool isMobile = Extensions.IsMobile();
            bool isAuthen = Extensions.IsAuthenticated;
            string curentCustomer = Extensions.GetCurrentCustomerId().ToString();
            string RetVal = isMobile.ToString() + "-" + isAuthen.ToString() + "-" + curentCustomer + "-" + context.Request.FilePath;
            sms.utils.Log.writeLog(RetVal, "GetVaryByCustomString: " + Request.RawUrl);
            return RetVal;
        }

        //void Application_AcquireRequestState(object sender, EventArgs e)
        //{
            //var sessionId = Extensions.GetCurrentSessionId();
            //if (string.IsNullOrWhiteSpace(sessionId)) return;
            //var s = Session["_LuatVietNamSessionId"] != null ? Session["_LuatVietNamSessionId"].ToString() : string.Empty;
            //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie != null)
            //{
            //    if (!string.IsNullOrEmpty(s) && !sessionId.Equals(s))
            //    {
            //        Logout();
            //    }
            //}
            //if (authCookie == null)
            //{
            //    if (!string.IsNullOrEmpty(s) && sessionId.Equals(s))
            //    {
            //        ClearSession();
            //    }
            //}
        //}

        private void Logout()
        {
            FormsAuthentication.SignOut();
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty) { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(authCookie);
            Response.Redirect(CmsConstants.ROOT_PATH);
        }

        private void ClearSession()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            var sessionCookie = new HttpCookie("ASP.NET_SessionId", string.Empty) { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(sessionCookie);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("_LuatVietNamSessionId", string.Empty);
        }
    }
}