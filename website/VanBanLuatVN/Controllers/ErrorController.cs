using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanBanLuat.Models;

namespace VanBanLuat.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var errorMessage = RouteData.Values["ErrorMessage"];
            var httpStatusCode = RouteData.Values["HttpStatusCode"];
            var model = new ViewModelBase();
            if (errorMessage != null)
            {
                model.ErrorMessage = (String)errorMessage;
            }

            if (httpStatusCode != null)
            {
                model.HttpStatusCode = Response.StatusCode = (Int32)httpStatusCode;
            }
            filterContext.Controller.ViewData.Model = model;
            Response.TrySkipIisCustomErrors = true;
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
