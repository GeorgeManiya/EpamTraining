using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.RateData;
using TelephoneExchangeApplication.Data.Requests;
using TelephoneExchangeApplication.Data.Responces;

namespace TelephoneExchangeApplication.Data
{
    class TelephoneExchange : ITelephoneExchange
    {
        private List<TerminalToPortConnection> _terminalToPertConnections;

        public TelephoneExchange()
        {
            _terminalToPertConnections = new List<TerminalToPortConnection>();
            CurrentSessions = new List<Session>();
            Rates = new List<IRate>();
            BillingSystem = new BillingSystem();

            Ports = new List<Port>();
            Terminals = new List<ClientTerminal>();
        }

        public TelephoneExchange(ICollection<Port> ports, ICollection<ClientTerminal> terminals)
        {
            _terminalToPertConnections = new List<TerminalToPortConnection>();
            CurrentSessions = new List<Session>();
            Rates = new List<IRate>();
            BillingSystem = new BillingSystem();

            Ports = ports;
            Terminals = terminals;
        }

        public ICollection<Port> Ports { get; private set; }

        public ICollection<ClientTerminal> Terminals { get; private set; }

        


        public BillingSystem BillingSystem { get; private set; }

        public ICollection<Session> CurrentSessions { get; private set; }

        public ICollection<IRate> Rates { get; private set; }

        public void AddRange(ICollection<Port> ports, ICollection<ClientTerminal> terminals)
        {
            ((List<Port>)Ports).AddRange(ports);
            ((List<ClientTerminal>)Terminals).AddRange(terminals);
        }


        public ClientContract AddClient(string name, ushort code, IRate rate)
        {
            // get free telephone number
            var freeNumber = GetFreeTelephoneNumber(code);
            if (!freeNumber.HasValue)
                throw new ArgumentException("There is no free telephone number in this code");

            // get free terminal
            var freeTerminal = Terminals.FirstOrDefault(t => BillingSystem.Contracts.All(c => c.Terminal != t));
            if (freeTerminal == null)
                freeTerminal = AddNewTerminal();

            // connect terminal with first free port
            ConnectTerminalToFreePort(freeTerminal);

            var newClientContract = new ClientContract(name, freeNumber.Value, freeTerminal, rate);
            BillingSystem.Contracts.Add(newClientContract);

            BillingSystem.RateHistory.Add(new ClientRateHistory(newClientContract, BillingSystem.CurrentTime, rate));

            return newClientContract;
        }

        protected void ConnectTerminalToFreePort(IClientTerminal terminal)
        {
            if (_terminalToPertConnections.Any(c => c.Terminal == terminal)) return;

            // get free port
            var freePort = Ports.FirstOrDefault(p => _terminalToPertConnections.All(c => c.Port != p));
            if (freePort == null)
                // add new port, if there is no free one
                freePort = AddNewPort();

            ConnectTerminalToPort(terminal, freePort);
        }

        protected void ConnectTerminalToPort(IClientTerminal terminal, IPort port)
        {
            var connection = new TerminalToPortConnection(port, terminal);
            _terminalToPertConnections.Add(connection);

            // add event handlers
            ConnectWithTerminal(terminal);
            ConnectWithPort(port);
        }

        public ClientTerminal AddNewTerminal()
        {
            var newTerminal = new ClientTerminal();
            Terminals.Add(newTerminal);
            return newTerminal;
        }

        public Port AddNewPort()
        {
            var newPort = new Port();
            Ports.Add(newPort);
            return newPort;
        }

        public TelephoneNumber? GetFreeTelephoneNumber(ushort code)
        {
            for(uint i = 100000; i <= 999999; i++)
            {
                if (BillingSystem.Contracts.Where(c => c.TelephoneNumber.Code == code).All(c => c.TelephoneNumber.Number != i))
                    return new TelephoneNumber(code, i);
            }

            return null;
        }


        public void RemoveClient(ClientContract client)
        {
            var clientTerminal = client.Terminal;
            var terminalToPortConnection = _terminalToPertConnections.First(c => c.Terminal == clientTerminal);
            _terminalToPertConnections.Remove(terminalToPortConnection);
            terminalToPortConnection.Dispose();

            DisconnectWithTerminal(clientTerminal);

            BillingSystem.Contracts.Remove(client);
        }

        public void ChangeClientRate(ClientContract client, IRate newRate)
        {
            var currentDate = BillingSystem.CurrentTime;
            var lastHistoryDate = BillingSystem.RateHistory.LastOrDefault(h => h.Client == client).Date;
            if (new DateTime(lastHistoryDate.Year, lastHistoryDate.Month + 1, lastHistoryDate.Day) > currentDate)
                throw new ArgumentException("You cannot change rate before on month has gone");

            client.Rate = newRate;
            BillingSystem.CalculateClientMonthlyTax(client);
            var rateHistory = new ClientRateHistory(client, currentDate, newRate);
            BillingSystem.RateHistory.Add(rateHistory);
        }



        protected void ConnectWithTerminal(IClientTerminal terminal)
        {
            DisconnectWithTerminal(terminal);

            terminal.Calling += OnTerminalCalling;
            terminal.Droped += OnTerminalDropCall;
            terminal.Accepted += OnTerminalAcceptCall;
        }

        protected void DisconnectWithTerminal(IClientTerminal terminal)
        {
            terminal.Calling -= OnTerminalCalling;
            terminal.Droped -= OnTerminalDropCall;
            terminal.Accepted -= OnTerminalAcceptCall;
        }

        protected void ConnectWithPort(IPort port)
        {
            DisconnectWithPort(port);

            port.StateChanged += OnPortStateChanged;
        }

        protected void DisconnectWithPort(IPort port)
        {
            port.StateChanged -= OnPortStateChanged;
        }


        #region Handlers

        private void OnTerminalCalling(object sender, CallingRequest e)
        {
            var sourceTerminal = sender as ClientTerminal;
            if (sourceTerminal == null) return;

            var sourceClient = BillingSystem.Contracts.First(c => c.Terminal == sourceTerminal);
            var sourceTelephoneNumber = sourceClient.TelephoneNumber;
            var incommingRequest = new InCommingRequest(sourceTelephoneNumber);

            var clients = BillingSystem.Contracts.Where(c => c.TelephoneNumber == e.Target);
            // Target number doesn't exist
            if (!clients.Any())     
            {
                sourceTerminal.ReceiveResponce(ResponceState.DoesNotExist);
                return;
            }

            var targetClient = clients.First();
            var session = new Session(sourceClient, targetClient, sourceClient.Rate);
            var systemTime = DateTime.Now;
            var currentTime = new DateTime(BillingSystem.CurrentTime.Year, BillingSystem.CurrentTime.Month, systemTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second);
            var connEvent = new ConnectionEvent(targetClient, ConnectionEventType.Accepted, currentTime);
            session.Events.Add(connEvent);

            var targetPort = _terminalToPertConnections.First(c => c.Terminal == targetClient.Terminal).Port;
            switch (targetPort.State)
            {
                case ConnectionState.Free:
                    {
                        sourceTerminal.ReceiveResponce(ResponceState.Calling);
                        CurrentSessions.Add(session);

                        targetClient.Terminal.IncomingRequestFrom(incommingRequest);
                        break;
                    }
                case ConnectionState.Busy:
                    {
                        sourceTerminal.ReceiveResponce(ResponceState.IsBusy);
                        BillingSystem.SessionHistory.Add(session);
                        break;
                    }
                case ConnectionState.UnConnected:
                    {
                        sourceTerminal.ReceiveResponce(ResponceState.UnConnected);
                        BillingSystem.SessionHistory.Add(session);
                        break;
                    }
            }
        }

        private void OnTerminalDropCall(object sender, EventArgs e)
        {
            var terminal = sender as ClientTerminal;
            if (terminal == null) return;

            var client = BillingSystem.Contracts.First(c => c.Terminal == terminal);
            var sessions = CurrentSessions.Where(s => s.Source == client || s.Target == client).ToList();
            if (!sessions.Any()) return;


            var session = sessions.First();
            var systemTime = DateTime.Now;
            var callingEventTime = session.Events.First().EventTime;
            var currentTime = new DateTime(callingEventTime.Year, callingEventTime.Month, callingEventTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second);
            var connEvent = new ConnectionEvent(client, ConnectionEventType.Droped, currentTime);
            session.Events.Add(connEvent);
            session.EndCalling = currentTime;

            CurrentSessions.Remove(session);
            BillingSystem.SessionHistory.Add(session);

            var isTarget = session.Target == client;
            if (isTarget)
            {
                session.Source.Terminal.ReceiveResponce(ResponceState.Droped);
            }
            else
            {
                session.Target.Terminal.ReceiveResponce(ResponceState.Droped);
            }
        }

        private void OnTerminalAcceptCall(object sender, EventArgs e)
        {
            var targetTerminal = sender as ClientTerminal;
            if (targetTerminal == null) return;

            var targetClient = BillingSystem.Contracts.First(c => c.Terminal == targetTerminal);
            var sessions = CurrentSessions.Where(s => s.Target == targetClient).ToList();
            if (!sessions.Any()) return;

            var session = sessions.First();
            var systemTime = DateTime.Now;
            var callingEventTime = session.Events.First().EventTime;
            var currentTime = new DateTime(callingEventTime.Year, callingEventTime.Month, callingEventTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second);
            var connEvent = new ConnectionEvent(targetClient, ConnectionEventType.Accepted, currentTime);
            session.Events.Add(connEvent);
            session.StartCalling = currentTime;

            session.Source.Terminal.ReceiveResponce(ResponceState.Accepted);
        }

        private void OnPortStateChanged(object sender, ConnectionState e)
        {
            if (e != ConnectionState.UnConnected) return;

            var port = sender as Port;
            if (port == null) return;

            var connections = _terminalToPertConnections.Where(c => c.Port == port).ToList();
            if (!connections.Any()) return;

            var connection = connections.First();
            var sourceTerminal = connection.Terminal;
            var sourceClient = BillingSystem.Contracts.First(c => c.Terminal == sourceTerminal);

            // if there is no session, connected with current terminal, do nothing
            var sessions = CurrentSessions.Where(s => s.Source == sourceClient || s.Target == sourceClient).ToList();
            if (!sessions.Any()) return;

            var session = sessions.First();
            var systemTime = DateTime.Now;
            var callingEventTime = session.Events.First().EventTime;
            var currentTime = new DateTime(callingEventTime.Year, callingEventTime.Month, callingEventTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second);
            var connEvent = new ConnectionEvent(sourceClient, ConnectionEventType.Disconnected, currentTime);
            // end this session
            session.Events.Add(connEvent);
            session.EndCalling = currentTime;

            CurrentSessions.Remove(session);
            BillingSystem.SessionHistory.Add(session);

            var isTarget = session.Target == sourceClient;
            if (isTarget)
            {
                session.Source.Terminal.ReceiveResponce(ResponceState.UnConnected);
            }
            else
            {
                session.Target.Terminal.ReceiveResponce(ResponceState.UnConnected);
            }
        }

        #endregion
    }
}
