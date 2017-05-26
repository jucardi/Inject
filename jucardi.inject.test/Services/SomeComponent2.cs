using System;
using jucardi.inject.Attributes;
using jucardi.inject.stereotype;

namespace jucardi.inject.test.Services
{
    [Service]
    public class SomeComponent2
    {
        [Autowire]
        [Qualifier("Bean2")]
        private ISomeService someService;

        [Autowire]
        private ISomeOtherService someOtherService;
    }
}
