using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class PreferencePaymentMethod
    {
        [JsonProperty(PropertyName = "excluded_payment_methods")]
        public List<PaymentTypes> ExcludedPaymentMethods { get; set; }

        [JsonProperty(PropertyName = "excluded_payment_types")]
        public List<PaymentTypes> ExcludedPaymentMethodTypes { get; set; }
    }
}