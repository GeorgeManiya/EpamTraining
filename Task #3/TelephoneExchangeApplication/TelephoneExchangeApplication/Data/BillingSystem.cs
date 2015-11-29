using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TelephoneExchangeApplication.Data
{
    class BillingSystem : IBillingSystem
    {
        private Timer _timer;
        public DateTime CurrentTime;

        public BillingSystem()
        {
            CurrentTime = DateTime.Now;
            _timer = new Timer(new TimerCallback(OnTimerTick), null, 2000, 2000);

            Contracts = new List<ClientContract>();
            SessionHistory = new List<Session>();
            RateHistory = new List<ClientRateHistory>();
            TaxHistory = new List<ClientTaxHistory>();
        }

        public ICollection<ClientContract> Contracts { get; private set; }

        public ICollection<Session> SessionHistory { get; private set; }

        public ICollection<ClientRateHistory> RateHistory { get; private set; }

        public ICollection<ClientTaxHistory> TaxHistory { get; private set; }

        public IEnumerable<Session> GetSessionHistory(ClientContract client, DateTime dateFrom, DateTime dateTo)
        {
            return SessionHistory.Where(s => (s.Source == client || s.Target == client) &&
                                                (s.Events.Any() && s.Events.First().EventTime >= dateFrom && s.Events.Last().EventTime <= dateTo));
        }

        public float CalculateSessionCost(Session session)
        {
            if (!session.CallAccepted) return 0;

            var rate = session.Rate;
            var duration = session.EndCalling.Subtract(session.StartCalling);

            var cost = rate.CalculateCost(duration);
            return cost;
        }

        public TimeSpan CalculateClientCallDuration(ClientContract client, DateTime dateFrom, DateTime dateTo)
        {
            var duration = SessionHistory.
                    Where(s => s.Source == client && (s.CallAccepted && s.Events.First().EventTime >= dateFrom && s.Events.Last().EventTime <= dateTo)).
                    Aggregate(TimeSpan.Zero, (total, current) => total.Add(current.EndCalling.Subtract(current.StartCalling)));

            return duration;
        }

        public void CalculateClientMonthlyTax(ClientContract client)
        {
            var lastTax = TaxHistory.Any(t => t.Client == client)
                ? TaxHistory.Last(t => t.Client == client).PayDate
                : DateTime.MinValue;

            var currentRate = RateHistory.Last(h => h.Client == client).Rate;
            var duration = CalculateClientCallDuration(client, lastTax, CurrentTime);
            duration = duration.Subtract(TimeSpan.FromMinutes(currentRate.FreeMinutesPerMonth));
            var tax = currentRate.CalculateCost(duration.TotalMinutes > 0 ? duration : TimeSpan.Zero) + currentRate.MonthlyFee;

            TaxHistory.Add(new ClientTaxHistory(client, CurrentTime, tax));
        }

        private void OnTimerTick(object par)
        {
            CurrentTime = CurrentTime.AddDays(1);

            foreach(var client in Contracts)
            {
                var lastRateHistory = RateHistory.Last(h => h.Client == client);
                if(CurrentTime.Day == lastRateHistory.Date.Day && (CurrentTime.Month - 1) == lastRateHistory.Date.Month)
                    CalculateClientMonthlyTax(client);
            }
        }
    }
}
