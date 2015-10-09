using System;
using SweetGift.Data;

namespace SweetGift
{
    class Program
    {
        static void Main()
        {
            var sweetGift = new Gift()
            {
                new Biscuit() {Sugar = 4},
                new Candy() {Sugar = 3},
                new ChocolateEgg() {Sugar = 1},
                new Lollipop() {Sugar = 2}
            };
            foreach (var sweet in sweetGift)
            {
                Console.WriteLine(sweet);
            }

            sweetGift.SortBy("Sugar");

            foreach (var sweet in sweetGift)
            {
                Console.WriteLine(sweet);
            }
        }
    }
}
