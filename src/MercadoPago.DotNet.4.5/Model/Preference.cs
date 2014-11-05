using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Preference
    {
        private readonly List<Item> items = new List<Item>();

        [JsonProperty(PropertyName = "items")]
        public List<Item> Items
        {
            get { return this.items; }
        }

        [JsonProperty(PropertyName = "payer")]
        public Payer Payer { get; set; }

        [JsonProperty(PropertyName = "back_urls")]
        public BackUrls BackUrls { get; set; }

        [JsonProperty(PropertyName = "auto_return")]
        public string AutoReturn { get; set; }
    }
}