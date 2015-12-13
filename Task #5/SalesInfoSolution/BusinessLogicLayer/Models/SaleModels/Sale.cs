using System;

namespace BusinessLogicLayer.Models.SaleModels
{
    public class Sale
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
        public DateTime SaleDate { get; set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void SetManger(Manager manager)
        {
            Manager = manager;
        }

        public void SetClient(Client client)
        {
            Client = client;
        }

        public void SetProduct(Product product)
        {
            Product = product;
        }

        public void SetSaleDate(DateTime saleDate)
        {
            SaleDate = saleDate;
        }
    }
}
