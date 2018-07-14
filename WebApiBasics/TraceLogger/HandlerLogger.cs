using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using WebApiBasics.Models;
using System.Web.Http.ExceptionHandling;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;

namespace WebApiBasics.TraceLogger
{
    public class HandlerLogger : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var x = context.Request.GetActionDescriptor().ReturnType;

            var urun = new Product();
            urun.ProductId = 5;
            urun.ProductName = "DNM";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, urun);
            context.Result = new ResponseMessageResult(response);
            base.Handle(context);
        }
    }
}