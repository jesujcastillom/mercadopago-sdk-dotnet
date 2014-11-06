using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class Preference
    {
        private readonly List<Item> items = new List<Item>();

        [JsonProperty(PropertyName = "collector_id")]
        public long? CollectorId { get; set; }

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

        //TODO: Not sure if this is one or many?
        [JsonProperty(PropertyName = "shipments")]
        public Shipment Shipment { get; set; }

        [JsonProperty(PropertyName = "notification_url")]
        public string NotificationUrl { get; set; }

        [JsonProperty(PropertyName = "external_reference")]
        public string ExternalReference { get; set; }

        [JsonProperty(PropertyName = "additional_info")]
        public string AdditionalInfo { get; set; }

        [JsonProperty(PropertyName = "expires")]
        public bool? Expires { get; set; }

        [JsonProperty(PropertyName = "expiration_date_from")]
        public DateTime? ExpirationDateFrom { get; set; }

        [JsonProperty(PropertyName = "expiration_date_to")]
        public DateTime? ExpirationDateTo { get; set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "init_point")]
        public string InitPoint { get; set; }

        [JsonProperty(PropertyName = "sandbox_init_point")]
        public string SandBoxInitPoint { get; set; }
    }

    public class Shipment
    {
        [JsonProperty(PropertyName = "receiver_address")]
        public Address ReceiverAddress { get; set; }
    }
}