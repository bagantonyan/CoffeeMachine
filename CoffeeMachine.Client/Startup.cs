using CoffeeMachine.Client.Factory;
using CoffeeMachine.Client.Infrastructure.Data;
using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;

namespace CoffeeMachine.Client
{
    public class Startup
    {
        /// <summary>
        /// Adds dependencies
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider ConfigureServices()
        {
            var provider = new ServiceCollection()
                .AddSingleton<ICoffeeDL, CoffeeDL>()
                .AddSingleton<IStoreDL, StoreDL>()
                .AddSingleton<ICoffeeFactory, CoffeeFactory>()
                .AddSingleton<ICoffeeMachineManager, CoffeeMachineManager>()
                .BuildServiceProvider();

            return provider;
        }

        /// <summary>
        /// Makes and returns the connection string
        /// </summary>
        /// <returns></returns>
        public static string GetDbConnection()
        {
            string path = Path.GetFullPath("CoffeeMachineDB.db");

            string strConnection = "Data Source=" + path.Split(new string[] { "bin" }, StringSplitOptions.None)[0] + "CoffeeMachineDB.db";

            return strConnection;
        }
    }
}
