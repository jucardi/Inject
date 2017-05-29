using System;

namespace Jucardi.Inject.stereotype
{
    /// <summary>
    /// Indicates that an annotated class is a "Service". This annotation serves
    /// as a specialization of <see cref="ComponentAttribute"/>, allowing for
    /// implementation classes to be autodetected through classpath scanning.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : ComponentAttribute
    {
    }
}
