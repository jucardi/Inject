using System.Reflection;

namespace Jucardi.Inject.Definitions
{
    internal class MethodDependencyInfo : AbstractDependencyInfo
    {

        private MethodInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        public MethodDependencyInfo(MethodInfo mInfo, string name, bool isPrimary = false) : base(name, isPrimary)
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
                return info.Invoke(Injector.GetConfiguration(info.DeclaringType), null);
            }

            return info.Invoke(Injector.GetConfiguration(info.DeclaringType), CreateParameters(parametersInfo));
        }
    }
}
