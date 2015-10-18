using System;
using SweetGift.Data;
using System.Collections.Generic;
using System.Linq;
using SweetGift.Interfaces;

namespace SweetGift
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Greetings!");
            ProgramInitialize();
        }

        private static void ProgramInitialize()
        {
            while (true)
            {
                Console.WriteLine("Choose action: \n1.Show gift content \n2.Exit");
                int choose;
                if (int.TryParse(Console.ReadLine(), out choose))
                {
                    switch (choose)
                    {
                        case 1:
                            ChooseAction();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Wrong input data");
                            Console.WriteLine("-----------------------------");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input data");
                    Console.WriteLine("-----------------------------");
                }
            }
        }

        private static void ChooseAction()
        {
            Console.Clear();
            Console.WriteLine("Gift contains:");
            var i = 1;
            foreach(var giftComponent in Core.Gift)
            {
                ShowComponentInfo(giftComponent, i);
                i++;
            }

            while (true)
            {
                Console.WriteLine("Please choose what do you want to do? " +
                                  "\n1.Get total weight " +
                                  "\n2.Get total net weight " +
                                  "\n3.Sort by property " +
                                  "\n4.Find gifts by property " +
                                  "\n5.Show sweets that contains chocolate " +
                                  "\n6.Back");
                int choose;
                if (int.TryParse(Console.ReadLine(), out choose))
                {
                    switch (choose)
                    {
                        case 1:
                            var totalWeight = Core.Gift.Weight;
                            Console.Clear();
                            Console.WriteLine("\nTotal weight of the gift: {0}", totalWeight);
                            Console.WriteLine("-----------------------------");
                            break;
                        case 2:
                            var totalNetWeight = Core.Gift.NetWeight;
                            Console.Clear();
                            Console.WriteLine("\nTotal net weight of the gift: {0}", totalNetWeight);
                            Console.WriteLine("-----------------------------");
                            break;
                        case 3:
                            SortByProperty();
                            break;
                        case 4:
                            FindByProperty();
                            break;
                        case 5:
                            ShowChocolateComponents();
                            break;
                        case 6:
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("Wrong input data");
                            Console.WriteLine("-----------------------------");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input data");
                    Console.WriteLine("-----------------------------");
                }
            }
        }

        private static void ShowComponentInfo(IGiftComponent giftComponent, int i)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("{0}.{1} {2} {3} \n  Weight: {4}, Net weight: {5} ", i, giftComponent,
                giftComponent.Name, giftComponent.CompanyName, giftComponent.Weight, giftComponent.NetWeight);

            var contentInfo = string.Format("  Sugar: {0} ", giftComponent.Sugar);
            var chocolateComponent = giftComponent as IChocolate;
            if (chocolateComponent != null)
            {
                contentInfo += "Chocolate: " + chocolateComponent.Chocolate;
            }
            Console.WriteLine(contentInfo);

            Console.WriteLine("  Wrapper: {0}", giftComponent.Wrapper != null
            ? giftComponent.Wrapper.WrapperType.ToString()
            : "missing");
            Console.WriteLine();
        }

        private static void SortByProperty()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Sort by: " +
                                  "\n1.Weight " +
                                  "\n2.Sugar " +
                                  "\n3.Name " +
                                  "\n4.Company name " +
                                  "\n5.Wrapper type " +
                                  "\n6.Back");
                int choose;
                if (int.TryParse(Console.ReadLine(), out choose))
                {
                    List<IGiftComponent> sortedList;

                    switch (choose)
                    {

                        case 1:
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Sorted by weight");
                            sortedList = Core.Gift.OrderBy(component => component.Weight).ToList();
                            break;
                        case 2:
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Sorted by sugar");
                            sortedList = Core.Gift.OrderBy(component => component.Sugar).ToList();
                            break;
                        case 3:
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Sorted by name");
                            sortedList = Core.Gift.OrderBy(component => component.Name).ToList();
                            break;
                        case 4:
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Sorted by company name");
                            sortedList = Core.Gift.OrderBy(component => component.CompanyName).ToList();
                            break;
                        case 5:
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Sorted by wrapper type");
                            sortedList = Core.Gift.OrderBy(component => component, new GiftComponentComparerByWrapperType()).ToList();
                            break;
                        case 6:
                            Console.Clear();
                            return;
                        default:
                            Console.WriteLine("Wrong input data");
                            Console.WriteLine("-----------------------------");
                            continue;
                    }

                    var i = 1;
                    foreach (var giftComponent in sortedList)
                    {
                        ShowComponentInfo(giftComponent, i);
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input data");
                    Console.WriteLine("-----------------------------");
                }
            }
        }

        private static void FindByProperty()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Find by: " +
                                  "\n1.Weight " +
                                  "\n2.Sugar " +
                                  "\n3.Name " +
                                  "\n4.Company name " +
                                  "\n5.Wrapper type " +
                                  "\n6.Back");
                int choose;
                if (int.TryParse(Console.ReadLine(), out choose))
                {
                    IEnumerable<IGiftComponent> giftComponents;

                    switch (choose)
                    {

                        case 1:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Find by weight");
                                Console.Write("Enter weight from:");
                                int weightFrom;
                                int.TryParse(Console.ReadLine(), out weightFrom);
                                Console.Write("Enter weight to:");
                                int weightTo;
                                int.TryParse(Console.ReadLine(), out weightTo);
                                giftComponents = Core.Gift.Where(component =>
                                    weightFrom <= component.Weight && component.Weight <= weightTo);
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Find by sugar");
                                Console.Write("Enter sugar from:");
                                int sugarFrom;
                                int.TryParse(Console.ReadLine(), out sugarFrom);
                                Console.Write("Enter sugar to:");
                                int sugarTo;
                                int.TryParse(Console.ReadLine(), out sugarTo);
                                giftComponents = Core.Gift.Where(component =>
                                    sugarFrom <= component.Sugar && component.Sugar <= sugarTo);
                            }
                            break;
                        case 3:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Find by name");
                                Console.Write("Enter name:");
                                var name = Console.ReadLine();
                                giftComponents = Core.Gift.Where(component => 
                                    !string.IsNullOrEmpty(component.Name) && component.Name.Contains(name));
                            }
                            break;
                        case 4:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Find by company name");
                                Console.Write("Enter name:");
                                var name = Console.ReadLine();
                                giftComponents = Core.Gift.Where(component => 
                                    !string.IsNullOrEmpty(component.CompanyName) && component.CompanyName.Contains(name));
                            }
                            break;
                        case 5:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Find by wrapper type");
                                Console.WriteLine("1.Loose wrapper \n2.TightWrapper");
                                int wrapperType;
                                int.TryParse(Console.ReadLine(), out wrapperType);
                                switch (wrapperType)
                                {
                                    case 1:
                                        giftComponents = Core.Gift.Where(component => 
                                            component.Wrapper != null && component.Wrapper.WrapperType == WrapperType.LooseWrapper);
                                        break;
                                    case 2:
                                        giftComponents = Core.Gift.Where(component =>
                                            component.Wrapper != null && component.Wrapper.WrapperType == WrapperType.TightWrapper);
                                        break;
                                    default:
                                        continue;
                                }
                            }
                            break;
                        case 6:
                            Console.Clear();
                            return;
                        default:
                            Console.WriteLine("Wrong input data");
                            Console.WriteLine("-----------------------------");
                            continue;
                    }

                    if (giftComponents.Any())
                    {
                        Console.Clear();
                        var i = 1;
                        foreach (var giftComponent in giftComponents)
                        {
                            ShowComponentInfo(giftComponent, i);
                            i++;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Cannot find elements by search criteria");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Wrong input data");
                    Console.WriteLine("-----------------------------");
                }
            }
        }


        private static void ShowChocolateComponents()
        {
            Console.Clear();
            var chocolateComponents = Core.Gift.OfType<IChocolate>();
            if (chocolateComponents.Any())
            {
                var list = chocolateComponents.ToList();

                var i = 1;
                foreach (IGiftComponent giftComponent in list)
                {
                    ShowComponentInfo(giftComponent, i);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Sweets with chocolate doen't exists");
            }
        }
    }
}
