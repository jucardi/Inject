using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jucardi.Inject.Attributes;

namespace Jucardi.Inject.Definitions
{
    internal abstract class AbstractDependencyInfo : IDependencyInfo
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> class.
        /// </summary>
        /// <param name="context">The application context</param>
        /// <param name="name">Name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        /// <param name="initMethod">Indicates the init method to be invoked after the bean is created</param>
        protected AbstractDependencyInfo(ApplicationContext context, string name, bool isPrimary, string initMethod)
        {
            this.Context = context;
            this.Name = name;
            this.IsPrimary = isPrimary;
            this.InitMethod = initMethod;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bean name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> is primary.
        /// </summary>
        /// <value><c>true</c> if is primary; otherwise, <c>false</c>.</value>
        public bool IsPrimary { get; private set; }

        /// <summary>
        /// Inidicates the method to be invoked to initialize the class after all dependencies have been injected.
        /// </summary>
        /// <value>The init method.</value>
        public string InitMethod { get; private set; }

        /// <summary>
        /// Gets the parent container.
        /// </summary>
        /// <value>The parent container.</value>
        public ApplicationContext Context { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new instance of the bean defined by this this isntance of <see cref="IDependencyInfo"/>
        /// </summary>
        /// <returns>The create.</returns>
        public abstract object Create();

        /// <summary>
        /// Creates the parameters to be used for the method or constructor to create the bean instance.
        /// </summary>
        /// <returns>The parameters.</returns>
        /// <param name="parametersInfo">Parameters info.</param>
        protected object[] CreateParameters(ParameterInfo[] parametersInfo)
        {
            List<object> parameters = new List<object>();

            parametersInfo.ToList().ForEach(x =>
            {
                QualifierAttribute qualifierAttribute = x.GetCustomAttribute<QualifierAttribute>();
                string qualifier = qualifierAttribute != null ? qualifierAttribute.Name : null;
                parameters.Add(Context.Resolve(x.ParameterType, qualifier));
            });

            return parameters.ToArray();
        }

        #endregion
    }
}
