using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Address
    {
        [JsonProperty(PropertyName = "street_name")]
        public string StreetName { get; set; }

        [JsonProperty(PropertyName = "street_number")]
        public string StreetNumber { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "floor")]
        public int? Floor { get; set; }

        [JsonProperty(PropertyName = "apartment")]
        public string Apartment { get; set; }
    }
}