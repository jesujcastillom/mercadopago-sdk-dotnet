namespace MercadoPago.DotNet
{
    public class PaymentTypes:IdValueElement
    {
        public static readonly PaymentTypes AccountMoney = new PaymentTypes
        {
            Id = "account_money",
            Name = "Account Money"
        };

        public static readonly PaymentTypes Ticket = new PaymentTypes
        {
            Id = "ticket",
            Name = "Ticket"
        };

        public static readonly PaymentTypes BankTransfer = new PaymentTypes
        {
            Id = "bank_transfer",
            Name = "Bank Transfer"
        };

        public static readonly PaymentTypes ATM = new PaymentTypes
        {
            Id = "atm",
            Name = "ATM Bank Transfer"
        };

        public static readonly PaymentTypes CreditCard = new PaymentTypes
        {
            Id = "credit_card",
            Name = "Credit Card"
        };

        public static readonly PaymentTypes DebitCard = new PaymentTypes
        {
            Id = "debit_card",
            Name = "Debit Card"
        };
  
        //Prevent instantiation of this class elsewhere
        private PaymentTypes()
        {
            
        }
    }

  // Payment Methods...
  //
  //  - {...
  //  "id": "visa",
  //  "name": "Visa",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/visa.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/visa.gif"
  //},
  //- {...
  //  "id": "master",
  //  "name": "Mastercard",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/master.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/master.gif"
  //},
  //- {...
  //  "id": "amex",
  //  "name": "American Express",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/amex.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/amex.gif"
  //},
  //- {...
  //  "id": "naranja",
  //  "name": "Naranja",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/naranja.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/naranja.gif"
  //},
  //- {...
  //  "id": "nativa",
  //  "name": "Nativa Mastercard",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/nativa.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/nativa.gif"
  //},
  //- {...
  //  "id": "tarshop",
  //  "name": "Tarjeta Shopping",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/tarshop.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/tarshop.gif"
  //},
  //- {...
  //  "id": "cencosud",
  //  "name": "Cencosud",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/cencosud.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/cencosud.gif"
  //},
  //- {...
  //  "id": "cabal",
  //  "name": "Cabal",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/cabal.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/cabal.gif"
  //},
  //- {...
  //  "id": "argencard",
  //  "name": "Argencard",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/argencard.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/argencard.gif"
  //},
  //- {...
  //  "id": "diners",
  //  "name": "Diners",
  //  "payment_type_id": "credit_card",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/diners.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/diners.gif"
  //},
  //- {...
  //  "id": "pagofacil",
  //  "name": "Pago Fácil",
  //  "payment_type_id": "ticket",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/pagofacil.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/pagofacil.gif"
  //},
  //- {...
  //  "id": "rapipago",
  //  "name": "Rapipago",
  //  "payment_type_id": "ticket",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/rapipago.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/rapipago.gif"
  //},
  //- {...
  //  "id": "redlink",
  //  "name": "RedLink",
  //  "payment_type_id": "atm",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/redlink.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/redlink.gif"
  //},
  //- {...
  //  "id": "bapropagos",
  //  "name": "Provincia Pagos",
  //  "payment_type_id": "ticket",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/bapropagos.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/bapropagos.gif"
  //},
  //- {...
  //  "id": "account_money",
  //  "name": "Dinero en mi cuenta de MercadoPago",
  //  "payment_type_id": "account_money",
  //  "thumbnail": "http://img.mlstatic.com/org-img/MP3/API/logos/account_money.gif",
  //  "secure_thumbnail": "https://www.mercadopago.com/org-img/MP3/API/logos/account_money.gif"
  //}

}