using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Manager = DataAccessLayer.Models.SaleModels.Manager;
using Client = DataAccessLayer.Models.SaleModels.Client;
using Product = DataAccessLayer.Models.SaleModels.Product;
using Sale = DataAccessLayer.Models.SaleModels.Sale;

namespace DataAccessLayer.Data
{
    public class SalesDataProvider : ISalesDataProvider
    {
        private SalesDBEntities _salesEntities;
        private ICollection<Model.Entities.Sale> _salesEntitiesList;

        public SalesDataProvider()
        {
            _salesEntities = new SalesDBEntities();
        }

        public Sale AddNewSale(Manager manager, Client client, Product product, DateTime saleDate)
        {
            var entitySale = new Model.Entities.Sale()
            {
                ManagerID = manager.Id,
                ClientID = client.Id,
                ProductID = product.Id,
                SaleDate = saleDate
            };

            _salesEntities.Sales.Add(entitySale);
            _salesEntities.SaveChanges();

            _salesEntitiesList.Add(entitySale);
            return SaleEntityToModel(entitySale);
        }

        public void ChangeSale(Sale sale, Manager newManger, Client newClient, Product newProduct)
        {
            var entitySale = _salesEntitiesList.FirstOrDefault(s => s.SaleID == sale.Id);
            if (entitySale == null)
                throw new ArgumentException("Cannot find current sale in data base");

            entitySale.ManagerID = newManger.Id;
            entitySale.ClientID = newClient.Id;
            entitySale.ProductID = newProduct.Id;
            _salesEntities.SaveChanges();

            sale.SetManger(newManger);
            sale.SetClient(newClient);
            sale.SetProduct(newProduct);
        }

        public void DeleteSale(Sale sale)
        {
            var entitySale = _salesEntitiesList.FirstOrDefault(s => s.SaleID == sale.Id);
            if (entitySale == null)
                throw new ArgumentException("Cannot find current sale in data base");

            _salesEntities.Sales.Remove(entitySale);
            _salesEntities.SaveChanges();

            _salesEntitiesList.Remove(entitySale);
        }

        public IEnumerable<Sale> GetSales(DateTime dateFrom, DateTime dateTo)
        {
            _salesEntitiesList = _salesEntities.Sales.Where(s => dateFrom <= s.SaleDate && s.SaleDate < dateTo).ToList();
            return _salesEntitiesList.Select(s => SaleEntityToModel(s));
        }



        #region Entities to object

        private Sale SaleEntityToModel(Model.Entities.Sale sale)
        {
            var objectManager = sale.Manager != null
                ? ManagerEntityToModel(sale.Manager)
                : null;

            var objectClient = sale.Client != null
                ? ClientEntityToModel(sale.Client)
                : null;

            var objectProduct = sale.Product != null
                ? ProductEntityToModel(sale.Product)
                : null;

            return new Sale(sale.SaleID, objectManager, objectClient, objectProduct, sale.SaleDate);
        }

        private Manager ManagerEntityToModel(Model.Entities.Manager manager)
        {
            return new Manager(manager.ManagerID, manager.MangerName);
        }

        private Client ClientEntityToModel(Model.Entities.Client client)
        {
            return new Client(client.ClientID, client.ClientName);
        }

        private Product ProductEntityToModel(Model.Entities.Product product)
        {
            return new Product(product.ProductID, product.ProductName, product.ProductCost);
        }

        #endregion
    }
}
