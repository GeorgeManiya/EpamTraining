using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.RateData
{
    class PerMinuteRate : IRate
    {
        public virtual float PricePerMinute { get; set; }

        public float MonthlyFee { get; set; }

        public int FreeMinutesPerMonth { get; set; }

        public bool HasFreeMinutes { get { return FreeMinutesPerMonth > 0; } }

        public virtual float CalculateCost(TimeSpan callDuration)
        {
            var totalMinutes = callDuration.TotalMinutes;
            return (float)totalMinutes * PricePerMinute;
        }
    }
}
