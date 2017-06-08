using System;
using Jucardi.Inject.Test.Services;

namespace Jucardi.Inject.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext.Default.Scan("jucardi.inject.test");
            SomeComponent component = ApplicationContext.Default.Resolve<SomeComponent>();
            SomeComponent2 component2 = ApplicationContext.Default.Resolve<SomeComponent2>();
            Console.WriteLine("Hello World!");
        }
    }
}
