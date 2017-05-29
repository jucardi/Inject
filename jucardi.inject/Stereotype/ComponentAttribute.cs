using System;

namespace Jucardi.Inject.stereotype
{
    /// <summary>
    /// 
    /// Indicates that an annotated class is a "Component". Such classes are
    /// considered as candidates for auto-detection when using annotation-based
    /// configuration and classpath scanning.
    /// 
    /// Other class-level annotations may be considered as identifying a
    /// component as well
    /// 
    /// <see cref="RepositoryAttribute"/>
    /// <see cref="ServiceAttribute"/> 
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// The value may indicate a suggestion for a logical component name,
        /// to be turned into a <c>Bean</c> in case of an autodetected
        /// component.
        /// </summary>
        /// 
        /// <value>The suggested component name, if any.</value>
        public string Value { get; set; }
    }
}
