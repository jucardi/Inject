using System;

namespace jucardi.inject.Attributes
{
    /// <summary>
    /// Indicates that a method produces a bean to be managed by Inject.
    /// 
    /// While a <c>Name</c> attribute is available, the default strategy for 
    /// determining the name of a bean is to use the name of the  method. 
    /// This is convenient and intuitive, but if explicit naming is desired, 
    /// the <c>Name</c> Property may be used.
    /// 
    /// <see cref="BeanAttribute"/> methods are declared within 
    /// <see cref="ConfigurationAttribute"/> classes. In this case, bean methods
    /// may reference other <see cref="BeanAttribute"/> methods in the same
    /// class by calling them directly.This ensures that references between
    /// beans are strongly typed and navigable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class BeanAttribute : Attribute
    {
        public string Name { get; set; }
        public string InitMethod { get; set; }
    }
}
