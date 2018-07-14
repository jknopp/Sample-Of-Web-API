using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApiBasics.Controllers
{
    public class BaseApiController : ApiController
    {
        protected double returnData()
        {
            return Math.PI;
        }
    }
}