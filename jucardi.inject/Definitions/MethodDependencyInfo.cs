using System.Reflection;

namespace Jucardi.Inject.Definitions
{
    internal class MethodDependencyInfo : AbstractDependencyInfo
    {

        private readonly MethodInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="context">The application context</param>
        /// <param name="mInfo">The <see cref="MethodInfo"/> to be used to create the bean instance.</param>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        /// <param name="initMethod">The init method to be invoked after the bean instance is created</param>
        public MethodDependencyInfo(ApplicationContext context, MethodInfo mInfo, string name, bool isPrimary, string initMethod)
            : base(context, name, isPrimary, initMethod)
        {
            this.info = mInfo;
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
                return info.Invoke(Context.GetConfiguration(info.DeclaringType), null);
            }

            return info.Invoke(Context.GetConfiguration(info.DeclaringType), CreateParameters(parametersInfo));
        }
    }
}
