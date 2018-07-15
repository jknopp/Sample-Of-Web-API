using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;

namespace API.Business
{
    public class ProductService : IProductService
    {
        public Product[] products { get; set; }
        public ProductService()
        {
            this.products = new Product[]
            {
                new Product {ProductId=1,ProductName="MFA"},
                new Product {ProductId=2,ProductName="DNM" }
            };
        }

        public IEnumerable<Product> getAllProducts()
        {
            return products;
        }

        public Product getProduct(int id)
        {
            var product = products.Where(q => q.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return null;
            }
            return product;
        }
    }
}