using CoffeeMachine.Client.Factory;
using CoffeeMachine.Client.Infrastructure.Data;
using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoffeeMachine.Client
{
    public class Startup
    {
        // add dependencies
        public static IServiceProvider ConfigureServices()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IProductDL, ProductDL>()
                .AddSingleton<IStoreDL, StoreDL>()
                .AddSingleton<ICoffeeFactory, CoffeeFactory>()
                .AddSingleton<ICoffeeMachineManager, CoffeeMachineManager>()
                .BuildServiceProvider();

            return provider;
        }

        // add appsettings.json and get connection string
        public static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("DefaultConnection");

            return strConnection;
        }
    }
}
