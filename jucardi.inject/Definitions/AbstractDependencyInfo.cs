using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using jucardi.inject.Attributes;

namespace jucardi.inject.Definitions
{
    internal abstract class AbstractDependencyInfo : IDependencyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        protected AbstractDependencyInfo(string name, bool isPrimary = false)
        {
            this.Name = name;
            this.IsPrimary = isPrimary;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> is primary.
        /// </summary>
        /// <value><c>true</c> if is primary; otherwise, <c>false</c>.</value>
        public bool IsPrimary { get; private set; }


        public abstract object Create();

        protected object[] CreateParameters(ParameterInfo[] parametersInfo)
        {
            List<object> parameters = new List<object>();

            parametersInfo.ToList().ForEach(x =>
            {
                QualifierAttribute qualifierAttribute = x.GetCustomAttribute<QualifierAttribute>();
                string qualifier = qualifierAttribute != null ? qualifierAttribute.Name : null;
                parameters.Add(Injector.Resolve(x.ParameterType, qualifier));
            });

            return parameters.ToArray();
        }
    }
}
