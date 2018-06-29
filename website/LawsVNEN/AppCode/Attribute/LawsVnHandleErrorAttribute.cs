using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LawsVNEN.Models;

namespace LawsVNEN.Library
{
    /// <summary>
    /// Thuộc tính xử lý ngoại lệ lawsvn
    /// </summary>
    public class LawsVnHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            // sms.utils.LogFiles.WriteLog(filterContext.Exception.ToString());
            // TODO: ngoại lệ đã được xử lý
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            var httpException = new HttpException(null, filterContext.Exception);
            var httpStatusCode = httpException.GetHttpCode();

            switch ((HttpStatusCode)httpStatusCode)
            {
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    break;

                default:
                    return;
            }

            // TODO: type không phải Exception
            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // TODO: truy vấn AJAX : trả về lỗi dạng JSON
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new AjaxResult
                {
                    AllowGet = true,
                    Completed = false,
                    Message = filterContext.Exception.Message
                };
            }
            else
            {
                var controllerName = (String)filterContext.RouteData.Values["controller"];
                var actionName = (String)filterContext.RouteData.Values["action"];
                var model = new ViewModelBase
                {
                    InnerException = httpException,
                    ErrorMessage = filterContext.Exception.Message,
                    ControllerName = controllerName,
                    ActionName = actionName
                }; //new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = String.Format("~/Views/Error/Error{0}.cshtml", httpStatusCode),
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = httpStatusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            //base.OnException(filterContext);
        }
    }
}