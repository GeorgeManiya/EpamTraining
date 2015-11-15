using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data.Requests
{
    public class CallingRequest
    {
        public CallingRequest(TelephoneNumber target)
        {
            Target = target;
        }

        public TelephoneNumber Target { get; set; }
    }
}
