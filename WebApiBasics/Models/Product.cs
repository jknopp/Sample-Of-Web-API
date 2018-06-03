using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBasics.Models
{
    /// <summary>
    /// Product class 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }
    }
}