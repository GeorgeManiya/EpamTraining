using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data
{
    interface IPort : IDisposable, IClearEventHandles
    {
        ConnectionState State { get; }

        event EventHandler<ConnectionState> StateChanged;

        void ConnectWithTerminal(IClientTerminal terminal);
    }
}
