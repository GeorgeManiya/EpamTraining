using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.RateData
{
    class PerSecondRate : PerMinuteRate
    {
        private float _pricePerSecond;
        public float PricePerSecond
        {
            get { return _pricePerSecond; }
            set { _pricePerSecond = value; }
        }

        private float _pricePerMinute;
        public override float PricePerMinute
        {
            get { return _pricePerSecond * 60; }
            set { _pricePerSecond = value / 60; }
        }

        public override float CalculateCost(TimeSpan callDuration)
        {
            var totalSeconds = callDuration.TotalSeconds;
            return (float)totalSeconds * PricePerSecond;
        }
    }
}
