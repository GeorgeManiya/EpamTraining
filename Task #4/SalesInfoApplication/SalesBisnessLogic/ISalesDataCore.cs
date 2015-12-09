using System;
using System.Collections.Generic;
using SalesDataAccess.Models;

namespace SalesBisnessLogic
{
    interface ISalesDataCore
    {
        IEnumerable<Manager> Managers { get; }
        IEnumerable<Client> Clients { get; }
        IEnumerable<Product> Products { get; }
        IEnumerable<Sale> Sales { get; }

        void AddSale(Sale sale);
        void AddSale(string managerName, string clientName, string productName, int productCost, DateTime saleDate);
        void Save();
        void Refill();

        IEnumerable<Sale> Read(string filePath);
        void Write(string filePath, IEnumerable<Sale> sales);
    }
}
