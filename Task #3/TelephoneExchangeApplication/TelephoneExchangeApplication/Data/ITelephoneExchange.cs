﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.RateData;

namespace TelephoneExchangeApplication.Data
{
    interface ITelephoneExchange
    {
        ICollection<Port> Ports { get; }
        ICollection<ClientTerminal> Terminals { get; }
        ICollection<ClientContract> Contracts { get; }
        ICollection<IRate> Rates { get; }

        ICollection<Session> SessionHistory { get; }
        ICollection<Session> CurrentSessions { get; }

        void AddRange(ICollection<Port> ports, ICollection<ClientTerminal> terminals);

        ClientContract AddClient(string name, ushort code, IRate rate);
        ClientTerminal AddNewTerminal();
        Port AddNewPort();
        TelephoneNumber? GetFreeTelephoneNumber(ushort code);
    }
}
