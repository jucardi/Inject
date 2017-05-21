using System;
using jucardi.inject.Attributes;
using jucardi.inject.test.Services;

namespace jucardi.inject.test.Config
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
