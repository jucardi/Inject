using System;
using jucardi.inject.test.Services;

namespace jucardi.inject.test
{
    class Program
    {
        static void Main(string[] args)
        {
            Injector.Scan("jucardi.inject.test");
            SomeComponent component = Injector.Resolve<SomeComponent>();
            SomeComponent2 component2 = Injector.Resolve<SomeComponent2>();
            Console.WriteLine("Hello World!");
        }
    }
}
