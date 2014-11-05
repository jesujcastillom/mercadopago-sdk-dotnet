using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Identification
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }
    }
}