using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.RateData
{
    internal interface IRate
    {
        float PricePerMinute { get; }

        float MonthlyFee { get; }

        int FreeMinutesPerMonth { get; }

        bool HasFreeMinutes { get; }

        float CalculateCost(TimeSpan callDuration);
    }
}
