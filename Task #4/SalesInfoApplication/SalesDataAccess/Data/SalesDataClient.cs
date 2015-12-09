using System.Collections.Generic;
using SalesData;
using System.Linq;
using Manager = SalesDataAccess.Models.Manager;
using Client = SalesDataAccess.Models.Client;
using Product = SalesDataAccess.Models.Product;
using Sale = SalesDataAccess.Models.Sale;

namespace SalesDataAccess.Data
{
    public class SalesDataClient : ISalesDataClient
    {
        private SalesDBEntities _salesDataContext;

        public SalesDataClient()
        {
            _salesDataContext = new SalesDBEntities();
        }


        public IEnumerable<Client> Clients
        {
            get
            {
                foreach(var client in _salesDataContext.Clients)
                {
                    yield return ToObject(client);
                }
            }
        }

        public IEnumerable<Manager> Managers
        {
            get
            {
                foreach (var manager in _salesDataContext.Managers)
                {
                    yield return ToObject(manager);
                }
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                foreach(var product in _salesDataContext.Products)
                {
                    yield return ToObject(product);
                }
            }
        }

        public IEnumerable<Sale> Sales
        {
            get
            {
                foreach (var sale in _salesDataContext.Sales)
                {
                    yield return ToObject(sale);
                }
            }
        }


        public void AddSale(Sale sale)
        {
            var eSale= ToEntity(sale);
            _salesDataContext.Sales.Add(eSale);
        }

        public void SaveData()
        {
            _salesDataContext.SaveChanges();
        }


        #region Entity to object conversions

        private Manager ToObject(SalesData.Manager manager)
        {
            var objectManager = new Manager(manager.ManagerID, manager.MangerName);

            return objectManager;
        }

        private Client ToObject(SalesData.Client client)
        {
            var objectClient = new Client(client.ClientID, client.ClientName);

            return objectClient;
        }

        private Product ToObject(SalesData.Product product)
        {
            var objectProduct = new Product(product.ProductID, product.ProductName, product.ProductCost);

            return objectProduct;
        }

        private Sale ToObject(SalesData.Sale sale)
        {
            var objectSale = new Sale(sale.SaleID, ToObject(sale.Manager), ToObject(sale.Client), ToObject(sale.Product), sale.SaleDate);

            return objectSale;
        }

        #endregion


        #region Object to entity conversions

        private SalesData.Manager ToEntity(Manager manager)
        {
            var entityManager = new SalesData.Manager()
            {
                ManagerID = manager.Id,
                MangerName = manager.Name
            };

            return entityManager;
        }

        private SalesData.Client ToEntity(Client client)
        {
            var entityClient = new SalesData.Client()
            {
                ClientID = client.Id,
                ClientName = client.Name
            };

            return entityClient;
        }

        private SalesData.Product ToEntity(Product product)
        {
            var entityProduct = new SalesData.Product()
            {
                ProductID = product.Id,
                ProductName = product.ProductName,
                ProductCost = product.Cost
            };

            return entityProduct;
        }

        private SalesData.Sale ToEntity(Sale sale)
        {
            var manager = _salesDataContext.Managers.Any(m => m.MangerName == sale.Manager.Name)
                ? _salesDataContext.Managers.First(m => m.MangerName == sale.Manager.Name)
                : ToEntity(sale.Manager);

            var client = _salesDataContext.Clients.Any(c => c.ClientName == sale.Client.Name)
                ? _salesDataContext.Clients.First(c => c.ClientName == sale.Client.Name)
                : ToEntity(sale.Client);

            var product = _salesDataContext.Products.Any(p => p.ProductName == sale.Product.ProductName && p.ProductCost == sale.Product.Cost)
                ? _salesDataContext.Products.First(p => p.ProductName == sale.Product.ProductName && p.ProductCost == sale.Product.Cost)
                : ToEntity(sale.Product);

            var entitySale = new SalesData.Sale()
            {
                SaleID = sale.Id,
                Manager = manager,
                Client = client,
                Product = product,
                SaleDate = sale.SaleDate
            };

            return entitySale;
        }

        #endregion
    }
}
