using CoffeeMachine.Client.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data.Interfaces
{
    public interface IStoreDL
    {
        Store GetStore();
        void TakeIngridients(Coffee product);
        void RechargeStore();
    }
}
