using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBasics.Models;

namespace WebApiBasics.Business
{
    public interface IProductApplicationService
    {
        IEnumerable<Product> getAllProducts();
        Product getProduct(int id);
    }
}
