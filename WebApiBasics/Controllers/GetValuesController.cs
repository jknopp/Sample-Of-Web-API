using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApiBasics.Attributes;
using WebApiBasics.Business;
using WebApiBasics.Models;

namespace WebApiBasics.Controllers
{
    [Logging]
    public class GetValuesController : ApiController
    {
        private readonly IProductService _service;
        public GetValuesController(IProductService service)
        {
            this._service = service;
        }
        public IEnumerable<Product> getAllProducts()
        {//error handling yapılabilir.action filter yapılabilir.
            return _service.getAllProducts();
        }
        public HttpResponseMessage getProduct(int id)
        {
            var response = _service.getProduct(id);
            if (response == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
