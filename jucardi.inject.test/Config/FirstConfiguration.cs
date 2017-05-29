using System;
using Jucardi.Inject.Attributes;
using Jucardi.Inject.Test.Services;

namespace Jucardi.Inject.Test.Config
{
    [Configuration]
    public class FirstConfiguration
    {
        [Primary]
        [Bean]
        public ISomeService Bean1()
        {
            return new SomeServiceImpl1();
        }

        [Bean]
        public ISomeService Bean2()
        {
            return new SomeServiceImpl2();
        }

        [Bean]
        public ISomeOtherService OtherService() {
            return new SomeOtherServiceImpl1();
        }
    }
}
