using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Business
{
    public class ProductDomainService : IDomainService
    {
        public double CreateProductId()
        {
            var id = Math.PI;
            return id;
        }
    }
}