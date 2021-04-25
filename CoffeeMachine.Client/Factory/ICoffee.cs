using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine.Client.Factory
{
    public interface ICoffee
    {
        void Make();
        void Take();
    }

    public class Black : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Black...");
        public void Take() => Console.WriteLine("...Black is ready, please take...");
    }

    public class Latte : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Latte...");
        public void Take() => Console.WriteLine("...Latte is ready, please take...");
    }

    public class Cappuccino : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Cappuccino...");
        public void Take() => Console.WriteLine("...Cappuccino is ready, please take...");
    }

    public class Americano : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Americano...");
        public void Take() => Console.WriteLine("...Americano is ready, please take...");
    }

    public class Espresso : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Espresso...");
        public void Take() => Console.WriteLine("...Espresso is ready, please take...");
    }

    public class Doppio : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Doppio...");
        public void Take() => Console.WriteLine("...Doppio is ready, please take...");
    }

    public class Cortado : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Cortado...");
        public void Take() => Console.WriteLine("...Cortado is ready, please take...");
    }

    public class Lungo : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Lungo...");
        public void Take() => Console.WriteLine("...Lungo is ready, please take...");
    }

    public class Mocha : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Mocha...");
        public void Take() => Console.WriteLine("...Mocha is ready, please take...");
    }

    public class Irish : ICoffee
    {
        public void Make() => Console.WriteLine("...Making Irish...");
        public void Take() => Console.WriteLine("...Irish is ready, please take...");
    }
}
