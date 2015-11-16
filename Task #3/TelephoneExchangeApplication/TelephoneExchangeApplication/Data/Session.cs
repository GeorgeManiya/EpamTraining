using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.RateData;
using TelephoneExchangeApplication.Data.Requests;

namespace TelephoneExchangeApplication.Data
{
    class Session
    {
        public Session(ClientContract source, ClientContract target, IRate rate)
        {
            Events = new List<ConnectionEvent>();

            Source = source;
            Target = target;
            Rate = rate;
        }

        public ClientContract Source { get; private set; }
        public ClientContract Target { get; private set; }
        public IRate Rate { get; private set; }

        public bool CallAccepted { get; private set; }

        private DateTime _startCalling;
        public DateTime StartCalling
        {
            set
            {
                if (value != default(DateTime))
                    CallAccepted = true;

                _startCalling = value;
            }
            get { return _startCalling; }
        }
        public DateTime EndCalling { get; set; }

        public ICollection<ConnectionEvent> Events { get; set; }
    }
}
