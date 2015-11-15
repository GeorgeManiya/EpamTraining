using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data.Requests;

namespace TelephoneExchangeApplication.Data.Responces
{
    class Responce
    {
        public Responce(CallingRequest request, ResponceState state)
        {
            CallingRequest = request;
            State = state;
        }

        public CallingRequest CallingRequest { get; set; }

        public ResponceState State { get; set; }
    }
}
