using System;
using Jucardi.Inject.Attributes;
using Jucardi.Inject.stereotype;

namespace Jucardi.Inject.Test.Services
{
    [Service]
    public class SomeComponent
    {
        private readonly ISomeService someService;
        private readonly ISomeOtherService someOtherService;

        public SomeComponent([Qualifier("Bean2")] ISomeService someService, ISomeOtherService someOtherService) {
            this.someService = someService;
            this.someOtherService = someOtherService;
        }

        [PostConstruct]
        private void Init()
        {
            Console.WriteLine("Init Method!");
        }
    }
}
