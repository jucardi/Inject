using System.Reflection;

namespace jucardi.inject.Definitions
{
    internal class PropertyDependencyInfo : AbstractDependencyInfo
    {

        private readonly PropertyInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        public PropertyDependencyInfo(PropertyInfo info, string name, bool isPrimary = false) : base(name, isPrimary)
        {
            this.info = info;
        }

        /// <summary>
        /// Create this instance.
        /// </summary>
        /// <returns>The create.</returns>
        public override object Create()
        {
            return info.GetValue(Injector.GetConfiguration(info.DeclaringType));
        }
    }
}
