using System;

namespace jucardi.inject.Attributes
{
    /// <summary>
    /// Marks a constructor, field, property setter or config method as to be
    /// autowired by Inject's dependency injection facilities.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Constructor)]
    public class AutowiredAttribute : Attribute
    {
    }
}
