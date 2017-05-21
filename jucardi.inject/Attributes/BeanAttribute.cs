using System;

namespace jucardi.inject.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class BeanAttribute : Attribute
    {
        public string Name { get; set; }
        public string InitMethod { get; set; }
        public string DestroyMethod { get; set; }
    }
}
