using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.Requests;
using TelephoneExchangeApplication.Data.Responces;

namespace TelephoneExchangeApplication.Data
{
    interface IClientTerminal : IDisposable
    {
        event EventHandler Connected;
        event EventHandler Disconnected;
        event EventHandler<CallingRequest> Calling;
        event EventHandler Droped;
        event EventHandler Accepted;
        event EventHandler<InCommingRequest> RequestCome;

        void Connect();
        void Disconnect();

        void Call(TelephoneNumber number);
        void Drop();
        void Accept();
        void ReceiveResponce(ResponceState responce);
        void IncomingRequestFrom(InCommingRequest request);
    }
}
