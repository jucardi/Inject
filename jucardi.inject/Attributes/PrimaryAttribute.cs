using System;

namespace jucardi.inject.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class PrimaryAttribute : Attribute
    {
    }
}
