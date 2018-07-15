using CustomLog4netLibrary;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Results;
using API.Business;
using API.Models;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _service;
        private readonly ProductDomainService productDomainService;
        public ProductController(IProductService service)
        {
            this._service = service;
        }
        ///// <summary>
        ///// Get All Products in PayFlex
        ///// </summary>
        ///// <remarks>
        ///// ### REMARKS ###
        ///// Use this call and retrieve all Products
        ///// <code>
        /////     call this for XML : api/GetValues?type=XML
        /////     call this for JSON : api/GetValues?type=JSON
        ///// </code>
        ///// </remarks>
        ///// <returns></returns>
        ///// <response code="200">Successfull Operation</response>
        ////[ResponseType(typeof(Product))]
        ////[HttpGet]
        ////public IEnumerable<Product> getAllProducts()
        ////{//error handling yapılabilir.action filter yapılabilir.
        ////    var list = _service.getAllProducts();
        ////    return list;
        ////}
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
        public Product getProduct(int id)
        {
            var response = _service.getProduct(id);
            return response;
        }
        ///// <summary>
        ///// Simply Check Service.
        ///// </summary>
        ///// <response code="200">Service is Active!</response>
        ///// <remarks>Check is active or not ?</remarks>
        ///// <returns></returns>
        ////[HttpPost]
        ////public HttpResponseMessage CheckService()
        ////{
        ////    return Request.CreateResponse(HttpStatusCode.OK, "Service is Active!");
        ////}
    }
}
