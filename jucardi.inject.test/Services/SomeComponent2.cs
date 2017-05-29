using System;
using Jucardi.Inject.Attributes;
using Jucardi.Inject.stereotype;

namespace Jucardi.Inject.Test.Services
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
