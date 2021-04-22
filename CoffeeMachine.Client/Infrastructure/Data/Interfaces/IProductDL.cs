using CoffeeMachine.Client.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data.Interfaces
{
    public interface IProductDL
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
