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
        public static IServiceProvider ConfigureServices()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IProductDL, ProductDL>()
                .AddSingleton<IStoreDL, StoreDL>()
                .AddSingleton<ICoffeeMachineManager, CoffeeMachineManager>()
                .BuildServiceProvider();

            return provider;
        }

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
