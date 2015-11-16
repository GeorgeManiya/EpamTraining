using System;

namespace TelephoneExchangeApplication.Data
{
    struct ClientTaxHistory
    {
        public ClientTaxHistory(ClientContract client, DateTime payDate, float tax)
        {
            Client = client;
            PayDate = payDate;
            Tax = tax;
        }

        public ClientContract Client { get; }
        public DateTime PayDate { get; }
        public float Tax { get; }
    }
}
