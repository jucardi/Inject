using System;

namespace jucardi.inject.test
{
    class Program
    {
        static void Main(string[] args)
        {
            Injector.Load("jucardi.inject.test");
            Console.WriteLine("Hello World!");
        }
    }
}
