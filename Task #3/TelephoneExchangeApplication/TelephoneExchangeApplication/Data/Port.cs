using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.Requests;

namespace TelephoneExchangeApplication.Data
{
    class Port : IPort
    {
        private IClientTerminal _terminal;

        private ConnectionState _state;
        public ConnectionState State
        {
            private set
            {
                _state = value;
                OnStateChanged(_state);
            }
            get { return _state; }
        }

        public void ConnectWithTerminal(IClientTerminal terminal)
        {
            ClearHandlers();

            _terminal = terminal;

            terminal.Connected += OnTerminalConnected;
            terminal.Disconnected += OnTerminalDisconnected;

            terminal.Calling += OnTerminalCalling;
            terminal.Droped += OnTerminalDropCall;
            terminal.Accepted += OnTerminalAcceptCall;
            terminal.RequestCome += OnTerminalReceiveRequest;
        }


        #region Handlers

        private void OnTerminalConnected(object sender, EventArgs e)
        {
            State = ConnectionState.Free;
        }

        private void OnTerminalDisconnected(object sender, EventArgs e)
        {
            State = ConnectionState.UnConnected;
        }

        private void OnTerminalCalling(object sender, CallingRequest e)
        {
            State = ConnectionState.Busy;
        }

        private void OnTerminalDropCall(object sender, EventArgs e)
        {
            State = ConnectionState.Free;
        }

        private void OnTerminalAcceptCall(object sender, EventArgs e)
        {
            State = ConnectionState.Busy;
        }

        private void OnTerminalReceiveRequest(object sender, InCommingRequest e)
        {
            State = ConnectionState.Busy;
        }

        #endregion


        event EventHandler<ConnectionState> _stateChanged;
        public event EventHandler<ConnectionState> StateChanged
        {
            add { _stateChanged += value; }
            remove { _stateChanged -= value; }
        }
        private void OnStateChanged(ConnectionState newState)
        {
            if (_stateChanged != null)
            {
                _stateChanged(this, newState);
            }
        }

        public void ClearHandlers()
        {
            if (_terminal == null) return;

            _terminal.Connected -= OnTerminalConnected;
            _terminal.Disconnected -= OnTerminalDisconnected;

            _terminal.Calling -= OnTerminalCalling;
            _terminal.Droped -= OnTerminalDropCall;
            _terminal.Accepted -= OnTerminalAcceptCall;
            _terminal.RequestCome -= OnTerminalReceiveRequest;
        }

        public void Dispose()
        {
            _stateChanged = null;
            ClearHandlers();
        }
    }
}
