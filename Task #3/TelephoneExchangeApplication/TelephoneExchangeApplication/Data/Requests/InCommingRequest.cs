using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.Requests
{
    class InCommingRequest
    {
        public InCommingRequest(TelephoneNumber source)
        {
            Source = source;
        }

        public TelephoneNumber Source { get; set; }
    }
}
