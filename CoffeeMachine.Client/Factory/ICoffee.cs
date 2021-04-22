using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Factory
{
    public interface ICoffee
    {
        string Make();
    }

    public class Black : ICoffee
    {
        public string Make() => "Making Black";
    }

    public class Latte : ICoffee
    {
        public string Make() => "Making Latte";
    }

    public class Cappuccino : ICoffee
    {
        public string Make() => "Making Cappuccino";
    }

    public class Americano : ICoffee
    {
        public string Make() => "Making Americano";
    }

    public class Espresso : ICoffee
    {
        public string Make() => "Making Espresso";
    }

    public class Doppio : ICoffee
    {
        public string Make() => "Making Doppio";
    }

    public class Cortado : ICoffee
    {
        public string Make() => "Making Cortado";
    }

    public class Lungo : ICoffee
    {
        public string Make() => "Making Lungo";
    }

    public class Mocha : ICoffee
    {
        public string Make() => "Making Mocha";
    }

    public class Irish : ICoffee
    {
        public string Make() => "Making Irish";
    }
}
