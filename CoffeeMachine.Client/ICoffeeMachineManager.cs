using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client
{
    public interface ICoffeeMachineManager
    {
        void Execute();
        void EnterCoins();
        void StartCoffeeMachine();
    }
}
