using System;

namespace jucardi.inject.stereotype
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public string Value { get; set; }
    }
}
