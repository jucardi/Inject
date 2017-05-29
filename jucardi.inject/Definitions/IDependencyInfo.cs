using System;

namespace Jucardi.Inject.Definitions
{
    internal interface IDependencyInfo
    {
        String Name { get; }
        bool IsPrimary { get; }
        object Create();
    }
}
