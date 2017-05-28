using System;
using jucardi.inject.Attributes;
using jucardi.inject.stereotype;

namespace jucardi.inject.test.Services
{
    [Service]
    public class SomeComponent2
    {
        [Autowired]
        [Qualifier("Bean2")]
        private ISomeService someService;

        [Autowired]
        private ISomeOtherService someOtherService;
    }
}
