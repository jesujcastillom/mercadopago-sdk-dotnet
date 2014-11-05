using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class BackUrls
    {
        [JsonProperty(PropertyName = "success")]
        public string Success { get; set; }

        [JsonProperty(PropertyName = "failure")]
        public string Failure { get; set; }

        [JsonProperty(PropertyName = "pending")]
        public string Pending { get; set; }
    }
}