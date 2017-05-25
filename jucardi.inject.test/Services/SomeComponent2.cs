using System;
using jucardi.inject.Attributes;
using jucardi.inject.stereotype;

namespace jucardi.inject.test.Services
{
    [Service]
    public class SomeComponent
    {
        private readonly ISomeService someService;
        private readonly ISomeOtherService someOtherService;

        public SomeComponent([Qualifier("Bean3")] ISomeService someService, ISomeOtherService someOtherService) {
            this.someService = someService;
            this.someOtherService = someOtherService;
        }
    }
}
