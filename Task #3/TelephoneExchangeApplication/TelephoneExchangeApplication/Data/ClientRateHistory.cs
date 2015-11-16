using System;
using TelephoneExchangeApplication.Data.RateData;

namespace TelephoneExchangeApplication.Data
{
    struct ClientRateHistory
    {
        public ClientRateHistory(ClientContract client, DateTime date, IRate rate)
        {
            Client = client;
            Date = date;
            Rate = rate;
        }

        public ClientContract Client { get; }
        public DateTime Date { get; }
        public IRate Rate { get; }
    }
}
