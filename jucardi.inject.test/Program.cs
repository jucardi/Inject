using System;
using Jucardi.Inject.Test.Services;

namespace Jucardi.Inject.Test
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
