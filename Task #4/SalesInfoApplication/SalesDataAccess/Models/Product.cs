using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDataAccess.Models
{
    public class Product : IHasIdentifications
    {
        public Product(long id, string productName, int cost)
        {
            Id = id;
            ProductName = productName;
            Cost = cost;
        }

        public long Id { get; private set; }

        public string ProductName { get; private set; }

        public int Cost { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
