using SalesBisnessLogic;
using SalesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManagerHelperApp
{
    class Program
    {
        private static ICollection<Sale> _sales;
        private static SalesDataCore _salesDataCore;
        private static string _managerName;

        static void Main(string[] args)
        {
            _sales = new List<Sale>();
            _salesDataCore = new SalesDataCore();

            Console.WriteLine("Введите Ваше имя");
            _managerName = Console.ReadLine();
            MainAction();
        }

        private static void MainAction()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие");
                Console.WriteLine("1. Добавить информацию о покупке");
                Console.WriteLine("2. Отправить данные о продажах на сервер");

                ushort result;
                var correctInput = ushort.TryParse(Console.ReadLine(), out result);
                if (!correctInput || result > 2)
                {
                    Console.WriteLine("Некоректный выбор. Для продолжение нажмите любую кнопку");
                    Console.ReadLine();
                    continue;
                }

                if (result == 1)
                    OnAddNewSale();
                else
                {
                    OnSendData();
                }
            }
        }

        private static void OnAddNewSale()
        {
            Console.Clear();
            Console.WriteLine("Введите имя клиента");
            var clientName = Console.ReadLine();

            Console.WriteLine("Введите название товара");
            var productName = Console.ReadLine();

            Console.WriteLine("Введите стоимость покупки");
            int cost;
            var correctInput = int.TryParse(Console.ReadLine(), out cost);
            while (!correctInput)
            {
                Console.WriteLine("Некоректная стоимость");
                Console.WriteLine("Введите стоимость покупки заново. Введите 'b' для выхода");
                var read = Console.ReadLine();
                if (read == "b") return;
                correctInput = int.TryParse(read, out cost);
            }

            var manager = new Manager(0, _managerName);
            var client = new Client(0, clientName);
            var product = new Product(0, productName, cost);
            var currentDate = DateTime.Now;
            var sale = new Sale(0, manager, client, product, currentDate);
            _sales.Add(sale);
        }

        private static void OnSendData()
        {
            Console.Clear();
            var fileName = string.Format("{0}_{1:ddMMyyyy}.csv", _managerName, DateTime.Now);
            _salesDataCore.Write(Path.Combine(@"D:\SalesRepository", fileName), _sales);
            Console.WriteLine("Данные отправлены. Нажмите любую кнопку.");
            Console.ReadLine();
        }
    }
}
