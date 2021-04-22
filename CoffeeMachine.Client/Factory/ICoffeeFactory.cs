using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Factory
{
    public interface ICoffeeFactory
    {
        ICoffee Create(int coffeeNumber);
    }
}
