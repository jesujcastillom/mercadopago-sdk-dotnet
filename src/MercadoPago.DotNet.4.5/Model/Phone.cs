using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Phone
    {
        [JsonProperty(PropertyName = "area_code")]
        public string AreaCode { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }
    }
}