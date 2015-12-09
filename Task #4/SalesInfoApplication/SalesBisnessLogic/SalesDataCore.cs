using SalesBisnessLogic.CSV;
using SalesDataAccess.Data;
using SalesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SalesBisnessLogic
{
    public class SalesDataCore : ISalesDataCore
    {
        private readonly SalesDataClient _salesDataClient;

        public IEnumerable<Manager> Managers { get; set; }

        public IEnumerable<Client> Clients { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Sale> Sales { get; set; }

        public SalesDataCore()
        {
            _salesDataClient = new SalesDataClient();

            FillData();
        }

        private void FillData()
        {
            Managers = _salesDataClient.Managers;
            Clients = _salesDataClient.Clients;
            Products = _salesDataClient.Products;
            Sales = _salesDataClient.Sales;
        }

        public void AddSale(Sale sale)
        {
            _salesDataClient.AddSale(sale);
        }

        public void AddSale(string managerName, string clientName, string productName, int productCost, DateTime saleDate)
        {
            var manager = new Manager(0, managerName);
            var client = new Client(0, clientName);
            var product = new Product(0, productName, productCost);
            var sale = new Sale(0, manager, client, product, saleDate);
            _salesDataClient.AddSale(sale);
        }

        public void Save()
        {
            _salesDataClient.SaveData();
        }

        public IEnumerable<Sale> Read(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException(string.Format("File path '{0}' is incorrect.", filePath));
            var managerNameEndIndex = fileName.IndexOf('_');
            if(managerNameEndIndex == -1)
                throw new ArgumentException(string.Format("File path '{0}' is incorrect.", filePath));

            var managerName = fileName.Substring(0, managerNameEndIndex);
            var manager = new Manager(0, managerName);

            using (var csvReader = new CSVFileReader(filePath))
            {
                while (!csvReader.EndOfStream)
                {
                    var values = csvReader.ReadLine();
                    if (values.Length < 4) continue;

                    DateTime saleDate;
                    var success = DateTime.TryParse(values[0].ToString(), out saleDate);
                    if (!success) continue;

                    int cost;
                    success = Int32.TryParse(values[3].ToString(), out cost);
                    if (!success) continue;

                    var clientName = values[1].ToString();
                    var productName = values[2].ToString();

                    var client = new Client(0, clientName);
                    var product = new Product(0, productName, cost);
                    var sale = new Sale(0, manager, client, product, saleDate);

                    yield return sale;
                }
            }
        }

        public void Write(string filePath, IEnumerable<Sale> sales)
        {
            using (var csvWriter = new CSVFileWriter(filePath))
            {
                foreach(var sale in sales)
                {
                    var saleDate = sale.SaleDate;
                    var clientName = sale.Client.Name;
                    var productName = sale.Product.ProductName;
                    var cost = sale.Product.Cost;

                    var values = new object[] { saleDate, clientName, productName, cost };
                    csvWriter.WriteLine(values);
                }
            }
        }

        public void Refill()
        {
            FillData();
        }
    }
}
