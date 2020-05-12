using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static My.Videos.Common.Utils.LogUtil;

namespace My.VideosProject.App_Start
{
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WebUiHandleErrorAttribute());
            filters.Add(new Json4NetFilter());
        }
    }

    /// <summary>
    /// 异常过虑
    /// </summary>
    public class WebUiHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebUiHandleErrorAttribute));

        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">操作筛选器上下文。</param>
        public override void OnException(ExceptionContext filterContext)
        {
            var messageBody = new
            {
                Action = filterContext.Controller.ControllerContext.RouteData.Values,
                QueryString = ToConnectionString(filterContext.RequestContext.HttpContext.Request.QueryString),
                Form = ToConnectionString(filterContext.RequestContext.HttpContext.Request.Form),
            };
            var message = new LogMessage { MessageKey = "异常拦截", MessageBody = messageBody, Exception = filterContext.Exception };
            Log.Error(message.ToString());
            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.HttpContext.Response.Headers.Add("StatusCode", "500");
            // ReSharper disable once AssignNullToNotNullAttribute
            if (filterContext.RequestContext.HttpContext.Request.AcceptTypes.Any(o => o != null && o.ToLower().Contains("json")))
                filterContext.HttpContext.Response.Write("出现错误了,日志id为:</br>" + message.LogId);
            else
                filterContext.HttpContext.Response.Redirect("/Home/Error?logId=" + message.LogId);
            filterContext.HttpContext.Response.End();
        }

        private static string ToConnectionString(NameValueCollection source, string ckv = "=", string cr = "&")
        {
            if (source == null) return string.Empty;
            var rows = source.AllKeys.Select(o => string.Format("{0}{1}{2}", o, ckv, source[o]));
            return string.Join(cr, rows);
        }
    }
}