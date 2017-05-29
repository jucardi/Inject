using System;

namespace Jucardi.Inject.Test.Services
{
    public class SomeServiceImpl2 : ISomeService
    {
        public string Id => "Service2";

        private void Init()
        {
            Console.WriteLine("Some Service Init!");
        }
    }
}
