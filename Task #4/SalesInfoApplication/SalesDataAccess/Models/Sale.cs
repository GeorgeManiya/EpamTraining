using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDataAccess.Models
{
    public struct Sale : IHasIdentifications
    {
        public Sale(long id, Manager manager, Client client, Product product, DateTime saleDate)
        {
            Id = id;
            Manager = manager;
            Client = client;
            Product = product;
            SaleDate = saleDate;
        }

        public long Id { get; private set; }

        public Manager Manager { get; private set; }

        public Client Client { get; private set; }

        public Product Product { get; private set; }

        public DateTime SaleDate { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
