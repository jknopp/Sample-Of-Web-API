using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebApiBasics.Attributes;
using WebApiBasics.Business;
using WebApiBasics.Models;

namespace WebApiBasics.Controllers
{
    [Logging]
    [Exception]
    public class ProductController : ApiController
    {
        private readonly IProductService _service;
        private readonly ProductDomainService productDomainService;
        private readonly double x;
        public ProductController(IProductService service, ProductDomainService productService)
        {
            this._service = service;
            this.productDomainService = productService;
            x = productDomainService.CreateProductId();
        }
        /// <summary>
        /// Get All Products in PayFlex
        /// </summary>
        /// <remarks>
        /// ### REMARKS ###
        /// Use this call and retrieve all Products
        /// <code>
        ///     call this for XML : api/GetValues?type=XML
        ///     call this for JSON : api/GetValues?type=JSON
        /// </code>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Successfull Operation</response>
        [ResponseType(typeof(Product))]

        [HttpGet]
        public HttpResponseMessage getAllProducts()
        {//error handling yapılabilir.action filter yapılabilir.
            var list = _service.getAllProducts();
            return this.Request.CreateResponse(HttpStatusCode.OK, list);

        }
        /// <summary>
        /// Get Unique Product With Unique Id
        /// </summary>
        /// <remarks>
        /// ### REMARKS ###
        /// Use this call and retrieve a Product with Unique Id.
        /// <code>
        ///     call this for XML : api/GetValues/{Id}?type=XML
        ///     call this for JSON : api/GetValues/{Id}?type=JSON
        /// </code>
        /// </remarks>
        /// <param name="id">Unique Product Id</param>
        /// <response code="404">Not Result of any Product</response>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(NotFoundResult))]
        [SwaggerResponse(HttpStatusCode.OK, "Product", typeof(Product))]
        public HttpResponseMessage getProduct(int id)
        {
            var y = x;
            var response = _service.getProduct(id);
            if (response == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        /// <summary>
        /// Simply Check Service.
        /// </summary>
        /// <response code="200">Service is Active!</response>
        /// <remarks>Check is active or not ?</remarks>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CheckService()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Service is Active!");
        }
    }
}
