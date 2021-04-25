using CoffeeMachine.Client.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data.Interfaces
{
    public interface ICoffeeDL
    {
        IEnumerable<Coffee> GetAllCoffees();
        Coffee GetCoffeeById(int id);
    }
}
