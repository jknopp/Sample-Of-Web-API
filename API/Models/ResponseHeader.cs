using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ResponseHeader
    {
        /// <summary>
        /// Response Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Response Code
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// Response Message
        /// </summary>
        public string Message { get; set; }
    }
}