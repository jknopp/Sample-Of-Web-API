using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApiBasics.Business;
using WebApiBasics.Models;

namespace WebApiBasics.Controllers
{
    public class GetValuesController : ApiController
    {
        private readonly IProductApplicationService _service;
        public GetValuesController(IProductApplicationService service)
        {
            this._service = service;
        }
        public IEnumerable<Product> getAllProducts()
        {
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
