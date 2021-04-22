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
        public void Start()
        {
            EnterCoins();
            ChooseCoffee();
        }

        public void EnterCoins()
        {
            var coins = new List<decimal> { 50, 100, 200, 500 };

            Console.WriteLine("Welcome to coffee machine program!");
            Console.WriteLine("Please enter coin(s), (50, 100, 200, 500) - only mentioned coins can be entered");
            Console.WriteLine("if it's enougth just press 'Enter' key");
            string input;
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
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Sorry, but wrong coin or invalid value has been entered.");
                    Console.WriteLine("Please try again, or tap 'Enter' key to choose a coffee.");
                }
            } while (!string.IsNullOrWhiteSpace(input));
        }

        public void ChooseCoffee()
        {
            string input;
            int coffeeNumber;
            var products = _productDL.GetAllProducts();

            Console.WriteLine("Please, choose the coffee (enter the coffee number from 1-10)");

            foreach (var product in products)
            {
                Console.WriteLine($"1 ==> {product.Name} - {product.Price} dram");
            }

            do
            {
                Console.Write("Enter the number of coffee ==> ");
                input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out coffeeNumber);

                if (isNumber && coffeeNumber >= 1 && coffeeNumber <= 10)
                {
                    var product = _productDL.GetProductById(coffeeNumber);

                    if (balance < product.Price)
                    {
                        Console.WriteLine("Not enough money!");
                    }
                    else
                    {
                        var store = _storeDL.GetStore();

                        if (store.Water >= product.Water && store.Sugar >= product.Sugar && store.Coffee >= product.Coffee)
                        {
                            _storeDL.TakeIngridients(store,product);
                            balance -= product.Price;

                            var coffee = _coffeeFactory.Create(coffeeNumber);
                            Console.WriteLine(coffee.Make());

                            Thread.Sleep(2000);
                            Console.WriteLine("Coffee is ready! please take");
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Sorry, but wrong number or invalid value has been entered.");
                    Console.WriteLine("Please try again, or tap 'Enter' key to take a change.");
                }
            } while (!string.IsNullOrWhiteSpace(input));
        }
    }
}
