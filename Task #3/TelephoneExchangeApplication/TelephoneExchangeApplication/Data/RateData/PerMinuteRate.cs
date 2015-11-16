using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.RateData
{
    class PerMinuteRate : IRate
    {
        public PerMinuteRate(float pricePerMinute, float monthlyFee, int freeMinutes)
        {
            PricePerMinute = pricePerMinute;
            MonthlyFee = monthlyFee;
            FreeMinutesPerMonth = freeMinutes;
        }

        public float PricePerMinute { get; private set; }

        public float MonthlyFee { get; private set; }

        public int FreeMinutesPerMonth { get; private set; }

        public bool HasFreeMinutes { get { return FreeMinutesPerMonth > 0; } }

        public float CalculateCost(TimeSpan callDuration)
        {
            var totalMinutes = callDuration.TotalMinutes;
            return (float)totalMinutes * PricePerMinute;
        }
    }
}
