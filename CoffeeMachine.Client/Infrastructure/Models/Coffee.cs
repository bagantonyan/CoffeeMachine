using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Models
{
    public class Coffee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Water { get; set; }
        public double Sugar { get; set; }
        public double Coffees { get; set; }
        public decimal Price { get; set; }
    }
}
