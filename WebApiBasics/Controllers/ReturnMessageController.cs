using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBasics.Models;

namespace WebApiBasics.Controllers
{
    public class ReturnMessageController : BaseApiController
    {
        [HttpGet]
        public Product returnProduct()
        {
            throw new Exception();
            var pi = base.returnData();
        }
    }
}
