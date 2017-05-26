using System;

namespace jucardi.inject.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class QualifierAttribute : Attribute
    {
        public string Name { get; set; }

        public QualifierAttribute(string name) {
            this.Name = name;
        }
    }
}
