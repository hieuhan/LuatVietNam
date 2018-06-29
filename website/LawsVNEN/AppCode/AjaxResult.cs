using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Library
{
    /// <summary>
    /// Định nghĩa kết quả trả về từ 1 hàm gọi Ajax
    /// </summary>
    public partial class AjaxResult : ActionResult
    {
        public AjaxResult()
        {
            this.StatusCode = 200;
        }

        public int StatusCode { get; set; }

        public object Data { get; set; }

        public bool Completed { get; set; }

        public string Message { get; set; }

        public bool AllowGet { get; set; }

        public string ReturnUrl { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (!this.AllowGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(Resource.JsonRequest_GetNotAllowed);

            var responseData = new
            {
                Completed = this.Completed,
                Message = this.Message,
                Data = this.Data,
                ReturnUrl = this.ReturnUrl
            };
            HttpResponseBase response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/json";
            response.StatusCode = this.StatusCode;
            response.Write(200 != response.StatusCode ? "{}" : JsonConvert.SerializeObject(responseData));
        }

    }
}