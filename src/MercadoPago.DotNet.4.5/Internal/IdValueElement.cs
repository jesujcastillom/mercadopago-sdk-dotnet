using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public abstract class IdValueElement
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string DisplayName { get; set; }

        // Prevent construction outside this assembly
        internal IdValueElement()
        {
            
        }
    }
}