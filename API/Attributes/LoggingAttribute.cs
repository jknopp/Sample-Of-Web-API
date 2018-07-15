using CustomLog4netLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace API.Attributes
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private string _Guid;
        public LoggingAttribute(ILogger logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string jSonData = actionExecutedContext.Response?.Content.ReadAsStringAsync().Result;
            this.logger.Log("Track Id : " + _Guid + " - OutGoing Response {" + jSonData + "}", LogType.Debug);
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string jSonData = actionContext.Request.Content?.ReadAsStringAsync().Result;
            _Guid = Guid.NewGuid().ToString();
            actionContext.Request.Headers.Add("TrackId", _Guid);
            this.logger.Log("Track Id : " + _Guid + " - " + actionContext.Request.RequestUri.LocalPath + " - Incoming Request {" + jSonData + "}", LogType.Debug);
            base.OnActionExecuting(actionContext);
        }
    }
}