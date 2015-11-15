using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data
{
    internal class TerminalToPortConnection : IDisposable
    {
        public TerminalToPortConnection(IPort port, IClientTerminal terminal)
        {
            Port = port;
            Terminal = terminal;

            // handle terminal events
            Port.ConnectWithTerminal(Terminal);
        }

        public IPort Port { get; }
        public IClientTerminal Terminal { get; }

        public void Dispose()
        {
            Port.ClearHandlers();
        }
    }
}
