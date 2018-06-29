using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawsVN.Models;
using LawsVN.Library;

namespace LawsVN.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            //var exception = RouteData.Values["Exception"];
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
            // TODO: thiết lập true để IIS7 không sử dụng trang báo lỗi riêng
            Response.TrySkipIisCustomErrors = true;
        }

        //
        // GET: /Error/

        [ActionName("Error403")]
        public ActionResult Error403()
        {
            return View();
        }

        [ActionName("Error404")]
        public ActionResult Error404()
        {
            var model = new ViewModelBase();
            return Extensions.GetViewMode("Error404", model);
        }

        [ActionName("Error500")]
        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult NotAuth()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
