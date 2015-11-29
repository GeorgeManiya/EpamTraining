using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDataAccess.Models
{
    interface IHasIdentifications
    {
        long Id { get; }

        void SetId(int id);
    }
}
