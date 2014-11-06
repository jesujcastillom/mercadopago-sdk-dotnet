using System;
using System.Net;

namespace MercadoPago.DotNet
{
    public class RESTException : Exception
    {
        public string RawData { get; private set; }

        public int HTTPStatusCode { get; private set; }

        public string HTTPStatusCodeDescription { get; private set; }

        internal RESTException(string rawData, HttpStatusCode statusCode, string statusCodeDescription)
        {
            this.RawData = rawData;
            this.HTTPStatusCode = (int) statusCode;
            this.HTTPStatusCodeDescription = HTTPStatusCodeDescription;
        }
    }
}