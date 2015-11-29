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
        private static TelephoneExchange _telephoneExchange;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating ports and terminals");
            var terminals = new List<ClientTerminal>() { new ClientTerminal(), new ClientTerminal(), new ClientTerminal() };
            var ports = new List<Port>() { new Port(), new Port(), new Port() };

            Console.WriteLine("Creating telephone exchange");
            _telephoneExchange = new TelephoneExchange(ports, terminals);

            var perMinuteRate = new PerMinuteRate(10, 1000, 20);
            _telephoneExchange.Rates.Add(perMinuteRate);
            var perSecondRate = new PerSecondRate(0.5f, 0, 0);
            _telephoneExchange.Rates.Add(perSecondRate);
            _telephoneExchange.Rates.Add(new PerMinuteRate(15, 0, 70));

            var georgeClient = _telephoneExchange.AddClient("George", 29, perSecondRate);
            var renataClient = _telephoneExchange.AddClient("Renata", 29, perSecondRate);
            var dmitriClient = _telephoneExchange.AddClient("Dmitri", 33, perMinuteRate);

            while(true)
            {
                Console.WriteLine("Choose your profile");
                var index = 0;
                foreach (var client in _telephoneExchange.BillingSystem.Contracts)
                {
                    index++;
                    Console.WriteLine("{0} {1}", index, client);
                }

                int result = 0;
                int.TryParse(Console.ReadLine(), out result);
                if (result > 0 && result <= index)
                {
                    ShowPersonalInfo(_telephoneExchange.BillingSystem.Contracts.ElementAt(result - 1));
                }
            }
        }

        private static void ShowPersonalInfo(ClientContract client)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Your personal account: {0}", client);
                var perMinuteRate = client.Rate as PerMinuteRate;
                if(perMinuteRate != null)
                {
                    Console.WriteLine("Per minute rate\n -price per minute: {0} \n -monthly fee: {1} \n -free minutes: {2}",
                        perMinuteRate.PricePerMinute, perMinuteRate.MonthlyFee, perMinuteRate.FreeMinutesPerMonth);
                }
                else
                {
                    var perSecondRate = client.Rate as PerSecondRate;
                    Console.WriteLine("Per second rate\n -price per minute: {0} \n -monthly fee: {1} \n -free minutes: {2}",
                        perSecondRate.PricePerSecond, perSecondRate.MonthlyFee, perSecondRate.FreeMinutesPerMonth);
                }


                Console.WriteLine();
                Console.WriteLine("1. Make a call");
                Console.WriteLine("2. Answer a call");
                Console.WriteLine("3. Change your rate");
                Console.WriteLine("4. Get pays detail info");
                Console.WriteLine("5. Back");

                int result = 0;
                int.TryParse(Console.ReadLine(), out result);
                if (result > 0 && result <= 5)
                {
                    switch(result)
                    {
                        case 1:
                            MakeACall(client);
                            break;
                        case 2:
                            {
                                Console.WriteLine("Choose your profile");
                                var index = 0;
                                foreach (var callingClient in _telephoneExchange.BillingSystem.Contracts)
                                {
                                    index++;
                                    Console.WriteLine("{0} {1}", index, callingClient);
                                }

                                int res = 0;
                                int.TryParse(Console.ReadLine(), out res);
                                if (res > 0 && res <= index)
                                {
                                    AnswerACall(_telephoneExchange.BillingSystem.Contracts.ElementAt(res - 1), client);
                                }
                                break;
                            }
                        case 3:
                            ChangeYourRate(client);
                            break;
                        case 4:
                            ShowPaysInfo(client);
                            break;
                        case 5:
                            return;
                    }
                }
            }
        }

        private static void MakeACall(ClientContract sourceClient)
        {
            Console.Clear();
            Console.WriteLine("Which telephone number do you want to connect?");
            Console.WriteLine("Code:");
            var code = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine("Number:");
            var number = Convert.ToUInt32(Console.ReadLine());

            TelephoneNumber targetTelephoneNumber;
            try
            {
                targetTelephoneNumber = new TelephoneNumber(code, number);
                sourceClient.Terminal.Call(targetTelephoneNumber);
            }
            catch (ArgumentNullException exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine("Press any key");
                Console.ReadLine();
                return;
            }

            if(_telephoneExchange.BillingSystem.Contracts.Any(c => c.TelephoneNumber == targetTelephoneNumber))
            {
                var targetTerminal = _telephoneExchange.BillingSystem.Contracts.First(c => c.TelephoneNumber == targetTelephoneNumber).Terminal;
                targetTerminal.Accept();
                Console.WriteLine("Who will drop a call?");
                Console.WriteLine("1. You");
                Console.WriteLine("2. Your companion");

                int result = 0;
                int.TryParse(Console.ReadLine(), out result);
                if (result > 0 && result <= 2)
                {
                    switch (result)
                    {
                        case 1:
                            sourceClient.Terminal.Drop();
                            break;
                        case 2:
                            targetTerminal.Drop();
                            break;
                    }
                }
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }

        private static void AnswerACall(ClientContract sourceClient, ClientContract targetClient)
        {
            Console.Clear();
            sourceClient.Terminal.Call(targetClient.TelephoneNumber);
            Console.WriteLine("There is incomming call from: {0}", sourceClient.TelephoneNumber);

            Console.WriteLine("Accept? (y/n)");
            var result = Console.ReadLine();
            if(result == "y")
            {
                targetClient.Terminal.Accept();

                Console.WriteLine("Who will drop a call?");
                Console.WriteLine("1. You");
                Console.WriteLine("2. Your companion");

                int res = 0;
                int.TryParse(Console.ReadLine(), out res);
                if (res > 0 && res <= 2)
                {
                    switch (res)
                    {
                        case 1:
                            sourceClient.Terminal.Drop();
                            break;
                        case 2:
                            targetClient.Terminal.Drop();
                            break;
                    }
                }
            }
            else
            {
                targetClient.Terminal.Drop();
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }

        private static void ChangeYourRate(ClientContract client)
        {
            Console.Clear();
            Console.WriteLine("Which rate do you want to choose?");

            var index = 0;
            foreach (var rate in _telephoneExchange.Rates.Where(r => r != client.Rate))
            {
                index++;
                if (rate is PerMinuteRate)
                {
                    Console.WriteLine("{0}. Per minute rate. Price per minute: {1}", index, ((PerMinuteRate)rate).PricePerMinute);
                }
                else if (rate is PerSecondRate)
                {
                    Console.WriteLine("{0}. Per second rate. Price per second: {1}", index, ((PerSecondRate)rate).PricePerSecond);
                }
            }

            int result = 0;
            int.TryParse(Console.ReadLine(), out result);
            if (result > 0 && result <= index)
            {
                var newRate = _telephoneExchange.Rates.Where(r => r != client.Rate).ElementAt(index - 1);
                try
                {
                    _telephoneExchange.ChangeClientRate(client, newRate);
                }
                catch(ArgumentException exp)
                {
                    Console.WriteLine(exp.Message);
                    Console.WriteLine("Press any key");
                    Console.ReadLine();
                }
            }
        }

        private static void ShowPaysInfo(ClientContract client)
        {
            Console.Clear();
            var lastEvent = DateTime.MinValue;

            foreach(var taxHistory in _telephoneExchange.BillingSystem.TaxHistory.Where(h => h.Client == client))
            {
                Console.WriteLine("-----------------------");
                var date = taxHistory.PayDate;
                Console.WriteLine("Date: {0} Tax: {1}", taxHistory.PayDate, taxHistory.Tax);
                Console.WriteLine("History:");
                foreach (var session in _telephoneExchange.BillingSystem.SessionHistory.
                    Where(s => s.Source == client))
                {
                    var endCalling = session.EndCalling;
                    if(lastEvent < endCalling && endCalling < date)
                    {
                        Console.WriteLine("Session: Target: {0}, Duration: {1}, EndCalling: {2:dd.MM HH:mm:ss}",
                        session.Target, session.CallAccepted ? session.EndCalling.Subtract(session.StartCalling) : TimeSpan.Zero, session.EndCalling);
                    }
                }

                lastEvent = date;
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }
    }
}
