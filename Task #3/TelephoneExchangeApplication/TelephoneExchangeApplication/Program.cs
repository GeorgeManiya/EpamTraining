using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneExchangeApplication.Data;
using TelephoneExchangeApplication.Data.RateData;

namespace TelephoneExchangeApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating ports and terminals");
            var terminals = new List<ClientTerminal>() { new ClientTerminal(), new ClientTerminal(), new ClientTerminal() };
            var ports = new List<Port>() { new Port(), new Port(), new Port() };

            Console.WriteLine("Creating telephone exchange");
            var telephoneExchange = new TelephoneExchange(ports, terminals);

            var rate = new PerMinuteRate() { MonthlyFee = 10000, PricePerMinute = 100 };
            var georgeClient = telephoneExchange.AddClient("George", 29, rate);
            var renataClient = telephoneExchange.AddClient("Renata", 29, rate);

            georgeClient.Terminal.Call(renataClient.TelephoneNumber);
            renataClient.Terminal.Accept();
            renataClient.Terminal.Drop();
        }
    }
}
