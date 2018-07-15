using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Business
{
    public interface IProductService
    {
        IEnumerable<Product> getAllProducts();
        Product getProduct(int id);
    }
}
