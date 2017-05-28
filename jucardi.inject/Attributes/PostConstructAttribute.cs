using System;
namespace jucardi.inject.Attributes
{
    /// <summary>
    /// The PostConstruct attribute is used on a method that needs to be
    /// executed after dependency injection is done to perform any
    /// initialization.
    /// 
    /// The method MUST NOT have any parameters.
    /// 
    /// The method on which PostConstruct is applied MAY be public, protected, 
    /// internal or private. Cannot be static.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostConstructAttribute : Attribute
    {
    }
}
