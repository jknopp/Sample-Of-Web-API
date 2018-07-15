using CustomLog4netLibrary;
using API.Models;
using System.Web.Http.ExceptionHandling;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;
using System;

namespace API.TraceLogger
{
    public class HandlerLogger : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        private readonly ILogger _logger;
        public HandlerLogger(ILogger logger)
        {
            _logger = logger;
        }
        public override void Handle(ExceptionHandlerContext context)
        {
            ResponseHeader header;
            var ReturnType = context.Request.GetActionDescriptor().ReturnType;
            header = Activator.CreateInstance(ReturnType) as ResponseHeader;
            header.Message = context.Exception.Message;
            header.ResponseCode = "0001";
            header.Status = 1;
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, header);
            context.Result = new ResponseMessageResult(response);
            base.Handle(context);
        }
    }
}