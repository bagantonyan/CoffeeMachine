using CoffeeMachine.Client.Factory;
using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
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
        private readonly IProductDL _productDL;
        private readonly IStoreDL _storeDL;
        private readonly ICoffeeFactory _coffeeFactory;

        public CoffeeMachineManager()
        {
            balance = 0;
            var container = Startup.ConfigureServices();
            _productDL = container.GetRequiredService<IProductDL>();
            _storeDL = container.GetRequiredService<IStoreDL>();
            _coffeeFactory = container.GetRequiredService<ICoffeeFactory>();
        }

        public void Execute()
        {
            StartCoffeeMachine();
        }

        // method for entering coins in coffee machine
        public void EnterCoins()
        {
            var coins = new List<decimal> { 50, 100, 200, 500 };

            Console.WriteLine("==> Please enter coin(s), (50, 100, 200, 500)\n"+
                              "==> only mentioned coins can be entered\n"+
                              "==> if it's enough just enter 0\n");

            string input;
            decimal coin;

            do
            {
                Console.Write("==> Insert coin => ");
                input = Console.ReadLine();
                bool isNumber = decimal.TryParse(input, out coin);

                if (isNumber && coins.Contains(coin))
                {
                    balance += coin;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"---Balance - {balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (input == "0")
                {
                    if (balance == 0)
                        Console.WriteLine("---Your balance is 0, please insert coins");
                    else
                        break;
                }
                else
                {
                    Console.WriteLine("---Sorry, but wrong coin or invalid value has been entered.");
                    Console.WriteLine("---Please try again, or tap 'Enter' key to choose a coffee.");
                }
            } while (input != "0" || balance == 0);
        }

        // method for executing the logic of coffee machine work
        public void StartCoffeeMachine()
        {
            do
            {
                Console.WriteLine();
                EnterCoins();

                string input;
                int coffeeNumber;
                var products = _productDL.GetAllProducts();

                Console.WriteLine();
                Console.WriteLine("==> Please, choose the coffee\n" +
                                  "==> Enter the coffee number from 1-10\n" +
                                  "==> Or Enter 0 to take your money back");
                Console.WriteLine();

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id} ==> {product.Name} - {product.Price} dram");
                }

                Console.WriteLine();

                do
                {
                    Console.Write($"Enter the number of coffee ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"(Balance - {balance})");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ==> ");

                    input = Console.ReadLine();
                    bool isNumber = int.TryParse(input, out coffeeNumber);

                    if (isNumber && coffeeNumber >= 1 && coffeeNumber <= 10)
                    {
                        var product = _productDL.GetProductById(coffeeNumber);

                        if (balance < product.Price)
                        {
                            Console.WriteLine("---Not enough money!");
                            Console.WriteLine("---Choose another coffee,\n" +
                                              "---Or enter '0' to take a money\n" +
                                              "---Or enter 'c' to insert coins\n");
                        }
                        else
                        {
                            var store = _storeDL.GetStore();

                            if (store.Water >= product.Water && store.Sugar >= product.Sugar && store.Coffee >= product.Coffee)
                            {
                                _storeDL.TakeIngridients(store, product);
                                balance -= product.Price;

                                var coffee = _coffeeFactory.Create(coffeeNumber);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine(coffee.Make());
                                Console.ForegroundColor = ConsoleColor.White;


                                Thread.Sleep(2000);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{product.Name} is ready! please take\n");
                                Console.ForegroundColor = ConsoleColor.White;

                                if (balance == 0)
                                {
                                    Console.WriteLine("---Your balance is 0\n");
                                    break;
                                }

                                Console.WriteLine("---Choose another coffee,\n" +
                                                  "---Or enter '0' to take a money\n" +
                                                  "---Or enter 'c' to insert coins\n");
                            }
                            else
                            {
                                Console.WriteLine("---Not enough ingridients to make a coffee");
                                Console.WriteLine("---You can choose another coffee,\n" +
                                                  "---or enter '0' to take a money,\n" +
                                                  "---or enter 'r' to recharge the machine with ingridients\n");
                            }
                        }
                    }
                    else if (input == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"---Take your change - {balance} dram\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        balance = 0;
                    }
                    else if (!string.IsNullOrWhiteSpace(input) && input != "r" && input != "c")
                    {
                        Console.WriteLine("---Sorry, but wrong number or invalid value has been entered.");
                        Console.WriteLine("---Please try again, or tap 'Enter' key to take a change.");
                    }
                    else if(input == "r")
                    {
                        _storeDL.RechargeMachine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("---Coffee machine has been recharged with ingridients\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        break;
                    }
                } while (!string.IsNullOrWhiteSpace(input) && input != "0");
            } while (true);
        }
    }
}
