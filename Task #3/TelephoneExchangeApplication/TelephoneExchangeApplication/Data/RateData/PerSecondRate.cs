using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.RateData
{
    class PerSecondRate : IRate
    {
        public PerSecondRate(float pricePerSecond, float monthlyFee, int freeMinutes)
        {
            PricePerSecond = pricePerSecond;
            MonthlyFee = monthlyFee;
            FreeMinutesPerMonth = freeMinutes;
        }

        public float PricePerSecond { get; private set; }

        public float MonthlyFee { get; private set; }

        public int FreeMinutesPerMonth { get; private set; }

        public bool HasFreeMinutes { get { return FreeMinutesPerMonth > 0; } }

        public float CalculateCost(TimeSpan callDuration)
        {
            var totalSeconds = callDuration.TotalSeconds;
            return (float)totalSeconds * PricePerSecond;
        }
    }
}
