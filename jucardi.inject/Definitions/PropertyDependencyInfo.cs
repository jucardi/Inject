using System.Reflection;

namespace Jucardi.Inject.Definitions
{
    internal class PropertyDependencyInfo : AbstractDependencyInfo
    {

        private readonly PropertyInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/> to be used to create the bean instance</param>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        /// <param name="initMethod">The init method to be invoked after the bean instance is created</param>
        public PropertyDependencyInfo(PropertyInfo info, string name, bool isPrimary, string initMethod) : base(name, isPrimary, initMethod)
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
