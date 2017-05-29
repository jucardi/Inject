using System;

namespace Jucardi.Inject.Attributes
{
    /// <summary>
    /// Indicates one or more <see cref="ConfigurationAttribute"/> classes to
    /// import.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportAttribute : Attribute
    {
        public Type[] Value { get; set; }

        // TODO: Implement logic
    }
}
