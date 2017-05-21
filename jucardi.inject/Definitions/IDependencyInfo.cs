using System;

namespace jucardi.inject.Definitions
{
    internal interface IDependencyInfo
    {
        String Name { get; }
        bool IsPrimary { get; }
        object Create();
    }
}
