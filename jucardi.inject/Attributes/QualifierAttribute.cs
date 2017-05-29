using System;

namespace Jucardi.Inject.Attributes
{
    /// <summary>
    /// This annotation may be used on a field or parameter as a qualifier for
    /// beans when autowiring.It may also be used to annotate other
    /// custom annotations that can then in turn be used as qualifiers.
    /// 
    /// <see cref="AutowiredAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class QualifierAttribute : Attribute
    {
        public string Name { get; set; }

        public QualifierAttribute(string name)
        {
            this.Name = name;
        }
    }
}
