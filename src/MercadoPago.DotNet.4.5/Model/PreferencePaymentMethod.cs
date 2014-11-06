using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class PreferencePaymentMethod
    {
        private readonly List<PaymentTypes> excludedPaymentMethods = new List<PaymentTypes>();
        private readonly List<PaymentTypes> excludedPaymentMethodTypes = new List<PaymentTypes>();

        [JsonProperty(PropertyName = "excluded_payment_methods")]
        public List<PaymentTypes> ExcludedPaymentMethods
        {
            get { return excludedPaymentMethods; }
        }

        [JsonProperty(PropertyName = "excluded_payment_types")]
        public List<PaymentTypes> ExcludedPaymentMethodTypes
        {
            get { return excludedPaymentMethodTypes; }
        }
    }
}