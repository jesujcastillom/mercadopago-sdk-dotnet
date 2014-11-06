using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Preference
    {
        private readonly List<Item> items = new List<Item>();

        [JsonProperty(PropertyName = "collector_id")]
        public string CollectorId { get; set; }

        [JsonProperty(PropertyName = "operation_type")]
        public string OperationType { get; set; }

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

        [JsonProperty(PropertyName = "payment_methods")]
        public PreferencePaymentMethod PaymentMethods { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public int? ClientId { get; set; }

        [JsonProperty(PropertyName = "marketplace")]
        public string MarketPlace { get; set; }

        [JsonProperty(PropertyName = "marketplace_fee")]
        public decimal? MarketPlaceFee { get; set; }

        




        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}