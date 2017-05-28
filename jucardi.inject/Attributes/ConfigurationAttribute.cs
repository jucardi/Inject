using System;

namespace jucardi.inject.Attributes
{
    /// <summary>
    /// Indicates that a class declares one or more <see cref="BeanAttribute"/> 
    /// methods and may be processed by Inject to generate bean definitions and
    /// service requests for those beans at runtime.
    /// 
    /// <see cref="ConfigurationAttribute"/> classes may be composed using the
    /// <see cref="ImportAttribute"/> attribute to explicitly indicate loading
    /// the configuration class requires the classes specified in the
    /// <see cref="ImportAttribute"/> to be loaded first.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationAttribute : Attribute
    {

    }
}
