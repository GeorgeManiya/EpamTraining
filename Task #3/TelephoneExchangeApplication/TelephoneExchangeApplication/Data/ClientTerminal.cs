using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.Requests;
using TelephoneExchangeApplication.Data.Responces;

namespace TelephoneExchangeApplication.Data
{
    class ClientTerminal : IClientTerminal
    {
        public ClientTerminal() { }


        public virtual void Connect()
        {
            OnConnected();
        }

        public virtual void Disconnect()
        {
            OnDisconnected();
        }


        public void Call(TelephoneNumber number)
        {
            var request = new CallingRequest(number);
            OnCalling(request);
        }

        public void Drop()
        {
            OnDroped();
        }

        public void Accept()
        {
            OnAccepted();
        }

        public void ReceiveResponce(ResponceState responce)
        {
            switch (responce)
            {
                case ResponceState.IsBusy:
                    Console.WriteLine("Target telephone number is busy now");
                    Drop();
                    break;
                case ResponceState.Droped:
                    Console.WriteLine("Companion drop a call");
                    Drop();
                    break;
                case ResponceState.UnConnected:
                    Console.WriteLine("Target telephone number is not connected");
                    Drop();
                    break;
                case ResponceState.DoesNotExist:
                    Console.WriteLine("Target telephone number doen't exist");
                    Drop();
                    break;
                case ResponceState.Calling:
                    Console.WriteLine("Connection success");
                    break;
                case ResponceState.Accepted:
                    Console.WriteLine("Companion accept a call");
                    break;
            }
        }

        public void IncomingRequestFrom(InCommingRequest request)
        {
            OnRequestCome(request);
        }


        #region Events

        EventHandler _connected;
        public event EventHandler Connected
        {
            add { _connected += value; }
            remove { _connected -= value; }
        }
        private void OnConnected()
        {
            if (_connected != null)
                _connected(this, null);
        }

        EventHandler _drisconnected;
        public event EventHandler Disconnected
        {
            add { _drisconnected += value; }
            remove { _drisconnected -= value; }
        }
        private void OnDisconnected()
        {
            if (_drisconnected != null)
                _drisconnected(this, null);
        }

        EventHandler<CallingRequest> _calling;
        public event EventHandler<CallingRequest> Calling
        {
            add { _calling += value; }
            remove { _calling -= value; }
        }
        private void OnCalling(CallingRequest request)
        {
            if (_calling != null)
            {
                _calling(this, request);
            }
        }

        EventHandler _droped;
        public event EventHandler Droped
        {
            add { _droped += value; }
            remove { _droped -= value; }
        }
        private void OnDroped()
        {
            if (_droped != null)
                _droped(this, null);
        }

        EventHandler _accepted;
        public event EventHandler Accepted
        {
            add { _accepted += value; }
            remove { _accepted -= value; }
        }
        private void OnAccepted()
        {
            if(_accepted != null)
                _accepted(this, null);
        }

        EventHandler<InCommingRequest> _requestCome;
        public event EventHandler<InCommingRequest> RequestCome
        {
            add { _requestCome += value; }
            remove { _requestCome -= value; }
        }
        private void OnRequestCome(InCommingRequest request)
        {
            if (_requestCome != null)
                _requestCome(this, request);
        }

        #endregion

        public void Dispose()
        {
            _connected = null;
            _drisconnected = null;
            _calling = null;
            _droped = null;
            _accepted = null;
            _requestCome = null;
        }
    }
}
