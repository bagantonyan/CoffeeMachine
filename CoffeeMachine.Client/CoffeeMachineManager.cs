using CoffeeMachine.Client.Factory;
using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using CoffeeMachine.Client.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoffeeMachine.Client
{
    public class CoffeeMachineManager : ICoffeeMachineManager
    {
        private decimal balance;
        private readonly ICoffeeDL _coffeeDL;
        private readonly IStoreDL _storeDL;
        private readonly ICoffeeFactory _coffeeFactory;

        public CoffeeMachineManager()
        {
            balance = 0;
            var container = Startup.ConfigureServices();
            _coffeeDL = container.GetRequiredService<ICoffeeDL>();
            _storeDL = container.GetRequiredService<IStoreDL>();
            _coffeeFactory = container.GetRequiredService<ICoffeeFactory>();
        }

        /// <summary>
        /// Starts coffee machine
        /// </summary>
        public void Execute()
        {
            // Rechareges the store with default values
            _storeDL.RechargeStore();
            
            do
            {
                InsertCoins();
                Console.WriteLine();
                ChooseCoffee();
            } while (true);
        }

        #region "Coins' region"

        /// <summary>
        /// Inserts coin(s)
        /// </summary>
        private void InsertCoins()
        {
            Console.WriteLine("\n==> Insert coin(s), (50, 100, 200, 500)\n" +
                                "==> Insert 'c' to choose a coffee\n" +
                                "==> Insert '0' to take your change\n");

            string input;
            decimal coin;

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"---Balance - {balance}");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("==> Insert => ");
                input = Console.ReadLine();

                if (CheckCoin(input, out coin))
                    balance += coin;
                else if (input == "c")
                {
                    if (balance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("---Your balance is 0, please insert coins");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        return;
                }
                else if (input == "0")
                {
                    if (balance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("---Your balance is 0, please insert coins");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"----Take your change - {balance} dram\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        balance = 0;
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("---Wrong coin or invalid value has been entered.\n" +
                                      "---Try again, or enter '0' to choose a coffee.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (input != "0" || balance == 0);
        }

        /// <summary>
        /// Checks if inserted coin is valid
        /// </summary>
        private bool CheckCoin(string input, out decimal coin)
        {
            var coins = new List<decimal> { 50, 100, 200, 500 };

            bool isNumber = decimal.TryParse(input, out coin);

            if (isNumber && coins.Contains(coin))
                return true;
            return false;
        }

        #endregion "Coins' region"

        #region "Coffees' region"

        /// <summary>
        /// Chooses coffee and makes it 
        /// if the requirements are true
        /// </summary>
        private void ChooseCoffee()
        {

            do
            {
                bool isStoreAvailable = CheckStoreAvailability();
                if (!isStoreAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("---No coffee available\n" +
                                      "---Come and try later");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }

                Console.WriteLine("---Choose coffee, availables are marked white\n" +
                                  "---Or enter '0' to take your change\n" +
                                  "---Or enter 'c' to insert coins\n");

                ShowCoffees();

                string input;
                int coffeeNumber;

                do
                {
                    Console.WriteLine();
                    Console.Write($"Enter the number of coffee ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"(Balance - {balance})");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ==> ");

                    input = Console.ReadLine();

                    if (CheckCoffeeNumber(input, out coffeeNumber))
                    {
                        if (!CheckBalance(coffeeNumber))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("---Not enough money!\n" +
                                              "---Choose another coffee\n" +
                                              "---Or enter '0' to take your change\n" +
                                              "---Or enter 'c' to insert coins\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            if (CheckCoffeeAvailability(coffeeNumber))
                            {
                                MakeCoffee(coffeeNumber);
                                Console.WriteLine();
                                if (balance == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("---Your balance is 0, please insert coins");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    return;
                                }
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("---Not enough ingridients\n" +
                                                  "---You can choose another coffee\n" +
                                                  "---Or enter '0' to take your change\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                    }
                    else if (input == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"---Take your change - {balance} dram\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        balance = 0;
                        return;
                    }
                    else if (input == "c")
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("---Wrong number or invalid value has been entered.\n" +
                                          "---Try again, or enter '0' to take a change.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (input != "0" && input != "c");
            } while (true);
        }

        /// <summary>
        /// Prints coffees' list, and mark the coffee with red, 
        /// if its not available with current balance or ingridients
        /// </summary>
        private void ShowCoffees()
        {
            var coffees = _coffeeDL.GetAllCoffees();

            var store = _storeDL.GetStore();

            foreach (var coffee in coffees)
            {
                Console.Write($"{coffee.Id} ==> ");
                if (coffee.Water <= store.Water &&
                    coffee.Sugar <= store.Sugar &&
                    coffee.Coffees <= store.Coffees && balance >= coffee.Price)
                {
                    Console.WriteLine($"{coffee.Name} - {coffee.Price} dram");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{coffee.Name} - {coffee.Price} dram");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// Checks if ingridients are enough to make at least one coffee,
        /// and returns true, if not returs false
        /// </summary>
        /// <returns></returns>
        private bool CheckStoreAvailability()
        {
            var coffees = _coffeeDL.GetAllCoffees();
            var store = _storeDL.GetStore();

            foreach (var coffee in coffees)
            {
                if (coffee.Water <= store.Water &&
                    coffee.Sugar <= store.Sugar &&
                    coffee.Coffees <= store.Coffees)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if inserted coffee number is valid
        /// </summary>
        private bool CheckCoffeeNumber(string input, out int coffeeNumber)
        {
            bool isNumber = int.TryParse(input, out coffeeNumber);

            if (isNumber && coffeeNumber >= 1 && coffeeNumber <= 10)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if balance is enough for choosing coffee
        /// </summary>
        private bool CheckBalance(int coffeeNumber)
        {
            var coffee = _coffeeDL.GetCoffeeById(coffeeNumber);
            if (balance < coffee.Price)
                return false;
            return true;
        }

        /// <summary>
        /// Checks if ingridients are enough to make a coffee
        /// </summary>
        private bool CheckCoffeeAvailability(int coffeeNumber)
        {
            var coffee = _coffeeDL.GetCoffeeById(coffeeNumber);
            var store = _storeDL.GetStore();

            if (store.Water >= coffee.Water &&
                store.Sugar >= coffee.Sugar &&
                store.Coffees >= coffee.Coffees)
                return true;
            return false;
        }

        /// <summary>
        /// Makes the choosen coffee
        /// </summary>
        private void MakeCoffee(int coffeeNumber)
        {
            var coffee = _coffeeDL.GetCoffeeById(coffeeNumber);
            _storeDL.TakeIngridients(coffee);
            balance -= coffee.Price;

            var coffeeToMake = _coffeeFactory.Create(coffeeNumber);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            coffeeToMake.Make();
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Green;
            coffeeToMake.Take();
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion "Coffees' Region"
    }
}
