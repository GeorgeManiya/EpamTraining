using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.RateData;

namespace TelephoneExchangeApplication.Data
{
    class ClientContract
    {
        public ClientContract(string clientName, TelephoneNumber number, IClientTerminal terminal, IRate rate)
        {
            ClientName = clientName;
            TelephoneNumber = number;
            Terminal = terminal;
            Rate = rate;
        }

        public string ClientName { get; private set; }

        public TelephoneNumber TelephoneNumber { get; set; }

        public IClientTerminal Terminal { get; set; }

        public IRate Rate { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", ClientName, TelephoneNumber);
        }
    }
}
