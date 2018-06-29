using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.WebPages;
using LawsVN.Library.Sercurity;
using NLog;
using LawsVNEN.Controllers;
using LawsVNEN.Library;
using System.Collections.Generic;
using ICSoft.CMSLib;
using LawsVNEN.AppCode;
using LawsVNEN.Models.Account;

namespace LawsVNEN
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
            sms.utils.LogFiles.WriteLog(exception.ToString());
            sms.utils.Log.writeLog(exception.ToString(), "Application_Error");
            Response.Clear();
            if (exception != null)
            {
                var routeData = new RouteData();

                routeData.Values.Add("Controller", "Error");
                routeData.Values.Add("Exception", exception);
                routeData.Values.Add("ErrorMessage", httpException.ToString());
                routeData.Values.Add("HttpStatusCode", httpStatusCode);

                switch ((HttpStatusCode) httpStatusCode)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                        routeData.Values.Add("action", string.Format("Error{0}", httpStatusCode));
                        break;

                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }

                Server.ClearError();

                IController controller = new ErrorController();

                // TODO: Ghi Log
                _logger.Log(LogLevel.Error,exception.Message);

                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
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
                                Response.RedirectPermanent("/Error/Error404");
                            }
                        }

                    }
                }
            }
            string lang = null;
            HttpCookie langCookie = Request.Cookies["LawsVnENCulture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                lang = userLang != "" ? userLang : LawsVnLanguages.GetDefaultLanguage();
            }
            new LawsVnLanguages().SetLanguage(lang);  
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
                        Roles = serializeModel.Roles
                    };

                    HttpContext.Current.User = lawsVnUser;
                }
            }
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