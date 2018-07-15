using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using API.Business;
using API.Models;

namespace APITests
{
    [TestClass]
    public class TestProductController
    {
        [TestMethod]
        public void getProduct_ReturnEmptyProduct()
        {
            var service = new ProductService();
            var controller = new ProductController(service);

            var result = controller.getProduct(9999);

            Assert.IsNull(result);
        }
        [TestMethod]
        public void getProduct_ReturnFirstProduct()
        {
            var service = new ProductService();
            var controller = new ProductController(service);

            Product sample = new Product()
            {
                ProductId = 1,
                ProductName = "MFA",
                Status = 0,
                ResponseCode = null,
                Message = null
            };
            var result = controller.getProduct(1);

            Assert.AreEqual(sample.ProductName, result.ProductName);

        }
    }
}
