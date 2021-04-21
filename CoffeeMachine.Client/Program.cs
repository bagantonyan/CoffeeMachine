using System;
using System.Collections.Generic;

namespace CoffeeMachine.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute();
        }

        static void EnterCoins()
        {
            var coins = new List<decimal> { 50, 100, 200, 500 };

            Console.WriteLine("Welcome to coffee machine program!");
            Console.WriteLine("Please enter coin(s), (50, 100, 200, 500) - only mentioned coins can be entered");
            Console.WriteLine("if it's enougth just press 'Enter' key");
            string input;
            decimal balance = 0;
            decimal coin;

            do
            {
                Console.WriteLine($"Balance - {balance}");
                Console.Write("===== Insert coin => ");
                input = Console.ReadLine();
                bool isNumber = decimal.TryParse(input, out coin);

                if (isNumber && coins.Contains(coin))
                {
                    balance += coin;
                    continue;
                }
                else if(!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Sorry, but wrong coin or invalid value has been entered.");
                    Console.WriteLine("Please try again, or tap 'Enter' key to choose a coffee.");
                }
            } while (!string.IsNullOrWhiteSpace(input));
        }
    }
}
