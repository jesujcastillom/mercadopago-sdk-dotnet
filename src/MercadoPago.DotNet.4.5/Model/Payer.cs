using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MercadoPago.DotNet
{
    public class Payer
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public Phone Phone { get; set; }

        [JsonProperty(PropertyName = "identification")]
        public Identification Identification { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}