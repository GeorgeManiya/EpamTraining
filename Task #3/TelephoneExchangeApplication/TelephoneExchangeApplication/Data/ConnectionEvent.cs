using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data
{
    public enum ConnectionEventType
    {
        Calling,
        Accepted,
        Droped,
        Disconnected
    }

    class ConnectionEvent
    {
        public ConnectionEvent(ClientContract source, ConnectionEventType type, DateTime eventTime)
        {
            Source = source;
            Type = type;
            EventTime = eventTime;
        }

        public ClientContract Source { get; private set; }
        public ConnectionEventType Type { get; private set; }
        public DateTime EventTime { get; private set; }
    }
}
