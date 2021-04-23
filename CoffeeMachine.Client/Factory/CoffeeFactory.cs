using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Factory
{
    // simple factory pattern
    public class CoffeeFactory : ICoffeeFactory
    {
        public ICoffee Create(int coffeeNumber)
        {
            switch (coffeeNumber)
            {
                case 1:
                    return new Black();
                case 2:
                    return new Latte();
                case 3:
                    return new Cappuccino();
                case 4:
                    return new Americano();
                case 5:
                    return new Espresso();
                case 6:
                    return new Doppio();
                case 7:
                    return new Cortado();
                case 8:
                    return new Lungo();
                case 9:
                    return new Mocha();
                case 10:
                    return new Irish();
                default:
                    throw new ArgumentException($"Wrong coffee number - {coffeeNumber}");
            }
        }
    }
}
