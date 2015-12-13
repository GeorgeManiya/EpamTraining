using DataAccessLayer.Models.SaleModels;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data
{
    interface ISalesDataProvider
    {
        IEnumerable<Sale> GetSales(DateTime dateFrom, DateTime dateTo);
        Sale AddNewSale(Manager manager, Client client, Product product, DateTime saleDate);
        void ChangeSale(Sale sale, Manager newManger, Client newClient, Product newProduct);
        void DeleteSale(Sale sale);
    }
}
