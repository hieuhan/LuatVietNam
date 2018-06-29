using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace LawsVNEN.Library
{
    /// <summary>
    /// Trả về kết quả validate modelstate
    /// </summary>
    public class ValidationResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (!actionContext.ModelState.IsValid)
            //{
            //    //actionContext.ModelState.Keys
            //    actionContext.Response = actionContext
            //        .Request
            //        .CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            //}
        }
    }
}