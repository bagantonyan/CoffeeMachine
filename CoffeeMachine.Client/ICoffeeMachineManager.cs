using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client
{
    public interface ICoffeeMachineManager
    {
        void Start();
        void EnterCoins();
        void ChooseCoffee();
    }
}
