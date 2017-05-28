using System;

namespace jucardi.inject.Attributes
{
    /// <summary>
    /// Indicates that a bean should be given preference when multiple
    /// candidates are qualified to autowire a single-valued dependency.If 
    /// exactly one 'primary' bean exists among the candidates, it will be the 
    /// autowired value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class PrimaryAttribute : Attribute
    {
    }
}
