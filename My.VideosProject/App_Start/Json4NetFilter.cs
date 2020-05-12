using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.VideosProject.App_Start
{
    public class Json4NetFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as JsonResult;
            if (result != null)
            {
                var jsonResult = result;
                var jsonNetResult = new Json4NetResult
                {
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior,
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    MaxJsonLength = jsonResult.MaxJsonLength,
                    RecursionLimit = jsonResult.RecursionLimit
                };
                filterContext.Result = jsonNetResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Json4NetResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");
                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("GetNotAllowed");
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;
                if (Data != null)
                    response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            }
        }
    }
}