using System;
using System.Collections.Generic;

namespace TelephoneExchangeApplication.Data
{
    interface IBillingSystem
    {
        ICollection<Session> SessionHistory { get; }
        ICollection<ClientRateHistory> RateHistory { get; }
        ICollection<ClientTaxHistory> TaxHistory { get; }
        ICollection<ClientContract> Contracts { get; }

        IEnumerable<Session> GetSessionHistory(ClientContract client, DateTime dateFrom, DateTime dateTo);

        float CalculateSessionCost(Session session);
        TimeSpan CalculateClientCallDuration(ClientContract client, DateTime dateFrom, DateTime dateTo);
        void CalculateClientMonthlyTax(ClientContract client);
    }
}
