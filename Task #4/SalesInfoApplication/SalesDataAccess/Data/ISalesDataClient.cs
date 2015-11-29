using SalesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDataAccess.Data
{
    interface ISalesDataClient
    {
        IEnumerable<Manager> Managers { get; }
        IEnumerable<Client> Clients { get; }
        IEnumerable<Product> Products { get; }
        IEnumerable<Sale> Sales { get; }

        void AddSale(Sale sale);
        void SaveData();
    }
}
