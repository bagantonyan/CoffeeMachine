using CoffeeMachine.Client.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data.Interfaces
{
    public interface IStoreDL
    {
        void TakeIngridients(Store store, Product product);
        Store GetStore();
    }
}
