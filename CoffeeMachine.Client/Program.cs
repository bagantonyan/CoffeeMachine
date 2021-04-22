using CoffeeMachine.Client.Infrastructure.Data;
using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace CoffeeMachine.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.ConfigureServices();

            var manager = container.GetRequiredService<ICoffeeMachineManager>();

            manager.Start();
        }
    }
}
