using Newtonsoft.Json;

namespace MercadoPago.DotNet
{
    public class PaymentMethodTypes
    {
        public static readonly PaymentMethodTypes AccountMoney = new PaymentMethodTypes
        {
            Id = "account_money",
            Name = "Account Money"
        };

        public static readonly PaymentMethodTypes Ticket = new PaymentMethodTypes
        {
            Id = "ticket",
            Name = "Ticket"
        };

        public static readonly PaymentMethodTypes BankTransfer = new PaymentMethodTypes
        {
            Id = "bank_transfer",
            Name = "Bank Transfer"
        };

        public static readonly PaymentMethodTypes ATM = new PaymentMethodTypes
        {
            Id = "atm",
            Name = "ATM Bank Transfer"
        };

        public static readonly PaymentMethodTypes CreditCard = new PaymentMethodTypes
        {
            Id = "credit_card",
            Name = "Credit Card"
        };

        public static readonly PaymentMethodTypes DebitCard = new PaymentMethodTypes
        {
            Id = "debit_card",
            Name = "Debit Card"
        };
  
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string DisplayName { get; set; }

        //Prevent instantiation of this class elsewhere
        private PaymentMethodTypes()
        {
            
        }
    }
}