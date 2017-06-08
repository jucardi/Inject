using System.Reflection;

namespace Jucardi.Inject.Definitions
{
    internal class ConstructorDependencyInfo : AbstractDependencyInfo
    {

        private readonly ConstructorInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="context">The application context</param>
        /// <param name="info">The <see cref="ConstructorInfo"/> to be used to create the bean instance.</param>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        /// <param name="initMethod">The init method to be invoked after the bean instance is created</param>
        public ConstructorDependencyInfo(ApplicationContext context, ConstructorInfo info, string name, bool isPrimary, string initMethod) 
            : base(context, name, isPrimary, initMethod)
        {
            this.info = info;
        }

        /// <summary>
        /// Create this instance.
        /// </summary>
        /// <returns>The create.</returns>
        public override object Create()
        {
            ParameterInfo[] parametersInfo = info.GetParameters();

            if (parametersInfo == null || parametersInfo.Length == 0)
            {
                return info.Invoke(null);
            }

            return info.Invoke(CreateParameters(parametersInfo));
        }
    }
}
