using System;


namespace jucardi.inject.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class AutowireAttribute : Attribute
    {
    }
}
