using System.Net;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    internal class Response<T>
    {
        public HttpStatusCode Status { get; private set; }

        public T Payload { get; private set; }

        public Response(HttpStatusCode status, T payload)
        {
            this.Status = status;
            this.Payload = payload;
        }
    }
}