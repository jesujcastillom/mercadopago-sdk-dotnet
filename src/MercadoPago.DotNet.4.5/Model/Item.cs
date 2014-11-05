using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Item
    {
        [JsonProperty(PropertyName = "id", Order = 10)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title", Order = 20)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "currency_id", Order = 30)]
        public string CurrencyId { get; set; }

        [JsonProperty(PropertyName = "picture_url", Order = 40)]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "description", Order = 50)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category_id", Order = 60)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "quantity", Order = 70)]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "unit_price", Order = 80)]
        public decimal UnitPrice { get; set; }
    }
}